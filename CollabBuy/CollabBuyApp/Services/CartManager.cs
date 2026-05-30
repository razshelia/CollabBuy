using CollabBuy.CollabBuyApp.Models;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Services
{
    /// <summary>
    /// Kelas Manager untuk mengelola Keranjang Kolektif di memori (RAM).
    /// Kelas ini TIDAK ADA di database. Murni dibuat untuk mengatur alur sistem.
    /// 
    /// Membuktikan Sub-bab 3.1: Struktur Data Terarah In-Memory.
    /// Menggunakan Dictionary untuk mengelompokkan pesanan berdasarkan ID Produk,
    /// sehingga efisien saat mengecek kuota Gotong Royong sebelum di-flush ke DB.
    /// </summary>
    public class CartManager
    {
        // === PRIVATE FIELDS ===
        private int _idPembeli;

        // Struktur Data In-Memory: Dictionary<int, List<TransactionDetail>>
        // Key = ID Produk, Value = List detail pesanan untuk produk tersebut
        private Dictionary<int, List<TransactionDetail>> _keranjangDict;

        // === KONSTRUKTOR ===
        public CartManager(int idPembeli)
        {
            _idPembeli = idPembeli;
            _keranjangDict = new Dictionary<int, List<TransactionDetail>>();
        }


        // === METHOD BISNIS LOGIC ===

        /// <summary>
        /// Menambahkan item titipan ke keranjang di RAM.
        /// Akan menolak jika produk bukan dari PO yang aktif.
        /// </summary>
        public void TambahItem(Product produk, string namaPenitip, int jumlah, string catatan)
        {
            if (produk == null)
            {
                throw new ArgumentNullException("Produk tidak boleh kosong!", "produk");
            }

            // Validasi Bisnis: Cek apakah PO aktif (Menggantikan trigger trg_cek_waktu_po)
            if (produk.GetIdPo().HasValue == false)
            {
                throw new InvalidOrderException("Produk ini tidak terikat dalam sesi Pre-Order!", "id_po", "CART_NO_PO");
            }

            // Buat detail pesanan baru di RAM
            TransactionDetail detailBaru = new TransactionDetail(produk.GetIdProduk(), namaPenitip, jumlah);
            detailBaru.SetCatatan(catatan);
            detailBaru.SetProduk(produk); // Set referensi ke objek Product (In-Memory)
            detailBaru.Validate();

            // Cek apakah produk ini sudah ada di Dictionary
            int keyProduk = produk.GetIdProduk();
            if (_keranjangDict.ContainsKey(keyProduk))
            {
                // Jika sudah ada, tambahkan detail titipan ke List produk tersebut
                _keranjangDict[keyProduk].Add(detailBaru);
            }
            else
            {
                // Jika belum ada, buat entry Dictionary baru
                List<TransactionDetail> listDetailBaru = new List<TransactionDetail>();
                listDetailBaru.Add(detailBaru);
                _keranjangDict.Add(keyProduk, listDetailBaru);
            }

            // Update jumlah terpesan di objek Product (In-Memory) untuk pengecekan kuota
            produk.TambahPesanan(jumlah);
        }


        /// <summary>
        /// Menghitung total tagihan seluruh keranjang di RAM.
        /// Sekaligus melakukan Finalisasi Snapshot harga pada setiap detail.
        /// </summary>
        public long HitungTotalKeranjang()
        {
            long total = 0;

            foreach (KeyValuePair<int, List<TransactionDetail>> entry in _keranjangDict)
            {
                foreach (TransactionDetail detail in entry.Value)
                {
                    Product produk = detail.GetProduk();
                    if (produk != null)
                    {
                        // Hitung harga berdasarkan logic di Product (Gotong Royong / Biasa)
                        long hargaSaatIni = produk.HitungTotal();

                        // Casting tipe data dari int? ke long? untuk kompatibilitas method
                        long? hargaDiskon = null;
                        if (produk.GetHargaDiskon().HasValue)
                        {
                            hargaDiskon = Convert.ToInt64(produk.GetHargaDiskon().Value);
                        }

                        // Finalisasi snapshot harga di detail transaksi (RAM)
                        detail.FinalisasiHargaSaatCheckout(hargaSaatIni, hargaDiskon);
                    }

                    total = total + detail.HitungTotal();
                }
            }

            return total;
        }


        /// <summary>
        /// Membangun objek Transaction utuh dari data keranjang di RAM 
        /// yang siap diteruskan ke Repository untuk di-insert ke DB.
        /// </summary>
        public Transaction BuildTransaction()
        {
            if (_keranjangDict.Count == 0)
            {
                throw new InvalidOrderException("Keranjang kosong, tidak bisa checkout!", "", "CART_EMPTY");
            }

            // Pastikan snapshot harga sudah di-finalisasi sebelum build
            HitungTotalKeranjang();

            // Buat objek Transaction Induk
            Transaction transaksi = new Transaction(_idPembeli);

            // Pindahkan semua detail dari Dictionary ke List di dalam Transaction (Komposisi)
            foreach (KeyValuePair<int, List<TransactionDetail>> entry in _keranjangDict)
            {
                foreach (TransactionDetail detail in entry.Value)
                {
                    // Hitung refund Gotong Royong jika kuota terpenuhi saat checkout
                    detail.HitungRefundGotongRoyong();

                    // Tambahkan ke objek transaksi
                    transaksi.TambahDetail(detail);
                }
            }

            transaksi.Validate();
            return transaksi;
        }


        /// <summary>
        /// Mengosongkan keranjang di RAM setelah checkout berhasil.
        /// </summary>
        public void KosongkanKeranjang()
        {
            _keranjangDict.Clear();
        }
        public void HapusItem(int idProduk)
        {
            if (_keranjangDict.ContainsKey(idProduk))
            {
                _keranjangDict.Remove(idProduk);
            }
        }


        /// <summary>
        /// Mengambil struktur Dictionary keranjang untuk di-flatten ke List 
        /// oleh Controller agar bisa ditampilkan di DataGridView UI.
        /// </summary>
        public Dictionary<int, List<TransactionDetail>> GetKeranjangDictionary()
        {
            return _keranjangDict;
        }
    }
}
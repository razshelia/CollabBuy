using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Exceptions;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Services
{
    public class CartManager
    {
        private readonly int _idPembeli;

        // Static: bertahan saat pindah menu, tapi diisolasi per idPembeli
        private static readonly Dictionary<int, Dictionary<int, List<TransactionDetail>>>
            _semuaKeranjang = new Dictionary<int, Dictionary<int, List<TransactionDetail>>>();

        private static readonly object _lockKeranjang = new object();

        // Shortcut ke keranjang pembeli aktif
        private Dictionary<int, List<TransactionDetail>> Keranjang
        {
            get
            {
                lock (_lockKeranjang)
                {
                    if (!_semuaKeranjang.ContainsKey(this._idPembeli))
                        _semuaKeranjang[this._idPembeli] = new Dictionary<int, List<TransactionDetail>>();
                    return _semuaKeranjang[this._idPembeli];
                }
            }
        }

        public CartManager(int idPembeli)
        {
            if (idPembeli <= 0)
                throw new InvalidOrderException("ID Pembeli tidak valid untuk CartManager!", "id_pembeli", "CART_PEMBELI_INVALID");
            this._idPembeli = idPembeli;
        }

        public void TambahItem(Product produk, string namaPenitip, int jumlah, string catatan)
        {
            if (produk == null)
                throw new InvalidOrderException("Produk tidak boleh null saat ditambahkan ke keranjang!", "produk", "CART_PRODUK_NULL");

            if (!produk.IdPo.HasValue)
                throw new InvalidOrderException("Produk ini tidak terikat dalam sesi PO!", "id_po", "CART_NO_PO");

            TransactionDetail detailBaru = new TransactionDetail(produk.IdProduk, namaPenitip, jumlah);
            detailBaru.Catatan = catatan;
            detailBaru.ProdukYangDipesan = produk;
            detailBaru.Validate();

            if (this.Keranjang.ContainsKey(produk.IdProduk))
                this.Keranjang[produk.IdProduk].Add(detailBaru);
            else
                this.Keranjang[produk.IdProduk] = new List<TransactionDetail> { detailBaru };

            produk.TambahPesanan(jumlah);
        }

        /// <summary>
        /// Hanya menghitung total untuk tampilan UI — tidak mengubah state detail.
        /// Aman dipanggil berulang kali.
        /// </summary>
        public long HitungTotalKeranjang()
        {
            // Hitung total qty per produk dari keranjang saat ini
            var qtyDiKeranjang = new Dictionary<int, int>();
            foreach (var entry in this.Keranjang)
            {
                int totalQtyEntry = 0;
                foreach (var detail in entry.Value)
                    totalQtyEntry += detail.JumlahPesanan;
                qtyDiKeranjang[entry.Key] = totalQtyEntry;
            }

            long total = 0;
            foreach (var entry in this.Keranjang)
            {
                foreach (TransactionDetail detail in entry.Value)
                {
                    Product produk = detail.ProdukYangDipesan;
                    if (produk != null)
                    {
                        int qtyKeranjangIni = qtyDiKeranjang.ContainsKey(produk.IdProduk) ? qtyDiKeranjang[produk.IdProduk] : 0;

                        // Terpesan di DB sudah include qty keranjang ini (karena TambahPesanan dipanggil saat add to cart)
                        // Jadi terpesanSebelumKeranjangIni = Terpesan - qtyKeranjangIni
                        int terpesanSebelumKeranjangIni = Math.Max(0, produk.Terpesan - qtyKeranjangIni);

                        // Total jika checkout sekarang = terpesanSebelum + qty keranjang ini
                        int totalJikaCheckout = terpesanSebelumKeranjangIni + qtyKeranjangIni;

                        bool isGR = produk.JenisPo == "Gotong Royong"
                            && produk.TargetKuota.HasValue
                            && produk.HargaDiskon.HasValue;

                        // Kuota terpenuhi kalau total setelah checkout >= target
                        bool kuotaTerpenuhi = isGR && totalJikaCheckout >= produk.TargetKuota.Value;

                        long hargaSaatIni = kuotaTerpenuhi
                            ? produk.HargaDiskon.Value   // ✅ harga sudah dipotong diskon
                            : produk.HargaDasar;          // harga normal

                        long? hargaDiskon = kuotaTerpenuhi
                            ? (long?)Convert.ToInt64(produk.HargaDiskon.Value)
                            : null;

                        detail.FinalisasiHargaSaatCheckout(hargaSaatIni, hargaDiskon);
                    }
                    total += detail.HitungTotal();
                }
            }
            return total;
        }

        /// <summary>
        /// Finalisasi harga saat checkout — mengubah state detail secara permanen.
        /// Hanya boleh dipanggil SEKALI saat BuildTransaction().
        /// </summary>
        public void FinalisasiHargaUntukCheckout()
        {
            foreach (var entry in this.Keranjang)
            {
                foreach (TransactionDetail detail in entry.Value)
                {
                    Product produk = detail.ProdukYangDipesan;
                    if (produk != null)
                    {
                        long hargaSaatIni = produk.HitungTotal();
                        long? hargaDiskon = produk.HargaDiskon.HasValue
                            ? (long?)Convert.ToInt64(produk.HargaDiskon.Value)
                            : null;
                        detail.FinalisasiHargaSaatCheckout(hargaSaatIni, hargaDiskon);
                    }
                }
            }
        }

        public Transaction BuildTransaction()
        {
            if (this.Keranjang.Count == 0)
                throw new InvalidOrderException("Keranjang kosong, tidak bisa checkout!", "", "CART_EMPTY");

            this.FinalisasiHargaUntukCheckout();

            foreach (var entry in this.Keranjang)
            {
                int totalQty = 0;
                Product produkRef = null;
                foreach (var detail in entry.Value)
                {
                    totalQty += detail.JumlahPesanan;
                    if (produkRef == null) produkRef = detail.ProdukYangDipesan;
                }
                produkRef?.ValidasiTotalPesanan(totalQty);
            }

            Transaction transaksi = new Transaction(this._idPembeli);
            foreach (var entry in this.Keranjang)
            {
                foreach (TransactionDetail detail in entry.Value)
                {
                    detail.HitungRefundGotongRoyong();
                    transaksi.TambahDetail(detail);
                }
            }

            transaksi.Validate();
            return transaksi;
        }

        public void KosongkanKeranjang()
        {
            // Kembalikan semua stok sebelum keranjang dikosongkan
            foreach (var entry in this.Keranjang)
            {
                foreach (var detail in entry.Value)
                {
                    detail.ProdukYangDipesan?.KurangiPesanan(detail.JumlahPesanan);
                }
            }

            this.Keranjang.Clear();
        }

        public void KosongkanKeranjangSetelahCheckout()
        {
            this.Keranjang.Clear();
        }

        public void HapusItem(int idProduk)
        {
            if (!this.Keranjang.ContainsKey(idProduk)) return;

            // Kembalikan stok semua detail produk ini sebelum dihapus
            foreach (var detail in this.Keranjang[idProduk])
            {
                detail.ProdukYangDipesan?.KurangiPesanan(detail.JumlahPesanan);
            }

            this.Keranjang.Remove(idProduk);
        }

        public void HapusDetailTitipan(int idProduk, string namaPenitip)
        {
            if (!this.Keranjang.ContainsKey(idProduk)) return;

            var list = this.Keranjang[idProduk];

            // Kembalikan stok sebelum dihapus
            var itemYangDihapus = list.FindAll(d => d.NamaPenitip == namaPenitip);
            foreach (var item in itemYangDihapus)
            {
                item.ProdukYangDipesan?.KurangiPesanan(item.JumlahPesanan);
            }

            list.RemoveAll(d => d.NamaPenitip == namaPenitip);

            if (list.Count == 0)
                this.Keranjang.Remove(idProduk);
        }

        public void UpdateDetailTitipan(int idProduk, string oldPenitip, string newPenitip, int jumlah, string catatan)
        {
            if (!this.Keranjang.ContainsKey(idProduk)) return;

            var detail = this.Keranjang[idProduk].Find(d => d.NamaPenitip == oldPenitip);
            if (detail == null) return;

            Product p = detail.ProdukYangDipesan;
            int selisih = jumlah - detail.JumlahPesanan;

            this.Keranjang[idProduk].Remove(detail);

            TransactionDetail newDetail = new TransactionDetail(idProduk, newPenitip, jumlah);
            newDetail.Catatan = catatan;
            newDetail.ProdukYangDipesan = p;
            this.Keranjang[idProduk].Add(newDetail);

            if (selisih > 0) p?.TambahPesanan(selisih);
            else if (selisih < 0) p?.KurangiPesanan(-selisih);
        }

        public Dictionary<int, List<TransactionDetail>> GetKeranjangDictionary()
        {
            return this.Keranjang;
        }

        /// <summary>
        /// Hapus keranjang pembeli ini saat logout agar tidak bocor ke sesi berikutnya.
        /// Panggil dari MainForm saat tombol Logout ditekan.
        /// </summary>
        public static void BersihkanSesiPembeli(int idPembeli)
        {
            lock (_lockKeranjang)
            {
                if (_semuaKeranjang.ContainsKey(idPembeli))
                    _semuaKeranjang.Remove(idPembeli);
            }
        }
    }
}
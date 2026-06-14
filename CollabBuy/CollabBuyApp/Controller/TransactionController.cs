using CollabBuy.CollabBuyApp.Exceptions;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Services;
using System;
using System.Collections.Generic;
using System.Data;

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller yang bertindak sebagai Mandor alur transaksi dan keranjang belanja.
    /// Mematuhi standar strict OOP dengan full encapsulation.
    /// </summary>
    public class TransactionController
    {
        // === PRIVATE FIELDS (DEPENDENCIES) ===
        private readonly TransactionRepository _transactionRepo;
        private readonly ProductRepository _productRepo;
        private readonly ActivityLogRepository _logRepo;
        private CartManager _cartManager;

        // === KONSTRUKTOR TANPA PARAMETER (Untuk Sesi Penjual / Admin) ===
        public TransactionController()
        {
            this._transactionRepo = new TransactionRepository();
            this._productRepo = new ProductRepository();
            this._logRepo = new ActivityLogRepository();
            this._cartManager = null;
        }

        // === KONSTRUKTOR DENGAN PARAMETER (Untuk Sesi Pembeli) ===
        public TransactionController(int idPembeli)
        {
            this._transactionRepo = new TransactionRepository();
            this._productRepo = new ProductRepository();
            this._logRepo = new ActivityLogRepository();
            this._cartManager = new CartManager(idPembeli);
        }
        /// <summary>
        /// Mengisi _riwayatTransaksi di objek Pembeli dari database.
        /// Panggil ini setelah login atau saat membuka halaman profil/riwayat pembeli.
        /// </summary>
        public void SyncRiwayatKePembeli(Models.Pembeli pembeli)
        {
            if (pembeli == null) return;

            try
            {
                List<Models.Transaction> listDariDb = _transactionRepo.GetByIdPembeli(pembeli.IdUser);
                foreach (var trx in listDariDb)
                    pembeli.TambahRiwayatTransaksi(trx);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[SyncRiwayat] Gagal: " + ex.Message);
            }
        }
        // =======================================================
        // FITUR KERANJANG (MEMAKAI CartManager & IN-MEMORY RAM)
        // =======================================================
        public (bool sukses, string pesan) TambahItemKeKeranjang(int idProduk, string namaPenitip, int jumlah, string catatan)
        {
            (bool sukses, string pesan) hasil;

            if (this._cartManager == null)
            {
                hasil = (false, "Sesi keranjang tidak tersedia. Gunakan konstruktor TransactionController(idPembeli).");
            }
            else
            {
                try
                {
                    Product produk = this._productRepo.GetById(idProduk);
                    if (produk == null)
                    {
                        hasil = (false, "Produk tidak ditemukan di database!");
                    }
                    else
                    {
                        this._cartManager.TambahItem(produk, namaPenitip, jumlah, catatan);
                        hasil = (true, "Item berhasil ditambahkan ke keranjang.");
                    }
                }
                catch (InvalidOrderException ex)
                {
                    hasil = (false, ex.GetPesanLengkap());
                }
            }

            return hasil;
        }

        public List<TransactionDetail> GetIsiKeranjang()
        {
            if (this._cartManager == null) return new List<TransactionDetail>();

            var listFlat = new List<TransactionDetail>();
            foreach (var entry in this._cartManager.GetKeranjangDictionary())
                foreach (var detail in entry.Value)
                    listFlat.Add(detail);

            return listFlat;
        }

        public long HitungTotalKeranjangSaatIni()
        {
            if (this._cartManager == null) return 0;
            try { return this._cartManager.HitungTotalKeranjang(); }
            catch (Exception) { return 0; }
        }

        // =======================================================
        // FITUR CHECKOUT (TRANSACTION TCL)
        // =======================================================
        public (bool sukses, string pesan) ProsesCheckout()
        {
            (bool sukses, string pesan) hasil;

            if (this._cartManager == null)
            {
                hasil = (false, "Sesi keranjang tidak tersedia. Gunakan konstruktor TransactionController(idPembeli).");
            }
            else
            {
                try
                {
                    Transaction transaksiBaru = this._cartManager.BuildTransaction();
                    int idTransaksi = this._transactionRepo.Checkout(transaksiBaru);

                    // Trigger cashback SEBELUM KosongkanKeranjang — cart masih berisi data produk GR
                    this.TriggerCashbackInternal();

                    this._cartManager.KosongkanKeranjangSetelahCheckout();

                    ActivityLog log = new ActivityLog(transaksiBaru.IdPembeli, "Berhasil melakukan checkout Transaksi #" + idTransaksi);
                    this._logRepo.Insert(log);

                    hasil = (true, "Checkout berhasil! ID Transaksi Anda: " + idTransaksi);
                }
                catch (InvalidOrderException ex)
                {
                    hasil = (false, "Checkout gagal: " + ex.GetPesanLengkap());
                }
                catch (Exception ex)
                {
                    hasil = (false, "Terjadi error sistem saat checkout: " + ex.Message);
                }
            }

            return hasil;
        }

        // Internal: dipanggil di dalam ProsesCheckout sebelum cart dikosongkan
        private void TriggerCashbackInternal()
        {
            try
            {
                var dict = this._cartManager.GetKeranjangDictionary();
                foreach (var entry in dict)
                {
                    Product produkRam = null;
                    foreach (var detail in entry.Value)
                    {
                        if (detail.ProdukYangDipesan != null)
                        {
                            produkRam = detail.ProdukYangDipesan;
                            break;
                        }
                    }

                    if (produkRam == null) continue;
                    if (produkRam.JenisPo != "Gotong Royong") continue;
                    if (!produkRam.HargaDiskon.HasValue) continue;
                    if (!produkRam.IdPo.HasValue) continue;

                    // PERBAIKAN: ambil data produk terbaru dari DB, bukan dari RAM
                    Product produkDb = this._productRepo.GetById(produkRam.IdProduk);
                    if (produkDb == null) continue;
                    if (!produkDb.TargetKuota.HasValue) continue;

                    int totalTerpesanDB = this._transactionRepo.GetTotalTerpesanProduk(
                        produkDb.IdProduk, produkRam.IdPo.Value);

                    bool kuotaTerpenuhi = totalTerpesanDB >= produkDb.TargetKuota.Value;

                    if (!kuotaTerpenuhi) continue;

                    // PERBAIKAN: RecalculateCashback harus idempoten (tidak boleh trigger ganda)
                    // Pastikan implementasi di repository menggunakan ON CONFLICT DO NOTHING
                    // atau cek dulu apakah cashback sudah pernah diberikan untuk PO ini
                    this._transactionRepo.RecalculateCashbackGotongRoyong(
                        produkDb.IdProduk,
                        produkRam.IdPo.Value,
                        produkDb.HargaDasar,
                        produkDb.HargaDiskon ?? produkRam.HargaDiskon.Value
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[TriggerCashbackInternal] Silent fail: " + ex.Message);
            }
        }
        /// <summary>
        /// Validasi keranjang sebelum pindah ke halaman pembayaran.
        /// Mengecek min order per produk (total semua titipan, bukan per baris).
        /// Tidak melakukan checkout — hanya validasi.
        /// </summary>
        public (bool valid, string pesan) ValidasiKeranjangSebelumCheckout()
        {
            // Null-check wajib: _cartManager bisa null jika konstruktor tanpa idPembeli dipakai
            if (this._cartManager == null)
                return (false, "Sesi keranjang tidak tersedia.");

            try
            {
                var dict = this._cartManager.GetKeranjangDictionary();

                if (dict.Count == 0)
                    return (false, "Keranjang masih kosong!");

                var pelanggaranMinOrder = new System.Collections.Generic.List<string>();

                foreach (var entry in dict)
                {
                    int totalQty = 0;
                    Product produkRef = null;

                    foreach (var detail in entry.Value)
                    {
                        totalQty += detail.JumlahPesanan;
                        if (produkRef == null) produkRef = detail.ProdukYangDipesan;
                    }

                    if (produkRef != null && totalQty < produkRef.MinOrder)
                    {
                        pelanggaranMinOrder.Add(
                            $"• {produkRef.NamaProduk}: pesan {totalQty} pcs (min. {produkRef.MinOrder} pcs)"
                        );
                    }
                }

                if (pelanggaranMinOrder.Count > 0)
                {
                    string detail = string.Join("\n", pelanggaranMinOrder);
                    return (false,
                        "Beberapa produk belum memenuhi minimal order:\n\n" + detail +
                        "\n\nTambah jumlah pesanan atau hapus produk tersebut dari keranjang.");
                }

                return (true, "OK");
            }
            catch (Exception ex)
            {
                return (false, "Terjadi error saat validasi: " + ex.Message);
            }
        }

        // =======================================================
        // FITUR MANAJEMEN TRANSAKSI (QUERY & UPDATE STATUS)
        // =======================================================
        public Transaction GetDetailTransaksi(int idTransaksi)
        {
            Transaction detail;
            try
            {
                detail = this._transactionRepo.GetById(idTransaksi);
            }
            catch (Exception)
            {
                detail = null;
            }
            return detail;
        }
        public DataTable GetDetailPesananPembeli(int idTransaksi)
        {
            try { return this._transactionRepo.GetDetailPesananPembeli(idTransaksi); }
            catch { return new DataTable(); }
        }

        public List<Transaction> GetTransaksiByPembeli(int idPembeli)
        {
            List<Transaction> list;
            try
            {
                list = this._transactionRepo.GetByIdPembeli(idPembeli);
            }
            catch (Exception)
            {
                list = new List<Transaction>();
            }
            return list;
        }

        public DataTable GetRiwayatPesanan(int idPembeli)
        {
            // PERBAIKAN: gunakan method repository yang sudah efisien (pakai view DB)
            try
            {
                return this._transactionRepo.GetRiwayatPesananDataTable(idPembeli);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetRiwayatPesanan: " + ex.Message);
                return new DataTable();
            }
        }

        public (bool sukses, string pesan) ValidasiPembayaran(int idTransaksi)
        {
            (bool sukses, string pesan) hasil;
            try
            {
                Transaction transaksi = this._transactionRepo.GetById(idTransaksi);
                if (transaksi == null)
                {
                    hasil = (false, "Transaksi tidak ditemukan!");
                }
                else if (!transaksi.ApakahSudahDibayar())
                {
                    // PERBAIKAN: tolak approve jika bukti bayar belum diupload
                    hasil = (false, "Bukti pembayaran belum diupload oleh pembeli. Tidak bisa divalidasi.");
                }
                else
                {
                    transaksi.Approve();
                    this._transactionRepo.Update(transaksi);
                    hasil = (true, "Pembayaran Transaksi #" + idTransaksi + " berhasil divalidasi.");
                }
            }
            catch (InvalidOrderException ex)
            {
                hasil = (false, ex.GetPesanLengkap());
            }
            return hasil;
        }

        public DataTable GetPesananMasuk(int idPenjual)
        {
            try { return this._transactionRepo.GetPesananMasukDataTable(idPenjual); }
            catch (Exception) { return new DataTable(); }
        }

        public (bool sukses, string pesan) UbahStatusPesanan(int idTransaksi, string statusBaru)
        {
            try
            {
                // PERBAIKAN: muat objek Transaction dulu, validasi lewat state machine
                Transaction transaksi = this._transactionRepo.GetById(idTransaksi);

                if (transaksi == null)
                    return (false, "Pesanan nggak ketemu di database.");

                if (!transaksi.BisaDiubahKe(statusBaru))
                    return (false, $"Status tidak bisa diubah dari '{transaksi.GetStatus()}' ke '{statusBaru}'. Transisi tidak valid.");

                transaksi.UbahStatus(statusBaru);
                this._transactionRepo.Update(transaksi);

                return (true, "Status pesanan berhasil di-update jadi " + statusBaru + "!");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                return (false, "Duh, gagal update status: " + ex.Message);
            }
        }

        public (bool sukses, string pesan) UploadBuktiBayar(int idTransaksi, byte[] buktiBayar, int idUserLog)
        {
            (bool sukses, string pesan) hasil;
            try
            {
                Transaction transaksi = this._transactionRepo.GetById(idTransaksi);
                if (transaksi == null)
                {
                    hasil = (false, "Transaksi tidak ditemukan!");
                }
                else
                {
                    transaksi.BuktiBayar = buktiBayar;
                    this._transactionRepo.Update(transaksi);

                    ActivityLog log = new ActivityLog(idUserLog, "Mengupload bukti bayar Transaksi #" + idTransaksi);
                    this._logRepo.Insert(log);

                    hasil = (true, "Bukti pembayaran berhasil diupload!");
                }
            }
            catch (InvalidOrderException ex)
            {
                hasil = (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                hasil = (false, "Error sistem: " + ex.Message);
            }
            return hasil;
        }

        public int GetTotalPesananAktif(int idKoordinator)
        {
            int jumlah;
            try
            {
                jumlah = this._transactionRepo.GetActiveTransactionCount(idKoordinator);
            }
            catch (Exception)
            {
                jumlah = 0;
            }
            return jumlah;
        }

        public DataTable GetKeranjangDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdProduk", typeof(int));
            dt.Columns.Add("NamaItem", typeof(string));
            dt.Columns.Add("NamaPenitip", typeof(string));
            dt.Columns.Add("Catatan", typeof(string));
            dt.Columns.Add("Harga", typeof(int));
            dt.Columns.Add("Kuantitas", typeof(int));
            dt.Columns.Add("Subtotal", typeof(int));

            if (this._cartManager == null) return dt;

            foreach (var entry in this._cartManager.GetKeranjangDictionary())
            {
                foreach (var detail in entry.Value)
                {
                    Product p = detail.ProdukYangDipesan;
                    int hargaSatuan;
                    string namaItem;

                    if (p != null)
                    {
                        hargaSatuan = p.HargaDasar;
                        namaItem = p.NamaProduk;
                    }
                    else
                    {
                        hargaSatuan = 0;
                        namaItem = "Produk Jastip";
                    }

                    int subtotal = hargaSatuan * detail.JumlahPesanan;

                    dt.Rows.Add(
                        entry.Key,
                        namaItem,
                        detail.NamaPenitip,
                        detail.Catatan ?? "-",
                        hargaSatuan,
                        detail.JumlahPesanan,
                        subtotal
                    );
                }
            }
            return dt;
        }

        public void UpdateTitipan(int idProduk, string oldPenitip, string newPenitip, int jumlah, string catatan)
        {
            this._cartManager.UpdateDetailTitipan(idProduk, oldPenitip, newPenitip, jumlah, catatan);
        }

        public void TambahTitipanBaru(int idProduk, string namaPenitip, int jumlah, string catatan)
        {
            var dict = this._cartManager.GetKeranjangDictionary();
            if (dict.ContainsKey(idProduk) && dict[idProduk].Count > 0)
            {
                Product p = dict[idProduk][0].ProdukYangDipesan;
                this._cartManager.TambahItem(p, namaPenitip, jumlah, catatan);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Produk ID {idProduk} tidak ada di keranjang. Tambah produk dulu sebelum menambah titipan.");
            }
        }

        public void HapusItemKeranjang(int idProduk, string namaPenitip)
        {
            this._cartManager.HapusDetailTitipan(idProduk, namaPenitip);
        }

        public void KosongkanKeranjang()
        {
            this._cartManager.KosongkanKeranjang();
        }

        // =======================================================
        // METHOD BARU: GET DETAIL PESANAN UNTUK PENJUAL
        // Mengambil header transaksi + item produk milik penjual
        // termasuk kolom bukti_bayar dari database.
        // =======================================================
        public DataTable GetDetailPesananPenjual(int idTransaksi, int idPenjual)
        {
            DataTable dt;
            try
            {
                dt = this._transactionRepo.GetDetailPesananPenjual(idTransaksi, idPenjual);
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }
        // Di TransactionController.cs — tambahkan static method:
        public static string FormatTagihan(long totalTagihan)
        {
            // PERBAIKAN: langsung format tanpa membuat dummy object
            return totalTagihan == 0 ? "Rp 0 (Gratis / Kosong)" : $"Rp {totalTagihan:N0}";
        }
    }
}
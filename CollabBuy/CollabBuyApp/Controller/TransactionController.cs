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
            List<TransactionDetail> listFlat = new List<TransactionDetail>();

            if (this._cartManager == null)
            {
                listFlat = new List<TransactionDetail>();
            }
            else
            {
                Dictionary<int, List<TransactionDetail>> keranjangDict = this._cartManager.GetKeranjangDictionary();
                foreach (KeyValuePair<int, List<TransactionDetail>> entry in keranjangDict)
                {
                    foreach (TransactionDetail detail in entry.Value)
                    {
                        listFlat.Add(detail);
                    }
                }
            }

            return listFlat;
        }

        public long HitungTotalKeranjangSaatIni()
        {
            long total;

            if (this._cartManager == null)
            {
                total = 0;
            }
            else
            {
                try
                {
                    total = this._cartManager.HitungTotalKeranjang();
                }
                catch (Exception)
                {
                    total = 0;
                }
            }

            return total;
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

                    this._cartManager.KosongkanKeranjang();

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
                    Product produk = null;
                    foreach (var detail in entry.Value)
                    {
                        if (detail.ProdukYangDipesan != null)
                        {
                            produk = detail.ProdukYangDipesan;
                            break;
                        }
                    }

                    if (produk == null) continue;
                    if (produk.JenisPo != "Gotong Royong") continue;
                    if (!produk.HargaDiskon.HasValue) continue;
                    if (!produk.IdPo.HasValue) continue;  // produk harus punya PO

                    int totalTerpesanDB = this._transactionRepo.GetTotalTerpesanProduk(
                        produk.IdProduk, produk.IdPo.Value);  // filter per PO

                    bool kuotaTerpenuhi = produk.TargetKuota.HasValue
                        && totalTerpesanDB >= produk.TargetKuota.Value;

                    if (!kuotaTerpenuhi) continue;

                    this._transactionRepo.RecalculateCashbackGotongRoyong(
                        produk.IdProduk,
                        produk.IdPo.Value,   // teruskan idPo
                        produk.HargaDasar,
                        produk.HargaDiskon.Value
                    );
                }
            }
            catch
            {
                // Silent fail
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
        public List<Transaction> GetAllTransaksi()
        {
            List<Transaction> list;
            try
            {
                list = this._transactionRepo.GetAll();
            }
            catch (Exception)
            {
                list = new List<Transaction>();
            }
            return list;
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
            DataTable dt = new DataTable();
            dt.Columns.Add("id_transaksi", typeof(int));
            dt.Columns.Add("waktu_pesan", typeof(DateTime));
            dt.Columns.Add("total_tagihan", typeof(long));
            dt.Columns.Add("status_pesanan", typeof(string));
            dt.Columns.Add("status_bayar", typeof(string));

            try
            {
                List<Transaction> listTrx = this._transactionRepo.GetByIdPembeli(idPembeli);

                if (listTrx != null)
                {
                    foreach (Transaction trx in listTrx)
                    {
                        string statusBayar;
                        if (trx.BuktiBayar != null && trx.BuktiBayar.Length > 0)
                        {
                            statusBayar = "Sudah Upload";
                        }
                        else
                        {
                            statusBayar = "Belum Bayar";
                        }

                        dt.Rows.Add(
                            trx.IdTransaksi,
                            trx.TanggalTransaksi,
                            trx.HitungTotal(),
                            trx.GetStatus(),
                            statusBayar
                        );
                    }
                }
                else
                {
                    // listTrx null berarti repository tidak menemukan data — kembalikan DataTable kosong
                    Console.WriteLine($"[GetRiwayatPesanan] Tidak ada riwayat untuk pembeli ID {idPembeli}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetRiwayatPesanan: " + ex.Message);
            }
            return dt;
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
            DataTable dt;
            try
            {
                dt = this._transactionRepo.GetPesananMasukDataTable(idPenjual);
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }

        public (bool sukses, string pesan) UbahStatusPesanan(int idTransaksi, string statusBaru)
        {
            (bool sukses, string pesan) hasil;
            try
            {
                bool berhasil = this._transactionRepo.UpdateStatusPesanan(idTransaksi, statusBaru);

                if (berhasil)
                {
                    hasil = (true, "Status pesanan berhasil di-update jadi " + statusBaru + "!");
                }
                else
                {
                    hasil = (false, "Pesanan nggak ketemu di database.");
                }
            }
            catch (Exception ex)
            {
                hasil = (false, "Duh, gagal update status: " + ex.Message);
            }
            return hasil;
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
    }
}
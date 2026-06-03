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
                    bool listKosong = true;
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
                bool keyTidakAda = true;
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
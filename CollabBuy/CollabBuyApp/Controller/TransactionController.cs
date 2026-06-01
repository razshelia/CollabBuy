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
    /// 
    /// PERBAIKAN OOP:
    /// - Konstruktor dipecah menjadi dua: default (tanpa parameter) untuk query/admin,
    ///   dan overload (int idPembeli) untuk keranjang belanja.
    /// - Ini memungkinkan View seperti PesananMasukControl dan RiwayatPesananControl
    ///   membuat instance tanpa harus tahu idPembeli saat konstruksi.
    /// - CartManager hanya dibuat jika idPembeli diberikan (Lazy initialization pattern).
    /// </summary>
    public class TransactionController
    {
        // === PRIVATE FIELDS (DEPENDENCIES) ===
        private readonly TransactionRepository _transactionRepo;
        private readonly ProductRepository _productRepo;
        private readonly ActivityLogRepository _logRepo;
        private CartManager _cartManager; // Nullable: hanya diinisialisasi jika ada sesi pembeli

        // === KONSTRUKTOR TANPA PARAMETER (untuk View yang hanya query/admin) ===
        /// <summary>
        /// Konstruktor default — cocok untuk View yang hanya membaca data transaksi
        /// tanpa perlu mengelola keranjang belanja (misalnya PesananMasukControl,
        /// RiwayatPesananControl, BeriUlasanControl).
        /// CartManager tidak diinisialisasi.
        /// </summary>
        public TransactionController()
        {
            _transactionRepo = new TransactionRepository();
            _productRepo = new ProductRepository();
            _logRepo = new ActivityLogRepository();
            _cartManager = null;
        }

        // === KONSTRUKTOR DENGAN PARAMETER (untuk sesi keranjang belanja) ===
        /// <summary>
        /// Konstruktor dengan idPembeli — dipakai oleh KeranjangBelanjaControl.
        /// Menginisialisasi CartManager khusus untuk pembeli ini.
        /// </summary>
        public TransactionController(int idPembeli)
        {
            _transactionRepo = new TransactionRepository();
            _productRepo = new ProductRepository();
            _logRepo = new ActivityLogRepository();
            _cartManager = new CartManager(idPembeli);
        }


        // =======================================================
        // FITUR KERANJANG (MEMAKAI CartManager & IN-MEMORY RAM)
        // Hanya tersedia jika diinisialisasi dengan idPembeli
        // =======================================================

        /// <summary>
        /// Menambahkan item titipan ke keranjang di RAM.
        /// View memanggil ini saat user klik tombol "Tambah ke Keranjang".
        /// </summary>
        public (bool sukses, string pesan) TambahItemKeKeranjang(int idProduk, string namaPenitip, int jumlah, string catatan)
        {
            if (_cartManager == null)
            {
                return (false, "Sesi keranjang tidak tersedia. Gunakan konstruktor TransactionController(idPembeli).");
            }

            try
            {
                Product produk = _productRepo.GetById(idProduk);
                if (produk == null)
                {
                    return (false, "Produk tidak ditemukan di database!");
                }

                _cartManager.TambahItem(produk, namaPenitip, jumlah, catatan);
                return (true, "Item berhasil ditambahkan ke keranjang.");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
        }

        /// <summary>
        /// Mengambil data keranjang dari RAM untuk ditampilkan di DataGridView.
        /// </summary>
        public List<TransactionDetail> GetIsiKeranjang()
        {
            List<TransactionDetail> listFlat = new List<TransactionDetail>();
            if (_cartManager == null) return listFlat;

            Dictionary<int, List<TransactionDetail>> keranjangDict = _cartManager.GetKeranjangDictionary();
            foreach (KeyValuePair<int, List<TransactionDetail>> entry in keranjangDict)
            {
                foreach (TransactionDetail detail in entry.Value)
                {
                    listFlat.Add(detail);
                }
            }
            return listFlat;
        }

        /// <summary>
        /// Menghitung total tagihan keranjang saat ini di RAM.
        /// </summary>
        public long HitungTotalKeranjangSaatIni()
        {
            if (_cartManager == null) return 0;
            try
            {
                return _cartManager.HitungTotalKeranjang();
            }
            catch (Exception)
            {
                return 0;
            }
        }


        // =======================================================
        // FITUR CHECKOUT (TRANSACTION TCL)
        // =======================================================

        /// <summary>
        /// Memproses seluruh isi keranjang menjadi satu transaksi di Database.
        /// </summary>
        public (bool sukses, string pesan) ProsesCheckout()
        {
            if (_cartManager == null)
            {
                return (false, "Sesi keranjang tidak tersedia. Gunakan konstruktor TransactionController(idPembeli).");
            }

            try
            {
                Transaction transaksiBaru = _cartManager.BuildTransaction();
                int idTransaksi = _transactionRepo.Checkout(transaksiBaru);

                _cartManager.KosongkanKeranjang();

                ActivityLog log = new ActivityLog(transaksiBaru.GetIdPembeli(), "Berhasil melakukan checkout Transaksi #" + idTransaksi);
                _logRepo.Insert(log);

                return (true, "Checkout berhasil! ID Transaksi Anda: " + idTransaksi);
            }
            catch (InvalidOrderException ex)
            {
                return (false, "Checkout gagal: " + ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                return (false, "Terjadi error sistem saat checkout: " + ex.Message);
            }
        }


        // =======================================================
        // FITUR MANAJEMEN TRANSAKSI (QUERY - Tersedia tanpa idPembeli)
        // =======================================================

        /// <summary>
        /// Mengambil satu transaksi berdasarkan ID untuk dilihat detailnya.
        /// </summary>
        public Transaction GetDetailTransaksi(int idTransaksi)
        {
            try
            {
                return _transactionRepo.GetById(idTransaksi);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Mengambil seluruh daftar transaksi (untuk View admin/penjual).
        /// </summary>
        public List<Transaction> GetAllTransaksi()
        {
            try
            {
                return _transactionRepo.GetAll();
            }
            catch (Exception)
            {
                return new List<Transaction>();
            }
        }

        /// <summary>
        /// Mengambil daftar transaksi berdasarkan ID pembeli/koordinator.
        /// Digunakan oleh RiwayatPesananControl agar hanya menampilkan
        /// transaksi milik user yang sedang login.
        /// </summary>
        public List<Transaction> GetTransaksiByPembeli(int idPembeli)
        {
            try
            {
                return _transactionRepo.GetByIdPembeli(idPembeli);
            }
            catch (Exception)
            {
                return new List<Transaction>();
            }
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
                // Ambil data utuh lewat repository
                List<Transaction> listTrx = _transactionRepo.GetByIdPembeli(idPembeli);

                if (listTrx != null)
                {
                    foreach (Transaction trx in listTrx)
                    {
                        // Cek status bayar dari ada/tidaknya array bytes bukti bayar
                        string statusBayar = (trx.GetBuktiBayar() != null && trx.GetBuktiBayar().Length > 0)
                            ? "Sudah Upload" : "Belum Bayar";

                        // Masukkan ke baris DataTable
                        dt.Rows.Add(
                            trx.GetIdTransaksi(),
                            trx.GetTanggalTransaksi(),
                            trx.HitungTotal(),
                            trx.GetStatus(),
                            statusBayar
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error GetRiwayatPesanan: " + ex.Message);
                // Kembalikan datatable kosong agar grid di UI tidak crash
            }
            return dt;
        }

        /// <summary>
        /// Menyetujui bukti pembayaran transaksi.
        /// </summary>
        public (bool sukses, string pesan) ValidasiPembayaran(int idTransaksi)
        {
            try
            {
                Transaction transaksi = _transactionRepo.GetById(idTransaksi);
                if (transaksi == null)
                {
                    return (false, "Transaksi tidak ditemukan!");
                }

                transaksi.Approve();
                _transactionRepo.Update(transaksi);

                return (true, "Pembayaran Transaksi #" + idTransaksi + " berhasil divalidasi.");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
        }

        /// <summary>
        /// Mengubah status pesanan (misal: dari Diproses ke Selesai).
        /// </summary>
        public DataTable GetPesananMasuk(int idPenjual)
        {
            try
            {
                return _transactionRepo.GetPesananMasukDataTable(idPenjual);
            }
            catch (Exception)
            {
                return new DataTable(); // Biar grid nggak crash kalau error
            }
        }

        public (bool sukses, string pesan) UbahStatusPesanan(int idTransaksi, string statusBaru)
        {
            try
            {
                bool berhasil = _transactionRepo.UpdateStatusPesanan(idTransaksi, statusBaru);
                
                if (berhasil) return (true, "Status pesanan berhasil di-update jadi " + statusBaru + "!");
                else return (false, "Pesanan nggak ketemu di database.");
            }
            catch (Exception ex)
            {
                return (false, "Duh, gagal update status: " + ex.Message);
            }
        }

        /// <summary>
        /// Mengupload bukti bayar untuk sebuah transaksi.
        /// </summary>
        public (bool sukses, string pesan) UploadBuktiBayar(int idTransaksi, byte[] buktiBayar, int idUserLog)
        {
            try
            {
                Transaction transaksi = _transactionRepo.GetById(idTransaksi);
                if (transaksi == null) return (false, "Transaksi tidak ditemukan!");

                transaksi.SetBuktiBayar(buktiBayar);
                _transactionRepo.Update(transaksi);

                ActivityLog log = new ActivityLog(idUserLog, "Mengupload bukti bayar Transaksi #" + idTransaksi);
                _logRepo.Insert(log);

                return (true, "Bukti pembayaran berhasil diupload!");
            }
            catch (InvalidOrderException ex) { return (false, ex.GetPesanLengkap()); }
            catch (Exception ex) { return (false, "Error sistem: " + ex.Message); }
        }
        public int GetTotalPesananAktif(int idKoordinator)
        {
            try
            {
                // Panggil method dari Repository (gudang)
                return _transactionRepo.GetActiveTransactionCount(idKoordinator);
            }
            catch (Exception)
            {
                // Kalau ada error (misal putus koneksi), return 0 aja biar aplikasi nggak crash
                return 0;
            }
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

            foreach (var entry in _cartManager.GetKeranjangDictionary())
            {
                foreach (var detail in entry.Value)
                {
                    Product p = detail.GetProduk();
                    int hargaSatuan = p != null ? p.GetHargaDasar() : 0;
                    int subtotal = hargaSatuan * detail.GetJumlahPesanan();

                    dt.Rows.Add(
                        entry.Key,
                        p != null ? p.GetNamaProduk() : "Produk Jastip",
                        detail.GetNamaPenitip(),
                        detail.GetCatatan() ?? "-",
                        hargaSatuan,
                        detail.GetJumlahPesanan(),
                        subtotal
                    );
                }
            }
            return dt;
        }

        /// <summary>
        /// Memperbarui rincian titipan temen yang sudah ada di dalam keranjang RAM.
        /// </summary>
        public void UpdateTitipan(int idProduk, string oldPenitip, string newPenitip, int jumlah, string catatan)
        {
            _cartManager.UpdateDetailTitipan(idProduk, oldPenitip, newPenitip, jumlah, catatan);
        }

        /// <summary>
        /// Menambahkan slot baris titipan baru (Split) untuk produk yang sama di dalam keranjang.
        /// </summary>
        public void TambahTitipanBaru(int idProduk, string namaPenitip, int jumlah, string catatan)
        {
            var dict = _cartManager.GetKeranjangDictionary();
            if (dict.ContainsKey(idProduk) && dict[idProduk].Count > 0)
            {
                Product p = dict[idProduk][0].GetProduk();
                _cartManager.TambahItem(p, namaPenitip, jumlah, catatan);
            }
        }

        /// <summary>
        /// Menghapus spesifik nama penitip dari item jualan tertentu di dalam daftar keranjang.
        /// </summary>
        public void HapusItemKeranjang(int idProduk, string namaPenitip)
        {
            _cartManager.HapusDetailTitipan(idProduk, namaPenitip);
        }

        /// <summary>
        /// Membersihkan seluruh daftar belanja kolektif di RAM.
        /// </summary>
        public void KosongkanKeranjang()
        {
            _cartManager.KosongkanKeranjang();
        }

    }
}
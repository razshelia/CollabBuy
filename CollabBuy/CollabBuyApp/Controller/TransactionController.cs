using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller yang bertindak sebagai Mandor alur transaksi dan keranjang belanja.
    /// 
    /// Tugas Utama (Sub-bab 2.1 & 3.3 Laporan):
    /// 1. Menghubungkan View (UI) dengan Repository & Manager.
    /// 2. Menangkap Exception dari Model/Repository agar aplikasi tidak crash (Force Close).
    /// 3. Tidak memuat logika bisnis (perhitungan/validasi), semuanya didelegasikan ke Model/Manager.
    /// </summary>
    public class TransactionController
    {
        // === PRIVATE FIELDS (DEPENDENCIES) ===
        private readonly TransactionRepository _transactionRepo;
        private readonly ProductRepository _productRepo;
        private readonly ActivityLogRepository _logRepo;
        private readonly CartManager _cartManager;

        // === KONSTRUKTOR ===
        public TransactionController(int idPembeli)
        {
            // Inisialisasi Repository (Mengambil konfigurasi otomatis dari App.config)
            _transactionRepo = new TransactionRepository();
            _productRepo = new ProductRepository();
            _logRepo = new ActivityLogRepository();

            // Inisialisasi CartManager khusus untuk pembeli ini (In-Memory di RAM)
            _cartManager = new CartManager(idPembeli);
        }


        // =======================================================
        // FITUR KERANJANG (MEMAKAI CartManager & IN-MEMORY RAM)
        // =======================================================

        /// <summary>
        /// Menambahkan item titipan ke keranjang di RAM.
        /// View memanggil ini saat user klik tombol "Tambah ke Keranjang".
        /// </summary>
        /// <returns>Tuple berisi status sukses dan pesan error (jika ada).</returns>
        public (bool sukses, string pesan) TambahItemKeKeranjang(int idProduk, string namaPenitip, int jumlah, string catatan)
        {
            try
            {
                // 1. Ambil objek Product utuh dari DB ke RAM via Repository
                Product produk = _productRepo.GetById(idProduk);
                if (produk == null)
                {
                    return (false, "Produk tidak ditemukan di database!");
                }

                // 2. Serahkan ke CartManager untuk divalidasi & dimasukkan ke Dictionary RAM
                _cartManager.TambahItem(produk, namaPenitip, jumlah, catatan);

                // 3. Jika sampai sini tanpa error, berarti sukses
                return (true, "Item berhasil ditambahkan ke keranjang.");
            }
            catch (InvalidOrderException ex)
            {
                // Tangkap exception dari Model (misal: PO sudah tutup, qty kurang dari min_order)
                // Aplikasi tidak crash, kembalikan pesan ke View untuk ditampilkan di MessageBox
                return (false, ex.GetPesanLengkap());
            }
        }

        /// <summary>
        /// Mengambil data keranjang dari RAM untuk ditampilkan di DataGridView View.
        /// Mengubah struktur Dictionary menjadi List datar agar mudah di-bind ke Grid.
        /// </summary>
        public List<TransactionDetail> GetIsiKeranjang()
        {
            List<TransactionDetail> listFlat = new List<TransactionDetail>();

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
        /// View memanggil ini untuk update Label "Total Harga" secara real-time.
        /// </summary>
        public long HitungTotalKeranjangSaatIni()
        {
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
        /// Menggunakan Database Transaction (BeginTransaction) di Repository.
        /// </summary>
        public (bool sukses, string pesan) ProsesCheckout()
        {
            try
            {
                // 1. CartManager merangkai objek Transaction dari Dictionary di RAM
                // Di dalam sini, logika Gotong Royong & validasi akhir dieksekusi
                Transaction transaksiBaru = _cartManager.BuildTransaction();

                // 2. Simpan ke Database secara atomik via Repository
                int idTransaksi = _transactionRepo.Checkout(transaksiBaru);

                // 3. Jika berhasil simpan, kosongkan keranjang di RAM
                _cartManager.KosongkanKeranjang();

                // 4. Catat aktivitas di log
                ActivityLog log = new ActivityLog(transaksiBaru.GetIdPembeli(), "Berhasil melakukan checkout Transaksi #" + idTransaksi);
                _logRepo.Insert(log);

                return (true, "Checkout berhasil! ID Transaksi Anda: " + idTransaksi);
            }
            catch (InvalidOrderException ex)
            {
                // Tangkap validasi bisnis yang gagal (misal: kuota tiba-tiba habis)
                return (false, "Checkout gagal: " + ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                // Tangkap error database (Rollback sudah otomatis dijalankan oleh Repository)
                return (false, "Terjadi error sistem saat checkout: " + ex.Message);
            }
        }


        // =======================================================
        // FITUR MANAJEMEN TRANSAKSI (ADMIN / PENJUAL)
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

                // Panggil method bisnis di Model
                transaksi.Approve();

                // Simpan perubahan status ke DB
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
        public (bool sukses, string pesan) UbahStatusPesanan(int idTransaksi, string statusBaru)
        {
            try
            {
                Transaction transaksi = _transactionRepo.GetById(idTransaksi);
                if (transaksi == null)
                {
                    return (false, "Transaksi tidak ditemukan!");
                }

                // Model yang mengatur state machine, bukan Controller
                transaksi.UbahStatus(statusBaru);

                _transactionRepo.Update(transaksi);

                return (true, "Status Transaksi #" + idTransaksi + " berhasil diubah ke " + statusBaru + ".");
            }
            catch (InvalidOrderException ex)
            {
                // Tangkap jika transisi status tidak valid (misal: dari Menunggu langsung Selesai)
                return (false, "Gagal mengubah status: " + ex.GetPesanLengkap());
            }
        }
        public (bool sukses, string pesan) UploadBuktiBayar(int idTransaksi, byte[] buktiBayar, int idUserLog)
        {
            try
            {
                Transaction transaksi = _transactionRepo.GetById(idTransaksi);
                if (transaksi == null) return (false, "Transaksi tidak ditemukan!");

                transaksi.SetBuktiBayar(buktiBayar); // Validasi ukuran ada di Model
                _transactionRepo.Update(transaksi);

                ActivityLog log = new ActivityLog(idUserLog, "Mengupload bukti bayar Transaksi #" + idTransaksi);
                _logRepo.Insert(log);

                return (true, "Bukti pembayaran berhasil diupload!");
            }
            catch (InvalidOrderException ex) { return (false, ex.GetPesanLengkap()); }
            catch (Exception ex) { return (false, "Error sistem: " + ex.Message); }
        }
    }
}
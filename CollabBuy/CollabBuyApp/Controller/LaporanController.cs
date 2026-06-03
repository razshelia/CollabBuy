using System;
using System.Data;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller yang bertindak sebagai Mandor alur pengambilan data Laporan & Analitik.
    /// 
    /// Tugas Utama:
    /// 1. Menjaga prinsip Layered Architecture (View tidak boleh panggil Repository langsung).
    /// 2. Menangani eksepsi database sehingga UI tidak mengalami force close/crash 
    ///    jika query analitik (CUBE, ROLLUP) memakan waktu lama atau koneksi terputus.
    /// </summary>
    public class LaporanController
    {
        // === PRIVATE FIELDS (DEPENDENCIES) ===
        private readonly LaporanRepository _laporanRepo;

        // === KONSTRUKTOR ===
        public LaporanController()
        {
            _laporanRepo = new LaporanRepository();
        }


        // =======================================================
        // 0. FITUR ANALITIK PENJUALAN (SELLER UI)
        // =======================================================

        /// <summary>
        /// Mengambil ringkasan pendapatan dan total pesanan selesai untuk Dashboard Analitik Penjual.
        /// </summary>
        public (long totalPendapatan, int totalPesanan) GetRingkasanLapak(int idPenjual)
        {
            try
            {
                return _laporanRepo.GetRingkasanPenjualan(idPenjual);
            }
            catch (Exception)
            {
                return (0, 0); // Kembalikan 0 jika database error agar UI tetap aman
            }
        }

        /// <summary>
        /// Mengambil rincian riwayat transaksi yang sudah selesai untuk ditampilkan di tabel Analitik.
        /// </summary>
        public DataTable GetDetailRiwayatCuan(int idPenjual)
        {
            try
            {
                return _laporanRepo.GetRiwayatCuanDataTable(idPenjual);
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }


        // =======================================================
        // 1. FITUR VIEW DATABASE (ADMIN UI)
        // =======================================================

        /// <summary>
        /// Mengambil data dari View vw_transaksi_lengkap.
        /// </summary>
        public DataTable GetTransaksiLengkap()
        {
            try
            {
                return _laporanRepo.GetTransaksiLengkap();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }


        // =======================================================
        // 2. FITUR PURE FUNCTION DATABASE
        // =======================================================

        /// <summary>
        /// Mengambil statistik dashboard untuk penjual tertentu menggunakan Function DB.
        /// </summary>
        public DataTable GetStatistikDashboardPenjual(int idPenjual)
        {
            try
            {
                return _laporanRepo.GetStatistikDashboardPenjual(idPenjual);
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Mengecek harga produk saat ini langsung dari Function DB.
        /// </summary>
        public int CekHargaSaatIniViaDatabase(int idProduk)
        {
            try
            {
                return _laporanRepo.CekHargaSaatIniViaDatabase(idProduk);
            }
            catch (Exception)
            {
                return 0; // Kembalikan 0 jika error
            }
        }


        // =======================================================
        // 3. FITUR TEORI HIMPUAN (SET OPERATIONS)
        // =======================================================

        /// <summary>
        /// UNION: Transaksi Diproses dan Selesai.
        /// </summary>
        public DataTable GetTransaksiAktifUnion()
        {
            try
            {
                return _laporanRepo.GetTransaksiAktifUnion();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// INTERSECT: Penjual yang juga pembeli (Sultan Member).
        /// </summary>
        public DataTable GetSultanMemberIntersect()
        {
            try
            {
                return _laporanRepo.GetSultanMemberIntersect();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// EXCEPT: User yang belum pernah transaksi (Pengguna Pasif).
        /// </summary>
        public DataTable GetPenggunaPasifExcept()
        {
            try
            {
                return _laporanRepo.GetPenggunaPasifExcept();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }


        // =======================================================
        // 4. FITUR GROUP BY & CASE (KLASIFIKASI)
        // =======================================================

        /// <summary>
        /// Statement 1: Status Ketersediaan Kuota (GROUP BY + CASE).
        /// </summary>
        public DataTable GetStatusKetersediaanKuota()
        {
            try
            {
                return _laporanRepo.GetStatusKetersediaanKuota();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Statement 2: Klasifikasi Performa Penjual (Leaderboard).
        /// </summary>

        public DataTable GetKlasifikasiPerformaPenjual()
        {
            try
            {
                return _laporanRepo.GetKlasifikasiPerformaPenjual();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Statement 3: Total barang terjual tiap produk.
        /// </summary>
        public DataTable GetTotalBarangTerjual()
        {
            try
            {
                return _laporanRepo.GetTotalBarangTerjual();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }


        // =======================================================
        // 5. FITUR CUBE, ROLLUP, GROUPING SETS, SUBQUERY
        // =======================================================

        /// <summary>
        /// CUBE: Kombinasi silang Kategori X Jenis PO.
        /// </summary>
        public DataTable GetAnalisisPasarCube()
        {
            try
            {
                return _laporanRepo.GetAnalisisPasarCube();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// ROLLUP: Hierarki Waktu → Total Tahun → Total Bulan.
        /// </summary>
        public DataTable GetLaporanKeuanganRollup()
        {
            try
            {
                return _laporanRepo.GetLaporanKeuanganRollup();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// GROUPING SETS: Rekap per Penjual & per Kategori sekaligus.
        /// </summary>
        public DataTable GetRingkasanGlobalGroupingSets()
        {
            try
            {
                return _laporanRepo.GetRingkasanGlobalGroupingSets();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// SUBQUERY: Deteksi produk dengan sisa kuota <= 5.
        /// </summary>
        public DataTable GetProdukSisaKuotaKritis()
        {
            try
            {
                return _laporanRepo.GetProdukSisaKuotaKritis();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }
        public DataTable GetLpjDanusPerPo(int idPenjual)
        {
            try
            {
                // Memanggil dari LaporanRepository yang sudah kita perbarui sebelumnya
                return _laporanRepo.GetLpjDanusPerPo(idPenjual);
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }
    }
}
using System;
using System.Data;
using CollabBuy.Repositories;

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
        // FITUR VIEW DATABASE
        // =======================================================

        /// <summary>
        /// Mengambil data dari View vw_katalog_aktif.
        /// </summary>
        public DataTable GetKatalogAktif()
        {
            try
            {
                return _laporanRepo.GetKatalogAktif();
            }
            catch (Exception)
            {
                // Jika DB error, kembalikan DataTable kosong agar DataGridView tidak crash
                return new DataTable();
            }
        }

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
        // FITUR PURE FUNCTION DATABASE
        // =======================================================

        /// <summary>
        /// Mengambil statistik dashboard untuk penjual tertentu.
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
        // FITUR TEORI HIMPUAN (SET OPERATIONS)
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
        // FITUR GROUP BY & CASE (KLASIFIKASI)
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
        // FITUR CUBE, ROLLUP, GROUPING SETS, SUBQUERY
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
    }
}
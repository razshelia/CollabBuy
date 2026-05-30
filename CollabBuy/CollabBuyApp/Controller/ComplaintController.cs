using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data; // Wajib ditambahkan untuk DataTable UI

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller untuk mengelola alur Aduan/Pengaduan.
    /// Sudah disesuaikan untuk UI Gen-Z tanpa membuang logic asli.
    /// </summary>
    public class ComplaintController
    {
        // === PRIVATE FIELDS ===
        private readonly ComplaintRepository _complaintRepo;
        private readonly ActivityLogRepository _logRepo;

        // === KONSTRUKTOR ===
        public ComplaintController()
        {
            _complaintRepo = new ComplaintRepository();
            _logRepo = new ActivityLogRepository();
        }

        // =======================================================
        // FITUR PEMBELI (NEO-RETRO UI)
        // =======================================================

        /// <summary>
        /// Mengambil riwayat aduan khusus untuk user tertentu (Untuk DataGridView UI).
        /// </summary>
        public DataTable GetRiwayatSpill(int idUser)
        {
            try { return _complaintRepo.GetRiwayatByUser(idUser); }
            catch (Exception) { return new DataTable(); }
        }

        /// <summary>
        /// Membuat aduan baru dari pembeli (Versi Upgrade dari KirimAduan).
        /// Mempertahankan validasi model dan Activity Log.
        /// </summary>
        public (bool sukses, string pesan) GasSpillKendala(int idUser, string subjek, string deskripsi)
        {
            if (string.IsNullOrWhiteSpace(subjek) || string.IsNullOrWhiteSpace(deskripsi))
                return (false, "Subjek sama deskripsi jangan dikosongin dong bestie, Mimin bingung nanti!");

            try
            {
                // 1. Buat Objek dan Validasi Asli
                Complaint aduan = new Complaint(idUser, subjek, deskripsi);
                aduan.Validate();

                // 2. Simpan ke Database
                _complaintRepo.Insert(aduan);

                // 3. Catat ke Activity Log Asli
                ActivityLog log = new ActivityLog(idUser, "Mengirim aduan: " + subjek);
                _logRepo.Insert(log);

                return (true, "Aman! Curhatan kamu udah dikirim ke Mimin. Tunggu balasan ya! 💌");
            }
            catch (InvalidOrderException ex)
            {
                return (false, "Waduh: " + ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                return (false, "Duh server lagi ngambek: " + ex.Message);
            }
        }


        // =======================================================
        // FITUR ADMIN (KODE ASLI DIPERTAHANKAN)
        // =======================================================

        /// <summary>
        /// Mengambil seluruh daftar aduan untuk dashboard Admin.
        /// </summary>
        public List<Complaint> GetAllAduan()
        {
            try
            {
                return _complaintRepo.GetAll();
            }
            catch (Exception)
            {
                return new List<Complaint>();
            }
        }

        /// <summary>
        /// Memberikan tanggapan dan menyelesaikan aduan.
        /// Memanfaatkan method BeriTanggapan() dari Interface IResolvable.
        /// </summary>
        public (bool sukses, string pesan) TanggapiAduan(int idAduan, string balasanAdmin, int idAdmin)
        {
            try
            {
                if (string.IsNullOrEmpty(balasanAdmin))
                {
                    return (false, "Balasan admin tidak boleh kosong!");
                }

                Complaint aduan = _complaintRepo.GetById(idAduan);
                if (aduan == null)
                {
                    return (false, "Aduan tidak ditemukan!");
                }

                // Panggil method dari IResolvable. 
                // Ini akan otomatis mengubah status IsSelesai menjadi true di RAM.
                aduan.BeriTanggapan(balasanAdmin);

                // Simpan perubahan ke DB
                _complaintRepo.Update(aduan);

                // Catat ke Activity Log
                ActivityLog log = new ActivityLog(idAdmin, "Membalas aduan ID: " + idAduan);
                _logRepo.Insert(log);

                return (true, "Tanggapan berhasil dikirim dan aduan diselesaikan.");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                return (false, "Error sistem: " + ex.Message);
            }
        }
        public DataTable GetAduanBelumBeres()
        {
            try { return _complaintRepo.GetPendingAduan(); }
            catch { return new DataTable(); }
        }
    }
}
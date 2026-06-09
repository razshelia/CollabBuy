using CollabBuy.CollabBuyApp.Exceptions;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data;

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
            this._complaintRepo = new ComplaintRepository();
            this._logRepo = new ActivityLogRepository();
        }

        // =======================================================
        // FITUR PEMBELI (NEO-RETRO UI)
        // =======================================================

        /// <summary>
        /// Mengambil riwayat aduan khusus untuk user tertentu (Untuk DataGridView UI).
        /// </summary>
        public DataTable GetRiwayatSpill(int idUser)
        {
            try
            {
                return this._complaintRepo.GetRiwayatByUser(idUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ComplaintController.GetRiwayatSpill] Error: {ex.Message}");
                return new DataTable();
            }
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
                Complaint aduan = new Complaint(idUser, subjek, deskripsi);
                aduan.Validate();
                this._complaintRepo.Insert(aduan);

                ActivityLog log = new ActivityLog(idUser, "Mengirim aduan: " + subjek);
                this._logRepo.Insert(log);

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
        public DataTable GetAllAduan()
        {
            try
            {
                return this._complaintRepo.GetPendingAduan();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ComplaintController.GetAllAduan] Error: " + ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// Memberikan tanggapan dan menyelesaikan aduan.
        /// Memanfaatkan method BerikanTanggapan() dari Class Complaint yang baru.
        /// </summary>
        public (bool sukses, string pesan) TanggapiAduan(int idAduan, string balasanAdmin, int idAdmin)
        {
            if (string.IsNullOrEmpty(balasanAdmin))
                return (false, "Balasan admin tidak boleh kosong!");

            try
            {
                Complaint aduan = this._complaintRepo.GetById(idAduan);

                if (aduan == null) return (false, "Aduan tidak ditemukan!");

                aduan.BerikanTanggapan(balasanAdmin, true);
                this._complaintRepo.Update(aduan);

                ActivityLog log = new ActivityLog(idAdmin, "Membalas aduan ID: " + idAduan);
                this._logRepo.Insert(log);

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
            try
            {
                return this._complaintRepo.GetPendingAduan();
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}
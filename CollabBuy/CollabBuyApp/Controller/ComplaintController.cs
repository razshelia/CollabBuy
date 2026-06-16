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
        // =======================================================
        // TIMPA METHOD GasSpillKendala
        // =======================================================
        public (bool sukses, string pesan) GasSpillKendala(int idUser, string subjek, string deskripsi)
        {
            try
            {
                // Cukup panggil Model, dia yang akan protes jika ada isian tidak sesuai.
                Complaint aduan = new Complaint(idUser, subjek, deskripsi);
                aduan.Validate();

                this._complaintRepo.Insert(aduan);

                ActivityLog log = new ActivityLog(idUser, "Mengirim aduan: " + subjek);
                this._logRepo.Insert(log);

                return (true, "Aman! Curhatan kamu udah dikirim ke Mimin. Tunggu balasan ya! 💌");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap()); // Menangkap pesan exception model
            }
            catch (Exception ex)
            {
                return (false, "Duh server lagi ngambek: " + ex.Message);
            }
        }

        // =======================================================
        // TIMPA METHOD TanggapiAduan
        // =======================================================
        public (bool sukses, string pesan) TanggapiAduan(int idAduan, string balasanAdmin, int idAdmin)
        {
            try
            {
                Complaint aduan = this._complaintRepo.GetById(idAduan);
                if (aduan == null)
                    throw new InvalidOrderException("Aduan tidak ditemukan!", "id_aduan", "ADUAN_NOT_FOUND");

                // Property Setter di dalam Model yang memvalidasi ini
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
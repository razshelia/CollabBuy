using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller untuk mengelola alur Aduan/Pengaduan.
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
        // FITUR PEMBELI
        // =======================================================

        /// <summary>
        /// Membuat aduan baru dari pembeli.
        /// </summary>
        public (bool sukses, string pesan) KirimAduan(int idUser, string subjek, string deskripsi)
        {
            try
            {
                Complaint aduan = new Complaint(idUser, subjek, deskripsi);
                aduan.Validate();

                _complaintRepo.Insert(aduan);

                ActivityLog log = new ActivityLog(idUser, "Mengirim aduan: " + subjek);
                _logRepo.Insert(log);

                return (true, "Aduan berhasil dikirim ke Admin.");
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
        // FITUR ADMIN
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
    }
}
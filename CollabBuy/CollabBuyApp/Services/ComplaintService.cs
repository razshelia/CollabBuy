using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Services
{
    public class ComplaintService
    {
        private readonly IComplaintRepository _complaintRepo;

        public ComplaintService()
        {
            _complaintRepo = new ComplaintRepository();
        }

        // ── 1. Kirim aduan baru (oleh user) ──
        public bool KirimAduan(int idUser, string subjek, string deskripsi)
        {
            // Validasi
            if (string.IsNullOrWhiteSpace(subjek))
            {
                UXHelper.TampilkanError("Subjek aduan wajib diisi.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(deskripsi))
            {
                UXHelper.TampilkanError("Deskripsi aduan wajib diisi.");
                return false;
            }

            Complaint aduan = new Complaint();
            aduan.IdUser = idUser;
            aduan.Subjek = subjek;
            aduan.Deskripsi = deskripsi;

            bool sukses = _complaintRepo.KirimAduan(aduan);
            if (sukses)
                UXHelper.TampilkanSukses("Aduan berhasil dikirim. Admin akan segera menindaklanjuti.");
            // Error sudah ditampilkan oleh repository
            return sukses;
        }

        // ── 2. Ambil semua aduan (untuk admin) ──
        public List<Complaint> AmbilSemuaAduan()
        {
            return _complaintRepo.AmbilSemuaAduan();
        }

        // ── 3. Ambil aduan milik user tertentu (riwayat aduan) ──
        public List<Complaint> AmbilAduanByUser(int idUser)
        {
            return _complaintRepo.AmbilAduanByUser(idUser);
        }

        // ── 4. Tandai aduan selesai (oleh admin) ──
        public bool TandaiSelesai(int idAduan)
        {
            if (!UXHelper.TampilkanKonfirmasi("Tandai aduan ini sebagai selesai?"))
                return false;

            bool sukses = _complaintRepo.TandaiSelesai(idAduan);
            if (sukses)
                UXHelper.TampilkanSukses("Aduan ditandai selesai.");
            return sukses;
        }

        // ── 5. Balas aduan (oleh admin) ──
        public bool BalasAduan(int idAduan, string balasan)
        {
            if (string.IsNullOrWhiteSpace(balasan))
            {
                UXHelper.TampilkanError("Balasan tidak boleh kosong.");
                return false;
            }

            bool sukses = _complaintRepo.BalasAduan(idAduan, balasan);
            if (sukses)
                UXHelper.TampilkanSukses("Balasan berhasil dikirim. Aduan ditandai selesai.");
            return sukses;
        }
    }
}
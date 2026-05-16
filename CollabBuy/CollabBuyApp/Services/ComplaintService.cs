using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Services
{
    public class ComplaintService
    {
        private readonly IComplaintRepository _complaintRepo;
        public ComplaintService(IComplaintRepository complaintRepo)
        {
            _complaintRepo = complaintRepo;
        }

        public bool KirimAduan(int idUser, string subjek, string deskripsi)
        {
            if (string.IsNullOrWhiteSpace(subjek)) { UXHelper.TampilkanError("Subjek aduan wajib diisi."); return false; }
            if (string.IsNullOrWhiteSpace(deskripsi)) { UXHelper.TampilkanError("Deskripsi aduan wajib diisi."); return false; }

            Complaint aduan = new Complaint();
            aduan.IdUser = idUser;
            aduan.Subjek = subjek;
            aduan.Deskripsi = deskripsi;

            try
            {
                bool sukses = _complaintRepo.KirimAduan(aduan);
                if (sukses) UXHelper.TampilkanSukses("Aduan berhasil dikirim. Admin akan segera menindaklanjuti.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public List<Complaint> AmbilSemuaAduan()
        {
            try { return _complaintRepo.AmbilSemuaAduan(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Complaint>(); }
        }

        public List<Complaint> AmbilAduanByUser(int idUser)
        {
            try { return _complaintRepo.AmbilAduanByUser(idUser); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Complaint>(); }
        }

        public bool TandaiSelesai(int idAduan)
        {
            if (!UXHelper.TampilkanKonfirmasi("Tandai aduan ini sebagai selesai?")) return false;

            try
            {
                bool sukses = _complaintRepo.TandaiSelesai(idAduan);
                if (sukses) UXHelper.TampilkanSukses("Aduan ditandai selesai.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public bool BalasAduan(int idAduan, string balasan)
        {
            if (string.IsNullOrWhiteSpace(balasan)) { UXHelper.TampilkanError("Balasan tidak boleh kosong."); return false; }

            try
            {
                bool sukses = _complaintRepo.BalasAduan(idAduan, balasan);
                if (sukses) UXHelper.TampilkanSukses("Balasan berhasil dikirim. Aduan ditandai selesai.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }
    }
}
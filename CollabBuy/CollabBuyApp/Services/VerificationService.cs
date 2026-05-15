using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Services
{
    public class VerificationService
    {
        private readonly IVerificationRepository _verifRepo;

        public VerificationService()
        {
            _verifRepo = new VerificationRepository();
        }

        public bool AjukanVerifikasi(int idUser, string nim, string namaToko, string pathKTM, int tahunMasuk)
        {
            // Validasi Data
            if (string.IsNullOrWhiteSpace(nim)) { UXHelper.TampilkanError("NIM wajib diisi."); return false; }
            if (string.IsNullOrWhiteSpace(namaToko)) { UXHelper.TampilkanError("Nama toko wajib diisi."); return false; }
            if (string.IsNullOrWhiteSpace(pathKTM)) { UXHelper.TampilkanError("Bukti KTM wajib diunggah."); return false; }

            int now = DateTime.Now.Year;
            if (tahunMasuk < now - 7 || tahunMasuk > now)
            {
                UXHelper.TampilkanError($"Tahun masuk harus antara {now - 7} dan {now}.");
                return false;
            }

            Verification verif = new Verification();
            verif.IdUser = idUser;
            verif.Nim = nim;
            verif.NamaToko = namaToko;
            verif.BuktiKtm = pathKTM;
            verif.TahunMasuk = tahunMasuk;

            try
            {
                bool sukses = _verifRepo.AjukanVerifikasi(verif);
                if (sukses) UXHelper.TampilkanSukses("Pengajuan verifikasi berhasil dikirim. Tunggu persetujuan admin.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public List<Verification> AmbilPengajuanPending()
        {
            try { return _verifRepo.AmbilPengajuanPending(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Verification>(); }
        }

        public bool SetujuiVerifikasi(int idVerifikasi)
        {
            if (!UXHelper.TampilkanKonfirmasi("Setujui pengajuan verifikasi ini?")) return false;

            try
            {
                bool sukses = _verifRepo.SetujuiVerifikasi(idVerifikasi);
                if (sukses) UXHelper.TampilkanSukses("Verifikasi disetujui. User sekarang menjadi penjual.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public bool TolakVerifikasi(int idVerifikasi)
        {
            if (!UXHelper.TampilkanKonfirmasi("Tolak pengajuan verifikasi ini?")) return false;

            try
            {
                bool sukses = _verifRepo.TolakVerifikasi(idVerifikasi);
                if (sukses) UXHelper.TampilkanSukses("Pengajuan ditolak.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public Verification AmbilVerifikasiByUser(int idUser)
        {
            try { return _verifRepo.AmbilVerifikasiByUser(idUser); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return null; }
        }
    }
}
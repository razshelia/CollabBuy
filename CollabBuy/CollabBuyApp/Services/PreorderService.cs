using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Services
{
    public class PreorderService
    {
        private readonly IPreorderRepository _poRepo;
        public PreorderService(IPreorderRepository poRepo)
        {
            _poRepo = poRepo;
        }

        public bool BuatPO(int idPenjual, int idProduk, string judul, string jenis, string infoRekening, DateTime batasWaktu, int targetKuota = 0)
        {
            if (idProduk <= 0) { UXHelper.TampilkanError("Silakan pilih produk terlebih dahulu."); return false; }
            if (string.IsNullOrWhiteSpace(judul)) { UXHelper.TampilkanError("Judul PO wajib diisi."); return false; }
            if (string.IsNullOrWhiteSpace(infoRekening)) { UXHelper.TampilkanError("Info rekening wajib diisi."); return false; }
            if (batasWaktu <= DateTime.Now) { UXHelper.TampilkanError("Batas waktu PO tidak boleh di masa lalu."); return false; }
            if (jenis != TipePO.Biasa && jenis != TipePO.GotongRoyong) { UXHelper.TampilkanError("Jenis PO tidak valid."); return false; }

            if (jenis == TipePO.GotongRoyong && targetKuota <= 0)
            {
                UXHelper.TampilkanError("Target kuota harus > 0 untuk PO Gotong Royong.");
                return false;
            }

            Preorder po = PreorderFactory.BuatPreorder(jenis);

            po.IdPenjual = idPenjual;
            po.JudulPo = judul;
            po.InfoRekening = infoRekening;
            po.BatasWaktu = batasWaktu;

            try
            {
                bool sukses = _poRepo.TambahPreorder(po, idProduk, targetKuota);
                if (sukses) UXHelper.TampilkanSukses("Preorder berhasil dibuat.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public List<Preorder> AmbilPOAktifUntukKatalog()
        {
            try { return _poRepo.AmbilPreorderAktif(); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Preorder>(); }
        }

        public List<Preorder> AmbilPOAktifByPenjual(int idPenjual)
        {
            try
            {
                List<Preorder> semua = _poRepo.AmbilPreorderByPenjual(idPenjual);
                List<Preorder> aktif = new List<Preorder>();
                foreach (var po in semua)
                {
                    if (po.IsAktif && po.BatasWaktu > DateTime.Now) aktif.Add(po);
                }
                return aktif;
            }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Preorder>(); }
        }

        public List<Preorder> AmbilSemuaPOByPenjual(int idPenjual)
        {
            try { return _poRepo.AmbilPreorderByPenjual(idPenjual); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Preorder>(); }
        }

        public Preorder AmbilPOById(int idPo)
        {
            try { return _poRepo.AmbilPreorderById(idPo); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return null; }
        }

        public bool TutupPO(int idPo, int idPenjual)
        {
            try
            {
                Preorder po = _poRepo.AmbilPreorderById(idPo);
                if (po == null) { UXHelper.TampilkanError("Preorder tidak ditemukan."); return false; }
                if (po.IdPenjual != idPenjual) { UXHelper.TampilkanError("Anda bukan pemilik preorder ini."); return false; }
                if (!po.IsAktif) { UXHelper.TampilkanError("Preorder sudah ditutup sebelumnya."); return false; }

                if (!UXHelper.TampilkanKonfirmasi($"Tutup PO \"{po.JudulPo}\"?")) return false;

                bool sukses = _poRepo.TutupPreorder(idPo);
                if (sukses) UXHelper.TampilkanSukses("Preorder berhasil ditutup.");
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
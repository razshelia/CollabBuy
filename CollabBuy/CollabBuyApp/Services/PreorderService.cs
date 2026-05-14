using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Services
{
    public class PreorderService
    {
        private readonly IPreorderRepository _poRepo;

        public PreorderService()
        {
            _poRepo = new PreorderRepository();
        }

        // 1. Buat PO baru
        public bool BuatPO(int idPenjual, string judul, string jenis, string infoRekening, DateTime batasWaktu, int targetKuota = 0)
        {
            // Validasi
            if (string.IsNullOrWhiteSpace(judul))
            {
                UXHelper.TampilkanError("Judul PO wajib diisi.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(infoRekening))
            {
                UXHelper.TampilkanError("Info rekening wajib diisi.");
                return false;
            }
            if (batasWaktu <= DateTime.Now)
            {
                UXHelper.TampilkanError("Batas waktu PO tidak boleh di masa lalu.");
                return false;
            }
            if (jenis != "Biasa" && jenis != "Gotong Royong")
            {
                UXHelper.TampilkanError("Jenis PO hanya boleh 'Biasa' atau 'Gotong Royong'.");
                return false;
            }

            Preorder po;
            if (jenis == "Gotong Royong")
            {
                var gr = new PreorderGotongRoyong();
                if (targetKuota <= 0)
                {
                    UXHelper.TampilkanError("Target kuota harus > 0 untuk PO Gotong Royong.");
                    return false;
                }
                gr.TargetKuota = targetKuota;
                po = gr;
            }
            else
            {
                po = new PreorderBiasa();
            }

            po.IdPenjual = idPenjual;
            po.JudulPo = judul;
            po.InfoRekening = infoRekening;
            po.BatasWaktu = batasWaktu;
            // isAktif default true dari constructor model

            bool sukses = _poRepo.TambahPreorder(po);
            if (sukses)
                UXHelper.TampilkanSukses("Preorder berhasil dibuat.");
            return sukses;
        }

        // 2. Ambil semua PO yang masih aktif (untuk katalog pembeli)
        public List<Preorder> AmbilPOAktifUntukKatalog()
        {
            return _poRepo.AmbilPreorderAktif();
        }

        // 3. Ambil PO aktif milik penjual tertentu
        public List<Preorder> AmbilPOAktifByPenjual(int idPenjual)
        {
            List<Preorder> semua = _poRepo.AmbilPreorderByPenjual(idPenjual);
            List<Preorder> aktif = new List<Preorder>();
            foreach (var po in semua)
            {
                if (po.IsAktif && po.BatasWaktu > DateTime.Now)
                    aktif.Add(po);
            }
            return aktif;
        }

        // 4. Ambil semua PO milik penjual (termasuk yang sudah tutup)
        public List<Preorder> AmbilSemuaPOByPenjual(int idPenjual)
        {
            return _poRepo.AmbilPreorderByPenjual(idPenjual);
        }

        // 5. Ambil detail PO berdasarkan ID
        public Preorder AmbilPOById(int idPo)
        {
            return _poRepo.AmbilPreorderById(idPo);
        }

        // 6. Tutup PO (hanya pemilik PO yang bisa)
        public bool TutupPO(int idPo, int idPenjual)
        {
            Preorder po = _poRepo.AmbilPreorderById(idPo);
            if (po == null)
            {
                UXHelper.TampilkanError("Preorder tidak ditemukan.");
                return false;
            }
            if (po.IdPenjual != idPenjual)
            {
                UXHelper.TampilkanError("Anda bukan pemilik preorder ini.");
                return false;
            }
            if (!po.IsAktif)
            {
                UXHelper.TampilkanError("Preorder sudah ditutup sebelumnya.");
                return false;
            }

            if (!UXHelper.TampilkanKonfirmasi($"Tutup PO \"{po.JudulPo}\"?"))
                return false;

            bool sukses = _poRepo.TutupPreorder(idPo);
            if (sukses)
                UXHelper.TampilkanSukses("Preorder berhasil ditutup.");
            return sukses;
        }
    }
}
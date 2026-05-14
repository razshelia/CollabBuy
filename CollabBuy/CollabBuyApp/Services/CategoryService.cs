using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _katRepo;

        public CategoryService()
        {
            _katRepo = new CategoryRepository();
        }

        public List<Category> AmbilSemua()
        {
            return _katRepo.AmbilSemua();
        }

        public Category AmbilById(int id)
        {
            return _katRepo.AmbilById(id);
        }

        public bool Tambah(string namaKategori)
        {
            if (string.IsNullOrWhiteSpace(namaKategori))
            {
                UXHelper.TampilkanError("Nama kategori wajib diisi.");
                return false;
            }

            Category kat = new Category();
            kat.NamaKategori = namaKategori;

            bool sukses = _katRepo.Tambah(kat);
            if (sukses)
                UXHelper.TampilkanSukses("Kategori berhasil ditambahkan.");
            return sukses;
        }

        public bool Update(int idKategori, string namaBaru)
        {
            if (string.IsNullOrWhiteSpace(namaBaru))
            {
                UXHelper.TampilkanError("Nama kategori wajib diisi.");
                return false;
            }

            Category kat = new Category();
            kat.IdKategori = idKategori;
            kat.NamaKategori = namaBaru;

            bool sukses = _katRepo.Update(kat);
            if (sukses)
                UXHelper.TampilkanSukses("Kategori berhasil diperbarui.");
            return sukses;
        }

        public bool Hapus(int idKategori)
        {
            if (!UXHelper.TampilkanKonfirmasi("Hapus kategori ini?"))
                return false;

            bool sukses = _katRepo.Hapus(idKategori);
            if (sukses)
                UXHelper.TampilkanSukses("Kategori berhasil dihapus.");
            return sukses;
        }
    }
}
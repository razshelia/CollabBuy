using System;
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
            try
            {
                return _katRepo.AmbilSemua();
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return new List<Category>();
            }
        }

        public Category AmbilById(int id)
        {
            try
            {
                return _katRepo.AmbilById(id);
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return null;
            }
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

            try
            {
                bool sukses = _katRepo.Tambah(kat);
                if (sukses) UXHelper.TampilkanSukses("Kategori berhasil ditambahkan.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
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

            try
            {
                bool sukses = _katRepo.Update(kat);
                if (sukses) UXHelper.TampilkanSukses("Kategori berhasil diperbarui.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public bool Hapus(int idKategori)
        {
            if (!UXHelper.TampilkanKonfirmasi("Hapus kategori ini?"))
                return false;

            try
            {
                bool sukses = _katRepo.Hapus(idKategori);
                if (sukses) UXHelper.TampilkanSukses("Kategori berhasil dihapus.");
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
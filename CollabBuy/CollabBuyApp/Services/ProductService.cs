using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;
        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public bool TambahProduk(Product produkBaru)
        {
            if (produkBaru == null) return false;
            if (string.IsNullOrWhiteSpace(produkBaru.NamaProduk))
            {
                UXHelper.TampilkanError("Nama produk tidak boleh kosong.");
                return false;
            }

            try
            {
                bool sukses = _productRepo.TambahProduk(produkBaru);
                if (sukses) UXHelper.TampilkanSukses("Produk berhasil ditambahkan ke Katalog Master!");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public List<Product> AmbilProdukByPo(int idPo)
        {
            try { return _productRepo.AmbilProdukByPo(idPo); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Product>(); }
        }

        public Product AmbilProdukById(int idProduk)
        {
            try { return _productRepo.AmbilProdukById(idProduk); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return null; }
        }

        public bool UpdateProduk(Product produkUpdate)
        {
            if (produkUpdate == null || string.IsNullOrWhiteSpace(produkUpdate.NamaProduk))
            {
                UXHelper.TampilkanError("Data tidak valid untuk diupdate.");
                return false;
            }

            try
            {
                bool sukses = _productRepo.UpdateProduk(produkUpdate);
                if (sukses) UXHelper.TampilkanSukses("Produk berhasil diperbarui!");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public bool HapusProduk(int idProduk)
        {
            if (UXHelper.TampilkanKonfirmasi("Yakin ingin menghapus produk ini dari katalog master?"))
            {
                try
                {
                    bool sukses = _productRepo.HapusProduk(idProduk);
                    if (sukses) UXHelper.TampilkanSukses("Produk berhasil dihapus.");
                    return sukses;
                }
                catch (Exception ex)
                {
                    UXHelper.TampilkanError(ex.Message);
                    return false;
                }
            }
            return false;
        }

        public int HitungHargaAktual(int idProduk)
        {
            try { return _productRepo.HitungHargaAktual(idProduk); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return 0; }
        }

        public int AmbilJumlahProduk()
        {
            try { return _productRepo.AmbilJumlahProduk(); }
            catch { return 0; }
        }

        public List<Product> AmbilProdukByPenjual(int idPenjual)
        {
            try { return _productRepo.AmbilProdukByPenjual(idPenjual); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Product>(); }
        }
    }
}
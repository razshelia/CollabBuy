using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService()
        {
            _productRepo = new ProductRepository();
        }

        // 1. Tambah Produk 
        public bool TambahProduk(Product produkBaru)
        {
            if (produkBaru == null) return false;

            if (string.IsNullOrWhiteSpace(produkBaru.NamaProduk))
            {
                UXHelper.TampilkanError("Nama produk tidak boleh kosong.");
                return false;
            }

            bool sukses = _productRepo.TambahProduk(produkBaru);
            if (sukses)
                UXHelper.TampilkanSukses("Produk berhasil ditambahkan ke Katalog Master!");

            return sukses;
        }

        // 2. Ambil Produk By PO
        public List<Product> AmbilProdukByPo(int idPo)
        {
            return _productRepo.AmbilProdukByPo(idPo);
        }

        // 3. Ambil Produk By Id
        public Product AmbilProdukById(int idProduk)
        {
            return _productRepo.AmbilProdukById(idProduk);
        }

        // 4. Update Produk
        public bool UpdateProduk(Product produkUpdate)
        {
            if (produkUpdate == null || string.IsNullOrWhiteSpace(produkUpdate.NamaProduk))
            {
                UXHelper.TampilkanError("Data tidak valid untuk diupdate.");
                return false;
            }

            bool sukses = _productRepo.UpdateProduk(produkUpdate);
            if (sukses)
                UXHelper.TampilkanSukses("Produk berhasil diperbarui!");

            return sukses;
        }

        // 5. Hapus Produk
        public bool HapusProduk(int idProduk)
        {
            if (UXHelper.TampilkanKonfirmasi("Yakin ingin menghapus produk ini dari katalog master?"))
            {
                bool sukses = _productRepo.HapusProduk(idProduk);
                if (sukses)
                    UXHelper.TampilkanSukses("Produk berhasil dihapus.");
                return sukses;
            }
            return false;
        }

        // 6. Hitung Harga Aktual (Dinamis berdasarkan pencapaian kuota)
        public int HitungHargaAktual(int idProduk)
        {
            return _productRepo.HitungHargaAktual(idProduk);
        }

        // 7. Ambil Total Jumlah Produk (Untuk Dashboard Admin)
        public int AmbilJumlahProduk()
        {
            return _productRepo.AmbilJumlahProduk();
        }

        // 8. Ambil Produk By Penjual (Wajib untuk ComboBox Buka PO)
        public List<Product> AmbilProdukByPenjual(int idPenjual)
        {
            return _productRepo.AmbilProdukByPenjual(idPenjual);
        }
    }
}
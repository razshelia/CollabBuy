using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Services
{
    public class ProductService
    {
        private readonly IProductRepository _prodRepo;

        public ProductService()
        {
            _prodRepo = new ProductRepository();
        }

        // 1. Ambil semua produk milik PO tertentu
        public List<Product> AmbilProdukByPo(int idPo)
        {
            return _prodRepo.AmbilProdukByPo(idPo);
        }

        // 2. Ambil detail produk berdasarkan ID
        public Product AmbilProdukById(int idProduk)
        {
            return _prodRepo.AmbilProdukById(idProduk);
        }

        // 3. Tambah produk baru ke dalam PO
        public bool TambahProduk(int idPo, int? idKategori, string namaProduk,
                                 int hargaDasar, int? hargaDiskon, int? targetKuota,
                                 int minOrder, string fotoPath)
        {
            // Validasi manual
            if (string.IsNullOrWhiteSpace(namaProduk))
            {
                UXHelper.TampilkanError("Nama produk wajib diisi.");
                return false;
            }
            if (hargaDasar < 0)
            {
                UXHelper.TampilkanError("Harga dasar tidak boleh negatif.");
                return false;
            }
            if (minOrder < 1)
            {
                UXHelper.TampilkanError("Minimal order harus ≥ 1.");
                return false;
            }
            // Validasi targetKuota khusus untuk PO Gotong Royong bisa ditambahkan di UI, tidak wajib di sini

            Product produk = new Product();
            produk.IdPo = idPo;
            produk.IdKategori = idKategori;
            produk.NamaProduk = namaProduk;
            produk.HargaDasar = hargaDasar;
            produk.HargaDiskon = hargaDiskon;
            produk.TargetKuota = targetKuota;
            produk.MinOrder = minOrder;
            produk.FotoProduk = fotoPath;  // path relatif dari FileHelper

            bool sukses = _prodRepo.TambahProduk(produk);
            if (sukses)
                UXHelper.TampilkanSukses("Produk berhasil ditambahkan.");
            // Error sudah ditampilkan oleh repository jika gagal
            return sukses;
        }

        // 4. Update produk (hanya oleh penjual yang memiliki PO terkait)
        public bool UpdateProduk(int idProduk, string nama, int hargaDasar, int? hargaDiskon,
                                 int? targetKuota, int minOrder, string fotoPath)
        {
            Product produk = _prodRepo.AmbilProdukById(idProduk);
            if (produk == null)
            {
                UXHelper.TampilkanError("Produk tidak ditemukan.");
                return false;
            }

            // Validasi
            if (string.IsNullOrWhiteSpace(nama))
            {
                UXHelper.TampilkanError("Nama produk wajib diisi.");
                return false;
            }
            if (hargaDasar < 0)
            {
                UXHelper.TampilkanError("Harga dasar tidak boleh negatif.");
                return false;
            }
            if (minOrder < 1)
            {
                UXHelper.TampilkanError("Minimal order harus ≥ 1.");
                return false;
            }

            produk.NamaProduk = nama;
            produk.HargaDasar = hargaDasar;
            produk.HargaDiskon = hargaDiskon;
            produk.TargetKuota = targetKuota;
            produk.MinOrder = minOrder;
            if (!string.IsNullOrEmpty(fotoPath))
                produk.FotoProduk = fotoPath;

            bool sukses = _prodRepo.UpdateProduk(produk);
            if (sukses)
                UXHelper.TampilkanSukses("Produk berhasil diperbarui.");
            return sukses;
        }

        // 5. Hapus produk (hanya oleh pemilik PO)
        public bool HapusProduk(int idProduk)
        {
            if (!UXHelper.TampilkanKonfirmasi("Hapus produk ini?"))
                return false;

            bool sukses = _prodRepo.HapusProduk(idProduk);
            if (sukses)
                UXHelper.TampilkanSukses("Produk berhasil dihapus.");
            return sukses;
        }

        // 6. Hitung harga aktual (memanggil function DB cek_harga_saat_ini)
        public int HitungHargaAktual(int idProduk)
        {
            return _prodRepo.HitungHargaAktual(idProduk);
        }
    }
}
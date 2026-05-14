using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Services
{
    public class ProductService
    {
        private ProductRepository productRepo;

        public ProductService()
        {
            this.productRepo = new ProductRepository();
        }

        public bool TambahProdukBaru(Product produkBaru, int idSeller)
        {
            if (produkBaru == null)
            {
                UXHelper.TampilkanError("Data produk tidak dikenali oleh sistem.");
                return false;
            }
            else
            {
                // LOGIKA BISNIS: Stok tidak boleh 0 saat baru didaftarkan
                if (produkBaru.StokProduk <= 0)
                {
                    UXHelper.TampilkanError("Stok awal produk harus lebih dari 0.");
                    return false;
                }
                else
                {
                    bool berhasil = this.productRepo.TambahProduk(produkBaru, idSeller);

                    if (berhasil)
                    {
                        UXHelper.TampilkanSukses($"Produk '{produkBaru.NamaProduk}' berhasil masuk ke Katalog!");
                        return true;
                    }
                    else
                    {
                        UXHelper.TampilkanError("Gagal terhubung ke Database. Periksa kembali jaringan Anda.");
                        return false;
                    }
                }
            }
        }
    }
}
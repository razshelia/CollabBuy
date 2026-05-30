using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data;

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller yang bertindak sebagai Mandor alur manajemen produk/katalog.
    /// Menangani penambahan produk, update, dan pengecekan status kuota di RAM.
    /// </summary>
    public class ProductController
    {
        // === PRIVATE FIELDS (DEPENDENCIES) ===
        private readonly ProductRepository _productRepo;
        private readonly ActivityLogRepository _logRepo;

        // === KONSTRUKTOR ===
        public ProductController()
        {
            _productRepo = new ProductRepository();
            _logRepo = new ActivityLogRepository();
        }


        // =======================================================
        // FITUR MANAJEMEN KATALOG
        // =======================================================

        /// <summary>
        /// Mengambil seluruh daftar produk untuk ditampilkan di View.
        /// </summary>
        public List<Product> GetAllProduk()
        {
            try
            {
                return _productRepo.GetAll();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }
        public DataTable GetKatalogAktifDashboard(int limit = 15)
        {
            try
            {
                // Panggil method dari Repository (gudang)
                return _productRepo.GetKatalogAktif(limit);
            }
            catch (Exception)
            {
                // Kalau error, kembalikan tabel kosong biar grid di UI nggak error
                return new DataTable();
            }
        }

        /// <summary>
        /// Menambahkan produk baru ke dalam database.
        /// </summary>
        public (bool sukses, string pesan) TambahProdukBaru(int idPenjual, int idKategori, string namaProduk, int hargaDasar, int? idPo, int? targetKuota, int minOrder, byte[] fotoProduk)
        {
            try
            {
                Product produk = new Product(idPenjual, idKategori, namaProduk, hargaDasar);
                if (idPo.HasValue) produk.SetIdPo(idPo.Value);
                if (targetKuota.HasValue) produk.SetTargetKuota(targetKuota);
                produk.SetMinOrder(minOrder);

                if (fotoProduk != null)
                {
                    produk.SetFotoProduk(fotoProduk); // Validasi ukuran ada di Model
                }

                produk.Validate();
                _productRepo.Insert(produk);

                ActivityLog log = new ActivityLog(idPenjual, "Menambahkan produk baru: " + namaProduk);
                _logRepo.Insert(log);

                return (true, "Produk berhasil ditambahkan!");
            }
            catch (InvalidOrderException ex) { return (false, ex.GetPesanLengkap()); }
            catch (Exception ex) { return (false, "Error sistem: " + ex.Message); }
        }



        // =======================================================
        // FITUR MONITORING KUOTA (IN-MEMORY RAM CHECK)
        // =======================================================

        /// <summary>
        /// Mengecek status kuota produk secara real-time.
        /// Memanfaatkan data In-Memory yang sudah disinkronkan oleh Repository.
        /// </summary>
        public (string status, int sisaKuota) CekStatusKuota(int idProduk)
        {
            try
            {
                // Repository akan menarik data dari DB dan mengisi state RAM
                Product produk = _productRepo.GetById(idProduk);
                if (produk == null)
                {
                    return ("Produk tidak ditemukan", 0);
                }

                if (produk.GetTargetKuota() == 0)
                {
                    return ("Tidak ada target kuota (PreOrder Biasa)", 0);
                }

                // Pemanggilan method bisnis di Model (Bukan di Controller/DB)
                if (produk.IsKuotaTerpenuhi())
                {
                    return ("TARGET KUOTA TERCAPAI! Diskon Gotong Royong Aktif.", 0);
                }
                else
                {
                    int sisa = produk.GetSisaKuota();
                    return ("Kuota belum terpenuhi.", sisa);
                }
            }
            catch (Exception ex)
            {
                return ("Error: " + ex.Message, 0);
            }
        }
        public DataTable GetKatalogUtama()
        {
            try { return _productRepo.GetKatalogUtama(); }
            catch { return new DataTable(); }
        }

        // Buat View Manajemen Produk (Penjual)
        public DataTable GetProdukLapak(int idPenjual)
        {
            try { return _productRepo.GetProdukLapak(idPenjual); }
            catch { return new DataTable(); }
        }

        // =======================================================
        // INI PENTING BUAT KERANJANG BELANJA
        // =======================================================
        public Product GetProdukById(int idProduk)
        {
            try
            {
                return _productRepo.GetById(idProduk);
            }
            catch (Exception ex)
            {
                // Biar gampang ke-track kalau error
                Console.WriteLine("Error narik data produk: " + ex.Message);
                return null;
            }
        }
    }
}
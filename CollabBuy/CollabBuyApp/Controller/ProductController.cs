using CollabBuy.CollabBuyApp.Exceptions;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data;

namespace CollabBuy.CollabBuyApp.Controllers
{
    public class ProductController
    {
        private readonly ProductRepository _productRepo;
        private readonly ActivityLogRepository _logRepo;

        public ProductController()
        {
            _productRepo = new ProductRepository();
            _logRepo = new ActivityLogRepository();
        }

        public List<Product> GetAllProduk()
        {
            try { return _productRepo.GetAll(); }
            catch { return new List<Product>(); }
        }

        public DataTable GetKatalogAktifDashboard(int limit = 15)
        {
            try { return _productRepo.GetKatalogAktif(limit); }
            catch { return new DataTable(); }
        }

        public (bool sukses, string pesan) TambahProdukBaru(int idPenjual, int idKategori, string namaProduk, int hargaDasar, int? idPo, int? targetKuota, int minOrder, byte[] fotoProduk)
        {
            try
            {
                Product produk = new Product(idPenjual, idKategori, namaProduk, hargaDasar);
                if (idPo.HasValue) produk.IdPo = idPo.Value;
                if (targetKuota.HasValue) produk.TargetKuota = targetKuota;
                produk.MinOrder = minOrder;
                if (fotoProduk != null) produk.FotoProduk = fotoProduk;
                produk.Validate();
                _productRepo.Insert(produk);

                ActivityLog log = new ActivityLog(idPenjual, "Menambahkan produk baru: " + namaProduk);
                _logRepo.Insert(log);

                return (true, "Produk berhasil ditambahkan!");
            }
            catch (InvalidOrderException ex) { return (false, ex.GetPesanLengkap()); }
            catch (Exception ex) { return (false, "Error sistem: " + ex.Message); }
        }

        public (bool sukses, string pesan) UpdateProduk(int idProduk, int idPenjual, int idKategori, string namaProduk, int hargaDasar, int minOrder, string deskripsi, byte[] fotoProduk)
        {
            try
            {
                Product produk = new Product(idPenjual, idKategori, namaProduk, hargaDasar);
                produk.IdProduk = idProduk;
                produk.MinOrder = minOrder;
                if (!string.IsNullOrEmpty(deskripsi)) produk.Deskripsi = deskripsi;
                if (fotoProduk != null) produk.FotoProduk = fotoProduk;
                produk.Validate();
                _productRepo.Update(produk);

                ActivityLog log = new ActivityLog(idPenjual, "Mengupdate produk: " + namaProduk);
                _logRepo.Insert(log);

                return (true, "Produk berhasil diupdate!");
            }
            catch (InvalidOrderException ex) { return (false, ex.GetPesanLengkap()); }
            catch (Exception ex) { return (false, "Error sistem: " + ex.Message); }
        }

        public (bool sukses, string pesan) HapusProduk(int idProduk, int idPenjual, string namaProduk)
        {
            try
            {
                _productRepo.SoftDelete(idProduk);

                ActivityLog log = new ActivityLog(idPenjual, "Menghapus produk: " + namaProduk);
                _logRepo.Insert(log);

                return (true, "Produk berhasil dihapus!");
            }
            catch (InvalidOrderException ex) { return (false, ex.GetPesanLengkap()); }
            catch (Exception ex) { return (false, "Error sistem: " + ex.Message); }
        }

        public (string status, int sisaKuota) CekStatusKuota(int idProduk)
        {
            try
            {
                Product produk = _productRepo.GetById(idProduk);
                if (produk == null) return ("Produk tidak ditemukan", 0);
                if (produk.GetTargetKuota() == 0) return ("Tidak ada target kuota (PreOrder Biasa)", 0);
                if (produk.IsKuotaTerpenuhi()) return ("TARGET KUOTA TERCAPAI! Diskon Gotong Royong Aktif.", 0);
                return ("Kuota belum terpenuhi.", produk.GetSisaKuota());
            }
            catch (Exception ex) { return ("Error: " + ex.Message, 0); }
        }

        public DataTable GetKatalogUtama()
        {
            try { return _productRepo.GetKatalogUtama(); }
            catch { return new DataTable(); }
        }

        public DataTable GetProdukLapak(int idPenjual)
        {
            try { return _productRepo.GetProdukLapak(idPenjual); }
            catch { return new DataTable(); }
        }

        public Product GetProdukById(int idProduk)
        {
            try { return _productRepo.GetById(idProduk); }
            catch (Exception ex) { Console.WriteLine("Error narik data produk: " + ex.Message); return null; }
        }
        public DataTable GetProdukDalamPO(int idPo)
        {
            try { return _productRepo.GetProdukDalamPO(idPo); }
            catch { return new DataTable(); }
        }
        public DataTable GetPOHampirPenuh()
        {
            try { return _productRepo.GetPOHampirPenuh(); }
            catch { return new DataTable(); }
        }
    }
}
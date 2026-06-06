using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Exceptions;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Services
{
    public class CartManager
    {
        private readonly int _idPembeli;

        // Static: bertahan saat pindah menu, tapi diisolasi per idPembeli
        private static readonly Dictionary<int, Dictionary<int, List<TransactionDetail>>>
            _semuaKeranjang = new Dictionary<int, Dictionary<int, List<TransactionDetail>>>();

        // Shortcut ke keranjang pembeli aktif
        private Dictionary<int, List<TransactionDetail>> Keranjang
        {
            get
            {
                if (!_semuaKeranjang.ContainsKey(this._idPembeli))
                    _semuaKeranjang[this._idPembeli] = new Dictionary<int, List<TransactionDetail>>();
                return _semuaKeranjang[this._idPembeli];
            }
        }

        public CartManager(int idPembeli)
        {
            if (idPembeli <= 0)
                throw new ArgumentException("idPembeli tidak valid.", nameof(idPembeli));
            this._idPembeli = idPembeli;
        }

        public void TambahItem(Product produk, string namaPenitip, int jumlah, string catatan)
        {
            if (produk == null)
                throw new ArgumentNullException(nameof(produk), "Produk tidak boleh kosong!");

            if (!produk.IdPo.HasValue)
                throw new InvalidOrderException("Produk ini tidak terikat dalam sesi PO!", "id_po", "CART_NO_PO");

            TransactionDetail detailBaru = new TransactionDetail(produk.IdProduk, namaPenitip, jumlah);
            detailBaru.Catatan = catatan;
            detailBaru.ProdukYangDipesan = produk;
            detailBaru.Validate();

            if (this.Keranjang.ContainsKey(produk.IdProduk))
                this.Keranjang[produk.IdProduk].Add(detailBaru);
            else
                this.Keranjang[produk.IdProduk] = new List<TransactionDetail> { detailBaru };

            produk.TambahPesanan(jumlah);
        }

        public long HitungTotalKeranjang()
        {
            long total = 0;
            foreach (var entry in this.Keranjang)
            {
                foreach (TransactionDetail detail in entry.Value)
                {
                    Product produk = detail.ProdukYangDipesan;
                    if (produk != null)
                    {
                        long hargaSaatIni = produk.HitungTotal();
                        long? hargaDiskon = produk.HargaDiskon.HasValue
                            ? (long?)Convert.ToInt64(produk.HargaDiskon.Value)
                            : null;
                        detail.FinalisasiHargaSaatCheckout(hargaSaatIni, hargaDiskon);
                    }
                    total += detail.HitungTotal();
                }
            }
            return total;
        }

        public Transaction BuildTransaction()
        {
            if (this.Keranjang.Count == 0)
                throw new InvalidOrderException("Keranjang kosong, tidak bisa checkout!", "", "CART_EMPTY");

            this.HitungTotalKeranjang();

            foreach (var entry in this.Keranjang)
            {
                int totalQty = 0;
                Product produkRef = null;
                foreach (var detail in entry.Value)
                {
                    totalQty += detail.JumlahPesanan;
                    if (produkRef == null) produkRef = detail.ProdukYangDipesan;
                }
                produkRef?.ValidasiTotalPesanan(totalQty);
            }

            Transaction transaksi = new Transaction(this._idPembeli);
            foreach (var entry in this.Keranjang)
            {
                foreach (TransactionDetail detail in entry.Value)
                {
                    detail.HitungRefundGotongRoyong();
                    transaksi.TambahDetail(detail);
                }
            }

            transaksi.Validate();
            return transaksi;
        }

        public void KosongkanKeranjang()
        {
            this.Keranjang.Clear();
        }

        public void HapusItem(int idProduk)
        {
            if (this.Keranjang.ContainsKey(idProduk))
                this.Keranjang.Remove(idProduk);
            // Tidak ditemukan → tidak ada yang dilakukan, bukan error
        }

        public void HapusDetailTitipan(int idProduk, string namaPenitip)
        {
            if (!this.Keranjang.ContainsKey(idProduk)) return;

            var list = this.Keranjang[idProduk];
            list.RemoveAll(d => d.NamaPenitip == namaPenitip);

            if (list.Count == 0)
                this.Keranjang.Remove(idProduk);
        }

        public void UpdateDetailTitipan(int idProduk, string oldPenitip, string newPenitip, int jumlah, string catatan)
        {
            if (!this.Keranjang.ContainsKey(idProduk)) return;

            var detail = this.Keranjang[idProduk].Find(d => d.NamaPenitip == oldPenitip);
            if (detail == null) return;

            Product p = detail.ProdukYangDipesan;
            int selisih = jumlah - detail.JumlahPesanan;

            this.Keranjang[idProduk].Remove(detail);

            TransactionDetail newDetail = new TransactionDetail(idProduk, newPenitip, jumlah);
            newDetail.Catatan = catatan;
            newDetail.ProdukYangDipesan = p;
            this.Keranjang[idProduk].Add(newDetail);

            if (selisih > 0) p?.TambahPesanan(selisih);
            else if (selisih < 0) p?.KurangiPesanan(-selisih);
        }

        public Dictionary<int, List<TransactionDetail>> GetKeranjangDictionary()
        {
            return this.Keranjang;
        }

        /// <summary>
        /// Hapus keranjang pembeli ini saat logout agar tidak bocor ke sesi berikutnya.
        /// Panggil dari MainForm saat tombol Logout ditekan.
        /// </summary>
        public static void BersihkanSesiPembeli(int idPembeli)
        {
            if (_semuaKeranjang.ContainsKey(idPembeli))
                _semuaKeranjang.Remove(idPembeli);
        }
    }
}
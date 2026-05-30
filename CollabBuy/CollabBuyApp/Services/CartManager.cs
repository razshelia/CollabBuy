using CollabBuy.CollabBuyApp.Models;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Services
{
    public class CartManager
    {
        private int _idPembeli;

        // PERBAIKAN UTAMA: Tambah kata 'static' agar keranjang tidak hilang saat pindah menu UI
        private static Dictionary<int, List<TransactionDetail>> _keranjangDict = new Dictionary<int, List<TransactionDetail>>();

        public CartManager(int idPembeli)
        {
            _idPembeli = idPembeli;
        }

        public void TambahItem(Product produk, string namaPenitip, int jumlah, string catatan)
        {
            if (produk == null) throw new ArgumentNullException("produk", "Produk tidak boleh kosong!");
            if (produk.GetIdPo().HasValue == false) throw new InvalidOrderException("Produk ini tidak terikat dalam sesi PO!", "id_po", "CART_NO_PO");

            TransactionDetail detailBaru = new TransactionDetail(produk.GetIdProduk(), namaPenitip, jumlah);
            detailBaru.SetCatatan(catatan);
            detailBaru.SetProduk(produk);
            detailBaru.Validate();

            int keyProduk = produk.GetIdProduk();
            if (_keranjangDict.ContainsKey(keyProduk))
            {
                _keranjangDict[keyProduk].Add(detailBaru);
            }
            else
            {
                _keranjangDict.Add(keyProduk, new List<TransactionDetail> { detailBaru });
            }

            produk.TambahPesanan(jumlah);
        }

        public long HitungTotalKeranjang()
        {
            long total = 0;
            foreach (KeyValuePair<int, List<TransactionDetail>> entry in _keranjangDict)
            {
                foreach (TransactionDetail detail in entry.Value)
                {
                    Product produk = detail.GetProduk();
                    if (produk != null)
                    {
                        long hargaSaatIni = produk.HitungTotal();
                        long? hargaDiskon = produk.GetHargaDiskon().HasValue ? Convert.ToInt64(produk.GetHargaDiskon().Value) : (long?)null;
                        detail.FinalisasiHargaSaatCheckout(hargaSaatIni, hargaDiskon);
                    }
                    total += detail.HitungTotal();
                }
            }
            return total;
        }

        public Transaction BuildTransaction()
        {
            if (_keranjangDict.Count == 0) throw new InvalidOrderException("Keranjang kosong, tidak bisa checkout!", "", "CART_EMPTY");

            HitungTotalKeranjang();
            Transaction transaksi = new Transaction(_idPembeli);

            foreach (KeyValuePair<int, List<TransactionDetail>> entry in _keranjangDict)
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

        public void KosongkanKeranjang() { _keranjangDict.Clear(); }

        public void HapusItem(int idProduk)
        {
            if (_keranjangDict.ContainsKey(idProduk)) _keranjangDict.Remove(idProduk);
        }

        // =======================================================
        // METHOD BARU UNTUK UI KELOLA TITIPAN
        // =======================================================
        public void HapusDetailTitipan(int idProduk, string namaPenitip)
        {
            if (_keranjangDict.ContainsKey(idProduk))
            {
                var list = _keranjangDict[idProduk];
                list.RemoveAll(d => d.GetNamaPenitip() == namaPenitip);
                if (list.Count == 0) _keranjangDict.Remove(idProduk);
            }
        }

        public void UpdateDetailTitipan(int idProduk, string oldPenitip, string newPenitip, int jumlah, string catatan)
        {
            if (_keranjangDict.ContainsKey(idProduk))
            {
                var detail = _keranjangDict[idProduk].Find(d => d.GetNamaPenitip() == oldPenitip);
                if (detail != null)
                {
                    Product p = detail.GetProduk();
                    int selisih = jumlah - detail.GetJumlahPesanan();

                    _keranjangDict[idProduk].Remove(detail);

                    TransactionDetail newDetail = new TransactionDetail(idProduk, newPenitip, jumlah);
                    newDetail.SetCatatan(catatan);
                    newDetail.SetProduk(p);
                    _keranjangDict[idProduk].Add(newDetail);

                    p.TambahPesanan(selisih); // Update sisa kuota Gotong Royong di RAM
                }
            }
        }

        public Dictionary<int, List<TransactionDetail>> GetKeranjangDictionary() { return _keranjangDict; }
    }
}
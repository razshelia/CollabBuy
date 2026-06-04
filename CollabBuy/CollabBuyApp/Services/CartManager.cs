using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Exceptions; // <-- INI YANG BIKIN ERROR SEBELUMNYA!
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
            this._idPembeli = idPembeli;
        }

        public void TambahItem(Product produk, string namaPenitip, int jumlah, string catatan)
        {
            if (produk == null)
            {
                throw new ArgumentNullException("produk", "Produk tidak boleh kosong!");
            }
            else
            {
                if (produk.IdPo.HasValue == false)
                {
                    throw new InvalidOrderException("Produk ini tidak terikat dalam sesi PO!", "id_po", "CART_NO_PO");
                }
                else
                {
                    TransactionDetail detailBaru = new TransactionDetail(produk.IdProduk, namaPenitip, jumlah);
                    detailBaru.Catatan = catatan;
                    detailBaru.ProdukYangDipesan = produk;
                    detailBaru.Validate();

                    int keyProduk = produk.IdProduk;

                    // OOP BEST PRACTICE: Pemanggilan static member memakai nama Class
                    if (CartManager._keranjangDict.ContainsKey(keyProduk))
                    {
                        CartManager._keranjangDict[keyProduk].Add(detailBaru);
                    }
                    else
                    {
                        CartManager._keranjangDict.Add(keyProduk, new List<TransactionDetail> { detailBaru });
                    }

                    produk.TambahPesanan(jumlah);
                }
            }
        }

        public long HitungTotalKeranjang()
        {
            long total = 0;

            foreach (KeyValuePair<int, List<TransactionDetail>> entry in CartManager._keranjangDict)
            {
                foreach (TransactionDetail detail in entry.Value)
                {
                    Product produk = detail.ProdukYangDipesan;

                    if (produk != null)
                    {
                        long hargaSaatIni = produk.HitungTotal();
                        long? hargaDiskon;

                        if (produk.HargaDiskon.HasValue)
                        {
                            hargaDiskon = Convert.ToInt64(produk.HargaDiskon.Value);
                        }
                        else
                        {
                            hargaDiskon = (long?)null;
                        }

                        detail.FinalisasiHargaSaatCheckout(hargaSaatIni, hargaDiskon);
                    }
                    else
                    {
                        bool produkKosong = true; // Assignment untuk menghindari else kosong
                    }

                    total += detail.HitungTotal();
                }
            }

            return total;
        }

        public Transaction BuildTransaction()
        {
            Transaction transaksi;

            if (CartManager._keranjangDict.Count == 0)
            {
                throw new InvalidOrderException("Keranjang kosong, tidak bisa checkout!", "", "CART_EMPTY");
            }
            else
            {
                this.HitungTotalKeranjang();
                foreach (var entry in CartManager._keranjangDict)
                {
                    int totalQtyProdukIni = 0;
                    Product produkRef = null;

                    foreach (var detail in entry.Value)
                    {
                        totalQtyProdukIni += detail.JumlahPesanan;
                        if (produkRef == null) produkRef = detail.ProdukYangDipesan;
                    }

                    if (produkRef != null)
                    {
                        produkRef.ValidasiTotalPesanan(totalQtyProdukIni);
                    }
                }

                transaksi = new Transaction(this._idPembeli);

                foreach (KeyValuePair<int, List<TransactionDetail>> entry in CartManager._keranjangDict)
                {
                    foreach (TransactionDetail detail in entry.Value)
                    {
                        detail.HitungRefundGotongRoyong();
                        transaksi.TambahDetail(detail);
                    }
                }

                transaksi.Validate();
            }

            return transaksi;
        }

        public void KosongkanKeranjang()
        {
            CartManager._keranjangDict.Clear();
        }

        public void HapusItem(int idProduk)
        {
            if (CartManager._keranjangDict.ContainsKey(idProduk))
            {
                CartManager._keranjangDict.Remove(idProduk);
            }
            else
            {
                bool itemTidakAda = true;
            }
        }

        // =======================================================
        // METHOD BARU UNTUK UI KELOLA TITIPAN
        // =======================================================
        public void HapusDetailTitipan(int idProduk, string namaPenitip)
        {
            if (CartManager._keranjangDict.ContainsKey(idProduk))
            {
                var list = CartManager._keranjangDict[idProduk];
                list.RemoveAll(d => d.NamaPenitip == namaPenitip);

                if (list.Count == 0)
                {
                    CartManager._keranjangDict.Remove(idProduk);
                }
                else
                {
                    bool sisaItemLain = true;
                }
            }
            else
            {
                bool itemTidakAda = true;
            }
        }

        public void UpdateDetailTitipan(int idProduk, string oldPenitip, string newPenitip, int jumlah, string catatan)
        {
            if (CartManager._keranjangDict.ContainsKey(idProduk))
            {
                var detail = CartManager._keranjangDict[idProduk].Find(d => d.NamaPenitip == oldPenitip);

                if (detail != null)
                {
                    Product p = detail.ProdukYangDipesan;
                    int selisih = jumlah - detail.JumlahPesanan;

                    CartManager._keranjangDict[idProduk].Remove(detail);

                    TransactionDetail newDetail = new TransactionDetail(idProduk, newPenitip, jumlah);
                    newDetail.Catatan = catatan;
                    newDetail.ProdukYangDipesan = p;
                    CartManager._keranjangDict[idProduk].Add(newDetail);

                    p.TambahPesanan(selisih); // Update sisa kuota Gotong Royong di RAM
                }
                else
                {
                    bool detailTidakAda = true;
                }
            }
            else
            {
                bool itemTidakAda = true;
            }
        }

        public Dictionary<int, List<TransactionDetail>> GetKeranjangDictionary()
        {
            return CartManager._keranjangDict;
        }
    }
}
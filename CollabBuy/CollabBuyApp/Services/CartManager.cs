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
                if (produk.GetIdPo().HasValue == false)
                {
                    throw new InvalidOrderException("Produk ini tidak terikat dalam sesi PO!", "id_po", "CART_NO_PO");
                }
                else
                {
                    TransactionDetail detailBaru = new TransactionDetail(produk.GetIdProduk(), namaPenitip, jumlah);
                    detailBaru.SetCatatan(catatan);
                    detailBaru.SetProduk(produk);
                    detailBaru.Validate();

                    int keyProduk = produk.GetIdProduk();

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
                    Product produk = detail.GetProduk();

                    if (produk != null)
                    {
                        long hargaSaatIni = produk.HitungTotal();
                        long? hargaDiskon;

                        if (produk.GetHargaDiskon().HasValue)
                        {
                            hargaDiskon = Convert.ToInt64(produk.GetHargaDiskon().Value);
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
                list.RemoveAll(d => d.GetNamaPenitip() == namaPenitip);

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
                var detail = CartManager._keranjangDict[idProduk].Find(d => d.GetNamaPenitip() == oldPenitip);

                if (detail != null)
                {
                    Product p = detail.GetProduk();
                    int selisih = jumlah - detail.GetJumlahPesanan();

                    CartManager._keranjangDict[idProduk].Remove(detail);

                    TransactionDetail newDetail = new TransactionDetail(idProduk, newPenitip, jumlah);
                    newDetail.SetCatatan(catatan);
                    newDetail.SetProduk(p);
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
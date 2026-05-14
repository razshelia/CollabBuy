using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Katalog
    {
        // AGGREGATION: Katalog MENGUMPULKAN barang PO.
        // Jika Katalog dihapus, barang PO di database masih tetap ada.
        private List<PreOrder> daftarPO;

        public Katalog()
        {
            this.daftarPO = new List<PreOrder>();
        }

        public void TambahKeKatalog(PreOrder poBaru)
        {
            if (poBaru != null)
            {
                this.daftarPO.Add(poBaru);
            }
            else
            {
                // Abaikan jika null
            }
        }

        // POLYMORPHISM OVERLOADING 1: Mencari berdasarkan Nama (String)
        public PreOrder CariBarang(string namaBarang)
        {
            if (string.IsNullOrWhiteSpace(namaBarang))
            {
                return null;
            }
            else
            {
                // Logika pencarian sederhana
                foreach (PreOrder item in this.daftarPO)
                {
                    if (item.NamaBarang.ToLower().Contains(namaBarang.ToLower()))
                    {
                        return item;
                    }
                    else
                    {
                        continue;
                    }
                }
                return null;
            }
        }

        // POLYMORPHISM OVERLOADING 2: Mencari berdasarkan Kategori/ID (Integer)
        // Method name sama, parameter berbeda
        public PreOrder CariBarang(int idKategori)
        {
            if (idKategori <= 0)
            {
                return null;
            }
            else
            {
                // Implementasi pencarian kategori (simulasi)
                if (this.daftarPO.Count > 0)
                {
                    return this.daftarPO[0];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
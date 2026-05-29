using System;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas turunan dari User yang bertindak sebagai pembeli/koordinator titip.
    /// Dulunya bernama Koordinator, diubah sesuai permintaan.
    /// 
    /// Pemetaan Database:
    /// - Tabel: users (peran = 'User')
    /// </summary>
    public class Pembeli : User
    {
        // === PRIVATE FIELDS ===
        // Pembeli tidak punya kolom khusus di DB, tapi sebagai OOP objek, 
        // dia bisa punya keranjang belanja In-Memory.
        private List<Transaction> _riwayatTransaksi;

        // === KONSTRUKTOR ===
        public Pembeli(string nama, string username, string password)
            : base(nama, username, password, "User")
        {
            _riwayatTransaksi = new List<Transaction>();
        }

        // === METHOD BISNIS LOGIC ===
        public void TambahRiwayatTransaksi(Transaction transaksi)
        {
            if (transaksi != null)
            {
                _riwayatTransaksi.Add(transaksi);
            }
        }

        public List<Transaction> GetRiwayatTransaksi()
        {
            return _riwayatTransaksi;
        }


        // === OVERRIDE METHOD ABSTRAK (POLIMORFISME) ===
        public override string GetTipeUser()
        {
            return "Pembeli / Koordinator Titip";
        }


        // === OVERRIDE VALIDATE ===
        public override void Validate()
        {
            base.Validate(); // Validasi dasar cukup untuk pembeli
        }
    }
}
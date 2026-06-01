using System;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas turunan dari User yang bertindak sebagai pembeli/koordinator titip.
    /// Memiliki behaviour khusus seperti level keanggotaan dan riwayat jajan.
    /// </summary>
    public class Pembeli : User
    {
        // === PRIVATE FIELDS ===
        private List<Transaction> _riwayatTransaksi;

        // === KONSTRUKTOR ===
        public Pembeli(string nama, string username, string password)
            : base(nama, username, password, "User")
        {
            this._riwayatTransaksi = new List<Transaction>();
        }

        // === GETTER & SETTER DENGAN ENKAPSULASI STRICT ===
        public List<Transaction> GetRiwayatTransaksi()
        {
            return this._riwayatTransaksi;
        }

        public void TambahRiwayatTransaksi(Transaction transaksi)
        {
            if (transaksi == null)
            {
                throw new InvalidOrderException("Transaksi tidak boleh kosong/null!", "transaksi", "TRX_NULL");
            }
            else
            {
                this._riwayatTransaksi.Add(transaksi);
            }
        }

        // === OVERRIDE METHOD ABSTRAK (POLIMORFISME) ===
        public override string GetTipeUser()
        {
            return "Pembeli / Koordinator Titip";
        }

        // === METHOD BISNIS (BEHAVIOR KHUSUS PEMBELI) ===

        /// <summary>
        /// Menghitung jumlah transaksi yang sudah pernah dilakukan pembeli ini.
        /// </summary>
        public int DapatkanTotalTransaksi()
        {
            int total;
            if (this._riwayatTransaksi == null)
            {
                total = 0;
            }
            else
            {
                total = this._riwayatTransaksi.Count;
            }
            return total;
        }

        /// <summary>
        /// Mengembalikan level pembeli berdasarkan seberapa sering dia jajan.
        /// Sangat bagus untuk fitur UI seperti "Badge" pengguna.
        /// </summary>
        public string DapatkanLevelPembeli()
        {
            string level;
            int jumlahJajan = this.DapatkanTotalTransaksi();

            if (jumlahJajan > 20)
            {
                level = "👑 Sultan Jajan";
            }
            else if (jumlahJajan > 5)
            {
                level = "🌟 Langganan Setia";
            }
            else
            {
                level = "🌱 Member Baru";
            }

            return level;
        }

        // === OVERRIDE VALIDATE (IF-ELSE KETAT) ===
        public override void Validate()
        {
            bool validasiListSelesai;

            base.Validate(); // Panggil validasi dari class Induk (User)

            if (this._riwayatTransaksi == null)
            {
                throw new InvalidOrderException("Validasi gagal: List riwayat transaksi belum diinisialisasi.", "riwayat_transaksi", "PEMBELI_INVALID");
            }
            else
            {
                validasiListSelesai = true; // Assignment nyata agar else tidak kosong
            }
        }
    }
}
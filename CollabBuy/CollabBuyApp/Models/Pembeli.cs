using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas turunan dari User yang bertindak sebagai pembeli/koordinator titip.
    /// Memiliki behaviour khusus seperti level keanggotaan dan riwayat jajan.
    /// </summary>
    public class Pembeli : User
    {
        // === PRIVATE FIELDS (Backing Field) ===
        private List<Transaction> _riwayatTransaksi;

        // === PROPERTIES ===
        // Best Practice: Untuk List, gunakan get saja agar isi List tidak bisa di-overwrite
        // oleh class lain (mencegah data riwayat hilang tidak sengaja).
        public List<Transaction> RiwayatTransaksi
        {
            get { return this._riwayatTransaksi; }
        }

        // === KONSTRUKTOR ===
        public Pembeli(string nama, string username, string password)
            : base(nama, username, password, "User")
        {
            this._riwayatTransaksi = new List<Transaction>();
        }

        // === ENKAPSULASI STRICT (BEHAVIOR LIST) ===
        public void TambahRiwayatTransaksi(Transaction transaksi)
        {
            // Guard clause tanpa else
            if (transaksi == null)
            {
                throw new InvalidOrderException("Transaksi tidak boleh kosong/null!", "transaksi", "TRX_NULL");
            }

            this._riwayatTransaksi.Add(transaksi);
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
            // Menggunakan Ternary Operator agar ringkas jadi 1 baris
            return this._riwayatTransaksi == null ? 0 : this._riwayatTransaksi.Count;
        }

        /// <summary>
        /// Mengembalikan level pembeli berdasarkan seberapa sering dia jajan.
        /// Sangat bagus untuk fitur UI seperti "Badge" pengguna.
        /// </summary>
        public string DapatkanLevelPembeli()
        {
            int jumlahJajan = this.DapatkanTotalTransaksi();

            // Early return beruntun tanpa pusing pakai else if / else
            if (jumlahJajan > 20) return "👑 Sultan Jajan";
            if (jumlahJajan > 5) return "🌟 Langganan Setia";

            return "🌱 Member Baru";
        }

        // === OVERRIDE VALIDATE ===
        public override void Validate()
        {
            base.Validate(); // Panggil validasi dari class Induk (User)

            if (this._riwayatTransaksi == null)
            {
                throw new InvalidOrderException("Validasi gagal: List riwayat transaksi belum diinisialisasi.", "riwayat_transaksi", "PEMBELI_INVALID");
            }

            // Variabel 'validasiListSelesai' dihapus karena itu dead code (tidak pernah dibaca sistem)
        }
    }
}
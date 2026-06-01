using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk mencatat riwayat aktivitas pengguna (Audit Trail).
    /// Mengimplementasikan IValidatable.
    /// </summary>
    public class ActivityLog : IValidatable
    {
        // === PRIVATE FIELDS ===
        private int _idLog;
        private int _idUser;
        private string _aktivitas;
        private DateTime _waktuAkses;

        // === KONSTRUKTOR ===
        public ActivityLog(int idUser, string aktivitas)
        {
            this.SetIdUser(idUser);
            this.SetAktivitas(aktivitas);
            this.SetWaktuAkses(DateTime.Now);
        }

        // === GETTER & SETTER DENGAN GUARD CLAUSES ===
        public int GetIdLog()
        {
            return this._idLog;
        }

        public void SetIdLog(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOrderException("ID Log tidak boleh kurang dari atau sama dengan nol!", "id_log", "LOG_ID_INVALID");
            }
            this._idLog = id;
        }

        public int GetIdUser()
        {
            return this._idUser;
        }

        public void SetIdUser(int idUser)
        {
            if (idUser <= 0)
            {
                throw new InvalidOrderException("ID User tidak boleh kurang dari atau sama dengan nol!", "id_user", "USER_ID_INVALID");
            }
            this._idUser = idUser;
        }

        public string GetAktivitas()
        {
            return this._aktivitas;
        }

        public void SetAktivitas(string aktivitas)
        {
            if (string.IsNullOrEmpty(aktivitas))
            {
                throw new InvalidOrderException("Deskripsi aktivitas log tidak boleh kosong!", "aktivitas", "LOG_KOSONG");
            }
            this._aktivitas = aktivitas;
        }

        public DateTime GetWaktuAkses()
        {
            return this._waktuAkses;
        }

        public void SetWaktuAkses(DateTime waktu)
        {
            if (waktu == DateTime.MinValue)
            {
                throw new InvalidOrderException("Waktu akses tidak valid!", "waktu_akses", "WAKTU_INVALID");
            }
            this._waktuAkses = waktu;
        }

        // =========================================================
        // IMPLEMENTASI METODE BISNIS / BEHAVIOR
        // 4 Method ini membuat Class memiliki peran utuh (Bukan sekadar penampung data)
        // =========================================================

        // Method 1: Validasi Integritas Data (Standar Interface)
        public void Validate()
        {
            if (string.IsNullOrEmpty(this._aktivitas))
            {
                throw new InvalidOrderException("Log tidak valid: Aktivitas kosong.", "aktivitas", "LOG_INVALID");
            }

            if (this._idUser <= 0)
            {
                throw new InvalidOrderException("Log tidak valid: User tidak dikenali.", "id_user", "LOG_USER_INVALID");
            }
        }

        // Method 2: Data Formatting (Untuk Export/Report)
        /// <summary>
        /// Mengembalikan representasi log dalam format teks rapi.
        /// Berguna untuk diekspor ke Notepad (.txt) atau untuk Admin.
        /// </summary>
        public string DapatkanFormatLog()
        {
            string waktuTeks = this._waktuAkses.ToString("yyyy-MM-dd HH:mm:ss");
            return $"[{waktuTeks}] User ID {this._idUser} - {this._aktivitas}";
        }

        // Method 3: Klasifikasi / Kategorisasi Cerdas
        /// <summary>
        /// Menganalisis teks aktivitas dan mengembalikan jenis/kategori log.
        /// Sangat berguna untuk memberikan Label/Warna Badge di UI atau filter cepat.
        /// </summary>
        public string DapatkanKategori()
        {
            string aksi = this._aktivitas.ToLower();

            if (aksi.Contains("login") || aksi.Contains("logout") || aksi.Contains("password"))
                return "Autentikasi";

            if (aksi.Contains("hapus") || aksi.Contains("blokir") || aksi.Contains("tolak"))
                return "Tindakan Kritis";

            if (aksi.Contains("checkout") || aksi.Contains("bayar") || aksi.Contains("keranjang") || aksi.Contains("transaksi"))
                return "Transaksi";

            if (aksi.Contains("tambah") || aksi.Contains("ubah") || aksi.Contains("update"))
                return "Perubahan Data";

            return "Informasi Umum";
        }

        // Method 4: Pemeriksaan Waktu Temporal
        /// <summary>
        /// Mengecek apakah aktivitas ini terjadi pada hari ini.
        /// Sangat umum digunakan di Dashboard untuk melihat "Aktivitas Hari Ini".
        /// </summary>
        public bool ApakahHariIni()
        {
            return this._waktuAkses.Date == DateTime.Now.Date;
        }
    }
}
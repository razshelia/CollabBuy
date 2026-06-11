using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk mengelola Aduan / Laporan Kendala (Complaint) dari pengguna.
    /// Mengimplementasikan IValidatable.
    /// </summary>
    public class Complaint : IValidatable, IResolvable
    {
        // === PRIVATE FIELDS (Backing Fields) ===
        private int _idAduan;
        private int _idUser;
        private string _jenisAduan;
        private string _deskripsi;
        private string _status;
        private DateTime _tanggalAduan;
        private string _tanggapanAdmin;

        // === PROPERTIES (Get & Set dalam satu blok dengan Guard Clauses) ===
        public int IdAduan
        {
            get { return this._idAduan; }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidOrderException("ID Aduan tidak valid!", "id_aduan", "ADUAN_ID_INVALID");
                }
                this._idAduan = value;
            }
        }

        public int IdUser
        {
            get { return this._idUser; }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidOrderException("ID User pelapor tidak valid!", "id_user", "ADUAN_USER_INVALID");
                }
                this._idUser = value;
            }
        }

        public string JenisAduan
        {
            get { return this._jenisAduan; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOrderException("Jenis aduan wajib diisi!", "jenis_aduan", "ADUAN_JENIS_KOSONG");
                }
                this._jenisAduan = value.Trim();
            }
        }

        public string Deskripsi
        {
            get { return this._deskripsi; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOrderException("Deskripsi kendala tidak boleh kosong bestie!", "deskripsi", "ADUAN_DESKRIPSI_KOSONG");
                }

                if (value.Trim().Length < 20)
                {
                    throw new InvalidOrderException("Deskripsi kendala minimal 20 karakter. Ceritain lebih detail ya!", "deskripsi", "ADUAN_DESKRIPSI_PENDEK");
                }

                if (value.Trim().Length > 1000)
                {
                    throw new InvalidOrderException("Deskripsi kendala maksimal 1000 karakter!", "deskripsi", "ADUAN_DESKRIPSI_PANJANG");
                }

                this._deskripsi = value.Trim();
            }
        }

        public string Status
        {
            get { return this._status; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOrderException("Status tidak boleh kosong!", "status", "ADUAN_STATUS_KOSONG");
                }
                this._status = value.Trim();
            }
        }

        public DateTime TanggalAduan
        {
            get { return this._tanggalAduan; }
            set
            {
                if (value == DateTime.MinValue)
                {
                    throw new InvalidOrderException("Tanggal aduan tidak valid!", "tanggal", "ADUAN_TANGGAL_INVALID");
                }
                this._tanggalAduan = value;
            }
        }

        public string TanggapanAdmin
        {
            get { return this._tanggapanAdmin; }
            set
            {
                // Tanggapan admin boleh null jika memang belum dibalas
                if (string.IsNullOrWhiteSpace(value))
                {
                    this._tanggapanAdmin = "";
                    return; // Early return sebagai pengganti else
                }
                this._tanggapanAdmin = value.Trim();
            }
        }

        // === KONSTRUKTOR ===
        public Complaint(int idUser, string jenisAduan, string deskripsi)
        {
            this.IdUser = idUser;
            this.JenisAduan = jenisAduan;
            this.Deskripsi = deskripsi;
            this.Status = "Menunggu"; // Status default saat baru dibuat
            this.TanggalAduan = DateTime.Now;
        }

        // =========================================================
        // IMPLEMENTASI METODE BISNIS / BEHAVIOR (OOP BEST PRACTICE)
        // =========================================================

        // Method 1: Validasi
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(this._jenisAduan) || string.IsNullOrWhiteSpace(this._deskripsi))
            {
                throw new InvalidOrderException("Aduan tidak valid, jenis dan deskripsi harus lengkap.", "validasi_aduan", "ADUAN_INVALID");
            }
            // Blok else yang kosong dihapus karena tidak berguna (dead code)
        }

        // Method 2: Cerdas Menampilkan Preview Teks (Untuk Datagrid)
        /// <summary>
        /// Memotong teks deskripsi jika terlalu panjang agar rapi di tabel UI.
        /// </summary>
        public string DapatkanPreviewDeskripsi(int batasKarakter)
        {
            if (string.IsNullOrWhiteSpace(this._deskripsi)) return "";

            if (this._deskripsi.Length <= batasKarakter) return this._deskripsi;

            return this._deskripsi.Substring(0, batasKarakter) + "...";
        }

        // Method 3: Formatting Status UI
        /// <summary>
        /// Mengembalikan status lengkap dengan emoji agar UI lebih interaktif.
        /// </summary>
        public string DapatkanStatusUI()
        {
            string statusLower = this._status.ToLower();

            // Menggunakan multiple if dengan return akan otomatis memutus eksekusi ke bawah
            if (statusLower == "menunggu") return "⏳ Menunggu Respon";
            if (statusLower == "diproses") return "⚙️ Sedang Dicek";
            if (statusLower == "selesai") return "✅ Selesai";
            if (statusLower == "ditolak") return "❌ Ditolak";

            return "❓ " + this._status;
        }

        // Method 4: Logika Membalas Aduan (Behavior Utama Admin)
        /// <summary>
        /// Dieksekusi saat Admin membalas aduan. Otomatis mengubah status dan menyimpan teks balasan.
        /// </summary>
        public void BerikanTanggapan(string teksBalasan, bool isSelesai)
        {
            if (string.IsNullOrWhiteSpace(teksBalasan))
            {
                throw new InvalidOrderException("Balasan admin tidak boleh kosong!", "tanggapan", "ADUAN_BALASAN_KOSONG");
            }

            // Gunakan setter property yang sudah kita buat
            this.TanggapanAdmin = teksBalasan;

            // Menggunakan Ternary Operator C# agar ringkas pengganti if-else
            this.Status = isSelesai ? "Selesai" : "Diproses";
        }

        public void BeriTanggapan(string tanggapan)
        {
            this.BerikanTanggapan(tanggapan, true);
        }

        public bool IsSelesai()
        {
            return this._status != null && this._status.ToLower() == "selesai";
        }

        public string GetTanggapan()
        {
            return this._tanggapanAdmin;
        }
    }
}
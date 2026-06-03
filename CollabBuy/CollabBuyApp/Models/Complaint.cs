using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk mengelola Aduan / Laporan Kendala (Complaint) dari pengguna.
    /// Mengimplementasikan IValidatable.
    /// </summary>
    public class Complaint : IValidatable
    {
        // === PRIVATE FIELDS ===
        private int _idAduan;
        private int _idUser;
        private string _jenisAduan;
        private string _deskripsi;
        private string _status;
        private DateTime _tanggalAduan;
        private string _tanggapanAdmin;

        // === KONSTRUKTOR ===
        public Complaint(int idUser, string jenisAduan, string deskripsi)
        {
            this.SetIdUser(idUser);
            this.SetJenisAduan(jenisAduan);
            this.SetDeskripsi(deskripsi);
            this.SetStatus("Menunggu"); // Status default saat baru dibuat
            this.SetTanggalAduan(DateTime.Now);
        }

        // === GETTER & SETTER DENGAN ENKAPSULASI PENUH (IF-ELSE) ===
        public int GetIdAduan()
        {
            return this._idAduan;
        }

        public void SetIdAduan(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOrderException("ID Aduan tidak valid!", "id_aduan", "ADUAN_ID_INVALID");
            }
            else
            {
                this._idAduan = id;
            }
        }

        public int GetIdUser()
        {
            return this._idUser;
        }

        public void SetIdUser(int idUser)
        {
            if (idUser <= 0)
            {
                throw new InvalidOrderException("ID User pelapor tidak valid!", "id_user", "ADUAN_USER_INVALID");
            }
            else
            {
                this._idUser = idUser;
            }
        }

        public string GetJenisAduan()
        {
            return this._jenisAduan;
        }

        public void SetJenisAduan(string jenis)
        {
            if (string.IsNullOrWhiteSpace(jenis))
            {
                throw new InvalidOrderException("Jenis aduan wajib diisi!", "jenis_aduan", "ADUAN_JENIS_KOSONG");
            }
            else
            {
                this._jenisAduan = jenis.Trim();
            }
        }

        public string GetDeskripsi()
        {
            return this._deskripsi;
        }

        public void SetDeskripsi(string deskripsi)
        {
            if (string.IsNullOrWhiteSpace(deskripsi))
            {
                throw new InvalidOrderException("Deskripsi kendala tidak boleh kosong bestie!", "deskripsi", "ADUAN_DESKRIPSI_KOSONG");
            }
            else if (deskripsi.Trim().Length < 20)
            {
                throw new InvalidOrderException("Deskripsi kendala minimal 20 karakter. Ceritain lebih detail ya!", "deskripsi", "ADUAN_DESKRIPSI_PENDEK");
            }
            else if (deskripsi.Trim().Length > 1000)
            {
                throw new InvalidOrderException("Deskripsi kendala maksimal 1000 karakter!", "deskripsi", "ADUAN_DESKRIPSI_PANJANG");
            }
            else
            {
                this._deskripsi = deskripsi.Trim();
            }
        }

        public string GetStatus()
        {
            return this._status;
        }

        public void SetStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                throw new InvalidOrderException("Status tidak boleh kosong!", "status", "ADUAN_STATUS_KOSONG");
            }
            else
            {
                this._status = status.Trim();
            }
        }

        public DateTime GetTanggalAduan()
        {
            return this._tanggalAduan;
        }

        public void SetTanggalAduan(DateTime tanggal)
        {
            if (tanggal == DateTime.MinValue)
            {
                throw new InvalidOrderException("Tanggal aduan tidak valid!", "tanggal", "ADUAN_TANGGAL_INVALID");
            }
            else
            {
                this._tanggalAduan = tanggal;
            }
        }

        public string GetTanggapanAdmin()
        {
            return this._tanggapanAdmin;
        }

        public void SetTanggapanAdmin(string tanggapan)
        {
            // Tanggapan admin boleh null jika memang belum dibalas
            if (string.IsNullOrWhiteSpace(tanggapan))
            {
                this._tanggapanAdmin = "";
            }
            else
            {
                this._tanggapanAdmin = tanggapan.Trim();
            }
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
            else
            {
                // Lolos validasi
            }
        }

        // Method 2: Cerdas Menampilkan Preview Teks (Untuk Datagrid)
        /// <summary>
        /// Memotong teks deskripsi jika terlalu panjang agar rapi di tabel UI.
        /// </summary>
        public string DapatkanPreviewDeskripsi(int batasKarakter)
        {
            if (string.IsNullOrWhiteSpace(this._deskripsi))
            {
                return "";
            }
            else
            {
                if (this._deskripsi.Length <= batasKarakter)
                {
                    return this._deskripsi;
                }
                else
                {
                    return this._deskripsi.Substring(0, batasKarakter) + "...";
                }
            }
        }

        // Method 3: Formatting Status UI
        /// <summary>
        /// Mengembalikan status lengkap dengan emoji agar UI lebih interaktif.
        /// </summary>
        public string DapatkanStatusUI()
        {
            string statusLower = this._status.ToLower();

            if (statusLower == "menunggu")
            {
                return "⏳ Menunggu Respon";
            }
            else if (statusLower == "diproses")
            {
                return "⚙️ Sedang Dicek";
            }
            else if (statusLower == "selesai")
            {
                return "✅ Selesai";
            }
            else if (statusLower == "ditolak")
            {
                return "❌ Ditolak";
            }
            else
            {
                return "❓ " + this._status;
            }
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
            else
            {
                this.SetTanggapanAdmin(teksBalasan);

                if (isSelesai)
                {
                    this.SetStatus("Selesai");
                }
                else
                {
                    this.SetStatus("Diproses");
                }
            }
        }
    }
}
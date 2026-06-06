using CollabBuy.CollabBuyApp.Models.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk permohonan verifikasi menjadi Penjual.
    /// Mengimplementasikan IValidatable dan IApprovable dengan Strict Encapsulation.
    /// </summary>
    public class Verification : IValidatable, IApprovable
    {
        // === PRIVATE FIELDS (Backing Fields) ===
        private int _idVerifikasi;
        private int _idUser;
        private string _nim;
        private string _namaToko;
        private byte[] _buktiKtm;
        private int _tahunMasuk;
        private bool _isVerifikasi;
        private string _alasanPenolakan;

        // === PROPERTIES (Get & Set dengan Guard Clauses) ===

        public int IdVerifikasi
        {
            get { return this._idVerifikasi; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID Verifikasi tidak valid!", "id_verifikasi", "VERIF_ID_INVALID");
                this._idVerifikasi = value;
            }
        }

        public int IdUser
        {
            get { return this._idUser; }
            set
            {
                if (value <= 0)
                    throw new InvalidOrderException("ID User pendaftar tidak valid!", "id_user", "VERIF_USER_INVALID");
                this._idUser = value;
            }
        }

        public string Nim
        {
            get { return this._nim; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("NIM wajib diisi untuk verifikasi!", "nim", "NIM_KOSONG");

                if (value.Trim().Length < 8)
                    throw new InvalidOrderException("NIM minimal 8 karakter!", "nim", "NIM_PENDEK");

                if (value.Trim().Length > 20)
                    throw new InvalidOrderException("NIM maksimal 20 karakter!", "nim", "NIM_PANJANG");

                this._nim = value.Trim();
            }
        }

        public string NamaToko
        {
            get { return this._namaToko; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOrderException("Nama toko wajib diisi!", "nama_toko", "TOKO_KOSONG");

                if (value.Trim().Length < 3)
                    throw new InvalidOrderException("Nama toko minimal 3 karakter!", "nama_toko", "TOKO_PENDEK");

                if (value.Trim().Length > 60)
                    throw new InvalidOrderException("Nama toko maksimal 60 karakter!", "nama_toko", "TOKO_PANJANG");

                this._namaToko = value.Trim();
            }
        }

        public byte[] BuktiKtm
        {
            get { return this._buktiKtm; }
            set
            {
                if (value == null || value.Length == 0)
                    throw new InvalidOrderException("Bukti KTM wajib di-upload!", "bukti_ktm", "KTM_KOSONG");

                if (value.Length > 2097152) // 2MB
                    throw new InvalidOrderException("Ukuran file bukti KTM maksimal 2MB!", "bukti_ktm", "KTM_OVERSIZE");

                this._buktiKtm = value;
            }
        }

        public int TahunMasuk
        {
            get { return this._tahunMasuk; }
            set
            {
                if (value >= 0 && value <= 99)
                    value = 2000 + value;

                if (value < 2000 || value > DateTime.Now.Year)
                    throw new InvalidOrderException(
                        $"Tahun masuk tidak valid! Isi dengan tahun 4 digit (2000–{DateTime.Now.Year}).",
                        "tahun_masuk", "TAHUN_INVALID");

                this._tahunMasuk = value;
            }
        }
        // Properti Read-Only untuk atribut yang diatur oleh sistem/admin
        public string AlasanPenolakan
        {
            get { return this._alasanPenolakan; }
        }

        // === KONSTRUKTOR ===
        public Verification(int idUser, string nim, string namaToko, byte[] buktiKtm, int tahunMasuk)
        {
            this.IdUser = idUser;
            this.Nim = nim;
            this.NamaToko = namaToko;
            this.BuktiKtm = buktiKtm;
            this.TahunMasuk = tahunMasuk;

            this._isVerifikasi = false;
            this._alasanPenolakan = "";
        }

        // =========================================================
        // PROACTIVE BUSINESS LOGIC & UI HELPER BEHAVIORS
        // =========================================================

        public string DapatkanInfoPendaftar()
        {
            if (string.IsNullOrWhiteSpace(this._namaToko) || string.IsNullOrWhiteSpace(this._nim))
            {
                return "Data Pendaftar Belum Lengkap";
            }

            return $"🏢 {this._namaToko} | 🎓 NIM: {this._nim} (Angkatan {this._tahunMasuk})";
        }

        public string DapatkanStatusVerifikasiUI()
        {
            if (this._isVerifikasi) return "✅ Disetujui (Aktif)";

            if (!string.IsNullOrWhiteSpace(this._alasanPenolakan)) return "❌ Ditolak: " + this._alasanPenolakan;

            return "⏳ Menunggu Review Admin";
        }

        /// <summary>
        /// Mengecek apakah pendaftar merupakan mahasiswa angkatan baru (Maba).
        /// </summary>
        public bool ApakahMahasiswaBaru()
        {
            // Evaluasi kondisi perbandingan secara langsung
            return this._tahunMasuk >= (DateTime.Now.Year - 1);
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            // Guard clauses vertikal tanpa variabel penampung yang membuang memori
            if (string.IsNullOrWhiteSpace(this._nim))
                throw new InvalidOrderException("Verifikasi gagal: NIM kosong.", "nim", "VERIF_INVALID");

            if (this._buktiKtm == null || this._buktiKtm.Length == 0)
                throw new InvalidOrderException("Verifikasi gagal: KTM belum di-upload.", "bukti_ktm", "VERIF_INVALID");
        }

        // === IMPLEMENTASI IApprovable ===
        public void Approve()
        {
            this._isVerifikasi = true;
            this._alasanPenolakan = "";
        }

        public void Reject(string alasan)
        {
            if (string.IsNullOrWhiteSpace(alasan))
                throw new InvalidOrderException("Alasan penolakan verifikasi wajib diisi!", "alasan", "REJECT_KOSONG");

            this._isVerifikasi = false;
            this._alasanPenolakan = alasan.Trim();
        }

        public bool GetStatusPersetujuan()
        {
            return this._isVerifikasi;
        }
    }
}
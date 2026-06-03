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
        // === PRIVATE FIELDS ===
        private int _idVerifikasi;
        private int _idUser;
        private string _nim;
        private string _namaToko;
        private byte[] _buktiKtm;
        private int _tahunMasuk;
        private bool _isVerifikasi;
        private string _alasanPenolakan; // Tambahan agar alasan reject tersimpan

        // === KONSTRUKTOR ===
        public Verification(int idUser, string nim, string namaToko, byte[] buktiKtm, int tahunMasuk)
        {
            this.SetIdUser(idUser);
            this.SetNim(nim);
            this.SetNamaToko(namaToko);
            this.SetBuktiKtm(buktiKtm);
            this.SetTahunMasuk(tahunMasuk);

            this._isVerifikasi = false;
            this._alasanPenolakan = "";
        }

        // === GETTER & SETTER DENGAN ENKAPSULASI KETAT (IF-ELSE) ===
        public int GetIdVerifikasi()
        {
            return this._idVerifikasi;
        }

        public void SetIdVerifikasi(int id)
        {
            if (id <= 0)
            {
                throw new InvalidOrderException("ID Verifikasi tidak valid!", "id_verifikasi", "VERIF_ID_INVALID");
            }
            else
            {
                this._idVerifikasi = id;
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
                throw new InvalidOrderException("ID User pendaftar tidak valid!", "id_user", "VERIF_USER_INVALID");
            }
            else
            {
                this._idUser = idUser;
            }
        }

        public string GetNim()
        {
            return this._nim;
        }

        public void SetNim(string nim)
        {
            if (string.IsNullOrWhiteSpace(nim))
            {
                throw new InvalidOrderException("NIM wajib diisi untuk verifikasi!", "nim", "NIM_KOSONG");
            }
            else if (nim.Trim().Length < 8)
            {
                throw new InvalidOrderException("NIM minimal 8 karakter!", "nim", "NIM_PENDEK");
            }
            else if (nim.Trim().Length > 20)
            {
                throw new InvalidOrderException("NIM maksimal 20 karakter!", "nim", "NIM_PANJANG");
            }
            else
            {
                this._nim = nim.Trim();
            }
        }

        public string GetNamaToko()
        {
            return this._namaToko;
        }

        public void SetNamaToko(string namaToko)
        {
            if (string.IsNullOrWhiteSpace(namaToko))
            {
                throw new InvalidOrderException("Nama toko wajib diisi!", "nama_toko", "TOKO_KOSONG");
            }
            else if (namaToko.Trim().Length < 3)
            {
                throw new InvalidOrderException("Nama toko minimal 3 karakter!", "nama_toko", "TOKO_PENDEK");
            }
            else if (namaToko.Trim().Length > 60)
            {
                throw new InvalidOrderException("Nama toko maksimal 60 karakter!", "nama_toko", "TOKO_PANJANG");
            }
            else
            {
                this._namaToko = namaToko.Trim();
            }
        }

        public byte[] GetBuktiKtm()
        {
            return this._buktiKtm;
        }

        public void SetBuktiKtm(byte[] bukti)
        {
            if (bukti == null || bukti.Length == 0)
            {
                throw new InvalidOrderException("Bukti KTM wajib di-upload!", "bukti_ktm", "KTM_KOSONG");
            }
            else if (bukti.Length > 2097152) // 2MB
            {
                throw new InvalidOrderException("Ukuran file bukti KTM maksimal 2MB!", "bukti_ktm", "KTM_OVERSIZE");
            }
            else
            {
                this._buktiKtm = bukti;
            }
        }

        public int GetTahunMasuk()
        {
            return this._tahunMasuk;
        }

        public void SetTahunMasuk(int tahun)
        {
            if (tahun < 2000 || tahun > DateTime.Now.Year)
            {
                throw new InvalidOrderException("Tahun masuk tidak valid!", "tahun_masuk", "TAHUN_INVALID");
            }
            else
            {
                this._tahunMasuk = tahun;
            }
        }

        public string GetAlasanPenolakan()
        {
            return this._alasanPenolakan;
        }

        // =========================================================
        // PROACTIVE BUSINESS LOGIC & UI HELPER BEHAVIORS
        // =========================================================

        public string DapatkanInfoPendaftar()
        {
            string info;
            if (string.IsNullOrWhiteSpace(this._namaToko) || string.IsNullOrWhiteSpace(this._nim))
            {
                info = "Data Pendaftar Belum Lengkap";
            }
            else
            {
                info = $"🏢 {this._namaToko} | 🎓 NIM: {this._nim} (Angkatan {this._tahunMasuk})";
            }
            return info;
        }

        public string DapatkanStatusVerifikasiUI()
        {
            string statusUi;

            if (this._isVerifikasi)
            {
                statusUi = "✅ Disetujui (Aktif)";
            }
            else if (!string.IsNullOrWhiteSpace(this._alasanPenolakan))
            {
                statusUi = "❌ Ditolak: " + this._alasanPenolakan;
            }
            else
            {
                statusUi = "⏳ Menunggu Review Admin";
            }

            return statusUi;
        }

        /// <summary>
        /// Mengecek apakah pendaftar merupakan mahasiswa angkatan baru (Maba).
        /// </summary>
        public bool ApakahMahasiswaBaru()
        {
            bool isMaba;
            int tahunSekarang = DateTime.Now.Year;

            if (this._tahunMasuk >= (tahunSekarang - 1))
            {
                isMaba = true;
            }
            else
            {
                isMaba = false;
            }

            return isMaba;
        }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            bool cekNim;
            bool cekKtm;

            if (string.IsNullOrWhiteSpace(this._nim))
            {
                throw new InvalidOrderException("Verifikasi gagal: NIM kosong.", "nim", "VERIF_INVALID");
            }
            else
            {
                cekNim = true; // Penugasan nyata agar else tidak kosong
            }

            if (this._buktiKtm == null || this._buktiKtm.Length == 0)
            {
                throw new InvalidOrderException("Verifikasi gagal: KTM belum di-upload.", "bukti_ktm", "VERIF_INVALID");
            }
            else
            {
                cekKtm = cekNim; // Chain penugasan nyata
            }
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
            {
                throw new InvalidOrderException("Alasan penolakan verifikasi wajib diisi!", "alasan", "REJECT_KOSONG");
            }
            else
            {
                this._isVerifikasi = false;
                this._alasanPenolakan = alasan.Trim();
            }
        }

        public bool GetStatusPersetujuan()
        {
            return this._isVerifikasi;
        }
    }
}
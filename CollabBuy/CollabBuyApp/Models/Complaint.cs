using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk Aduan/Pengaduan dari pengguna.
    /// Mengimplementasikan IValidatable dan IResolvable.
    /// 
    /// Pemetaan Database:
    /// - Tabel: complaints
    /// - Kolom: is_selesai, balasan
    /// </summary>
    public class Complaint : IValidatable, IResolvable
    {
        // === PRIVATE FIELDS ===
        private int _idAduan;
        private int _idUser;
        private string _subjek;
        private string _deskripsi;
        private DateTime _tanggal;
        private bool _isSelesai;
        private string _balasan;

        // === KONSTRUKTOR ===
        public Complaint(int idUser, string subjek, string deskripsi)
        {
            _idUser = idUser;
            SetSubjek(subjek);
            SetDeskripsi(deskripsi);
            _tanggal = DateTime.Now;
            _isSelesai = false;
            _balasan = "";
        }

        // === GETTER & SETTER ===
        public int GetIdAduan() { return _idAduan; }
        public void SetIdAduan(int id) { _idAduan = id; }

        public int GetIdUser() { return _idUser; }

        public string GetSubjek() { return _subjek; }
        public void SetSubjek(string subjek)
        {
            if (string.IsNullOrEmpty(subjek))
            {
                throw new InvalidOrderException("Subjek aduan tidak boleh kosong!", "subjek", "ADUAN_SUBJEK_KOSONG");
            }
            _subjek = subjek;
        }

        public string GetDeskripsi() { return _deskripsi; }
        public void SetDeskripsi(string deskripsi)
        {
            if (string.IsNullOrEmpty(deskripsi))
            {
                throw new InvalidOrderException("Deskripsi aduan wajib diisi lengkap!", "deskripsi", "ADUAN_DESK_KOSONG");
            }
            _deskripsi = deskripsi;
        }

        public DateTime GetTanggal() { return _tanggal; }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (string.IsNullOrEmpty(_subjek)) { throw new InvalidOrderException("Aduan tidak valid: Subjek kosong.", "subjek", "ADUAN_INVALID"); }
            if (string.IsNullOrEmpty(_deskripsi)) { throw new InvalidOrderException("Aduan tidak valid: Deskripsi kosong.", "deskripsi", "ADUAN_INVALID"); }
        }

        // === IMPLEMENTASI IResolvable ===
        public void BeriTanggapan(string tanggapan)
        {
            if (string.IsNullOrEmpty(tanggapan))
            {
                throw new InvalidOrderException("Balasan admin tidak boleh kosong!", "balasan", "ADUAN_BALAS_KOSONG");
            }
            _balasan = tanggapan;
            _isSelesai = true;
        }

        public bool IsSelesai()
        {
            return _isSelesai;
        }

        public string GetTanggapan()
        {
            return _balasan;
        }
    }
}
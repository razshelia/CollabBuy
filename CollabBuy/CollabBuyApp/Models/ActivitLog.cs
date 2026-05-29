using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Models.Interfaces;
using System;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk mencatat riwayat aktivitas pengguna (Audit Trail).
    /// Mengimplementasikan IValidatable.
    /// 
    /// Pemetaan Database:
    /// - Tabel: activity_logs
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
            _idUser = idUser;
            SetAktivitas(aktivitas);
            _waktuAkses = DateTime.Now;
        }

        // === GETTER & SETTER ===
        public int GetIdLog() { return _idLog; }
        public void SetIdLog(int id) { _idLog = id; }

        public int GetIdUser() { return _idUser; }

        public string GetAktivitas() { return _aktivitas; }
        public void SetAktivitas(string aktivitas)
        {
            if (string.IsNullOrEmpty(aktivitas))
            {
                throw new InvalidOrderException("Deskripsi aktivitas log tidak boleh kosong!", "aktivitas", "LOG_KOSONG");
            }
            _aktivitas = aktivitas;
        }

        public DateTime GetWaktuAkses() { return _waktuAkses; }

        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (string.IsNullOrEmpty(_aktivitas))
            {
                throw new InvalidOrderException("Log tidak valid: Aktivitas kosong.", "aktivitas", "LOG_INVALID");
            }
            if (_idUser <= 0)
            {
                throw new InvalidOrderException("Log tidak valid: User tidak dikenali.", "id_user", "LOG_USER_INVALID");
            }
        }
    }
}
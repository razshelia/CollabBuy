using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Models.Interfaces;

namespace CollabBuy.CollabBuyApp.Models
{
    /// <summary>
    /// Kelas Model untuk Sesi Pre-Order.
    /// Mengimplementasikan IValidatable, IStatusTrackable, dan IQuotaTrackable (Agregat).
    /// 
    /// Pemetaan Database:
    /// - Tabel: preorders
    /// - Relasi: Menjadi induk dari kumpulan Product (Agregasi In-Memory)
    /// </summary>
    public class PreOrder : IValidatable, IStatusTrackable, IQuotaTrackable
    {
        // === PRIVATE FIELDS ===
        private int _idPo;
        private int _idPenjual;
        private string _judulPo;
        private string _jenisPo; // "Biasa" atau "Gotong Royong"
        private string _infoRekening;
        private DateTime _batasWaktu;
        private bool _isAktif;

        // Struktur Data In-Memory (Agregasi)
        // PreOrder memantau produk-produk yang ada di dalamnya
        private List<Product> _produkDiPo;

        // === KONSTRUKTOR ===
        public PreOrder(int idPenjual, string judulPo, string jenisPo, string infoRekening, DateTime batasWaktu)
        {
            _idPenjual = idPenjual;
            SetJudulPo(judulPo);
            SetJenisPo(jenisPo);
            SetInfoRekening(infoRekening);
            SetBatasWaktu(batasWaktu);

            _isAktif = true;
            _produkDiPo = new List<Product>();
        }


        // === GETTER & SETTER DENGAN VALIDASI ===
        public int GetIdPo() { return _idPo; }
        public void SetIdPo(int id) { _idPo = id; }

        public int GetIdPenjual() { return _idPenjual; }

        public string GetJudulPo() { return _judulPo; }
        public void SetJudulPo(string judul)
        {
            if (string.IsNullOrEmpty(judul))
            {
                throw new InvalidOrderException("Judul PO wajib diisi!", "judul_po", "PO_JUDUL_KOSONG");
            }
            _judulPo = judul;
        }

        public string GetJenisPo() { return _jenisPo; }
        public void SetJenisPo(string jenis)
        {
            if (jenis != "Biasa" && jenis != "Gotong Royong")
            {
                throw new InvalidOrderException("Jenis PO hanya boleh 'Biasa' atau 'Gotong Royong'!", "jenis_po", "PO_JENIS_INVALID");
            }
            _jenisPo = jenis;
        }

        public string GetInfoRekening() { return _infoRekening; }
        public void SetInfoRekening(string rekening)
        {
            if (string.IsNullOrEmpty(rekening))
            {
                throw new InvalidOrderException("Info rekening wajib diisi!", "info_rekening", "PO_REK_KOSONG");
            }
            _infoRekening = rekening;
        }

        public DateTime GetBatasWaktu() { return _batasWaktu; }
        public void SetBatasWaktu(DateTime batas)
        {
            // Validasi bisnis: Batas waktu tidak boleh di masa lalu saat PO dibuat
            if (batas < DateTime.Now)
            {
                throw new InvalidOrderException("Batas waktu PO tidak boleh di masa lalu!", "batas_waktu", "PO_WAKTU_INVALID");
            }
            _batasWaktu = batas;
        }


        // === METHOD BISNIS LOGIC (Manajemen Produk di PO) ===

        /// <summary>
        /// Menambahkan produk ke dalam sesi PO di memori (RAM).
        /// </summary>
        public void TambahProduk(Product produk)
        {
            if (produk == null)
            {
                throw new InvalidOrderException("Produk yang ditambahkan tidak boleh null!", "produk", "PO_PRODUK_NULL");
            }
            // Validasi kepemilikan (Menggantikan Trigger trg_cek_kepemilikan_po)
            if (produk.GetIdPenjual() != _idPenjual)
            {
                throw new InvalidOrderException("Produk ini bukan milik penjual PO!", "id_penjual", "PO_KEPEMILIKAN_INVALID");
            }
            _produkDiPo.Add(produk);
        }

        public List<Product> GetSemuaProduk()
        {
            return _produkDiPo;
        }


        // === IMPLEMENTASI IValidatable ===
        public void Validate()
        {
            if (string.IsNullOrEmpty(_judulPo))
            {
                throw new InvalidOrderException("Validasi gagal: Judul PO kosong.", "judul_po", "PO_INVALID");
            }
            if (_jenisPo != "Biasa" && _jenisPo != "Gotong Royong")
            {
                throw new InvalidOrderException("Validasi gagal: Jenis PO tidak sesuai.", "jenis_po", "PO_INVALID");
            }
        }


        // === IMPLEMENTASI IStatusTrackable ===
        public string GetStatus()
        {
            if (_isAktif)
            {
                return "Aktif";
            }
            return "Tutup";
        }

        public bool BisaDiubahKe(string statusBaru)
        {
            string statusSaatIni = GetStatus();
            if (statusSaatIni == "Aktif" && statusBaru == "Tutup")
            {
                return true;
            }
            return false;
        }

        public void UbahStatus(string statusBaru)
        {
            if (BisaDiubahKe(statusBaru) == false)
            {
                throw new InvalidOrderException("Status PO tidak bisa diubah ke '" + statusBaru + "'!", "is_aktif", "PO_STATUS_INVALID");
            }
            _isAktif = false;
        }


        // === IMPLEMENTASI IQuotaTrackable (Agregat dari Produk) ===
        /// <summary>
        /// Mendapatkan total target kuota dari semua produk di dalam PO.
        /// </summary>
        public int GetTargetKuota()
        {
            int total = 0;
            foreach (Product p in _produkDiPo)
            {
                total = total + p.GetTargetKuota();
            }
            return total;
        }

        /// <summary>
        /// Mendapatkan total pesanan dari semua produk di dalam PO.
        /// </summary>
        public int GetTerpesan()
        {
            int total = 0;
            foreach (Product p in _produkDiPo)
            {
                total = total + p.GetTerpesan();
            }
            return total;
        }

        public int GetSisaKuota()
        {
            return GetTargetKuota() - GetTerpesan();
        }

        /// <summary>
        /// Kuota PO dianggap terpenuhi jika SEMUA produk di dalamnya sudah terpenuhi.
        /// </summary>
        public bool IsKuotaTerpenuhi()
        {
            if (_produkDiPo.Count == 0) { return false; }

            foreach (Product p in _produkDiPo)
            {
                if (p.IsKuotaTerpenuhi() == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
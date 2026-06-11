using System;

namespace CollabBuy.CollabBuyApp.Exceptions // Folder dan Namespace baru yang lebih rapi!
{
    /// <summary>
    /// Custom Exception untuk menangani error validasi aturan bisnis di aplikasi CollabBuy.
    /// </summary>
    public class InvalidOrderException : AppException

    {
        // === PRIVATE FIELDS ===
        private string _fieldYangError;
        private string _aturanYangDilanggar;

        // === KONSTRUKTOR ===
        public InvalidOrderException(string pesan) : base(pesan)
        {
            this.FieldYangError = "";
            this.AturanYangDilanggar = "";
        }

        public InvalidOrderException(string pesan, string fieldYangError, string aturanYangDilanggar) : base(pesan)
        {
            this.FieldYangError = fieldYangError;
            this.AturanYangDilanggar = aturanYangDilanggar;
        }

        public InvalidOrderException(string pesan, string fieldYangError, string aturanYangDilanggar, Exception innerException) : base(pesan, innerException)
        {
            this.FieldYangError = fieldYangError;
            this.AturanYangDilanggar = aturanYangDilanggar;
        }

        // === GETTER & SETTER (ENKAPSULASI PENUH DENGAN IF-ELSE AKTIF) ===
        public string FieldYangError
        {
            get { return this._fieldYangError; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this._fieldYangError = "Tidak Spesifik";
                else
                    this._fieldYangError = value.Trim();
            }
        }

        public string AturanYangDilanggar
        {
            get { return this._aturanYangDilanggar; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this._aturanYangDilanggar = "Aturan Umum";
                else
                    this._aturanYangDilanggar = value.Trim();
            }
        }

        // === BEHAVIOR / METHOD BISNIS ===
        public string GetPesanLengkap()
        {
            string pesanLengkap = this.Message;
            string tambahanField;
            string tambahanAturan;

            // Setiap IF memiliki ELSE yang melakukan operasi assignment nyata (tidak kosong)
            if (this.FieldYangError != "Tidak Spesifik")
            {
                tambahanField = " [Field: " + this.FieldYangError + "]";
            }
            else
            {
                tambahanField = ""; // Assignment string kosong secara eksplisit
            }

            if (this.AturanYangDilanggar != "Aturan Umum")
            {
                tambahanAturan = " [Aturan: " + this.AturanYangDilanggar + "]";
            }
            else
            {
                tambahanAturan = ""; // Assignment string kosong secara eksplisit
            }

            // Gabungkan semua komponen menjadi satu pesan utuh
            pesanLengkap = pesanLengkap + tambahanField + tambahanAturan;

            return pesanLengkap;
        }
        public override string GetKategoriError()
        {
            return "Pelanggaran Aturan Bisnis";
        }

    }
}
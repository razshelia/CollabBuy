using System;

namespace CollabBuy.CollabBuyApp.Exceptions // Folder dan Namespace baru yang lebih rapi!
{
    /// <summary>
    /// Custom Exception untuk menangani error validasi aturan bisnis di aplikasi CollabBuy.
    /// </summary>
    public class InvalidOrderException : Exception
    {
        // === PRIVATE FIELDS ===
        private string _fieldYangError;
        private string _aturanYangDilanggar;

        // === KONSTRUKTOR ===
        public InvalidOrderException(string pesan) : base(pesan)
        {
            this.SetFieldYangError("");
            this.SetAturanYangDilanggar("");
        }

        public InvalidOrderException(string pesan, string fieldYangError, string aturanYangDilanggar) : base(pesan)
        {
            this.SetFieldYangError(fieldYangError);
            this.SetAturanYangDilanggar(aturanYangDilanggar);
        }

        public InvalidOrderException(string pesan, string fieldYangError, string aturanYangDilanggar, Exception innerException) : base(pesan, innerException)
        {
            this.SetFieldYangError(fieldYangError);
            this.SetAturanYangDilanggar(aturanYangDilanggar);
        }

        // === GETTER & SETTER (ENKAPSULASI PENUH DENGAN IF-ELSE AKTIF) ===
        public string GetFieldYangError()
        {
            return this._fieldYangError;
        }

        public void SetFieldYangError(string field)
        {
            // Logika validasi: Cegah data null/spasi kosong
            if (string.IsNullOrWhiteSpace(field))
            {
                this._fieldYangError = "Tidak Spesifik";
            }
            else
            {
                this._fieldYangError = field.Trim();
            }
        }

        public string GetAturanYangDilanggar()
        {
            return this._aturanYangDilanggar;
        }

        public void SetAturanYangDilanggar(string aturan)
        {
            // Logika validasi: Cegah data null/spasi kosong
            if (string.IsNullOrWhiteSpace(aturan))
            {
                this._aturanYangDilanggar = "Aturan Umum";
            }
            else
            {
                this._aturanYangDilanggar = aturan.Trim();
            }
        }

        // === BEHAVIOR / METHOD BISNIS ===
        public string GetPesanLengkap()
        {
            string pesanLengkap = this.Message;
            string tambahanField;
            string tambahanAturan;

            // Setiap IF memiliki ELSE yang melakukan operasi assignment nyata (tidak kosong)
            if (this._fieldYangError != "Tidak Spesifik")
            {
                tambahanField = " [Field: " + this._fieldYangError + "]";
            }
            else
            {
                tambahanField = ""; // Assignment string kosong secara eksplisit
            }

            if (this._aturanYangDilanggar != "Aturan Umum")
            {
                tambahanAturan = " [Aturan: " + this._aturanYangDilanggar + "]";
            }
            else
            {
                tambahanAturan = ""; // Assignment string kosong secara eksplisit
            }

            // Gabungkan semua komponen menjadi satu pesan utuh
            pesanLengkap = pesanLengkap + tambahanField + tambahanAturan;

            return pesanLengkap;
        }
    }
}
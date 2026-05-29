using System;
using System.Collections.Generic;
using System.Text;

namespace CollabBuy.CollabBuyApp.Models
{
    public class InvalidOrderException : Exception
    {
        private string _fieldYangError;
        private string _aturanYangDilanggar;
        public InvalidOrderException(string pesan)
            : base(pesan)
        {
            _fieldYangError = "";
            _aturanYangDilanggar = "";
        }
        public InvalidOrderException(string pesan, string fieldYangError, string aturanYangDilanggar)
            : base(pesan)
        {
            _fieldYangError = fieldYangError;
            _aturanYangDilanggar = aturanYangDilanggar;
        }
        public InvalidOrderException(string pesan, string fieldYangError, string aturanYangDilanggar, Exception innerException)
            : base(pesan, innerException)
        {
            _fieldYangError = fieldYangError;
            _aturanYangDilanggar = aturanYangDilanggar;
        }
        public string GetFieldYangError()
        {
            return _fieldYangError;
        }
        public string GetAturanYangDilanggar()
        {
            return _aturanYangDilanggar;
        }
        public string GetPesanLengkap()
        {
            string pesan = Message;
            if (string.IsNullOrEmpty(_fieldYangError) == false)
            {
                pesan = pesan + " [Field: " + _fieldYangError + "]";
            }
            if (string.IsNullOrEmpty(_aturanYangDilanggar) == false)
            {
                pesan = pesan + " [Aturan: " + _aturanYangDilanggar + "]";
            }
            return pesan;
        }
    }
}

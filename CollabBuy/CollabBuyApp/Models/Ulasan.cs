using System;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Ulasan
    {
        private int rating;
        private string komentar;

        public int Rating
        {
            get { return this.rating; }
            set
            {
                if (value < 1)
                {
                    this.rating = 1;
                }
                else
                {
                    if (value > 5)
                    {
                        this.rating = 5;
                    }
                    else
                    {
                        this.rating = value;
                    }
                }
            }
        }

        public string Komentar
        {
            get { return this.komentar; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.komentar = "Tidak ada komentar.";
                }
                else
                {
                    this.komentar = value;
                }
            }
        }
    }
}
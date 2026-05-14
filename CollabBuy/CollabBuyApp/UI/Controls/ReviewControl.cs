using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ReviewControl : UserControl
    {
        private InteractionService interactionService;

        public ReviewControl()
        {
            this.InitializeComponent();
            this.interactionService = new InteractionService();
        }

        private void btnKirimTesti_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtKomentar.Text))
            {
                UXHelper.TampilkanError("Tulis dikit dong testimoninya, jangan pelit kata-kata! ✍️");
            }
            else
            {
                Review ulasanBaru = new Review();
                ulasanBaru.Rating = (int)this.numRating.Value;
                ulasanBaru.Komentar = this.txtKomentar.Text;

                if (this.interactionService.KirimUlasan(ulasanBaru))
                {
                    this.txtKomentar.Clear();
                    this.numRating.Value = 5;
                    // UXHelper sukses dipanggil dari dalam Service
                }
            }
        }
    }
}
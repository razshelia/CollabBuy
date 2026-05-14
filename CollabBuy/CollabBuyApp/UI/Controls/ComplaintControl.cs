using System;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ComplaintControl : UserControl
    {
        private InteractionService interactionService;

        public ComplaintControl()
        {
            this.InitializeComponent();
            this.interactionService = new InteractionService();
        }

        private void btnKirim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtSubjek.Text) ||
                string.IsNullOrWhiteSpace(this.txtPesan.Text))
            {
                UXHelper.TampilkanError("Subjek sama isi aduannya jangan dikosongin ya, Bestie! Nanti mimin bingung.");
                return;
            }

            Aduan aduanBaru = new Aduan();
            aduanBaru.Subjek = this.txtSubjek.Text;
            aduanBaru.Pesan = this.txtPesan.Text;

            if (UXHelper.TampilkanKonfirmasi("Udah bener ceritanya? Mau dikirim ke Admin sekarang?"))
            {
                bool sukses = this.interactionService.KirimAduan(aduanBaru);
                if (sukses)
                {
                    this.txtSubjek.Clear();
                    this.txtPesan.Clear();
                }
            }
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class ComplaintControl : UserControl
    {
        private int _idUser;

        public ComplaintControl()
        {
            InitializeComponent();
            this.Resize += (s, e) => CenterCard();
            if (ParentForm is MainForm main)
            {
                var user = main.AmbilUserAktif();
                if (user != null)
                    _idUser = user.IdUser;
            }
        }

        private void CenterCard()
        {
            if (pnlCard == null) return;
            pnlCard.Left = (this.ClientSize.Width - pnlCard.Width) / 2;
            pnlCard.Top = (this.ClientSize.Height - pnlCard.Height) / 2;
        }

        private void btnKirim_Click(object sender, EventArgs e)
        {
            string subjek = txtSubjek.Text.Trim();
            string deskripsi = txtDeskripsi.Text.Trim();

            if (string.IsNullOrWhiteSpace(subjek))
            {
                UXHelper.TampilkanError("Subjek aduan jangan kosong ya, bestie! 📝");
                return;
            }
            if (string.IsNullOrWhiteSpace(deskripsi))
            {
                UXHelper.TampilkanError("Ceritain dulu detailnya, biar admin paham 😊");
                return;
            }

            var complaintService = new ComplaintService();
            bool sukses = complaintService.KirimAduan(_idUser, subjek, deskripsi);
            if (sukses)
            {
                txtSubjek.Clear();
                txtDeskripsi.Clear();
            }
        }

        // Tombol baru: lihat riwayat aduan
        private void btnLihatAduanSaya_Click(object sender, EventArgs e)
        {
            if (ParentForm is MainForm main)
            {
                main.GantiHalaman(new ComplaintHistoryControl(_idUser));
            }
        }
    }
}
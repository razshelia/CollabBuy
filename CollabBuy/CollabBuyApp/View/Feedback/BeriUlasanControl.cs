using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Feedback
{
    public partial class BeriUlasanControl : UserControl
    {
        private readonly Models.User _currentUser;
        private readonly ReviewController _controller;

        public BeriUlasanControl(Models.User user)
        {
            this.InitializeComponent();
            this._currentUser = user;
            this._controller = new ReviewController();

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void BeriUlasanControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.LoadProduk();
        }

        private void LoadProduk()
        {
            DataTable dt = this._controller.GetListProdukBuatDiulas(this._currentUser.GetIdUser());
            this.cbProduk.DataSource = dt;
            this.cbProduk.DisplayMember = "nama_produk";
            this.cbProduk.ValueMember = "id_produk";

            if (dt.Rows.Count == 0)
            {
                this.btnKirim.Enabled = false;
                this.btnKirim.BackColor = Color.FromArgb(210, 210, 210);
                this.btnKirim.ForeColor = Color.FromArgb(140, 140, 140);
                MessageBox.Show("Belum ada barang beres yang bisa di-review nih. Jajan dulu gih!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.btnKirim.Enabled = true;
                this.btnKirim.BackColor = Color.FromArgb(36, 0, 70);
                this.btnKirim.ForeColor = Color.FromArgb(224, 170, 255);
            }
        }

        private void btnKirim_Click(object sender, EventArgs e)
        {
            if (this.cbProduk.SelectedValue == null)
            {
                MessageBox.Show("Pilih dulu produk yang mau diulas ya!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int idProduk = Convert.ToInt32(this.cbProduk.SelectedValue);
                var (sukses, pesan) = this._controller.GasNgasihRating(idProduk, this._currentUser.GetIdUser(), (int)this.numRating.Value, this.txtKomentar.Text);

                if (sukses)
                {
                    MessageBox.Show(pesan, "Suksessss!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtKomentar.Clear();
                    this.numRating.Value = 5;
                }
                else
                {
                    MessageBox.Show(pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void AdjustLayout()
        {
            int margin = 38;
            int w = this.Width - (margin * 2);

            if (w > 600)
            {
                w = 600;
            }
            else
            {
                // Tetap gunakan lebar yang sudah dihitung
                bool tidakPerluDibatasi = true;
            }

            this.pnlForm.Width = w;
            this.pnlForm.Left = (this.Width - w) / 2;

            int innerW = w - 48;
            this.cbProduk.Width = innerW;
            this.txtKomentar.Width = innerW;
            this.btnKirim.Width = innerW;
        }
    }
}
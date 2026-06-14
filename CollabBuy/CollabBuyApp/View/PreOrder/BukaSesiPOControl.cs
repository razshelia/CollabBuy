using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    public partial class BukaSesiPOControl : UserControl
    {
        private readonly Models.User _currentSeller;
        private readonly PreOrderController _poController;

        public BukaSesiPOControl(Models.User seller)
        {
            this.InitializeComponent();

            this._currentSeller = seller;
            this._poController = new PreOrderController();

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void BukaSesiPOControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.dtpBatasWaktu.MinDate = DateTime.Now;
            this.cbJenisPO.SelectedIndex = 0;
            this.LoadMasterProduk();
            // Sembunyikan numQuota karena kuota diatur per-produk saat tambah ke sesi PO
            this.numQuota.Visible = false;
            this.lblQuota.Visible = false;
        }

        private void btnSimpanSesi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtNamaSesi.Text) || string.IsNullOrWhiteSpace(this.txtRekening.Text))
            {
                MessageBox.Show("Nama sesi dan rekening tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Buka sesi PO '{this.txtNamaSesi.Text}'?\n\nSetelah ini, tambahkan produk ke sesi ini lewat menu Manajemen Produk.",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var (sukses, pesan, _) = this._poController.BukaSesiPOBaru(
                this._currentSeller.IdUser,
                this.txtNamaSesi.Text,
                this.cbJenisPO.Text,
                this.txtRekening.Text,
                this.dtpBatasWaktu.Value
            );

            if (sukses)
            {
                MessageBox.Show(pesan, "Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ResetForm();
            }
            else
            {
                MessageBox.Show(pesan, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMasterProduk()
        {
            this.btnSimpanSesi.Enabled = true;
            this.btnSimpanSesi.BackColor = Color.FromArgb(36, 0, 70);
            this.btnSimpanSesi.ForeColor = Color.FromArgb(253, 255, 182);
        }

        private void ResetForm()
        {
            this.txtNamaSesi.Clear();
            this.txtRekening.Clear();
            this.cbJenisPO.SelectedIndex = 0;
            this.numQuota.Value = 10;
            this.dtpBatasWaktu.Value = DateTime.Now.AddDays(1);
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            if (w < 400) w = 400;

            this.pnlForm.Width = w;

            int innerW = w - 68;
            this.txtNamaSesi.Width = (int)(innerW * 0.57);

            this.lblJenis.Left = this.txtNamaSesi.Left + this.txtNamaSesi.Width + 20;
            this.cbJenisPO.Left = this.lblJenis.Left;
            this.cbJenisPO.Width = innerW - this.txtNamaSesi.Width - 20;

            this.dtpBatasWaktu.Width = Math.Min(320, innerW);
            this.txtRekening.Width = innerW;
            this.btnSimpanSesi.Width = innerW;
        }
    }
}
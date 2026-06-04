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
        }

        private void btnSimpanSesi_Click(object sender, EventArgs e)
        {
            if (this.cbProduk.SelectedValue == null)
            {
                MessageBox.Show("Pilih dulu barang yang mau dijual ngab!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult confirm = MessageBox.Show(
                    $"Udah yakin mau launching sesi '{this.txtNamaSesi.Text}'? Gaskeun?",
                    "CollabBuy - Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    int idProduk = Convert.ToInt32(this.cbProduk.SelectedValue);
                    int targetKuota = Convert.ToInt32(this.numQuota.Value);

                    var (sukses, pesan) = this._poController.GasLuncurkanPO(
                        this._currentSeller.GetIdUser(),
                        this.txtNamaSesi.Text,
                        this.cbJenisPO.Text,
                        this.txtRekening.Text,
                        this.dtpBatasWaktu.Value,
                        idProduk,
                        targetKuota
                    );

                    if (sukses)
                    {
                        MessageBox.Show(pesan, "CollabBuy - Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ResetForm();
                        this.LoadMasterProduk(); 
                    }
                    else
                    {
                        MessageBox.Show(pesan, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    bool batalLaunch = true;
                }
            }
        }

        private void LoadMasterProduk()
        {
            try
            {
                DataTable dtProduk = this._poController.GetProdukTersedia(this._currentSeller.GetIdUser());

                this.cbProduk.DataSource = dtProduk;
                this.cbProduk.DisplayMember = "nama_produk";
                this.cbProduk.ValueMember = "id_produk";

                if (dtProduk != null && dtProduk.Rows.Count == 0)
                {
                    MessageBox.Show("Kamu belum punya produk aktif nih. Daftarin produk dulu di menu Manajemen Produk!",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.btnSimpanSesi.Enabled = false;
                    this.btnSimpanSesi.BackColor = Color.FromArgb(210, 210, 210);
                    this.btnSimpanSesi.ForeColor = Color.FromArgb(140, 140, 140);
                }
                else
                {
                    this.btnSimpanSesi.Enabled = true;
                    this.btnSimpanSesi.BackColor = Color.FromArgb(36, 0, 70);
                    this.btnSimpanSesi.ForeColor = Color.FromArgb(253, 255, 182);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat produk: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            if (w < 400)
            {
                w = 400;
            }
            else
            {
                bool lebarAman = true; 
            }

            this.pnlForm.Width = w;

            int innerW = this.pnlForm.Width - 68;
            this.txtNamaSesi.Width = (int)(innerW * 0.58);

            this.cbJenisPO.Left = this.txtNamaSesi.Left + this.txtNamaSesi.Width + 20;
            this.cbJenisPO.Width = innerW - this.txtNamaSesi.Width - 20;
            this.lblJenis.Left = this.cbJenisPO.Left;

            this.cbProduk.Width = innerW;
            this.txtRekening.Width = innerW;
            this.btnSimpanSesi.Width = innerW;

            this.numQuota.Width = (int)(innerW * 0.45);
            this.dtpBatasWaktu.Left = this.numQuota.Left + this.numQuota.Width + 20;
            this.dtpBatasWaktu.Width = innerW - this.numQuota.Width - 20;
            this.lblBatasWaktu.Left = this.dtpBatasWaktu.Left;
        }
    }
}
using System;
using System.Data;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    public partial class BukaSesiPOControl : UserControl
    {
        private readonly User _currentSeller;
        private readonly PreOrderController _poController;

        public BukaSesiPOControl(User seller)
        {
            InitializeComponent();
            _currentSeller = seller;
            _poController = new PreOrderController();

            this.Resize += (s, e) => AdjustLayout();
        }

        private void BukaSesiPOControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
            dtpBatasWaktu.MinDate = DateTime.Now;
            cbJenisPO.SelectedIndex = 0; // Default Biasa
            LoadMasterProduk();
        }

        private void btnSimpanSesi_Click(object sender, EventArgs e)
        {
            if (cbProduk.SelectedValue == null)
            {
                MessageBox.Show("Pilih dulu barang yang mau dijual ngab!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Udah yakin mau launching sesi '{txtNamaSesi.Text}'? Gaskeun?",
                "CollabBuy - Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                int idProduk = Convert.ToInt32(cbProduk.SelectedValue);
                int targetKuota = Convert.ToInt32(numQuota.Value);

                var result = _poController.GasLuncurkanPO(
                    _currentSeller.GetIdUser(),
                    txtNamaSesi.Text,
                    cbJenisPO.Text,
                    txtRekening.Text,
                    dtpBatasWaktu.Value,
                    idProduk,
                    targetKuota
                );

                if (result.sukses)
                {
                    MessageBox.Show(result.pesan, "CollabBuy - Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                    LoadMasterProduk(); // Refresh produk yg belum ada PO
                }
                else
                {
                    MessageBox.Show(result.pesan, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadMasterProduk()
        {
            try
            {
                DataTable dtProduk = _poController.GetProdukTersedia(_currentSeller.GetIdUser());
                cbProduk.DataSource = dtProduk;
                cbProduk.DisplayMember = "nama_produk";
                cbProduk.ValueMember = "id_produk";

                if (dtProduk.Rows.Count == 0)
                {
                    MessageBox.Show("Barang jualan lo udah masuk PO semua atau belum didaftarin nih. Input produk baru dulu gih di menu Manajemen Produk!",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSimpanSesi.Enabled = false;
                }
                else
                {
                    btnSimpanSesi.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat produk: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            txtNamaSesi.Clear();
            txtRekening.Clear();
            cbJenisPO.SelectedIndex = 0;
            numQuota.Value = 10;
            dtpBatasWaktu.Value = DateTime.Now.AddDays(1);
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            if (w < 400) w = 400;
            pnlForm.Width = w;

            int innerW = pnlForm.Width - 68;
            txtNamaSesi.Width = (int)(innerW * 0.58);
            cbJenisPO.Left = txtNamaSesi.Left + txtNamaSesi.Width + 20;
            cbJenisPO.Width = innerW - txtNamaSesi.Width - 20;
            lblJenis.Left = cbJenisPO.Left;
            cbProduk.Width = innerW;
            txtRekening.Width = innerW;
            btnSimpanSesi.Width = innerW;
            numQuota.Width = (int)(innerW * 0.45);
            dtpBatasWaktu.Left = numQuota.Left + numQuota.Width + 20;
            dtpBatasWaktu.Width = innerW - numQuota.Width - 20;
            lblBatasWaktu.Left = dtpBatasWaktu.Left;
        }
    }
}
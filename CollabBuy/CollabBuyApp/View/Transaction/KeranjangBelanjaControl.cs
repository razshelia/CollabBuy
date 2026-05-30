using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    public partial class KeranjangBelanjaControl : UserControl
    {
        private readonly User _currentUser;
        private readonly TransactionController _transactionController;
        private int _selectedIdProduk = 0;
        private string _selectedOldPenitip = "";

        public KeranjangBelanjaControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _transactionController = new TransactionController(_currentUser.GetIdUser());
            this.Dock = DockStyle.Fill;

            pnlTitipan.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dgvKeranjang.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlSummary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnKosongkan.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            this.Resize += (s, e) => AdjustLayout();
        }

        private void KeranjangBelanjaControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
            SetupDataGridView();
            LoadDataKeranjang();
        }

        private void SetupDataGridView()
        {
            dgvKeranjang.AutoGenerateColumns = false;
            dgvKeranjang.Columns.Clear();

            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdProduk", DataPropertyName = "IdProduk", Visible = false });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaItem", HeaderText = "Produk", DataPropertyName = "NamaItem", Width = 150 });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaPenitip", HeaderText = "Atas Nama", DataPropertyName = "NamaPenitip", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Catatan", HeaderText = "Notes", DataPropertyName = "Catatan", Width = 120 });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Harga", HeaderText = "Harga", DataPropertyName = "Harga", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kuantitas", HeaderText = "Qty", DataPropertyName = "Kuantitas", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            DataGridViewButtonColumn btnHapus = new DataGridViewButtonColumn
            {
                Name = "BtnHapus",
                HeaderText = "Aksi",
                Text = "❌",
                UseColumnTextForButtonValue = true,
                Width = 50,
                FlatStyle = FlatStyle.Flat
            };
            btnHapus.DefaultCellStyle.BackColor = Color.LightCoral;
            btnHapus.DefaultCellStyle.ForeColor = Color.White;
            dgvKeranjang.Columns.Add(btnHapus);
        }

        private void LoadDataKeranjang()
        {
            try
            {
                DataTable dtKeranjang = _transactionController.GetKeranjangDataTable();
                dgvKeranjang.DataSource = dtKeranjang;

                HitungTotalPembayaran(dtKeranjang);
                UpdateTombolState(dtKeranjang.Rows.Count > 0);

                dgvKeranjang.ClearSelection();
                ResetFormTitipan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat keranjang: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HitungTotalPembayaran(DataTable dt)
        {
            decimal total = 0;
            foreach (DataRow row in dt.Rows) total += Convert.ToDecimal(row["Subtotal"]);
            lblTotalHarga.Text = $"Rp {total:N0}";
        }

        private void UpdateTombolState(bool adaBarang)
        {
            btnCheckout.Enabled = adaBarang;
            btnCheckout.BackColor = adaBarang ? Color.FromArgb(36, 0, 70) : Color.Gray;
            btnKosongkan.Enabled = adaBarang;
            pnlTitipan.Enabled = false; // Kunci panel titipan sampai row diklik
        }

        private void dgvKeranjang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int idProduk = Convert.ToInt32(dgvKeranjang.Rows[e.RowIndex].Cells["IdProduk"].Value);
            string namaPenitip = dgvKeranjang.Rows[e.RowIndex].Cells["NamaPenitip"].Value.ToString();

            if (dgvKeranjang.Columns[e.ColumnIndex].Name == "BtnHapus")
            {
                _transactionController.HapusItemKeranjang(idProduk, namaPenitip);
                LoadDataKeranjang();
                return;
            }

            // Populasikan ke Form Edit Titipan
            _selectedIdProduk = idProduk;
            _selectedOldPenitip = namaPenitip;

            txtProduk.Text = dgvKeranjang.Rows[e.RowIndex].Cells["NamaItem"].Value.ToString();
            txtPenitip.Text = namaPenitip;
            txtCatatan.Text = dgvKeranjang.Rows[e.RowIndex].Cells["Catatan"].Value.ToString();
            numQty.Value = Convert.ToInt32(dgvKeranjang.Rows[e.RowIndex].Cells["Kuantitas"].Value);

            pnlTitipan.Enabled = true;
        }

        private void btnSimpanTitipan_Click(object sender, EventArgs e)
        {
            if (_selectedIdProduk == 0 || string.IsNullOrWhiteSpace(txtPenitip.Text)) return;

            _transactionController.UpdateTitipan(_selectedIdProduk, _selectedOldPenitip, txtPenitip.Text, (int)numQty.Value, txtCatatan.Text);
            LoadDataKeranjang();
        }

        private void btnTambahTitipan_Click(object sender, EventArgs e)
        {
            if (_selectedIdProduk == 0 || string.IsNullOrWhiteSpace(txtPenitip.Text)) return;

            _transactionController.TambahTitipanBaru(_selectedIdProduk, txtPenitip.Text, (int)numQty.Value, txtCatatan.Text);
            LoadDataKeranjang();
        }

        private void ResetFormTitipan()
        {
            _selectedIdProduk = 0;
            _selectedOldPenitip = "";
            txtProduk.Clear();
            txtPenitip.Clear();
            txtCatatan.Clear();
            numQty.Value = 1;
            pnlTitipan.Enabled = false;
        }

        private void btnKosongkan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin hapus semua jajanannya? 😭", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _transactionController.KosongkanKeranjang();
                LoadDataKeranjang();
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Udah yakin sama pesanan kamu? Langsung gaskeun bayar?", "Checkout Yuk!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var res = _transactionController.ProsesCheckout();
                if (res.sukses)
                {
                    MessageBox.Show(res.pesan + " 🎉", "Mantap!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _transactionController.KosongkanKeranjang();
                    LoadDataKeranjang();
                }
                else MessageBox.Show(res.pesan, "Gagal Checkout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int availableWidth = this.Width - (margin * 2);

            // Grid kiri: 58% dari lebar
            int gridWidth = (int)(availableWidth * 0.58);
            dgvKeranjang.Width = gridWidth;

            // Panel titipan kanan: sisanya
            int titipanLeft = margin + gridWidth + 24;
            pnlTitipan.Left = titipanLeft;
            pnlTitipan.Width = this.Width - titipanLeft - margin;

            // Panel summary full width
            pnlSummary.Width = availableWidth;

            // Tombol hapus semua nempel kanan
            btnKosongkan.Left = this.Width - margin - btnKosongkan.Width;

            // Tombol checkout nempel kanan dalam panel summary
            btnCheckout.Left = pnlSummary.Width - btnCheckout.Width - 20;
        }
    }
}
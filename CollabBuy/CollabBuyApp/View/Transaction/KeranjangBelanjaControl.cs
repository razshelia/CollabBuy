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

        public KeranjangBelanjaControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            // Inisialisasi Controller dengan ID Pembeli
            _transactionController = new TransactionController(_currentUser.GetIdUser());
        }

        private void KeranjangBelanjaControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataKeranjang();
        }

        private void SetupDataGridView()
        {
            dgvKeranjang.AutoGenerateColumns = false;
            dgvKeranjang.Columns.Clear();

            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdItem", DataPropertyName = "IdItem", Visible = false });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaItem", HeaderText = "Nama Jajanan / PO", DataPropertyName = "NamaItem", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Harga", HeaderText = "Harga Satuan", DataPropertyName = "Harga", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kuantitas", HeaderText = "Jumlah", DataPropertyName = "Kuantitas", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subtotal", HeaderText = "Subtotal (Rp)", DataPropertyName = "Subtotal", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Font = new Font("Segoe UI", 10F, FontStyle.Bold) } });

            DataGridViewButtonColumn btnHapus = new DataGridViewButtonColumn
            {
                Name = "BtnHapus",
                HeaderText = "Aksi",
                Text = "❌ Hapus",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            btnHapus.DefaultCellStyle.BackColor = Color.LightCoral;
            btnHapus.DefaultCellStyle.ForeColor = Color.White;
            btnHapus.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvKeranjang.Columns.Add(btnHapus);
        }

        private void LoadDataKeranjang()
        {
            try
            {
                // PANGGIL CONTROLLER (Data Asli dari CartManager)
                DataTable dtKeranjang = _transactionController.GetKeranjangDataTable();
                dgvKeranjang.DataSource = dtKeranjang;

                HitungTotalPembayaran();
                UpdateTombolState();

                dgvKeranjang.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat keranjang nih: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HitungTotalPembayaran()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvKeranjang.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
                }
            }
            lblTotalHarga.Text = $"Rp {total:N0}";
        }

        private void UpdateTombolState()
        {
            bool adaBarang = dgvKeranjang.Rows.Count > 0;
            btnCheckout.Enabled = adaBarang;
            btnCheckout.BackColor = adaBarang ? Color.FromArgb(36, 0, 70) : Color.Gray;

            btnKosongkan.Enabled = adaBarang;
        }

        private void dgvKeranjang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvKeranjang.Columns[e.ColumnIndex].Name == "BtnHapus")
            {
                int idItem = Convert.ToInt32(dgvKeranjang.Rows[e.RowIndex].Cells["IdItem"].Value);
                string namaItem = dgvKeranjang.Rows[e.RowIndex].Cells["NamaItem"].Value.ToString();

                DialogResult dr = MessageBox.Show($"Beneran mau hapus '{namaItem}' dari keranjang?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    // Hapus dari CartManager via Controller
                    _transactionController.HapusItemKeranjang(idItem);

                    // Refresh Grid
                    LoadDataKeranjang();
                }
            }
        }

        private void btnKosongkan_Click(object sender, EventArgs e)
        {
            if (dgvKeranjang.Rows.Count == 0) return;

            DialogResult dr = MessageBox.Show("Yakin mau hapus semua jajanannya? 😭", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                // Kosongkan CartManager via Controller
                _transactionController.KosongkanKeranjang();

                // Refresh Grid
                LoadDataKeranjang();
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Udah yakin sama pesanan kamu? Kalau di-oke, langsung masuk ke lapak penjual lho!", "Checkout Yuk!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    var (sukses, pesan) = _transactionController.ProsesCheckout();

                    if (sukses)
                    {
                        MessageBox.Show("Checkout Berhasil! 🎉\nCek tab 'Riwayat Pesanan' buat pantau statusnya ya.", "Mantap!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Kosongkan CartManager karena sudah dicheckout
                        _transactionController.KosongkanKeranjang();
                        LoadDataKeranjang();
                    }
                    else
                    {
                        MessageBox.Show(pesan, "Gagal Checkout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Waduh, ada error sistem: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
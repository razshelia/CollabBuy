using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Feedback
{
    public partial class BeriUlasanControl : UserControl
    {
        private readonly User _currentUser;
        private readonly ReviewController _reviewController;
        private readonly TransactionController _transactionController;

        private int _selectedTransactionId = 0;
        private int _selectedProductId = 0;

        public BeriUlasanControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _reviewController = new ReviewController();

            // PERBAIKAN: Gunakan konstruktor default — View ini hanya query transaksi selesai,
            // tidak mengelola keranjang belanja.
            _transactionController = new TransactionController();
        }

        private void BeriUlasanControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadPesananBelumDiulas();
            cbRating.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dgvPesananSelesai.AutoGenerateColumns = false;
            dgvPesananSelesai.Columns.Clear();

            dgvPesananSelesai.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdTransaction", DataPropertyName = "IdTransaction", Visible = false });
            dgvPesananSelesai.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdProduct", DataPropertyName = "IdProduct", Visible = false });

            dgvPesananSelesai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tanggal",
                HeaderText = "Tgl Selesai",
                DataPropertyName = "Tanggal",
                Width = 100
            });

            dgvPesananSelesai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NamaProduk",
                HeaderText = "Nama Produk",
                DataPropertyName = "NamaProduk",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvPesananSelesai.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Penjual",
                HeaderText = "Toko / Lapak",
                DataPropertyName = "NamaToko",
                Width = 120
            });

            DataGridViewButtonColumn btnPilih = new DataGridViewButtonColumn
            {
                Name = "BtnPilih",
                HeaderText = "Aksi",
                Text = "✍️ Ulas",
                UseColumnTextForButtonValue = true,
                Width = 80,
                FlatStyle = FlatStyle.Flat
            };
            btnPilih.DefaultCellStyle.BackColor = Color.FromArgb(200, 182, 255);
            btnPilih.DefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgvPesananSelesai.Columns.Add(btnPilih);
        }

        private void LoadPesananBelumDiulas()
        {
            try
            {
                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("IdTransaction", typeof(int));
                dtMock.Columns.Add("IdProduct", typeof(int));
                dtMock.Columns.Add("Tanggal", typeof(string));
                dtMock.Columns.Add("NamaProduk", typeof(string));
                dtMock.Columns.Add("NamaToko", typeof(string));

                dtMock.Rows.Add(101, 1, "20 May", "Danus Makaroni HMTI", "HMTI Mandiri");
                dtMock.Rows.Add(102, 2, "22 May", "Kemeja PDH Custom", "BEM Fasilkom");

                dgvPesananSelesai.DataSource = dtMock;

                if (dtMock.Rows.Count == 0) ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat pesanan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPesananSelesai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvPesananSelesai.Columns[e.ColumnIndex].Name == "BtnPilih")
            {
                _selectedTransactionId = Convert.ToInt32(dgvPesananSelesai.Rows[e.RowIndex].Cells["IdTransaction"].Value);
                _selectedProductId = Convert.ToInt32(dgvPesananSelesai.Rows[e.RowIndex].Cells["IdProduct"].Value);
                string namaProduk = dgvPesananSelesai.Rows[e.RowIndex].Cells["NamaProduk"].Value.ToString();
                string namaToko = dgvPesananSelesai.Rows[e.RowIndex].Cells["Penjual"].Value.ToString();

                pnlFormUlasan.Enabled = true;
                txtProdukTerpilih.Text = $"{namaProduk} (dari {namaToko})";
                cbRating.SelectedIndex = 0;
                txtKomentar.Clear();
                txtKomentar.Focus();
            }
        }

        private void btnKirimUlasan_Click(object sender, EventArgs e)
        {
            if (_selectedTransactionId == 0)
            {
                MessageBox.Show("Silakan pilih pesanan yang ingin diulas terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rating = 5 - cbRating.SelectedIndex;
            string komentar = txtKomentar.Text.Trim();

            DialogResult dr = MessageBox.Show($"Kirim ulasan dengan {rating} Bintang untuk produk ini?",
                                              "Konfirmasi Ulasan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                ProsesSimpanUlasan(rating, komentar);
            }
        }

        private void ProsesSimpanUlasan(int rating, string komentar)
        {
            try
            {
                // PERBAIKAN: Gunakan GetIdUser() (getter method) bukan properti .IdUser
                var (sukses, pesan) = _reviewController.KirimUlasan(_selectedProductId, _currentUser.GetIdUser(), rating, komentar);

                if (sukses)
                {
                    MessageBox.Show("Terima kasih! Ulasan Anda berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPesananBelumDiulas();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show(pesan, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            _selectedTransactionId = 0;
            _selectedProductId = 0;
            txtProdukTerpilih.Text = "Pilih pesanan di tabel kiri...";
            txtKomentar.Clear();
            pnlFormUlasan.Enabled = false;
        }
    }
}
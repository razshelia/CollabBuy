using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class KelolaKategoriControl : UserControl
    {
        private readonly AdminController _controller;
        private int _selectedId = 0; // Menyimpan ID kategori yang sedang diklik

        public KelolaKategoriControl()
        {
            InitializeComponent();
            _controller = new AdminController();
        }

        private void KelolaKategoriControl_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            dgvKategori.DataSource = _controller.GetKategori();
            if (dgvKategori.Columns.Count > 0)
            {
                dgvKategori.Columns["id_kategori"].HeaderText = "ID";
                dgvKategori.Columns["id_kategori"].Width = 50;
                dgvKategori.Columns["nama_kategori"].HeaderText = "Nama Kategori Barang";
                dgvKategori.Columns["nama_kategori"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            ResetForm();
        }

        private void ResetForm()
        {
            txtNama.Clear();
            _selectedId = 0;
            btnTambah.Enabled = true;
            btnUpdate.Enabled = false;
            btnHapus.Enabled = false;
        }

        private void dgvKategori_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKategori.Rows[e.RowIndex];
                _selectedId = Convert.ToInt32(row.Cells["id_kategori"].Value);
                txtNama.Text = row.Cells["nama_kategori"].Value.ToString();

                btnTambah.Enabled = false;
                btnUpdate.Enabled = true;
                btnHapus.Enabled = true;
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            var res = _controller.TambahKategori(txtNama.Text);
            if (res.sukses)
            {
                MessageBox.Show(res.pesan, "Suksesss!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGrid();
            }
            else MessageBox.Show(res.pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0) return;

            var res = _controller.EditKategori(_selectedId, txtNama.Text);
            if (res.sukses)
            {
                MessageBox.Show(res.pesan, "Suksesss!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGrid();
            }
            else MessageBox.Show(res.pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0) return;

            DialogResult confirm = MessageBox.Show(
                "Yakin nih mau ngehapus kategori ini? Ga bisa di-undo loh!",
                "Konfirmasi Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var res = _controller.HapusKategori(_selectedId);
                if (res.sukses)
                {
                    MessageBox.Show(res.pesan, "Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGrid();
                }
                else MessageBox.Show(res.pesan, "Ditolak Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
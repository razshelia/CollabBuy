using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models; // PENTING: Untuk memanggil class Category

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

            this.Resize += (s, e) => AdjustLayout();
        }

        private void KelolaKategoriControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
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
            try
            {
                // 1. MENGGUNAKAN MODEL OOP
                // Saat objek dibuat, Model otomatis memvalidasi kekosongan dan merapikan teks!
                if (txtNama.Text.Length < 4)
                {
                    MessageBox.Show("Nama kategori minimal harus 4 karakter!", "Validasi Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                }
                Category katBaru = new Category(txtNama.Text);

                // 2. Ambil teks yang sudah dirapikan (Title Case) oleh Model untuk dilempar ke Controller
                string namaBersih = katBaru.GetNamaKategori();
                 
                var res = _controller.TambahKategori(namaBersih);
                if (res.sukses)
                {
                    MessageBox.Show(res.pesan, "Suksesss!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGrid();
                }
                else MessageBox.Show(res.pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) // Akan menangkap InvalidOrderException dari Model jika TextBox kosong
            {
                MessageBox.Show(ex.Message, "Validasi Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0) return;

            try
            {
                // 1. MENGGUNAKAN MODEL OOP UNTUK UPDATE
                Category katUpdate = new Category(txtNama.Text);
                katUpdate.SetIdKategori(_selectedId); // Memanfaatkan validasi Setter ID juga

                // 2. Ambil teks dan ID yang sudah diproses oleh Model
                var res = _controller.EditKategori(katUpdate.GetIdKategori(), katUpdate.GetNamaKategori());
                if (res.sukses)
                {
                    MessageBox.Show(res.pesan, "Suksesss!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGrid();
                }
                else MessageBox.Show(res.pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validasi Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void AdjustLayout()
        {
            int margin = 38;
            int w = this.Width - (margin * 2);

            int formW = (int)(w * 0.38);
            if (formW < 300) formW = 300;
            pnlForm.Width = formW;

            // Sesuaikan lebar elemen di dalam pnlForm
            int innerW = formW - 48;
            txtNama.Width = innerW;
            btnTambah.Width = innerW;
            btnUpdate.Width = (innerW / 2) - 5;
            btnHapus.Width = (innerW / 2) - 5;
            btnHapus.Left = btnUpdate.Left + btnUpdate.Width + 10;
            btnReset.Width = innerW;

            int gridLeft = margin + formW + 24;
            dgvKategori.Left = gridLeft;
            dgvKategori.Width = this.Width - gridLeft - margin;
            dgvKategori.Height = this.Height - dgvKategori.Top - margin;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib ditambahkan untuk memanggil Repository

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class AdminCategoryControl : UserControl
    {
        private readonly CategoryService _categoryService;
        private List<Category> _daftarKategori;

        public AdminCategoryControl()
        {
            InitializeComponent();

            // TAHAP 4: INJEKSI MANUAL DI UI
            // Kita menyuntikkan CategoryRepository ke dalam CategoryService
            _categoryService = new CategoryService(new CategoryRepository());

            LoadData();
        }

        private void LoadData()
        {
            _daftarKategori = _categoryService.AmbilSemua();
            TampilkanKategori();
        }

        private void TampilkanKategori()
        {
            flowPanelKategori.Controls.Clear();

            if (_daftarKategori.Count == 0)
            {
                Label lblKosong = new Label()
                {
                    Text = "Belum ada kategori nih, bestie! 😴\nYuk tambah kategori baru!",
                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple Neo-Retro
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Dock = DockStyle.Fill
                };
                flowPanelKategori.Controls.Add(lblKosong);
                return;
            }

            foreach (var kat in _daftarKategori)
            {
                // Desain Card Gen-Z: Kotak tegas, warna pastel cerah, border solid
                Panel card = new Panel()
                {
                    Size = new Size(320, 60),
                    BackColor = Color.FromArgb(253, 255, 182), // Pastel Yellow
                    Margin = new Padding(10),
                    BorderStyle = BorderStyle.FixedSingle // Memberi kesan Neo-Retro Flat
                };

                Label lblNama = new Label()
                {
                    Text = kat.NamaKategori.ToUpper(), // Teks kapital agar lebih bold
                    Font = new Font("Segoe UI Black", 11F),
                    ForeColor = Color.FromArgb(36, 0, 70), // Dark Purple
                    Size = new Size(200, 25),
                    Location = new Point(15, 17)
                };

                // Tombol Edit yang lebih membaur dengan gaya retro
                Button btnEdit = new Button()
                {
                    Text = "✏️",
                    BackColor = Color.FromArgb(200, 182, 255), // Pastel Purple
                    ForeColor = Color.Black,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(40, 34),
                    Location = new Point(220, 12),
                    Cursor = Cursors.Hand
                };
                btnEdit.FlatAppearance.BorderSize = 1;
                btnEdit.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
                btnEdit.Click += (s, e) =>
                {
                    string namaBaru = InputDialog.Show("Edit nama kategori:", "Edit Kategori", kat.NamaKategori);
                    if (!string.IsNullOrWhiteSpace(namaBaru) && namaBaru != kat.NamaKategori)
                    {
                        if (_categoryService.Update(kat.IdKategori, namaBaru))
                            LoadData();
                    }
                };

                // Tombol Hapus 
                Button btnHapus = new Button()
                {
                    Text = "🗑",
                    BackColor = Color.FromArgb(255, 138, 138), // Soft Red Pastel
                    ForeColor = Color.Black,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(40, 34),
                    Location = new Point(265, 12),
                    Cursor = Cursors.Hand
                };
                btnHapus.FlatAppearance.BorderSize = 1;
                btnHapus.FlatAppearance.BorderColor = Color.FromArgb(36, 0, 70);
                btnHapus.Click += (s, e) =>
                {
                    if (_categoryService.Hapus(kat.IdKategori))
                        LoadData();
                };

                card.Controls.Add(lblNama);
                card.Controls.Add(btnEdit);
                card.Controls.Add(btnHapus);
                flowPanelKategori.Controls.Add(card);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            string namaBaru = InputDialog.Show("Masukkan nama kategori baru:", "Tambah Kategori", "");
            if (!string.IsNullOrWhiteSpace(namaBaru))
            {
                if (_categoryService.Tambah(namaBaru))
                    LoadData();
            }
        }
    }
}
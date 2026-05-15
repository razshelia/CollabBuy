using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class AdminCategoryControl : UserControl
    {
        private CategoryService _categoryService;
        private List<Category> _daftarKategori;

        public AdminCategoryControl()
        {
            InitializeComponent();
            _categoryService = new CategoryService();
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
                    Text = "Belum ada kategori nih, bestie! 😴",
                    Font = new Font("Segoe UI", 14F),
                    ForeColor = Color.FromArgb(45, 27, 79),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                flowPanelKategori.Controls.Add(lblKosong);
                return;
            }

            foreach (var kat in _daftarKategori)
            {
                Panel card = new Panel()
                {
                    Size = new Size(300, 50),
                    BackColor = Color.White,
                    Margin = new Padding(5),
                    Padding = new Padding(10)
                };

                Label lblNama = new Label()
                {
                    Text = kat.NamaKategori,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(45, 27, 79),
                    Size = new Size(180, 25),
                    Location = new Point(10, 12)
                };

                Button btnEdit = new Button()
                {
                    Text = "✏️",
                    BackColor = Color.FromArgb(167, 139, 250),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(35, 28),
                    Location = new Point(200, 10)
                };
                btnEdit.Click += (s, e) =>
                {
                    string namaBaru = InputDialog.Show("Edit nama kategori:", "Edit Kategori", kat.NamaKategori);
                    if (!string.IsNullOrWhiteSpace(namaBaru) && namaBaru != kat.NamaKategori)
                    {
                        if (_categoryService.Update(kat.IdKategori, namaBaru))
                            LoadData();
                    }
                };

                Button btnHapus = new Button()
                {
                    Text = "🗑",
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(35, 28),
                    Location = new Point(240, 10)
                };
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
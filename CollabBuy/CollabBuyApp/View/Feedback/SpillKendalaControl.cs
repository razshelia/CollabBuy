using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Feedback
{
    public partial class SpillKendalaControl : UserControl
    {
        private readonly User _currentUser;
        private readonly ComplaintController _controller;

        public SpillKendalaControl(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _controller = new ComplaintController();

            this.Resize += (s, e) => AdjustLayout();
        }

        private void SpillKendalaControl_Load(object sender, EventArgs e)
        {
            AdjustLayout();
            LoadRiwayat();
        }

        private void btnAduan_Click(object sender, EventArgs e)
        {
            var result = _controller.GasSpillKendala(_currentUser.GetIdUser(), txtSubjek.Text, txtDeskripsi.Text);

            if (result.sukses)
            {
                // Menyesuaikan logika fitur aduan agar pengguna mendapat feedback notifikasi sesuai request
                MessageBox.Show("Laporan udah masuk ke sistem! Beberapa saat lagi akan dikabari sama Mimin ya bestie! 💌",
                                "Aduan Terkirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubjek.Clear();
                txtDeskripsi.Clear();
                LoadRiwayat();
            }
            else
            {
                MessageBox.Show(result.pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadRiwayat()
        {
            dgvRiwayat.DataSource = _controller.GetRiwayatSpill(_currentUser.GetIdUser());
            if (dgvRiwayat.Columns.Count > 0)
            {
                dgvRiwayat.Columns["subjek"].HeaderText = "Subjek Masalah";
                dgvRiwayat.Columns["tanggal"].HeaderText = "Waktu Spill";
                dgvRiwayat.Columns["is_selesai"].HeaderText = "Status Beres?";
            }
        }

        private void AdjustLayout()
        {
            int margin = 38;
            int w = this.Width - (margin * 2);

            int formW = (int)(w * 0.42);
            if (formW < 300) formW = 300;
            pnlForm.Width = formW;
            pnlForm.Height = this.Height - pnlForm.Top - margin; // ← TAMBAH INI

            int innerW = formW - 48;
            txtSubjek.Width = innerW;
            txtDeskripsi.Width = innerW;
            txtDeskripsi.Height = pnlForm.Height - btnAduan.Height - 200; // ← TAMBAH INI
            btnAduan.Top = txtDeskripsi.Top + txtDeskripsi.Height + 15; // ← TAMBAH INI
            btnAduan.Width = innerW;

            int riwayatLeft = margin + formW + 24;
            lblRiwayat.Left = riwayatLeft;
            dgvRiwayat.Left = riwayatLeft;
            dgvRiwayat.Width = this.Width - riwayatLeft - margin;
            dgvRiwayat.Height = this.Height - dgvRiwayat.Top - margin; // ← TAMBAH INI
        }
    }
}
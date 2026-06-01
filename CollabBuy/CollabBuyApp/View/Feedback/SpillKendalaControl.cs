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
                // Notifikasi aduan sesuai request
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
            DataTable dtRaw = _controller.GetRiwayatSpill(_currentUser.GetIdUser());

            // Buat DataTable khusus UI agar lebih rapi
            DataTable dtUI = new DataTable();
            dtUI.Columns.Add("subjek", typeof(string));
            dtUI.Columns.Add("deskripsi", typeof(string));
            dtUI.Columns.Add("tanggal", typeof(string));
            dtUI.Columns.Add("status", typeof(string));
            dtUI.Columns.Add("balasan", typeof(string));

            foreach (DataRow row in dtRaw.Rows)
            {
                string subjek = row["subjek"].ToString();
                string deskripsiRaw = row["deskripsi"].ToString();
                bool isSelesai = false;
                if (!row.IsNull("is_selesai")) isSelesai = Convert.ToBoolean(row["is_selesai"]);

                string balasan = row["balasan"]?.ToString();

                // --- OOP BEST PRACTICE CALL ---
                // Buat objek untuk memanfaatkan behavior-nya
                Complaint aduanObj = new Complaint(_currentUser.GetIdUser(), subjek, deskripsiRaw);
                aduanObj.SetStatus(isSelesai ? "Selesai" : "Menunggu");

                if (!string.IsNullOrWhiteSpace(balasan))
                {
                    aduanObj.SetTanggapanAdmin(balasan);
                }

                // Gunakan Method Behavior Model!
                string statusKece = aduanObj.DapatkanStatusUI();
                string previewTeks = aduanObj.DapatkanPreviewDeskripsi(30);
                string previewBalasan = string.IsNullOrWhiteSpace(aduanObj.GetTanggapanAdmin()) ? "Belum direspon" : aduanObj.GetTanggapanAdmin();

                // Batasi balasan admin jika terlalu panjang di grid
                if (previewBalasan.Length > 35 && previewBalasan != "Belum direspon")
                {
                    previewBalasan = previewBalasan.Substring(0, 35) + "...";
                }
                // ------------------------------

                dtUI.Rows.Add(
                    subjek,
                    previewTeks,
                    Convert.ToDateTime(row["tanggal"]).ToString("dd MMM yyyy, HH:mm"),
                    statusKece,
                    previewBalasan
                );
            }

            dgvRiwayat.DataSource = dtUI;

            if (dgvRiwayat.Columns.Count > 0)
            {
                dgvRiwayat.Columns["subjek"].HeaderText = "Subjek Masalah";
                dgvRiwayat.Columns["deskripsi"].HeaderText = "Detail Curhatan";
                dgvRiwayat.Columns["tanggal"].HeaderText = "Waktu Spill";
                dgvRiwayat.Columns["status"].HeaderText = "Status";
                dgvRiwayat.Columns["balasan"].HeaderText = "Balasan Mimin";
            }
        }

        private void AdjustLayout()
        {
            int margin = 38;
            int w = this.Width - (margin * 2);

            int formW = (int)(w * 0.42);
            if (formW < 300) formW = 300;
            pnlForm.Width = formW;
            pnlForm.Height = this.Height - pnlForm.Top - margin;

            int innerW = formW - 48;
            txtSubjek.Width = innerW;
            txtDeskripsi.Width = innerW;
            txtDeskripsi.Height = pnlForm.Height - btnAduan.Height - 200;
            btnAduan.Top = txtDeskripsi.Top + txtDeskripsi.Height + 15;
            btnAduan.Width = innerW;

            int riwayatLeft = margin + formW + 24;
            lblRiwayat.Left = riwayatLeft;
            dgvRiwayat.Left = riwayatLeft;
            dgvRiwayat.Width = this.Width - riwayatLeft - margin;
            dgvRiwayat.Height = this.Height - dgvRiwayat.Top - margin;
        }
    }
}
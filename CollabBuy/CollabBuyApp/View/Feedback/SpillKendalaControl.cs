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
        private readonly Models.User _currentUser;
        private readonly ComplaintController _controller;

        public SpillKendalaControl(Models.User user)
        {
            this.InitializeComponent();

            this._currentUser = user;
            this._controller = new ComplaintController();

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void SpillKendalaControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.LoadRiwayat();
        }

        private void btnAduan_Click(object sender, EventArgs e)
        {
            var (sukses, pesan) = this._controller.GasSpillKendala(this._currentUser.GetIdUser(), this.txtSubjek.Text, this.txtDeskripsi.Text);

            if (sukses)
            {
                MessageBox.Show("Laporan udah masuk ke sistem! Beberapa saat lagi akan dikabari sama Mimin ya bestie! 💌",
                                "Aduan Terkirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtSubjek.Clear();
                this.txtDeskripsi.Clear();
                this.LoadRiwayat();
            }
            else
            {
                MessageBox.Show(pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadRiwayat()
        {
            DataTable dtRaw = this._controller.GetRiwayatSpill(this._currentUser.GetIdUser());

            DataTable dtUI = new DataTable();
            dtUI.Columns.Add("subjek", typeof(string));
            dtUI.Columns.Add("deskripsi", typeof(string));
            dtUI.Columns.Add("tanggal", typeof(string));
            dtUI.Columns.Add("status", typeof(string));
            dtUI.Columns.Add("balasan", typeof(string));

            if (dtRaw != null)
            {
                foreach (DataRow row in dtRaw.Rows)
                {
                    string subjek = row["subjek"].ToString();
                    string deskripsiRaw = row["deskripsi"].ToString();

                    bool isSelesai;
                    if (!row.IsNull("is_selesai"))
                    {
                        isSelesai = Convert.ToBoolean(row["is_selesai"]);
                    }
                    else
                    {
                        isSelesai = false;
                    }

                    string balasan;
                    if (row["balasan"] != DBNull.Value)
                    {
                        balasan = row["balasan"].ToString();
                    }
                    else
                    {
                        balasan = "";
                    }

                    // =======================================================
                    // OOP BEST PRACTICE: PEMANFAATAN BEHAVIOR MODEL
                    // =======================================================
                    Complaint aduanObj = new Complaint(this._currentUser.GetIdUser(), subjek, deskripsiRaw);

                    if (isSelesai)
                    {
                        aduanObj.SetStatus("Selesai");
                    }
                    else
                    {
                        aduanObj.SetStatus("Menunggu");
                    }

                    if (!string.IsNullOrWhiteSpace(balasan))
                    {
                        aduanObj.SetTanggapanAdmin(balasan);
                    }
                    else
                    {
                        bool skipTanggapan = true; // Assignment nyata menghindari else kosong
                    }

                    string statusKece = aduanObj.DapatkanStatusUI();
                    string previewTeks = aduanObj.DapatkanPreviewDeskripsi(30);

                    string previewBalasan;
                    if (string.IsNullOrWhiteSpace(aduanObj.GetTanggapanAdmin()))
                    {
                        previewBalasan = "Belum direspon";
                    }
                    else
                    {
                        previewBalasan = aduanObj.GetTanggapanAdmin();
                    }

                    if (previewBalasan.Length > 35 && previewBalasan != "Belum direspon")
                    {
                        previewBalasan = previewBalasan.Substring(0, 35) + "...";
                    }
                    else
                    {
                        bool skipLimit = true;
                    }

                    string tanggalFormat;
                    if (row["tanggal"] != DBNull.Value)
                    {
                        tanggalFormat = Convert.ToDateTime(row["tanggal"]).ToString("dd MMM yyyy, HH:mm");
                    }
                    else
                    {
                        tanggalFormat = "-";
                    }

                    dtUI.Rows.Add(
                        subjek,
                        previewTeks,
                        tanggalFormat,
                        statusKece,
                        previewBalasan
                    );
                }
            }
            else
            {
                bool tableKosong = true;
            }

            this.dgvRiwayat.DataSource = dtUI;

            if (this.dgvRiwayat.Columns.Count > 0)
            {
                this.dgvRiwayat.Columns["subjek"].HeaderText = "Subjek Masalah";
                this.dgvRiwayat.Columns["deskripsi"].HeaderText = "Detail Curhatan";
                this.dgvRiwayat.Columns["tanggal"].HeaderText = "Waktu Spill";
                this.dgvRiwayat.Columns["status"].HeaderText = "Status";
                this.dgvRiwayat.Columns["balasan"].HeaderText = "Balasan Mimin";
            }
            else
            {
                bool gridAwal = true;
            }
        }

        private void AdjustLayout()
        {
            int margin = 38;
            int w = this.Width - (margin * 2);

            int formW = (int)(w * 0.42);

            if (formW < 300)
            {
                formW = 300;
            }
            else
            {
                bool ukuranAman = true;
            }

            this.pnlForm.Width = formW;
            this.pnlForm.Height = this.Height - this.pnlForm.Top - margin;

            int innerW = formW - 48;
            this.txtSubjek.Width = innerW;
            this.txtDeskripsi.Width = innerW;
            this.txtDeskripsi.Height = this.pnlForm.Height - this.btnAduan.Height - 200;

            this.btnAduan.Top = this.txtDeskripsi.Top + this.txtDeskripsi.Height + 15;
            this.btnAduan.Width = innerW;

            int riwayatLeft = margin + formW + 24;
            this.lblRiwayat.Left = riwayatLeft;
            this.dgvRiwayat.Left = riwayatLeft;
            this.dgvRiwayat.Width = this.Width - riwayatLeft - margin;
            this.dgvRiwayat.Height = this.Height - this.dgvRiwayat.Top - margin;
        }
    }
}
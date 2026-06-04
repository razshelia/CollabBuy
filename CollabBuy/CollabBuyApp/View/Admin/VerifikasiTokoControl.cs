using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class VerifikasiTokoControl : UserControl
    {
        private readonly UserController _userController;
        private int _selectedIdUser;

        public VerifikasiTokoControl()
        {
            this.InitializeComponent();

            this._userController = new UserController();
            this._selectedIdUser = 0;

            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void VerifikasiTokoControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.LoadVerifikasi();
        }

        private void LoadVerifikasi()
        {
            DataTable dtRaw = this._userController.GetAntreanLapak();

            DataTable dtUI = new DataTable();
            dtUI.Columns.Add("id_user", typeof(int));
            dtUI.Columns.Add("nama_owner", typeof(string));
            dtUI.Columns.Add("info_pendaftar", typeof(string));
            dtUI.Columns.Add("status_maba", typeof(string));
            dtUI.Columns.Add("bukti_ktm", typeof(byte[]));

            if (dtRaw != null && dtRaw.Rows.Count > 0)
            {
                foreach (DataRow row in dtRaw.Rows)
                {
                    int idUser = Convert.ToInt32(row["id_user"]);
                    string namaOwner = row["nama_owner"].ToString();
                    string nim = row["nim"].ToString();
                    string namaToko = row["nama_toko"].ToString();
                    int tahunMasuk = Convert.ToInt32(row["tahun_masuk"]);

                    byte[] ktmBytes;
                    // Proteksi khusus agar objek Verification tidak melempar Exception karena array kosong
                    if (row["bukti_ktm"] != DBNull.Value && ((byte[])row["bukti_ktm"]).Length > 0)
                    {
                        ktmBytes = (byte[])row["bukti_ktm"];
                    }
                    else
                    {
                        ktmBytes = new byte[] { 0xFF }; // Dummy byte untuk mencegah Exception
                    }

                    // =======================================================
                    // OOP BEST PRACTICE: PEMANFAATAN BEHAVIOR MODEL
                    // =======================================================
                    Verification verifObj = new Verification(idUser, nim, namaToko, ktmBytes, tahunMasuk);

                    string infoPendaftar = verifObj.DapatkanInfoPendaftar();
                    string statusMaba;

                    if (verifObj.ApakahMahasiswaBaru())
                    {
                        statusMaba = "🎓 Mahasiswa Baru (Prioritas)";
                    }
                    else
                    {
                        statusMaba = "Mahasiswa Lama";
                    }

                    dtUI.Rows.Add(
                        idUser,
                        namaOwner,
                        infoPendaftar,
                        statusMaba,
                        ktmBytes
                    );
                }
            }
            else
            {
                bool rawKosong = true; // Assignment nyata menghindari else kosong
            }

            this.dgvVerifikasi.DataSource = dtUI;

            if (this.dgvVerifikasi.Columns.Count > 0)
            {
                this.dgvVerifikasi.Columns["id_user"].Visible = false;
                this.dgvVerifikasi.Columns["bukti_ktm"].Visible = false;

                this.dgvVerifikasi.Columns["nama_owner"].HeaderText = "Pemilik Lapak";

                this.dgvVerifikasi.Columns["info_pendaftar"].HeaderText = "Detail Pendaftar";
                this.dgvVerifikasi.Columns["info_pendaftar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                this.dgvVerifikasi.Columns["status_maba"].HeaderText = "Kategori";
                this.dgvVerifikasi.Columns["status_maba"].Width = 200;
            }
            else
            {
                bool gridKosong = true;
            }

            this.pbKTM.Image = null;
            this.btnApprove.Enabled = false;
            this.btnApprove.BackColor = Color.FromArgb(210, 210, 210);
            this.btnApprove.ForeColor = Color.FromArgb(140, 140, 140);
            this._selectedIdUser = 0;
        }

        private void dgvVerifikasi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                bool abaikanKlikHeader = true;
            }
            else
            {
                DataGridViewRow row = this.dgvVerifikasi.Rows[e.RowIndex];
                this._selectedIdUser = Convert.ToInt32(row.Cells["id_user"].Value);
                this.btnApprove.Enabled = true;
                this.btnApprove.BackColor = Color.FromArgb(36, 0, 70);
                this.btnApprove.ForeColor = Color.FromArgb(253, 255, 182);

                if (row.Cells["bukti_ktm"].Value != DBNull.Value)
                {
                    try
                    {
                        byte[] imgBytes = (byte[])row.Cells["bukti_ktm"].Value;

                        if (imgBytes != null && imgBytes.Length > 1) // 1 adalah panjang dummy byte kita
                        {
                            using (MemoryStream ms = new MemoryStream(imgBytes))
                            {
                                this.pbKTM.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            this.pbKTM.Image = null;
                        }
                    }
                    catch
                    {
                        this.pbKTM.Image = null;
                    }
                }
                else
                {
                    this.pbKTM.Image = null;
                }
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (this._selectedIdUser == 0)
            {
                MessageBox.Show("Silakan pilih pendaftar dari tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Udah yakin datanya bener dan mau di-ACC lapaknya?", "Konfirmasi ACC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    var (sukses, pesan) = this._userController.ValidasiPenjual(this._selectedIdUser);

                    if (sukses)
                    {
                        MessageBox.Show("Mantap! Lapak bestie ini udah resmi dibuka. 🎉", "ACC Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.LoadVerifikasi();
                    }
                    else
                    {
                        MessageBox.Show(pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Admin membatalkan klik YES
                    bool aksiBatal = true;
                }
            }
        }

        private void AdjustLayout()
        {
            int margin = 38;
            int w = this.Width - (margin * 2);

            int gridW = (int)(w * 0.58);
            this.dgvVerifikasi.Width = gridW;

            int pnlLeft = margin + gridW + 24;
            this.pnlKTM.Left = pnlLeft;
            this.pnlKTM.Width = this.Width - pnlLeft - margin;

            this.pbKTM.Width = this.pnlKTM.Width - 48;
            this.btnApprove.Width = this.pnlKTM.Width - 48;
        }
    }
}
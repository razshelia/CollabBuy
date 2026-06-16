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
        private RichTextBox _rtbInfoPendaftar;
        private DataTable _dtVerifikasiCache;

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

                    // SESUDAH — cukup 1 objek:
                    Penjual penjualInfoTemp = new Penjual(namaOwner, "tmp_" + idUser, "placeholder123");
                    penjualInfoTemp.Nim = nim;
                    penjualInfoTemp.NamaToko = namaToko;
                    penjualInfoTemp.TahunMasuk = tahunMasuk;

                    bool mahasiswaAktif = penjualInfoTemp.ApakahMahasiswaAktif();

                    string statusMaba;
                    if (verifObj.ApakahMahasiswaBaru())
                    {
                        statusMaba = "🎓 Mahasiswa Baru (Prioritas)";
                    }
                    else if (!mahasiswaAktif)
                    {
                        statusMaba = "⚠️ Masa Studi > 7 Tahun";  // Info tambahan untuk Admin
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

            this._dtVerifikasiCache = dtUI; 
            this.dgvVerifikasi.DataSource = this._dtVerifikasiCache;

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

            this.pbKTM.Image = null;
            this.btnApprove.Enabled = true;
            this.btnApprove.BackColor = Color.FromArgb(36, 0, 70);
            this.btnApprove.ForeColor = Color.FromArgb(253, 255, 182);

            this.btnTolak.Enabled = true;
            this.btnTolak.BackColor = Color.FromArgb(200, 50, 50);
            this.btnTolak.ForeColor = Color.White;
            this._selectedIdUser = 0;
        }

        private void TerapkanFilterVerifikasi()
        {
            if (this._dtVerifikasiCache == null) return;
            string kata = this.txtCariVerifikasi.Text.Trim();
            DataView dv = this._dtVerifikasiCache.DefaultView;
            dv.RowFilter = string.IsNullOrEmpty(kata) ? ""
                : $"nama_owner LIKE '%{kata}%'";
            this.dgvVerifikasi.DataSource = dv;
            this.dgvVerifikasi.ClearSelection();
        }

        private void txtCariVerifikasi_TextChanged(object sender, EventArgs e)
        {
            this.TerapkanFilterVerifikasi();
        }

        private void PastikanInfoPendaftarAda()
        {
            if (_rtbInfoPendaftar != null) return;

            // Kecilkan pbKTM untuk beri ruang teks
            this.pbKTM.Size = new Size(300, 200);

            var lblInfo = new Label
            {
                Text = "📋 Info Pendaftar:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(36, 0, 70),
                Location = new Point(24, 260),
                Size = new Size(300, 20),
                AutoSize = false
            };

            _rtbInfoPendaftar = new RichTextBox
            {
                Location = new Point(24, 284),
                Size = new Size(300, 120),
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = RichTextBoxScrollBars.Vertical,
                Text = "← Klik baris pendaftar untuk lihat info"
            };

            // Geser btnApprove ke bawah rtb
            this.btnApprove.Top = 418;

            this.pnlKTM.Controls.Add(lblInfo);
            this.pnlKTM.Controls.Add(_rtbInfoPendaftar);
        }
        private void dgvVerifikasi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            else
            {
                DataGridViewRow row = this.dgvVerifikasi.Rows[e.RowIndex];
                this._selectedIdUser = Convert.ToInt32(row.Cells["id_user"].Value);
                this.btnApprove.Enabled = true;
                this.btnApprove.BackColor = Color.FromArgb(36, 0, 70);
                this.btnApprove.ForeColor = Color.FromArgb(253, 255, 182);
                this.PastikanInfoPendaftarAda();
                string infoPendaftar = row.Cells["info_pendaftar"].Value?.ToString() ?? "-";
                string namaOwner = row.Cells["nama_owner"].Value?.ToString() ?? "";
                _rtbInfoPendaftar.Text = string.IsNullOrWhiteSpace(infoPendaftar) ? "-" : infoPendaftar;

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
            }
        }
        private void btnTolak_Click(object sender, EventArgs e)
        {
            if (this._selectedIdUser == 0)
            {
                MessageBox.Show("Silakan pilih pendaftar dari tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string alasan = Microsoft.VisualBasic.Interaction.InputBox(
                "Masukkan alasan penolakan (wajib diisi):",
                "Tolak Pengajuan Lapak", "");

            if (string.IsNullOrWhiteSpace(alasan))
            {
                MessageBox.Show("Penolakan dibatalkan karena alasan tidak diisi.", "Batal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show(
                $"Yakin mau tolak pengajuan ini?\nAlasan: {alasan}",
                "Konfirmasi Tolak", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                var (sukses, pesan) = this._userController.TolakPenjual(this._selectedIdUser, alasan);
                if (sukses)
                {
                    MessageBox.Show("Pengajuan lapak berhasil ditolak.", "Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.LoadVerifikasi();
                }
                else
                {
                    MessageBox.Show(pesan, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
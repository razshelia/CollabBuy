using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.PreOrder
{
    public partial class KelolaSesiPOControl : UserControl
    {
        private readonly Models.User _currentUser;
        private readonly PreOrderController _poController;
        private int _selectedIdPo = 0;
        private ToolTip _gridTooltip = new ToolTip();

        public KelolaSesiPOControl(Models.User currentUser)
        {
            this.InitializeComponent();
            this._currentUser = currentUser;
            this._poController = new PreOrderController();
            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void KelolaSesiPOControl_Load(object sender, EventArgs e)
        {
            this.SetupDataGridView();
            this.LoadDataPO();
            this.SetFormEnabled(false);
            this.BeginInvoke(new Action(() => this.AdjustLayout()));
        }

        private void SetupDataGridView()
        {
            this.dgvPO.AutoGenerateColumns = false;
            this.dgvPO.Columns.Clear();

            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdPo", DataPropertyName = "id_po", Visible = false });
            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "BatasRaw", DataPropertyName = "batas_waktu_raw", Visible = false });
            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "Judul", HeaderText = "Nama Sesi", DataPropertyName = "judul_po", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "Jenis", HeaderText = "Tipe", DataPropertyName = "jenis_po", Width = 130 });
            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "Batas", HeaderText = "Tutup Pada", DataPropertyName = "batas_waktu_format", Width = 175 });
            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", DataPropertyName = "status_label", Width = 100 });
            _gridTooltip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400, ShowAlways = true };
            this.dgvPO.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (this.dgvPO.Columns[e.ColumnIndex].Name != "Judul") return;
                string teks = this.dgvPO.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
                if (teks.Length > 30)
                    _gridTooltip.Show(teks, this.dgvPO,
                        this.dgvPO.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location, 5000);
            };
            this.dgvPO.CellMouseLeave += (s, e) => _gridTooltip.Hide(this.dgvPO);
        }

        private void LoadDataPO()
        {
            try
            {
                DataTable dtRaw = this._poController.GetPOByPenjual(this._currentUser.IdUser);

                DataTable dtUI = new DataTable();
                dtUI.Columns.Add("id_po", typeof(int));
                dtUI.Columns.Add("batas_waktu_raw", typeof(string)); // raw ISO untuk TryParse
                dtUI.Columns.Add("judul_po", typeof(string));
                dtUI.Columns.Add("jenis_po", typeof(string));
                dtUI.Columns.Add("batas_waktu_format", typeof(string));
                dtUI.Columns.Add("status_label", typeof(string));

                foreach (DataRow row in dtRaw.Rows)
                {
                    bool isAktif = row["is_aktif"] != DBNull.Value && Convert.ToBoolean(row["is_aktif"]);

                    DateTime batas = row["batas_waktu"] != DBNull.Value
                        ? Convert.ToDateTime(row["batas_waktu"])
                        : DateTime.Now.AddDays(1);

                    string batasRaw = batas.ToString("yyyy-MM-dd HH:mm:ss");

                    string batasFormat = batas < DateTime.Now
                        ? "⛔ " + batas.ToString("dd MMM yyyy")
                        : "📅 " + batas.ToString("dd MMM yyyy HH:mm");

                    string statusLabel = isAktif ? "🟢 Aktif" : "🔴 Tutup";

                    dtUI.Rows.Add(
                        Convert.ToInt32(row["id_po"]),
                        batasRaw,
                        row["judul_po"].ToString(),
                        row["jenis_po"].ToString(),
                        batasFormat,
                        statusLabel
                    );
                }

                this.dgvPO.DataSource = dtUI;
                this.dgvPO.ClearSelection();
                this.ResetForm();

                if (dtUI.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Kamu belum punya sesi PO. Buka sesi dulu lewat menu 'Buka Sesi PO'!",
                        "Belum Ada Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat data PO: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = this.dgvPO.Rows[e.RowIndex];
            this._selectedIdPo = Convert.ToInt32(row.Cells["IdPo"].Value);

            this.txtJudul.Text = row.Cells["Judul"].Value?.ToString() ?? "";
            this.txtRekening.Text = "";

            // Pilih jenis dari kolom raw (bukan format emoji)
            string jenis = row.Cells["Jenis"].Value?.ToString() ?? "Biasa";
            int idx = this.cbJenis.FindStringExact(jenis);
            this.cbJenis.SelectedIndex = idx >= 0 ? idx : 0;

            // Baca tanggal dari kolom raw ISO, bukan kolom format emoji
            string rawBatas = row.Cells["BatasRaw"].Value?.ToString() ?? "";
            if (DateTime.TryParse(rawBatas, out DateTime batas))
                this.dtpBatas.Value = batas > DateTime.Now ? batas : DateTime.Now.AddDays(1);
            else
                this.dtpBatas.Value = DateTime.Now.AddDays(1);

            this.SetFormEnabled(true);
        }

        private void btnSimpanEdit_Click(object sender, EventArgs e)
        {
            if (this._selectedIdPo == 0)
            {
                MessageBox.Show("Pilih dulu sesi PO yang mau diedit!", "Oops",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.txtRekening.Text))
            {
                MessageBox.Show("Info rekening wajib diisi ulang untuk konfirmasi update!", "Validasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtRekening.Focus();
                return;
            }

            DialogResult dr = MessageBox.Show(
                $"Yakin update sesi '{this.txtJudul.Text}'?",
                "Konfirmasi Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                var (sukses, pesan) = this._poController.EditSesiPO(
                    this._selectedIdPo,
                    this.txtJudul.Text.Trim(),
                    this.cbJenis.SelectedItem.ToString(),
                    this.txtRekening.Text.Trim(),
                    this.dtpBatas.Value
                );

                if (sukses)
                {
                    MessageBox.Show(pesan, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.LoadDataPO();
                }
                else
                {
                    MessageBox.Show(pesan, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapusPO_Click(object sender, EventArgs e)
        {
            if (this._selectedIdPo == 0)
            {
                MessageBox.Show("Pilih dulu sesi PO yang mau dihapus!", "Oops",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                $"Yakin mau tutup & hapus sesi '{this.txtJudul.Text}'?\n\nData tidak akan muncul lagi di katalog (soft delete, data aman di DB).",
                "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                var (sukses, pesan) = this._poController.TutupSesiPO(this._selectedIdPo);
                if (sukses)
                {
                    MessageBox.Show(pesan, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.LoadDataPO();
                }
                else
                {
                    MessageBox.Show(pesan, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadDataPO();
        }

        private void ResetForm()
        {
            this._selectedIdPo = 0;
            this.txtJudul.Clear();
            this.txtRekening.Clear();
            this.dtpBatas.Value = DateTime.Now.AddDays(1);
            if (this.cbJenis.Items.Count > 0) this.cbJenis.SelectedIndex = 0;
            this.SetFormEnabled(false);
        }

        private void SetFormEnabled(bool enabled)
        {
            this.txtJudul.Enabled = enabled;
            this.cbJenis.Enabled = enabled;
            this.dtpBatas.Enabled = enabled;
            this.txtRekening.Enabled = enabled;

            this.btnSimpanEdit.Enabled = enabled;
            this.btnSimpanEdit.BackColor = enabled
                ? Color.FromArgb(36, 0, 70) : Color.FromArgb(210, 210, 210);
            this.btnSimpanEdit.ForeColor = enabled
                ? Color.White : Color.FromArgb(140, 140, 140);

            this.btnHapusPO.Enabled = enabled;
            this.btnHapusPO.BackColor = enabled
                ? Color.FromArgb(220, 53, 69) : Color.FromArgb(210, 210, 210);
            this.btnHapusPO.ForeColor = enabled
                ? Color.White : Color.FromArgb(140, 140, 140);
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            if (w < 400) return;

            // Tabel PO: lebar penuh, tinggi pas untuk beberapa baris
            this.dgvPO.Left = margin;
            this.dgvPO.Width = w;
            this.dgvPO.Height = 200;

            // Panel edit: tepat di bawah tabel + gap 15px
            this.pnlEdit.Left = margin;
            this.pnlEdit.Width = w;
            this.pnlEdit.Top = this.dgvPO.Top + this.dgvPO.Height + 15;

            // Kontrol dalam pnlEdit: sesuaikan lebar txtRekening
            int innerW = this.pnlEdit.Width - 30;

            // Tombol di kanan (posisi relatif terhadap innerW)
            this.btnSimpanEdit.Left = innerW - this.btnHapusPO.Width - this.btnSimpanEdit.Width - 10;
            this.btnHapusPO.Left = innerW - this.btnHapusPO.Width;

            // txtRekening: isi sisa lebar
            this.txtRekening.Left = 15;
            int rekeningWidth = innerW - 15;
            if (rekeningWidth < 150) rekeningWidth = 150;
            this.txtRekening.Width = rekeningWidth;
        }
    }
}
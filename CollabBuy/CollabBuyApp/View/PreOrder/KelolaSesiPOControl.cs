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

        public KelolaSesiPOControl(Models.User currentUser)
        {
            this.InitializeComponent();
            this._currentUser = currentUser;
            this._poController = new PreOrderController();
            this.Resize += (s, e) => this.AdjustLayout();
        }

        private void KelolaSesiPOControl_Load(object sender, EventArgs e)
        {
            this.AdjustLayout();
            this.SetupDataGridView();
            this.LoadDataPO();
            this.SetFormEnabled(false);
        }

        private void SetupDataGridView()
        {
            this.dgvPO.AutoGenerateColumns = false;
            this.dgvPO.Columns.Clear();

            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdPo", DataPropertyName = "id_po", Visible = false });
            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "Judul", HeaderText = "Nama Sesi", DataPropertyName = "judul_po", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "Jenis", HeaderText = "Tipe", DataPropertyName = "jenis_po", Width = 110 });
            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "Batas", HeaderText = "Tutup Pada", DataPropertyName = "batas_waktu", Width = 155 });
            this.dgvPO.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", DataPropertyName = "is_aktif", Width = 70 });
        }

        private void LoadDataPO()
        {
            try
            {
                DataTable dt = this._poController.GetPOByPenjual(this._currentUser.GetIdUser());
                this.dgvPO.DataSource = dt;
                this.dgvPO.ClearSelection();
                this.ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal muat data PO: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = this.dgvPO.Rows[e.RowIndex];
            this._selectedIdPo = Convert.ToInt32(row.Cells["IdPo"].Value);

            this.txtJudul.Text = row.Cells["Judul"].Value.ToString();
            this.txtRekening.Text = "";

            string jenis = row.Cells["Jenis"].Value.ToString();
            this.cbJenis.SelectedItem = jenis;

            if (DateTime.TryParse(row.Cells["Batas"].Value.ToString(), out DateTime batas))
            {
                if (batas > DateTime.Now)
                {
                    this.dtpBatas.Value = batas;
                }
                else
                {
                    this.dtpBatas.Value = DateTime.Now.AddDays(1);
                }
            }
            else
            {
                this.dtpBatas.Value = DateTime.Now.AddDays(1);
            }

            this.SetFormEnabled(true);
        }

        private void btnSimpanEdit_Click(object sender, EventArgs e)
        {
            if (this._selectedIdPo == 0)
            {
                MessageBox.Show("Pilih dulu sesi PO yang mau diedit!", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.txtRekening.Text))
            {
                MessageBox.Show("Info rekening wajib diisi ulang untuk konfirmasi update!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtRekening.Focus();
                return;
            }

            DialogResult dr = MessageBox.Show(
                $"Yakin update sesi '{this.txtJudul.Text}'?",
                "Konfirmasi Edit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

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
            else
            {
                bool batalEdit = true;
            }
        }

        private void btnHapusPO_Click(object sender, EventArgs e)
        {
            if (this._selectedIdPo == 0)
            {
                MessageBox.Show("Pilih dulu sesi PO yang mau dihapus!", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                $"Yakin mau tutup & hapus sesi '{this.txtJudul.Text}'?\n\nData tidak akan muncul lagi di katalog (soft delete, data aman di DB).",
                "Konfirmasi Hapus",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

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
            else
            {
                bool batalHapus = true;
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

            // Tombol edit & hapus: tampak pudar kalau tidak aktif (sesuai permintaan)
            this.btnSimpanEdit.Enabled = enabled;
            this.btnSimpanEdit.BackColor = enabled
                ? System.Drawing.Color.FromArgb(36, 0, 70)
                : System.Drawing.Color.FromArgb(160, 160, 160);
            this.btnSimpanEdit.ForeColor = System.Drawing.Color.White;

            this.btnHapusPO.Enabled = enabled;
            this.btnHapusPO.BackColor = enabled
                ? System.Drawing.Color.FromArgb(220, 53, 69)
                : System.Drawing.Color.FromArgb(160, 160, 160);
            this.btnHapusPO.ForeColor = System.Drawing.Color.White;
        }

        private void AdjustLayout()
        {
            int margin = 36;
            int w = this.Width - (margin * 2);
            this.dgvPO.Width = w;
            this.pnlEdit.Width = w;
        }
    }
}
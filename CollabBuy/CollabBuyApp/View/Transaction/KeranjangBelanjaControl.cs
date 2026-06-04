using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Transaction
{
    public partial class KeranjangBelanjaControl : UserControl
    {
        private readonly Models.User _user;
        private readonly TransactionController _trxCtrl;
        private int _selectedIdProduk;
        private string _selectedOldPenitip;

        private System.Windows.Forms.Timer _timerInfo;

        // Event navigasi ke halaman pembayaran
        public event Action<long> OnNavigatePembayaran;

        public KeranjangBelanjaControl(Models.User user, TransactionController trxCtrl)
        {
            this.InitializeComponent();

            this._user = user;
            this._trxCtrl = trxCtrl;
            this._selectedIdProduk = 0;
            this._selectedOldPenitip = "";

            this._timerInfo = new System.Windows.Forms.Timer();
            this._timerInfo.Interval = 3500;
            this._timerInfo.Tick += (s, e) =>
            {
                this.lblInfo.Visible = false;
                this._timerInfo.Stop();
            };

            this.Dock = DockStyle.Fill;
        }

        private void KeranjangBelanjaControl_Load(object sender, EventArgs e)
        {
            this.SetupDataGridView();
            this.MuatKeranjang();
        }

        private void SetupDataGridView()
        {
            this.dgvKeranjang.AutoGenerateColumns = false;
            this.dgvKeranjang.Columns.Clear();

            this.dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdProduk", DataPropertyName = "IdProduk", Visible = false });
            this.dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaItem", HeaderText = "Produk", DataPropertyName = "NamaItem", Width = 150 });
            this.dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaPenitip", HeaderText = "Atas Nama", DataPropertyName = "NamaPenitip", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            this.dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Catatan", HeaderText = "Notes", DataPropertyName = "Catatan", Width = 120 });
            this.dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Harga", HeaderText = "Harga", DataPropertyName = "Harga", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            this.dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kuantitas", HeaderText = "Qty", DataPropertyName = "Kuantitas", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

            DataGridViewButtonColumn btnHapus = new DataGridViewButtonColumn
            {
                Name = "BtnHapus",
                HeaderText = "Aksi",
                Text = "❌",
                UseColumnTextForButtonValue = true,
                Width = 50,
                FlatStyle = FlatStyle.Flat
            };
            btnHapus.DefaultCellStyle.BackColor = Color.LightCoral;
            btnHapus.DefaultCellStyle.ForeColor = Color.White;

            this.dgvKeranjang.Columns.Add(btnHapus);
        }

        public void MuatKeranjang()
        {
            try
            {
                DataTable dt = this._trxCtrl.GetKeranjangDataTable();
                this.dgvKeranjang.DataSource = dt;
                this.dgvKeranjang.ClearSelection();

                long total = this._trxCtrl.HitungTotalKeranjangSaatIni();
                this.lblTotalHarga.Text = "Rp " + total.ToString("N0");

                bool adaBarang;
                if (dt.Rows.Count > 0)
                {
                    adaBarang = true;
                    this.btnCheckout.BackColor = Color.FromArgb(36, 0, 70);
                }
                else
                {
                    adaBarang = false;
                    this.btnCheckout.BackColor = Color.Gray;
                }

                this.btnCheckout.Enabled = adaBarang;
                this.btnKosongkan.Enabled = adaBarang;

                this.ResetFormTitipan();
            }
            catch (Exception ex)
            {
                this.TampilkanInfo($"Gagal memuat keranjang: {ex.Message}", false);
            }
        }

        private void dgvKeranjang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                // Klik pada header kolom diabaikan
                bool ignoreClick = true;
            }
            else
            {
                int idProduk = Convert.ToInt32(this.dgvKeranjang.Rows[e.RowIndex].Cells["IdProduk"].Value);
                string namaPenitip = this.dgvKeranjang.Rows[e.RowIndex].Cells["NamaPenitip"].Value.ToString();

                if (this.dgvKeranjang.Columns[e.ColumnIndex].Name == "BtnHapus")
                {
                    this._trxCtrl.HapusItemKeranjang(idProduk, namaPenitip);
                    this.TampilkanInfo("✅ Item berhasil dihapus dari keranjang.", true);
                    this.MuatKeranjang();
                }
                else
                {
                    // Populasikan form Edit Titipan jika baris (bukan tombol hapus) diklik
                    this._selectedIdProduk = idProduk;
                    this._selectedOldPenitip = namaPenitip;

                    this.txtProduk.Text = this.dgvKeranjang.Rows[e.RowIndex].Cells["NamaItem"].Value.ToString();
                    this.txtPenitip.Text = namaPenitip;
                    this.txtCatatan.Text = this.dgvKeranjang.Rows[e.RowIndex].Cells["Catatan"].Value.ToString();
                    this.numQty.Value = Convert.ToInt32(this.dgvKeranjang.Rows[e.RowIndex].Cells["Kuantitas"].Value);

                    this.pnlTitipan.Enabled = true;
                    this.btnSimpanTitipan.BackColor = Color.FromArgb(155, 246, 255);
                    this.btnSimpanTitipan.ForeColor = Color.FromArgb(36, 0, 70);
                    this.btnTambahTitipan.BackColor = Color.FromArgb(36, 0, 70);
                    this.btnTambahTitipan.ForeColor = Color.White;
                }
            }
        }

        private void btnSimpanTitipan_Click(object sender, EventArgs e)
        {
            if (this._selectedIdProduk == 0 || string.IsNullOrWhiteSpace(this.txtPenitip.Text))
            {
                // Form belum valid/kosong
                bool invalidForm = true;
            }
            else
            {
                this._trxCtrl.UpdateTitipan(this._selectedIdProduk, this._selectedOldPenitip, this.txtPenitip.Text, (int)this.numQty.Value, this.txtCatatan.Text);
                this.TampilkanInfo("✅ Sip! Titipan berhasil di-update.", true);
                this.MuatKeranjang();
            }
        }

        private void btnTambahTitipan_Click(object sender, EventArgs e)
        {
            if (this._selectedIdProduk == 0 || string.IsNullOrWhiteSpace(this.txtPenitip.Text))
            {
                // Form belum valid/kosong
                bool invalidForm = true;
            }
            else
            {
                this._trxCtrl.TambahTitipanBaru(this._selectedIdProduk, this.txtPenitip.Text, (int)this.numQty.Value, this.txtCatatan.Text);
                this.TampilkanInfo("✅ Nice! Titipan baru berhasil dipisah.", true);
                this.MuatKeranjang();
            }
        }

        private void ResetFormTitipan()
        {
            this._selectedIdProduk = 0;
            this._selectedOldPenitip = "";
            this.txtProduk.Clear();
            this.txtPenitip.Clear();
            this.txtCatatan.Clear();
            this.numQty.Value = 1;
            this.pnlTitipan.Enabled = false;
            this.btnSimpanTitipan.BackColor = Color.FromArgb(210, 210, 210);
            this.btnSimpanTitipan.ForeColor = Color.FromArgb(140, 140, 140);
            this.btnTambahTitipan.BackColor = Color.FromArgb(210, 210, 210);
            this.btnTambahTitipan.ForeColor = Color.FromArgb(140, 140, 140);
        }

        private void btnKosongkan_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Yakin hapus semua jajanannya? 😭", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (konfirmasi == DialogResult.Yes)
            {
                this._trxCtrl.KosongkanKeranjang();
                this.TampilkanInfo("✅ Keranjang berhasil dikosongkan.", true);
                this.MuatKeranjang();
            }
            else
            {
                // Aksi batal
                bool aksiBatal = true;
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            long total = this._trxCtrl.HitungTotalKeranjangSaatIni();

            if (total <= 0)
            {
                this.TampilkanInfo("⚠️ Keranjang masih kosong! Jajan dulu yuk.", false);
                return;
            }

            // Jalankan validasi (termasuk min order) sebelum navigasi ke pembayaran
            var (validasi, pesanValidasi) = this._trxCtrl.ValidasiKeranjangSebelumCheckout();
            if (!validasi)
            {
                MessageBox.Show(
                    pesanValidasi,
                    "⚠️ Tidak Bisa Lanjut,Periksa kembali Keranjang Anda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (this.OnNavigatePembayaran != null)
            {
                this.OnNavigatePembayaran.Invoke(total);
            }
        }

        private void TampilkanInfo(string pesan, bool sukses)
        {
            this.lblInfo.Text = pesan;
            this.lblInfo.Visible = true;

            if (sukses)
            {
                this.lblInfo.BackColor = Color.FromArgb(210, 255, 230);
                this.lblInfo.ForeColor = Color.FromArgb(0, 100, 50);
            }
            else
            {
                this.lblInfo.BackColor = Color.FromArgb(255, 220, 220);
                this.lblInfo.ForeColor = Color.FromArgb(150, 0, 0);
            }

            this._timerInfo.Stop();
            this._timerInfo.Start();
        }
    }
}
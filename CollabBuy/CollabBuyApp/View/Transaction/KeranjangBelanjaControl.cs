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
        private int _selectedIdProduk = 0;
        private string _selectedOldPenitip = "";

        // PERBAIKAN: Perjelas namespace Timer biar nggak bentrok (Ambiguous CS0104)
        private System.Windows.Forms.Timer _timerInfo;

        // Event: checkout sekarang mengarah ke halaman pembayaran
        public event Action<long> OnNavigatePembayaran;

        public KeranjangBelanjaControl(Models.User user, TransactionController trxCtrl)
        {
            InitializeComponent();
            _user = user;
            _trxCtrl = trxCtrl;

            // PERBAIKAN: Inisialisasi secara eksplisit
            _timerInfo = new System.Windows.Forms.Timer();
            _timerInfo.Interval = 3500;
            _timerInfo.Tick += (s, e) => { lblInfo.Visible = false; _timerInfo.Stop(); };

            this.Dock = DockStyle.Fill;
        }

        private void KeranjangBelanjaControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            MuatKeranjang();
        }

        private void SetupDataGridView()
        {
            dgvKeranjang.AutoGenerateColumns = false;
            dgvKeranjang.Columns.Clear();

            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdProduk", DataPropertyName = "IdProduk", Visible = false });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaItem", HeaderText = "Produk", DataPropertyName = "NamaItem", Width = 150 });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "NamaPenitip", HeaderText = "Atas Nama", DataPropertyName = "NamaPenitip", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Catatan", HeaderText = "Notes", DataPropertyName = "Catatan", Width = 120 });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Harga", HeaderText = "Harga", DataPropertyName = "Harga", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvKeranjang.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kuantitas", HeaderText = "Qty", DataPropertyName = "Kuantitas", Width = 50, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

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
            dgvKeranjang.Columns.Add(btnHapus);
        }

        public void MuatKeranjang()
        {
            try
            {
                DataTable dt = _trxCtrl.GetKeranjangDataTable();
                dgvKeranjang.DataSource = dt;
                dgvKeranjang.ClearSelection();

                long total = _trxCtrl.HitungTotalKeranjangSaatIni();
                lblTotalHarga.Text = "Rp " + total.ToString("N0");

                bool adaBarang = dt.Rows.Count > 0;
                btnCheckout.Enabled = adaBarang;
                btnCheckout.BackColor = adaBarang ? Color.FromArgb(36, 0, 70) : Color.Gray;
                btnKosongkan.Enabled = adaBarang;

                ResetFormTitipan();
            }
            catch (Exception ex)
            {
                TampilkanInfo($"Gagal memuat keranjang: {ex.Message}", false);
            }
        }

        private void dgvKeranjang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int idProduk = Convert.ToInt32(dgvKeranjang.Rows[e.RowIndex].Cells["IdProduk"].Value);
            string namaPenitip = dgvKeranjang.Rows[e.RowIndex].Cells["NamaPenitip"].Value.ToString();

            if (dgvKeranjang.Columns[e.ColumnIndex].Name == "BtnHapus")
            {
                _trxCtrl.HapusItemKeranjang(idProduk, namaPenitip);
                TampilkanInfo("✅ Item berhasil dihapus dari keranjang.", true);
                MuatKeranjang();
                return;
            }

            // Populasikan form Edit Titipan
            _selectedIdProduk = idProduk;
            _selectedOldPenitip = namaPenitip;

            txtProduk.Text = dgvKeranjang.Rows[e.RowIndex].Cells["NamaItem"].Value.ToString();
            txtPenitip.Text = namaPenitip;
            txtCatatan.Text = dgvKeranjang.Rows[e.RowIndex].Cells["Catatan"].Value.ToString();
            numQty.Value = Convert.ToInt32(dgvKeranjang.Rows[e.RowIndex].Cells["Kuantitas"].Value);

            pnlTitipan.Enabled = true;
        }

        private void btnSimpanTitipan_Click(object sender, EventArgs e)
        {
            if (_selectedIdProduk == 0 || string.IsNullOrWhiteSpace(txtPenitip.Text)) return;

            _trxCtrl.UpdateTitipan(_selectedIdProduk, _selectedOldPenitip, txtPenitip.Text, (int)numQty.Value, txtCatatan.Text);
            TampilkanInfo("✅ Sip! Titipan berhasil di-update.", true);
            MuatKeranjang();
        }

        private void btnTambahTitipan_Click(object sender, EventArgs e)
        {
            if (_selectedIdProduk == 0 || string.IsNullOrWhiteSpace(txtPenitip.Text)) return;

            _trxCtrl.TambahTitipanBaru(_selectedIdProduk, txtPenitip.Text, (int)numQty.Value, txtCatatan.Text);
            TampilkanInfo("✅ Nice! Titipan baru berhasil dipisah.", true);
            MuatKeranjang();
        }

        private void ResetFormTitipan()
        {
            _selectedIdProduk = 0;
            _selectedOldPenitip = "";
            txtProduk.Clear();
            txtPenitip.Clear();
            txtCatatan.Clear();
            numQty.Value = 1;
            pnlTitipan.Enabled = false;
        }

        private void btnKosongkan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin hapus semua jajanannya? 😭", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _trxCtrl.KosongkanKeranjang();
                TampilkanInfo("✅ Keranjang berhasil dikosongkan.", true);
                MuatKeranjang();
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            long total = _trxCtrl.HitungTotalKeranjangSaatIni();

            if (total <= 0)
            {
                TampilkanInfo("⚠️ Keranjang masih kosong! Jajan dulu yuk.", false);
                return;
            }

            // Arahkan ke form pembayaran dan kirim data total tagihan
            if (OnNavigatePembayaran != null)
            {
                OnNavigatePembayaran.Invoke(total);
            }
        }

        private void TampilkanInfo(string pesan, bool sukses)
        {
            lblInfo.Text = pesan;
            lblInfo.BackColor = sukses ? Color.FromArgb(210, 255, 230) : Color.FromArgb(255, 220, 220);
            lblInfo.ForeColor = sukses ? Color.FromArgb(0, 100, 50) : Color.FromArgb(150, 0, 0);
            lblInfo.Visible = true;
            _timerInfo.Stop();
            _timerInfo.Start();
        }
    }
}
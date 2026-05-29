using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Admin
{
    public partial class TanggapanAduanControl : UserControl
    {
        // Gunakan ComplaintController untuk memisahkan logika khusus Aduan
        private readonly ComplaintController _complaintController;

        public TanggapanAduanControl()
        {
            InitializeComponent();
            _complaintController = new ComplaintController();
        }

        private void TanggapanAduanControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataAduan();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataAduan();
        }

        // --- KONFIGURASI KOLOM TABEL ---
        private void SetupDataGridView()
        {
            dgvAduan.AutoGenerateColumns = false;
            dgvAduan.Columns.Clear();

            dgvAduan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdComplaint",
                DataPropertyName = "Id", // Ganti sesuai property ID di model Complaint
                Visible = false // Disembunyikan, hanya untuk referensi aksi
            });

            dgvAduan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tanggal",
                HeaderText = "Tanggal",
                DataPropertyName = "Tanggal", // Ganti sesuai property Tanggal di model
                Width = 120
            });

            dgvAduan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pengirim",
                HeaderText = "Pengirim (User)",
                DataPropertyName = "NamaPengirim", // Ganti sesuai property nama user di model
                Width = 150
            });

            // AutoSizeMode = Fill agar deskripsi memanjang menyesuaikan layar
            dgvAduan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Deskripsi",
                HeaderText = "Isi Aduan Kendala",
                DataPropertyName = "Deskripsi",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvAduan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 120
            });

            // Tombol Aksi (Tandai Selesai)
            DataGridViewButtonColumn btnSelesaikan = new DataGridViewButtonColumn
            {
                Name = "BtnSelesaikan",
                HeaderText = "Aksi",
                Text = "✅ Selesaikan",
                UseColumnTextForButtonValue = true,
                Width = 120,
                FlatStyle = FlatStyle.Flat
            };
            btnSelesaikan.DefaultCellStyle.BackColor = Color.FromArgb(200, 182, 255);
            btnSelesaikan.DefaultCellStyle.ForeColor = Color.FromArgb(36, 0, 70);
            dgvAduan.Columns.Add(btnSelesaikan);
        }

        // --- MEMUAT DATA ADUAN ---
        private void LoadDataAduan()
        {
            try
            {
                // TODO: Panggil method dari ComplaintController Anda
                // var daftarAduan = _complaintController.GetAllComplaints();

                // MOCK DATA (Hapus bagian ini jika method controller sudah siap)
                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("Id", typeof(int));
                dtMock.Columns.Add("Tanggal", typeof(string));
                dtMock.Columns.Add("NamaPengirim", typeof(string));
                dtMock.Columns.Add("Deskripsi", typeof(string));
                dtMock.Columns.Add("Status", typeof(string));

                dtMock.Rows.Add(1, "12 Nov 2023", "Budi Santoso", "Pesanan saya dibatalkan sepihak oleh penjual tanpa alasan.", "Pending");
                dtMock.Rows.Add(2, "14 Nov 2023", "Siti Aminah", "Aplikasi force close ketika menekan tombol keranjang.", "Pending");
                dtMock.Rows.Add(3, "15 Nov 2023", "Andi Wijaya", "Penjual tidak merespon chat selama 2 hari setelah checkout.", "Selesai");

                dgvAduan.DataSource = dtMock;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data aduan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- EVENT KLIK TABEL ---
        private void dgvAduan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Abaikan jika yang diklik adalah Header (baris -1)
            if (e.RowIndex < 0) return;

            // Jika yang diklik adalah kolom tombol "Selesaikan"
            if (dgvAduan.Columns[e.ColumnIndex].Name == "BtnSelesaikan")
            {
                string statusSaatIni = dgvAduan.Rows[e.RowIndex].Cells["Status"].Value.ToString();

                // Cegah memproses aduan yang sudah selesai
                if (statusSaatIni.ToLower() == "selesai" || statusSaatIni.ToLower() == "resolved")
                {
                    MessageBox.Show("Aduan ini sudah ditandai selesai.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int idAduan = Convert.ToInt32(dgvAduan.Rows[e.RowIndex].Cells["IdComplaint"].Value);
                string namaPengirim = dgvAduan.Rows[e.RowIndex].Cells["Pengirim"].Value.ToString();

                DialogResult dialog = MessageBox.Show(
                    $"Tandai aduan dari {namaPengirim} sebagai 'Selesai'?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    ProsesPenyelesaianAduan(idAduan);
                }
            }
        }

        private void ProsesPenyelesaianAduan(int idAduan)
        {
            try
            {
                // PANGGIL CONTROLLER DI SINI
                // bool sukses = _complaintController.ResolveComplaint(idAduan);

                // MOCK SUCCESS
                bool sukses = true;

                if (sukses)
                {
                    MessageBox.Show("Aduan berhasil diselesaikan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataAduan(); // Refresh data agar status berubah
                }
                else
                {
                    MessageBox.Show("Gagal menyelesaikan aduan. Silakan coba lagi.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

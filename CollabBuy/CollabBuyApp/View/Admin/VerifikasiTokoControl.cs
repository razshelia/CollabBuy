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
    public partial class VerifikasiTokoControl : UserControl
    {
        private readonly AdminController _adminController;

        public VerifikasiTokoControl()
        {
            InitializeComponent();
            _adminController = new AdminController();
        }

        private void VerifikasiTokoControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataVerifikasi();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataVerifikasi();
        }

        // Konfigurasi Kolom DataGridView
        private void SetupDataGridView()
        {
            dgvVerifikasi.AutoGenerateColumns = false;
            dgvVerifikasi.Columns.Clear();

            // Kolom ID User/Toko (Disembunyikan, hanya untuk referensi sistem)
            dgvVerifikasi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdUser",
                DataPropertyName = "IdUser",
                Visible = false
            });

            dgvVerifikasi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nama",
                HeaderText = "Nama Pemohon",
                DataPropertyName = "Nama",
                Width = 200
            });

            dgvVerifikasi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NIM",
                HeaderText = "NIM/NPM",
                DataPropertyName = "NIM",
                Width = 150
            });

            dgvVerifikasi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NamaToko",
                HeaderText = "Nama Lapak/Toko",
                DataPropertyName = "NamaToko",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvVerifikasi.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tanggal",
                HeaderText = "Tanggal Pengajuan",
                DataPropertyName = "Tanggal",
                Width = 150
            });

            // Kolom Tombol Terima
            DataGridViewButtonColumn btnTerima = new DataGridViewButtonColumn
            {
                Name = "BtnTerima",
                HeaderText = "Aksi",
                Text = "✅ Terima",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            btnTerima.DefaultCellStyle.BackColor = Color.LightGreen;
            btnTerima.DefaultCellStyle.ForeColor = Color.Black;
            dgvVerifikasi.Columns.Add(btnTerima);

            // Kolom Tombol Tolak
            DataGridViewButtonColumn btnTolak = new DataGridViewButtonColumn
            {
                Name = "BtnTolak",
                HeaderText = "",
                Text = "❌ Tolak",
                UseColumnTextForButtonValue = true,
                Width = 100,
                FlatStyle = FlatStyle.Flat
            };
            btnTolak.DefaultCellStyle.BackColor = Color.LightCoral;
            btnTolak.DefaultCellStyle.ForeColor = Color.Black;
            dgvVerifikasi.Columns.Add(btnTolak);
        }

        private void LoadDataVerifikasi()
        {
            try
            {
                // TODO: Panggil method dari AdminController Anda
                // var pendingList = _adminController.GetPendingVerifications();

                // MOCK DATA (Data Bohongan untuk Preview UI)
                // Hapus bagian ini jika method _adminController.GetPendingVerifications() sudah siap
                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("IdUser", typeof(int));
                dtMock.Columns.Add("Nama", typeof(string));
                dtMock.Columns.Add("NIM", typeof(string));
                dtMock.Columns.Add("NamaToko", typeof(string));
                dtMock.Columns.Add("Tanggal", typeof(string));

                dtMock.Rows.Add(1, "Budi Santoso", "210001823", "Danus HMTI Budi", "12 Nov 2023");
                dtMock.Rows.Add(2, "Siti Aminah", "210001899", "Siti Jajanan", "13 Nov 2023");
                dtMock.Rows.Add(3, "Andi Wijaya", "220001234", "Lapak Fasilkom", "14 Nov 2023");

                dgvVerifikasi.DataSource = dtMock;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event saat salah satu sel/tombol di grid diklik
        private void dgvVerifikasi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Abaikan jika yang diklik adalah Header
            if (e.RowIndex < 0) return;

            // Ambil ID User dari baris yang diklik
            int userId = Convert.ToInt32(dgvVerifikasi.Rows[e.RowIndex].Cells["IdUser"].Value);
            string namaToko = dgvVerifikasi.Rows[e.RowIndex].Cells["NamaToko"].Value.ToString();

            // Cek jika yang diklik adalah kolom tombol "Terima"
            if (dgvVerifikasi.Columns[e.ColumnIndex].Name == "BtnTerima")
            {
                DialogResult dialog = MessageBox.Show($"Apakah Anda yakin ingin MENERIMA pengajuan toko '{namaToko}'?",
                                                      "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    ProsesVerifikasi(userId, true);
                }
            }
            // Cek jika yang diklik adalah kolom tombol "Tolak"
            else if (dgvVerifikasi.Columns[e.ColumnIndex].Name == "BtnTolak")
            {
                DialogResult dialog = MessageBox.Show($"Apakah Anda yakin ingin MENOLAK pengajuan toko '{namaToko}'?",
                                                      "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    ProsesVerifikasi(userId, false);
                }
            }
        }

        private void ProsesVerifikasi(int userId, bool isApproved)
        {
            try
            {
                // PANGGIL CONTROLLER DI SINI
                // bool success = _adminController.ProcessVerification(userId, isApproved);

                // MOCK SUCCESS
                bool success = true;

                if (success)
                {
                    string status = isApproved ? "diterima" : "ditolak";
                    MessageBox.Show($"Pengajuan berhasil {status}!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataVerifikasi(); // Refresh tabel setelah aksi
                }
                else
                {
                    MessageBox.Show("Gagal memproses pengajuan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

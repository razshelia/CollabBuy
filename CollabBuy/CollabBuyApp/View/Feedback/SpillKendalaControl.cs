using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Feedback
{
    public partial class SpillKendalaControl : UserControl
    {
        private readonly User _currentUser;
        private readonly ComplaintController _complaintController;

        public SpillKendalaControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _complaintController = new ComplaintController();
        }

        private void SpillKendalaControl_Load(object sender, EventArgs e)
        {
            if (cbJenisKendala.Items.Count > 0)
            {
                cbJenisKendala.SelectedIndex = 0;
            }
        }

        private void btnKirimAduan_Click(object sender, EventArgs e)
        {
            // 1. Validasi Input Dasar
            if (cbJenisKendala.SelectedItem == null)
            {
                MessageBox.Show("Silakan pilih kategori kendala Anda.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDeskripsi.Text))
            {
                MessageBox.Show("Detail permasalahan tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDeskripsi.Focus();
                return;
            }

            // Validasi ID Pesanan hanya boleh diisi angka jika tidak kosong
            int? idPesananTerlampir = null;
            if (!string.IsNullOrWhiteSpace(txtIdPesanan.Text))
            {
                if (int.TryParse(txtIdPesanan.Text, out int parsedId))
                {
                    idPesananTerlampir = parsedId;
                }
                else
                {
                    MessageBox.Show("ID Pesanan harus berupa angka!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIdPesanan.Focus();
                    return;
                }
            }

            // 2. Konfirmasi Pengiriman
            DialogResult dr = MessageBox.Show("Apakah Anda yakin ingin mengirimkan aduan ini ke Admin?",
                                              "Konfirmasi Aduan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                ProsesKirimAduan(cbJenisKendala.SelectedItem.ToString(), txtDeskripsi.Text.Trim(), idPesananTerlampir);
            }
        }

        private void ProsesKirimAduan(string kategori, string deskripsi, int? idPesanan)
        {
            try
            {
                // Format gabungan untuk deskripsi agar admin tahu kategorinya
                string keluhanLengkap = $"[{kategori}] - {deskripsi}";

                // Tambahkan catatan jika ada ID Pesanan
                if (idPesanan.HasValue)
                {
                    keluhanLengkap += $"\n(Terkait ID Pesanan: #{idPesanan.Value})";
                }

                // TODO: Panggil method dari ComplaintController untuk menyimpan ke database
                // Asumsi: Model Complaint memiliki konstruktor (int idUser, string deskripsi) atau (int idUser, string kategori, string deskripsi)
                // var aduanBaru = new CollabBuy.CollabBuyApp.Models.Complaint(_currentUser.IdUser, keluhanLengkap);
                // bool sukses = _complaintController.SubmitComplaint(aduanBaru);

                bool sukses = true; // Mock sukses

                if (sukses)
                {
                    MessageBox.Show("Aduan berhasil dikirim. Tim kami akan segera meninjaunya.\nTerima kasih atas laporan Anda.",
                                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Gagal mengirim aduan. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            if (cbJenisKendala.Items.Count > 0) cbJenisKendala.SelectedIndex = 0;
            txtIdPesanan.Clear();
            txtDeskripsi.Clear();
        }
    }
}

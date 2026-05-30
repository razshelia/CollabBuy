using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.UserDashboard
{
    public partial class DaftarTokoControl : UserControl
    {
        private User _currentUser;
        private readonly UserController _userController;
        private byte[] _buktiKtmBytes; // Buat nampung data foto

        public DaftarTokoControl(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _userController = new UserController();
            this.Resize += DaftarTokoControl_Resize;
        }

        private void DaftarTokoControl_Resize(object sender, EventArgs e)
        {
            if (pnlCard != null)
            {
                pnlCard.Left = (this.Width - pnlCard.Width) / 2;
                pnlCard.Top = (this.Height - pnlCard.Height) / 2;
            }
        }

        private void DaftarTokoControl_Load(object sender, EventArgs e)
        {
            CekStatusVerifikasi();
        }

        // Cegah input abjad di NIM dan Tahun Masuk
        private void HanyaAngka_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Fitur Dialog Pilih Foto
        private void btnUploadKTM_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih Foto KTM Kamu";
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _buktiKtmBytes = File.ReadAllBytes(ofd.FileName);
                    lblNamaFile.Text = Path.GetFileName(ofd.FileName);
                    lblNamaFile.ForeColor = Color.Green;
                }
            }
        }

        private void CekStatusVerifikasi()
        {
            bool isVerifiedSeller = _currentUser.GetPeran() == "Penjual";
            bool isPendingVerification = _userController.CekPendingVerifikasi(_currentUser.GetIdUser());

            if (isVerifiedSeller)
            {
                pnlForm.Visible = false;
                pnlStatus.Visible = true;
                lblStatusVerifikasi.Text = "✅ Asyik! Lapak kamu udah terverifikasi.";
                pnlStatus.BackColor = Color.LightGreen;
            }
            else if (isPendingVerification)
            {
                pnlForm.Visible = false;
                pnlStatus.Visible = true;
                lblStatusVerifikasi.Text = "⏳ Pengajuan lagi antre dicek Admin nih. Sabar ya!";
                pnlStatus.BackColor = Color.FromArgb(253, 255, 182);
            }
            else
            {
                pnlForm.Visible = true;
                pnlStatus.Visible = false;
            }
        }

        private void btnAjukan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaToko.Text) || string.IsNullOrWhiteSpace(txtNIM.Text) || string.IsNullOrWhiteSpace(txtTahunMasuk.Text))
            {
                MessageBox.Show("Formnya diisi yang lengkap ya bestie!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!chkSyarat.Checked)
            {
                MessageBox.Show("Centang dulu dong persyaratannya.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialog = MessageBox.Show($"Yakin mau buka lapak dengan nama '{txtNamaToko.Text}'?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                int tahun = int.TryParse(txtTahunMasuk.Text, out int t) ? t : DateTime.Now.Year;

                // Kirim byte[] foto KTM ke Controller
                var (sukses, pesan) = _userController.AjukanVerifikasiToko(_currentUser.GetIdUser(), txtNIM.Text.Trim(), txtNamaToko.Text.Trim(), tahun, _buktiKtmBytes);

                if (sukses)
                {
                    MessageBox.Show(pesan, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CekStatusVerifikasi();
                }
                else
                {
                    MessageBox.Show(pesan, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
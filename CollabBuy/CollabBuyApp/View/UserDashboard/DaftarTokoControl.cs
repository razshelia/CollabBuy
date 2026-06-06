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
        private Models.User _currentUser;
        private readonly UserController _userController;
        private byte[] _buktiKtmBytes;

        public DaftarTokoControl(Models.User currentUser)
        {
            this.InitializeComponent();

            this._currentUser = currentUser;
            this._userController = new UserController();

            this.Resize += this.DaftarTokoControl_Resize;
        }

        private void DaftarTokoControl_Resize(object sender, EventArgs e)
        {
            if (this.pnlCard != null)
            {
                int maxW = 500;
                int availW = this.Width - 80;

                if (availW < maxW)
                {
                    this.pnlCard.Width = availW;
                }
                else
                {
                    this.pnlCard.Width = maxW;
                }

                int maxH = 650;
                int availH = this.Height - 60;

                if (availH < maxH)
                {
                    this.pnlCard.Height = availH;
                }
                else
                {
                    this.pnlCard.Height = maxH;
                }

                this.pnlCard.AutoScroll = true;

                int innerW = this.pnlCard.Width - 80;
                this.pnlStatus.Width = innerW;
                this.pnlForm.Width = innerW;
                this.txtNamaToko.Width = innerW;
                this.txtNIM.Width = innerW;
                this.txtTahunMasuk.Width = innerW;
                this.chkSyarat.Width = innerW;
                this.btnAjukan.Width = innerW;

                this.pnlCard.Left = (this.Width - this.pnlCard.Width) / 2;

                int topPos = (this.Height - this.pnlCard.Height) / 2;
                if (topPos > 20)
                {
                    this.pnlCard.Top = topPos;
                }
                else
                {
                    this.pnlCard.Top = 20;
                }
            }
            else
            {
                bool cardBelumDimuat = true; // Assignment nyata menghindari else kosong
            }
        }

        private void DaftarTokoControl_Load(object sender, EventArgs e)
        {
            this.DaftarTokoControl_Resize(null, null);
            this.CekStatusVerifikasi();
        }

        private void HanyaAngka_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                bool karakterAman = true;
            }
        }

        private void btnUploadKTM_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Pilih Foto KTM Kamu";
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this._buktiKtmBytes = File.ReadAllBytes(ofd.FileName);
                    this.lblNamaFile.Text = Path.GetFileName(ofd.FileName);
                    this.lblNamaFile.ForeColor = Color.Green;
                }
                else
                {
                    // User batal memilih file
                    bool batalPilihGambar = true;
                }
            }
        }

        private void CekStatusVerifikasi()
        {
            bool isVerifiedSeller;
            if (this._currentUser.Peran == "Penjual")
            {
                isVerifiedSeller = true;
            }
            else
            {
                isVerifiedSeller = false;
            }

            bool isPendingVerification = this._userController.CekPendingVerifikasi(this._currentUser.IdUser);

            if (isVerifiedSeller)
            {
                this.pnlForm.Visible = false;
                this.pnlStatus.Visible = true;
                this.lblStatusVerifikasi.Text = "✅ Asyik! Lapak kamu udah terverifikasi.";
                this.pnlStatus.BackColor = Color.LightGreen;
            }
            else if (isPendingVerification)
            {
                this.pnlForm.Visible = false;
                this.pnlStatus.Visible = true;
                this.lblStatusVerifikasi.Text = "⏳ Pengajuan lagi antre dicek Admin nih. Sabar ya!";
                this.pnlStatus.BackColor = Color.FromArgb(253, 255, 182);
            }
            else
            {
                this.pnlForm.Visible = true;
                this.pnlStatus.Visible = false;
            }
        }

        private void btnAjukan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtNamaToko.Text) || string.IsNullOrWhiteSpace(this.txtNIM.Text) || string.IsNullOrWhiteSpace(this.txtTahunMasuk.Text))
            {
                MessageBox.Show("Formnya diisi yang lengkap ya bestie!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!this.chkSyarat.Checked)
                {
                    MessageBox.Show("Centang dulu dong persyaratannya.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dialog = MessageBox.Show($"Yakin mau buka lapak dengan nama '{this.txtNamaToko.Text}'?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialog == DialogResult.Yes)
                    {
                        int tahun;
                        if (int.TryParse(this.txtTahunMasuk.Text, out int t))
                        {
                            tahun = t;
                        }
                        else
                        {
                            tahun = DateTime.Now.Year;
                        }

                        var (sukses, pesan) = this._userController.AjukanVerifikasiToko(this._currentUser.IdUser, this.txtNIM.Text.Trim(), this.txtNamaToko.Text.Trim(), tahun, this._buktiKtmBytes);

                        if (sukses)
                        {
                            MessageBox.Show(pesan, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.CekStatusVerifikasi();
                        }
                        else
                        {
                            MessageBox.Show(pesan, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // User batal mendaftar di pop-up konfirmasi
                        bool aksiBatal = true;
                    }
                }
            }
        }
    }
}
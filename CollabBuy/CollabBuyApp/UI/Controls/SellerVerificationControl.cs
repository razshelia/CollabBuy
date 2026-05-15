using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerVerificationControl : UserControl
    {
        private RegularUser _user;
        private Action _onVerificationSuccess;
        private string _pathKTM;

        public SellerVerificationControl(RegularUser user, Action onVerificationSuccess)
        {
            InitializeComponent();
            _user = user;
            _onVerificationSuccess = onVerificationSuccess;
            this.Resize += (s, e) => CenterCard();
        }

        private void CenterCard()
        {
            if (pnlCard == null) return;
            pnlCard.Left = (this.ClientSize.Width - pnlCard.Width) / 2;
            pnlCard.Top = (this.ClientSize.Height - pnlCard.Height) / 2;
        }

        private void btnUploadKTM_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Pilih Scan / Foto KTM";
                dlg.Filter = "File Gambar|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _pathKTM = FileHelper.SimpanFile(dlg.FileName, "KTM");
                        lblStatusKTM.Text = "KTM berhasil diunggah ✨";
                        lblStatusKTM.ForeColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        UXHelper.TampilkanError("Gagal menyimpan KTM: " + ex.Message);
                    }
                }
            }
        }

        private void btnKirim_Click(object sender, EventArgs e)
        {
            string nim = txtNIM.Text.Trim();
            string namaToko = txtNamaToko.Text.Trim();
            string tahunMasukStr = txtTahunMasuk.Text.Trim();

            if (string.IsNullOrWhiteSpace(nim))
            {
                UXHelper.TampilkanError("NIM wajib diisi, bestie! 🎓");
                return;
            }
            if (string.IsNullOrWhiteSpace(namaToko))
            {
                UXHelper.TampilkanError("Nama toko / danus jangan kosong ya~");
                return;
            }
            if (!int.TryParse(tahunMasukStr, out int tahunMasuk) || tahunMasuk < DateTime.Now.Year - 7 || tahunMasuk > DateTime.Now.Year)
            {
                UXHelper.TampilkanError($"Tahun masuk harus antara {DateTime.Now.Year - 7} sampai {DateTime.Now.Year}.");
                return;
            }
            if (string.IsNullOrEmpty(_pathKTM))
            {
                UXHelper.TampilkanError("Upload foto KTM dulu dong~ 📸");
                return;
            }

            var verifService = new VerificationService();
            bool sukses = verifService.AjukanVerifikasi(_user.IdUser, nim, namaToko, _pathKTM, tahunMasuk);
            if (sukses)
            {
                _onVerificationSuccess?.Invoke();
                if (ParentForm is MainForm main)
                    main.RefreshSidebar();
            }
        }
    }
}
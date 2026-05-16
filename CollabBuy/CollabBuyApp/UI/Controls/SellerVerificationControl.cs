using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Repositories; // Wajib ditambahkan untuk memanggil Repository

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerVerificationControl : UserControl
    {
        private readonly RegularUser _user;
        private readonly Action _onVerificationSuccess;
        private readonly VerificationService _verifService; // DI Class Level Field
        private string _pathKTM;

        public SellerVerificationControl(RegularUser user, Action onVerificationSuccess)
        {
            InitializeComponent();
            _user = user;
            _onVerificationSuccess = onVerificationSuccess;

            // TAHAP 4: INJEKSI MANUAL DI UI
            // Kita menyuntikkan VerificationRepository ke dalam VerificationService
            _verifService = new VerificationService(new VerificationRepository());

            // Memastikan posisi pnlCard otomatis berada di tengah layar simetris
            this.Resize += (s, e) => CenterCard();
            this.Load += (s, e) => CenterCard();
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
                        lblStatusKTM.Text = "KTM BERHASIL DIUNGGAH ✨";
                        lblStatusKTM.ForeColor = Color.FromArgb(182, 255, 200); // Soft Green Pastel
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

            // Menggunakan objek field service yang telah aman ter-injeksi arsitektur repositori
            bool sukses = _verifService.AjukanVerifikasi(_user.IdUser, nim, namaToko, _pathKTM, tahunMasuk);
            if (sukses)
            {
                _onVerificationSuccess?.Invoke();
                if (ParentForm is MainForm main)
                    main.RefreshSidebar();
            }
        }
    }
}
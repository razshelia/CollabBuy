using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Services;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerVerificationControl : UserControl
    {
        private Akun userAktif;
        private Action onVerificationSubmitted;
        private string pathKTM = null; // path relatif yang akan disimpan ke DB

        public SellerVerificationControl(Akun user, Action refreshSidebarCallback)
        {
            InitializeComponent();
            this.userAktif = user;
            this.onVerificationSubmitted = refreshSidebarCallback;
        }

        // Event handler tombol Upload KTM
        private void btnUploadKTM_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Pilih Scan / Foto KTM";
                openFileDialog.Filter = "File Gambar|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 1. Tentukan folder tujuan (Uploads/KTM di direktori aplikasi)
                        string folderTujuan = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads", "KTM");
                        if (!Directory.Exists(folderTujuan))
                            Directory.CreateDirectory(folderTujuan);

                        // 2. Buat nama file unik (timestamp + nama asli)
                        string namaFile = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(openFileDialog.FileName)}";
                        string fullPath = Path.Combine(folderTujuan, namaFile);

                        // 3. Salin file yang dipilih ke folder tujuan
                        File.Copy(openFileDialog.FileName, fullPath);

                        // 4. Simpan path relatif (dari BaseDirectory) untuk disimpan ke database
                        pathKTM = Path.Combine("Uploads", "KTM", namaFile);

                        // 5. Tampilkan status ke user
                        lblStatusKTM.Text = "KTM berhasil diunggah: " + namaFile;
                        lblStatusKTM.ForeColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        UXHelper.TampilkanError("Gagal menyimpan gambar KTM: " + ex.Message);
                    }
                }
            }
        }

        private void btnKirimPengajuan_Click(object sender, EventArgs e)
        {
            // Validasi input ...
            // Panggil UserService untuk menyimpan pengajuan (termasuk pathKTM)
            UserService userService = new UserService();
            bool sukses = userService.AjukanVerifikasiSeller(
                userAktif.IdUser,
                txtNamaToko.Text,
                txtNIM.Text,
                int.Parse(txtTahunMasuk.Text),
                pathKTM   // <-- path relatif disimpan ke database
            );

            if (sukses)
            {
                UXHelper.TampilkanSukses("Pengajuan seller berhasil dikirim. Tunggu verifikasi admin ya!");
                onVerificationSubmitted?.Invoke(); // refresh sidebar
            }
            else
            {
                UXHelper.TampilkanError("Gagal mengirim pengajuan, coba lagi nanti.");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class SellerVerificationControl : UserControl
    {
        public SellerVerificationControl()
        {
            InitializeComponent();
        }
        private void btnKirim_Click(object sender, EventArgs e)
        {
            int tahunSekarang = DateTime.Now.Year;
            int tahunMasuk = (int)this.numTahun.Value;

            // Logika Pencabutan Otomatis: Maksimal 7 tahun studi
            if ((tahunSekarang - tahunMasuk) > 7)
            {
                UXHelper.TampilkanError("Waduh sepuh! 🙏 Status mahasiswa sudah kadaluarsa, jadi nggak bisa daftar seller.");
            }
            else
            {
                UXHelper.TampilkanSukses("Request dikirim! Admin bakal gercep cek KTM kamu. Tunggu ya bestie! ✨");
            }
        }
    }
}

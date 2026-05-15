using System;
using System.Drawing;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Services;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.UI.Controls
{
    public partial class PreorderControl : UserControl
    {
        private int _idPenjual;

        public PreorderControl(int idPenjual)
        {
            InitializeComponent();
            _idPenjual = idPenjual;
            dtpBatasWaktu.MinDate = DateTime.Now.AddDays(1);
            dtpBatasWaktu.Value = DateTime.Now.AddDays(14);
        }

        // Dipanggil setiap kali ukuran kontrol berubah → card selalu di tengah
        private void PreorderControl_Resize(object sender, EventArgs e)
        {
            pnlCard.Location = new Point(
                (this.ClientSize.Width - pnlCard.Width) / 2,
                (this.ClientSize.Height - pnlCard.Height) / 2
            );
        }

        private void cmbJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isGotong = cmbJenis.Text == "Gotong Royong";
            lblTargetKuota.Visible = isGotong;
            txtTargetKuota.Visible = isGotong;
        }

        private void btnBuat_Click(object sender, EventArgs e)
        {
            string judul = txtJudulPO.Text.Trim();
            string jenis = cmbJenis.Text;
            string rekening = txtInfoRekening.Text.Trim();
            DateTime batas = dtpBatasWaktu.Value;
            int targetKuota = 0;

            if (string.IsNullOrWhiteSpace(judul))
            {
                UXHelper.TampilkanError("Judul PO jangan kosong ya bestie! 📝");
                return;
            }
            if (string.IsNullOrWhiteSpace(rekening))
            {
                UXHelper.TampilkanError("Info rekening wajib diisi buat transferan nanti~");
                return;
            }
            if (jenis == "Gotong Royong")
            {
                if (!int.TryParse(txtTargetKuota.Text, out targetKuota) || targetKuota < 1)
                {
                    UXHelper.TampilkanError("Target kuota harus angka > 0!");
                    return;
                }
            }

            var poService = new PreorderService();
            bool sukses = poService.BuatPO(_idPenjual, judul, jenis, rekening, batas, targetKuota);
            if (sukses)
            {
                UXHelper.TampilkanSukses("PO berhasil dibuat! 🎉 Sekarang tambahkan produkmu~");
                if (ParentForm is MainForm main)
                    main.GantiHalaman(new SellerPOListControl(_idPenjual));
            }
        }
    }
}
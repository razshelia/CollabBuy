using System.Windows.Forms;

namespace CollabBuy.CollabBuyApp.Helpers
{
    public static class UXHelper
    {
        public static void TampilkanError(string pesan)
        {
            MessageBox.Show(pesan, "CollabBuy - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void TampilkanSukses(string pesan)
        {
            MessageBox.Show(pesan, "CollabBuy - Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool TampilkanKonfirmasi(string pesan)
        {
            DialogResult hasil = MessageBox.Show(pesan, "CollabBuy - Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return hasil == DialogResult.Yes;
        }
    }
}
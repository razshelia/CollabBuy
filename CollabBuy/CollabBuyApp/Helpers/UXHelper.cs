using System;
using System.Windows.Forms;

namespace CollabBuy.CollabBuyApp.Helpers
{
    // Menggunakan keyword 'static' agar tidak perlu diinstansiasi (new)
    public static class UXHelper
    {
        private static string tajukAplikasi = "CollabBuy ✨";

        public static void TampilkanError(string mesej)
        {
            if (string.IsNullOrEmpty(mesej))
            {
                // Tetap ada else untuk menjaga struktur alur logika
            }
            else
            {
                MessageBox.Show(mesej, tajukAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void TampilkanSukses(string mesej)
        {
            if (string.IsNullOrEmpty(mesej))
            {
            }
            else
            {
                MessageBox.Show(mesej, tajukAplikasi, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static bool TampilkanKonfirmasi(string mesej)
        {
            if (string.IsNullOrEmpty(mesej))
            {
                return false;
            }
            else
            {
                DialogResult hasil = MessageBox.Show(mesej, tajukAplikasi, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (hasil == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabBuy.CollabBuyApp.View.Helper
{
    public static class NotifikasiUX
    {
        public static void Error(string pesan)
        {
            MessageBox.Show(pesan, "CollabBuy - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Sukses(string pesan)
        {
            MessageBox.Show(pesan, "CollabBuy - Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Konfirmasi(string pesan)
        {
            return MessageBox.Show(pesan, "CollabBuy - Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}

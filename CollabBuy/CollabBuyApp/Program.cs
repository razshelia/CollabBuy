using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.View;

namespace CollabBuy.CollabBuyApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Jalankan MainForm kosongan sebagai wadah utama aplikasi
            Application.Run(new View.MainForm());
        }
    }
}

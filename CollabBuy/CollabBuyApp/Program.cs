using CollabBuy.CollabBuyApp.View.Main;
using System;
using System.Windows.Forms;

namespace CollabBuy.CollabBuyApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // PERBAIKAN: Namespace MainForm diubah ke CollabBuy.CollabBuyApp.View.Main
            Application.Run(new MainForm());
        }
    }
}
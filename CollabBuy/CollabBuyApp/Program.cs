using CollabBuy.CollabBuyApp.UI;
using System;
using System.Windows.Forms;

namespace CollabBuy.CollabBuyApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
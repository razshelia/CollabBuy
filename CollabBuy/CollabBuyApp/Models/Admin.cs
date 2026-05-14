namespace CollabBuy.CollabBuyApp.Models
{
    public class Admin : Akun
    {
        private string kodeAkses;

        public string KodeAkses
        {
            get { return this.kodeAkses; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.kodeAkses = "DEFAULT_ADMIN";
                }
                else
                {
                    this.kodeAkses = value;
                }
            }
        }

        // OVERRIDE: Tampilan khusus Admin
        public override string TampilkanDashboard()
        {
            return "Dashboard Admin: Selamat bertugas memantau sistem CollabBuy.";
        }
    }
}
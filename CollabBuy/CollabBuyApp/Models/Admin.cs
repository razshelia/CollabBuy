using CollabBuy.CollabBuyApp.Exceptions;

namespace CollabBuy.CollabBuyApp.Models
{
    public class Admin : User
    {
        public Admin(string nama, string username, string password)
            : base(nama, username, password, "Admin")
        {
        }

        public override string GetTipeUser()
        {
            return "Administrator Sistem";
        }

        public string DapatkanNamaResmiMimin()
        {
            if (string.IsNullOrWhiteSpace(this.Nama))
                return "CS CollabBuy";

            return "[ADMIN] " + this.Nama;
        }
    }
}
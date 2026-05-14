namespace CollabBuy.CollabBuyApp.Models
{
    public class Admin : User
    {
        public Admin()
        {
            // Set peran = Admin
            this.Peran = "Admin";
        }

        public override string TampilkanDashboard()
        {
            return $"Admin Dashboard - Selamat datang, {this.Nama}";
        }

        // Method khusus admin
        public string BlokirUser(RegularUser user)
        {
            user.IsDiblokir = true;
            return $"User {user.Username} telah diblokir.";
        }
    }
}
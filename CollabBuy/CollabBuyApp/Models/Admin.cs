namespace CollabBuy.CollabBuyApp.Models
{
    public class Admin : User
    {
        public Admin() { this.Peran = "Admin"; }

        public override string TampilkanDashboard()
        {
            return $"Admin Dashboard - Selamat datang, {this.Nama}";
        }

        public string BlokirUser(RegularUser user)
        {
            if (user == null) return "User tidak ditemukan.";
            user.IsDiblokir = true;
            return $"User {user.Username} telah diblokir.";
        }
    }
}
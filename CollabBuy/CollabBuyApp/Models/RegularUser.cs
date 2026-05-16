namespace CollabBuy.CollabBuyApp.Models
{
    public class RegularUser : User
    {
        public RegularUser() { this.Peran = "User"; }

        public override string TampilkanDashboard()
        {
            return $"Katalog CollabBuy - Halo, {this.Nama}!";
        }
    }
}
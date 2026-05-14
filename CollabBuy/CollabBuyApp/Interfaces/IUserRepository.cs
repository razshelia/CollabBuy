using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Interfaces
{
    public interface IUserRepository
    {
        User Login(string username, string password);
        bool Register(User user);
        bool UpdateProfil(User user);
        bool BlokirUser(int idUser, bool diblokir);
        List<User> AmbilSemuaUser();
        User AmbilUserById(int idUser);
    }
}
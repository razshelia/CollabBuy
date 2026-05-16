using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public User Login(string username, string password)
        {
            User user = null;
            string sql = @"SELECT id_user, nama, nomor_telepon, email, username, password, peran, is_diblokir
                           FROM users WHERE username = @user";
            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("user", username), reader =>
            {
                string hashDb = reader.GetString(5);
                if (PasswordHelper.VerifyPassword(password, hashDb))
                {
                    user = MapUser(reader);
                }
            });

            return user; 
        }

        public bool Register(User user)
        {
            string cekSql = "SELECT COUNT(*) FROM users WHERE username = @user";
            long jumlah = Convert.ToInt64(ExecuteScalar(cekSql, cmd => cmd.Parameters.AddWithValue("user", user.Username)));

            if (jumlah > 0)
            {
                throw new Exception("Username sudah digunakan. Silakan pilih username lain.");
            }

            string sql = @"INSERT INTO users (nama, nomor_telepon, email, username, password, peran, is_diblokir)
                           VALUES (@nama, @telp, @email, @user, @pass, @peran, @blokir)";
            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("nama", user.Nama);
                cmd.Parameters.AddWithValue("telp", (object)user.NomorTelepon ?? DBNull.Value);
                cmd.Parameters.AddWithValue("email", user.Email);
                cmd.Parameters.AddWithValue("user", user.Username);
                cmd.Parameters.AddWithValue("pass", user.Password);
                cmd.Parameters.AddWithValue("peran", "User");
                cmd.Parameters.AddWithValue("blokir", false);
            });

            return row > 0;
        }

        public bool UpdateProfil(User user)
        {
            string sql = @"UPDATE users SET nama = @nama, nomor_telepon = @telp, email = @email, password = @pass
                           WHERE id_user = @id";

            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("nama", user.Nama);
                cmd.Parameters.AddWithValue("telp", (object)user.NomorTelepon ?? DBNull.Value);
                cmd.Parameters.AddWithValue("email", user.Email);
                cmd.Parameters.AddWithValue("pass", user.Password);
                cmd.Parameters.AddWithValue("id", user.IdUser);
            });

            return row > 0;
        }

        public bool BlokirUser(int idUser, bool diblokir)
        {
            string sql = "UPDATE users SET is_diblokir = @blokir WHERE id_user = @id";

            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("blokir", diblokir);
                cmd.Parameters.AddWithValue("id", idUser);
            });

            return row > 0;
        }

        public List<User> AmbilSemuaUser()
        {
            List<User> list = new List<User>();
            string sql = "SELECT id_user, nama, nomor_telepon, email, username, password, peran, is_diblokir FROM users";

            ExecuteQuery(sql, null, reader =>
            {
                list.Add(MapUser(reader));
            });

            return list;
        }

        public User AmbilUserById(int idUser)
        {
            User user = null;
            string sql = "SELECT id_user, nama, nomor_telepon, email, username, password, peran, is_diblokir FROM users WHERE id_user = @id";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idUser), reader =>
            {
                user = MapUser(reader);
            });

            return user;
        }

        private User MapUser(NpgsqlDataReader reader)
        {
            User user;
            string peran = reader.GetString(6);

            if (peran == "Admin")
                user = new Admin();
            else
                user = new RegularUser();

            user.IdUser = reader.GetInt32(0);
            user.Nama = reader.GetString(1);
            user.NomorTelepon = reader.IsDBNull(2) ? null : reader.GetString(2);
            user.Email = reader.GetString(3);
            user.Username = reader.GetString(4);
            user.Password = reader.GetString(5);
            user.IsDiblokir = reader.GetBoolean(7);

            return user;
        }
    }
}
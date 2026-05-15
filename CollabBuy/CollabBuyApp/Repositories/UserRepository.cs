using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseHelper _db;

        public UserRepository()
        {
            _db = new DatabaseHelper();
        }

        public User Login(string username, string password)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = @"SELECT id_user, nama, nomor_telepon, email, username, password, peran, is_diblokir
                               FROM users WHERE username = @user";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("user", username);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashDb = reader.GetString(5);
                            if (!PasswordHelper.VerifyPassword(password, hashDb))
                                return null;

                            string peran = reader.GetString(6);
                            bool diblokir = reader.GetBoolean(7);

                            User user;
                            if (peran == "Admin")
                                user = new Admin();
                            else
                                user = new RegularUser();

                            user.IdUser = reader.GetInt32(0);
                            user.Nama = reader.GetString(1);
                            user.NomorTelepon = reader.IsDBNull(2) ? null : reader.GetString(2);
                            user.Email = reader.GetString(3);
                            user.Username = reader.GetString(4);
                            user.Password = hashDb;
                            user.IsDiblokir = diblokir;

                            return user;
                        }
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Gagal melakukan proses login.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
            return null;
        }

        public bool Register(User user)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();

                // Cek username sudah ada
                string cekSql = "SELECT COUNT(*) FROM users WHERE username = @user";
                using (NpgsqlCommand cmdCek = new NpgsqlCommand(cekSql, conn))
                {
                    cmdCek.Parameters.AddWithValue("user", user.Username);
                    long jumlah = (long)cmdCek.ExecuteScalar();
                    if (jumlah > 0)
                    {
                        // Lempar error agar ditangkap oleh Service
                        throw new Exception("Username sudah digunakan. Silakan pilih username lain.");
                    }
                }

                string sql = @"INSERT INTO users (nama, nomor_telepon, email, username, password, peran, is_diblokir)
                               VALUES (@nama, @telp, @email, @user, @pass, @peran, @blokir)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("nama", user.Nama);
                    cmd.Parameters.AddWithValue("telp", (object)user.NomorTelepon ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("user", user.Username);
                    cmd.Parameters.AddWithValue("pass", user.Password);
                    cmd.Parameters.AddWithValue("peran", "User");
                    cmd.Parameters.AddWithValue("blokir", false);

                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); } // Menangkap Exception custom kita di atas atau error DB
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
        }

        public bool UpdateProfil(User user)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = @"UPDATE users SET nama = @nama, nomor_telepon = @telp, email = @email, password = @pass
                               WHERE id_user = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("nama", user.Nama);
                    cmd.Parameters.AddWithValue("telp", (object)user.NomorTelepon ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("pass", user.Password);
                    cmd.Parameters.AddWithValue("id", user.IdUser);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception ex) { throw new Exception("Gagal menyimpan update profil ke database.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
        }

        public bool BlokirUser(int idUser, bool diblokir)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = "UPDATE users SET is_diblokir = @blokir WHERE id_user = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("blokir", diblokir);
                    cmd.Parameters.AddWithValue("id", idUser);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex) { throw new Exception("Gagal memperbarui status blokir user.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
        }

        public List<User> AmbilSemuaUser()
        {
            List<User> list = new List<User>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = "SELECT id_user, nama, nomor_telepon, email, username, password, peran, is_diblokir FROM users";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user;
                        if (reader.GetString(6) == "Admin")
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
                        list.Add(user);
                    }
                }
            }
            catch (Exception ex) { throw new Exception("Gagal memuat daftar semua pengguna.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
            return list;
        }

        public User AmbilUserById(int idUser)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                string sql = "SELECT id_user, nama, nomor_telepon, email, username, password, peran, is_diblokir FROM users WHERE id_user = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idUser);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User user;
                            if (reader.GetString(6) == "Admin")
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
            }
            catch (Exception ex) { throw new Exception("Gagal mengambil data spesifik pengguna.", ex); }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
            return null;
        }
    }
}
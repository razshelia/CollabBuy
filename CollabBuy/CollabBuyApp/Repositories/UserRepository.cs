using System;
using Npgsql;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseHelper dbHelper;
        private PasswordHelper passHelper;

        public UserRepository()
        {
            this.dbHelper = new DatabaseHelper();
            this.passHelper = new PasswordHelper();
        }

        public Akun Login(string username, string passwordInput)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(passwordInput))
            {
                return null;
            }

            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null)
            {
                return null;
            }

            Akun akunDitemukan = null;
            try
            {
                koneksi.Open();
                string sql = "SELECT id_user, password, nama, peran FROM users WHERE username = @user AND is_diblokir = FALSE";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("user", username);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashTersimpan = reader.GetString(1);

                            if (this.passHelper.VerifikasiPassword(passwordInput, hashTersimpan))
                            {
                                string peran = reader.GetString(3);

                                if (peran == "Admin")
                                {
                                    Admin admin = new Admin();
                                    admin.IdUser = reader.GetInt32(0);
                                    admin.Username = username;
                                    akunDitemukan = admin;
                                }
                                else
                                {
                                    User pengguna = new User();
                                    pengguna.IdUser = reader.GetInt32(0);
                                    pengguna.Username = username;
                                    pengguna.NamaLengkap = reader.GetString(2);
                                    akunDitemukan = pengguna;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                akunDitemukan = null;
            }
            finally
            {
                if (koneksi.State == System.Data.ConnectionState.Open)
                {
                    koneksi.Close();
                }
            }

            return akunDitemukan;
        }

        public bool Register(User newUser)
        {
            if (newUser == null)
            {
                return false;
            }

            // ✅ FIX: Validasi Password tidak boleh kosong sebelum di-hash
            if (string.IsNullOrWhiteSpace(newUser.Password))
            {
                return false;
            }

            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null)
            {
                return false;
            }

            try
            {
                koneksi.Open();

                // ✅ FIX: Cek duplikat dulu dengan pesan yang jelas sebelum insert
                string sqlCek = "SELECT COUNT(*) FROM users WHERE username = @usr OR email = @email";
                using (NpgsqlCommand cmdCek = new NpgsqlCommand(sqlCek, koneksi))
                {
                    cmdCek.Parameters.AddWithValue("usr", newUser.Username);
                    cmdCek.Parameters.AddWithValue("email", newUser.Email ?? (object)DBNull.Value);
                    long jumlah = (long)cmdCek.ExecuteScalar();
                    if (jumlah > 0)
                    {
                        // Duplikat → kembalikan false, UserService akan tampilkan pesan
                        return false;
                    }
                }

                string hashPass = this.passHelper.HashPassword(newUser.Password);

                string sql = "INSERT INTO users (username, password, nama, email, nomor_telepon, peran, is_diblokir) VALUES (@usr, @pass, @nama, @email, @telp, 'User', FALSE)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("usr", newUser.Username);
                    cmd.Parameters.AddWithValue("pass", hashPass);
                    cmd.Parameters.AddWithValue("nama", newUser.NamaLengkap);
                    cmd.Parameters.AddWithValue("email", newUser.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("telp", string.IsNullOrWhiteSpace(newUser.NomorTelepon) ? (object)DBNull.Value : newUser.NomorTelepon);

                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (koneksi.State == System.Data.ConnectionState.Open)
                {
                    koneksi.Close();
                }
            }
        }

        public bool AjukanVerifikasiPenjual(Seller pengajuan)
        {
            if (pengajuan == null) return false;

            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return false;

            try
            {
                koneksi.Open();
                string sql = "INSERT INTO verifications (id_user, nama_toko, nim, tahun_masuk, bukti_mahasiswa, is_verifikasi) VALUES (@id, @toko, @nim, @tahun, @bukti, FALSE)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("id", pengajuan.IdUser);
                    cmd.Parameters.AddWithValue("toko", pengajuan.NamaToko);
                    cmd.Parameters.AddWithValue("nim", pengajuan.Nim);
                    cmd.Parameters.AddWithValue("tahun", pengajuan.TahunMasuk);
                    cmd.Parameters.AddWithValue("bukti", pengajuan.LinkFotoKtm);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (koneksi.State == System.Data.ConnectionState.Open) koneksi.Close();
            }
        }

        public System.Collections.Generic.List<User> AmbilSemuaUser()
        {
            var daftarUser = new System.Collections.Generic.List<User>();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return daftarUser;

            try
            {
                koneksi.Open();
                string sql = "SELECT id_user, username, nama, email, nomor_telepon, peran FROM users WHERE peran = 'User'";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User u = new User();
                            u.IdUser = reader.GetInt32(0);
                            u.Username = reader.GetString(1);
                            u.NamaLengkap = reader.GetString(2);
                            if (!reader.IsDBNull(3)) u.Email = reader.GetString(3);
                            if (!reader.IsDBNull(4)) u.NomorTelepon = reader.GetString(4);
                            daftarUser.Add(u);
                        }
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                if (koneksi.State == System.Data.ConnectionState.Open) koneksi.Close();
            }
            return daftarUser;
        }

        public bool BlokirAkun(int idUser)
        {
            if (idUser <= 0) return false;
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return false;

            try
            {
                koneksi.Open();
                string sql = "UPDATE users SET is_diblokir = TRUE WHERE id_user = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("id", idUser);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception) { return false; }
            finally
            {
                if (koneksi.State == System.Data.ConnectionState.Open) koneksi.Close();
            }
        }

        public System.Collections.Generic.List<Seller> AmbilDaftarPengajuanVerifikasi()
        {
            var daftar = new System.Collections.Generic.List<Seller>();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return daftar;

            try
            {
                koneksi.Open();
                string sql = @"SELECT v.id_verifikasi, u.nama, v.nama_toko, v.nim, v.tahun_masuk, v.bukti_mahasiswa 
                       FROM verifications v JOIN users u ON v.id_user = u.id_user 
                       WHERE v.is_verifikasi = FALSE";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Seller s = new Seller();
                            s.IdUser = reader.GetInt32(0);
                            s.NamaLengkap = reader.GetString(1);
                            s.NamaToko = reader.GetString(2);
                            s.Nim = reader.GetString(3);
                            s.TahunMasuk = reader.GetInt32(4);
                            s.LinkFotoKtm = reader.GetString(5);
                            daftar.Add(s);
                        }
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                if (koneksi.State == System.Data.ConnectionState.Open) koneksi.Close();
            }
            return daftar;
        }

        public bool SetujuiVerifikasi(int idVerifikasi)
        {
            if (idVerifikasi <= 0) return false;
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return false;

            try
            {
                koneksi.Open();
                string sql = "UPDATE verifications SET is_verifikasi = TRUE WHERE id_verifikasi = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("id", idVerifikasi);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception) { return false; }
            finally
            {
                if (koneksi.State == System.Data.ConnectionState.Open) koneksi.Close();
            }
        }
    }
}
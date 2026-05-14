using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using Npgsql;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseHelper dbHelper;

        public UserRepository()
        {
            this.dbHelper = new DatabaseHelper();
        }

        // Helper: Hash password dengan SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }

        // Login: cek username & password yang sudah di-hash
        public Akun Login(string username, string password)
        {
            NpgsqlConnection koneksi = dbHelper.AmbilKoneksi();
            if (koneksi == null)
            {
                UXHelper.TampilkanError("Tidak dapat terhubung ke database. Periksa koneksi Anda.");
                return null;
            }

            try
            {
                koneksi.Open();
                string sql = @"SELECT id_user, username, password, nama, email, role, is_verifikasi
                               FROM users WHERE username = @user";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("user", username);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string passHashDiDB = reader.GetString(2);
                            string passHashInput = HashPassword(password);

                            // Bandingkan hash, bukan plaintext
                            if (passHashDiDB == passHashInput)
                            {
                                string role = reader.GetString(5);
                                if (role == "admin")
                                    return new Admin
                                    {
                                        IdUser = reader.GetInt32(0),
                                        Username = username
                                    };
                                else
                                    return new User
                                    {
                                        IdUser = reader.GetInt32(0),
                                        Username = username,
                                        NamaLengkap = reader.GetString(3),
                                        Email = reader.GetString(4),
                                        IsVerifikasi = reader.GetBoolean(6)
                                    };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Error saat login: " + ex.Message);
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                    koneksi.Close();
            }
            return null;
        }

        // Register user baru (password di-hash sebelum disimpan)
        public bool Register(Akun akun)
        {
            NpgsqlConnection koneksi = dbHelper.AmbilKoneksi();
            if (koneksi == null)
            {
                UXHelper.TampilkanError("Tidak dapat terhubung ke database.");
                return false;
            }

            try
            {
                koneksi.Open();

                // Cek apakah username sudah ada
                string cekSql = "SELECT COUNT(*) FROM users WHERE username = @user";
                using (NpgsqlCommand cmdCek = new NpgsqlCommand(cekSql, koneksi))
                {
                    cmdCek.Parameters.AddWithValue("user", akun.Username);
                    long jumlah = (long)cmdCek.ExecuteScalar();
                    if (jumlah > 0)
                    {
                        UXHelper.TampilkanError("Username sudah digunakan. Silakan pilih username lain.");
                        return false;
                    }
                }

                string sql = @"INSERT INTO users (username, password, nama, email, role, is_verifikasi)
                               VALUES (@user, @pass, @nama, @email, 'user', false)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("user", akun.Username);
                    cmd.Parameters.AddWithValue("pass", HashPassword(akun.Password)); // SIMPAN HASH
                    cmd.Parameters.AddWithValue("nama", ((User)akun).NamaLengkap);
                    cmd.Parameters.AddWithValue("email", ((User)akun).Email);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal mendaftar: " + ex.Message);
                return false;
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                    koneksi.Close();
            }
        }

        // Ajukan verifikasi seller
        public bool AjukanVerifikasiSeller(int idUser, string namaToko, string nim, int tahunMasuk, string pathKTM)
        {
            NpgsqlConnection koneksi = dbHelper.AmbilKoneksi();
            if (koneksi == null) return false;
            try
            {
                koneksi.Open();
                string sql = @"INSERT INTO verifications (id_user, nama_toko, nim, tahun_masuk, ktm_path, status)
                               VALUES (@idUser, @toko, @nim, @tahun, @ktm, 'pending')";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("idUser", idUser);
                    cmd.Parameters.AddWithValue("toko", namaToko);
                    cmd.Parameters.AddWithValue("nim", nim);
                    cmd.Parameters.AddWithValue("tahun", tahunMasuk);
                    cmd.Parameters.AddWithValue("ktm", pathKTM);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception) { return false; }
            finally { if (koneksi.State == ConnectionState.Open) koneksi.Close(); }
        }

        // Ambil daftar pengajuan verifikasi yang statusnya 'pending'
        public List<dynamic> AmbilDaftarPengajuanVerifikasi()
        {
            var hasil = new List<dynamic>();
            NpgsqlConnection koneksi = dbHelper.AmbilKoneksi();
            if (koneksi == null) return hasil;

            try
            {
                koneksi.Open();
                string sql = @"
                    SELECT v.id_verifikasi, u.username, u.nama, u.email,
                           v.nama_toko, v.nim, v.tahun_masuk, v.status, v.created_at
                    FROM verifications v
                    JOIN users u ON v.id_user = u.id_user
                    WHERE v.status = 'pending'
                    ORDER BY v.created_at ASC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hasil.Add(new
                        {
                            IdVerifikasi = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            NamaLengkap = reader.GetString(2),
                            Email = reader.GetString(3),
                            NamaToko = reader.GetString(4),
                            Nim = reader.GetString(5),
                            TahunMasuk = reader.GetInt32(6),
                            Status = reader.GetString(7),
                            TanggalPengajuan = reader.GetDateTime(8)
                        });
                    }
                }
            }
            catch (Exception) { }
            finally { if (koneksi.State == ConnectionState.Open) koneksi.Close(); }

            return hasil;
        }

        // Setujui verifikasi
        public bool SetujuiVerifikasi(int idVerifikasi)
        {
            NpgsqlConnection koneksi = dbHelper.AmbilKoneksi();
            if (koneksi == null) return false;
            try
            {
                koneksi.Open();
                string sql1 = "UPDATE verifications SET status = 'disetujui' WHERE id_verifikasi = @id";
                string sql2 = @"UPDATE users SET is_verifikasi = true
                                FROM verifications v
                                WHERE users.id_user = v.id_user AND v.id_verifikasi = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql1 + ";" + sql2, koneksi))
                {
                    cmd.Parameters.AddWithValue("id", idVerifikasi);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception) { return false; }
            finally { if (koneksi.State == ConnectionState.Open) koneksi.Close(); }
        }

        // Tolak verifikasi
        public bool TolakVerifikasi(int idVerifikasi)
        {
            NpgsqlConnection koneksi = dbHelper.AmbilKoneksi();
            if (koneksi == null) return false;
            try
            {
                koneksi.Open();
                string sql = "UPDATE verifications SET status = 'ditolak' WHERE id_verifikasi = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("id", idVerifikasi);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception) { return false; }
            finally { if (koneksi.State == ConnectionState.Open) koneksi.Close(); }
        }

        public bool UpdateProfil(Akun akun)
        {
            // Implementasi update profil sesuai kebutuhan
            throw new NotImplementedException();
        }
    }
}
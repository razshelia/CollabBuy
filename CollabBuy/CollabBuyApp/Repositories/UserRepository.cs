using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace CollabBuy.CollabBuyApp.Repositories
{
    /// <summary>
    /// Repository untuk mengakses data User.
    /// Mengimplementasikan IQueryRepository dan ICommandRepository dengan Strict OOP.
    /// </summary>
    public class UserRepository : IQueryRepository<User>, IQueryAllRepository<User>, ICommandRepository<User>
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
        }


        // =======================================================
        // IMPLEMENTASI IQueryRepository<User>
        // =======================================================

        public User GetById(int idUser)
        {
            User userObj = null;
            string query = "SELECT * FROM fn_get_user_lengkap_by_id(@id);";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUser);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) userObj = MappingReaderToUser(reader);
                    }
                }
            }
            return userObj;
        }

        public List<User> GetAll()
        {
            var listUser = new List<User>();
            string query = "SELECT id_user, nama, username, password, peran, is_diblokir FROM users ORDER BY nama;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string peran = reader.GetString(reader.GetOrdinal("peran"));
                        string nama = reader.GetString(reader.GetOrdinal("nama"));
                        string username = reader.GetString(reader.GetOrdinal("username"));
                        string password = reader.GetString(reader.GetOrdinal("password"));

                        // Polimorfisme: Instansiasi sesuai peran agar tidak crash
                        User user = peran == "Penjual" ? new Penjual(nama, username, password)
                                  : peran == "Admin" ? (User)new Admin(nama, username, password)
                                  : new Pembeli(nama, username, password);

                        user.IdUser = reader.GetInt32(reader.GetOrdinal("id_user"));
                        user.Peran = peran;
                        if (!reader.IsDBNull(reader.GetOrdinal("is_diblokir")) && reader.GetBoolean(reader.GetOrdinal("is_diblokir")))
                            user.Blokir("Diblokir oleh Admin");
                        listUser.Add(user);
                    }
                }
            }
            return listUser;
        }


        // =======================================================
        // IMPLEMENTASI ICommandRepository<User>
        // =======================================================

        public void Insert(User entity)
        {
            if (entity == null) throw new ArgumentNullException("entity", "Entity user tidak boleh null.");

            Penjual penjual = entity as Penjual;
            if (penjual != null)
            {
                InsertPenjualWithVerification(penjual);
                return;
            }

            string query = "INSERT INTO users (nama, nomor_telepon, email, username, password, peran) VALUES (@nama, @telp, @email, @uname, @pass, @peran);";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    MappingUserToParameters(cmd, entity);
                    cmd.Parameters.AddWithValue("@peran", entity.Peran);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOrderException("Gagal menyimpan user baru.", "", "DB_INSERT_USER_FAILED");
                }
            }
        }

        public void Update(User entity)
        {
            if (entity == null) throw new ArgumentNullException("entity", "Entity user tidak boleh null.");

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        string queryUser = @"UPDATE users 
                                     SET nama = @nama, email = @email, nomor_telepon = @telp, 
                                         password = @pass, is_diblokir = @isBlokir,
                                         username = @uname
                                     WHERE id_user = @id;";

                        using (var cmd = new NpgsqlCommand(queryUser, conn, dbTx))
                        {
                            cmd.Parameters.AddWithValue("@id", entity.IdUser);
                            cmd.Parameters.AddWithValue("@nama", entity.Nama);
                            cmd.Parameters.AddWithValue("@uname", entity.Username);
                            cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(entity.Email) ? (object)DBNull.Value : entity.Email);
                            cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(entity.NomorTelepon) ? (object)DBNull.Value : entity.NomorTelepon);
                            cmd.Parameters.AddWithValue("@pass", entity.Password);
                            cmd.Parameters.AddWithValue("@isBlokir", entity.IsDiblokir);
                            cmd.ExecuteNonQuery();
                        }

                        Penjual penjual = entity as Penjual;
                        if (penjual != null)
                        {
                            string queryVerif = "UPDATE verifications SET is_verifikasi = @isVerif WHERE id_user = @idUser;";
                            using (var cmdVerif = new NpgsqlCommand(queryVerif, conn, dbTx))
                            {
                                cmdVerif.Parameters.AddWithValue("@idUser", penjual.IdUser);
                                cmdVerif.Parameters.AddWithValue("@isVerif", penjual.GetStatusPersetujuan());
                                cmdVerif.ExecuteNonQuery();
                            }
                        }

                        dbTx.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTx.Rollback();
                        throw new InvalidOrderException("Update user dibatalkan: " + ex.Message, "", "DB_UPDATE_USER_FAILED", ex);
                    }
                }
            }
        }
        /// <summary>
        /// Mengecek apakah username tersedia untuk dipakai user lain
        /// (mengecualikan user dengan id yang sama — untuk update profil).
        /// </summary>
        public bool IsUsernameAvailable(int idUserSaatIni, string username)
        {
            string query = "SELECT COUNT(*) FROM users WHERE username = @uname AND id_user <> @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uname", username.Trim());
                    cmd.Parameters.AddWithValue("@id", idUserSaatIni);
                    long count = Convert.ToInt64(cmd.ExecuteScalar());
                    return count == 0;
                }
            }
        }


        // =======================================================
        // TAMBAHAN UNTUK FITUR DAFTAR TOKO & MANAJEMEN ADMIN
        // =======================================================

        public bool CheckPendingVerification(int idUser)
        {
            string query = "SELECT is_verifikasi FROM verifications WHERE id_user = @id";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUser);
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value && !Convert.ToBoolean(result);
                }
            }
        }

        public void AjukanLapakBaru(int idUser, string nim, string namaToko, int tahunMasuk, byte[] buktiKtm)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        string queryVerif = "INSERT INTO verifications (id_user, nim, nama_toko, tahun_masuk, bukti_ktm) VALUES (@id, @nim, @toko, @tahun, @ktm);";
                        using (var cmd = new NpgsqlCommand(queryVerif, conn, dbTx))
                        {
                            cmd.Parameters.AddWithValue("@id", idUser);
                            cmd.Parameters.AddWithValue("@nim", nim);
                            cmd.Parameters.AddWithValue("@toko", namaToko);
                            cmd.Parameters.AddWithValue("@tahun", tahunMasuk);
                            cmd.Parameters.AddWithValue("@ktm", buktiKtm ?? new byte[0]);
                            cmd.ExecuteNonQuery();
                        }
                        dbTx.Commit();
                    }
                    catch
                    {
                        dbTx.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable GetPendingVerifikasi()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM vw_verifikasi_pending;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetSemuaUser()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM vw_semua_user;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }

        public void ToggleBlokirUser(int idUser, bool blokir)
        {
            string query = "UPDATE users SET is_diblokir = @blokir WHERE id_user = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@blokir", blokir);
                    cmd.Parameters.AddWithValue("@id", idUser);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("User tidak ditemukan atau gagal diupdate.");
                }
            }
        }


        // =======================================================
        // METHOD KHUSUS STORED PROCEDURE
        // =======================================================

        public void TindakPenjualNakal(int idAduan, int idPenjual, string balasan)
        {
            string query = "CALL sp_tindak_penjual_nakal(@idAduan, @idPenjual, @balasan);";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idAduan", idAduan);
                    cmd.Parameters.AddWithValue("@idPenjual", idPenjual);
                    cmd.Parameters.AddWithValue("@balasan", balasan);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        // =======================================================
        // HELPER METHODS (DRY)
        // =======================================================

        private User MappingReaderToUser(NpgsqlDataReader reader)
        {
            string peranDb = reader.GetString(reader.GetOrdinal("peran"));
            string nama = reader.GetString(reader.GetOrdinal("nama"));
            string username = reader.GetString(reader.GetOrdinal("username"));
            string password = reader.GetString(reader.GetOrdinal("password"));
            bool sudahVerifikasiPenjual = HasColumn(reader, "is_verifikasi")
                && !reader.IsDBNull(reader.GetOrdinal("is_verifikasi"))
                && reader.GetBoolean(reader.GetOrdinal("is_verifikasi"));

            User userObj;

            if (peranDb.ToLower() == "admin")
            {
                userObj = new Admin(nama, username, password);
                userObj.Peran = "Admin";
            }
            else if (sudahVerifikasiPenjual)
            {
                Penjual penjual = new Penjual(nama, username, password);

                if(HasColumn(reader, "nim") && !reader.IsDBNull(reader.GetOrdinal("nim")))
{
                    penjual.Nim = reader.GetString(reader.GetOrdinal("nim"));
                }
                if (HasColumn(reader, "nama_toko") && !reader.IsDBNull(reader.GetOrdinal("nama_toko")))
                {
                    penjual.NamaToko = reader.GetString(reader.GetOrdinal("nama_toko"));
                }
                if (HasColumn(reader, "tahun_masuk") && !reader.IsDBNull(reader.GetOrdinal("tahun_masuk")))
                {
                    penjual.TahunMasuk = reader.GetInt32(reader.GetOrdinal("tahun_masuk"));
                }
                penjual.Approve();
                if (HasColumn(reader, "bukti_ktm") && !reader.IsDBNull(reader.GetOrdinal("bukti_ktm")))
                {
                    byte[] ktmBytes = (byte[])reader["bukti_ktm"];
                    if (ktmBytes != null && ktmBytes.Length > 0)
                    {
                        penjual.BuktiKtm = ktmBytes;
                    }
                }

                try { penjual.Peran = "Penjual"; } catch { }
                userObj = penjual;
            }
            else
            {
                userObj = new Pembeli(nama, username, password);
            }

            userObj.IdUser = reader.GetInt32(reader.GetOrdinal("id_user"));
            if (HasColumn(reader, "nomor_telepon") && !reader.IsDBNull(reader.GetOrdinal("nomor_telepon")))
                userObj.NomorTelepon = reader.GetString(reader.GetOrdinal("nomor_telepon"));
            if (HasColumn(reader, "email") && !reader.IsDBNull(reader.GetOrdinal("email")))
                userObj.Email = reader.GetString(reader.GetOrdinal("email"));
            if (HasColumn(reader, "is_diblokir") && !reader.IsDBNull(reader.GetOrdinal("is_diblokir")) && reader.GetBoolean(reader.GetOrdinal("is_diblokir")))
                userObj.Blokir("Diblokir oleh Admin");

            return userObj;
        }

        private void MappingUserToParameters(NpgsqlCommand cmd, User entity)
        {
            cmd.Parameters.AddWithValue("@nama", entity.Nama);
            cmd.Parameters.AddWithValue("@uname", entity.Username);
            cmd.Parameters.AddWithValue("@pass", entity.Password);
            cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(entity.NomorTelepon) ? (object)DBNull.Value : entity.NomorTelepon);
            cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(entity.Email) ? (object)DBNull.Value : entity.Email);
        }

        private bool HasColumn(NpgsqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private void InsertPenjualWithVerification(Penjual penjual)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        string queryUser = "INSERT INTO users (nama, nomor_telepon, email, username, password, peran) VALUES (@nama, @telp, @email, @uname, @pass, @peran) RETURNING id_user;";

                        using (var cmdUser = new NpgsqlCommand(queryUser, conn, dbTx))
                        {
                            MappingUserToParameters(cmdUser, penjual);
                            cmdUser.Parameters.AddWithValue("@peran", "Penjual");

                            object result = cmdUser.ExecuteScalar();
                            if (result == null || result == DBNull.Value)
                            {
                                throw new InvalidOrderException("Gagal mendapatkan ID User baru.", "", "DB_INSERT_USER_FAILED");
                            }
                            penjual.IdUser = Convert.ToInt32(result);
                        }

                        string queryVerif = "INSERT INTO verifications (id_user, nim, nama_toko, bukti_ktm, tahun_masuk) VALUES (@idUser, @nim, @toko, @ktm, @tahun);";

                        using (var cmdVerif = new NpgsqlCommand(queryVerif, conn, dbTx))
                        {
                            cmdVerif.Parameters.AddWithValue("@idUser", penjual.IdUser);
                            cmdVerif.Parameters.AddWithValue("@nim", penjual.Nim);
                            cmdVerif.Parameters.AddWithValue("@toko", penjual.NamaToko);
                            cmdVerif.Parameters.AddWithValue("@tahun", penjual.TahunMasuk);
                            cmdVerif.Parameters.AddWithValue("@ktm", (object)penjual.BuktiKtm ?? DBNull.Value);

                            if (cmdVerif.ExecuteNonQuery() == 0)
                            {
                                throw new InvalidOrderException("Gagal menyimpan data verifikasi penjual.", "", "DB_INSERT_VERIF_FAILED");
                            }
                        }
                        dbTx.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTx.Rollback();
                        throw new InvalidOrderException("Registrasi penjual dibatalkan (Rollback): " + ex.Message, "", "DB_TX_PENJUAL_FAILED", ex);
                    }
                }
            }
        }
        /// <summary>
        /// Mengambil user berdasarkan username untuk kebutuhan login (lebih efisien dari GetAll).
        /// </summary>
        public User GetByUsername(string username)
        {
            User userObj = null;
            string query = "SELECT * FROM fn_get_user_lengkap_by_username(@username);";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username.Trim());
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) userObj = MappingReaderToUser(reader);
                    }
                }
            }
            return userObj;
        }

        public int? VerifikasiIdentitasUser(string username, string email, string nomorTelepon)
        {
            string query = @"SELECT id_user FROM users
                             WHERE username       = @user
                               AND email          = @email
                               AND nomor_telepon  = @telepon
                               AND is_diblokir    = FALSE;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@telepon", nomorTelepon);
                    object result = cmd.ExecuteScalar();
                    return result != null ? (int?)Convert.ToInt32(result) : null;
                }
            }
        }

        public bool ResetPasswordUser(int idUser, string passwordHashBaru)
        {
            string query = "UPDATE users SET password = @hash WHERE id_user = @id;";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@hash", passwordHashBaru);
                    cmd.Parameters.AddWithValue("@id", idUser);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public int? GetIdPenjualByNamaToko(string namaToko)
        {
            string query = @"
        SELECT u.id_user 
        FROM users u
        JOIN verifications v ON u.id_user = v.id_user
        WHERE LOWER(v.nama_toko) = LOWER(@namaToko)
          AND v.is_verifikasi = TRUE
        LIMIT 1;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@namaToko", namaToko.Trim());
                    var result = cmd.ExecuteScalar();
                    return result != null ? (int?)Convert.ToInt32(result) : null;
                }
            }
        }
    }
}

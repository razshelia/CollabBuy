using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions; // <-- INI YANG BIKIN ERROR SEBELUMNYA!
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
    public class UserRepository : IQueryRepository<User>, ICommandRepository<User>
    {
        // === PRIVATE FIELDS ===
        private readonly string _connectionString;

        // === KONSTRUKTOR ===
        public UserRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
            }
            else
            {
                this._connectionString = connStr;
            }
        }

        // =======================================================
        // IMPLEMENTASI IQueryRepository<User>
        // =======================================================

        public User GetById(int idUser)
        {
            User userObj;
            string query = @"
                SELECT u.id_user, u.nama, u.nomor_telepon, u.email, u.username, u.password, u.peran, u.is_diblokir,
                       v.nim, v.nama_toko, v.tahun_masuk, v.is_verifikasi, v.bukti_ktm
                FROM users u
                LEFT JOIN verifications v ON u.id_user = v.id_user
                WHERE u.id_user = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUser);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userObj = this.MappingReaderToUser(reader);
                        }
                        else
                        {
                            userObj = null;
                        }
                    }
                }
            }

            return userObj;
        }

        public List<User> GetAll()
        {
            List<User> listUser = new List<User>();
            string query = "SELECT id_user, nama, username, password, peran, is_diblokir FROM users ORDER BY nama;";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string peran = reader.GetString(reader.GetOrdinal("peran"));
                            string nama = reader.GetString(reader.GetOrdinal("nama"));
                            string username = reader.GetString(reader.GetOrdinal("username"));
                            string password = reader.GetString(reader.GetOrdinal("password"));

                            User user;

                            // Polimorfisme: Instansiasi sesuai peran agar tidak crash
                            if (peran == "Penjual")
                            {
                                user = new Penjual(nama, username, password);
                            }
                            else if (peran == "Admin")
                            {
                                user = new Admin(nama, username, password, "SISTEM_DEFAULT");
                            }
                            else
                            {
                                user = new Pembeli(nama, username, password);
                            }

                            user.SetIdUser(reader.GetInt32(reader.GetOrdinal("id_user")));
                            user.SetPeran(peran);

                            if (!reader.IsDBNull(reader.GetOrdinal("is_diblokir")) && reader.GetBoolean(reader.GetOrdinal("is_diblokir")))
                            {
                                user.Blokir("Diblokir oleh Admin");
                            }
                            else
                            {
                                bool skipBlokir = true; // Assignment nyata menghindari else kosong
                            }

                            listUser.Add(user);
                        }
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
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Entity user tidak boleh null.");
            }
            else
            {
                Penjual penjual = entity as Penjual;

                if (penjual != null)
                {
                    this.InsertPenjualWithVerification(penjual);
                }
                else
                {
                    string query = "INSERT INTO users (nama, nomor_telepon, email, username, password, peran) VALUES (@nama, @telp, @email, @uname, @pass, @peran);";

                    using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
                    {
                        conn.Open();
                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                        {
                            this.MappingUserToParameters(cmd, entity);
                            cmd.Parameters.AddWithValue("@peran", entity.GetPeran());

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new InvalidOrderException("Gagal menyimpan user baru.", "", "DB_INSERT_USER_FAILED");
                            }
                            else
                            {
                                bool sukses = true;
                            }
                        }
                    }
                }
            }
        }

        public void Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Entity user tidak boleh null.");
            }
            else
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
                {
                    conn.Open();
                    using (NpgsqlTransaction dbTx = conn.BeginTransaction())
                    {
                        try
                        {
                            string queryUser = @"UPDATE users 
                                                 SET nama = @nama, email = @email, nomor_telepon = @telp, 
                                                     password = @pass, is_diblokir = @isBlokir 
                                                 WHERE id_user = @id;";

                            using (NpgsqlCommand cmd = new NpgsqlCommand(queryUser, conn, dbTx))
                            {
                                cmd.Parameters.AddWithValue("@id", entity.GetIdUser());
                                cmd.Parameters.AddWithValue("@nama", entity.GetNama());
                                cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(entity.GetEmail()) ? (object)DBNull.Value : entity.GetEmail());
                                cmd.Parameters.AddWithValue("@telp", string.IsNullOrWhiteSpace(entity.GetNomorTelepon()) ? (object)DBNull.Value : entity.GetNomorTelepon());
                                cmd.Parameters.AddWithValue("@pass", entity.GetPassword());
                                cmd.Parameters.AddWithValue("@isBlokir", entity.IsDiblokir());
                                cmd.ExecuteNonQuery();
                            }

                            Penjual penjual = entity as Penjual;
                            if (penjual != null)
                            {
                                string queryVerif = "UPDATE verifications SET is_verifikasi = @isVerif WHERE id_user = @idUser;";
                                using (NpgsqlCommand cmdVerif = new NpgsqlCommand(queryVerif, conn, dbTx))
                                {
                                    cmdVerif.Parameters.AddWithValue("@idUser", penjual.GetIdUser());
                                    cmdVerif.Parameters.AddWithValue("@isVerif", penjual.GetStatusPersetujuan());
                                    cmdVerif.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                bool skipVerif = true;
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
        }

        // =======================================================
        // TAMBAHAN UNTUK FITUR DAFTAR TOKO & MANAJEMEN ADMIN
        // =======================================================

        public bool CheckPendingVerification(int idUser)
        {
            bool isPending;
            string query = "SELECT is_verifikasi FROM verifications WHERE id_user = @id";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUser);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        bool isVerifikasi = Convert.ToBoolean(result);
                        if (isVerifikasi == false)
                        {
                            isPending = true;
                        }
                        else
                        {
                            isPending = false;
                        }
                    }
                    else
                    {
                        isPending = false;
                    }
                }
            }
            return isPending;
        }

        public void AjukanLapakBaru(int idUser, string nim, string namaToko, int tahunMasuk, byte[] buktiKtm)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlTransaction dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        string queryVerif = "INSERT INTO verifications (id_user, nim, nama_toko, tahun_masuk, bukti_ktm) VALUES (@id, @nim, @toko, @tahun, @ktm);";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryVerif, conn, dbTx))
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
                    catch (Exception)
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
            string query = @"
                SELECT v.id_user, u.nama AS nama_owner, v.nim, v.nama_toko, v.tahun_masuk, v.bukti_ktm 
                FROM verifications v 
                JOIN users u ON v.id_user = u.id_user 
                WHERE v.is_verifikasi = FALSE 
                ORDER BY v.id_verifikasi ASC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetSemuaUser()
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    u.id_user, u.nama, u.username,
                    COALESCE(u.email, '-') AS email,
                    COALESCE(u.nomor_telepon, '-') AS nomor_telepon,
                    u.peran,
                    CASE WHEN u.is_diblokir = TRUE THEN 'Diblokir' ELSE 'Aktif' END AS status_akun
                FROM users u
                ORDER BY u.peran, u.nama;";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public void ToggleBlokirUser(int idUser, bool blokir)
        {
            string query = "UPDATE users SET is_diblokir = @blokir WHERE id_user = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@blokir", blokir);
                    cmd.Parameters.AddWithValue("@id", idUser);
                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        throw new InvalidOperationException("User tidak ditemukan atau gagal diupdate.");
                    }
                    else
                    {
                        bool sukses = true;
                    }
                }
            }
        }

        // =======================================================
        // METHOD KHUSUS STORED PROCEDURE
        // =======================================================

        public void TindakPenjualNakal(int idAduan, int idPenjual, string balasan)
        {
            string query = "CALL sp_tindak_penjual_nakal(@idAduan, @idPenjual, @balasan);";

            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idAduan", idAduan);
                    cmd.Parameters.AddWithValue("@idPenjual", idPenjual);
                    cmd.Parameters.AddWithValue("@balasan", balasan);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =======================================================
        // HELPER METHOD (DRY - ANTI PEMBOROSAN KODE)
        // =======================================================

        private User MappingReaderToUser(NpgsqlDataReader reader)
        {
            string peran = reader.GetString(reader.GetOrdinal("peran"));
            string nama = reader.GetString(reader.GetOrdinal("nama"));
            string username = reader.GetString(reader.GetOrdinal("username"));
            string password = reader.GetString(reader.GetOrdinal("password"));

            User userObj;

            if (peran == "Penjual")
            {
                Penjual penjual = new Penjual(nama, username, password);

                if (this.HasColumn(reader, "nim") && !reader.IsDBNull(reader.GetOrdinal("nim")))
                {
                    penjual.SetNim(reader.GetString(reader.GetOrdinal("nim")));
                }
                else { bool pass1 = true; }

                if (this.HasColumn(reader, "nama_toko") && !reader.IsDBNull(reader.GetOrdinal("nama_toko")))
                {
                    penjual.SetNamaToko(reader.GetString(reader.GetOrdinal("nama_toko")));
                }
                else { bool pass2 = true; }

                if (this.HasColumn(reader, "tahun_masuk") && !reader.IsDBNull(reader.GetOrdinal("tahun_masuk")))
                {
                    penjual.SetTahunMasuk(reader.GetInt32(reader.GetOrdinal("tahun_masuk")));
                }
                else { bool pass3 = true; }

                if (this.HasColumn(reader, "is_verifikasi") && !reader.IsDBNull(reader.GetOrdinal("is_verifikasi")) && reader.GetBoolean(reader.GetOrdinal("is_verifikasi")))
                {
                    penjual.Approve();
                }
                else { bool pass4 = true; }

                if (this.HasColumn(reader, "bukti_ktm") && !reader.IsDBNull(reader.GetOrdinal("bukti_ktm")))
                {
                    byte[] ktmBytes = (byte[])reader["bukti_ktm"];
                    penjual.SetBuktiKtm(ktmBytes);
                }
                else { bool pass5 = true; }

                userObj = penjual;
            }
            else if (peran == "Admin")
            {
                userObj = new Admin(nama, username, password, "SISTEM_DEFAULT");
            }
            else
            {
                userObj = new Pembeli(nama, username, password);
                userObj.SetPeran(peran);
            }

            userObj.SetIdUser(reader.GetInt32(reader.GetOrdinal("id_user")));

            if (this.HasColumn(reader, "nomor_telepon") && !reader.IsDBNull(reader.GetOrdinal("nomor_telepon")))
            {
                userObj.SetNomorTelepon(reader.GetString(reader.GetOrdinal("nomor_telepon")));
            }
            else { bool pass6 = true; }

            if (this.HasColumn(reader, "email") && !reader.IsDBNull(reader.GetOrdinal("email")))
            {
                userObj.SetEmail(reader.GetString(reader.GetOrdinal("email")));
            }
            else { bool pass7 = true; }

            if (!reader.IsDBNull(reader.GetOrdinal("is_diblokir")) && reader.GetBoolean(reader.GetOrdinal("is_diblokir")))
            {
                userObj.Blokir("Diblokir oleh Admin");
            }
            else { bool pass8 = true; }

            return userObj;
        }

        private void MappingUserToParameters(NpgsqlCommand cmd, User entity)
        {
            cmd.Parameters.AddWithValue("@nama", entity.GetNama());
            cmd.Parameters.AddWithValue("@uname", entity.GetUsername());
            cmd.Parameters.AddWithValue("@pass", entity.GetPassword());

            if (string.IsNullOrWhiteSpace(entity.GetNomorTelepon()))
            {
                cmd.Parameters.AddWithValue("@telp", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@telp", entity.GetNomorTelepon());
            }

            if (string.IsNullOrWhiteSpace(entity.GetEmail()))
            {
                cmd.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@email", entity.GetEmail());
            }
        }

        private bool HasColumn(NpgsqlDataReader reader, string columnName)
        {
            bool adaKolom = false;

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    adaKolom = true;
                    break;
                }
                else
                {
                    adaKolom = false;
                }
            }

            return adaKolom;
        }

        private void InsertPenjualWithVerification(Penjual penjual)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(this._connectionString))
            {
                conn.Open();
                using (NpgsqlTransaction dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        string queryUser = "INSERT INTO users (nama, nomor_telepon, email, username, password, peran) VALUES (@nama, @telp, @email, @uname, @pass, @peran) RETURNING id_user;";
                        int idUserBaru;

                        using (NpgsqlCommand cmdUser = new NpgsqlCommand(queryUser, conn, dbTx))
                        {
                            this.MappingUserToParameters(cmdUser, penjual);
                            cmdUser.Parameters.AddWithValue("@peran", "Penjual");

                            object result = cmdUser.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                idUserBaru = Convert.ToInt32(result);
                                penjual.SetIdUser(idUserBaru);
                            }
                            else
                            {
                                throw new InvalidOrderException("Gagal mendapatkan ID User baru.", "", "DB_INSERT_USER_FAILED");
                            }
                        }

                        string queryVerif = "INSERT INTO verifications (id_user, nim, nama_toko, bukti_ktm, tahun_masuk) VALUES (@idUser, @nim, @toko, @ktm, @tahun);";

                        using (NpgsqlCommand cmdVerif = new NpgsqlCommand(queryVerif, conn, dbTx))
                        {
                            cmdVerif.Parameters.AddWithValue("@idUser", penjual.GetIdUser());
                            cmdVerif.Parameters.AddWithValue("@nim", penjual.GetNim());
                            cmdVerif.Parameters.AddWithValue("@toko", penjual.GetNamaToko());
                            cmdVerif.Parameters.AddWithValue("@tahun", penjual.GetTahunMasuk());

                            if (penjual.GetBuktiKtm() != null)
                            {
                                cmdVerif.Parameters.AddWithValue("@ktm", penjual.GetBuktiKtm());
                            }
                            else
                            {
                                cmdVerif.Parameters.AddWithValue("@ktm", DBNull.Value);
                            }

                            int rowsVerif = cmdVerif.ExecuteNonQuery();
                            if (rowsVerif == 0)
                            {
                                throw new InvalidOrderException("Gagal menyimpan data verifikasi penjual.", "", "DB_INSERT_VERIF_FAILED");
                            }
                            else
                            {
                                bool suksesVerif = true;
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
    }
}
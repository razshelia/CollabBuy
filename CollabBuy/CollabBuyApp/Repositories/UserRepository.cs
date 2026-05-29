using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CollabBuy.CollabBuyApp.Repositories
{
    /// <summary>
    /// Repository untuk mengakses data User.
    /// Mengimplementasikan IQueryRepository dan ICommandRepository.
    /// 
    /// PERBAIKAN OOP:
    /// - Update() kini menangani kedua tabel (users + verifications) untuk objek Penjual.
    /// - ArgumentNullException menggunakan overload dengan paramName agar konsisten.
    /// - MappingReaderToUser menggunakan helper IsColumnExists untuk menghindari
    ///   InvalidCastException saat query tidak menyertakan kolom verifikasi.
    /// </summary>
    public class UserRepository : IQueryRepository<User>, ICommandRepository<User>
    {
        // === PRIVATE FIELDS ===
        private readonly string _connectionString;

        // === KONSTRUKTOR ===
        public UserRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr))
            {
                throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
            }
            _connectionString = connStr;
        }


        // =======================================================
        // IMPLEMENTASI IQueryRepository<User>
        // =======================================================

        public User GetById(int idUser)
        {
            User user = null;

            string query = @"
                SELECT u.id_user, u.nama, u.nomor_telepon, u.email, u.username, u.password, u.peran, u.is_diblokir,
                       v.nim, v.nama_toko, v.tahun_masuk, v.is_verifikasi, v.bukti_ktm
                FROM users u
                LEFT JOIN verifications v ON u.id_user = v.id_user
                WHERE u.id_user = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUser);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = MappingReaderToUser(reader);
                        }
                    }
                }
            }
            return user;
        }

        public List<User> GetAll()
        {
            List<User> listUser = new List<User>();
            string query = "SELECT id_user, nama, username, password, peran, is_diblokir FROM users ORDER BY nama;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
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
                            if (peran == "Penjual") { user = new Penjual(nama, username, password); }
                            else { user = new Pembeli(nama, username, password); }

                            user.SetIdUser(reader.GetInt32(reader.GetOrdinal("id_user")));
                            if (!reader.IsDBNull(reader.GetOrdinal("is_diblokir")) && reader.GetBoolean(reader.GetOrdinal("is_diblokir")))
                            {
                                user.Blokir("Diblokir oleh Admin");
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
            if (entity == null) throw new ArgumentNullException("entity", "Entity user tidak boleh null.");

            // Jika entity adalah Penjual, gunakan Transaction karena harus insert 2 tabel
            Penjual penjual = entity as Penjual;
            if (penjual != null)
            {
                InsertPenjualWithVerification(penjual);
                return;
            }

            string query = "INSERT INTO users (nama, nomor_telepon, email, username, password, peran) VALUES (@nama, @telp, @email, @uname, @pass, @peran);";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    MappingUserToParameters(cmd, entity);
                    cmd.Parameters.AddWithValue("@peran", entity.GetPeran());

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0) throw new InvalidOrderException("Gagal menyimpan user baru.", "", "DB_INSERT_USER_FAILED");
                }
            }
        }

        /// <summary>
        /// Memperbarui data user di database.
        /// 
        /// PERBAIKAN: Jika entity adalah Penjual, method ini juga mengupdate tabel verifications
        /// (kolom is_verifikasi) agar perubahan dari Approve() tersimpan ke database.
        /// Sebelumnya, update verifikasi tidak ditangani sehingga Approve() hanya berlaku
        /// di memori (RAM) dan hilang saat aplikasi di-restart.
        /// </summary>
        public void Update(User entity)
        {
            if (entity == null) throw new ArgumentNullException("entity", "Entity user tidak boleh null.");

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlTransaction dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Update tabel users (nama, is_diblokir)
                        string queryUser = "UPDATE users SET nama = @nama, is_diblokir = @isBlokir WHERE id_user = @id;";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(queryUser, conn, dbTx))
                        {
                            cmd.Parameters.AddWithValue("@id", entity.GetIdUser());
                            cmd.Parameters.AddWithValue("@nama", entity.GetNama());
                            cmd.Parameters.AddWithValue("@isBlokir", entity.IsDiblokir());
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Jika Penjual, update tabel verifications juga
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

                        dbTx.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTx.Rollback();
                        throw new InvalidOrderException("Update user dibatalkan (Rollback): " + ex.Message, "", "DB_UPDATE_USER_FAILED", ex);
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

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
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

        /// <summary>
        /// Memetakan baris dari NpgsqlDataReader menjadi objek User (Penjual/Pembeli).
        /// Mendukung query dengan atau tanpa JOIN ke tabel verifications.
        /// </summary>
        private User MappingReaderToUser(NpgsqlDataReader reader)
        {
            string peran = reader.GetString(reader.GetOrdinal("peran"));
            string nama = reader.GetString(reader.GetOrdinal("nama"));
            string username = reader.GetString(reader.GetOrdinal("username"));
            string password = reader.GetString(reader.GetOrdinal("password"));

            User user;
            if (peran == "Penjual")
            {
                Penjual penjual = new Penjual(nama, username, password);

                // Cek keberadaan kolom verifikasi sebelum membaca (guard agar tidak crash
                // saat dipanggil dari query yang tidak menyertakan JOIN ke verifications)
                if (HasColumn(reader, "nim") && !reader.IsDBNull(reader.GetOrdinal("nim")))
                    penjual.SetNim(reader.GetString(reader.GetOrdinal("nim")));

                if (HasColumn(reader, "nama_toko") && !reader.IsDBNull(reader.GetOrdinal("nama_toko")))
                    penjual.SetNamaToko(reader.GetString(reader.GetOrdinal("nama_toko")));

                if (HasColumn(reader, "tahun_masuk") && !reader.IsDBNull(reader.GetOrdinal("tahun_masuk")))
                    penjual.SetTahunMasuk(reader.GetInt32(reader.GetOrdinal("tahun_masuk")));

                if (HasColumn(reader, "is_verifikasi") && !reader.IsDBNull(reader.GetOrdinal("is_verifikasi")) && reader.GetBoolean(reader.GetOrdinal("is_verifikasi")))
                    penjual.Approve();

                if (HasColumn(reader, "bukti_ktm") && !reader.IsDBNull(reader.GetOrdinal("bukti_ktm")))
                {
                    byte[] ktmBytes = (byte[])reader["bukti_ktm"];
                    penjual.SetBuktiKtm(ktmBytes);
                }

                user = penjual;
            }
            else
            {
                user = new Pembeli(nama, username, password);
            }

            user.SetIdUser(reader.GetInt32(reader.GetOrdinal("id_user")));

            if (HasColumn(reader, "nomor_telepon") && !reader.IsDBNull(reader.GetOrdinal("nomor_telepon")))
                user.SetNomorTelepon(reader.GetString(reader.GetOrdinal("nomor_telepon")));

            if (HasColumn(reader, "email") && !reader.IsDBNull(reader.GetOrdinal("email")))
                user.SetEmail(reader.GetString(reader.GetOrdinal("email")));

            if (!reader.IsDBNull(reader.GetOrdinal("is_diblokir")) && reader.GetBoolean(reader.GetOrdinal("is_diblokir")))
                user.Blokir("Diblokir oleh Admin");

            return user;
        }

        private void MappingUserToParameters(NpgsqlCommand cmd, User entity)
        {
            cmd.Parameters.AddWithValue("@nama", entity.GetNama());
            cmd.Parameters.AddWithValue("@uname", entity.GetUsername());
            cmd.Parameters.AddWithValue("@pass", entity.GetPassword());
            cmd.Parameters.AddWithValue("@telp", string.IsNullOrEmpty(entity.GetNomorTelepon()) ? (object)DBNull.Value : entity.GetNomorTelepon());
            cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(entity.GetEmail()) ? (object)DBNull.Value : entity.GetEmail());
        }

        /// <summary>
        /// Mengecek apakah sebuah kolom ada dalam result set reader.
        /// Digunakan sebagai guard clause agar tidak crash saat kolom tidak ada di query.
        /// </summary>
        private bool HasColumn(NpgsqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Menyimpan data Penjual sekaligus data Verifikasinya (termasuk BYTEA bukti_ktm).
        /// Menggunakan BeginTransaction agar atomic.
        /// </summary>
        private void InsertPenjualWithVerification(Penjual penjual)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlTransaction dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert Tabel Users
                        string queryUser = "INSERT INTO users (nama, nomor_telepon, email, username, password, peran) VALUES (@nama, @telp, @email, @uname, @pass, @peran) RETURNING id_user;";
                        int idUserBaru;

                        using (NpgsqlCommand cmdUser = new NpgsqlCommand(queryUser, conn, dbTx))
                        {
                            MappingUserToParameters(cmdUser, penjual);
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

                        // 2. Insert Tabel Verifications
                        string queryVerif = "INSERT INTO verifications (id_user, nim, nama_toko, bukti_ktm, tahun_masuk) VALUES (@idUser, @nim, @toko, @ktm, @tahun);";

                        using (NpgsqlCommand cmdVerif = new NpgsqlCommand(queryVerif, conn, dbTx))
                        {
                            cmdVerif.Parameters.AddWithValue("@idUser", penjual.GetIdUser());
                            cmdVerif.Parameters.AddWithValue("@nim", penjual.GetNim());
                            cmdVerif.Parameters.AddWithValue("@toko", penjual.GetNamaToko());
                            cmdVerif.Parameters.AddWithValue("@tahun", penjual.GetTahunMasuk());
                            cmdVerif.Parameters.AddWithValue("@ktm", (object)penjual.GetBuktiKtm() ?? DBNull.Value);

                            int rowsVerif = cmdVerif.ExecuteNonQuery();
                            if (rowsVerif == 0) throw new InvalidOrderException("Gagal menyimpan data verifikasi penjual.", "", "DB_INSERT_VERIF_FAILED");
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
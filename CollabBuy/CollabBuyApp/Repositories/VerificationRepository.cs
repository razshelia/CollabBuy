using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class VerificationRepository : IVerificationRepository
    {
        private readonly DatabaseHelper _db;

        public VerificationRepository()
        {
            _db = new DatabaseHelper();
        }

        public bool AjukanVerifikasi(Verification verif)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = @"INSERT INTO verifications (id_user, nim, nama_toko, bukti_ktm, tahun_masuk, is_verifikasi)
                               VALUES (@idUser, @nim, @toko, @ktm, @tahun, false)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("idUser", verif.IdUser);
                    cmd.Parameters.AddWithValue("nim", verif.Nim);
                    cmd.Parameters.AddWithValue("toko", verif.NamaToko);
                    cmd.Parameters.AddWithValue("ktm", verif.BuktiKtm);
                    cmd.Parameters.AddWithValue("tahun", verif.TahunMasuk);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ajukan verifikasi: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public List<Verification> AmbilPengajuanPending()
        {
            List<Verification> list = new List<Verification>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;

            try
            {
                conn.Open();
                string sql = @"SELECT id_verifikasi, id_user, nim, nama_toko, bukti_ktm, tahun_masuk, is_verifikasi
                               FROM verifications WHERE is_verifikasi = false";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Verification v = new Verification();
                        v.IdVerifikasi = reader.GetInt32(0);
                        v.IdUser = reader.GetInt32(1);
                        v.Nim = reader.GetString(2);
                        v.NamaToko = reader.GetString(3);
                        v.BuktiKtm = reader.GetString(4);
                        v.TahunMasuk = reader.GetInt32(5);
                        v.IsVerifikasi = reader.GetBoolean(6);
                        list.Add(v);
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil pengajuan: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return list;
        }

        public bool SetujuiVerifikasi(int idVerifikasi)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "UPDATE verifications SET is_verifikasi = true WHERE id_verifikasi = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idVerifikasi);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal setujui: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool TolakVerifikasi(int idVerifikasi)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "DELETE FROM verifications WHERE id_verifikasi = @id"; // atau update status ditolak, tapi di tabel tidak ada kolom status. Saya gunakan delete
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idVerifikasi);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal tolak: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public Verification AmbilVerifikasiByUser(int idUser)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return null;

            try
            {
                conn.Open();
                string sql = "SELECT id_verifikasi, id_user, nim, nama_toko, bukti_ktm, tahun_masuk, is_verifikasi FROM verifications WHERE id_user = @idUser";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("idUser", idUser);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Verification v = new Verification();
                            v.IdVerifikasi = reader.GetInt32(0);
                            v.IdUser = reader.GetInt32(1);
                            v.Nim = reader.GetString(2);
                            v.NamaToko = reader.GetString(3);
                            v.BuktiKtm = reader.GetString(4);
                            v.TahunMasuk = reader.GetInt32(5);
                            v.IsVerifikasi = reader.GetBoolean(6);
                            return v;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil verifikasi: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }
    }
}
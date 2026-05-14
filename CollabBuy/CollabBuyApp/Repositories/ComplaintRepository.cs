using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly DatabaseHelper _db;

        public ComplaintRepository()
        {
            _db = new DatabaseHelper();
        }

        public bool KirimAduan(Complaint aduan)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "INSERT INTO complaints (id_user, subjek, deskripsi) VALUES (@idUser, @subjek, @deskripsi)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("idUser", aduan.IdUser);
                    cmd.Parameters.AddWithValue("subjek", aduan.Subjek);
                    cmd.Parameters.AddWithValue("deskripsi", aduan.Deskripsi);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal mengirim aduan: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        public List<Complaint> AmbilSemuaAduan()
        {
            List<Complaint> list = new List<Complaint>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;

            try
            {
                conn.Open();
                string sql = @"SELECT c.id_aduan, c.id_user, c.subjek, c.deskripsi, c.tanggal, c.is_selesai, c.balasan, u.username
                               FROM complaints c JOIN users u ON c.id_user = u.id_user
                               ORDER BY c.tanggal DESC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Complaint aduan = new Complaint();
                        aduan.IdAduan = reader.GetInt32(0);
                        aduan.IdUser = reader.GetInt32(1);
                        aduan.Subjek = reader.GetString(2);
                        aduan.Deskripsi = reader.GetString(3);
                        aduan.Tanggal = reader.GetDateTime(4);
                        aduan.IsSelesai = reader.GetBoolean(5);
                        aduan.Balasan = reader.IsDBNull(6) ? null : reader.GetString(6);
                        list.Add(aduan);
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal mengambil aduan: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return list;
        }

        public List<Complaint> AmbilAduanByUser(int idUser)
        {
            List<Complaint> list = new List<Complaint>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;

            try
            {
                conn.Open();
                string sql = @"SELECT id_aduan, id_user, subjek, deskripsi, tanggal, is_selesai, balasan
                               FROM complaints WHERE id_user = @idUser
                               ORDER BY tanggal DESC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("idUser", idUser);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Complaint aduan = new Complaint();
                            aduan.IdAduan = reader.GetInt32(0);
                            aduan.IdUser = reader.GetInt32(1);
                            aduan.Subjek = reader.GetString(2);
                            aduan.Deskripsi = reader.GetString(3);
                            aduan.Tanggal = reader.GetDateTime(4);
                            aduan.IsSelesai = reader.GetBoolean(5);
                            aduan.Balasan = reader.IsDBNull(6) ? null : reader.GetString(6);
                            list.Add(aduan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal mengambil aduan: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return list;
        }

        public bool TandaiSelesai(int idAduan)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "UPDATE complaints SET is_selesai = true WHERE id_aduan = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idAduan);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal menandai selesai: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        public bool BalasAduan(int idAduan, string balasan)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "UPDATE complaints SET balasan = @balasan, is_selesai = true WHERE id_aduan = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("balasan", balasan);
                    cmd.Parameters.AddWithValue("id", idAduan);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal membalas aduan: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
    }
}
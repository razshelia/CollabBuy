using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class ComplaintRepository : BaseRepository, IComplaintRepository
    {
        public bool KirimAduan(Complaint aduan)
        {
            string sql = "INSERT INTO complaints (id_user, subjek, deskripsi) VALUES (@idUser, @subjek, @deskripsi)";
            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("idUser", aduan.IdUser);
                cmd.Parameters.AddWithValue("subjek", aduan.Subjek);
                cmd.Parameters.AddWithValue("deskripsi", aduan.Deskripsi);
            });

            return row > 0;
        }

        public List<Complaint> AmbilSemuaAduan()
        {
            List<Complaint> list = new List<Complaint>();
            string sql = @"SELECT c.id_aduan, c.id_user, c.subjek, c.deskripsi, c.tanggal, c.is_selesai, c.balasan, u.username
                           FROM complaints c JOIN users u ON c.id_user = u.id_user
                           ORDER BY c.tanggal DESC";
            ExecuteQuery(sql, null, reader =>
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
            });

            return list;
        }

        public List<Complaint> AmbilAduanByUser(int idUser)
        {
            List<Complaint> list = new List<Complaint>();
            string sql = @"SELECT id_aduan, id_user, subjek, deskripsi, tanggal, is_selesai, balasan
                           FROM complaints WHERE id_user = @idUser
                           ORDER BY tanggal DESC";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("idUser", idUser), reader =>
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
            });

            return list;
        }

        public bool TandaiSelesai(int idAduan)
        {
            string sql = "UPDATE complaints SET is_selesai = true WHERE id_aduan = @id";

            int row = ExecuteNonQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idAduan));

            return row > 0;
        }

        public bool BalasAduan(int idAduan, string balasan)
        {
            string sql = "UPDATE complaints SET balasan = @balasan, is_selesai = true WHERE id_aduan = @id";

            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("balasan", balasan);
                cmd.Parameters.AddWithValue("id", idAduan);
            });

            return row > 0;
        }
    }
}
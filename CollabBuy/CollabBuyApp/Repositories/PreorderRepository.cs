using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class PreorderRepository : IPreorderRepository
    {
        private readonly DatabaseHelper _db;

        public PreorderRepository()
        {
            _db = new DatabaseHelper();
        }

        public bool TambahPreorder(Preorder preorder)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = @"INSERT INTO preorders (id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif)
                               VALUES (@penjual, @judul, @jenis, @rekening, @batas, true)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("penjual", preorder.IdPenjual);
                    cmd.Parameters.AddWithValue("judul", preorder.JudulPo);
                    cmd.Parameters.AddWithValue("jenis", preorder.JenisPo);
                    cmd.Parameters.AddWithValue("rekening", preorder.InfoRekening);
                    cmd.Parameters.AddWithValue("batas", preorder.BatasWaktu);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal buat PO: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public List<Preorder> AmbilPreorderAktif()
        {
            List<Preorder> list = new List<Preorder>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;

            try
            {
                conn.Open();
                string sql = @"SELECT id_po, id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif
                               FROM preorders WHERE is_aktif = true AND batas_waktu >= CURRENT_TIMESTAMP";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Preorder po = BuatPreorderDariReader(reader);
                        list.Add(po);
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil PO aktif: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return list;
        }

        public List<Preorder> AmbilPreorderByPenjual(int idPenjual)
        {
            List<Preorder> list = new List<Preorder>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;

            try
            {
                conn.Open();
                string sql = @"SELECT id_po, id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif
                               FROM preorders WHERE id_penjual = @penjual ORDER BY batas_waktu DESC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("penjual", idPenjual);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Preorder po = BuatPreorderDariReader(reader);
                            list.Add(po);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil PO penjual: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return list;
        }

        public Preorder AmbilPreorderById(int idPo)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return null;

            try
            {
                conn.Open();
                string sql = @"SELECT id_po, id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif
                               FROM preorders WHERE id_po = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idPo);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return BuatPreorderDariReader(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil PO: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }

        public bool TutupPreorder(int idPo)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "UPDATE preorders SET is_aktif = false WHERE id_po = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idPo);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal tutup PO: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        // Helper: mapping reader ke subclass Preorder yang tepat
        private Preorder BuatPreorderDariReader(NpgsqlDataReader reader)
        {
            string jenis = reader.GetString(3);
            Preorder po;
            if (jenis == "Gotong Royong")
                po = new PreorderGotongRoyong();
            else
                po = new PreorderBiasa();

            po.IdPo = reader.GetInt32(0);
            po.IdPenjual = reader.GetInt32(1);
            po.JudulPo = reader.GetString(2);
            // JenisPo sudah diatur oleh subclass
            po.InfoRekening = reader.GetString(4);
            po.BatasWaktu = reader.GetDateTime(5);
            po.IsAktif = reader.GetBoolean(6);

            return po;
        }
    }
}
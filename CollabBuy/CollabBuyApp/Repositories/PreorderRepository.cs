using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class PreorderRepository : IPreorderRepository
    {
        private DatabaseHelper _db = new DatabaseHelper();

        public bool TambahPreorder(Preorder preorder, int idProduk, int targetKuota)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;
            try
            {
                conn.Open();
                using (NpgsqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert ke preorders TANPA id_produk, dan minta kembalian ID PO barunya (RETURNING id_po)
                        string sqlPO = @"INSERT INTO preorders (id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif)
                                 VALUES (@penjual, @judul, @jenis, @rekening, @batas, true) RETURNING id_po";
                        int newIdPo;
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sqlPO, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("penjual", preorder.IdPenjual);
                            cmd.Parameters.AddWithValue("judul", preorder.JudulPo);
                            cmd.Parameters.AddWithValue("jenis", preorder.JenisPo);
                            cmd.Parameters.AddWithValue("rekening", preorder.InfoRekening);
                            cmd.Parameters.AddWithValue("batas", preorder.BatasWaktu);
                            newIdPo = Convert.ToInt32(cmd.ExecuteScalar()); // Tangkap ID PO yang baru terbuat
                        }

                        // 2. UPDATE tabel products: Masukkan ID PO baru ke produk yang dipilih!
                        string sqlProd = "UPDATE products SET id_po = @po_baru WHERE id_produk = @produk";
                        using (NpgsqlCommand cmdProd = new NpgsqlCommand(sqlProd, conn, trans))
                        {
                            cmdProd.Parameters.AddWithValue("po_baru", newIdPo);
                            cmdProd.Parameters.AddWithValue("produk", idProduk);
                            cmdProd.ExecuteNonQuery();
                        }

                        // 3. Jika Gotong Royong, update juga target kuotanya
                        if (preorder.JenisPo == "Gotong Royong" && targetKuota > 0)
                        {
                            string sqlTarget = "UPDATE products SET target_kuota = @target WHERE id_produk = @produk";
                            using (NpgsqlCommand cmdTarget = new NpgsqlCommand(sqlTarget, conn, trans))
                            {
                                cmdTarget.Parameters.AddWithValue("target", targetKuota);
                                cmdTarget.Parameters.AddWithValue("produk", idProduk);
                                cmdTarget.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        return true;
                    }
                    catch { trans.Rollback(); throw; }
                }
            }
            catch (Exception ex) { UXHelper.TampilkanError("Gagal simpan PO: " + ex.Message); return false; }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
        }

        public List<Preorder> AmbilPreorderAktif()
        {
            var list = new List<Preorder>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;
            try
            {
                conn.Open();
                string sql = "SELECT * FROM preorders WHERE is_aktif = true AND batas_waktu > CURRENT_TIMESTAMP";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(MapEntity(reader));
                }
            }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
            return list;
        }

        public List<Preorder> AmbilPreorderByPenjual(int idPenjual)
        {
            var list = new List<Preorder>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;
            try
            {
                conn.Open();
                string sql = "SELECT * FROM preorders WHERE id_penjual = @id ORDER BY id_po DESC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idPenjual);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) list.Add(MapEntity(reader));
                    }
                }
            }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
            return list;
        }

        public Preorder AmbilPreorderById(int idPo)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return null;
            try
            {
                conn.Open();
                string sql = "SELECT * FROM preorders WHERE id_po = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idPo);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapEntity(reader);
                    }
                }
            }
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
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
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
        }

        private Preorder MapEntity(NpgsqlDataReader reader)
        {
            string jenis = reader["jenis_po"].ToString();
            Preorder po = (jenis == TipePO.GotongRoyong) ? (Preorder)new POGotongRoyong() : (Preorder)new POBiasa();
            po.IdPo = Convert.ToInt32(reader["id_po"]);
            po.IdPenjual = Convert.ToInt32(reader["id_penjual"]);
            po.JudulPo = reader["judul_po"].ToString();
            po.InfoRekening = reader["info_rekening"].ToString();
            po.BatasWaktu = Convert.ToDateTime(reader["batas_waktu"]);
            po.IsAktif = Convert.ToBoolean(reader["is_aktif"]);
            return po;
        }
    }
}
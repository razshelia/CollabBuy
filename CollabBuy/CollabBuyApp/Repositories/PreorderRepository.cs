using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using Npgsql;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class PreorderRepository : BaseRepository, IPreorderRepository
    {
        public bool TambahPreorder(Preorder preorder, int idProduk, int targetKuota)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");

            try
            {
                conn.Open();
                using (NpgsqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
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
                            newIdPo = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        string sqlProd = "UPDATE products SET id_po = @po_baru WHERE id_produk = @produk";
                        using (NpgsqlCommand cmdProd = new NpgsqlCommand(sqlProd, conn, trans))
                        {
                            cmdProd.Parameters.AddWithValue("po_baru", newIdPo);
                            cmdProd.Parameters.AddWithValue("produk", idProduk);
                            cmdProd.ExecuteNonQuery();
                        }

                        if (preorder.JenisPo == TipePO.GotongRoyong && targetKuota > 0)
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
            catch (Exception ex) { throw new Exception("Gagal menyimpan PO ke database.", ex); } 
            finally { if (conn.State == System.Data.ConnectionState.Open) conn.Close(); }
        }

        public List<Preorder> AmbilPreorderAktif()
        {
            var list = new List<Preorder>();
            string sql = "SELECT * FROM preorders WHERE is_aktif = true AND batas_waktu > CURRENT_TIMESTAMP";
            ExecuteQuery(sql, null, reader =>
            {
                list.Add(MapEntity(reader));
            });

            return list;
        }

        public List<Preorder> AmbilPreorderByPenjual(int idPenjual)
        {
            var list = new List<Preorder>();
            string sql = "SELECT * FROM preorders WHERE id_penjual = @id ORDER BY id_po DESC";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idPenjual), reader =>
            {
                list.Add(MapEntity(reader));
            });

            return list;
        }

        public Preorder AmbilPreorderById(int idPo)
        {
            Preorder po = null;
            string sql = "SELECT * FROM preorders WHERE id_po = @id";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idPo), reader =>
            {
                po = MapEntity(reader);
            });

            return po;
        }

        public bool TutupPreorder(int idPo)
        {
            string sql = "UPDATE preorders SET is_aktif = false WHERE id_po = @id";
            int row = ExecuteNonQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idPo));

            return row > 0;
        }

        private Preorder MapEntity(NpgsqlDataReader reader)
        {
            string jenis = reader["jenis_po"].ToString();
            Preorder po = PreorderFactory.BuatPreorder(jenis);

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
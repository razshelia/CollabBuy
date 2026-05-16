using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using Npgsql;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        public int BuatTransaksi(Transaction transaksi, List<TransactionDetail> details)
        {
            using (var conn = _db.AmbilKoneksi())
            {
                if (conn == null) throw new Exception("Tidak dapat terhubung ke database.");
                try
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            int idTransaksi;
                            string sqlTrans = @"INSERT INTO transactions (id_koordinator, total_bayar_grup, status_pesanan)
                                                VALUES (@idKoor, @total, @status) RETURNING id_transaksi";

                            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlTrans, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("idKoor", transaksi.IdKoordinator);
                                cmd.Parameters.AddWithValue("total", transaksi.TotalBayarGrup);
                                cmd.Parameters.AddWithValue("status", transaksi.StatusPesanan ?? "Menunggu");
                                idTransaksi = (int)cmd.ExecuteScalar();
                            }

                            foreach (var detail in details)
                            {
                                using (NpgsqlCommand cmd = new NpgsqlCommand("CALL proses_checkout_detail(@idTrans, @idProd, @penitip, @jumlah, @catatan)", conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("idTrans", idTransaksi);
                                    cmd.Parameters.AddWithValue("idProd", detail.IdProduk);
                                    cmd.Parameters.AddWithValue("penitip", detail.NamaPenitip);
                                    cmd.Parameters.AddWithValue("jumlah", detail.JumlahPesanan);
                                    cmd.Parameters.AddWithValue("catatan", (object)detail.Catatan ?? DBNull.Value);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return idTransaksi;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                catch (Exception ex) { throw new Exception("Gagal membuat transaksi ke database.", ex); }
            }
        }

        public bool ValidasiPembayaran(int idTransaksi, string buktiBayar)
        {
            string sql = "UPDATE transactions SET is_valid = true, bukti_bayar = @bukti WHERE id_transaksi = @id";
            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("bukti", buktiBayar);
                cmd.Parameters.AddWithValue("id", idTransaksi);
            });

            return row > 0;
        }

        public bool UbahStatusPesanan(int idTransaksi, string statusBaru)
        {
            string sql = "UPDATE transactions SET status_pesanan = @status WHERE id_transaksi = @id";

            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("status", statusBaru);
                cmd.Parameters.AddWithValue("id", idTransaksi);
            });

            return row > 0;
        }

        public List<Transaction> AmbilRiwayatKoordinator(int idKoordinator)
        {
            List<Transaction> list = new List<Transaction>();
            string sql = "SELECT id_transaksi, id_koordinator, tanggal_transaksi, total_bayar_grup, status_pesanan, bukti_bayar, is_valid FROM transactions WHERE id_koordinator = @idKoor ORDER BY tanggal_transaksi DESC";
            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("idKoor", idKoordinator), reader =>
            {
                list.Add(MapTransaction(reader));
            });

            return list;
        }

        public List<Transaction> AmbilPesananMasukPenjual(int idPenjual)
        {
            List<Transaction> list = new List<Transaction>();
            string sql = @"SELECT DISTINCT t.id_transaksi, t.id_koordinator, t.tanggal_transaksi, t.total_bayar_grup, t.status_pesanan, t.bukti_bayar, t.is_valid
                           FROM transactions t
                           JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
                           JOIN products p ON td.id_produk = p.id_produk
                           JOIN preorders po ON p.id_po = po.id_po
                           WHERE po.id_penjual = @idPenjual
                           ORDER BY t.tanggal_transaksi DESC";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("idPenjual", idPenjual), reader =>
            {
                list.Add(MapTransaction(reader));
            });

            return list;
        }

        public List<TransactionDetail> AmbilDetailTransaksi(int idTransaksi)
        {
            List<TransactionDetail> list = new List<TransactionDetail>();
            string sql = "SELECT id_detail, id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan, selisih_refund FROM transaction_details WHERE id_transaksi = @idTrans";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("idTrans", idTransaksi), reader =>
            {
                TransactionDetail d = new TransactionDetail();
                d.IdDetail = reader.GetInt32(0);
                d.IdTransaksi = reader.GetInt32(1);
                d.IdProduk = reader.GetInt32(2);
                d.NamaPenitip = reader.GetString(3);
                d.JumlahPesanan = reader.GetInt32(4);
                d.Catatan = reader.IsDBNull(5) ? null : reader.GetString(5);
                d.SelisihRefund = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                list.Add(d);
            });

            return list;
        }

        public Transaction AmbilTransaksiById(int idTransaksi)
        {
            Transaction t = null;
            string sql = "SELECT id_transaksi, id_koordinator, tanggal_transaksi, total_bayar_grup, status_pesanan, bukti_bayar, is_valid FROM transactions WHERE id_transaksi = @id";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idTransaksi), reader =>
            {
                t = MapTransaction(reader);
            });

            return t;
        }

        public int AmbilJumlahTransaksi()
        {
            string sql = "SELECT COUNT(*) FROM transactions";
            var result = ExecuteScalar(sql, null);

            if (result != DBNull.Value && result != null)
                return Convert.ToInt32(result);

            return 0;
        }

        private Transaction MapTransaction(NpgsqlDataReader reader)
        {
            return new Transaction
            {
                IdTransaksi = reader.GetInt32(0),
                IdKoordinator = reader.GetInt32(1),
                TanggalTransaksi = reader.GetDateTime(2),
                TotalBayarGrup = reader.GetInt32(3),
                StatusPesanan = reader.GetString(4),
                BuktiBayar = reader.IsDBNull(5) ? null : reader.GetString(5),
                IsValid = reader.GetBoolean(6)
            };
        }
    }
}
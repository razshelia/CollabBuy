using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DatabaseHelper _db;

        public TransactionRepository()
        {
            _db = new DatabaseHelper();
        }

        public int BuatTransaksi(Transaction transaksi, List<TransactionDetail> details)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return -1;

            try
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert transaksi utama dan ambil id_transaksi
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

                        // 2. Panggil procedure untuk setiap detail (atau insert manual, tapi kita pakai procedure)
                        foreach (var detail in details)
                        {
                            using (NpgsqlCommand cmd = new NpgsqlCommand("CALL proses_checkout(@p_id_koordinator, @p_total_bayar, @p_id_produk, @p_nama_penitip, @p_jumlah, @p_catatan)", conn, transaction))
                            {
                                // Karena procedure mengharapkan semua parameter, tapi kita sudah punya id_transaksi, 
                                // lebih baik procedure disesuaikan agar menerima id_transaksi juga. 
                                // Untuk sementara, kita insert manual transaction_details seperti sebelumnya.
                                // Karena procedure kita desain tanpa id_transaksi (membuat transaksi baru), 
                                // lebih aman tetap insert manual untuk detail.
                                // Jadi kita tidak ubah bagian detail.
                            }
                        }

                        // Tetap gunakan insert manual untuk detail (procedure kita tidak cocok untuk multi detail)
                        string sqlDetail = @"INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan)
                                    VALUES (@idTrans, @idProd, @penitip, @jumlah, @catatan)";
                        foreach (var detail in details)
                        {
                            using (NpgsqlCommand cmd = new NpgsqlCommand(sqlDetail, conn, transaction))
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
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal membuat transaksi: " + ex.Message);
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        public bool ValidasiPembayaran(int idTransaksi, string buktiBayar)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "UPDATE transactions SET is_valid = true, bukti_bayar = @bukti WHERE id_transaksi = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("bukti", buktiBayar);
                    cmd.Parameters.AddWithValue("id", idTransaksi);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal validasi: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool UbahStatusPesanan(int idTransaksi, string statusBaru)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return false;

            try
            {
                conn.Open();
                string sql = "UPDATE transactions SET status_pesanan = @status WHERE id_transaksi = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("status", statusBaru);
                    cmd.Parameters.AddWithValue("id", idTransaksi);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ubah status: " + ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public List<Transaction> AmbilRiwayatKoordinator(int idKoordinator)
        {
            List<Transaction> list = new List<Transaction>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;

            try
            {
                conn.Open();
                string sql = "SELECT id_transaksi, id_koordinator, tanggal_transaksi, total_bayar_grup, status_pesanan, bukti_bayar, is_valid FROM transactions WHERE id_koordinator = @idKoor ORDER BY tanggal_transaksi DESC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("idKoor", idKoordinator);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transaction t = new Transaction();
                            t.IdTransaksi = reader.GetInt32(0);
                            t.IdKoordinator = reader.GetInt32(1);
                            t.TanggalTransaksi = reader.GetDateTime(2);
                            t.TotalBayarGrup = reader.GetInt32(3);
                            t.StatusPesanan = reader.GetString(4);
                            t.BuktiBayar = reader.IsDBNull(5) ? null : reader.GetString(5);
                            t.IsValid = reader.GetBoolean(6);
                            list.Add(t);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil riwayat: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return list;
        }

        public List<Transaction> AmbilPesananMasukPenjual(int idPenjual)
        {
            List<Transaction> list = new List<Transaction>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;

            try
            {
                conn.Open();
                // Join dengan products dan preorders untuk mendapatkan penjual
                string sql = @"SELECT DISTINCT t.id_transaksi, t.id_koordinator, t.tanggal_transaksi, t.total_bayar_grup, t.status_pesanan, t.bukti_bayar, t.is_valid
                               FROM transactions t
                               JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
                               JOIN products p ON td.id_produk = p.id_produk
                               JOIN preorders po ON p.id_po = po.id_po
                               WHERE po.id_penjual = @idPenjual
                               ORDER BY t.tanggal_transaksi DESC";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("idPenjual", idPenjual);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transaction t = new Transaction();
                            t.IdTransaksi = reader.GetInt32(0);
                            t.IdKoordinator = reader.GetInt32(1);
                            t.TanggalTransaksi = reader.GetDateTime(2);
                            t.TotalBayarGrup = reader.GetInt32(3);
                            t.StatusPesanan = reader.GetString(4);
                            t.BuktiBayar = reader.IsDBNull(5) ? null : reader.GetString(5);
                            t.IsValid = reader.GetBoolean(6);
                            list.Add(t);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil pesanan: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return list;
        }

        public List<TransactionDetail> AmbilDetailTransaksi(int idTransaksi)
        {
            List<TransactionDetail> list = new List<TransactionDetail>();
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return list;

            try
            {
                conn.Open();
                string sql = "SELECT id_detail, id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan, selisih_refund FROM transaction_details WHERE id_transaksi = @idTrans";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("idTrans", idTransaksi);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TransactionDetail d = new TransactionDetail();
                            d.IdDetail = reader.GetInt32(0);
                            d.IdTransaksi = reader.GetInt32(1);
                            d.IdProduk = reader.GetInt32(2);
                            d.NamaPenitip = reader.GetString(3);
                            d.JumlahPesanan = reader.GetInt32(4);
                            d.Catatan = reader.IsDBNull(5) ? null : reader.GetString(5);
                            d.SelisihRefund = reader.GetInt32(6);
                            list.Add(d);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil detail: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return list;
        }

        public Transaction AmbilTransaksiById(int idTransaksi)
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return null;

            try
            {
                conn.Open();
                string sql = "SELECT id_transaksi, id_koordinator, tanggal_transaksi, total_bayar_grup, status_pesanan, bukti_bayar, is_valid FROM transactions WHERE id_transaksi = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", idTransaksi);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Transaction t = new Transaction();
                            t.IdTransaksi = reader.GetInt32(0);
                            t.IdKoordinator = reader.GetInt32(1);
                            t.TanggalTransaksi = reader.GetDateTime(2);
                            t.TotalBayarGrup = reader.GetInt32(3);
                            t.StatusPesanan = reader.GetString(4);
                            t.BuktiBayar = reader.IsDBNull(5) ? null : reader.GetString(5);
                            t.IsValid = reader.GetBoolean(6);
                            return t;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError("Gagal ambil transaksi: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return null;
        }
        public int AmbilJumlahTransaksi()
        {
            NpgsqlConnection conn = _db.AmbilKoneksi();
            if (conn == null) return 0;
            try
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM transactions";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch { return 0; }
            finally { if (conn.State == ConnectionState.Open) conn.Close(); }
        }
    }
}
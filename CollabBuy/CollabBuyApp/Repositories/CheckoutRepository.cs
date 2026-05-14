using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Helpers;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private DatabaseHelper dbHelper;

        public CheckoutRepository()
        {
            this.dbHelper = new DatabaseHelper();
        }

        // 1. Buat transaksi baru
        public bool BuatTransaksi(int idUser, int idPo, int jumlahPesanan, string pathBukti)
        {
            NpgsqlConnection koneksi = dbHelper.AmbilKoneksi();
            if (koneksi == null) return false;
            try
            {
                koneksi.Open();
                // Sesuaikan nama stored procedure atau query dengan parameter path_bukti
                string sql = "CALL sp_buat_transaksi(@p_id_user, @p_id_po, @p_qty, @p_bukti)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("p_id_user", idUser);
                    cmd.Parameters.AddWithValue("p_id_po", idPo);
                    cmd.Parameters.AddWithValue("p_qty", jumlahPesanan);
                    cmd.Parameters.AddWithValue("p_bukti", pathBukti);  // <-- path relatif
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception) { return false; }
            finally { if (koneksi.State == ConnectionState.Open) koneksi.Close(); }
        }

        // 2. Riwayat pesanan seorang user (coordinator)
        public List<dynamic> AmbilRiwayatPesanan(int idUser)
        {
            var hasil = new List<dynamic>();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return hasil;

            try
            {
                koneksi.Open();
                string sql = @"
                    SELECT 
                        c.id_checkout,
                        p.nama_produk,
                        c.jumlah,
                        c.total_bayar_awal,
                        c.status,
                        c.created_at
                    FROM checkouts c
                    JOIN preorders po ON c.id_po = po.id_po
                    JOIN products p ON po.id_produk = p.id_produk
                    WHERE c.id_user_coordinator = @idUser
                    ORDER BY c.created_at DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("idUser", idUser);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hasil.Add(new
                            {
                                IdCheckout = reader.GetInt32(0),
                                NamaProduk = reader.GetString(1),
                                Jumlah = reader.GetInt32(2),
                                TotalBayar = reader.GetDecimal(3),
                                Status = reader.GetString(4),
                                Tanggal = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                // log error jika perlu
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                    koneksi.Close();
            }

            return hasil;
        }

        // 3. Pesanan masuk untuk seller tertentu
        public List<dynamic> AmbilPesananMasuk(int idSeller)
        {
            var hasil = new List<dynamic>();
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return hasil;

            try
            {
                koneksi.Open();
                string sql = @"
                    SELECT 
                        c.id_checkout,
                        u.username AS pembeli,
                        p.nama_produk,
                        c.jumlah,
                        c.total_bayar_awal,
                        c.status,
                        c.bukti_pembayaran,
                        c.created_at
                    FROM checkouts c
                    JOIN preorders po ON c.id_po = po.id_po
                    JOIN products p ON po.id_produk = p.id_produk
                    JOIN users u ON c.id_user_coordinator = u.id_user
                    WHERE p.id_seller = @idSeller
                    ORDER BY c.created_at DESC";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("idSeller", idSeller);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hasil.Add(new
                            {
                                IdCheckout = reader.GetInt32(0),
                                Pembeli = reader.GetString(1),
                                NamaProduk = reader.GetString(2),
                                Jumlah = reader.GetInt32(3),
                                TotalBayar = reader.GetDecimal(4),
                                Status = reader.GetString(5),
                                BuktiPembayaran = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                Tanggal = reader.GetDateTime(7)
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                // log error
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                    koneksi.Close();
            }

            return hasil;
        }

        // 4. Validasi pembayaran (admin/seller)
        public bool ValidasiPembayaran(int idCheckout)
        {
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return false;

            try
            {
                koneksi.Open();
                string sql = "UPDATE checkouts SET status = 'dibayar' WHERE id_checkout = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("id", idCheckout);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                    koneksi.Close();
            }
        }

        // 5. Ubah status menjadi selesai
        public bool UbahStatusSelesai(int idCheckout)
        {
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return false;

            try
            {
                koneksi.Open();
                string sql = "UPDATE checkouts SET status = 'selesai' WHERE id_checkout = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("id", idCheckout);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                    koneksi.Close();
            }
        }

        // 6. Batalkan pesanan
        public bool BatalkanPesanan(int idCheckout)
        {
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();
            if (koneksi == null) return false;

            try
            {
                koneksi.Open();
                string sql = "UPDATE checkouts SET status = 'dibatalkan' WHERE id_checkout = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                {
                    cmd.Parameters.AddWithValue("id", idCheckout);
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                    koneksi.Close();
            }
        }
    }
}
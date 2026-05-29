using System;
using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class TransactionRepository : IQueryRepository<Transaction>, ICommandRepository<Transaction>
    {
        private readonly string _connectionString;

        public TransactionRepository()
        {
            string connStr = ConfigurationManager.ConnectionStrings["CollabBuyDb"]?.ConnectionString;
            if (string.IsNullOrEmpty(connStr)) throw new Exception("Connection string 'CollabBuyDb' tidak ditemukan di App.config!");
            _connectionString = connStr;
        }


        // =======================================================
        // IMPLEMENTASI IQueryRepository<Transaction>
        // =======================================================

        public Transaction GetById(int idTransaksi)
        {
            Transaction transaksi = null;

            string queryHeader = "SELECT id_transaksi, id_koordinator, tanggal_transaksi, status_pesanan, is_valid FROM transactions WHERE id_transaksi = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(queryHeader, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idTransaksi);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idPembeli = reader.GetInt32(reader.GetOrdinal("id_koordinator"));
                            transaksi = new Transaction(idPembeli);
                            transaksi.SetIdTransaksi(reader.GetInt32(reader.GetOrdinal("id_transaksi")));

                            // PERBAIKAN: Gunakan helper method untuk set status langsung dari DB
                            // tanpa melewati state machine UbahStatus() yang mensyaratkan transisi valid.
                            // Status yang tersimpan di DB sudah dianggap valid; kita hanya merestore state-nya.
                            string statusDb = reader.GetString(reader.GetOrdinal("status_pesanan"));
                            SetStatusDariDatabase(transaksi, statusDb);

                            if (!reader.IsDBNull(reader.GetOrdinal("is_valid")) && reader.GetBoolean(reader.GetOrdinal("is_valid")))
                            {
                                transaksi.Approve();
                            }
                        }
                    }
                }
            }

            if (transaksi != null)
            {
                string queryDetail = "SELECT id_produk, nama_penitip, jumlah_pesanan, catatan, nama_produk_snapshot, harga_satuan_saat_beli, harga_diskon_saat_beli, selisih_refund FROM transaction_details WHERE id_transaksi = @idTrx;";

                using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(queryDetail, conn))
                    {
                        cmd.Parameters.AddWithValue("@idTrx", idTransaksi);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idProduk = reader.GetInt32(reader.GetOrdinal("id_produk"));
                                string namaPenitip = reader.GetString(reader.GetOrdinal("nama_penitip"));
                                int jumlah = reader.GetInt32(reader.GetOrdinal("jumlah_pesanan"));

                                TransactionDetail detail = new TransactionDetail(idProduk, namaPenitip, jumlah);

                                long hargaSatuan = Convert.ToInt64(reader.GetInt32(reader.GetOrdinal("harga_satuan_saat_beli")));
                                long? hargaDiskon = null;
                                if (!reader.IsDBNull(reader.GetOrdinal("harga_diskon_saat_beli")))
                                {
                                    hargaDiskon = Convert.ToInt64(reader.GetInt32(reader.GetOrdinal("harga_diskon_saat_beli")));
                                }
                                detail.FinalisasiHargaSaatCheckout(hargaSatuan, hargaDiskon);

                                if (!reader.IsDBNull(reader.GetOrdinal("catatan")))
                                {
                                    detail.SetCatatan(reader.GetString(reader.GetOrdinal("catatan")));
                                }

                                transaksi.TambahDetail(detail);
                            }
                        }
                    }
                }
            }

            return transaksi;
        }

        public List<Transaction> GetAll()
        {
            List<Transaction> listTransaksi = new List<Transaction>();
            string query = "SELECT id_transaksi, id_koordinator, tanggal_transaksi, status_pesanan, is_valid FROM transactions ORDER BY tanggal_transaksi DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idPembeli = reader.GetInt32(reader.GetOrdinal("id_koordinator"));
                            Transaction transaksi = new Transaction(idPembeli);
                            transaksi.SetIdTransaksi(reader.GetInt32(reader.GetOrdinal("id_transaksi")));

                            // PERBAIKAN: Restore status dari DB tanpa state machine
                            string statusDb = reader.GetString(reader.GetOrdinal("status_pesanan"));
                            SetStatusDariDatabase(transaksi, statusDb);

                            if (!reader.IsDBNull(reader.GetOrdinal("is_valid")) && reader.GetBoolean(reader.GetOrdinal("is_valid")))
                            {
                                transaksi.Approve();
                            }
                            listTransaksi.Add(transaksi);
                        }
                    }
                }
            }
            return listTransaksi;
        }


        // =======================================================
        // IMPLEMENTASI ICommandRepository<Transaction>
        // =======================================================

        public void Insert(Transaction entity)
        {
            throw new NotSupportedException("Gunakan method Checkout() untuk insert Transaksi agar atomic.");
        }

        public void Update(Transaction entity)
        {
            if (entity == null) throw new ArgumentNullException("entity", "Entity transaksi tidak boleh null.");

            string query = "UPDATE transactions SET status_pesanan = @status, is_valid = @isValid, bukti_bayar = @bukti WHERE id_transaksi = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", entity.GetStatus());
                    cmd.Parameters.AddWithValue("@isValid", entity.GetStatusPersetujuan());
                    cmd.Parameters.AddWithValue("@id", entity.GetIdTransaksi());
                    cmd.Parameters.AddWithValue("@bukti", (object)entity.GetBuktiBayar() ?? DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new InvalidOrderException("Gagal mengupdate transaksi, ID tidak ditemukan di database.", "id_transaksi", "DB_UPDATE_FAILED");
                    }
                }
            }
        }


        // =======================================================
        // METHOD KHUSUS DB TRANSACTION (TCL)
        // =======================================================

        public int Checkout(Transaction transaksi)
        {
            if (transaksi == null) throw new ArgumentNullException("transaksi", "Transaksi checkout tidak boleh null.");

            int idTransaksiBaru = 0;

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (NpgsqlTransaction dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        string queryHeader = "INSERT INTO transactions (id_koordinator, bukti_bayar, status_pesanan) VALUES (@koord, @bukti, @status) RETURNING id_transaksi;";

                        using (NpgsqlCommand cmdHeader = new NpgsqlCommand(queryHeader, conn, dbTx))
                        {
                            cmdHeader.Parameters.AddWithValue("@koord", transaksi.GetIdPembeli());
                            cmdHeader.Parameters.AddWithValue("@bukti", (object)transaksi.GetBuktiBayar() ?? DBNull.Value);
                            cmdHeader.Parameters.AddWithValue("@status", transaksi.GetStatus());

                            object result = cmdHeader.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                idTransaksiBaru = Convert.ToInt32(result);
                                transaksi.SetIdTransaksi(idTransaksiBaru);
                            }
                            else
                            {
                                throw new InvalidOrderException("Gagal mendapatkan ID Transaksi dari database.", "", "DB_INSERT_HEADER_FAILED");
                            }
                        }

                        string queryDetail = "INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan, nama_produk_snapshot, harga_satuan_saat_beli, harga_diskon_saat_beli) VALUES (@trx, @produk, @penitip, @jumlah, @catatan, @snapshot, @hargaSatuan, @hargaDiskon);";

                        foreach (TransactionDetail detail in transaksi.GetSemuaDetail())
                        {
                            using (NpgsqlCommand cmdDetail = new NpgsqlCommand(queryDetail, conn, dbTx))
                            {
                                cmdDetail.Parameters.AddWithValue("@trx", idTransaksiBaru);
                                cmdDetail.Parameters.AddWithValue("@produk", detail.GetIdProduk());
                                cmdDetail.Parameters.AddWithValue("@penitip", detail.GetNamaPenitip());
                                cmdDetail.Parameters.AddWithValue("@jumlah", detail.GetJumlahPesanan());
                                cmdDetail.Parameters.AddWithValue("@catatan", string.IsNullOrEmpty(detail.GetCatatan()) ? (object)DBNull.Value : detail.GetCatatan());
                                cmdDetail.Parameters.AddWithValue("@snapshot", detail.GetNamaProdukSnapshot() ?? "");
                                cmdDetail.Parameters.AddWithValue("@hargaSatuan", Convert.ToInt32(detail.GetHargaSatuanSaatBeli()));

                                if (detail.GetHargaDiskonSaatBeli().HasValue)
                                {
                                    cmdDetail.Parameters.AddWithValue("@hargaDiskon", Convert.ToInt32(detail.GetHargaDiskonSaatBeli().Value));
                                }
                                else
                                {
                                    cmdDetail.Parameters.AddWithValue("@hargaDiskon", DBNull.Value);
                                }

                                int rowDetail = cmdDetail.ExecuteNonQuery();
                                if (rowDetail == 0) throw new InvalidOrderException("Gagal menyimpan detail item transaksi.", "details", "DB_INSERT_DETAIL_FAILED");
                            }
                        }

                        dbTx.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTx.Rollback();
                        throw new InvalidOrderException("Checkout dibatalkan (Rollback) karena error DB: " + ex.Message, "", "DB_TX_ROLLBACK");
                    }
                }
            }

            return idTransaksiBaru;
        }


        // =======================================================
        // HELPER PRIVATE
        // =======================================================

        /// <summary>
        /// Merestore status transaksi dari database langsung ke field tanpa melalui
        /// state machine UbahStatus() — digunakan saat loading data dari DB.
        /// 
        /// PERBAIKAN: UbahStatus() dirancang untuk mutasi bisnis (validasi transisi),
        /// bukan untuk hydration objek dari DB. Memisahkan keduanya adalah praktik OOP
        /// yang benar (Separation of Concerns).
        /// 
        /// Jika status DB valid ("Menunggu", "Diproses", "Selesai", "Dibatalkan"),
        /// gunakan state machine normal. Untuk "Menunggu" yang merupakan nilai default,
        /// tidak perlu memanggil UbahStatus sama sekali.
        /// </summary>
        private void SetStatusDariDatabase(Transaction transaksi, string statusDb)
        {
            // Status "Menunggu" adalah default konstruktor Transaction, tidak perlu diubah.
            if (statusDb == "Menunggu") return;

            // Untuk status lain, gunakan mekanisme yang konsisten.
            // Kita perlu bypass state machine untuk hydration dari DB.
            // Solusi: manfaatkan state machine secara berurutan dari "Menunggu".
            if (statusDb == "Diproses" || statusDb == "Dibatalkan")
            {
                transaksi.UbahStatus(statusDb);
            }
            else if (statusDb == "Selesai")
            {
                // Untuk mencapai "Selesai", harus lewat "Diproses" dulu
                transaksi.UbahStatus("Diproses");
                transaksi.UbahStatus("Selesai");
            }
            // Status tidak dikenal dibiarkan "Menunggu" (default)
        }
    }
}
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

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


        /// <summary>
        /// Mengambil semua transaksi milik satu pembeli (id_koordinator).
        /// Digunakan oleh RiwayatPesananControl.
        /// Catatan: Detail setiap transaksi juga di-hydrate agar HitungTotal() akurat.
        /// </summary>
        public List<Transaction> GetByIdPembeli(int idPembeli)
        {
            List<Transaction> listTransaksi = new List<Transaction>();
            string query = @"SELECT id_transaksi, id_koordinator, tanggal_transaksi, status_pesanan, is_valid
                             FROM transactions
                             WHERE id_koordinator = @idPembeli
                             ORDER BY tanggal_transaksi DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPembeli", idPembeli);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idPembeliDb = reader.GetInt32(reader.GetOrdinal("id_koordinator"));
                            Transaction transaksi = new Transaction(idPembeliDb);
                            transaksi.SetIdTransaksi(reader.GetInt32(reader.GetOrdinal("id_transaksi")));

                            string statusDb = reader.GetString(reader.GetOrdinal("status_pesanan"));
                            SetStatusDariDatabase(transaksi, statusDb);

                            if (!reader.IsDBNull(reader.GetOrdinal("is_valid")) && reader.GetBoolean(reader.GetOrdinal("is_valid")))
                            {
                                transaksi.Approve();
                            }

                            // Set tanggal dari DB
                            if (!reader.IsDBNull(reader.GetOrdinal("tanggal_transaksi")))
                            {
                                transaksi.SetTanggalTransaksi(reader.GetDateTime(reader.GetOrdinal("tanggal_transaksi")));
                            }

                            // Simpan tanggal menggunakan reflection-free approach
                            // (tanggal sudah di-set oleh konstruktor; kita set ulang melalui GetById jika perlu)
                            listTransaksi.Add(transaksi);
                        }
                    }
                }
            }

            // Hydrate detail setiap transaksi agar JumlahItem dan HitungTotal() akurat
            string queryDetail = @"SELECT id_produk, nama_penitip, jumlah_pesanan, catatan,
                                          nama_produk_snapshot, harga_satuan_saat_beli,
                                          harga_diskon_saat_beli, selisih_refund
                                   FROM transaction_details
                                   WHERE id_transaksi = @idTrx;";

            foreach (Transaction trx in listTransaksi)
            {
                using (NpgsqlConnection conn2 = new NpgsqlConnection(_connectionString))
                {
                    conn2.Open();
                    using (NpgsqlCommand cmd2 = new NpgsqlCommand(queryDetail, conn2))
                    {
                        cmd2.Parameters.AddWithValue("@idTrx", trx.GetIdTransaksi());
                        using (NpgsqlDataReader reader2 = cmd2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                int idProduk = reader2.GetInt32(reader2.GetOrdinal("id_produk"));
                                string penitip = reader2.GetString(reader2.GetOrdinal("nama_penitip"));
                                int jumlah = reader2.GetInt32(reader2.GetOrdinal("jumlah_pesanan"));

                                TransactionDetail detail = new TransactionDetail(idProduk, penitip, jumlah);

                                long hargaSatuan = Convert.ToInt64(reader2.GetInt32(reader2.GetOrdinal("harga_satuan_saat_beli")));
                                long? hargaDiskon = null;
                                if (!reader2.IsDBNull(reader2.GetOrdinal("harga_diskon_saat_beli")))
                                    hargaDiskon = Convert.ToInt64(reader2.GetInt32(reader2.GetOrdinal("harga_diskon_saat_beli")));

                                detail.FinalisasiHargaSaatCheckout(hargaSatuan, hargaDiskon);

                                if (!reader2.IsDBNull(reader2.GetOrdinal("catatan")))
                                    detail.SetCatatan(reader2.GetString(reader2.GetOrdinal("catatan")));

                                if (!reader2.IsDBNull(reader2.GetOrdinal("nama_produk_snapshot")))
                                    detail.SetNamaProdukSnapshot(reader2.GetString(reader2.GetOrdinal("nama_produk_snapshot")));

                                trx.TambahDetail(detail);
                            }
                        }
                    }
                }
            }

            return listTransaksi;
        }


        // =======================================================
        // IMPLEMENTASI ICommandRepository<Transaction>
        // =======================================================

        public void Insert(Transaction transaksi)
        {
            if (transaksi == null)
            {
                throw new ArgumentNullException(nameof(transaksi), "Data transaksi tidak boleh kosong!");
            }

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert ke tabel induk 'transactions'
                        // Catatan: Kolom di database namanya 'id_koordinator', tapi di C# Model kita pakai 'GetIdPembeli()'
                        string insertHeaderQuery = @"
                            INSERT INTO transactions (id_koordinator, tanggal_transaksi, status_pesanan, bukti_bayar, is_valid)
                            VALUES (@koord, CURRENT_TIMESTAMP, @status, @bukti, FALSE)
                            RETURNING id_transaksi;";

                        int idTransaksiGenerated = 0;
                        using (var cmdHeader = new NpgsqlCommand(insertHeaderQuery, conn, dbTx))
                        {
                            cmdHeader.Parameters.AddWithValue("@koord", transaksi.GetIdPembeli());
                            cmdHeader.Parameters.AddWithValue("@status", transaksi.GetStatus());

                            // Cek apakah ada bukti bayar (BYTEA), jika null kirim DBNull ke database
                            byte[] buktiBayar = transaksi.GetBuktiBayar();
                            if (buktiBayar != null && buktiBayar.Length > 0)
                            {
                                cmdHeader.Parameters.AddWithValue("@bukti", buktiBayar);
                            }
                            else
                            {
                                cmdHeader.Parameters.AddWithValue("@bukti", DBNull.Value);
                            }

                            // Eksekusi dan ambil ID transaksi yang baru saja ter-generate
                            idTransaksiGenerated = Convert.ToInt32(cmdHeader.ExecuteScalar());

                            // Set ID transaksi balik ke object RAM
                            transaksi.SetIdTransaksi(idTransaksiGenerated);
                        }

                        // 2. Loop insert setiap detail item (Titipan) ke 'transaction_details'
                        // Catatan DB: snapshot nama produk, harga beli, dan diskon akan OTOMATIS diisi 
                        // oleh TRIGGER database (t_before_insert_detail) yang sudah dibuat sebelumnya.
                        string insertDetailQuery = @"
                            INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan)
                            VALUES (@idTrx, @idProduk, @penitip, @jumlah, @catatan);";

                        foreach (TransactionDetail detail in transaksi.GetSemuaDetail())
                        {
                            using (var cmdDetail = new NpgsqlCommand(insertDetailQuery, conn, dbTx))
                            {
                                cmdDetail.Parameters.AddWithValue("@idTrx", idTransaksiGenerated);
                                cmdDetail.Parameters.AddWithValue("@idProduk", detail.GetIdProduk());
                                cmdDetail.Parameters.AddWithValue("@penitip", detail.GetNamaPenitip());
                                cmdDetail.Parameters.AddWithValue("@jumlah", detail.GetJumlahPesanan());

                                // Handling catatan opsional (bisa null)
                                if (string.IsNullOrWhiteSpace(detail.GetCatatan()))
                                {
                                    cmdDetail.Parameters.AddWithValue("@catatan", DBNull.Value);
                                }
                                else
                                {
                                    cmdDetail.Parameters.AddWithValue("@catatan", detail.GetCatatan());
                                }

                                cmdDetail.ExecuteNonQuery();
                            }
                        }

                        // 3. Commit transaksi jika SELURUH header & detail sukses tanpa error
                        dbTx.Commit();
                    }
                    catch (Exception)
                    {
                        // 4. Rollback jika ada error di tengah jalan, agar data tidak masuk setengah-setengah
                        dbTx.Rollback();
                        throw;
                    }
                }
            }
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
                        // 1. Insert Header Transaksi
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

                        // 2. Insert Detail Transaksi 
                        // PERBAIKAN: Hanya insert kolom bisnis utama! 
                        // Snapshot harga dan nama akan OTOMATIS diisi oleh Trigger PostgreSQL (t_before_insert_detail)
                        string queryDetail = "INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan) VALUES (@trx, @produk, @penitip, @jumlah, @catatan);";

                        foreach (TransactionDetail detail in transaksi.GetSemuaDetail())
                        {
                            using (NpgsqlCommand cmdDetail = new NpgsqlCommand(queryDetail, conn, dbTx))
                            {
                                cmdDetail.Parameters.AddWithValue("@trx", idTransaksiBaru);
                                cmdDetail.Parameters.AddWithValue("@produk", detail.GetIdProduk());
                                cmdDetail.Parameters.AddWithValue("@penitip", detail.GetNamaPenitip());
                                cmdDetail.Parameters.AddWithValue("@jumlah", detail.GetJumlahPesanan());
                                cmdDetail.Parameters.AddWithValue("@catatan", string.IsNullOrEmpty(detail.GetCatatan()) ? (object)DBNull.Value : detail.GetCatatan());

                                // Parameter snapshot di-HAPUS dari sini karena sudah diserahkan ke Trigger Database.

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
        public int GetActiveTransactionCount(int idKoordinator)
        {
            string query = "SELECT COUNT(*) FROM transactions WHERE id_koordinator = @id AND status_pesanan != 'Selesai';";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idKoordinator);
                    object result = cmd.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }
        public DataTable GetPesananMasukDataTable(int idPenjual)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT DISTINCT t.id_transaksi, u.nama AS nama_pembeli, t.tanggal_transaksi, t.status_pesanan,
                       (SELECT COALESCE(SUM(td2.jumlah_pesanan * td2.harga_satuan_saat_beli), 0)
                        FROM transaction_details td2
                        JOIN products p2 ON td2.id_produk = p2.id_produk
                        WHERE td2.id_transaksi = t.id_transaksi AND p2.id_penjual = @idPenjual) AS total_harga_lapak
                FROM transactions t
                JOIN users u ON t.id_koordinator = u.id_user
                JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
                JOIN products p ON td.id_produk = p.id_produk
                WHERE p.id_penjual = @idPenjual
                ORDER BY t.tanggal_transaksi DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPenjual", idPenjual);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Mengupdate status pesanan di database.
        /// </summary>
        public bool UpdateStatusPesanan(int idTransaksi, string statusBaru)
        {
            string query = "UPDATE transactions SET status_pesanan = @status WHERE id_transaksi = @id;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", statusBaru);
                    cmd.Parameters.AddWithValue("@id", idTransaksi);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public DataTable GetRiwayatPesananDataTable(int idKoordinator)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT id_transaksi, tanggal_transaksi, total_tagihan, total_cashback, status_pesanan 
                FROM vw_transaksi_lengkap 
                WHERE id_koordinator = @id 
                ORDER BY tanggal_transaksi DESC;";

            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idKoordinator);
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Mengambil data leaderboard penjual berdasarkan total omzet bersih.
        /// Digunakan oleh DashboardAdminControl untuk menampilkan ranking penjual.
        /// </summary>
        public DataTable GetLeaderboardPenjual()
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT
            u.nama AS nama_penjual,
            COALESCE(SUM(
                (td.jumlah_pesanan * td.harga_satuan_saat_beli)
                - COALESCE(td.selisih_refund, 0)
            ), 0) AS total_omzet_bersih,
            CASE
                WHEN COALESCE(SUM(
                    (td.jumlah_pesanan * td.harga_satuan_saat_beli)
                    - COALESCE(td.selisih_refund, 0)
                ), 0) >= 500000 THEN '👑 Seller Sultan'
                WHEN COALESCE(SUM(
                    (td.jumlah_pesanan * td.harga_satuan_saat_beli)
                    - COALESCE(td.selisih_refund, 0)
                ), 0) >= 100000 THEN '⭐ Seller Menengah'
                ELSE '🌱 Seller Newbie'
            END AS tier_penjual
        FROM transaction_details td
        JOIN products p ON td.id_produk = p.id_produk
        JOIN users    u ON p.id_penjual = u.id_user
        GROUP BY u.nama
        ORDER BY total_omzet_bersih DESC;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }
    }
}
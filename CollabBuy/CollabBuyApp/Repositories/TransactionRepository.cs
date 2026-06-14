using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories.Interfaces;
using CollabBuy.CollabBuyApp.Exceptions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class TransactionRepository : BaseRepository, IQueryRepository<Transaction>, IQueryAllRepository<Transaction>, ICommandRepository<Transaction>
    {
        public TransactionRepository() : base() { }


        // =======================================================
        // IMPLEMENTASI IQueryRepository<Transaction>
        // =======================================================

        public Transaction GetById(int idTransaksi)
        {
            Transaction transaksi = null;
            string query = "SELECT * FROM fn_transaksi_by_id(@id);";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idTransaksi);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (transaksi == null)
                            {
                                transaksi = new Transaction(reader.GetInt32(reader.GetOrdinal("id_koordinator")));
                                transaksi.IdTransaksi = reader.GetInt32(reader.GetOrdinal("id_transaksi"));
                                SetStatusDariDatabase(transaksi, reader.GetString(reader.GetOrdinal("status_pesanan")));
                                if (!reader.IsDBNull(reader.GetOrdinal("is_valid")) && reader.GetBoolean(reader.GetOrdinal("is_valid")))
                                    transaksi.Approve();
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("id_produk")))
                            {
                                var detail = new TransactionDetail(
                                    reader.GetInt32(reader.GetOrdinal("id_produk")),
                                    reader.GetString(reader.GetOrdinal("nama_penitip")),
                                    reader.GetInt32(reader.GetOrdinal("jumlah_pesanan")));

                                long hargaSatuan = reader.IsDBNull(reader.GetOrdinal("harga_satuan_saat_beli"))
                                    ? 0 : Convert.ToInt64(reader.GetInt32(reader.GetOrdinal("harga_satuan_saat_beli")));
                                long? hargaDiskon = reader.IsDBNull(reader.GetOrdinal("harga_diskon_saat_beli"))
                                    ? (long?)null : Convert.ToInt64(reader.GetInt32(reader.GetOrdinal("harga_diskon_saat_beli")));

                                detail.FinalisasiHargaSaatCheckout(hargaSatuan > 0 ? hargaSatuan : 1, hargaDiskon);
                                if (!reader.IsDBNull(reader.GetOrdinal("catatan")))
                                    detail.Catatan = reader.GetString(reader.GetOrdinal("catatan"));
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
            var listTransaksi = new List<Transaction>();
            string query = "SELECT id_transaksi, id_koordinator, tanggal_transaksi, status_pesanan, is_valid FROM transactions ORDER BY tanggal_transaksi DESC;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var transaksi = new Transaction(reader.GetInt32(reader.GetOrdinal("id_koordinator")));
                        transaksi.IdTransaksi = reader.GetInt32(reader.GetOrdinal("id_transaksi"));
                        // PERBAIKAN: Restore status dari DB tanpa state machine
                        SetStatusDariDatabase(transaksi, reader.GetString(reader.GetOrdinal("status_pesanan")));
                        if (!reader.IsDBNull(reader.GetOrdinal("is_valid")) && reader.GetBoolean(reader.GetOrdinal("is_valid")))
                            transaksi.Approve();
                        listTransaksi.Add(transaksi);
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
            var listTransaksi = new List<Transaction>();
            string query = "SELECT * FROM fn_transaksi_lengkap_pembeli(@idPembeli);";

            var rawRows = new Dictionary<int, (Transaction trx, List<DataRow> rows)>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPembeli", idPembeli);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idTrx = reader.GetInt32(reader.GetOrdinal("id_transaksi"));

                            if (!rawRows.ContainsKey(idTrx))
                            {
                                var trx = new Transaction(reader.GetInt32(reader.GetOrdinal("id_koordinator")));
                                trx.IdTransaksi = idTrx;
                                SetStatusDariDatabase(trx, reader.GetString(reader.GetOrdinal("status_pesanan")));
                                if (!reader.IsDBNull(reader.GetOrdinal("is_valid")) && reader.GetBoolean(reader.GetOrdinal("is_valid")))
                                    trx.Approve();
                                if (!reader.IsDBNull(reader.GetOrdinal("tanggal_transaksi")))
                                    trx.TanggalTransaksi = reader.GetDateTime(reader.GetOrdinal("tanggal_transaksi"));
                                if (!reader.IsDBNull(reader.GetOrdinal("bukti_bayar")))
                                    trx.BuktiBayar = (byte[])reader["bukti_bayar"];
                                rawRows[idTrx] = (trx, new List<DataRow>());
                                listTransaksi.Add(trx);
                            }

                            // Hydrate detail jika ada (LEFT JOIN bisa null kalau transaksi tidak punya detail)
                            if (!reader.IsDBNull(reader.GetOrdinal("id_produk")))
                            {
                                try
                                {
                                    var detail = new TransactionDetail(
                                        reader.GetInt32(reader.GetOrdinal("id_produk")),
                                        reader.GetString(reader.GetOrdinal("nama_penitip")),
                                        reader.GetInt32(reader.GetOrdinal("jumlah_pesanan")));

                                    long hargaSatuan = reader.IsDBNull(reader.GetOrdinal("harga_satuan_saat_beli"))
                                        ? 0
                                        : Convert.ToInt64(reader.GetInt32(reader.GetOrdinal("harga_satuan_saat_beli")));

                                    long? hargaDiskon = null;
                                    if (!reader.IsDBNull(reader.GetOrdinal("harga_diskon_saat_beli")))
                                        hargaDiskon = Convert.ToInt64(reader.GetInt32(reader.GetOrdinal("harga_diskon_saat_beli")));

                                    string namaSnap = reader.IsDBNull(reader.GetOrdinal("nama_produk_snapshot"))
                                        ? "-"
                                        : reader.GetString(reader.GetOrdinal("nama_produk_snapshot"));

                                    detail.IsiHargaDariDatabase(hargaSatuan, hargaDiskon, namaSnap);

                                    long selisihRefund = reader.IsDBNull(reader.GetOrdinal("selisih_refund"))
                                        ? 0
                                        : Convert.ToInt64(reader.GetInt32(reader.GetOrdinal("selisih_refund")));
                                    if (selisihRefund > 0)
                                        detail.SetSelisihRefundDariDatabase(selisihRefund);

                                    if (!reader.IsDBNull(reader.GetOrdinal("catatan")))
                                        detail.Catatan = reader.GetString(reader.GetOrdinal("catatan"));

                                    rawRows[idTrx].trx.TambahDetail(detail);
                                }
                                catch { /* Skip baris corrupt */ }
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
            if (transaksi == null) throw new ArgumentNullException(nameof(transaksi), "Data transaksi tidak boleh kosong!");

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

                        int idTransaksiGenerated;
                        using (var cmdHeader = new NpgsqlCommand(insertHeaderQuery, conn, dbTx))
                        {
                            cmdHeader.Parameters.AddWithValue("@koord", transaksi.IdPembeli);
                            cmdHeader.Parameters.AddWithValue("@status", transaksi.GetStatus());
                            byte[] buktiBayar = transaksi.BuktiBayar;
                            cmdHeader.Parameters.AddWithValue("@bukti", (buktiBayar != null && buktiBayar.Length > 0) ? (object)buktiBayar : DBNull.Value);
                            idTransaksiGenerated = Convert.ToInt32(cmdHeader.ExecuteScalar());
                            transaksi.IdTransaksi =idTransaksiGenerated;
                        }

                        // 2. Loop insert setiap detail item (Titipan) ke 'transaction_details'
                        // Catatan DB: snapshot nama produk, harga beli, dan diskon akan OTOMATIS diisi 
                        // oleh TRIGGER database (t_before_insert_detail) yang sudah dibuat sebelumnya.
                        string insertDetailQuery = @"
                            INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan)
                            VALUES (@idTrx, @idProduk, @penitip, @jumlah, @catatan);";

                        foreach (var detail in transaksi.Details)
                        {
                            using (var cmdDetail = new NpgsqlCommand(insertDetailQuery, conn, dbTx))
                            {
                                cmdDetail.Parameters.AddWithValue("@idTrx", idTransaksiGenerated);
                                cmdDetail.Parameters.AddWithValue("@idProduk", detail.IdProduk);
                                cmdDetail.Parameters.AddWithValue("@penitip", detail.NamaPenitip);
                                cmdDetail.Parameters.AddWithValue("@jumlah", detail.JumlahPesanan);
                                cmdDetail.Parameters.AddWithValue("@catatan", string.IsNullOrWhiteSpace(detail.Catatan) ? (object)DBNull.Value : detail.Catatan);
                                cmdDetail.ExecuteNonQuery();
                            }
                        }

                        // 3. Commit transaksi jika SELURUH header & detail sukses tanpa error
                        dbTx.Commit();
                    }
                    catch
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

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@status", entity.GetStatus());
                    cmd.Parameters.AddWithValue("@isValid", entity.GetStatusPersetujuan());
                    cmd.Parameters.AddWithValue("@id", entity.IdTransaksi);
                    cmd.Parameters.AddWithValue("@bukti", (object)entity.BuktiBayar ?? DBNull.Value);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOrderException("Gagal mengupdate transaksi, ID tidak ditemukan di database.", "id_transaksi", "DB_UPDATE_FAILED");
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

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var dbTx = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert Header Transaksi
                        string queryHeader = "INSERT INTO transactions (id_koordinator, bukti_bayar, status_pesanan) VALUES (@koord, @bukti, @status) RETURNING id_transaksi;";

                        using (var cmdHeader = new NpgsqlCommand(queryHeader, conn, dbTx))
                        {
                            cmdHeader.Parameters.AddWithValue("@koord", transaksi.IdPembeli);
                            cmdHeader.Parameters.AddWithValue("@bukti", (object)transaksi.BuktiBayar ?? DBNull.Value);
                            cmdHeader.Parameters.AddWithValue("@status", transaksi.GetStatus());

                            object result = cmdHeader.ExecuteScalar();
                            if (result == null || result == DBNull.Value)
                                throw new InvalidOrderException("Gagal mendapatkan ID Transaksi dari database.", "", "DB_INSERT_HEADER_FAILED");
                            idTransaksiBaru = Convert.ToInt32(result);
                            transaksi.IdTransaksi = idTransaksiBaru;
                        }

                        // 2. Insert Detail Transaksi 
                        // PERBAIKAN: Hanya insert kolom bisnis utama! 
                        // Snapshot harga dan nama akan OTOMATIS diisi oleh Trigger PostgreSQL (t_before_insert_detail)
                        string queryDetail = "INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan) VALUES (@trx, @produk, @penitip, @jumlah, @catatan);";

                        foreach (var detail in transaksi.Details)
                        {
                            using (var cmdDetail = new NpgsqlCommand(queryDetail, conn, dbTx))
                            {
                                cmdDetail.Parameters.AddWithValue("@trx", idTransaksiBaru);
                                cmdDetail.Parameters.AddWithValue("@produk", detail.IdProduk);
                                cmdDetail.Parameters.AddWithValue("@penitip", detail.NamaPenitip);
                                cmdDetail.Parameters.AddWithValue("@jumlah", detail.JumlahPesanan);
                                cmdDetail.Parameters.AddWithValue("@catatan", string.IsNullOrEmpty(detail.Catatan) ? (object)DBNull.Value : detail.Catatan);
                                if (cmdDetail.ExecuteNonQuery() == 0)
                                    throw new InvalidOrderException("Gagal menyimpan detail item transaksi.", "details", "DB_INSERT_DETAIL_FAILED");
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
        /// </summary>
        private void SetStatusDariDatabase(Transaction transaksi, string statusDb)
        {
            // Status "Menunggu" adalah default konstruktor Transaction, tidak perlu diubah.
            if (statusDb == "Menunggu") return;

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

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
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
            SELECT id_transaksi, nama_pembeli, nomor_telepon, tanggal_transaksi,
                   status_pesanan, total_harga_lapak
            FROM vw_pesanan_masuk_penjual
            WHERE id_penjual = @idPenjual
            ORDER BY tanggal_transaksi DESC;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPenjual", idPenjual);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        public bool UpdateStatusPesanan(int idTransaksi, string statusBaru)
        {
            string query = "UPDATE transactions SET status_pesanan = @status WHERE id_transaksi = @id;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
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
                SELECT id_transaksi, tanggal_transaksi, total_tagihan,
                       total_cashback, status_pesanan, bukti_bayar
                FROM vw_transaksi_lengkap
                WHERE id_koordinator = @id
                ORDER BY tanggal_transaksi DESC;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idKoordinator);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetLeaderboardPenjual()
        {
            DataTable dt = new DataTable();
            string query = "SELECT nama_penjual, total_omzet_bersih, tier_penjual FROM vw_leaderboard_penjual;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var da = new NpgsqlDataAdapter(cmd))
                    da.Fill(dt);
            }
            return dt;
        }


        /// <summary>
        /// Detail rincian pesanan untuk tampilan pembeli (koordinator) — semua produk dalam transaksi.
        /// </summary>
        public DataTable GetDetailPesananPembeli(int idTransaksi)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT tanggal_transaksi, status_pesanan, bukti_bayar,
                       nama_produk, nama_penitip, jumlah, harga_satuan,
                       subtotal, catatan, selisih_refund
                FROM vw_detail_pesanan_pembeli
                WHERE id_transaksi = @idTrx
                ORDER BY nama_penitip, nama_produk;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idTrx", idTransaksi);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }


        // =======================================================
        // METHOD BARU: GET DETAIL PESANAN UNTUK HALAMAN PENJUAL
        // Mengambil header transaksi (termasuk bukti_bayar) dan
        // detail item yang merupakan produk milik penjual tsb.
        // =======================================================
        public DataTable GetDetailPesananPenjual(int idTransaksi, int idPenjual)
        {
            DataTable dt = new DataTable();
            // Sebelumnya: 11 dt.Columns.Add manual + ExecuteReader loop
            // Sekarang: NpgsqlDataAdapter.Fill — kolom otomatis dari view
            string query = @"
                SELECT id_produk, nama_pembeli, nomor_telepon, tanggal_transaksi, status_pesanan, bukti_bayar,
                       nama_produk, nama_penitip, jumlah, harga_satuan,
                       subtotal, catatan, selisih_refund
                FROM vw_detail_pesanan_penjual
                WHERE id_transaksi = @idTrx
                  AND id_penjual   = @idPenjual;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idTrx", idTransaksi);
                    cmd.Parameters.AddWithValue("@idPenjual", idPenjual);
                    using (var da = new NpgsqlDataAdapter(cmd)) da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Ambil total jumlah_pesanan dari DB untuk satu produk (semua transaksi non-batal).
        /// Digunakan untuk cek kuota GR secara akurat tanpa bergantung pada data RAM.
        /// </summary>
        public int GetTotalTerpesanProduk(int idProduk, int idPo)
        {
            try
            {
                string query = @"
            SELECT COALESCE(SUM(td.jumlah_pesanan), 0)
            FROM transaction_details td
            JOIN transactions t ON td.id_transaksi = t.id_transaksi
            WHERE td.id_produk      = @idProduk
              AND td.id_po_saat_beli = @idPo
              AND t.status_pesanan  NOT IN ('Dibatalkan', 'Batal', 'Gagal');";

                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idProduk", idProduk);
                        cmd.Parameters.AddWithValue("@idPo", idPo);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[GetTotalTerpesanProduk] ERROR: {ex.Message}");
                return 0;
            }
        }

        public (bool sukses, string pesan) RecalculateCashbackGotongRoyong(int idProduk, int idPo, long hargaDasar, long hargaDiskon)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("CALL sp_recalculate_cashback_gr(@idProduk, @idPo, @hargaDasar, @hargaDiskon, @sukses, @pesan);", conn))
                    {
                        cmd.Parameters.AddWithValue("@idProduk", idProduk);
                        cmd.Parameters.AddWithValue("@idPo", idPo);
                        cmd.Parameters.AddWithValue("@hargaDasar", hargaDasar);
                        cmd.Parameters.AddWithValue("@hargaDiskon", hargaDiskon);

                        var pSukses = new NpgsqlParameter("@sukses", NpgsqlTypes.NpgsqlDbType.Boolean)
                        { Direction = System.Data.ParameterDirection.InputOutput, Value = false };
                        var pPesan = new NpgsqlParameter("@pesan", NpgsqlTypes.NpgsqlDbType.Text)
                        { Direction = System.Data.ParameterDirection.InputOutput, Value = "" };
                        cmd.Parameters.Add(pSukses);
                        cmd.Parameters.Add(pPesan);
                        cmd.ExecuteNonQuery();

                        return (Convert.ToBoolean(pSukses.Value), pPesan.Value?.ToString() ?? "");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "Gagal update cashback: " + ex.Message);
            }
        }
    }
}
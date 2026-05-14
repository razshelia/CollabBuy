using System;
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

        public bool BuatTransaksi(int idUser, int idPo, int jumlahPesanan, string pathBukti)
        {
            NpgsqlConnection koneksi = this.dbHelper.AmbilKoneksi();

            if (koneksi == null)
            {
                return false;
            }
            else
            {
                try
                {
                    koneksi.Open();
                    // Memanggil Stored Procedure dari PostgreSQL
                    string sql = "CALL sp_buat_transaksi(@p_id_user, @p_id_po, @p_qty, @p_bukti)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, koneksi))
                    {
                        cmd.Parameters.AddWithValue("p_id_user", idUser);
                        cmd.Parameters.AddWithValue("p_id_po", idPo);
                        cmd.Parameters.AddWithValue("p_qty", jumlahPesanan);
                        cmd.Parameters.AddWithValue("p_bukti", pathBukti);

                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception)
                {
                    // Jika gagal (misal stok kurang, Trigger akan menolak), akan return false
                    return false;
                }
                finally
                {
                    if (koneksi.State == System.Data.ConnectionState.Open)
                    {
                        koneksi.Close();
                    }
                    else
                    {
                        // Sudah tertutup
                    }
                }
            }
        }

        // ... Implementasi method ICheckoutRepository lainnya (AmbilRiwayatPesanan, BatalkanPesanan, dll)
        // ... Pola try-catch-finally dan if-else tetap sama persis seperti di atas.
        public System.Collections.Generic.List<Models.Checkout> AmbilRiwayatPesanan(int idUser)
        {
            return new System.Collections.Generic.List<Models.Checkout>();
        }

        public System.Collections.Generic.List<Models.Checkout> AmbilPesananMasuk(int idSeller)
        {
            return new System.Collections.Generic.List<Models.Checkout>();
        }

        public bool ValidasiPembayaran(int idCheckout)
        {
            return false;
        }

        public bool UbahStatusSelesai(int idCheckout)
        {
            return false;
        }

        public bool BatalkanPesanan(int idCheckout)
        {
            return false;
        }
    }
}
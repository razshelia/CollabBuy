using System;
using System.Collections.Generic;
using Npgsql;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Repositories
{
    public class VerificationRepository : BaseRepository, IVerificationRepository
    {
        public bool AjukanVerifikasi(Verification verif)
        {
            string sql = @"INSERT INTO verifications (id_user, nim, nama_toko, bukti_ktm, tahun_masuk, is_verifikasi)
                           VALUES (@idUser, @nim, @toko, @ktm, @tahun, false)";
            int row = ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("idUser", verif.IdUser);
                cmd.Parameters.AddWithValue("nim", verif.Nim);
                cmd.Parameters.AddWithValue("toko", verif.NamaToko);
                cmd.Parameters.AddWithValue("ktm", verif.BuktiKtm);
                cmd.Parameters.AddWithValue("tahun", verif.TahunMasuk);
            });

            return row > 0;
        }

        public List<Verification> AmbilPengajuanPending()
        {
            List<Verification> list = new List<Verification>();
            string sql = @"SELECT id_verifikasi, id_user, nim, nama_toko, bukti_ktm, tahun_masuk, is_verifikasi
                           FROM verifications WHERE is_verifikasi = false";
            ExecuteQuery(sql, null, reader =>
            {
                list.Add(MapVerification(reader));
            });

            return list;
        }

        public bool SetujuiVerifikasi(int idVerifikasi)
        {
            string sql = "UPDATE verifications SET is_verifikasi = true WHERE id_verifikasi = @id";

            int row = ExecuteNonQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idVerifikasi));

            return row > 0;
        }

        public bool TolakVerifikasi(int idVerifikasi)
        {
            string sql = "DELETE FROM verifications WHERE id_verifikasi = @id";

            int row = ExecuteNonQuery(sql, cmd => cmd.Parameters.AddWithValue("id", idVerifikasi));

            return row > 0;
        }

        public Verification AmbilVerifikasiByUser(int idUser)
        {
            Verification v = null;
            string sql = "SELECT id_verifikasi, id_user, nim, nama_toko, bukti_ktm, tahun_masuk, is_verifikasi FROM verifications WHERE id_user = @idUser";

            ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("idUser", idUser), reader =>
            {
                v = MapVerification(reader);
            });

            return v;
        }

        private Verification MapVerification(NpgsqlDataReader reader)
        {
            Verification v = new Verification();
            v.IdVerifikasi = reader.GetInt32(0);
            v.IdUser = reader.GetInt32(1);
            v.Nim = reader.GetString(2);
            v.NamaToko = reader.GetString(3);
            v.BuktiKtm = reader.GetString(4);
            v.TahunMasuk = reader.GetInt32(5);
            v.IsVerifikasi = reader.GetBoolean(6);
            return v;
        }
    }
}
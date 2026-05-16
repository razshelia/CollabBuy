using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transRepo;
        public TransactionService(ITransactionRepository transRepo)
        {
            _transRepo = transRepo;
        }

        public int BuatTransaksi(int idKoordinator, int totalBayarGrup, List<TransactionDetail> details)
        {
            if (details == null || details.Count == 0)
            {
                UXHelper.TampilkanError("Minimal satu detail pesanan harus ditambahkan.");
                return -1;
            }

            foreach (var d in details)
            {
                if (string.IsNullOrWhiteSpace(d.NamaPenitip)) { UXHelper.TampilkanError("Nama penitip wajib diisi."); return -1; }
                if (d.JumlahPesanan < 1) { UXHelper.TampilkanError("Jumlah pesanan harus ≥ 1."); return -1; }
                if (d.IdProduk <= 0) { UXHelper.TampilkanError("Produk tidak valid."); return -1; }
            }

            Transaction transaksi = new Transaction();
            transaksi.IdKoordinator = idKoordinator;
            transaksi.TotalBayarGrup = totalBayarGrup;
            transaksi.StatusPesanan = StatusTransaksi.Menunggu;

            try
            {
                int idTransaksi = _transRepo.BuatTransaksi(transaksi, details);
                if (idTransaksi > 0)
                    UXHelper.TampilkanSukses("Transaksi berhasil dibuat. Silakan upload bukti bayar.");
                else
                    UXHelper.TampilkanError("Gagal membuat transaksi.");
                return idTransaksi;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return -1;
            }
        }

        public bool ValidasiPembayaran(int idTransaksi, string pathBukti)
        {
            if (string.IsNullOrWhiteSpace(pathBukti))
            {
                UXHelper.TampilkanError("Bukti pembayaran wajib diunggah.");
                return false;
            }

            try
            {
                bool sukses = _transRepo.ValidasiPembayaran(idTransaksi, pathBukti);
                if (sukses) UXHelper.TampilkanSukses("Pembayaran berhasil divalidasi.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public bool UbahStatusPesanan(int idTransaksi, string statusBaru)
        {
            if (statusBaru != StatusTransaksi.Menunggu && statusBaru != StatusTransaksi.Diproses && statusBaru != StatusTransaksi.Selesai)
            {
                UXHelper.TampilkanError("Status tidak valid.");
                return false;
            }

            try
            {
                bool sukses = _transRepo.UbahStatusPesanan(idTransaksi, statusBaru);
                if (sukses) UXHelper.TampilkanSukses($"Status berhasil diubah menjadi {statusBaru}.");
                return sukses;
            }
            catch (Exception ex)
            {
                UXHelper.TampilkanError(ex.Message);
                return false;
            }
        }

        public List<Transaction> AmbilRiwayatKoordinator(int idKoordinator)
        {
            try { return _transRepo.AmbilRiwayatKoordinator(idKoordinator); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Transaction>(); }
        }

        public List<Transaction> AmbilPesananMasukPenjual(int idPenjual)
        {
            try { return _transRepo.AmbilPesananMasukPenjual(idPenjual); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<Transaction>(); }
        }

        public List<TransactionDetail> AmbilDetailTransaksi(int idTransaksi)
        {
            try { return _transRepo.AmbilDetailTransaksi(idTransaksi); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return new List<TransactionDetail>(); }
        }

        public Transaction AmbilTransaksiById(int idTransaksi)
        {
            try { return _transRepo.AmbilTransaksiById(idTransaksi); }
            catch (Exception ex) { UXHelper.TampilkanError(ex.Message); return null; }
        }

        public int AmbilJumlahTransaksi()
        {
            try { return _transRepo.AmbilJumlahTransaksi(); }
            catch { return 0; } // Untuk dashboard admin, biarkan 0 jika error
        }
    }
}
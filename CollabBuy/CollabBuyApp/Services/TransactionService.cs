using System;
using System.Collections.Generic;
using CollabBuy.CollabBuyApp.Helpers;
using CollabBuy.CollabBuyApp.Interfaces;
using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;

namespace CollabBuy.CollabBuyApp.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transRepo;

        public TransactionService()
        {
            _transRepo = new TransactionRepository();
        }

        // ── 1. Buat transaksi baru (checkout) ──
        public int BuatTransaksi(int idKoordinator, int totalBayarGrup, List<TransactionDetail> details)
        {
            if (details == null || details.Count == 0)
            {
                UXHelper.TampilkanError("Minimal satu detail pesanan harus ditambahkan.");
                return -1;
            }

            // Validasi setiap detail
            foreach (var d in details)
            {
                if (string.IsNullOrWhiteSpace(d.NamaPenitip))
                {
                    UXHelper.TampilkanError("Nama penitip wajib diisi.");
                    return -1;
                }
                if (d.JumlahPesanan < 1)
                {
                    UXHelper.TampilkanError("Jumlah pesanan harus ≥ 1.");
                    return -1;
                }
                if (d.IdProduk <= 0)
                {
                    UXHelper.TampilkanError("Produk tidak valid.");
                    return -1;
                }
            }

            Transaction transaksi = new Transaction();
            transaksi.IdKoordinator = idKoordinator;
            transaksi.TotalBayarGrup = totalBayarGrup;
            transaksi.StatusPesanan = "Menunggu";

            int idTransaksi = _transRepo.BuatTransaksi(transaksi, details);
            if (idTransaksi > 0)
                UXHelper.TampilkanSukses("Transaksi berhasil dibuat. Silakan upload bukti bayar.");
            else
                UXHelper.TampilkanError("Gagal membuat transaksi.");
            return idTransaksi;
        }

        // ── 2. Validasi pembayaran (oleh penjual/admin) ──
        public bool ValidasiPembayaran(int idTransaksi, string pathBukti)
        {
            if (string.IsNullOrWhiteSpace(pathBukti))
            {
                UXHelper.TampilkanError("Bukti pembayaran wajib diunggah.");
                return false;
            }

            bool sukses = _transRepo.ValidasiPembayaran(idTransaksi, pathBukti);
            if (sukses)
                UXHelper.TampilkanSukses("Pembayaran berhasil divalidasi.");
            return sukses;
        }

        // ── 3. Ubah status pesanan (oleh penjual) ──
        public bool UbahStatusPesanan(int idTransaksi, string statusBaru)
        {
            // Status yang diperbolehkan: Menunggu, Diproses, Selesai
            if (statusBaru != "Menunggu" && statusBaru != "Diproses" && statusBaru != "Selesai")
            {
                UXHelper.TampilkanError("Status tidak valid.");
                return false;
            }

            bool sukses = _transRepo.UbahStatusPesanan(idTransaksi, statusBaru);
            if (sukses)
                UXHelper.TampilkanSukses($"Status berhasil diubah menjadi {statusBaru}.");
            return sukses;
        }

        // ── 4. Riwayat transaksi koordinator (pembeli) ──
        public List<Transaction> AmbilRiwayatKoordinator(int idKoordinator)
        {
            return _transRepo.AmbilRiwayatKoordinator(idKoordinator);
        }

        // ── 5. Pesanan masuk untuk penjual ──
        public List<Transaction> AmbilPesananMasukPenjual(int idPenjual)
        {
            return _transRepo.AmbilPesananMasukPenjual(idPenjual);
        }

        // ── 6. Detail transaksi ──
        public List<TransactionDetail> AmbilDetailTransaksi(int idTransaksi)
        {
            return _transRepo.AmbilDetailTransaksi(idTransaksi);
        }

        // ── 7. Ambil satu transaksi ──
        public Transaction AmbilTransaksiById(int idTransaksi)
        {
            return _transRepo.AmbilTransaksiById(idTransaksi);
        }
        public int AmbilJumlahTransaksi() => _transRepo.AmbilJumlahTransaksi();
    }
}
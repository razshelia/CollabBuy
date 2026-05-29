using CollabBuy.CollabBuyApp.Models;
using CollabBuy.CollabBuyApp.Repositories;
using System;
using System.Collections.Generic;

namespace CollabBuy.CollabBuyApp.Controllers
{
    /// <summary>
    /// Controller yang bertindak sebagai Mandor alur sesi PreOrder.
    /// Menangani pembukaan/tutup PO, dan update status massal (Stored Procedure).
    /// </summary>
    public class PreOrderController
    {
        // === PRIVATE FIELDS (DEPENDENCIES) ===
        private readonly PreOrderRepository _poRepo;
        private readonly ActivityLogRepository _logRepo;

        // === KONSTRUKTOR ===
        public PreOrderController()
        {
            _poRepo = new PreOrderRepository();
            _logRepo = new ActivityLogRepository();
        }


        // =======================================================
        // FITUR MANAJEMEN SESI PO
        // =======================================================

        /// <summary>
        /// Membuka sesi PreOrder baru.
        /// </summary>
        public (bool sukses, string pesan) BukaSesiPO(int idPenjual, string judul, string jenis, string rekening, DateTime batasWaktu)
        {
            try
            {
                PreOrder po = new PreOrder(idPenjual, judul, jenis, rekening, batasWaktu);
                po.Validate();

                _poRepo.Insert(po);

                ActivityLog log = new ActivityLog(idPenjual, "Membuka sesi PO baru: " + judul);
                _logRepo.Insert(log);

                return (true, "Sesi PreOrder berhasil dibuka!");
            }
            catch (InvalidOrderException ex)
            {
                return (false, ex.GetPesanLengkap());
            }
            catch (Exception ex)
            {
                return (false, "Error sistem: " + ex.Message);
            }
        }

        /// <summary>
        /// Menutup sesi PO secara manual oleh Penjual.
        /// </summary>
        public (bool sukses, string pesan) TutupSesiPO(int idPo, int idPenjual)
        {
            try
            {
                PreOrder po = _poRepo.GetById(idPo);
                if (po == null)
                {
                    return (false, "Sesi PO tidak ditemukan!");
                }

                // Hanya pemilik PO yang boleh menutup
                if (po.GetIdPenjual() != idPenjual)
                {
                    return (false, "Anda bukan pemilik sesi PO ini!");
                }

                // Model yang mengatur state machine via IStatusTrackable
                po.UbahStatus("Tutup");

                _poRepo.Update(po);

                ActivityLog log = new ActivityLog(idPenjual, "Menutup sesi PO: " + po.GetJudulPo());
                _logRepo.Insert(log);

                return (true, "Sesi PO berhasil ditutup.");
            }
            catch (InvalidOrderException ex)
            {
                return (false, "Gagal menutup PO: " + ex.GetPesanLengkap());
            }
        }

        /// <summary>
        /// Mengubah status pesanan secara massal berdasarkan Sesi PO.
        /// Memanggil Stored Procedure sp_update_status_massal_po via Repository.
        /// </summary>
        public (bool sukses, string pesan) UpdateStatusMassal(int idPo, string statusBaru)
        {
            try
            {
                if (string.IsNullOrEmpty(statusBaru))
                {
                    return (false, "Status baru tidak boleh kosong!");
                }

                // Eksekusi Stored Procedure via Repository
                _poRepo.UpdateStatusMassal(idPo, statusBaru);

                return (true, "Semua pesanan dalam PO berhasil diubah ke status '" + statusBaru + "'.");
            }
            catch (Exception ex)
            {
                return (false, "Gagal update massal: " + ex.Message);
            }
        }


        // =======================================================
        // FITUR LIHAT DATA PO
        // =======================================================

        public List<PreOrder> GetAllPreOrder()
        {
            try
            {
                return _poRepo.GetAll();
            }
            catch (Exception)
            {
                return new List<PreOrder>();
            }
        }
    }
}
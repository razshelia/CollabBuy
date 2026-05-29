using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Feedback
{
    public partial class UlasanLapakControl : UserControl
    {
        private readonly User _currentSeller;
        private readonly ReviewController _reviewController;

        public UlasanLapakControl(User seller)
        {
            InitializeComponent();
            _currentSeller = seller;
            _reviewController = new ReviewController();
        }

        private void UlasanLapakControl_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDataUlasan();
        }

        private void SetupDataGridView()
        {
            dgvUlasan.AutoGenerateColumns = false;
            dgvUlasan.Columns.Clear();

            dgvUlasan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Pembeli", HeaderText = "Pembeli", DataPropertyName = "NamaPembeli", Width = 150 });
            dgvUlasan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Produk", HeaderText = "Produk", DataPropertyName = "NamaProduk", Width = 200 });
            dgvUlasan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Rating", HeaderText = "Rating", DataPropertyName = "Rating", Width = 80 });
            dgvUlasan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Komentar", HeaderText = "Komentar", DataPropertyName = "Komentar", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvUlasan.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tanggal", HeaderText = "Tgl Ulas", DataPropertyName = "Tanggal", Width = 100 });
        }

        private void LoadDataUlasan()
        {
            try
            {
                // TODO: Panggil controller untuk mendapatkan ulasan berdasarkan ID penjual
                // var daftarUlasan = _reviewController.GetReviewsBySeller(_currentSeller.IdUser);

                DataTable dtMock = new DataTable();
                dtMock.Columns.Add("NamaPembeli", typeof(string));
                dtMock.Columns.Add("NamaProduk", typeof(string));
                dtMock.Columns.Add("Rating", typeof(string));
                dtMock.Columns.Add("Komentar", typeof(string));
                dtMock.Columns.Add("Tanggal", typeof(string));

                dtMock.Rows.Add("Budi", "Danus Makaroni", "⭐⭐⭐⭐⭐", "Enak banget, pengiriman cepat!", "20 Mei");
                dtMock.Rows.Add("Siti", "PDH Custom", "⭐⭐⭐⭐", "Bagus, tapi agak lama jahitnya.", "22 Mei");

                dgvUlasan.DataSource = dtMock;

                // Update rata-rata rating (Mock)
                lblRatingAvg.Text = "Rating Rata-rata: 4.5 / 5.0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat ulasan: " + ex.Message);
            }
        }
    }
}

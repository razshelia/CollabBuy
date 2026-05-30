using System;
using System.Data;
using System.Windows.Forms;
using CollabBuy.CollabBuyApp.Controllers;
using CollabBuy.CollabBuyApp.Models;

namespace CollabBuy.CollabBuyApp.View.Feedback
{
    public partial class BeriUlasanControl : UserControl
    {
        private readonly User _currentUser;
        private readonly ReviewController _controller;

        public BeriUlasanControl(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _controller = new ReviewController();
        }

        private void BeriUlasanControl_Load(object sender, EventArgs e)
        {
            LoadProduk();
        }

        private void LoadProduk()
        {
            DataTable dt = _controller.GetListProdukBuatDiulas(_currentUser.GetIdUser());
            cbProduk.DataSource = dt;
            cbProduk.DisplayMember = "nama_produk";
            cbProduk.ValueMember = "id_produk";

            if (dt.Rows.Count == 0)
            {
                btnKirim.Enabled = false;
                MessageBox.Show("Belum ada barang beres yang bisa di-review nih. Jajan dulu gih!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnKirim_Click(object sender, EventArgs e)
        {
            if (cbProduk.SelectedValue == null) return;

            int idProduk = Convert.ToInt32(cbProduk.SelectedValue);
            var res = _controller.GasNgasihRating(idProduk, _currentUser.GetIdUser(), (int)numRating.Value, txtKomentar.Text);

            if (res.sukses)
            {
                MessageBox.Show(res.pesan, "Suksessss!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtKomentar.Clear();
                numRating.Value = 5;
            }
            else
            {
                MessageBox.Show(res.pesan, "Waduh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
using System.Drawing;
using System.Windows.Forms;

namespace CollabBuy.CollabBuyApp.Helpers
{
    public static class InputDialog
    {
        /// <summary>
        /// Menampilkan dialog input dengan gaya neo retro.
        /// </summary>
        public static string Show(string prompt, string title, string defaultValue = "")
        {
            Form form = new Form();
            form.Text = title;
            form.Size = new Size(420, 190);
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.BackColor = Color.FromArgb(45, 27, 79);   // #2D1B4F

            Label lbl = new Label();
            lbl.Text = prompt;
            lbl.ForeColor = Color.White;
            lbl.Font = new Font("Segoe UI", 10F);
            lbl.Location = new Point(15, 15);
            lbl.Size = new Size(380, 40);

            TextBox txt = new TextBox();
            txt.Text = defaultValue;
            txt.Font = new Font("Segoe UI", 10F);
            txt.Location = new Point(15, 65);
            txt.Size = new Size(380, 27);

            Button btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(220, 105);
            btnOk.BackColor = Color.FromArgb(167, 139, 250);
            btnOk.ForeColor = Color.White;
            btnOk.FlatStyle = FlatStyle.Flat;

            Button btnCancel = new Button();
            btnCancel.Text = "Batal";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(310, 105);
            btnCancel.BackColor = Color.Gray;
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;

            form.Controls.Add(lbl);
            form.Controls.Add(txt);
            form.Controls.Add(btnOk);
            form.Controls.Add(btnCancel);
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            return form.ShowDialog() == DialogResult.OK ? txt.Text : null;
        }
    }
}
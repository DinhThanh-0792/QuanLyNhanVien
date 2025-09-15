using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanVien
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void PanelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ContextMenuStrip1.Show(btnAdmin, new Point(0, btnAdmin.Height));
        }

        private void LoadControl(UserControl uc)
        {
            PanelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            PanelMain.Controls.Add(uc);
        }
        private void BtnNhanVien_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControlNhanVien());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControlPhongBan());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControlBCTK());
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Xác nhận trước khi đăng xuất
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?",
                                                  "Xác nhận",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Mở lại form login
                FormLogin formLogin = new FormLogin();
                formLogin.Show();

                // Đóng form main hiện tại
                this.Close();
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            LoadControl(new UserControlBangLuong());
        }
    }
}

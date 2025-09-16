using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanVien
{
    public partial class UserControlDMK : UserControl
    {
        public UserControlDMK()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string oldPass = txtMatKhauCu.Text;
            string newPass = txtMatKhauMoi.Text;
            string confirmPass = txtXacNhan.Text;

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }

            // Kiểm tra mật khẩu cũ trong database
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QLNV;Integrated Security=True"))
            {
                conn.Open();
                string sqlCheck = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap=@user AND MatKhau=@oldPass";
                SqlCommand cmd = new SqlCommand(sqlCheck, conn);
                cmd.Parameters.AddWithValue("@user", ClassTenDangNhap.TenDangNhap); // biến lưu user hiện tại
                cmd.Parameters.AddWithValue("@oldPass", oldPass);

                int count = (int)cmd.ExecuteScalar();
                if (count == 0)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!");
                    return;
                }

                // Cập nhật mật khẩu mới
                string sqlUpdate = "UPDATE TaiKhoan SET MatKhau=@newPass WHERE TenDangNhap=@user";
                SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn);
                cmdUpdate.Parameters.AddWithValue("@newPass", newPass);
                cmdUpdate.Parameters.AddWithValue("@user", ClassTenDangNhap.TenDangNhap);
                cmdUpdate.ExecuteNonQuery();

                //MessageBox.Show("Đổi mật khẩu thành công!");
                DialogResult D = MessageBox.Show("Đổi mật khẩu thành công!Bạn có muốn duy trì đăng nhập", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (D == DialogResult.No)
                {
                    Application.OpenForms["FormMain"]?.Close();

                    // Mở lại form đăng nhập
                    FormLogin fLogin = new FormLogin();
                    fLogin.Show();

                    // Đóng form đổi mật khẩu
                    //this.Close();
                }
                else
                {

                }
            }
        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (hienMK.Checked)
            {
                txtMatKhauCu.UseSystemPasswordChar = false;
                txtMatKhauMoi.UseSystemPasswordChar = false;
                txtXacNhan.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhauMoi.UseSystemPasswordChar = true;
                txtMatKhauCu.UseSystemPasswordChar = true;
                txtXacNhan.UseSystemPasswordChar = true;
            }
                
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyNhanVien
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;
                                                 Initial Catalog=QLNV;
                                                 Integrated Security=True");
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
           try
            {
                conn.Open();
                string tk = txt_taikhoan.Text;
                string mk = txt_matkhau.Text;
                string sql = "SELECT TenDangNhap, MatKhau, VaiTro\r\nFROM     TaiKhoan\r\nWHERE  (TenDangNhap = N'"+tk+"') AND (MatKhau = N'"+mk+"')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (txt_matkhau.Text == "" || txt_taikhoan.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin để đăng nhập ","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);   
                }
                else if (dataReader.Read() == true)
                {
                    ClassTenDangNhap.TenDangNhap = txt_taikhoan.Text; // Lưu tên đăng nhập vào biến toàn cục
                    ClassTenDangNhap.VaiTro = dataReader["VaiTro"].ToString(); 
                    this.Hide();
                    FormMain f = new FormMain();
                    f.ShowDialog();
                    f = null;
                    txt_matkhau.Text = "";
                    this.Show();
                    
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng? Vui lòng kiểm tra lại!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                conn.Close();
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Lỗi khi tải dữ liệu:" + ex.Message);
            }
        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked) 
                txt_matkhau.UseSystemPasswordChar = false;
            else
                txt_matkhau.UseSystemPasswordChar = true;
        }
    }
}

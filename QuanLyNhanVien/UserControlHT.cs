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
    public partial class UserControlHT : UserControl
    {
        public UserControlHT()
        {
            InitializeComponent();
        }
        //Kết nối cơ sở dữ liệu
        String Nguon = @"Data Source=.\SQLEXPRESS;
                         Initial Catalog=QLNV;
                         Integrated Security=True";
        String Lenh = @"";
        SqlConnection KetNoi;
        SqlCommand ThucHien;
        SqlDataReader Doc;

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            Lenh = @"INSERT INTO TaiKhoan
                   (TenDangNhap, MatKhau, VaiTro)
                   VALUES (@TenDangNhap, @MatKhau,@VaiTro)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@MatKhau", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@VaiTro", SqlDbType.NVarChar);
            ThucHien.Parameters["@TenDangNhap"].Value = txtTDN.Text;
            ThucHien.Parameters["@MatKhau"].Value = txtMatKhau.Text;
            ThucHien.Parameters["@VaiTro"].Value = txtVaiTro.Text;
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            HienThiTK();
        }
        void HienThiTK()
        {
            DataGridViewTK.Rows.Clear();
            Lenh = @"SELECT ID_TaiKhoan, TenDangNhap, MatKhau, VaiTro
                   FROM     TaiKhoan";
            ThucHien = new SqlCommand(Lenh, KetNoi);

            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                DataGridViewTK.Rows.Add();
                DataGridViewTK.Rows[i].Cells[0].Value = Doc[0];
                DataGridViewTK.Rows[i].Cells[1].Value = Doc[1];
                DataGridViewTK.Rows[i].Cells[2].Value = Doc[2];
                DataGridViewTK.Rows[i].Cells[3].Value = Doc[3];
                i++;
            }

            KetNoi.Close();
        }

        private void UserControlHT_Load(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            HienThiTK();
        }

        private void DataGridViewTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTDN.Text = DataGridViewTK.CurrentRow.Cells[1].Value.ToString();
            txtMatKhau.Text = DataGridViewTK.CurrentRow.Cells[2].Value.ToString();
            txtVaiTro.Text = DataGridViewTK.CurrentRow.Cells[3].Value.ToString();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Lenh = @"UPDATE TaiKhoan
                   SET          TenDangNhap = @TenDangNhap, MatKhau=@MatKhau, VaiTro = @VaiTro
                   WHERE  (ID_TaiKhoan = @Original_ID_TaiKhoan)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@MatKhau", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@VaiTro", SqlDbType.NVarChar);
            ThucHien.Parameters["@TenDangNhap"].Value = txtTDN.Text;
            ThucHien.Parameters["@MatKhau"].Value = txtMatKhau.Text;
            ThucHien.Parameters["@VaiTro"].Value = txtVaiTro.Text;
            ThucHien.Parameters.Add("@Original_ID_TaiKhoan", SqlDbType.Int);
            ThucHien.Parameters["@Original_ID_TaiKhoan"].Value = DataGridViewTK.CurrentRow.Cells[0].Value;
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            HienThiTK();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Ban co muon xoa " + txtTDN.Text, "Chu y", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (D == DialogResult.Yes)
            {
                Lenh = @"DELETE FROM TaiKhoan
                   WHERE  (ID_TaiKhoan = @Original_ID_TaiKhoan)";
                ThucHien = new SqlCommand(Lenh, KetNoi);

                ThucHien.Parameters.Add("@Original_ID_TaiKhoan", SqlDbType.Int);
                ThucHien.Parameters["@Original_ID_TaiKhoan"].Value = DataGridViewTK.CurrentRow.Cells[0].Value;
                KetNoi.Open();
                ThucHien.ExecuteNonQuery();
                KetNoi.Close();
                HienThiTK();
            }
            else
            {

            }
        }
    }
}

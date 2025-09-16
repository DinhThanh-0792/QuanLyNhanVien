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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyNhanVien
{
    public partial class UserControlTCTT : UserControl
    {
        public UserControlTCTT()
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

        private void btnTraCuu_Click(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            DataGridViewTC.Rows.Clear();
            Lenh = @"SELECT NhanVien.ID_NhanVien, PhongBan.TenPhongBan, NhanVien.HoTen, NhanVien.NgaySinh, NhanVien.GioiTinh, NhanVien.DiaChi, NhanVien.SoDienThoai, NhanVien.Email, ChucVu.TenChucVu, 
                    Luong.LuongCoBan, Luong.PhuCap, Luong.NgayCong, Luong.TongLuong
                    FROM     NhanVien INNER JOIN
                             ChucVu ON NhanVien.ID_ChucVu = ChucVu.ID_ChucVu INNER JOIN
                             PhongBan ON NhanVien.ID_PhongBan = PhongBan.ID_PhongBan INNER JOIN
                                Luong ON NhanVien.ID_NhanVien = Luong.ID_NhanVien   
                    WHERE (NhanVien.HoTen=@HoTen) ";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@HoTen", SqlDbType.NVarChar);
            ThucHien.Parameters["@HoTen"].Value = txtTenNV.Text;
            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                DataGridViewTC.Rows.Add();
                DataGridViewTC.Rows[i].Cells[0].Value = Doc[0];
                DataGridViewTC.Rows[i].Cells[1].Value = Doc[1];
                DataGridViewTC.Rows[i].Cells[2].Value = Doc[2];
                DataGridViewTC.Rows[i].Cells[3].Value = Doc[3];
                DataGridViewTC.Rows[i].Cells[4].Value = Doc[4];
                DataGridViewTC.Rows[i].Cells[5].Value = Doc[5];
                DataGridViewTC.Rows[i].Cells[6].Value = Doc[6];
                DataGridViewTC.Rows[i].Cells[7].Value = Doc[7];
                DataGridViewTC.Rows[i].Cells[8].Value = Doc[8];
                DataGridViewTC.Rows[i].Cells[9].Value = Doc[9];
                DataGridViewTC.Rows[i].Cells[10].Value = Doc[10];
                DataGridViewTC.Rows[i].Cells[11].Value = Doc[11];
                DataGridViewTC.Rows[i].Cells[12].Value = Doc[12];
                i++;
            }

            KetNoi.Close();
        }
    }
}

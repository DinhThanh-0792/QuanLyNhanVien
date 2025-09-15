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
    public partial class UserControlBangLuong : UserControl
    {
        public UserControlBangLuong()
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void themLuong_Click(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            Lenh = @"INSERT INTO Luong 
                (ID_Luong, ID_NhanVien, LuongCoBan, PhuCap, GhiChu)"
                + "VALUES (@ID_Luong, @ID_NhanVien, @LuongCoBan, @PhuCap, @GhiChu)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@LuongCoBan", SqlDbType.Float);
            ThucHien.Parameters.Add("@PhuCap", SqlDbType.Float);
            ThucHien.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            ThucHien.Parameters["@LuongCoBan"].Value = txtLuongCoBan.Text;
            ThucHien.Parameters["@PhuCap"].Value = txtPhuCap.Text;
            ThucHien.Parameters["@GhiChu"].Value = txtGhiChu.Text;
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            HienThi();
        }
        void HienThi()
        {
            DataGridViewLuong.Rows.Clear();
            Lenh = @"SELECT ID_Luong, ID_NhanVien, LuongCoBan, PhuCap, GhiChu
                   FROM     Luong";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                DataGridViewLuong.Rows.Add();
                DataGridViewLuong.Rows[i].Cells[0].Value = Doc[0];
                DataGridViewLuong.Rows[i].Cells[1].Value = Doc[1];
                DataGridViewLuong.Rows[i].Cells[2].Value = Doc[2];
                DataGridViewLuong.Rows[i].Cells[3].Value = Doc[3];
                DataGridViewLuong.Rows[i].Cells[4].Value = Doc[4];
                i++;
            }
            Doc.Close();
            KetNoi.Close();
        }

        private void DataGridViewLuong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLuong.Text = DataGridViewLuong.CurrentRow.Cells[0].Value.ToString();
            IDNV.Text = DataGridViewLuong.CurrentRow.Cells[1].Value.ToString();
            txtLuongCoBan.Text = DataGridViewLuong.CurrentRow.Cells[2].Value.ToString();
            txtPhuCap.Text = DataGridViewLuong.CurrentRow.Cells[3].Value.ToString();
            txtGhiChu.Text = DataGridViewLuong.CurrentRow.Cells[4].Value.ToString();
        }
        private void UserControlBangLuong_Load(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            HienThi();
        }

        private void suaLuong_Click(object sender, EventArgs e)
        {
            Lenh = @"UPDATE Luong 
            SET ID_Luong = LuongCoBan = @LuongCoBan, PhuCap = @PhuCap, GhiChu = @GhiChu
            WHERE  (ID_Luong = @Original_ID_Luong)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@LuongCoBan", SqlDbType.Float);
            ThucHien.Parameters.Add("@PhuCap", SqlDbType.Float);
            ThucHien.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            ThucHien.Parameters["@LuongCoBan"].Value = txtLuongCoBan.Text;
            ThucHien.Parameters["@PhuCap"].Value = txtPhuCap.Text;
            ThucHien.Parameters["@GhiChu"].Value = txtGhiChu.Text;
            ThucHien.Parameters.Add("@Original_ID_Luong", SqlDbType.Int);
            ThucHien.Parameters["@Original_ID_Luong"].Value = Convert.ToInt32(txtMaLuong.Text);
            KetNoi.Open();
            KetNoi.Close();
            HienThi();

        }

        private void xoaLuong_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Ban co muon xoa " + IDNV.Text, "Chu y", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (D == DialogResult.Yes)
            {
                Lenh = @"DELETE FROM Luong 
                WHERE  (ID_Luong = @Original_ID_Luong)";
                ThucHien = new SqlCommand(Lenh, KetNoi);
                ThucHien.Parameters.Add("@Original_ID_Luong", SqlDbType.Int);
                ThucHien.Parameters["@Original_ID_Luong"].Value = Convert.ToInt32(txtMaLuong.Text);
                KetNoi.Open();
                ThucHien.ExecuteNonQuery();
                KetNoi.Close();
                HienThi();
            }
        }
    }
}

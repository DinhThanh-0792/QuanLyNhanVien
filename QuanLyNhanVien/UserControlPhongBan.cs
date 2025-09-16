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
    public partial class UserControlPhongBan : UserControl
    {
        public UserControlPhongBan()
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
            Lenh = @"INSERT INTO PhongBan
                   (TenPhongBan, MoTa, GhiChu)
                   VALUES (@TenPhongBan, @MoTa,@GhiChu)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@TenPhongBan", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@MoTa", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            ThucHien.Parameters["@TenPhongBan"].Value = txtPB.Text;
            ThucHien.Parameters["@MoTa"].Value = txtMoTa.Text;
            ThucHien.Parameters["@GhiChu"].Value = txtGhiChu.Text;
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            HienThi();
        }
        void HienThi()
        {
            DataGridViewPB.Rows.Clear();
            Lenh = @"SELECT ID_PhongBan, TenPhongBan, MoTa, GhiChu
                   FROM     PhongBan";
            ThucHien = new SqlCommand(Lenh, KetNoi);

            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                DataGridViewPB.Rows.Add();
                DataGridViewPB.Rows[i].Cells[0].Value = Doc[0];
                DataGridViewPB.Rows[i].Cells[1].Value = Doc[1];
                DataGridViewPB.Rows[i].Cells[2].Value = Doc[2];
                DataGridViewPB.Rows[i].Cells[3].Value = Doc[3];
                i++;
            }

            KetNoi.Close();
        }
        //private void UserControlPhongBan_Load(object sender, EventArgs e)
        //{
        //    KetNoi = new SqlConnection(Nguon);
        //    HienThi();
        //}

        private void DataGridViewPB_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPB.Text = DataGridViewPB.CurrentRow.Cells[1].Value.ToString();
            txtMoTa.Text = DataGridViewPB.CurrentRow.Cells[2].Value.ToString();
            txtGhiChu.Text = DataGridViewPB.CurrentRow.Cells[3].Value.ToString();
        }

        private void UserControlPhongBan_Load_1(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            HienThi();
            Hien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Lenh = @"UPDATE PhongBan
                   SET          TenPhongBan = @TenPhongBan, MoTa=@MoTa, GhiChu = @GhiChu
                   WHERE  (ID_PhongBan = @Original_ID_PhongBan)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@TenPhongBan", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@MoTa", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            ThucHien.Parameters["@TenPhongBan"].Value = txtPB.Text;
            ThucHien.Parameters["@MoTa"].Value = txtMoTa.Text;
            ThucHien.Parameters["@GhiChu"].Value = txtGhiChu.Text;
            ThucHien.Parameters.Add("@Original_ID_PhongBan", SqlDbType.Int);
            ThucHien.Parameters["@Original_ID_PhongBan"].Value = DataGridViewPB.CurrentRow.Cells[0].Value;
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            HienThi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Ban co muon xoa " + txtPB.Text, "Chu y", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (D == DialogResult.Yes)
            {
                Lenh = @"DELETE FROM PhongBan
                   WHERE  (ID_PhongBan = @Original_ID_PhongBan)";
                ThucHien = new SqlCommand(Lenh, KetNoi);

                ThucHien.Parameters.Add("@Original_ID_PhongBan", SqlDbType.Int);
                ThucHien.Parameters["@Original_ID_PhongBan"].Value = DataGridViewPB.CurrentRow.Cells[0].Value;
                KetNoi.Open();
                ThucHien.ExecuteNonQuery();
                KetNoi.Close();
                HienThi();
            }
            else
            {

            }
        }

       

        private void btnThemCV_Click(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            Lenh = @"INSERT INTO ChucVu
                   (TenChucVu, MoTa, GhiChu)
                   VALUES (@TenChucVu, @MoTa,@GhiChu)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@TenChucVu", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@MoTa", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            ThucHien.Parameters["@TenChucVu"].Value = txtTenChucVu.Text;
            ThucHien.Parameters["@MoTa"].Value = txtMoTaChucVu.Text;
            ThucHien.Parameters["@GhiChu"].Value = txtGhiChuChucVu.Text;
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            Hien();
        }
        void Hien()
        {
            DataGridViewCV.Rows.Clear();
            Lenh = @"SELECT ID_ChucVu, TenChucVu, MoTa, GhiChu
                   FROM     ChucVu";
            ThucHien = new SqlCommand(Lenh, KetNoi);

            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                DataGridViewCV.Rows.Add();
                DataGridViewCV.Rows[i].Cells[0].Value = Doc[0];
                DataGridViewCV.Rows[i].Cells[1].Value = Doc[1];
                DataGridViewCV.Rows[i].Cells[2].Value = Doc[2];
                DataGridViewCV.Rows[i].Cells[3].Value = Doc[3];
                i++;
            }

            KetNoi.Close();
        }

        private void DataGridViewCV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTenChucVu.Text = DataGridViewCV.CurrentRow.Cells[1].Value.ToString();
            txtMoTaChucVu.Text = DataGridViewCV.CurrentRow.Cells[2].Value.ToString();
            txtGhiChuChucVu.Text = DataGridViewCV.CurrentRow.Cells[3].Value.ToString();
        }
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            Lenh = @"UPDATE ChucVu
                   SET          TenChucVu = @TenChucVu, MoTa=@MoTa, GhiChu = @GhiChu
                   WHERE  (ID_ChucVu = @Original_ID_ChucVu)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@TenChucVu", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@MoTa", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            ThucHien.Parameters["@TenChucVu"].Value = txtTenChucVu.Text;
            ThucHien.Parameters["@MoTa"].Value = txtMoTaChucVu.Text;
            ThucHien.Parameters["@GhiChu"].Value = txtGhiChuChucVu.Text;
            ThucHien.Parameters.Add("@Original_ID_ChucVu", SqlDbType.Int);
            ThucHien.Parameters["@Original_ID_ChucVu"].Value = DataGridViewCV.CurrentRow.Cells[0].Value;
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            Hien();
        }

        private void btnXoaCV_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Ban co muon xoa " + txtTenChucVu.Text, "Chu y", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (D == DialogResult.Yes)
            {
                Lenh = @"DELETE FROM ChucVu
                   WHERE  (ID_ChucVu = @Original_ID_ChucVu)";
                ThucHien = new SqlCommand(Lenh, KetNoi);

                ThucHien.Parameters.Add("@Original_ID_ChucVu", SqlDbType.Int);
                ThucHien.Parameters["@Original_ID_ChucVu"].Value = DataGridViewCV.CurrentRow.Cells[0].Value;
                KetNoi.Open();
                ThucHien.ExecuteNonQuery();
                KetNoi.Close();
                Hien();
            }
            else
            {

            }
        }
    }
}

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
    public partial class FormThem : Form
    {
        public FormThem()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Kết nối cơ sở dữ liệu
        String Nguon = @"Data Source=.\SQLEXPRESS;
                         Initial Catalog=QLNV;
                         Integrated Security=True";
        String Lenh = @"";
        SqlConnection KetNoi;
        SqlCommand ThucHien;
        SqlDataReader Doc;

        private void FormThem_Load(object sender, EventArgs e)
        {
            LoadPhongBan();
            LoadChucVu();
        }
        private void LoadPhongBan()
        {
            KetNoi = new SqlConnection(Nguon);
            ComboBoxPB.Items.Clear();
            Lenh = @"SELECT TenPhongBan
                   FROM     PhongBan";
            ThucHien = new SqlCommand(Lenh, KetNoi);

            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                ComboBoxPB.Items.Add(Doc[0]);
                i++;
            }

            KetNoi.Close();
        }
        private void LoadChucVu()
        {
            KetNoi = new SqlConnection(Nguon);
            ComboBoxCV.Items.Clear();
            Lenh = @"SELECT TenChucVu
                   FROM     ChucVu";
            ThucHien = new SqlCommand(Lenh, KetNoi);

            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                ComboBoxCV.Items.Add(Doc[0]);
                i++;
            }

            KetNoi.Close();
        }
        int ID_PhongBan;
        private void ComboBoxPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lenh = @"SELECT ID_PhongBan
                   FROM     PhongBan
                   WHERE  (TenPhongBan = @TenPhongBan)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@TenPhongBan", SqlDbType.NVarChar);
            ThucHien.Parameters["@TenPhongBan"].Value = ComboBoxPB.Text;
            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                ID_PhongBan = (int)Doc[0];
                i++;
            }

            KetNoi.Close();
        }
        int ID_ChucVu;
        private void ComboBoxCV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lenh = @"SELECT ID_ChucVu
                   FROM     ChucVu
                   WHERE  (TenChucVu = @TenChucVu)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@TenChucVu", SqlDbType.NVarChar);
            ThucHien.Parameters["@TenChucVu"].Value = ComboBoxCV.Text;
            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                ID_ChucVu = (int)Doc[0];
                i++;
            }

            KetNoi.Close();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Lenh = @"INSERT INTO NhanVien
                    (HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, NgayVaoLam, NgayNghiViec, ID_PhongBan, ID_ChucVu, TrangThai, GhiChu)
                    VALUES (@HoTen,@NgaySinh,@GioiTinh,@DiaChi,@SoDienThoai,@Email,@NgayVaoLam,@NgayNghiViec,@ID_PhongBan,@ID_ChucVu,@TrangThai,@GhiChu)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@HoTen", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@NgaySinh", SqlDbType.Date);
            ThucHien.Parameters.Add("@GioiTinh", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@DiaChi", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@Email", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@NgayVaoLam", SqlDbType.Date);
            ThucHien.Parameters.Add("@NgayNghiViec", SqlDbType.Date);
            ThucHien.Parameters.Add("@ID_PhongBan", SqlDbType.Int);
            ThucHien.Parameters.Add("@ID_ChucVu", SqlDbType.Int);
            ThucHien.Parameters.Add("@TrangThai", SqlDbType.NVarChar);
            ThucHien.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            ThucHien.Parameters["@HoTen"].Value = txtTen.Text;
            ThucHien.Parameters["@NgaySinh"].Value = DateTimeNgaySinh.Text;
            ThucHien.Parameters["@GioiTinh"].Value = txtGioiTinh.Text;
            ThucHien.Parameters["@DiaChi"].Value = txtDiaChi.Text;
            ThucHien.Parameters["@SoDienThoai"].Value = txtSDT.Text;
            ThucHien.Parameters["@Email"].Value = txtEmail.Text;
            ThucHien.Parameters["@NgayVaoLam"].Value = DateTimeNgayVaoLam.Text;
            ThucHien.Parameters["@NgayNghiViec"].Value = DateTimeNNV.Text;
            ThucHien.Parameters["@ID_PhongBan"].Value = ID_PhongBan;
            ThucHien.Parameters["@ID_ChucVu"].Value = ID_ChucVu;
            ThucHien.Parameters["@TrangThai"].Value = txtTrangThai.Text;
            ThucHien.Parameters["@GhiChu"].Value = txtGhiChu.Text;
            
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            //Hien();
            MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK; // báo về Form chính
            this.Close();
        }
        
    }
}

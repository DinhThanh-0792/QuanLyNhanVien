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
    public partial class FormSua : Form
    {
        public FormSua()
        {
            InitializeComponent();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int _idNhanVien; // giữ ID để update

        // Kết nối CSDL
        String Nguon = @"Data Source=.\SQLEXPRESS;
                         Initial Catalog=QLNV;
                         Integrated Security=True";
        String Lenh = @"";
        SqlConnection KetNoi;
        SqlCommand ThucHien;
        SqlDataReader Doc;
        public FormSua(int id, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi,
                        string soDienThoai, string email, DateTime ngayVaoLam, DateTime? ngayNghiViec,string trangThai,
                        string ghiChu, string chucVu, string phongBan)
        {
            InitializeComponent();

            _idNhanVien = id;

            // Gán dữ liệu vào control
            txtTen.Text = hoTen;
            DateTimeNgaySinh.Value = ngaySinh;
            txtGioiTinh.Text = gioiTinh;
            txtDiaChi.Text = diaChi;
            txtSDT.Text = soDienThoai;
            txtEmail.Text = email;
            DateTimeNgayVaoLam.Value = ngayVaoLam;
            if (ngayNghiViec.HasValue)
            {
                DateTimeNNV.Value = ngayNghiViec.Value;
            }
            else
            {
                DateTimeNNV.Checked = false; // hoặc ẩn đi
            }
            txtTrangThai.Text = trangThai;
            txtGhiChu.Text = ghiChu;
            //ComboBoxCV.Text = chucVu;
            //ComboBoxPB.Text = phongBan;
            // Load danh sách phòng ban và chức vụ trước khi gán
            LoadPhongBan();
            LoadChucVu();

            // Gán giá trị đã có
            ComboBoxPB.SelectedItem = phongBan;
            ComboBoxCV.SelectedItem = chucVu;

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
           
        }
        int ID_ChucVu;
        private void ComboBoxCV_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void FormSua_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLNVDataSet1.ChucVu' table. You can move, or remove it, as needed.
            this.chucVuTableAdapter.Fill(this.qLNVDataSet1.ChucVu);
            //LoadPhongBan();
            //LoadChucVu();
            //ComboBoxPB.SelectedValue = ID_PhongBan;
            //ComboBoxCV.SelectedValue = ID_ChucVu;
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Lenh = @"UPDATE NhanVien
                    SET  HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email, NgayVaoLam = @NgayVaoLam, NgayNghiViec = @NgayNghiViec,ID_PhongBan=@ID_PhongBan, ID_ChucVu = @ID_ChucVu, 
                    TrangThai = @TrangThai, GhiChu = @GhiChu
                    WHERE  (ID_NhanVien = @Original_ID_NhanVien)";
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
            ThucHien.Parameters.Add("@Original_ID_NhanVien", SqlDbType.Int);
            ThucHien.Parameters["@Original_ID_NhanVien"].Value = _idNhanVien;
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            //HienThi();
            MessageBox.Show("Cập nhật thành công!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ComboBoxPB_SelectedIndexChanged_1(object sender, EventArgs e)
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
                label15.Text = "ID_PhongBan" + Doc[0];
                i++;
            }

            KetNoi.Close();
        }

        private void ComboBoxCV_SelectedIndexChanged_1(object sender, EventArgs e)
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

    }



}

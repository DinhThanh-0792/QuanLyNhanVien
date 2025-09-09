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
    public partial class UserControlNhanVien : UserControl
    {
        public UserControlNhanVien()
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
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FormThem f = new FormThem();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Hien(); // gọi lại để load dữ liệu mới thêm
            }
        }
        void Hien()
        {
            DataGridViewNV.Rows.Clear();
            Lenh = @"SELECT NhanVien.ID_NhanVien, PhongBan.TenPhongBan, NhanVien.HoTen, NhanVien.NgaySinh, NhanVien.GioiTinh, NhanVien.DiaChi, NhanVien.SoDienThoai, NhanVien.Email, NhanVien.NgayVaoLam, ChucVu.TenChucVu, NhanVien.TrangThai, 
                    NhanVien.GhiChu
                    FROM     NhanVien INNER JOIN
                    ChucVu ON NhanVien.ID_ChucVu = ChucVu.ID_ChucVu INNER JOIN
                    PhongBan ON NhanVien.ID_PhongBan = PhongBan.ID_PhongBan";
            KetNoi = new SqlConnection(Nguon);
            KetNoi.Open();
            ThucHien = new SqlCommand(Lenh, KetNoi);
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                DataGridViewNV.Rows.Add();
                DataGridViewNV.Rows[i].Cells[0].Value = Doc[0];
                DataGridViewNV.Rows[i].Cells[4].Value = Doc[4];
                DataGridViewNV.Rows[i].Cells[5].Value = Doc[5];
                DataGridViewNV.Rows[i].Cells[6].Value = Doc[6];
                DataGridViewNV.Rows[i].Cells[7].Value = Doc[7];
                DataGridViewNV.Rows[i].Cells[8].Value = Doc[8];
                DataGridViewNV.Rows[i].Cells[9].Value = Doc[9];
                DataGridViewNV.Rows[i].Cells[3].Value = Doc[3];
                DataGridViewNV.Rows[i].Cells[11].Value = Doc[11];
                DataGridViewNV.Rows[i].Cells[2].Value = Doc[2];
                DataGridViewNV.Rows[i].Cells[10].Value = Doc[10];
                DataGridViewNV.Rows[i].Cells[1].Value = Doc[1];
                i++;
            }

            KetNoi.Close();
        }
        private void UserControlNhanVien_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Load"); // test
            KetNoi = new SqlConnection(Nguon);
            Hien();
        }

        private void UserControlNhanVien_Load_1(object sender, EventArgs e)
        {
            KetNoi = new SqlConnection(Nguon);
            Hien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (DataGridViewNV.SelectedRows.Count > 0)
            {
                DataGridViewRow row = DataGridViewNV.SelectedRows[0];

                // Lấy dữ liệu từ DataGridView
                int id = Convert.ToInt32(row.Cells[0].Value);
                string hoTen = row.Cells[2].Value.ToString();
                DateTime ngaySinh = Convert.ToDateTime(row.Cells[3].Value);
                string gioiTinh = row.Cells[4].Value.ToString();
                string diaChi = row.Cells[5].Value.ToString();
                string soDienThoai = row.Cells[6].Value.ToString();
                string email = row.Cells[7].Value.ToString();
                DateTime ngayVaoLam = Convert.ToDateTime(row.Cells[8].Value);
                string trangThai = row.Cells[10].Value.ToString();
                string ghiChu = row.Cells[11].Value.ToString();
                string chucVu = row.Cells[9].Value.ToString();
                string phongBan = row.Cells[1].Value.ToString();

                // Gọi FormSua và truyền dữ liệu sang
                FormSua f = new FormSua(id, hoTen, ngaySinh, gioiTinh, diaChi,
                                        soDienThoai, email, ngayVaoLam, trangThai,
                                        ghiChu, chucVu, phongBan);

                if (f.ShowDialog() == DialogResult.OK)
                {
                    Hien(); // load lại danh sách sau khi sửa
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên để sửa!");
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Ban co muon xoa " , "Chu y", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (D == DialogResult.Yes)
            {
                Lenh = @"DELETE FROM NhanVien
                   WHERE  (ID_NhanVien = @Original_ID_NhanVien)";
                ThucHien = new SqlCommand(Lenh, KetNoi);

                ThucHien.Parameters.Add("@Original_ID_NhanVien", SqlDbType.Int);
                ThucHien.Parameters["@Original_ID_NhanVien"].Value = DataGridViewNV.CurrentRow.Cells[0].Value;
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

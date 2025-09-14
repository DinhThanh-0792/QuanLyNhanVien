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
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyNhanVien
{
    public partial class UserControlBCTK : UserControl
    {
        public UserControlBCTK()
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

        private void UserControlBCTK_Load(object sender, EventArgs e)
        {
            // Load combobox tháng, năm
            for (int i = 1; i <= 12; i++)
            {
                comboBoxThang.Items.Add(i);
            }
            comboBoxThang.SelectedIndex = DateTime.Now.Month -1;
            for (int y = 2020; y <= DateTime.Now.Year; y++)
            {
                comboBoxNam.Items.Add(y);
            }
            comboBoxNam.SelectedItem = DateTime.Now.Year;
            //Cau hinh chart
            chartBaoCao.Series.Clear();

            chartBaoCao.Series.Add("Nhan vien vao lam");
            chartBaoCao.Series["Nhan vien vao lam"].ChartType = SeriesChartType.Column;
            chartBaoCao.Series["Nhan vien vao lam"].Color = Color.Blue;

            chartBaoCao.Series.Add("Nhan vien nghi viec");
            chartBaoCao.Series["Nhan vien nghi viec"].ChartType = SeriesChartType.Column;
            chartBaoCao.Series["Nhan vien nghi viec"].Color = Color.Red;

            chartBaoCao.Series.Add("Nhan vien thoi viec");
            chartBaoCao.Series["Nhan vien thoi viec"].ChartType = SeriesChartType.Column;
            chartBaoCao.Series["Nhan vien thoi viec"].Color = Color.Orange;
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (comboBoxThang.SelectedItem == null || comboBoxNam.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tháng và năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int thang = (int)comboBoxThang.SelectedItem;
            int nam = (int)comboBoxNam.SelectedItem;
            int VaoLam = 0, NghiViec = 0, ThoiViec = 0;

            KetNoi = new SqlConnection(Nguon);
            KetNoi.Open();
            // Nhân viên vào làm
            Lenh = @"SELECT COUNT(*) FROM NhanVien WHERE MONTH(NgayVaoLam) = @Thang AND YEAR(NgayVaoLam) = @Nam";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.AddWithValue("@Thang", thang);
            ThucHien.Parameters.AddWithValue("@Nam", nam);
            VaoLam = (int)ThucHien.ExecuteScalar();
            // Nhân viên nghỉ việc
            Lenh = @"SELECT COUNT(*) FROM NhanVien WHERE MONTH(NgayNghiViec) = @Thang AND YEAR(NgayNghiViec) = @Nam";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.AddWithValue("@Thang", thang);
            ThucHien.Parameters.AddWithValue("@Nam", nam);
            NghiViec = (int)ThucHien.ExecuteScalar();
            // Nhân viên thôi việc
            Lenh = @"SELECT COUNT(*) FROM NhanVien WHERE TrangThai = N'Thôi việc' AND MONTH(NgayNghiViec) = @Thang AND YEAR(NgayNghiViec) = @Nam";   
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.AddWithValue("@Thang", thang);
            ThucHien.Parameters.AddWithValue("@Nam", nam);
            ThoiViec = (int)ThucHien.ExecuteScalar();
            // Cập nhật biểu đồ
            foreach (var series in chartBaoCao.Series)
            {
                series.Points.Clear();
            }
            chartBaoCao.Series["Nhan vien vao lam"].Points.AddXY("Loai nhan vien ", VaoLam);
            chartBaoCao.Series["Nhan vien nghi viec"].Points.AddXY("Loai nhan vien ", NghiViec);
            chartBaoCao.Series["Nhan vien thoi viec"].Points.AddXY("Loai nhan vien ", ThoiViec);
        }
       
}
}

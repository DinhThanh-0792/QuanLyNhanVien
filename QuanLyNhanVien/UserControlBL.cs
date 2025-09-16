using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyNhanVien
{
    public partial class UserControlBL : UserControl
    {
        public UserControlBL()
        {
            InitializeComponent();
        }
        private void TinhLuong()
        {
            if (decimal.TryParse(txtLCB.Text, out decimal luongCoBan) &&
                decimal.TryParse(txtHSL.Text, out decimal heSoLuong) &&
                decimal.TryParse(txtPC.Text, out decimal phuCap) &&
                int.TryParse(txtNgayCong.Text, out int ngayCong))
            {
                // Giả sử 26 ngày công chuẩn
                decimal thucLinh = (luongCoBan * heSoLuong / 26 * ngayCong) + phuCap;
                txtThucLinh.Text = thucLinh.ToString("N0"); // format có dấu phân cách
            }
            else
            {
                txtThucLinh.Text = "";
            }
        }

        private void txtLCB_TextChanged(object sender, EventArgs e)
        {
            TinhLuong();
        }

        private void txtHSL_TextChanged(object sender, EventArgs e)
        {
            TinhLuong();
        }

        private void txtPC_TextChanged(object sender, EventArgs e)
        {
            TinhLuong();
        }

        private void txtNgayCong_TextChanged(object sender, EventArgs e)
        {
            TinhLuong();
        }
        String Nguon = @"Data Source=.\SQLEXPRESS;
                         Initial Catalog=QLNV;
                         Integrated Security=True";
        String Lenh = @"";
        SqlConnection KetNoi;
        SqlCommand ThucHien;
        SqlDataReader Doc;
        private void LoadHoTen()
        {
            KetNoi = new SqlConnection(Nguon);
            ComboBoxHoTen.Items.Clear();
            Lenh = @"SELECT HoTen
                   FROM     NhanVien";
            ThucHien = new SqlCommand(Lenh, KetNoi);

            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                ComboBoxHoTen.Items.Add(Doc[0]);
                i++;
            }

            KetNoi.Close();
        }

        private void UserControlBL_Load(object sender, EventArgs e)
        {
            LoadHoTen();
            HienLuong();
        }
        int ID_NhanVien;

        private void ComboBoxHoTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lenh = @"SELECT ID_NhanVien
                   FROM     NhanVien
                   WHERE  (HoTen = @HoTen)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.Add("@HoTen", SqlDbType.NVarChar);
            ThucHien.Parameters["@HoTen"].Value = ComboBoxHoTen.Text;
            KetNoi.Open();
            Doc = ThucHien.ExecuteReader();
            int i = 0;
            while (Doc.Read())
            {
                ID_NhanVien = (int)Doc[0];
                i++;
            }

            KetNoi.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QLNV;Integrated Security=True"))
                {
                    conn.Open();
                    string sql = @"INSERT INTO Luong (ID_NhanVien, LuongCoBan, HeSoLuong, PhuCap, NgayCong, TongLuong, GhiChu)
                           VALUES (@ID_NhanVien, @LuongCoBan, @HeSoLuong, @PhuCap, @NgayCong, @TongLuong, @GhiChu)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_NhanVien", ID_NhanVien);
                        cmd.Parameters.AddWithValue("@LuongCoBan", decimal.Parse(txtLCB.Text));
                        cmd.Parameters.AddWithValue("@HeSoLuong", decimal.Parse(txtHSL.Text));
                        cmd.Parameters.AddWithValue("@PhuCap", decimal.Parse(txtPC.Text));
                        cmd.Parameters.AddWithValue("@NgayCong", int.Parse(txtNgayCong.Text));
                        cmd.Parameters.AddWithValue("@TongLuong", decimal.Parse(txtThucLinh.Text));
                        cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm bảng lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienLuong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        void HienLuong()
        {
            DataGridViewLuong.Rows.Clear();
            Lenh = @"SELECT Luong.ID_Luong, NhanVien.HoTen, Luong.LuongCoBan, Luong.HeSoLuong, 
                     Luong.PhuCap, Luong.NgayCong, Luong.TongLuong, Luong.GhiChu
                    FROM     Luong INNER JOIN
                    NhanVien ON NhanVien.ID_NhanVien = Luong.ID_NhanVien ";
            KetNoi = new SqlConnection(Nguon);
            KetNoi.Open();
            ThucHien = new SqlCommand(Lenh, KetNoi);
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
                DataGridViewLuong.Rows[i].Cells[5].Value = Doc[5];
                DataGridViewLuong.Rows[i].Cells[6].Value = Doc[6];
                DataGridViewLuong.Rows[i].Cells[7].Value = Doc[7];
                i++;
            }

            KetNoi.Close();
        }

        private void DataGridViewLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtLCB.Text = DataGridViewLuong.CurrentRow.Cells[2].Value.ToString();
            txtHSL.Text = DataGridViewLuong.CurrentRow.Cells[3].Value.ToString();
            txtPC.Text = DataGridViewLuong.CurrentRow.Cells[4].Value.ToString();
            txtNgayCong.Text = DataGridViewLuong.CurrentRow.Cells[5].Value.ToString();
            txtThucLinh.Text = DataGridViewLuong.CurrentRow.Cells[6].Value.ToString();
            txtGhiChu.Text = DataGridViewLuong.CurrentRow.Cells[7].Value.ToString();
            ComboBoxHoTen.Text = DataGridViewLuong.CurrentRow.Cells[1].Value.ToString();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Lenh = @"UPDATE Luong
                   SET          ID_NhanVien=@ID_NhanVien, LuongCoBan=@LuongCoBan, HeSoLuong=@HeSoLuong, PhuCap=@PhuCap, NgayCong=@NgayCong, TongLuong=@TongLuong, GhiChu = @GhiChu
                   WHERE  (ID_Luong = @Original_ID_Luong)";
            ThucHien = new SqlCommand(Lenh, KetNoi);
            ThucHien.Parameters.AddWithValue("@ID_NhanVien", ID_NhanVien);
            ThucHien.Parameters.AddWithValue("@LuongCoBan", decimal.Parse(txtLCB.Text));
            ThucHien.Parameters.AddWithValue("@HeSoLuong", decimal.Parse(txtHSL.Text));
            ThucHien.Parameters.AddWithValue("@PhuCap", decimal.Parse(txtPC.Text));
            ThucHien.Parameters.AddWithValue("@NgayCong", int.Parse(txtNgayCong.Text));
            ThucHien.Parameters.AddWithValue("@TongLuong", decimal.Parse(txtThucLinh.Text));
            ThucHien.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
            ThucHien.Parameters.Add("@Original_ID_Luong", SqlDbType.Int);
            ThucHien.Parameters["@Original_ID_Luong"].Value = DataGridViewLuong.CurrentRow.Cells[0].Value;
            KetNoi.Open();
            ThucHien.ExecuteNonQuery();
            KetNoi.Close();
            HienLuong();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Ban co muon xoa luong cua " + ComboBoxHoTen.Text, "Chu y", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (D == DialogResult.Yes)
            {
                Lenh = @"DELETE FROM Luong
                   WHERE  (ID_Luong = @Original_ID_Luong)";
                ThucHien = new SqlCommand(Lenh, KetNoi);

                ThucHien.Parameters.Add("@Original_ID_Luong", SqlDbType.Int);
                ThucHien.Parameters["@Original_ID_Luong"].Value = DataGridViewLuong.CurrentRow.Cells[0].Value;
                KetNoi.Open();
                ThucHien.ExecuteNonQuery();
                KetNoi.Close();
                HienLuong();
            }
            else
            {

            }
        }
        //Xuất Excel
        private void ExportToExcel(DataGridView dgv)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel._Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Add(Type.Missing);
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "NhanVien";

                // Xuất tiêu đề cột
                for (int i = 1; i <= dgv.Columns.Count; i++)
                {
                    worksheet.Cells[1, i] = dgv.Columns[i - 1].HeaderText;
                }

                // Xuất dữ liệu
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // Lấy vùng dữ liệu đã dùng
                Excel.Range usedRange = worksheet.UsedRange;

                // Thêm border cho toàn bộ bảng
                usedRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                usedRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                // Format tiêu đề (dòng 1)
                Excel.Range headerRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, dgv.Columns.Count]];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = Color.LightGray; // nền xám nhạt
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                headerRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // AutoFit toàn bộ cột
                worksheet.Columns.AutoFit();

                // Hiển thị Excel
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
            finally
            {
                // Giải phóng COM object để tránh crash
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (excelApp != null) Marshal.ReleaseComObject(excelApp);

                worksheet = null;
                workbook = null;
                excelApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            ExportToExcel(DataGridViewLuong);
        }
    }
}

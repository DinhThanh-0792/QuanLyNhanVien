using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace QuanLyNhanVien
{
    internal class ClassTenDangNhap
    {
        // 🔹 Biến lưu tên đăng nhập hiện tại
        //public static string TenDangNhap = "";
        public static string TenDangNhap { get; set; }
        public static string VaiTro { get; set; } // Biến lưu vai trò (quyền) của người dùng
    }
}

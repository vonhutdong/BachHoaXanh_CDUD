using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_SanPhamKhoHang
    {
        private string maSanPham { get; set; }
        private string tenSanPham { get; set; }
        private string maLoaiHang { get; set; }
        private double giaBan { get; set; }
        private int soLuong { get; set; }
        private System.Data.Linq.Binary anhSanPham { get; set; }

        public string TenSanPham { get => tenSanPham; set => tenSanPham = value; }
        public string MaSanPham { get => maSanPham; set => maSanPham = value; }
        public string MaLoaiHang { get => maLoaiHang; set => maLoaiHang = value; }
        public double GiaBan { get => giaBan; set => giaBan = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public System.Data.Linq.Binary AnhSanPham { get => anhSanPham; set => anhSanPham = value; }
    }
}

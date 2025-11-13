using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ChiTietHoaDon
    {
        private int id;
        private int soLuong;
        private string maHoaDon;
        private string maSanPham;

        public DTO_ChiTietHoaDon(int soLuong, string mahoadon, string maSanPham)
        {
            this.soLuong = soLuong;
            maHoaDon = mahoadon;
            this.maSanPham = maSanPham;
        }


        public DTO_ChiTietHoaDon(int id, int soLuong, string maHoaDon, string maSanPham)
        {
            this.id = id;
            this.soLuong = soLuong;
            this.maHoaDon = maHoaDon;
            this.maSanPham = maSanPham;
        }

        public int Id { get => id; set => id = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public string MaHoaDon { get => maHoaDon; set => maHoaDon = value; }
        public string MaSanPham { get => maSanPham; set => maSanPham = value; }
    }
}

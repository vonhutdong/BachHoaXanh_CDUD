using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_KhoHang
    {
        private int id;
        private string maSanPham;
        private int soLuong;
        private string maChiNhanh;
        private int idChiTietPhieuNhap;

        public DTO_KhoHang() { }
        public DTO_KhoHang(int id, string maSanPham, int soLuong, string maChiNhanh)
        {
            this.Id = id;
            this.maSanPham = maSanPham;
            this.SoLuong = soLuong;
            this.maChiNhanh = maChiNhanh;
        }
        public DTO_KhoHang(int id, string maSanPham, int soLuong, string maChiNhanh, int idChiTietPhieuNhap)
        {
            this.Id = id;
            this.maSanPham = maSanPham;
            this.SoLuong = soLuong;
            this.maChiNhanh = maChiNhanh;
            this.idChiTietPhieuNhap = idChiTietPhieuNhap;
        }

        public int Id { get => id; set => id = value; }
        public string IdSanPham { get => maSanPham; set => maSanPham = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public string MaChiNhanh { get => maChiNhanh; set => maChiNhanh = value; }
        public int IdChiTietPhieuNhap { get => idChiTietPhieuNhap; set => idChiTietPhieuNhap = value; }
    }
}

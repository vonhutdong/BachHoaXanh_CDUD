using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_SanPham
    {
        private string maSanPham;
        private string tenSanPham;
        private string donViTinh;
        private float donGia;
        private DateTime ngaySanXuat;
        private DateTime hanSuDung;
        private string maLoaiHang;
        private string maNhaCungCap;
        private string maKhuyenMai;
        private byte[] anhSanPham;

        public DTO_SanPham() { }
        public DTO_SanPham(string maSanPham, string tenSanPham, string donViTinh, float donGia, DateTime ngaySanXuat, DateTime hanSuDung, string maLoaiHang, string maNhaCungCap, string maKhuyenMai, byte[] anhSanPham)
        {
            this.maSanPham = maSanPham;
            this.tenSanPham = tenSanPham;
            this.donViTinh = donViTinh;
            this.donGia = donGia;
            this.ngaySanXuat = ngaySanXuat;
            this.hanSuDung = hanSuDung;
            this.maLoaiHang = maLoaiHang;
            this.maNhaCungCap = maNhaCungCap;
            this.maKhuyenMai = maKhuyenMai;
            this.anhSanPham = anhSanPham;
        }
        public DTO_SanPham( string tenSanPham, string donViTinh, float donGia, DateTime ngaySanXuat, DateTime hanSuDung, string maLoaiHang, string maNhaCungCap, string maKhuyenMai, byte[] anhSanPham)
        {
            this.tenSanPham = tenSanPham;
            this.donViTinh = donViTinh;
            this.donGia = donGia;
            this.ngaySanXuat = ngaySanXuat;
            this.hanSuDung = hanSuDung;
            this.maLoaiHang = maLoaiHang;
            this.maNhaCungCap = maNhaCungCap;
            this.maKhuyenMai = maKhuyenMai;
            this.anhSanPham = anhSanPham;
        }

        public string MaSanPham { get => maSanPham; set => maSanPham = value; }
        public string TenSanPham { get => tenSanPham; set => tenSanPham = value; }
        public string DonViTinh { get => donViTinh; set => donViTinh = value; }
        public float DonGia { get => donGia; set => donGia = value; }
        public DateTime NgaySanXuat { get => ngaySanXuat; set => ngaySanXuat = value; }
        public DateTime HanSuDung { get => hanSuDung; set => hanSuDung = value; }
        public string MaLoaiHang { get => maLoaiHang; set => maLoaiHang = value; }
        public string MaNhaCungCap { get => maNhaCungCap; set => maNhaCungCap = value; }
        public string MaKhuyenMai { get => maKhuyenMai; set => maKhuyenMai = value; }
        public byte[] AnhSanPham { get => anhSanPham; set => anhSanPham = value; }
    }
}

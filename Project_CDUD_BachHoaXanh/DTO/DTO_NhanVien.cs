using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_NhanVien
    {
        private string maNV;
        private string tenNV;
        private string SoDT;
        private string diaChi;
        private string loaiNhanVien;
        private string maChucVu;
        private string maChiNhanh;
        private string maTaiKhoan;


        public DTO_NhanVien() { }
        public DTO_NhanVien(string maNV, string tenNV, string soDT, string diaChi, string loaiNhanVien, string maChucVu, string maChiNhanh, string maTaiKhoan)
        {
            this.MaNV = maNV;
            this.TenNV = tenNV;
            this.SoDT1 = soDT;
            this.DiaChi = diaChi;
            this.LoaiNhanVien = loaiNhanVien;
            this.MaChucVu = maChucVu;
            this.MaChiNhanh = maChiNhanh;
            this.MaTaiKhoan = maTaiKhoan;
        }
        public DTO_NhanVien( string tenNV, string soDT, string diaChi, string loaiNhanVien, string maChucVu, string maChiNhanh, string maTaiKhoan)
        {
            this.TenNV = tenNV;
            this.SoDT1 = soDT;
            this.DiaChi = diaChi;
            this.LoaiNhanVien = loaiNhanVien;
            this.MaChucVu = maChucVu;
            this.MaChiNhanh = maChiNhanh;
            this.MaTaiKhoan = maTaiKhoan;
        }
        public string MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public string SoDT1 { get => SoDT; set => SoDT = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string LoaiNhanVien { get => loaiNhanVien; set => loaiNhanVien = value; }
        public string MaChucVu { get => maChucVu; set => maChucVu = value; }
        public string MaChiNhanh { get => maChiNhanh; set => maChiNhanh = value; }
        public string MaTaiKhoan { get => maTaiKhoan; set => maTaiKhoan = value; }
    }
}

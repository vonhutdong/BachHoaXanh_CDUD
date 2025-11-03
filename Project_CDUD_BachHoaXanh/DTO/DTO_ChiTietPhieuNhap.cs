using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ChiTietPhieuNhap
    {

        // Fields
        private int id;
        private int soLuong;
        private float donGia;
        private string  maPhieuNhap;
        private string maSanPham;

        // Constructors
        public DTO_ChiTietPhieuNhap() { }

        public DTO_ChiTietPhieuNhap(int id, int soLuong, float donGia, string maPhieuNhap, string maSanPham)
        {
            this.id = id;
            this.soLuong = soLuong;
            this.donGia = donGia;
            this.maPhieuNhap = maPhieuNhap;
            this.maSanPham = maSanPham;
        }

        public DTO_ChiTietPhieuNhap(int soLuong, float donGia, string maPhieuNhap, string maSanPham)
        {
            this.soLuong = soLuong;
            this.donGia = donGia;
            this.maPhieuNhap = maPhieuNhap;
            this.maSanPham = maSanPham;
        }

        // Properties
        public int Id { get => id; set => id = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public float DonGia { get => donGia; set => donGia = value; }
        public string MaPhieuNhap { get => maPhieuNhap; set => maPhieuNhap = value; }
        public string MaSanPham { get => maSanPham; set => maSanPham = value; }
    }

}

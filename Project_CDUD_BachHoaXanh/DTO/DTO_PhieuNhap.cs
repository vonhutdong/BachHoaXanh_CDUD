using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_PhieuNhap
    {
        private string maPhieuNhap;
        private DateTime ngayNhap;
        private float thanhTien;
        private string maNhanVien;
        private string maChiNhanh;
        public DTO_PhieuNhap()
        {

        }
        public DTO_PhieuNhap(string maPhieuNhap, DateTime ngayNhap, float thanhTien, string maNhanVien, string maChiNhanh)
        {
            this.maPhieuNhap = maPhieuNhap;
            this.ngayNhap = ngayNhap;
            this.thanhTien = thanhTien;
            this.maNhanVien = maNhanVien;
            this.maChiNhanh = maChiNhanh;
        }

        public string MaPhieuNhap { get => maPhieuNhap; set => maPhieuNhap = value; }
        public DateTime NgayNhap { get => ngayNhap; set => ngayNhap = value; }
        public float ThanhTien { get => thanhTien; set => thanhTien = value; }
        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public string MaChiNhanh { get => maChiNhanh; set => maChiNhanh = value; }
    }
}

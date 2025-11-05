using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_HoaDon
    {
        private string maHoaDon;
        private DateTime ngayLapHD;
        private DateTime gioLapHD;
        private float tongTien;
        private float thanhTien;
        private string phuongThucThanhToan;
        private string maKhachHang;
        private string maNhanVien;

        public DTO_HoaDon(string maHoaDon, DateTime ngayLapHD, DateTime gioLapHD, float tongTien, float thanhTien, string phuongThucThanhToan, string maKhachHang, string maNhanVien)
        {
            this.maHoaDon = maHoaDon;
            this.ngayLapHD = ngayLapHD;
            this.gioLapHD = gioLapHD;
            this.tongTien = tongTien;
            this.thanhTien = thanhTien;
            this.phuongThucThanhToan = phuongThucThanhToan;
            this.maKhachHang = maKhachHang;
            this.maNhanVien = maNhanVien;
        }
        public DTO_HoaDon(string maHoaDon, string maKhachHang, string maNhanVien,string phuongThucThanhToan)
        {
            this.maHoaDon = maHoaDon;
            this.maKhachHang = maKhachHang;
            this.maNhanVien = maNhanVien;
            this.phuongThucThanhToan = phuongThucThanhToan;
        }
        public string MaHoaDon { get => maHoaDon; set => maHoaDon = value; }
        public DateTime NgayLapHD { get => ngayLapHD; set => ngayLapHD = value; }
        public DateTime GioLapHD { get => gioLapHD; set => gioLapHD = value; }
        public float TongTien { get => tongTien; set => tongTien = value; }
        public float ThanhTien { get => thanhTien; set => thanhTien = value; }
        public string PhuongThucThanhToan { get => phuongThucThanhToan; set => phuongThucThanhToan = value; }
        public string MaKhachHang { get => maKhachHang; set => maKhachHang = value; }
        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
    }
}

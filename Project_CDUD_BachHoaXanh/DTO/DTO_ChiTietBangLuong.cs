using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ChiTietBangLuong
    {
        private string maChiTietBangLuong;
        private float soGioCongThucTe;
        private string maBangLuong;
        private string maLichLam;
        private DateTime ngayLam;

        public DTO_ChiTietBangLuong(string maChiTietBangLuong, float soGioCongThucTe, string maBangLuong, string maLichLam, DateTime ngayLam)
        {
            this.MaChiTietBangLuong = maChiTietBangLuong;
            this.SoGioCongThucTe = soGioCongThucTe;
            this.MaBangLuong = maBangLuong;
            this.MaLichLam = maLichLam;
            this.NgayLam = ngayLam;
        }
        public DTO_ChiTietBangLuong() { }
        public DTO_ChiTietBangLuong(float soGioCongThucTe, string maBangLuong, string maLichLam, DateTime ngayLam)
        {
            this.SoGioCongThucTe = soGioCongThucTe;
            this.MaBangLuong = maBangLuong;
            this.MaLichLam = maLichLam;
            this.NgayLam = ngayLam;
        }

        public string MaChiTietBangLuong { get => maChiTietBangLuong; set => maChiTietBangLuong = value; }
        public float SoGioCongThucTe { get => soGioCongThucTe; set => soGioCongThucTe = value; }
        public string MaBangLuong { get => maBangLuong; set => maBangLuong = value; }
        public string MaLichLam { get => maLichLam; set => maLichLam = value; }
        public DateTime NgayLam { get => ngayLam; set => ngayLam = value; }
    }
}

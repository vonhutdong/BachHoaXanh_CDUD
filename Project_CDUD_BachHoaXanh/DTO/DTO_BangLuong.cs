using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_BangLuong
    {
        private string maBangLuong;
        private DateTime thangNam;
        private float tongGioCong;
        private float _luong;
        private string maNhanVien;

        public DTO_BangLuong(string maBangLuong, DateTime thangNam, float tongGioCong, float luong, string maNhanVien)
        {
            this.MaBangLuong = maBangLuong;
            this.ThangNam = thangNam;
            this.TongGioCong = tongGioCong;
            Luong = luong;
            this.MaNhanVien = maNhanVien;
        }
        public DTO_BangLuong( DateTime thangNam, float tongGioCong, float luong, string maNhanVien)
        {
            this.ThangNam = thangNam;
            this.TongGioCong = tongGioCong;
            Luong = luong;
            this.MaNhanVien = maNhanVien;
        }
        public DTO_BangLuong() { }
        public string MaBangLuong { get => maBangLuong; set => maBangLuong = value; }
        public DateTime ThangNam { get => thangNam; set => thangNam = value; }
        public float TongGioCong { get => tongGioCong; set => tongGioCong = value; }
        public float Luong { get => _luong; set => _luong = value; }
        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_KhachHang
    {
        
        private string maKH;
        private string tenKH;
        private string soDienThoai;
        private string diaChi;
        private float diem;
        private string capBac;

        // Constructor đầy đủ
        public DTO_KhachHang(string maKH, string tenKH, string soDienThoai, string diaChi, float diem, string capBac)
        {
            this.MaKH = maKH;
            this.TenKH = tenKH;
            this.SoDienThoai = soDienThoai;
            this.DiaChi = diaChi;
            this.Diem = diem;
            this.capBac = capBac;
        }

        public DTO_KhachHang(string tenKH, string soDienThoai, string diaChi, float diem,string capBac)
        {
            this.TenKH = tenKH;
            this.SoDienThoai = soDienThoai;
            this.DiaChi = diaChi;
            this.Diem = diem;
            this.capBac = capBac;
        }

        // Constructor rỗng
        public DTO_KhachHang() { }

        public string MaKH { get => maKH; set => maKH = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public float Diem { get => diem; set => diem = value; }
        public string CapBac { get => capBac; set => capBac = value; }
    }
}


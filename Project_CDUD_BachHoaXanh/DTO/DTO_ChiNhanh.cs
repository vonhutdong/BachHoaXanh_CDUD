using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ChiNhanh
    {
    
        private string maChiNhanh;
        private string tenChiNhanh;
        private string diaChi;
        private string soDienThoai;

        public DTO_ChiNhanh() { }
        public DTO_ChiNhanh(string maChiNhanh, string tenChiNhanh, string diaChi, string soDienThoai)
        {
            this.MaChiNhanh = maChiNhanh;
            this.TenChiNhanh = tenChiNhanh;
            this.DiaChi = diaChi;
            this.SoDienThoai = soDienThoai;
        }
        public DTO_ChiNhanh( string tenChiNhanh, string diaChi, string soDienThoai)
        {
            this.TenChiNhanh = tenChiNhanh;
            this.DiaChi = diaChi;
            this.SoDienThoai = soDienThoai;
        }


        public string MaChiNhanh { get => maChiNhanh; set => maChiNhanh = value; }
        public string TenChiNhanh { get => tenChiNhanh; set => tenChiNhanh = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
    }
}

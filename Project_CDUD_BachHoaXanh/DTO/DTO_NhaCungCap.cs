using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_NhaCungCap
    {
        //Fields
        private string maNhaCungCap;
        private string tenNhaCungCap;
        private string soDienThoai;
        private string diaChi;


        //Constructor
        public DTO_NhaCungCap()
        {
        }

        public DTO_NhaCungCap(string maNhaCungCap, string tenNhaCungCap, string soDienThoai, string diaChi)
        {
            this.maNhaCungCap = maNhaCungCap;
            this.tenNhaCungCap = tenNhaCungCap;
            this.soDienThoai = soDienThoai;
            this.diaChi = diaChi;
        }

        public DTO_NhaCungCap(string tenNhaCungCap, string soDienThoai, string diaChi)
        {
            this.tenNhaCungCap = tenNhaCungCap;
            this.soDienThoai = soDienThoai;
            this.diaChi = diaChi;
        }

        public string MaNhaCungCap { get => maNhaCungCap; set => maNhaCungCap = value; }
        public string TenNhaCungCap { get => tenNhaCungCap; set => tenNhaCungCap = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
    }
}


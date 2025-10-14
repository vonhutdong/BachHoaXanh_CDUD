using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_LoaiHang
    {
        private string maLoaiHang;
        private string tenLoaiHang;

        public DTO_LoaiHang(string maLoaiHang, string tenLoaiHang)
        {
            this.maLoaiHang = maLoaiHang;
            this.tenLoaiHang = tenLoaiHang;
        }

        public DTO_LoaiHang() { }

 
        public string MaLoaiHang { get => maLoaiHang; set => maLoaiHang = value; }
        public string TenLoaiHang { get => tenLoaiHang; set => tenLoaiHang = value; }
    }
}

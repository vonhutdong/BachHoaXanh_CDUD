using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_KhuyenMai
    {
        // Fields
        private string maKhuyenMai;
        private string tenKhuyenMai;
        private double giaTri;


        // Constructors
        public DTO_KhuyenMai()
        {

        }    

        public DTO_KhuyenMai(string maKhuyenMai, string tenKhuyenMai, double giaTri)
        {
            this.maKhuyenMai = maKhuyenMai;
            this.tenKhuyenMai = tenKhuyenMai;
            this.giaTri = giaTri;
        }

        public DTO_KhuyenMai(string tenKhuyenMai, double giaTri)
        {
            this.tenKhuyenMai = tenKhuyenMai;
            this.giaTri = giaTri;
        }

        // Properties
   
        public string MaKhuyenMai { get => maKhuyenMai; set => maKhuyenMai = value; }
        public string TenKhuyenMai { get => tenKhuyenMai; set => tenKhuyenMai = value; }
        public double GiaTri { get => giaTri; set => giaTri = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_TaiKhoan
    {
        
        private string maTaiKhoan;
        private string tenTaiKhoan;
        private string matKhau;
        private int quyen;

        // Constructors
        public DTO_TaiKhoan()
        {
        }

        public DTO_TaiKhoan(string maTaiKhoan, string tenTaiKhoan, string matKhau, int quyen)
        {
            this.maTaiKhoan = maTaiKhoan;
            this.tenTaiKhoan = tenTaiKhoan;
            this.matKhau = matKhau;
            this.quyen = quyen;
        }


        public DTO_TaiKhoan(string tenTaiKhoan, string matKhau, int quyen)
        {
            this.tenTaiKhoan = tenTaiKhoan;
            this.matKhau = matKhau;
            this.quyen = quyen;
        }

        // Properties
        public string MaTaiKhoan { get => maTaiKhoan; set => maTaiKhoan = value; }
        public string TenTaiKhoan { get => tenTaiKhoan; set => tenTaiKhoan = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
        public int Quyen { get => quyen; set => quyen = value; }
    }
}

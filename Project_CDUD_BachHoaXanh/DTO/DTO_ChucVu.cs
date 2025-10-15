using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ChucVu
    {
        private string maChucvu;
        private string tenChucVu;

        public DTO_ChucVu(string maChucvu, string tenChucVu)
        {
            this.MaChucvu = maChucvu;
            this.TenChucVu = tenChucVu;
        }

        public DTO_ChucVu() { }
        public DTO_ChucVu (string tenChucVu )
        {
            this.TenChucVu = tenChucVu;
        }
        public string MaChucvu { get => maChucvu; set => maChucvu = value; }
        public string TenChucVu { get => tenChucVu; set => tenChucVu = value; }
    }
}

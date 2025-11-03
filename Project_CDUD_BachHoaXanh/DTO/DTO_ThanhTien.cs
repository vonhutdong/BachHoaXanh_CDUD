using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_ThanhTien
    {
        private int thanhTien;

        // Constructors
        public DTO_ThanhTien(int thanhTien)
        {
            this.thanhTien = thanhTien;
        }

        public DTO_ThanhTien()
        {
            this.thanhTien = 0;
        }

        // Properties
        public int ThanhTien { get => thanhTien; set => thanhTien = value; }
    }
}

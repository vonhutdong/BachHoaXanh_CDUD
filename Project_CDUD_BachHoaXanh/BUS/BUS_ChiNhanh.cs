using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_ChiNhanh
    {
        private DAL_ChiNhanh dal_chiNhanh = new DAL_ChiNhanh();
        public IQueryable LayDSChiNhanh()
        {
            return dal_chiNhanh.LayDSChiNhanh();
        }
        public void themChiNhanh(DTO_ChiNhanh chiNhanh)
        {
            dal_chiNhanh.ThemChiNhanh(chiNhanh);
        }
        public void XoaChiNhanh(string maChiNhanh)
        {
            dal_chiNhanh.XoaChiNhanh(maChiNhanh);
        }
        public void suaChinhNhanh(DTO_ChiNhanh chiNhanh)
        {
            dal_chiNhanh.suaChiNhanh(chiNhanh);
        }
    }
}

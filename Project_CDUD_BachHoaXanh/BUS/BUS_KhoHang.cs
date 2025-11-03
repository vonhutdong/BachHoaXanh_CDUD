using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class BUS_KhoHang
    {
        DAL_KhoHang dal_kh = new DAL_KhoHang();
        //load kho hàng
        public IQueryable LoadKhoHang()
        {
            return dal_kh.LoadKhoHang();
        }
    }
}

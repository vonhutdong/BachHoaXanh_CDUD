using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_ChiTietHoa
    {
        DAL_ChiTietHoaDon dal_CTHD = new DAL_ChiTietHoaDon();

        public IQueryable GetListCTHD()
        {
            return dal_CTHD.GetListCTHD();
        }

        public void AddCTHD(DTO_ChiTietHoaDon chiTietHoaDon)
        {
            dal_CTHD.AddCTHD(chiTietHoaDon);
        }
        public IQueryable GetListCTHDTheoMaHD(string idMaHD)
        {
            return dal_CTHD.GetListCTHDTheoMaHD(idMaHD);
        }
        public void AddCTHD2(DTO_ChiTietHoaDon chiTietHoaDon)
        {
            dal_CTHD.AddCTHD2(chiTietHoaDon);
        }
        public int GetTotalCashByIdHd(string idHd)
        {
            return dal_CTHD.GetTotalCashByIdHd(idHd);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;


namespace BUS
{
    public class BUS_ChiTietPhieuNhap
    {
        private DAL_ChiTietPhieuNhap dalCTPN = new DAL_ChiTietPhieuNhap();

        public IQueryable LayDSCTPN()
        {
            return dalCTPN.LayDSChiTietPhieuNhap();
        }

        public IQueryable LayDSSP()
        {
            return dalCTPN.LayDSSP();
        }
        public bool ThemChiTiet(DTO_ChiTietPhieuNhap ct)
        {
            return dalCTPN.ThemChiTiet(ct);

        }
        public void XoaChiTiet(int idChiTiet)
        {
            dalCTPN.XoaChiTiet(idChiTiet);
        }
        public bool SuaChiTiet(DTO_ChiTietPhieuNhap ct)
        {
            return dalCTPN.SuaChiTiet(ct);
        }
    }
}

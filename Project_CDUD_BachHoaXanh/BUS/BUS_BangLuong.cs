using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_BangLuong
    {
        DAL_BangLuong dal_bangluong = new DAL_BangLuong();
        public IQueryable LayDSBangLuong()
        {
            return dal_bangluong.LayDSBangLuong();
        }
        public IQueryable LayDSBangLuong(string maNhanVien)
        {
            return dal_bangluong.LayDSBangLuong(maNhanVien);
        }
        public IQueryable LayBangLuongTheoThang(string maNhanVien, int thang, int nam)
        {
            return dal_bangluong.LayBangLuongTheoThang(maNhanVien, thang, nam);
        }
        public BangLuong LayBangLuongTheoThang1(string maNhanVien, int thang, int nam)
        {
            return dal_bangluong.LayBangLuongTheoThang1(maNhanVien, thang, nam);
        }
        public IQueryable DSNhanVien()
        {
            return dal_bangluong.DSNhanVien();
        }
        public void ThemBangLuong(DTO_BangLuong bangluong)
        {
            dal_bangluong.ThemBangLuong(bangluong);
        }
        public BangLuong LayHoacTaoBangLuong(string maNV, int thang, int nam)
        {
            return dal_bangluong.LayHoacTaoBangLuong(maNV, thang, nam);
        }
    }
}

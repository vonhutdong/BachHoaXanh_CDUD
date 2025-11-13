using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_ChiTietBangLuong
    {
        DAL_ChiTietBangLuong dal_ctbangluong = new DAL_ChiTietBangLuong();
        public IQueryable LayDSCTBangLuong()
        {
            return dal_ctbangluong.LayDSBangLuong();
        }
        public IQueryable LayDSCTBangLuong(string maBangLuong)
        {
            return dal_ctbangluong.LayDSChiTietBangLuong(maBangLuong);
        }
        public void ThemChiTietbangLuong(DTO_ChiTietBangLuong ctbl)
        {
            dal_ctbangluong.ThemChiTietBangLuong(ctbl);
        }
        public IQueryable LayDSLichLam()
        {
            return dal_ctbangluong.LayDSLichLam();
        }
        public int UpdateChiTietBangLuong_Only(string maLichLam, string maNV, DateTime ngayLam, double gioLam)
        {
            return dal_ctbangluong.UpdateChiTietBangLuong_Only(maLichLam, maNV, ngayLam, gioLam);
        }
    }
}

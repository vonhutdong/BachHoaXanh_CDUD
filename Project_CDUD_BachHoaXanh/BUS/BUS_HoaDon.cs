using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_HoaDon
    {
        DAL_HoaDon dal_hd = new DAL_HoaDon();

        public IQueryable GetListHD()
        {
            return dal_hd.GetListHD();
        }
        public IQueryable GetHoaDonByMa(string maHD)
        {
            return dal_hd.GetHoaDonByMa(maHD);
        }
        public bool DelHD(string maHD)
        {

            return dal_hd.DeleteHD(maHD);
        }

        public bool updateHD(DTO_HoaDon dto_hd)
        {
            return dal_hd.UpdateHD(dto_hd);
        }
        public bool updateThanhTienHD(string maHD, double thanhTien)
        {
            return dal_hd.UpdateThanhTienHD(maHD, thanhTien);
        }
        public void CapNhatTongTien(string maHD, double tongTienMoi)
        {
            dal_hd.CapNhatTongTien(maHD, tongTienMoi);
        }
        public IQueryable TimKiemHD(string tuKhoa)
        {
            return dal_hd.TimKiemHD(tuKhoa);
        }
        public string GetMaxIdHD()
        {
            return dal_hd.GetMaxIdHD();
        }
        public string TimMaHoaDon(string id)
        {
            return dal_hd.TimMaHoaDon(id);
        }
        public void AddHD2(DTO_HoaDon hoaDon)
        {
            dal_hd.AddHD2(hoaDon);
        }
    }
}

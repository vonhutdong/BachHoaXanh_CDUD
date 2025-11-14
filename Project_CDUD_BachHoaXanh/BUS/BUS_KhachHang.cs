using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUS_KhachHang
    {
        private DAL_KhachHang dal_kh = new DAL_KhachHang();

        public IQueryable LayDSKH()
        {
            return dal_kh.LayDSKH();
        }
        public DTO_KhachHang LayKhachHang_SDT(string sodienthoai)
        {
            return dal_kh.LayKhachHang_SDT(sodienthoai);
        }
        public bool ThemKhachHang(DTO_KhachHang khachHang)
        {
            return dal_kh.ThemKH(khachHang);
        }
        public bool SuaKhachHang(DTO_KhachHang khachHang)
        {
            return dal_kh.SuaKH(khachHang);
        }
        public bool XoaKH(string khachHang)
        {
            return dal_kh.XoaKhachHang(khachHang);
        }
        public IQueryable<DTO_KhachHang> TimKiemTheoTenHoacSDT(string tukhoa)
        {
            return dal_kh.TimKiemTheoTenHoacSDT(tukhoa);
        }
        public bool DiemCong(string soDienThoai, int diemCong)
        {
            return dal_kh.DiemCong(soDienThoai, diemCong);
        }
        public DTO_KhachHang LayKHMacDinh(string maKH)
        {
            return dal_kh.LayKhachHangMatDinh(maKH);
        }
        public DTO_KhachHang GetKhachHangTheoMa (string maKH)
        {
            return dal_kh.GetKhachHangTheoMa(maKH);
        }
        public string LayCapBac(string maKH)
        {
            return dal_kh.LayCapBac(maKH);
        }
    }
}

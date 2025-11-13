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
        public IQueryable LayDSSP()
        {
            return dal_kh.LayDSSP();
        }
        public IQueryable LayDSCN()
        {
            return dal_kh.layDSCN();
        }
        public bool SuaKhoHang(string maSanPhamCu, string maChiNhanhCu, string maSanPhamMoi, string maChiNhanhMoi, int soLuongMoi)
        {
            return dal_kh.SuaKhoHang(maSanPhamCu, maChiNhanhCu, maSanPhamMoi, maChiNhanhMoi, soLuongMoi);
        }
        public void CapNhatSoLuong(string maSP, int soluong)
        {
            dal_kh.CapNhatSoLuong(maSP, soluong);
        }
    }
}

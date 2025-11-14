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
        public int GetChiTietTheoMa(string maHD, string maSP)
        {
            return dal_CTHD.GetChiTietTheoMa(maHD, maSP);
        }

        public void AddCTHD(DTO_ChiTietHoaDon chiTietHoaDon)
        {
            dal_CTHD.AddCTHD(chiTietHoaDon);
        }
        public IQueryable GetListCTHDTheoMaHD(string idMaHD)
        {
            return dal_CTHD.GetListCTHDTheoMaHD(idMaHD);
        }
        public IQueryable<dynamic> GetListCTHDTheoMaHD1 (string mahd)
        {
            return dal_CTHD.GetListCTHDTheoMaHD1(mahd);
        }
        public void AddCTHD2(DTO_ChiTietHoaDon chiTietHoaDon)
        {
            dal_CTHD.AddCTHD2(chiTietHoaDon);
        }
        public int GetTotalCashByIdHd(string idHd)
        {
            return dal_CTHD.GetTotalCashByIdHd(idHd);
        }
        public IQueryable LayDSSanPhamTheoMaHD(string maHD)
        {
            return dal_CTHD.LayDSSanPhamTheoMaSP(maHD);
        }
        public IQueryable SreachChiTietHoaDonTheoDonGia(string maHD, double tukhoa)
        {
            return dal_CTHD.SreachChiTietHoaDonTheoDonGia(maHD, tukhoa);
        }
        public IQueryable SreachChiTietHoaDonTheoMaSP(string maHD, string tukhoa)
        {
            return dal_CTHD.SreachChiTietHoaDonTheoMaSP(maHD, tukhoa);
        }
        public IQueryable SreachSanPhamTheoMaSP(string maHD, string tukhoa)
        {
            return dal_CTHD.SreachSanPhamTheoMaSP(maHD, tukhoa);
        }

        public IQueryable SreachSanPhamTheoDonGia(string maHD, double tukhoa)
        {
            return dal_CTHD.SreachSanPhamTheoDonGia(maHD, tukhoa);
        }
        public double LayDonGiaTheoMaSP(string maSP)
        {
            return dal_CTHD.LayDonGiaTheoMaSP(maSP);
        }
        public int UpdateCTHD(string maHD, string maSP, int soLuong)
        {
            return dal_CTHD.UpdateCTHD(maHD,maSP,soLuong);
        }
        public decimal TinhTongTienTheoMaHD_TuSoLuongVaDonGia(string maHD)
        {
            return dal_CTHD.TinhTongTienTheoMaHD_TuSoLuongVaDonGia(maHD);
        }
    }
}

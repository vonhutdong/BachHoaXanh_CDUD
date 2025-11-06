using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_SanPham
    {
        public IQueryable LoadNCC()
        {
            return dal_sp.LoadNhaCungCap();
        }
        //dal sản phẩm
        DAL_SanPham dal_sp = new DAL_SanPham();
        public IQueryable LoadDSSanPham()
        {
            return dal_sp.LayDSSanPham();
        }
        public bool ThemSanPham(DTO_SanPham sanpham)
        {
            return dal_sp.ThemSanPham(sanpham);
        }
        //xoa san pham
        public bool XoaSanPham(string maSP)
        {
            return dal_sp.XoaSanPham(maSP);
        }
        public bool SuaSanPham(DTO_SanPham sanPham)
        {
            return dal_sp.SuaSanPham(sanPham);
        }
        public List<DTO_SanPhamKhoHang> ListSanPham()
        {
            return dal_sp.ListSanPham();
        }
        //public List<DTO_SanPhamKhoHang> ListTimKiemSanPhamBangMa(string tukhoa)
        //{
        //    return dal_sp.ListTimKiemSanPhamBangMa(tukhoa);
        //}
        public List<DTO_SanPhamKhoHang> ListSP_BH()
        {
            return dal_sp.ListSanPham_BanHang();
        }

        public IQueryable LocSpTheoTen(string key)
        {
            return dal_sp.LocSpTheoTen(key);
        }

        public int GetSoLuongSpTrongKho(string maSP)
        {
            return dal_sp.GetSoLuongSpTrongKho(maSP);
        }

        public IQueryable GetListSP()
        {
            return dal_sp.GetListSP();
        }

        public IQueryable SearchSpByTenSP(string tenSp)
        {
            return dal_sp.SearchSpByTenSP(tenSp);
        }

        public IQueryable SearchSpByDVT(string donViTinh)
        {
            return dal_sp.SearchSpByDVT(donViTinh);
        }
        public bool KiemTraTrung(string tenSP, string donViTinh, float donGia, string maSPHienTai = null)
        {
            return dal_sp.KiemTraTrung(tenSP, donViTinh, donGia, maSPHienTai);
        }
        public int LaySoLuongTonKho(string maSP)
        {
            return dal_sp.LaySoLuongTonKho(maSP);
        }
    }
}

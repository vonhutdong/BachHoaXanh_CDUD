using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_KhoHang
    {
        // Fields
        private DatabaseAccess da = new DatabaseAccess();
        // Methods
        public IQueryable LoadKhoHang()
        {
            var result = from kh in da.Db.KhoHangs
                         join sp in da.Db.SanPhams on kh.maSanPham equals sp.maSanPham
                         join cn in da.Db.ChiNhanhs on kh.maChiNhanh equals cn.MaChiNhanh
                         group kh by new { kh.maSanPham, sp.tenSanPham, kh.maChiNhanh, cn.TenChiNhanh } into g
                        
                         select new
                         {
                             MaSanPham = g.Key.maSanPham,
                             TenSanPham = g.Key.tenSanPham,
                             SoLuong = g.Sum(kh => kh.soLuong),
                             MaChiNhanh = g.Key.maChiNhanh,
                             TenChiNhanh = g.Key.TenChiNhanh,
                             id = g.Select(kh => kh.id).FirstOrDefault() // hoặc default nếu không cần
                         };
            return result;
        }
        public IQueryable LayDSSP()
        {
            IQueryable temp = from ll in da.Db.SanPhams
                              select new
                              {
                                  ll.maSanPham,
                                  ll.tenSanPham,
                              };
            return temp;
        }
        public IQueryable layDSCN()
        {
            IQueryable temp = from kh in da.Db.ChiNhanhs
                              select new
                              {
                                  kh.MaChiNhanh,
                                  kh.TenChiNhanh,
                              };
            return temp;
        }
        public bool ThemHoacCapNhatKho(string maSanPham, string maChiNhanh, int soLuong)
        {
            try
            {
                if (string.IsNullOrEmpty(maSanPham))
                    throw new Exception("Mã sản phẩm không được để trống!");
                if (string.IsNullOrEmpty(maChiNhanh))
                    throw new Exception("Mã chi nhánh không được để trống!");
                if (soLuong <= 0)
                    throw new Exception("Số lượng phải lớn hơn 0!");

                // ✅ Tìm xem sản phẩm này đã có trong kho chi nhánh chưa
                var kho = da.Db.KhoHangs.FirstOrDefault(
                    k => k.maSanPham == maSanPham && k.maChiNhanh == maChiNhanh);

                if (kho != null)
                {
                    // ✅ Đã tồn tại → cập nhật tăng số lượng
                    kho.soLuong += soLuong;
                }
                else
                {
                    // ✅ Chưa có → thêm mới
                    KhoHang khMoi = new KhoHang
                    {
                        maSanPham = maSanPham,
                        maChiNhanh = maChiNhanh,
                        soLuong = soLuong
                    };
                    da.Db.KhoHangs.InsertOnSubmit(khMoi);
                }

                da.Db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm/cập nhật kho hàng: " + ex.Message);
            }
        }
        public bool SuaKhoHang(string maSanPhamCu, string maChiNhanhCu, string maSanPhamMoi, string maChiNhanhMoi, int soLuongMoi)
        {
            try
            {
                if (string.IsNullOrEmpty(maSanPhamCu) || string.IsNullOrEmpty(maChiNhanhCu))
                    throw new Exception("Thiếu mã kho hàng cần sửa!");
                if (string.IsNullOrEmpty(maSanPhamMoi))
                    throw new Exception("Mã sản phẩm mới không được để trống!");
                if (string.IsNullOrEmpty(maChiNhanhMoi))
                    throw new Exception("Mã chi nhánh mới không được để trống!");
                if (soLuongMoi < 0)
                    throw new Exception("Số lượng không được âm!");

                // ✅ Tìm kho hàng cần sửa
                var kho = da.Db.KhoHangs.FirstOrDefault(
                    k => k.maSanPham == maSanPhamCu && k.maChiNhanh == maChiNhanhCu);

                if (kho == null)
                    throw new Exception("Không tìm thấy kho hàng để sửa.");

                // ✅ Cập nhật thông tin mới
                kho.maSanPham = maSanPhamMoi;
                kho.maChiNhanh = maChiNhanhMoi;
                kho.soLuong = soLuongMoi;

                da.Db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa kho hàng: " + ex.Message);
            }
        }



    }
}

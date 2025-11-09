using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void CongSoLuong(string maSanPham, string maChiNhanh, int soLuong)
        {
            var kho = da.Db.KhoHangs.FirstOrDefault(k => k.maSanPham == maSanPham && k.maChiNhanh == maChiNhanh);
            if (kho == null)
            {
                // Nếu chưa có kho, tự động thêm mới
                kho = new KhoHang
                {
                    maSanPham = maSanPham,
                    maChiNhanh = maChiNhanh,
                    soLuong = soLuong
                };
                da.Db.KhoHangs.InsertOnSubmit(kho);
            }
            else
            {
                kho.soLuong += soLuong;
            }
            da.Db.SubmitChanges();
        }

        public void TruSoLuong(string maSanPham, string maChiNhanh, int soLuong)
        {
            var kho = da.Db.KhoHangs.FirstOrDefault(k => k.maSanPham == maSanPham && k.maChiNhanh == maChiNhanh);
            if (kho != null)
            {
                kho.soLuong -= soLuong;
                if (kho.soLuong < 0) kho.soLuong = 0;
                da.Db.SubmitChanges();
            }
        }
        private string RemoveDiacritics(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
        //public IQueryable<DTO_KhoHang> TimKiemSanPhamTrongKho(string tuKhoa)
        //{
        //    string keyword = RemoveDiacritics(tuKhoa.ToLower());

        //    var danhSach = da.Db.KhoHangs
        //        .Join(da.Db.SanPhams,
        //              kh => kh.maSanPham,
        //              sp => sp.maSanPham,
        //              (kh, sp) => new { kh, sp })
        //        .Join(da.Db.ChiNhanhs,
        //              temp => temp.kh.maChiNhanh,
        //              cn => cn.MaChiNhanh,
        //              (temp, cn) => new
        //              {
        //                  temp.kh.maSanPham,
        //                  temp.sp.tenSanPham,
        //                  temp.kh.soLuong,
        //                  temp.kh.maChiNhanh,
        //                  cn.TenChiNhanh
        //              })
        //        .AsEnumerable() // Xử lý tìm kiếm không dấu trong bộ nhớ
        //        .Where(x =>
        //            (!string.IsNullOrEmpty(x.tenSanPham) &&
        //                RemoveDiacritics(x.tenSanPham.ToLower()).Contains(keyword))
        //            || (!string.IsNullOrEmpty(x.maSanPham) &&
        //                x.maSanPham.ToLower().Contains(keyword))
        //            || (!string.IsNullOrEmpty(x.TenChiNhanh) &&
        //                RemoveDiacritics(x.TenChiNhanh.ToLower()).Contains(keyword))
        //        )
        //        .GroupBy(x => new { x.maSanPham, x.tenSanPham, x.maChiNhanh, x.TenChiNhanh })
        //        .Select(g => new
        //        {
        //            MaSanPham = g.Key.maSanPham,
        //            TenSanPham = g.Key.tenSanPham,
        //            SoLuong = g.Sum(x => x.soLuong),
        //            MaChiNhanh = g.Key.maChiNhanh,
        //            TenChiNhanh = g.Key.TenChiNhanh
        //        })
        //        .AsQueryable();

        //    return danhSach;
        //}



    }
}

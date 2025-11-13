using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_HoaDon
    {
        private DatabaseAccess da = new DatabaseAccess();
        public IQueryable GetListHD()
        {
            return from hd in da.Db.HoaDons
                   join kh in da.Db.KhachHangs
                        on hd.maKhachHang equals kh.maKhachHang
                   join nv in da.Db.NhanViens
                        on hd.maNhanVien equals nv.maNhanVien into leftJoinNV
                   from nv in leftJoinNV.DefaultIfEmpty()   // LEFT JOIN NHÂN VIÊN
                   select new
                   {
                       MaHD = hd.maHD,
                       hd.ngayLapHD,
                       hd.gioLapHD,
                       hd.tongTien,
                       hd.thanhTien,
                       hd.phuongThucThanhToan,
                       hd.maKhachHang,
                       hd.maNhanVien,
                       TenKhachHang = kh.tenKhachHang,
                       TenNhanVien = nv != null ? nv.tenNhanVien : ""
                   };
        }


        public IQueryable GetHoaDonByMa(string maHD)
        {
            return (from hd in da.Db.HoaDons
                    join kh in da.Db.KhachHangs on hd.maKhachHang equals kh.maKhachHang
                    join nv in da.Db.NhanViens on hd.maNhanVien equals nv.maNhanVien
                    where hd.maHD == maHD
                    select new
                    {
                        MaHD = hd.maHD,
                        NgayLapHD = hd.ngayLapHD,
                        GioLapHD = hd.gioLapHD,
                        TongTien = hd.tongTien,
                        ThanhTien = hd.thanhTien,
                        PhuongThucThanhToan = hd.phuongThucThanhToan,
                        MaKhachHang = hd.maKhachHang,
                        MaNhanVien = hd.maNhanVien,
                        TenKhachHang = kh.tenKhachHang,
                        TenNhanVien = nv.tenNhanVien
                    });
        }

        public bool DeleteHD(string maHD)
        {
            try
            {
                // Xóa chi tiết hóa đơn trước
                var chiTietList = da.Db.ChiTietHoaDons.Where(ct => ct.maHoaDon == maHD);
                da.Db.ChiTietHoaDons.DeleteAllOnSubmit(chiTietList);
                da.Db.SubmitChanges();

                // Xóa hóa đơn
                var hoaDon = da.Db.HoaDons.FirstOrDefault(hd => hd.maHD == maHD);
                if (hoaDon != null)
                {
                    da.Db.HoaDons.DeleteOnSubmit(hoaDon);
                    da.Db.SubmitChanges();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateHD(DTO_HoaDon hoaDon)
        {
            try
            {
                if (hoaDon != null)
                {
                    HoaDon hd_update = da.Db.HoaDons.SingleOrDefault(hd => hd.maHD == hoaDon.MaHoaDon);

                    if (hd_update != null)
                    {
                        hd_update.phuongThucThanhToan = hoaDon.PhuongThucThanhToan;
                        hd_update.maKhachHang = hoaDon.MaKhachHang;
                        hd_update.maNhanVien = hoaDon.MaNhanVien;

                        da.Db.SubmitChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public void CapNhatTongTien(string maHD, double tongTienMoi)
        {
            try
            {
                var hd_update = da.Db.HoaDons.FirstOrDefault(hd => hd.maHD == maHD);
                if (hd_update != null)
                {
                    hd_update.tongTien = tongTienMoi;
                    da.Db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public bool UpdateThanhTienHD(string maHD, double thanhTienMoi)
        {
            try
            {
                HoaDon hd_update = da.Db.HoaDons.SingleOrDefault(hd => hd.maHD == maHD);
                if (hd_update != null)
                {
                    hd_update.thanhTien = thanhTienMoi;
                    da.Db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                // Xử lý lỗi nếu cần
                return false;
            }
            return false;
        }
        public IQueryable TimKiemHD(string tuKhoa)
        {
            tuKhoa = tuKhoa.ToLower();

            var danhSach = da.Db.HoaDons
                .Where(hd =>
                    hd.maHD.ToLower().Contains(tuKhoa) ||
                    hd.KhachHang.tenKhachHang.ToLower().Contains(tuKhoa)
                )
                .Select(hd => new
                {
                    hd.maHD,
                    hd.ngayLapHD,
                    hd.gioLapHD,
                    hd.tongTien,
                    hd.thanhTien,
                    hd.phuongThucThanhToan,
                    hd.maKhachHang,
                    hd.maNhanVien,
                    TenKhachHang = hd.KhachHang.tenKhachHang,
                    TenNhanVien = hd.NhanVien.tenNhanVien
                })
                .AsQueryable();

            return danhSach;
        }
        public string GetMaxIdHD()
        {
            var query = da.Db.HoaDons.OrderByDescending(hd => hd.maHD).FirstOrDefault();
            return query.maHD;
        }

        public string TimMaHoaDon(string maHd)
        {
            var query = (from hd in da.Db.HoaDons
                         where hd.maHD == maHd
                         select new
                         {
                             hd.maHD,

                         }).FirstOrDefault();
            string maHoaDon = "";
            if (query == null)
            {
                maHoaDon = "";
            }
            else
            {
                maHoaDon = query.maHD;
            }


            return maHoaDon;
        }
        public void AddHD2(DTO_HoaDon hoaDon)
        {
            try
            {
                // Kiểm tra hóa đơn đã tồn tại chưa
                var query = (from hd in da.Db.HoaDons
                             where hd.maHD == hoaDon.MaHoaDon
                             select hd).FirstOrDefault();

                if (query == null)
                {
                    // Lấy hóa đơn có id lớn nhất
                    var existingMaHD = da.Db.HoaDons.Select(cl => cl.maHD).ToList();
                    int nextNumber = 1;
                    string newMa;

                    while (true)
                    {
                        newMa = nextNumber < 10 ? $"HD00{nextNumber}" :
                                    nextNumber < 100 ? $"HD0{nextNumber}" : $"HD{nextNumber}";
                        if (!existingMaHD.Contains(newMa))
                            break;
                        nextNumber++;
                    }
                    HoaDon hd = new HoaDon
                    {
                        maHD = newMa.Trim(),
                        ngayLapHD = DateTime.Now,
                        gioLapHD = DateTime.Now,
                        phuongThucThanhToan = hoaDon.PhuongThucThanhToan,
                        tongTien = 0,
                        thanhTien = 0,
                        maKhachHang = hoaDon.MaKhachHang,
                        maNhanVien = hoaDon.MaNhanVien
                    };
                    // Thêm hóa đơn mới
                    da.Db.HoaDons.InsertOnSubmit(hd);
                    da.Db.SubmitChanges();
                }
            }
            catch
            {
                // Tùy bạn xử lý logging nội bộ nếu cần
                throw;
            }
        }
        public void UpdateTotalCash2(string maHd)
        {
            // Lấy hóa đơn
            var hd_update = da.Db.HoaDons.SingleOrDefault(hd => hd.maHD == maHd);
            if (hd_update == null) return;

            // Lấy chi tiết hóa đơn
            var listCTHD = da.Db.ChiTietHoaDons.Where(ct => ct.maHoaDon == maHd).ToList();

            double totalThanhTien = 0;

            foreach (var item in listCTHD)
            {
                // Lấy sản phẩm
                var sp = da.Db.SanPhams.SingleOrDefault(s => s.maSanPham == item.maSanPham);
                if (sp == null) continue;

                double gia = (double)sp.donGia;

                // Áp dụng khuyến mãi nếu có
                if (!string.IsNullOrEmpty(sp.maKhuyenMai))
                {
                    var km = da.Db.KhuyenMais.SingleOrDefault(k => k.MaKhuyenMai == sp.maKhuyenMai);
                    if (km != null && km.GiaTri.HasValue)
                    {
                        gia *= (1 - km.GiaTri.Value / 100.0);
                    }
                }

                totalThanhTien += (double)gia * (int)item.soLuong;
            }

            // Cập nhật hóa đơn
            hd_update.tongTien = totalThanhTien;
            hd_update.thanhTien = totalThanhTien;

            da.Db.SubmitChanges();
        }

    }
}

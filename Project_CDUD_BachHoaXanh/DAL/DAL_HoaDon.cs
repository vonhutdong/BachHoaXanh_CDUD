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
        public string AddHD2(DTO_HoaDon hoaDon)
        {
            try
            {
                if (hoaDon == null)
                    throw new Exception("Dữ liệu hóa đơn không hợp lệ!");

                // Kiểm tra trùng mã (nếu có)
                var query = (from h in da.Db.HoaDons where h.maHD == hoaDon.MaHoaDon select h).FirstOrDefault();

                if (query != null)
                {
                    throw new Exception("Hóa đơn đã tồn tại!");
                }

                // 🔹 Sinh mã hóa đơn mới
                var existingMaHD = da.Db.HoaDons.Select(h => h.maHD).ToList();
                int nextNumber = 1;
                string newMaHD;

                while (true)
                {
                    newMaHD = nextNumber < 10 ? $"HD00{nextNumber}" :
                              nextNumber < 100 ? $"HD0{nextNumber}" : $"HD{nextNumber}";
                    if (!existingMaHD.Contains(newMaHD))
                        break;
                    nextNumber++;
                }

                // 🔹 Tạo hóa đơn mới
                HoaDon hd = new HoaDon
                {
                    maHD = newMaHD.Trim(),
                    ngayLapHD = DateTime.Now,
                    gioLapHD = DateTime.Now,
                    phuongThucThanhToan = hoaDon.PhuongThucThanhToan,
                    tongTien = 0,      
                    thanhTien = 0,     // ⚡ bạn có thể chỉnh nếu khác nhau
                    maKhachHang = hoaDon.MaKhachHang,
                    maNhanVien = hoaDon.MaNhanVien
                };

                // 🔹 Thêm vào DB
                da.Db.HoaDons.InsertOnSubmit(hd);
                da.Db.SubmitChanges(); // ⚠️ Bắt buộc để dữ liệu được lưu

                return newMaHD; // ✅ Trả mã hóa đơn mới để liên kết chi tiết
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm hóa đơn: " + ex.Message);
            }
        }

        public bool UpdateTotalCash2(string maHd, string maKhachHang)
        {
            using (var db = new QLBHXDataContext()) // Tạo DataContext mới
            {
                var hd_update = db.HoaDons.FirstOrDefault(hd => hd.maHD == maHd);
                if (hd_update == null) return false;

                float tongTruocGiam = 0;
                float tongSauGiamSP = 0;

                var listCTHD = db.ChiTietHoaDons.Where(ct => ct.maHoaDon == maHd).ToList();

                foreach (var item in listCTHD)
                {
                    var sp = db.SanPhams.FirstOrDefault(s => s.maSanPham == item.maSanPham);
                    if (sp == null) continue;

                    float giaGoc = (float)sp.donGia;
                    float giaSauKM = giaGoc;

                    if (!string.IsNullOrEmpty(sp.maKhuyenMai))
                    {
                        var km = db.KhuyenMais.FirstOrDefault(k => k.MaKhuyenMai == sp.maKhuyenMai);
                        if (km != null && km.GiaTri.HasValue)
                        {
                            giaSauKM = giaGoc * (1 - (float)km.GiaTri.Value / 100f);
                        }
                    }

                    tongTruocGiam += (float)(giaGoc * item.soLuong);
                    tongSauGiamSP += (float)(giaSauKM * item.soLuong);
                }

                float giamPhanTram = 0f;

                if (!string.IsNullOrEmpty(maKhachHang))
                {
                    var kh = db.KhachHangs.FirstOrDefault(k => k.maKhachHang == maKhachHang);
                    if (kh != null && !string.IsNullOrEmpty(kh.capBac))
                    {
                        switch (kh.capBac.Trim())
                        {
                            case "Bạc": giamPhanTram = 0.02f; break;
                            case "Vàng": giamPhanTram = 0.05f; break;
                            case "Kim cương": giamPhanTram = 0.10f; break;
                        }
                    }
                }

                float giamTheoCapBac = tongSauGiamSP * giamPhanTram;
                float tongSauGiam = tongSauGiamSP - giamTheoCapBac;

                hd_update.thanhTien = tongTruocGiam;
                hd_update.tongTien = tongSauGiam;

                db.SubmitChanges();
                return true;
            }
        }



        public HoaDon LayHoaDonTheoMaHD(string maHD)
        {
            return da.Db.HoaDons.FirstOrDefault(x => x.maHD == maHD);
        }
        public DTO_HoaDon GetHDByMaHD(string ma)
        {
            using (var db  = new QLBHXDataContext())
            {
                if (string.IsNullOrEmpty(ma))
                    return null;

                try
                {
                    // Lấy hóa đơn từ DB
                    var hd = db.HoaDons.ToList()
               .FirstOrDefault(h => h.maHD.Trim().ToUpper() == ma.Trim().ToUpper());



                    if (hd == null) return null;

                    // Trả về DTO_HoaDon hoặc đối tượng tương tự
                    return new DTO_HoaDon
                    {
                        MaHoaDon = hd.maHD,
                        MaKhachHang = hd.maKhachHang,
                        MaNhanVien = hd.maNhanVien,
                        PhuongThucThanhToan = hd.phuongThucThanhToan,
                        ThanhTien = (float)hd.thanhTien,
                        TongTien = (float)hd.tongTien,
                        NgayLapHD = (DateTime)hd.ngayLapHD,
                        GioLapHD = (DateTime)hd.gioLapHD
                    };
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Lỗi khi lấy hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }
        public string LayMaKHTheoMaHD(string maHD)
        {
            return da.Db.HoaDons
                .Where(hd => hd.maHD == maHD)
                .Select(hd => hd.maKhachHang)
                .FirstOrDefault();
        }



    }
}

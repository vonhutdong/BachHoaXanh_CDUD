using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_ChiTietHoaDon
    {
        DatabaseAccess da = new DatabaseAccess();

        public IQueryable GetListCTHD()
        {
            var query = from cthd in da.Db.ChiTietHoaDons
                        select new
                        {
                            cthd.id,
                            cthd.soLuong,
                            cthd.maHoaDon,
                            cthd.maSanPham
                        };

            return query;
        }
        public bool AddCTHD(DTO_ChiTietHoaDon chiTietHoaDon)
        {
            try
            {
                var query = (from cthd in da.Db.ChiTietHoaDons
                             where cthd.maHoaDon == chiTietHoaDon.MaHoaDon &&
                                   cthd.maSanPham == chiTietHoaDon.MaSanPham
                             select cthd).FirstOrDefault();

                if (query == null)
                {
                    da.Db.ChiTietHoaDons.InsertOnSubmit(new ChiTietHoaDon
                    {
                        soLuong = chiTietHoaDon.SoLuong,
                        maHoaDon = chiTietHoaDon.MaHoaDon,
                        maSanPham = chiTietHoaDon.MaSanPham
                    });

                    da.Db.SubmitChanges();
                    return true;
                }

                return false; // Đã tồn tại
            }
            catch
            {
                return false; // Gặp lỗi
            }
        }
        public int GetChiTietTheoMa(string maHD, string maSP)
        {
            var soluong = (from ct in da.Db.ChiTietHoaDons
                           where ct.maHoaDon == maHD && ct.maSanPham == maSP
                           select ct.soLuong)
                           .FirstOrDefault();

            return soluong ?? 0; // nếu null thì trả 0
        }


        public IQueryable GetListCTHDTheoMaHD(string MaHD)
        {
            var query = from cthd in da.Db.ChiTietHoaDons
                        join hd in da.Db.HoaDons
                        on cthd.maHoaDon equals hd.maHD
                        join sp in da.Db.SanPhams
                        on cthd.maSanPham equals sp.maSanPham
                        where cthd.maHoaDon == MaHD
                        select new
                        {
                            cthd.id,                         // ID chi tiết hóa đơn
                            hd.maHD,                    // Mã hóa đơn (ví dụ: HD001)
                            sp.tenSanPham,                  // Tên sản phẩm
                            cthd.soLuong,                   // Số lượng sản phẩm
                            sp.donGia,                      // Giá 1 sản phẩm
                            ThanhTien = cthd.soLuong * sp.donGia,  // Tính tổng tiền = số lượng × đơn giá
                            cthd.maHoaDon,                  // ID hóa đơn (khóa ngoại)
                            cthd.maSanPham,                 // ID sản phẩm (khóa ngoại)
                        };

            return query;
        }
        public IQueryable<dynamic> GetListCTHDTheoMaHD1(string MaHD)
        {
            var query = from cthd in da.Db.ChiTietHoaDons
                        join hd in da.Db.HoaDons on cthd.maHoaDon equals hd.maHD
                        join sp in da.Db.SanPhams on cthd.maSanPham equals sp.maSanPham
                        where cthd.maHoaDon == MaHD
                        select new
                        {
                            cthd.id,
                            hd.maHD,
                            sp.tenSanPham,
                            cthd.soLuong,
                            sp.donGia,
                            ThanhTien = cthd.soLuong * sp.donGia,
                            cthd.maHoaDon,
                            cthd.maSanPham
                        };

            return query;
        }
        public void AddCTHD2(DTO_ChiTietHoaDon chiTietHoaDon)
        {
            try
            {
                // Kiểm tra chi tiết hóa đơn đã tồn tại chưa
                var query = (from cthd in da.Db.ChiTietHoaDons
                             where cthd.maHoaDon == chiTietHoaDon.MaHoaDon &&
                                   cthd.maSanPham == chiTietHoaDon.MaSanPham
                             select cthd).FirstOrDefault();

                if (query == null)
                {
                    // Thêm bản ghi mới
                    da.Db.ChiTietHoaDons.InsertOnSubmit(new ChiTietHoaDon
                    {
                        soLuong = chiTietHoaDon.SoLuong,
                        maHoaDon = chiTietHoaDon.MaHoaDon,
                        maSanPham = chiTietHoaDon.MaSanPham
                    });

                    da.Db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_ThanhTien> GetListCTHDTheoMaHD2(string maHD)
        {
            var query = (from cthd in da.Db.ChiTietHoaDons
                         join hd in da.Db.HoaDons on cthd.maHoaDon equals hd.maHD
                         join sp in da.Db.SanPhams on cthd.maSanPham equals sp.maSanPham
                         where cthd.maHoaDon == maHD
                         select new
                         {
                             cthd.id,
                             hd.maHD,
                             sp.tenSanPham,
                             cthd.soLuong,
                             sp.donGia,
                             ThanhTien = cthd.soLuong * sp.donGia,
                             cthd.maHoaDon,
                             cthd.maSanPham,
                         }).ToList();

            var list = new List<DTO_ThanhTien>();

            foreach (var item in query)
            {
                DTO_ThanhTien tt = new DTO_ThanhTien();
                tt.ThanhTien = (int)item.ThanhTien;
                list.Add(tt);
            }

            return list;
        }

        // Tinh tong tien by idHd
        public int GetTotalCashByIdHd(string maHd)
        {
            // Initialize Variables
            var list = this.GetListCTHDTheoMaHD2(maHd);
            int totalCash = 0;

            foreach (var item in list)
            {
                totalCash += item.ThanhTien;
            }

            return totalCash;

        }
        public IQueryable LayDSSanPhamTheoMaSP(string maHD)
        {
            return  (from cthd in da.Db.ChiTietHoaDons
                        join sp in da.Db.SanPhams
                        on cthd.maSanPham equals sp.maSanPham
                        where cthd.maHoaDon == maHD
                        select new
                        {
                            sp.maSanPham,
                            sp.tenSanPham,
                            sp.donGia,
                        });

        }
        public IQueryable SreachChiTietHoaDonTheoMaSP(string maHD, string tukhoa)
        {
            return (from cthd in da.Db.ChiTietHoaDons
                    join hd in da.Db.HoaDons
                    on cthd.maHoaDon equals hd.maHD
                    join sp in da.Db.SanPhams
                    on cthd.maSanPham equals sp.maSanPham
                    where cthd.maHoaDon.ToLower().Contains(maHD.ToLower())
                               && sp.tenSanPham.ToLower().Contains(tukhoa.ToLower())
                    select new
                    {
                        cthd.id,                         // ID chi tiết hóa đơn
                        hd.maHD,                    // Mã hóa đơn (ví dụ: HD001)
                        sp.tenSanPham,                  // Tên sản phẩm
                        cthd.soLuong,                   // Số lượng sản phẩm
                        sp.donGia,                      // Giá 1 sản phẩm
                        ThanhTien = cthd.soLuong * sp.donGia,  // Tính tổng tiền = số lượng × đơn giá
                        cthd.maHoaDon,                  // ID hóa đơn (khóa ngoại)
                        cthd.maSanPham,                 // ID sản phẩm (khóa ngoại)
                    });
        }

        public IQueryable SreachChiTietHoaDonTheoDonGia(string maHD, double tukhoa)
        {
            return (from cthd in da.Db.ChiTietHoaDons
                    join hd in da.Db.HoaDons
                    on cthd.maHoaDon equals hd.maHD
                    join sp in da.Db.SanPhams
                    on cthd.maSanPham equals sp.maSanPham
                    where cthd.maHoaDon.ToLower().Contains(maHD.ToLower())
                             && sp.donGia.Equals(tukhoa)
                    select new
                    {
                        cthd.id,                         // ID chi tiết hóa đơn
                        hd.maHD,                    // Mã hóa đơn (ví dụ: HD001)
                        sp.tenSanPham,                  // Tên sản phẩm
                        cthd.soLuong,                   // Số lượng sản phẩm
                        sp.donGia,                      // Giá 1 sản phẩm
                        ThanhTien = cthd.soLuong * sp.donGia,  // Tính tổng tiền = số lượng × đơn giá
                        cthd.maHoaDon,                  // ID hóa đơn (khóa ngoại)
                        cthd.maSanPham,                 // ID sản phẩm (khóa ngoại)
                    });
        }
        public IQueryable SreachSanPhamTheoMaSP(string maHD, string tukhoa)
        {
            tukhoa = tukhoa?.ToLower().Trim() ?? ""; // tránh null và loại khoảng trắng

            var query = from cthd in da.Db.ChiTietHoaDons
                        join sp in da.Db.SanPhams
                            on cthd.maSanPham equals sp.maSanPham
                        where cthd.maHoaDon.ToLower().Contains(maHD.ToLower())
                                && sp.tenSanPham.ToLower().Contains(tukhoa.ToLower())
                        select new
                        {
                            sp.maSanPham,
                            sp.tenSanPham,
                            sp.donGia
                        };

            return query;
        }
        public IQueryable SreachSanPhamTheoDonGia(string maHD, double tukhoa)
        {
            var query = from cthd in da.Db.ChiTietHoaDons
                        join sp in da.Db.SanPhams
                            on cthd.maSanPham equals sp.maSanPham
                        where cthd.maHoaDon.ToLower().Contains(maHD.ToLower())
                              && sp.donGia.Equals(tukhoa)
                        select new
                        {
                            sp.maSanPham,
                            sp.tenSanPham,
                            sp.donGia
                        };

            return query;
        }
        public double LayDonGiaTheoMaSP(string maSP)
        {
            var query = (from sp in da.Db.SanPhams
                         where sp.maSanPham == maSP
                         select sp.donGia).FirstOrDefault();

            // Nếu null => trả 0
            return query ?? 0;
        }

        public bool UpdateCTHD(string maHD, string maSP, int soLuong)
        {
            try
            {
                var cthd = da.Db.ChiTietHoaDons.SingleOrDefault(x => x.maHoaDon == maHD && x.maSanPham == maSP);
                //var hd = da.Db.HoaDons.SingleOrDefault(x => x.maHD == maHD);
                if (cthd != null)
                {
                    cthd.soLuong = soLuong;
                }
                //if (hd != null)
                //{
                //    hd.thanhTien = thanhTien;
                //}
                da.Db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public decimal TinhTongTienTheoMaHD_TuSoLuongVaDonGia(string maHD)
        {
            try
            {
                // sp.donGia và c.soLuong có thể là non-nullable; cast về decimal? để Sum xử lý trường hợp rỗng
                var q = from c in da.Db.ChiTietHoaDons
                        join sp in da.Db.SanPhams on c.maSanPham equals sp.maSanPham
                        where c.maHoaDon == maHD
                        select (decimal?)(c.soLuong * sp.donGia); // nếu sp.donGia là decimal

                // Nếu sp.donGia là double trong DB, convert:
                // select (decimal?)(c.soLuong * (decimal)sp.donGia);

                decimal sum = q.Sum() ?? 0m;
                return sum;
            }
            catch
            {
                throw;
            }
        }
    }
}

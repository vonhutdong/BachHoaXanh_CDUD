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

    }
}

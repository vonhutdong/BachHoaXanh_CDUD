using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_BangLuong
    {
        private DatabaseAccess da = new DatabaseAccess();

        public IQueryable LayDSBangLuong()
        {
            try
            {
                return (from b in da.Db.BangLuongs
                        join nv in da.Db.NhanViens
                        on b.maNhanVien equals nv.maNhanVien
                        select new
                        {
                            b.MaBangLuong,
                            b.maNhanVien,
                            nv.tenNhanVien,
                            b.ThangNam,
                            b.TongGioCong,
                            b.Luong,
                        });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        public IQueryable LayDSBangLuong(string maNhanVien)
        {
            try
            {
                return (from b in da.Db.BangLuongs
                        join nv in da.Db.NhanViens
                        on b.maNhanVien equals nv.maNhanVien
                        where b.maNhanVien == maNhanVien
                        select new
                        {
                            b.MaBangLuong,
                            b.maNhanVien,
                            nv.tenNhanVien,
                            b.ThangNam,
                            b.TongGioCong,
                            b.Luong,
                        });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public IQueryable LayBangLuongTheoThang(string maNhanVien, int thang, int nam)
        {
            try
            {
                return (from b in da.Db.BangLuongs
                        join nv in da.Db.NhanViens
                        on b.maNhanVien equals nv.maNhanVien
                        where b.maNhanVien == maNhanVien
                              && b.ThangNam.Value.Month == thang
                              && b.ThangNam.Value.Year == nam
                        select new
                        {
                            b.MaBangLuong,
                            nv.tenNhanVien,
                            b.ThangNam,
                            b.TongGioCong,
                            b.Luong,
                        });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public BangLuong LayBangLuongTheoThang1(string maNhanVien, int thang, int nam)
        {
            try
            {
                return da.Db.BangLuongs
                         .FirstOrDefault(b => b.maNhanVien == maNhanVien
                                           && b.ThangNam.HasValue
                                           && b.ThangNam.Value.Month == thang
                                           && b.ThangNam.Value.Year == nam);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BangLuong LayHoacTaoBangLuong(string maNV, int thang, int nam)
        {
            try
            {
                // 1️⃣ Thử lấy bảng lương tháng hiện tại
                var bangLuong = da.Db.BangLuongs
                    .FirstOrDefault(b => b.maNhanVien == maNV
                                      && b.ThangNam.HasValue
                                      && b.ThangNam.Value.Month == thang
                                      && b.ThangNam.Value.Year == nam);

                // 2️⃣ Nếu chưa có, tạo mới
                if (bangLuong == null)
                {
                    // Sinh mã bảng lương mới
                    var existingMaBL = da.Db.BangLuongs.Select(b => b.MaBangLuong).ToList();
                    int nextNumber = 1;
                    string newMaBL;
                    while (true)
                    {
                        newMaBL = nextNumber < 10 ? $"BL00{nextNumber}" :
                                   nextNumber < 100 ? $"BL0{nextNumber}" : $"BL{nextNumber}";
                        if (!existingMaBL.Contains(newMaBL))
                            break;
                        nextNumber++;
                    }

                    bangLuong = new BangLuong
                    {
                        MaBangLuong = newMaBL,
                        maNhanVien = maNV,
                        ThangNam = new DateTime(nam, thang, 1),
                        TongGioCong = 0,
                        Luong = 0
                    };

                    da.Db.BangLuongs.InsertOnSubmit(bangLuong);
                    da.Db.SubmitChanges();
                }

                return bangLuong;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy hoặc tạo bảng lương: " + ex.Message);
            }
        }

        public void ThemBangLuong(DTO_BangLuong bangluong)
        {
            try
            {
                // Lấy bản ghi có id lớn nhất
                var query2 = da.Db.BangLuongs.OrderByDescending(x => x.maNhanVien).FirstOrDefault();

                // Kiểm tra xem đã có bảng lương nào cho nhân viên đó trong tháng/năm đó chưa
                var data = da.Db.BangLuongs.SingleOrDefault(cn =>
                    cn.ThangNam.Value.Month == bangluong.ThangNam.Month &&
                    cn.ThangNam.Value.Year == bangluong.ThangNam.Year &&
                    cn.maNhanVien == bangluong.MaNhanVien);

                // --- Sinh ma tự động ---
                var existingMaBL = da.Db.BangLuongs
                    .Select(bl => bl.MaBangLuong)
                    .ToList();

                if (data == null)
                {
                    int nextNumber = 1;
                    string newMaBL;
                    while (true)
                    {
                        newMaBL = nextNumber < 10 ? $"BL00{nextNumber}" :
                                     nextNumber < 100 ? $"BL0{nextNumber}" : $"BL{nextNumber}";
                        if (!existingMaBL.Contains(newMaBL))
                            break;
                        nextNumber++;
                    }

                    // --- Tạo ca làm mới ---
                    BangLuong newBL = new BangLuong
                    {
                        MaBangLuong = newMaBL,
                        ThangNam = bangluong.ThangNam,
                        Luong = bangluong.Luong,
                        TongGioCong = bangluong.TongGioCong,
                        maNhanVien = bangluong.MaNhanVien,
                    };

                    da.Db.BangLuongs.InsertOnSubmit(newBL);
                    da.Db.SubmitChanges();
                }
                else
                {
                    throw new Exception("Đã tồn tại bảng lương cho nhân viên này trong tháng/năm này.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra: " + ex.Message);
            }
        }
        public IQueryable DSNhanVien()
        {
            return from nv in da.Db.NhanViens
                   select nv;
        }
    }
}

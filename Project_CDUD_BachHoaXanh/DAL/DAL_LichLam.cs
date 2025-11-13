using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_LichLam
    {
        private DatabaseAccess da = new DatabaseAccess();

        public DatabaseAccess Da { get => da; set => da = value; }

        // Methods
        // LayDSLichLam()
        public IQueryable LayDSLichLam()
        {
            try
            {
                return (from b in da.Db.LichLams
                        join nv in da.Db.NhanViens
                        on b.maNhanVien equals nv.maNhanVien
                        join cl in da.Db.CaLams
                        on b.maCaLam equals cl.maCaLam
                        select new
                        {
                            MaLichLam = b.maLichLam,
                            NgayLam = b.ngayLam,
                            MaNhanVien = nv.maNhanVien,
                            TenNhanVien = nv.tenNhanVien,
                            TenCaLam = cl.tenCaLam,
                            Ten = b.maCaLam,
                        });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ThemLichLam(DTO_LichLam lichLam)
        {

            // 🔍 Kiểm tra trùng lịch theo ngày + mã ca + mã NV
            bool isDuplicate = da.Db.LichLams.Any(ll =>
                ll.maNhanVien == lichLam.MaNhanVien &&
                ll.maCaLam == lichLam.MaCaLam &&
                ll.ngayLam.Value.Year == lichLam.NgayLam.Year &&
                ll.ngayLam.Value.Month == lichLam.NgayLam.Month &&
                ll.ngayLam.Value.Day == lichLam.NgayLam.Day);

            if (!isDuplicate)
            {
                // 🔢 Sinh mã lịch làm mới
                var existingMaCLs = da.Db.LichLams.Select(cl => cl.maLichLam).ToList();
                int nextNumber = 1;
                string newMa;

                while (true)
                {
                    newMa = nextNumber < 10 ? $"LL00{nextNumber}" :
                                nextNumber < 100 ? $"LL0{nextNumber}" : $"LL{nextNumber}";
                    if (!existingMaCLs.Contains(newMa))
                        break;
                    nextNumber++;
                }

                LichLam newCL = new LichLam
                {
                    maLichLam = newMa.Trim(),
                    ngayLam = lichLam.NgayLam.Date, // đảm bảo chỉ lưu ngày
                    maNhanVien = lichLam.MaNhanVien.Trim(),
                    maCaLam = lichLam.MaCaLam.Trim(),
                };
                da.Db.LichLams.InsertOnSubmit(newCL);

                da.Db.SubmitChanges();
                lichLam.MaLichLam = newCL.maLichLam;
                return true;
            }
            return false;
            //throw new Exception("Nhân viên này đã có lịch làm trong ngày và ca đó!");

        }


        public bool XoaLichLam(string maLL)
        {
            try
            {
                var data = da.Db.LichLams.FirstOrDefault(dt => dt.maLichLam == maLL);
                if (data == null)
                    return false;

                da.Db.LichLams.DeleteOnSubmit(data);
                da.Db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Thao tác thất bại: " + ex.Message, ex);
            }
        }
        public bool KiemTraTrungLich(DTO_LichLam ll)
        {
            // 🔹 Kiểm tra trùng lịch mới (ngày + ca) với các lịch khác của nhân viên
            bool isDuplicate = da.Db.LichLams.Any(llDb =>
                llDb.maNhanVien == ll.MaNhanVien &&
                llDb.maCaLam == ll.MaCaLam &&
                llDb.ngayLam.Value.Date == ll.NgayLam.Date &&
                llDb.maLichLam != ll.MaLichLam);  // loại bỏ lịch hiện tại đang sửa
            return isDuplicate;
        }
        public bool SuaLichLam(DTO_LichLam lichLam)
        {
            try
            {
                if (lichLam == null)
                    return false;

                var ll = da.Db.LichLams.FirstOrDefault(dt => dt.maLichLam == lichLam.MaLichLam);
                if (ll != null)
                {
                    // Cập nhật các cột
                    ll.ngayLam = lichLam.NgayLam;
                    ll.maNhanVien = lichLam.MaNhanVien;
                    ll.maCaLam = lichLam.MaCaLam;

                    da.Db.SubmitChanges();
                    return true;
                }

                return false; // Không tìm thấy lịch
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật lịch làm: " + ex.Message, ex);
            }
        }


        public IQueryable LayDSNV()
        {
            IQueryable temp = from ll in da.Db.NhanViens
                              select new
                              {
                                  ll.maNhanVien,
                                  ll.tenNhanVien,
                              };
            return temp;
        }

        public IQueryable timkiemTheoTen(string ten)
        {
            return from b in da.Db.LichLams
                   join nv in da.Db.NhanViens
                   on b.maNhanVien equals nv.maNhanVien
                   join cl in da.Db.CaLams
                   on b.maCaLam equals cl.maCaLam
                   where nv.tenNhanVien.Contains(ten) // Điều kiện tìm kiếm theo tên
                   select new
                   {
                       MaLichLam = b.maLichLam,
                       NgayLam = b.ngayLam,
                       TenNhanVien = nv.tenNhanVien,
                       TenCaLam = cl.tenCaLam,
                       ten = b.maCaLam,
                   }; // Trả về đối tượng LichLam
        }

        public IQueryable timkiemTheoNgay(DateTime ngay)
        {
            return from b in da.Db.LichLams
                   join nv in da.Db.NhanViens
                   on b.maNhanVien equals nv.maNhanVien
                   join cl in da.Db.CaLams
                   on b.maCaLam equals cl.maCaLam
                   where b.ngayLam.Value.Date == ngay.Date
                   select new
                   {
                       MaLichLam = b.maLichLam,
                       NgayLam = b.ngayLam,
                       TenNhanVien = nv.tenNhanVien,
                       TenCaLam = cl.tenCaLam,
                       ten = b.maCaLam,
                   };
        }

        public bool DellLL(string MaLL)
        {
            try
            {
                // 1️⃣ Lấy ChiTietBangLuong liên quan
                var ctbls = da.Db.ChiTietBangLuongs.Where(ct => ct.maLichLam == MaLL).ToList();

                // 2️⃣ Lấy maBangLuong của các chi tiết này (nếu có)
                string maBangLuong = ctbls.FirstOrDefault()?.maBangLuong;

                // 3️⃣ Xoá chi tiết
                if (ctbls.Count > 0)
                {
                    da.Db.ChiTietBangLuongs.DeleteAllOnSubmit(ctbls);
                }

                // 4️⃣ Xoá LichLam
                var lichLam = da.Db.LichLams.SingleOrDefault(lnv => lnv.maLichLam == MaLL);
                if (lichLam != null)
                {
                    da.Db.LichLams.DeleteOnSubmit(lichLam);
                }

                // 5️⃣ Lưu thay đổi
                da.Db.SubmitChanges();

                // 6️⃣ Cập nhật lại tổng giờ công và lương nếu có bảng lương
                if (!string.IsNullOrEmpty(maBangLuong))
                {
                    var bangLuong = da.Db.BangLuongs.SingleOrDefault(bl => bl.MaBangLuong == maBangLuong);
                    if (bangLuong != null)
                    {
                        // Tổng giờ công mới = tổng SoGioCongThucTe còn lại
                        double tongGioCong = da.Db.ChiTietBangLuongs
                            .Where(ct => ct.maBangLuong == maBangLuong)
                            .Sum(ct => (double?)ct.SoGioCongThucTe) ?? 0;

                        bangLuong.TongGioCong = tongGioCong;
                        bangLuong.Luong = tongGioCong * 50000; // hoặc theo lương NV
                        da.Db.SubmitChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa lịch làm: {ex.Message}");
                return false;
            }
        }


        public LichLam GetLichLamByMa(string maLichLam)
        {
            return da.Db.LichLams
                .FirstOrDefault(ll => ll.maLichLam == maLichLam);
        }

    }
}

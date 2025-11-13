using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class DAL_ChiTietBangLuong
    {
        
        private DatabaseAccess da = new DatabaseAccess();

        public IQueryable LayDSBangLuong()
        {
            try
            {
                return (from ct in da.Db.ChiTietBangLuongs
                        select new { ct.MaChiTietBangLuong, ct.maBangLuong, ct.NgayLam, ct.SoGioCongThucTe });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IQueryable LayDSChiTietBangLuong(string maBangLuong)
        {
            try
            {
                return (from ct in da.Db.ChiTietBangLuongs
                            //on ct.idNhanVien equals nv.id
                        where ct.maBangLuong == maBangLuong 
                        select new { ct.MaChiTietBangLuong, ct.maBangLuong, ct.NgayLam, ct.SoGioCongThucTe });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IQueryable LayDSLichLam()
        {
            return from ll in da.Db.LichLams select ll;
        }

        public void ThemChiTietBangLuong(DTO_ChiTietBangLuong ctbl)
        {
            try
            {
                var query2 = da.Db.ChiTietBangLuongs.OrderByDescending(x => x.MaChiTietBangLuong).FirstOrDefault();

                // Lấy bảng lương cần kiểm tra
                var bangluong = da.Db.BangLuongs.FirstOrDefault(bl => bl.MaBangLuong == ctbl.MaBangLuong);

                if (bangluong == null)
                    throw new Exception("Không tìm thấy bảng lương tương ứng.");

                if (bangluong.ThangNam.HasValue &&
                    bangluong.ThangNam.Value.Month == ctbl.NgayLam.Month &&
                    bangluong.ThangNam.Value.Year == ctbl.NgayLam.Year)
                {

                    // Kiểm tra trùng ngày làm
                    bool trungNgayLam = da.Db.ChiTietBangLuongs.Any(ct =>
                        ct.maBangLuong == ctbl.MaBangLuong &&
                        ct.NgayLam.Value.Date == ctbl.NgayLam.Date &&
                        ct.maLichLam == ctbl.MaLichLam);

                    if (trungNgayLam)
                    {
                        throw new Exception("Đã tồn tại chi tiết bảng lương cho ngày này.");
                    }

                    // Cho phép thêm vì cùng tháng
                    // --- Sinh ma tự động ---
                    var existingMaBL = da.Db.ChiTietBangLuongs
                        .Select(bl => bl.MaChiTietBangLuong)
                        .ToList();

                        int nextNumber = 1;
                        string newMaCTBL;
                        while (true)
                        {
                            newMaCTBL = nextNumber < 10 ? $"CTBL00{nextNumber}" :
                                         nextNumber < 100 ? $"CTBL0{nextNumber}" : $"CTBL{nextNumber}";
                            if (!existingMaBL.Contains(newMaCTBL))
                                break;
                            nextNumber++;
                        }

                        // --- Tạo ca làm mới ---
                        ChiTietBangLuong newCTBL = new ChiTietBangLuong
                        {
                            MaChiTietBangLuong = newMaCTBL,
                            maBangLuong = ctbl.MaBangLuong,
                            maLichLam = ctbl.MaLichLam,
                            SoGioCongThucTe = ctbl.SoGioCongThucTe,
                            NgayLam = ctbl. NgayLam,
                        };

                        da.Db.ChiTietBangLuongs.InsertOnSubmit(newCTBL);
                        da.Db.SubmitChanges();
                        //
                        

                    // Cập nhật bảng lương
                    var tongGioCong = da.Db.ChiTietBangLuongs
                        .Where(ct => ct.maBangLuong == ctbl.MaBangLuong)
                        .Sum(ct => ct.SoGioCongThucTe);

                    bangluong.TongGioCong = tongGioCong;
                    bangluong.Luong = tongGioCong * 50000;
                    da.Db.SubmitChanges();
                }
                else
                {
                    throw new Exception("Chỉ được thêm chi tiết bảng lương trong cùng tháng/năm với bảng lương.");
                }



            }
            catch (Exception ex)
            {
                return; 
            }

        }
        private string TaoMaBangLuong()
        {
            var list = da.Db.BangLuongs.Select(x => x.MaBangLuong).ToList();
            int next = 1;

            while (true)
            {
                string ma = next < 10 ? $"BL00{next}" :
                           next < 100 ? $"BL0{next}" : $"BL{next}";

                if (!list.Contains(ma))
                    return ma;

                next++;
            }
        }

        private string TaoMaChiTietBangLuong()
        {
            var list = da.Db.ChiTietBangLuongs.Select(x => x.MaChiTietBangLuong).ToList();
            int next = 1;

            while (true)
            {
                string ma = next < 10 ? $"CTBL00{next}" :
                           next < 100 ? $"CTBL0{next}" : $"CTBL{next}";

                if (!list.Contains(ma))
                    return ma;

                next++;
            }
        }
        public int UpdateChiTietBangLuong_Only(string maLichLam, string maNV, DateTime ngayLam, double gioLam)
        {
            // 1️⃣ Lấy chi tiết bảng lương gắn với lịch làm
            var ctbl = da.Db.ChiTietBangLuongs
                .FirstOrDefault(ct => ct.maLichLam == maLichLam);

            if (ctbl == null)
            {
                // Không có chi tiết → KHÔNG ADD → trả về null hoặc throw
                return 0;
                // hoặc: throw new Exception("Chi tiết bảng lương không tồn tại!");
            }

            // 2️⃣ Lấy bảng lương tương ứng của tháng/năm
            var bangLuong = da.Db.BangLuongs
                .FirstOrDefault(bl =>
                    bl.MaBangLuong == ctbl.maBangLuong &&
                    bl.maNhanVien == maNV &&
                    bl.ThangNam.Value.Month == ngayLam.Month &&
                    bl.ThangNam.Value.Year == ngayLam.Year
                );

            if (bangLuong == null)
            {
                // Không có bảng lương → KHÔNG ADD → trả về null hoặc throw
                return 1;
                // hoặc: throw new Exception("Bảng lương không tồn tại!");
            }

            // 3️⃣ Cập nhật chi tiết bảng lương
            ctbl.SoGioCongThucTe = gioLam;
            ctbl.NgayLam = ngayLam;

            da.Db.SubmitChanges();

            // 4️⃣ Tính lại tổng giờ công + lương
            double tongGioCong = (double)da.Db.ChiTietBangLuongs
                .Where(ct => ct.maBangLuong == bangLuong.MaBangLuong)
                .Sum(ct => ct.SoGioCongThucTe);

            bangLuong.TongGioCong = tongGioCong;
            bangLuong.Luong = tongGioCong * 50000; // hoặc mức lương NV

            da.Db.SubmitChanges();

            return 2;
        }




    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_ChucVu
    {
        private DatabaseAccess da = new DatabaseAccess();

        public IQueryable LayDSChucVu()
        {
            IQueryable iquery = from cv in da.Db.ChucVus
                                select new
                                {
                                    cv.maChucVu,
                                    cv.tenChucVu
                                };
            return iquery;
        }

        public IQueryable GetListAllLNVByTen()
        {
            IQueryable iquery = from cv in da.Db.ChucVus
                                select new
                                {
                                    MaChucVu = cv.maChucVu,
                                    TenChucVu = cv.tenChucVu
                                };
            return iquery;
        }

        public IQueryable GetListOneLNVByTen(string maChucVu)
        {
            IQueryable query = from cv in da.Db.ChucVus
                               where cv.maChucVu == maChucVu
                               select new
                               {
                                   TenChucVu = cv.tenChucVu
                               };
            return query;
        }

        public bool AddChucVu(DTO_ChucVu chucVu)
        {

            var query = (from cv in da.Db.ChucVus
                         where cv.maChucVu == chucVu.MaChucvu
                         select cv).FirstOrDefault();

            if (query == null)
            {
                var existingMaCV = da.Db.ChucVus
                .Select(cv => cv.maChucVu)
                .ToList();

                int nextNumber = 1;
                string newMa;
                while (true)
                {
                    newMa = nextNumber < 10 ? $"CV00{nextNumber}" :
                                 nextNumber < 100 ? $"CV0{nextNumber}" : $"CV{nextNumber}";
                    if (!existingMaCV.Contains(newMa))
                        break;
                    nextNumber++;
                }

                // --- Tạo ca làm mới ---
                ChucVu newCv = new ChucVu
                {
                    maChucVu = newMa,
                    tenChucVu = chucVu.TenChucVu.Trim(),
                };

                da.Db.ChucVus.InsertOnSubmit(newCv);
                da.Db.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateChucVu(DTO_ChucVu chucVu) 
        {
            try
            {
                // Initialize Variables
                string nameCV = string.Empty;

                // Checked loainhanvien != null
                if (chucVu != null)
                {
                    // Init LoaiNhanVien
                    ChucVu cV_update = da.Db.ChucVus.Single(cv => cv.maChucVu == chucVu.MaChucvu);

                    // Updated lnv_update
                    cV_update.tenChucVu = chucVu.TenChucVu;

                    // Saved db
                    da.Db.SubmitChanges();
                    nameCV = chucVu.TenChucVu;
                    return true;

                }
            }
            catch (Exception ex)
            {

                return false;
            }
            return false;
        }

        public bool DelChucVu(string maCV)
        {
            try
            {
                // Initialize Variables
                string nameLNV = string.Empty;

                // Checked id lnv saved in db lnv?
                var query = (from lnv in da.Db.ChucVus
                             where lnv.maChucVu == maCV
                             select lnv).Count();

                if (query == 1)
                {
                    // Init LoaiNhanVien
                    ChucVu lnv_update = da.Db.ChucVus.Single(lnv => lnv.maChucVu == maCV);

                    da.Db.ChucVus.DeleteOnSubmit(lnv_update);
                    // Saved db
                    da.Db.SubmitChanges();
                    //nameLNV = lnv_update.TenLoaiNhanVien;
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public bool AddChucVu2(DTO_ChucVu chucVu)
        {

            string ten = chucVu.TenChucVu.Trim();
            bool tenTrung = da.Db.ChucVus
            .Any(cv => cv.tenChucVu.ToLower().Trim() == ten.ToLower());

            if (tenTrung)
            {
                throw new Exception("Tên chức vụ đã tồn tại, vui lòng nhập lại");
            }
            try
            {
                
                var query = (from cv in da.Db.ChucVus
                             where cv.maChucVu == chucVu.MaChucvu
                             select cv).FirstOrDefault();

                if (query == null)
                {
                    var existingMaCV = da.Db.ChucVus
                .Select(cv => cv.maChucVu)
                .ToList();

                    int nextNumber = 1;
                    string newMa;
                    while (true)
                    {
                        newMa = nextNumber < 10 ? $"CV00{nextNumber}" :
                                     nextNumber < 100 ? $"CV0{nextNumber}" : $"CV{nextNumber}";
                        if (!existingMaCV.Contains(newMa))
                            break;
                        nextNumber++;
                    }

                    // --- Tạo ca làm mới ---
                    ChucVu newCv = new ChucVu
                    {
                        maChucVu = newMa,
                        tenChucVu = chucVu.TenChucVu.Trim(),
                    };
                    da.Db.ChucVus.InsertOnSubmit(newCv);
                    da.Db.SubmitChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public bool UpdateChucVu2(DTO_ChucVu chucVu)
        {
            if (chucVu == null || string.IsNullOrWhiteSpace(chucVu.TenChucVu))
                return false;

            try
            {
                string tenMoi = chucVu.TenChucVu.Trim();

                // --- Kiểm tra trùng tên (với các chức vụ KHÁC) ---
                bool tenTrung = da.Db.ChucVus
                    .Any(cv => cv.tenChucVu.ToLower().Trim() == tenMoi.ToLower()
                               && cv.maChucVu != chucVu.MaChucvu);

                if (tenTrung)
                {
                    // Tên đã tồn tại cho chức vụ khác
                    return false;
                }

                // --- Tìm chức vụ cần sửa ---
                var lnv = da.Db.ChucVus.SingleOrDefault(x => x.maChucVu == chucVu.MaChucvu);

                if (lnv != null)
                {
                    lnv.tenChucVu = tenMoi;
                    da.Db.SubmitChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật chức vụ: " + ex.Message);
                return false;
            }
        }

        public string GetMaxMaChucVu()
        {
            var query = da.Db.ChucVus.OrderByDescending(lnv => lnv.maChucVu).FirstOrDefault();

            return query.maChucVu;
        }
    }
}

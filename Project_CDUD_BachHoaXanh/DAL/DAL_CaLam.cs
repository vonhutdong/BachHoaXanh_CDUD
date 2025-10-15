using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_CaLam
    {
        private DatabaseAccess da = new DatabaseAccess();
        public DatabaseAccess Da { get => da; set => da = value; }

        public IQueryable LayDSCaLam()
        {
            IQueryable temp = from cl in Da.Db.CaLams
                              select new
                              {
                                  cl.maCaLam,
                                  cl.tenCaLam,
                                  cl.gioBatDau,
                                  cl.gioKetThuc
                              };
            return temp;                  
        }

        public void ThemCaLam(DTO_CaLam caLam)
        {
            // --- Kiểm tra trùng mã ---
            var existed = da.Db.CaLams.FirstOrDefault(cl => cl.maCaLam == caLam.MaCaLam);
            if (existed != null)
                throw new Exception("Ca làm đã tồn tại!");

            // --- Kiểm tra trùng tên ---
            var existedName = da.Db.CaLams
                .FirstOrDefault(cl => cl.tenCaLam.Trim().ToLower() == caLam.TenCaLam.Trim().ToLower());
            if (existedName != null)
                throw new Exception("Tên ca làm đã tồn tại, vui lòng nhập tên khác!");

            // --- Kiểm tra giờ hợp lệ ---
            if (!TimeSpan.TryParseExact(caLam.GioBatDau, "hh\\:mm", null, out _))
                throw new Exception("Giờ bắt đầu không hợp lệ! Định dạng phải là HH:mm (ví dụ 07:00, 23:30)");

            if (!TimeSpan.TryParseExact(caLam.GioKetThuc, "hh\\:mm", null, out _))
                throw new Exception("Giờ kết thúc không hợp lệ! Định dạng phải là HH:mm (ví dụ 12:00, 06:00)");

            // --- Sinh mã ca làm tự động ---
            var existingMaCLs = da.Db.CaLams
                .Select(cl => cl.maCaLam)
                .ToList();

            int nextNumber = 1;
            string newMaCaLam;
            while (true)
            {
                newMaCaLam = nextNumber < 10 ? $"CL00{nextNumber}" :
                             nextNumber < 100 ? $"CL0{nextNumber}" : $"CL{nextNumber}";
                if (!existingMaCLs.Contains(newMaCaLam))
                    break;
                nextNumber++;
            }

            // --- Tạo ca làm mới ---
            CaLam newCL = new CaLam
            {
                maCaLam = newMaCaLam,
                tenCaLam = caLam.TenCaLam.Trim(),
                gioBatDau = caLam.GioBatDau.Trim(),
                gioKetThuc = caLam.GioKetThuc.Trim()
            };

            da.Db.CaLams.InsertOnSubmit(newCL);
            da.Db.SubmitChanges();
        }



        public bool XoaCaLam(string maCaLam)
        {
            try
            {
                var data = da.Db.CaLams.FirstOrDefault(dt => dt.maCaLam == maCaLam);
                if (data == null) { 
                    return false;
                }
                da.Db.CaLams.DeleteOnSubmit(data);
                da.Db.SubmitChanges();

                //thanh cong
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SuaCaLam(DTO_CaLam caLam)
        {
            try
            {
                // --- Kiểm tra giờ hợp lệ ---
                if (!TimeSpan.TryParseExact(caLam.GioBatDau, "hh\\:mm", null, out _) ||
                    !TimeSpan.TryParseExact(caLam.GioKetThuc, "hh\\:mm", null, out _))
                {
                    throw new Exception("Giờ không hợp lệ! Định dạng phải là HH:mm (ví dụ 07:00)");
                }

                // --- Tìm ca làm cần sửa ---
                var cl = da.Db.CaLams.FirstOrDefault(dt => dt.maCaLam == caLam.MaCaLam);
                if (cl == null)
                {
                    throw new Exception("Không tìm thấy ca làm cần sửa!");
                }

                // --- Kiểm tra trùng tên (trừ chính nó) ---
                var existedName = da.Db.CaLams
                    .FirstOrDefault(c => c.tenCaLam.Trim().ToLower() == caLam.TenCaLam.Trim().ToLower()
                                      && c.maCaLam != caLam.MaCaLam);
                if (existedName != null)
                {
                    throw new Exception("Tên ca làm đã tồn tại, vui lòng nhập tên khác!");
                }

                // --- Cập nhật dữ liệu ---
                cl.tenCaLam = caLam.TenCaLam.Trim();
                cl.gioBatDau = caLam.GioBatDau.Trim();
                cl.gioKetThuc = caLam.GioKetThuc.Trim();

                da.Db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public string TinhGioLam(CaLam calam)
        {
            if (calam != null)
            {
                if (TimeSpan.TryParse(calam.gioBatDau, out TimeSpan start) &&
                    TimeSpan.TryParse(calam.gioKetThuc, out TimeSpan end))
                {
                    // Nếu giờ kết thúc nhỏ hơn giờ bắt đầu, cộng thêm 1 ngày
                    if (end < start)
                    {
                        end = end.Add(TimeSpan.FromDays(1));
                    }

                    TimeSpan timeSpan = end - start;
                    return timeSpan.TotalHours.ToString("0.##"); // làm tròn 2 chữ số
                }
                else
                {
                    return "Định dạng giờ bắt đầu/kết thúc không hợp lệ!";
                }
            }
            else
            {
                return "Ca làm không tồn tại!";
            }
        }


        public CaLam GetCaLamByMaCaLam(string maCaLam)
        {
            try
            {
                var caLam = da.Db.CaLams.SingleOrDefault(cl => cl.maCaLam == maCaLam);
                return caLam;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Lỗi khi lấy ca làm: {ex.Message}");
                return null;
            }
        }
    }
}

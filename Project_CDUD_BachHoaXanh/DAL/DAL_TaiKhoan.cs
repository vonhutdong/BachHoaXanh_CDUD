using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_TaiKhoan
    {
        private DatabaseAccess da = new DatabaseAccess();
        public DatabaseAccess Da { get => da; set => da = value; }

        
        

        public bool CheckTaiKhoan(string taiKhoan, string matKhau)
        {
            return da.Db.TaiKhoans.Any(tk => tk.TenTaiKhoan == taiKhoan && tk.MatKhau == matKhau);
        }

        // Lấy mã tài khoản
        public string GetMaTaiKhoan(string taiKhoan, string matKhau)
        {
            var tk = da.Db.TaiKhoans.FirstOrDefault(t => t.TenTaiKhoan == taiKhoan && t.MatKhau == matKhau);
            if (tk == null)
                throw new Exception("Không tìm thấy tài khoản hợp lệ!");

            return tk.MaTaiKhoan;
        }

        // Lấy danh sách tất cả tài khoản
        public IQueryable GetListTK()
        {
            return from tk in da.Db.TaiKhoans
                   select new
                   {
                       tk.MaTaiKhoan,
                       tk.TenTaiKhoan,
                       tk.MatKhau,
                       tk.Quyen
                   };
        }

        // Lấy danh sách quyền
        public IQueryable GetListTKByQuyen()
        {
            return da.Db.TaiKhoans
                   .GroupBy(tk => tk.Quyen)
                   .Select(q => new { Quyen = q.Key });
        }

        // Lấy tất cả tài khoản theo tên
        public IQueryable GetListAllTKByTenTK()
        {
            return da.Db.TaiKhoans
                   .Select(tk => new { tk.MaTaiKhoan, tk.TenTaiKhoan });
        }

        // Lấy 1 tài khoản theo ID
         public IQueryable GetOneTKByMaTK(string maTK)
    {
        return da.Db.TaiKhoans
               .Where(tk => tk.MaTaiKhoan == maTK)
               .Select(tk => new { tk.MaTaiKhoan, tk.TenTaiKhoan, tk.MatKhau, tk.Quyen });
    }
        //lay danh sach tk theo id
        public IQueryable GetAllListTKByTenTK(string maTaiKhoan)
        {
            return da.Db.TaiKhoans
                       .Where(tk => tk.MaTaiKhoan == maTaiKhoan)
                       .Select(tk => new { tk.MaTaiKhoan, tk.TenTaiKhoan });
        }
        // Lấy quyền theo tài khoản
        public int GetRole(string taiKhoan, string matKhau)
        {
            var result = da.Db.TaiKhoans.FirstOrDefault(tk => tk.TenTaiKhoan == taiKhoan && tk.MatKhau == matKhau);
            return result?.Quyen ?? -1;
        }

        // Thêm tài khoản có mã tự sinh (MaTaiKhoan không bị nhảy số)
        public void AddTaiKhoan(DTO_TaiKhoan taiKhoan)
        {
            // 1️⃣ Kiểm tra dữ liệu đầu vào
            string ten = taiKhoan.TenTaiKhoan?.Trim();
            string matKhau = taiKhoan.MatKhau?.Trim();

            if (string.IsNullOrWhiteSpace(ten))
                throw new Exception("Tên tài khoản không được để trống.");

            if (ten.Length > 30)
                throw new Exception("Tên tài khoản không được vượt quá 30 ký tự.");

            // 2️⃣ Kiểm tra định dạng tên tài khoản bằng Regex
            if (!System.Text.RegularExpressions.Regex.IsMatch(ten, @"^[A-Za-zÀ-ỹ0-9]+(?:\s[A-Za-zÀ-ỹ0-9]+)*$"))
                throw new Exception("Tên tài khoản không hợp lệ!!!");
            if (System.Text.RegularExpressions.Regex.IsMatch(matKhau, @"\s{2,}"))
                throw new Exception("Mật khẩu không hợp lệ!!!");
            if (string.IsNullOrWhiteSpace(matKhau))
                throw new Exception("Mật khẩu không được để trống.");

            if (matKhau.Length < 4)
                throw new Exception("Mật khẩu phải có ít nhất 4 ký tự.");

            // 3️⃣ Kiểm tra trùng tên tài khoản
            var existedTenTK = da.Db.TaiKhoans.FirstOrDefault(tk => tk.TenTaiKhoan == ten);
            if (existedTenTK != null)
                throw new Exception("Tên tài khoản đã tồn tại. Vui lòng chọn tên khác.");

            // 4️⃣ Lấy danh sách mã tài khoản hiện có
            var existingMaTK = da.Db.TaiKhoans
                                    .Select(t => t.MaTaiKhoan)
                                    .ToList();

            // 5️⃣ Sinh mã tài khoản mới
            int nextNumber = 1;
            string maTK;
            while (true)
            {
                maTK = nextNumber < 10 ? $"TK00{nextNumber}" :
                       nextNumber < 100 ? $"TK0{nextNumber}" : $"TK{nextNumber}";

                if (!existingMaTK.Contains(maTK))
                    break;

                nextNumber++;
            }

            // 6️⃣ Thêm tài khoản mới
            TaiKhoan newTK = new TaiKhoan
            {
                MaTaiKhoan = maTK,
                TenTaiKhoan = ten,
                MatKhau = matKhau,
                Quyen = taiKhoan.Quyen
            };

            da.Db.TaiKhoans.InsertOnSubmit(newTK);
            da.Db.SubmitChanges();
        }


        //Cập nhật tài khoản
        public void UpdateTK(DTO_TaiKhoan taiKhoan)
        {
            // 1️⃣ Kiểm tra dữ liệu đầu vào
            string ten = taiKhoan.TenTaiKhoan?.Trim();
            string matKhau = taiKhoan.MatKhau?.Trim();

            if (string.IsNullOrWhiteSpace(ten))
                throw new Exception("Tên tài khoản không được để trống.");

            if (ten.Length > 30)
                throw new Exception("Tên tài khoản không được vượt quá 30 ký tự.");

            // 2️⃣ Kiểm tra định dạng tên tài khoản bằng Regex
            if (!System.Text.RegularExpressions.Regex.IsMatch(ten, @"^[A-Za-zÀ-ỹ0-9]+(?:\s[A-Za-zÀ-ỹ0-9]+)*$"))
                throw new Exception("Tên tài khoản không hợp lệ!!!");
            if (System.Text.RegularExpressions.Regex.IsMatch(matKhau, @"\s{2,}"))
                throw new Exception("Mật khẩu không hợp lệ!!!");
            if (string.IsNullOrWhiteSpace(matKhau))
                throw new Exception("Mật khẩu không được để trống.");

            if (matKhau.Length < 4)
                throw new Exception("Mật khẩu phải có ít nhất 4 ký tự.");

            var tk = da.Db.TaiKhoans.FirstOrDefault(t => t.MaTaiKhoan == taiKhoan.MaTaiKhoan);
            if (tk == null)
                throw new Exception("Tài khoản không tồn tại.");
            var existed = da.Db.TaiKhoans.FirstOrDefault(t => t.TenTaiKhoan == ten && t.MaTaiKhoan != taiKhoan.MaTaiKhoan);
            if (existed != null)
                throw new Exception("Tên tài khoản đã tồn tại.");

            // Cập nhật dữ liệu
            tk.TenTaiKhoan = ten;
            tk.MatKhau = matKhau;
            tk.Quyen = taiKhoan.Quyen;

            da.Db.SubmitChanges();
        }

        // Xóa tài khoản
        public void DelTK(string maTaiKhoan)
        {
            if (string.IsNullOrWhiteSpace(maTaiKhoan))
                throw new Exception("Mã tài khoản không hợp lệ.");

            // Tìm tài khoản theo mã
            var tk = da.Db.TaiKhoans.FirstOrDefault(t => t.MaTaiKhoan == maTaiKhoan);
            if (tk == null)
                throw new Exception("Không tìm thấy tài khoản cần xóa.");

            // Xác nhận xóa
            da.Db.TaiKhoans.DeleteOnSubmit(tk);
            da.Db.SubmitChanges();
        }

        // Lấy ID lớn nhất
        public string GetMaxMaTK()
        {
            var last = da.Db.TaiKhoans
                .OrderByDescending(t => t.MaTaiKhoan)
                .Select(t => t.MaTaiKhoan)
                .FirstOrDefault();

            return last ?? "TK000";
        }
    }
}

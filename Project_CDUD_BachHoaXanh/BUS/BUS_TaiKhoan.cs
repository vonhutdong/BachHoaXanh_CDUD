using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_TaiKhoan
    {
        private DAL_TaiKhoan dal_tk = new DAL_TaiKhoan();

        // Kiểm tra đăng nhập
        public bool CheckTaiKhoan(string taiKhoan, string matKhau)
        {
            return dal_tk.CheckTaiKhoan(taiKhoan, matKhau);
        }

        // Lấy mã tài khoản theo tên và mật khẩu
        public string GetMaTaiKhoan(string taiKhoan, string matKhau)
        {
            return dal_tk.GetMaTaiKhoan(taiKhoan, matKhau);
        }

        // Lấy quyền người dùng
        public int GetRole(string taiKhoan, string matKhau)
        {
            return dal_tk.GetRole(taiKhoan, matKhau);
        }

        // Lấy toàn bộ tài khoản
        public IQueryable GetListTaiKhoan()
        {
            return dal_tk.GetListTK();
        }

        // Lấy danh sách quyền
        public IQueryable GetListTaiKhoanTheoQuyen()
        {
            return dal_tk.GetListTKByQuyen();
        }

        // Lấy tất cả tài khoản theo tên
        public IQueryable GetAllTaiKhoanByTen()
        {
            return dal_tk.GetListAllTKByTenTK();
        }

        // Lấy 1 tài khoản theo ID
        public IQueryable GetOneTaiKhoanByMa(string maTaiKhoan)
        {
            return dal_tk.GetOneTKByMaTK(maTaiKhoan);
        }

        // Thêm tài khoản
        public void AddTaiKhoan(DTO_TaiKhoan taiKhoan)
        {
            dal_tk.AddTaiKhoan(taiKhoan);
        }

        //Cập nhật tài khoản
        public void UpdateTaiKhoan(DTO_TaiKhoan taiKhoan)
        {
            dal_tk.UpdateTK(taiKhoan);
        }

        // Xóa tài khoản
        public void DeleteTaiKhoan(string maTK)
        {
            dal_tk.DelTK(maTK);
        }

        // Lấy ID lớn nhất
        public string GetMaxMaTaiKhoan()
        {
            return dal_tk.GetMaxMaTK();
        }
    }
}

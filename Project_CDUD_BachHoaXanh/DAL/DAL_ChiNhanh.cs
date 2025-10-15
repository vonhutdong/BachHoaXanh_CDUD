using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_ChiNhanh
    {
        private DatabaseAccess da = new DatabaseAccess();
        public DatabaseAccess Da { get => da; set => da = value; }

        public IQueryable LayDSChiNhanh()
        {
            IQueryable temp = from cn in Da.Db.ChiNhanhs
                              select new
                              {
                                  cn.MaChiNhanh,
                                  cn.TenChiNhanh,
                                  cn.DiaChi,
                                  cn.SoDienThoai
                              };
            return temp;
        }
        public void ThemChiNhanh(DTO_ChiNhanh chiNhanh)
        {
            try
            {
                //  Kiểm tra null
                if (chiNhanh == null)
                    throw new Exception("Dữ liệu chi nhánh không hợp lệ!");

                //  Làm sạch chuỗi nhập vào
                string ma = LamSachChuoi(chiNhanh.MaChiNhanh);
                string ten = LamSachChuoi(chiNhanh.TenChiNhanh);
                string diaChi = LamSachChuoi(chiNhanh.DiaChi);
                string sdt = chiNhanh.SoDienThoai.Trim();

                //  Kiểm tra viết in hoa
                if (chiNhanh.MaChiNhanh != chiNhanh.MaChiNhanh.ToUpper())
                    throw new Exception("Mã chi nhánh phải được viết IN HOA (ví dụ: CN001)!");

                //  Kiểm tra rỗng
                if (string.IsNullOrWhiteSpace(ma))
                    throw new Exception("Mã chi nhánh không được để trống!");
                if (string.IsNullOrWhiteSpace(ten))
                    throw new Exception("Tên chi nhánh không được để trống!");
                if (string.IsNullOrWhiteSpace(diaChi))
                    throw new Exception("Địa chỉ không được để trống!");
                if (string.IsNullOrWhiteSpace(sdt))
                    throw new Exception("Số điện thoại không được để trống!");
                if (!sdt.All(char.IsDigit))
                    throw new Exception("Số điện thoại chỉ được chứa ký tự số!");
                //  Kiểm tra độ dài
                if (ma.Length > 10)
                    throw new Exception("Mã chi nhánh không được vượt quá 10 ký tự!");
                if (ten.Length > 20)
                    throw new Exception("Tên chi nhánh không được vượt quá 20 ký tự!");
                if (diaChi.Length > 100)
                    throw new Exception("Địa chỉ không được vượt quá 100 ký tự!");
                if (sdt.Length > 10)
                    throw new Exception("Số điện thoại không được quá 10 số");
                if (sdt.Length < 10 )
                    throw new Exception("Số điện thoại phải có 10 số");

                //  Kiểm tra số điện thoại chỉ chứa số
               

                if (!System.Text.RegularExpressions.Regex.IsMatch(ma, @"^[A-Z0-9_]+$"))
                    throw new Exception("Mã không hợp lệ !!!");
                if (!System.Text.RegularExpressions.Regex.IsMatch(ten, @"^[A-Za-zÀ-ỹ0-9]+(?:\s[A-Za-zÀ-ỹ0-9]+)*$"))
                    throw new Exception("Tên không hợp lệ !!!");
                if (!System.Text.RegularExpressions.Regex.IsMatch(diaChi, @"^[A-Za-zÀ-ỹ0-9/,]+(?:\s[A-Za-zÀ-ỹ0-9/,]+)*$"))
                    throw new Exception("Địa chỉ không hợp lệ !!!");
                if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^(0|\+84)[0-9]{9,10}$"))
                    throw new Exception("Số điện thoại không hợp lệ! (VD: 0949452421 hoặc +84949452421)");

                //  Kiểm tra trùng trong DB
                if (da.Db.ChiNhanhs.Any(cn => cn.MaChiNhanh == ma))
                    throw new Exception("Mã chi nhánh đã tồn tại!");
                if (da.Db.ChiNhanhs.Any(cn => cn.DiaChi == diaChi))
                    throw new Exception("Địa chỉ chi nhánh đã tồn tại!");
                if (da.Db.ChiNhanhs.Any(cn => cn.SoDienThoai == sdt))
                    throw new Exception("Số điện thoại đã được sử dụng!");

                // 🔹 Tạo đối tượng mới
                ChiNhanh cnMoi = new ChiNhanh
                {
                    MaChiNhanh = ma,
                    TenChiNhanh = ten,
                    DiaChi = diaChi,
                    SoDienThoai = sdt
                };

                // 🔹 Thêm và lưu
                da.Db.ChiNhanhs.InsertOnSubmit(cnMoi);
                da.Db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi thêm chi nhánh: " + ex.Message);
            }
        }

        private string LamSachChuoi(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Xóa khoảng trắng đầu/cuối, và gộp nhiều khoảng trắng liên tiếp thành 1
            string cleaned = System.Text.RegularExpressions.Regex.Replace(input.Trim(), @"\s+", " ");
            return cleaned;
        }

        public void XoaChiNhanh(string maChiNhanh)
        {
            try
            {
                var data = da.Db.ChiNhanhs.FirstOrDefault(dt => dt.MaChiNhanh == maChiNhanh);
                if (data == null)
                    throw new Exception("Không tìm thấy chi nhánh cần xóa!");

                da.Db.ChiNhanhs.DeleteOnSubmit(data);
                da.Db.SubmitChanges();
                da = new DatabaseAccess(); // làm mới context
            }

            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi xóa chi nhánh: " + ex.Message);
            }
        }
        public void suaChiNhanh(DTO_ChiNhanh chiNhanh)
        {
            try
            {
                if (chiNhanh == null)
                    throw new Exception("Dữ liệu chi nhánh không hợp lệ!");

                // Làm sạch chuỗi
                string ma = LamSachChuoi(chiNhanh.MaChiNhanh);
                string ten = LamSachChuoi(chiNhanh.TenChiNhanh);
                string diaChi = LamSachChuoi(chiNhanh.DiaChi);
                string sdt = chiNhanh.SoDienThoai.Trim();

                // Kiểm tra viết in hoa
                if (ma != ma.ToUpper())
                    throw new Exception("Mã chi nhánh phải được viết IN HOA (ví dụ: CN001)!");

                // Kiểm tra rỗng
                if (string.IsNullOrWhiteSpace(ma))
                    throw new Exception("Mã chi nhánh không được để trống!");
                if (string.IsNullOrWhiteSpace(ten))
                    throw new Exception("Tên chi nhánh không được để trống!");
                if (string.IsNullOrWhiteSpace(diaChi))
                    throw new Exception("Địa chỉ không được để trống!");
                if (string.IsNullOrWhiteSpace(sdt))
                    throw new Exception("Số điện thoại không được để trống!");
                // Kiểm tra chỉ chứa số
                if (!sdt.All(char.IsDigit))
                    throw new Exception("Số điện thoại chỉ được chứa ký tự số!");
                // Kiểm tra độ dài
                if (ma.Length > 15)
                    throw new Exception("Mã chi nhánh không được vượt quá 15 ký tự!");
                if (ten.Length > 50)
                    throw new Exception("Tên chi nhánh không quá 50 ký tự!");
                if (diaChi.Length > 100)
                    throw new Exception("Địa chỉ không quá 100 ký tự!");
                if (sdt.Length > 10)
                    throw new Exception("Số điện thoại không được quá 10 số");
                if (sdt.Length < 10)
                    throw new Exception("Số điện thoại phải có 10 số");

               

                if (!System.Text.RegularExpressions.Regex.IsMatch(ma, @"^[A-Z0-9_]+$"))
                    throw new Exception("Mã chi nhánh không hợp lệ! (Chỉ gồm chữ in hoa, số và dấu gạch dưới)");

                if (!System.Text.RegularExpressions.Regex.IsMatch(ten, @"^[A-Za-zÀ-ỹ0-9/,]+(?:\s[A-Za-zÀ-ỹ0-9/,]+)*$"))
                    throw new Exception("Tên chi nhánh không hợp lệ! (Không chứa ký tự đặc biệt hoặc khoảng trắng thừa)");

                if (!System.Text.RegularExpressions.Regex.IsMatch(diaChi, @"^[A-Za-zÀ-ỹ0-9/,]+(?:\s[A-Za-zÀ-ỹ0-9/,]+)*$"))
                    throw new Exception("Địa chỉ không hợp lệ! (Không chứa ký tự đặc biệt hoặc khoảng trắng thừa)");

                if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^(0|\+84)[0-9]{9,10}$"))
                    throw new Exception("Số điện thoại không hợp lệ! (VD: 0949452421 hoặc +84949452421)");

                // Tìm chi nhánh cần sửa
                var data = da.Db.ChiNhanhs.FirstOrDefault(dt => dt.MaChiNhanh == ma);
                if (data == null)
                    throw new Exception("Không tìm thấy chi nhánh cần sửa!");

                // Kiểm tra trùng (ngoại trừ chính nó)
                if (da.Db.ChiNhanhs.Any(cn => cn.DiaChi == diaChi && cn.MaChiNhanh != ma))
                    throw new Exception("Địa chỉ chi nhánh đã tồn tại!");
                if (da.Db.ChiNhanhs.Any(cn => cn.SoDienThoai == sdt && cn.MaChiNhanh != ma))
                    throw new Exception("Số điện thoại đã được sử dụng!");

                // Cập nhật dữ liệu (đã làm sạch)
                data.TenChiNhanh = ten;
                data.DiaChi = diaChi;
                data.SoDienThoai = sdt;

                da.Db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi sửa chi nhánh: " + ex.Message);
            }
        }
    }
}

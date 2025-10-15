using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_LoaiHang
    {
        private DatabaseAccess da = new DatabaseAccess();
        public DatabaseAccess Da { get => da; set => da = value; }

        public IQueryable LayDSLH()
        {
            IQueryable query = from lh in da.Db.LoaiHangs
                               select new
                               {
                                   lh.maLoaiHang,
                                   lh.tenLoaiHang
                               };
            return query;
        }
        public void ThemLoaiHang(DTO_LoaiHang loaihangdto)
        {
            try
            {
                // Kiểm tra null
                if (loaihangdto == null)
                    throw new Exception("Dữ liệu loại hàng không hợp lệ!");

                // Làm sạch chuỗi nhập vào
                string ma = LamSachChuoi(loaihangdto.MaLoaiHang);
                string ten = LamSachChuoi(loaihangdto.TenLoaiHang);

                // Kiểm tra rỗng
                if (string.IsNullOrWhiteSpace(ma))
                    throw new Exception("Mã loại hàng không được để trống!");
                if (string.IsNullOrWhiteSpace(ten))
                    throw new Exception("Tên loại hàng không được để trống!");

                // 4️⃣ Kiểm tra viết IN HOA (chuẩn mã)
                if (ma != ma.ToUpper())
                    throw new Exception("Mã loại hàng phải được viết IN HOA (ví dụ: LH001)!");

                // Kiểm tra độ dài
                if (ma.Length > 10)
                    throw new Exception("Mã loại hàng không được vượt quá 10 ký tự!");
                if (ten.Length > 20)
                    throw new Exception("Tên loại hàng không được vượt quá 20 ký tự!");

                // Kiểm tra ký tự hợp lệ (chỉ cho phép chữ, số và dấu gạch dưới)
                if (!System.Text.RegularExpressions.Regex.IsMatch(ma, @"^[A-Z0-9_]+$"))
                    throw new Exception("Mã không hợp lệ !!!");
                if (!System.Text.RegularExpressions.Regex.IsMatch(ten, @"^[A-Za-zÀ-ỹ0-9]+(?:\s[A-Za-zÀ-ỹ0-9]+)*$"))
                    throw new Exception("Tên không hợp lệ !!!");

                // Kiểm tra trùng trong DB
                if (da.Db.LoaiHangs.Any(lh => lh.maLoaiHang == ma))
                    throw new Exception("Mã loại hàng đã tồn tại!");
                if (da.Db.LoaiHangs.Any(lh => lh.tenLoaiHang == ten))
                    throw new Exception("Tên loại hàng đã tồn tại!");

                // Tạo đối tượng mới
                LoaiHang lhMoi = new LoaiHang
                {
                    maLoaiHang = ma,
                    tenLoaiHang = ten
                };

                // Thêm và lưu
                da.Db.LoaiHangs.InsertOnSubmit(lhMoi);
                da.Db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi thêm loại hàng: " + ex.Message);
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
        public void XoaLoaiHang(string maLoaiHang)
        {
            try
            {
                var data = da.Db.LoaiHangs.FirstOrDefault(lh => lh.maLoaiHang == maLoaiHang);
                if (data == null)
                    throw new Exception("Không tìm thấy loại hàng cần xóa!");

                da.Db.LoaiHangs.DeleteOnSubmit(data);
                da.Db.SubmitChanges();
                da = new DatabaseAccess(); // làm mới context
            }

            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi xóa chi nhánh: " + ex.Message);
            }
        }
            public void SuaLoaiHang(DTO_LoaiHang loaihangdto)
            {
                try
                {
                    // Kiểm tra null
                    if (loaihangdto == null)
                        throw new Exception("Dữ liệu loại hàng không hợp lệ!");

                    // Làm sạch chuỗi nhập vào
                    string ma = LamSachChuoi(loaihangdto.MaLoaiHang);
                    string ten = LamSachChuoi(loaihangdto.TenLoaiHang);

                    // Kiểm tra rỗng
                    if (string.IsNullOrWhiteSpace(ma))
                        throw new Exception("Mã loại hàng không được để trống!");
                    if (string.IsNullOrWhiteSpace(ten))
                        throw new Exception("Tên loại hàng không được để trống!");

                    // 4️⃣ Kiểm tra viết IN HOA (chuẩn mã)
                    if (ma != ma.ToUpper())
                        throw new Exception("Mã loại hàng phải được viết IN HOA (ví dụ: LH001)!");

                    // Kiểm tra độ dài
                    if (ma.Length > 10)
                        throw new Exception("Mã loại hàng không được vượt quá 10 ký tự!");
                    if (ten.Length > 30)
                        throw new Exception("Tên loại hàng không được vượt quá 30 ký tự!");

                    // Kiểm tra ký tự hợp lệ (chỉ cho phép chữ, số và dấu gạch dưới)
                    if (!System.Text.RegularExpressions.Regex.IsMatch(ma, @"^[A-Z0-9_]+$"))
                        throw new Exception("Mã không hợp lệ !!!");
                    if (!System.Text.RegularExpressions.Regex.IsMatch(ten, @"^[A-Za-zÀ-ỹ0-9]+(?:\s[A-Za-zÀ-ỹ0-9]+)*$"))
                        throw new Exception("Tên không hợp lệ !!!");

                    var data = da.Db.LoaiHangs.FirstOrDefault(dt => dt.maLoaiHang == ma);
                    if (data == null)
                        throw new Exception("Không tìm thấy loại hàng cần sửa!");

                    // ⚠️ Kiểm tra trùng tên (ngoại trừ chính nó)
                    if (da.Db.LoaiHangs.Any(lh => lh.tenLoaiHang == ten && lh.maLoaiHang != ma))
                        throw new Exception("Tên loại hàng đã tồn tại!");

                    // 💾 Cập nhật dữ liệu đã được làm sạch
                    data.tenLoaiHang = ten;

                    da.Db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Có lỗi khi sửa loại hàng: " + ex.Message);
                }

        }
    }
}

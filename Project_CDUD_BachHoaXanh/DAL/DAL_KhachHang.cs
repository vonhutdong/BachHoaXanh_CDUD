using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_KhachHang
    {
        private DatabaseAccess da = new DatabaseAccess();

        public IQueryable<DTO_KhachHang> LayDSKH()
        {
            return from kh in da.Db.KhachHangs
                   select new DTO_KhachHang
                   {
                       MaKH = kh.maKhachHang,
                       TenKH = kh.tenKhachHang,
                       SoDienThoai = kh.soDienThoai,
                       DiaChi = kh.diaChi,
                       Diem = (float)(kh.diem ?? 0),
                       CapBac = kh.capBac
                   };
        }
        public DTO_KhachHang LayKhachHang_SDT(string soDienThoai)
        {
            try
            {
                // 1. Validate dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(soDienThoai))
                    return null;   // Không ném lỗi để tránh crash chương trình

                // 2. Truy vấn
                var kh = da.Db.KhachHangs
                    .FirstOrDefault(k => k.soDienThoai == soDienThoai);

                // 3. Không tìm thấy thì trả null
                if (kh == null)
                    return null;

                // 4. Map sang DTO
                return new DTO_KhachHang
                {
                    MaKH = kh.maKhachHang,
                    TenKH = kh.tenKhachHang,
                    SoDienThoai = kh.soDienThoai,
                    DiaChi = kh.diaChi,
                    Diem = (float)(kh.diem ?? 0),
                    CapBac = kh.capBac
                };
            }
            catch
            {
                // ✅ Không ném lỗi ra UI → tránh drop chương trình
                return null;
            }
        }

        public bool ThemKH(DTO_KhachHang khachHang)
        {
            try
            {
                if (khachHang == null)
                    throw new Exception("Dữ liệu khách hàng không hợp lệ!");

                // --- Làm sạch chuỗi ---
                string ten = khachHang.TenKH?.Trim() ?? "";
                string sdt = khachHang.SoDienThoai?.Trim() ?? "";
                string diaChi = khachHang.DiaChi?.Trim() ?? "";

                // --- Kiểm tra rỗng ---
                if (string.IsNullOrWhiteSpace(ten))
                    throw new Exception("Tên khách hàng không được để trống!");
                if (string.IsNullOrWhiteSpace(sdt))
                    throw new Exception("Số điện thoại không được để trống!");
                if (string.IsNullOrWhiteSpace(diaChi))
                    throw new Exception("Địa chỉ không được để trống!");

                // --- Kiểm tra số điện thoại ---
                if (!sdt.All(char.IsDigit))
                    throw new Exception("Số điện thoại chỉ được chứa ký tự số!");
                if (sdt.Length != 10)
                    throw new Exception("Số điện thoại phải có đúng 10 số!");
                if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^(0|\+84)[0-9]{9,10}$"))
                    throw new Exception("Số điện thoại không hợp lệ! (VD: 0949452421 hoặc +84949452421)");

                // --- Kiểm tra tên & địa chỉ ---
                if (ten.Length > 30)
                    throw new Exception("Tên khách hàng không được vượt quá 30 ký tự!");
                if (diaChi.Length > 50)
                    throw new Exception("Địa chỉ khách hàng không được vượt quá 50 ký tự!");

                if (!System.Text.RegularExpressions.Regex.IsMatch(ten, @"^[A-Za-zÀ-ỹ0-9]+(?:\s[A-Za-zÀ-ỹ0-9]+)*$"))
                    throw new Exception("Tên khách hàng không hợp lệ!");
                if (!System.Text.RegularExpressions.Regex.IsMatch(diaChi, @"^[A-Za-zÀ-ỹ0-9/,.]+(?:\s[A-Za-zÀ-ỹ0-9/,.]+)*$"))
                    throw new Exception("Địa chỉ khách hàng không hợp lệ!");

                // --- Kiểm tra trùng số điện thoại ---
                bool daTonTai = da.Db.KhachHangs.Any(kh => kh.soDienThoai == sdt);
                if (daTonTai)
                    throw new Exception("Số điện thoại này đã tồn tại!");

                // --- Kiểm tra điểm ---
                if (khachHang.Diem < 0)
                    throw new Exception("Điểm không được là số âm!");
                if (khachHang.Diem > 1000)
                    throw new Exception("Điểm không được vượt quá 1000!");
               
                var existingMaKHs = da.Db.KhachHangs
                    .Select(kh => kh.maKhachHang)
                    .ToList();

                int nextNumber = 1;
                string newMaKH;
                while (true)
                {
                    newMaKH = nextNumber < 10 ? $"KH00{nextNumber}" :
                              nextNumber < 100 ? $"KH0{nextNumber}" : $"KH{nextNumber}";
                    if (!existingMaKHs.Contains(newMaKH))
                        break;
                    nextNumber++;
                }

                // --- Tính cấp bậc dựa trên điểm ---
                float diem = khachHang.Diem;
                string capBac = "";
                if (diem > 200)
                    capBac = "Kim cương";
                else if (diem > 100)
                    capBac = "Vàng";
                else if (diem > 50)
                    capBac = "Bạc";
                else
                    capBac = "Đồng";

                // --- Tạo khách hàng mới ---
                KhachHang newKH = new KhachHang
                {
                    maKhachHang = newMaKH,
                    tenKhachHang = ten,
                    soDienThoai = sdt,
                    diaChi = diaChi,
                    diem = diem,
                    capBac = capBac
                };

                da.Db.KhachHangs.InsertOnSubmit(newKH);
                da.Db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm khách hàng thất bại: " + ex.Message);
            }
        }
        public bool SuaKH(DTO_KhachHang khachHang)
        {
            try
            {
                if (khachHang == null)
                    throw new Exception("Dữ liệu khách hàng không hợp lệ!");

                // --- Làm sạch chuỗi ---
                string maKH = khachHang.MaKH?.Trim() ?? "";
                string ten = khachHang.TenKH?.Trim() ?? "";
                string sdt = khachHang.SoDienThoai?.Trim() ?? "";
                string diaChi = khachHang.DiaChi?.Trim() ?? "";

                // --- Kiểm tra rỗng ---
                if (string.IsNullOrWhiteSpace(maKH))
                    throw new Exception("Mã khách hàng không được để trống!");
                if (string.IsNullOrWhiteSpace(ten))
                    throw new Exception("Tên khách hàng không được để trống!");
                if (string.IsNullOrWhiteSpace(sdt))
                    throw new Exception("Số điện thoại không được để trống!");
                if (string.IsNullOrWhiteSpace(diaChi))
                    throw new Exception("Địa chỉ không được để trống!");

                // --- Kiểm tra số điện thoại ---
                if (!sdt.All(char.IsDigit))
                    throw new Exception("Số điện thoại chỉ được chứa ký tự số!");
                if (sdt.Length != 10)
                    throw new Exception("Số điện thoại phải có đúng 10 số!");
                if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^(0|\+84)[0-9]{9,10}$"))
                    throw new Exception("Số điện thoại không hợp lệ! (VD: 0949452421 hoặc +84949452421)");

                // --- Kiểm tra tên & địa chỉ ---
                if (ten.Length > 30)
                    throw new Exception("Tên khách hàng không được vượt quá 30 ký tự!");
                if (diaChi.Length > 50)
                    throw new Exception("Địa chỉ khách hàng không được vượt quá 50 ký tự!");

                if (!System.Text.RegularExpressions.Regex.IsMatch(ten, @"^[A-Za-zÀ-ỹ0-9]+(?:\s[A-Za-zÀ-ỹ0-9]+)*$"))
                    throw new Exception("Tên khách hàng không hợp lệ!");
                if (!System.Text.RegularExpressions.Regex.IsMatch(diaChi, @"^[A-Za-zÀ-ỹ0-9/,.]+(?:\s[A-Za-zÀ-ỹ0-9/,.]+)*$"))
                    throw new Exception("Địa chỉ khách hàng không hợp lệ!");

                // --- Tìm khách hàng cần sửa ---
                var kh = da.Db.KhachHangs.SingleOrDefault(k => k.maKhachHang == maKH);
                if (kh == null)
                    throw new Exception("Không tìm thấy khách hàng cần sửa!");

                // --- Kiểm tra trùng số điện thoại (ngoại trừ chính khách hàng đang sửa) ---
                bool daTonTai = da.Db.KhachHangs.Any(k => k.soDienThoai == sdt && k.maKhachHang != maKH);
                if (daTonTai)
                    throw new Exception("Số điện thoại này đã được sử dụng bởi khách hàng khác!");

                // --- Kiểm tra điểm ---
                if (khachHang.Diem < 0)
                    throw new Exception("Điểm không được là số âm!");
                if (khachHang.Diem > 1000)
                    throw new Exception("Điểm không được vượt quá 1000!");

                // --- Tính cấp bậc dựa trên điểm ---
                float diem = khachHang.Diem;
                string capBac = "";
                if (diem > 200)
                    capBac = "Kim cương";
                else if (diem > 100)
                    capBac = "Vàng";
                else if (diem > 50)
                    capBac = "Bạc";
                else
                    capBac = "Đồng";

                var data = da.Db.KhachHangs.FirstOrDefault(kh1 => kh1.maKhachHang == maKH);
                if (data == null)
                    throw new Exception("Không tìm thấy khách hàng cần sửa!");

                // ⚠️ Kiểm tra trùng tên (ngoại trừ chính nó)
                if (da.Db.KhachHangs.Any(k => k.soDienThoai == sdt && k.maKhachHang != maKH))
                    throw new Exception("Số điện thoại này đã được sử dụng!");

                // --- Cập nhật thông tin ---
                kh.tenKhachHang = ten;
                kh.soDienThoai = sdt;
                kh.diaChi = diaChi;
                kh.diem = diem;
                kh.capBac = capBac;

                da.Db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Sửa khách hàng thất bại: " + ex.Message);
            }
        }
        public bool XoaKhachHang(string maKh)
        {
            try
            {
                // Tìm khách hàng theo mã
                var kh = da.Db.KhachHangs.SingleOrDefault(k => k.maKhachHang == maKh);
                if (kh == null)
                    throw new Exception("Không tìm thấy khách hàng cần xóa!");

                da.Db.KhachHangs.DeleteOnSubmit(kh);
                da.Db.SubmitChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                // Nếu bị lỗi ràng buộc khóa ngoại (FK)
                if (ex.Number == 547)
                    throw new Exception("Không thể xóa khách hàng này vì đang được sử dụng trong bảng khác!");
                else
                    throw new Exception("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi xóa khách hàng: " + ex.Message);
            }
        }

        private string RemoveDiacritics(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
        public IQueryable<DTO_KhachHang> TimKiemTheoTenHoacSDT(string tukhoa)
        {
            string keyword = RemoveDiacritics(tukhoa.ToLower());

            var danhSach = da.Db.KhachHangs
                .AsEnumerable() // ⬅️ Chuyển sang xử lý trong bộ nhớ
                .Where(kh =>
                    !string.IsNullOrEmpty(kh.tenKhachHang) &&
                    RemoveDiacritics(kh.tenKhachHang.ToLower()).Contains(keyword)
                    || (!string.IsNullOrEmpty(kh.soDienThoai) &&
                        kh.soDienThoai.Contains(tukhoa))
                    || (!string.IsNullOrEmpty(kh.diaChi) &&
                        RemoveDiacritics(kh.diaChi.ToLower()).Contains(keyword))
                )
                .Select(kh => new DTO_KhachHang
                {
                    MaKH = kh.maKhachHang,
                    TenKH = kh.tenKhachHang,
                    SoDienThoai = kh.soDienThoai,
                    DiaChi = kh.diaChi,
                    Diem = (float)(kh.diem ?? 0),
                    CapBac = kh.capBac
                })
                .AsQueryable();

            return danhSach;
        }





    }
}

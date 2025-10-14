using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_NhaCungCap
    {
        private DatabaseAccess da = new DatabaseAccess();
        public DatabaseAccess Da { get => da; set => da = value; }

        public IQueryable LayDSNCC()
        {
            IQueryable temp = from ncc in da.Db.NhaCungCaps
                              select new
                              {
                                  MaNCC = ncc.MaNhaCungCap,
                                  TenNCC = ncc.TenNhaCungCap,
                                  SDT = ncc.SoDienThoai,
                                  DiaChi = ncc.DiaChi
                              };
            return temp;
        }
        public void ThemNhaCungCap(DTO_NhaCungCap ncc)
        {
            try
            {
                if (ncc == null)
                    throw new Exception("Dữ liệu nhà cung cấp không hợp lệ!");

                // --- Làm sạch chuỗi ---
                string ten = ncc.TenNhaCungCap.Trim();
                string sdt = ncc.SoDienThoai.Trim();
                string diaChi = ncc.DiaChi.Trim();

                // --- Kiểm tra rỗng ---
                if (string.IsNullOrWhiteSpace(ten))
                    throw new Exception("Tên nhà cung cấp không được để trống!");
                if (string.IsNullOrWhiteSpace(sdt))
                    throw new Exception("Số điện thoại không được để trống!");
                if (string.IsNullOrWhiteSpace(diaChi))
                    throw new Exception("Địa chỉ không được để trống!");
                // Kiểm tra chỉ chứa số
                if (!sdt.All(char.IsDigit))
                    throw new Exception("Số điện thoại chỉ được chứa ký tự số!");
                // --- Kiểm tra tên hợp lệ ---
                if (ten.Length > 30)
                    throw new Exception("Tên nhà cung cấp không được vượt quá 30 ký tự!");
                if (diaChi.Length > 30)
                    throw new Exception("Địa chỉ nhà cung cấp không được vượt quá 30 ký tự!");
                if (sdt.Length > 10)
                    throw new Exception("Số điện thoại không được quá 10 số");
                if (sdt.Length < 10)
                    throw new Exception("Số điện thoại phải có 10 số");

               
                if (!System.Text.RegularExpressions.Regex.IsMatch(ten, @"^[A-Za-zÀ-ỹ0-9]+(?:\s[A-Za-zÀ-ỹ0-9]+)*$"))
                    throw new Exception("Tên nhà cung cấp không hợp lệ! Không được chứa ký tự đặc biệt hoặc nhiều khoảng trắng liên tiếp.");
                if (!System.Text.RegularExpressions.Regex.IsMatch(diaChi, @"^[A-Za-zÀ-ỹ0-9/,]+(?:\s[A-Za-zÀ-ỹ0-9/,]+)*$"))
                    throw new Exception("Địa chỉ không hợp lệ !!!");
                // --- Kiểm tra số điện thoại ---
                if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^(0|\+84)[0-9]{9,10}$"))
                    throw new Exception("Số điện thoại không hợp lệ! (VD: 0901234567 hoặc +84901234567)");

                // --- Kiểm tra trùng tên ---
                var existed = da.Db.NhaCungCaps.FirstOrDefault(x =>
                x.TenNhaCungCap == ten || x.SoDienThoai == sdt || x.DiaChi == diaChi);

                if (existed != null)
                {
                    if (existed.TenNhaCungCap == ten)
                        throw new Exception("Tên nhà cung cấp đã tồn tại!");
                    if (existed.SoDienThoai == sdt)
                        throw new Exception("Số điện thoại đã được sử dụng!");
                    if (existed.DiaChi == diaChi)
                        throw new Exception("Địa chỉ này đã được sử dụng!");
                }

                // --- Lấy danh sách mã NCC đã có ---
                var existingMaNCCs = da.Db.NhaCungCaps
                    .Select(n => n.MaNhaCungCap)
                    .ToList();

                // --- Tìm mã NCC nhỏ nhất chưa dùng ---
                int nextNumber = 1;
                string maNCC;
                while (true)
                {
                    maNCC = nextNumber < 10 ? $"NCC00{nextNumber}" :
                             nextNumber < 100 ? $"NCC0{nextNumber}" :
                                                $"NCC{nextNumber}";
                    if (!existingMaNCCs.Contains(maNCC))
                        break;
                    nextNumber++;
                }

                // --- Tạo và thêm mới ---
                NhaCungCap newNCC = new NhaCungCap
                {
                    MaNhaCungCap = maNCC,
                    TenNhaCungCap = ten,
                    SoDienThoai = sdt,
                    DiaChi = diaChi
                };

                da.Db.NhaCungCaps.InsertOnSubmit(newNCC);
                da.Db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi thêm nhà cung cấp: " + ex.Message);
            }
        }
        public void DelNCC(string maNCC)
        {
            var ncc = da.Db.NhaCungCaps.FirstOrDefault(t => t.MaNhaCungCap == maNCC);
            if (ncc == null) throw new Exception("Mã nhà cung cấp không tồn tại.");

            da.Db.NhaCungCaps.DeleteOnSubmit(ncc);
            da.Db.SubmitChanges();
        }
        public void SuaNhaCungCap(DTO_NhaCungCap ncc)
        {
            try
            {
                if (ncc == null)
                    throw new Exception("Dữ liệu nhà cung cấp không hợp lệ!");

                // --- Làm sạch chuỗi ---
                string ten = ncc.TenNhaCungCap.Trim();
                string sdt = ncc.SoDienThoai.Trim();
                string diaChi = ncc.DiaChi.Trim();

                // --- Kiểm tra rỗng ---
                if (string.IsNullOrWhiteSpace(ten))
                    throw new Exception("Tên nhà cung cấp không được để trống!");
                if (string.IsNullOrWhiteSpace(sdt))
                    throw new Exception("Số điện thoại không được để trống!");
                if (string.IsNullOrWhiteSpace(diaChi))
                    throw new Exception("Địa chỉ không được để trống!");
                if (!sdt.All(char.IsDigit))
                    throw new Exception("Số điện thoại chỉ được chứa ký tự số!");
                // --- Kiểm tra tên hợp lệ ---
                if (ten.Length > 30)
                    throw new Exception("Tên nhà cung cấp không được vượt quá 30 ký tự!");
                if (diaChi.Length > 30)
                    throw new Exception("Địa chỉ nhà cung cấp không được vượt quá 30 ký tự!");
                if (sdt.Length > 10)
                    throw new Exception("Số điện thoại không được quá 10 số");
                if (sdt.Length < 10)
                    throw new Exception("Số điện thoại phải có 10 số");

                // Kiểm tra chỉ chứa số

                if (!System.Text.RegularExpressions.Regex.IsMatch(ten, @"^[A-Za-zÀ-ỹ0-9]+(?:\s[A-Za-zÀ-ỹ0-9]+)*$"))
                    throw new Exception("Tên nhà cung cấp không hợp lệ! Không được chứa ký tự đặc biệt hoặc nhiều khoảng trắng liên tiếp.");

                // --- Kiểm tra địa chỉ ---
                if (!System.Text.RegularExpressions.Regex.IsMatch(diaChi, @"^[A-Za-zÀ-ỹ0-9/,]+(?:\s[A-Za-zÀ-ỹ0-9/,]+)*$"))
                    throw new Exception("Địa chỉ không hợp lệ !!!");

                // --- Kiểm tra số điện thoại ---
                if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^(0|\+84)[0-9]{9,10}$"))
                    throw new Exception("Số điện thoại không hợp lệ! (VD: 0901234567 hoặc +84901234567)");
                var data = da.Db.NhaCungCaps.FirstOrDefault(x => x.MaNhaCungCap == ncc.MaNhaCungCap);
                if (data == null)
                    throw new Exception("Không tìm thấy nhà cung cấp cần sửa!");

                // Kiểm tra trùng (ngoại trừ chính nó)
                if (da.Db.NhaCungCaps.Any(x => x.TenNhaCungCap == ten && x.MaNhaCungCap != ncc.MaNhaCungCap))
                    throw new Exception("Tên nhà cung cấp đã tồn tại!");
                if (da.Db.NhaCungCaps.Any(x => x.SoDienThoai == sdt && x.MaNhaCungCap != ncc.MaNhaCungCap))
                    throw new Exception("Số điện thoại đã được sử dụng!");
                if (da.Db.NhaCungCaps.Any(x => x.DiaChi == diaChi && x.MaNhaCungCap != ncc.MaNhaCungCap))
                    throw new Exception("Địa chỉ này đã được sử dụng!");

                // Cập nhật dữ liệu
                data.TenNhaCungCap = ten;
                data.SoDienThoai = sdt;
                data.DiaChi = diaChi;

                da.Db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi khi sửa nhà cung cấp: " + ex.Message);
            }
        


        }
    }
}

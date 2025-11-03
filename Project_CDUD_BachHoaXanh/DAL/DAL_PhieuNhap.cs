using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_PhieuNhap
    {
        // Fields
        private DatabaseAccess da = new DatabaseAccess();
        public DatabaseAccess Da { get => da; set => da = value; }

        // Lấy danh sách phiếu nhập
        public IQueryable LayDSPhieuNhap()
        {
            IQueryable temp = from pn in da.Db.PhieuNhaps
                              select new
                              {

                                  MaPhieuNhap = pn.MaPhieuNhap,
                                  NgayNhap = pn.NgayNhap,
                                  ThanhTien = pn.ThanhTien,
                                  MaNhanVien = pn.maNhanVien
                              };
            return temp;
        }

        public IQueryable LayDSNhanVien()
        {
            IQueryable queryable = from nv in da.Db.NhanViens
                                   select new
                                   {
                                       nv.maNhanVien,
                                       nv.tenNhanVien,
                                       nv.soDienThoai,
                                       nv.diaChi,
                                       nv.maChucVu,
                                       nv.maTaiKhoan
                                   };
            return queryable;
        }
        public bool ThemPhieuNhap(DTO_PhieuNhap pn)
        {
            try
            {
                if (pn == null)
                    throw new Exception("Dữ liệu phiếu nhập không hợp lệ (null).");

                // ✅ 1. Kiểm tra nhân viên tồn tại bằng MÃ
                bool nvExists = da.Db.NhanViens.Any(nv => nv.maNhanVien == pn.MaNhanVien);
                if (!nvExists)
                    throw new Exception("Nhân viên nhập phiếu không tồn tại.");

                // ✅ 2. Ngày nhập phải hợp lệ
                if (pn.NgayNhap == default(DateTime))
                    throw new Exception("Ngày nhập không hợp lệ.");

                // ✅ 3. Không cho trùng phiếu cùng nhân viên và cùng ngày
                bool duplicate = da.Db.PhieuNhaps
                    .Any(x => x.NgayNhap == pn.NgayNhap && x.maNhanVien == pn.MaNhanVien);
                if (duplicate)
                    throw new Exception("Phiếu nhập của nhân viên này trong ngày đã tồn tại.");

                // ✅ 4. Sinh mã phiếu nhập tự động (PN001, PN002,...)
                var existingMaPNs = da.Db.PhieuNhaps
                    .Select(p => p.MaPhieuNhap)
                    .ToList();

                int nextNumber = 1;
                string newMaPN;
                while (true)
                {
                    newMaPN = nextNumber < 10 ? $"PN00{nextNumber}" :
                               nextNumber < 100 ? $"PN0{nextNumber}" : $"PN{nextNumber}";

                    if (!existingMaPNs.Contains(newMaPN))
                        break;

                    nextNumber++;
                }

               

                // ✅ 6. Tạo đối tượng PhieuNhap mới
                var newPN = new PhieuNhap
                {
                    MaPhieuNhap = newMaPN,
                    NgayNhap = pn.NgayNhap,
                    maNhanVien = pn.MaNhanVien, // Dùng mã nhân viên
                    ThanhTien = 0,// Tạm thời gán 0
                };

                // ✅ 7. Lưu vào DB
                da.Db.PhieuNhaps.InsertOnSubmit(newPN);
                da.Db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public void XoaPhieuNhap(string maPhieuNhap)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maPhieuNhap))
                    throw new Exception("Mã phiếu nhập không được để trống.");

                // Làm mới context mỗi lần gọi để tránh cache dữ liệu cũ
                da = new DatabaseAccess();

                var phieu = da.Db.PhieuNhaps.FirstOrDefault(p => p.MaPhieuNhap == maPhieuNhap);
                if (phieu == null)
                    throw new Exception($"Không tìm thấy phiếu nhập có mã [{maPhieuNhap}] để xóa.");

                // Kiểm tra còn chi tiết không
                bool hasDetails = da.Db.ChiTietPhieuNhaps.Any(ct => ct.maPhieuNhap == maPhieuNhap);
                if (hasDetails)
                    throw new Exception("Không thể xóa phiếu nhập vì vẫn còn chi tiết phiếu nhập. Hãy xóa hết chi tiết trước!");

                // Xóa phiếu nhập
                da.Db.PhieuNhaps.DeleteOnSubmit(phieu);
                da.Db.SubmitChanges();

                // Làm mới lại context sau khi xóa
                da = new DatabaseAccess();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }



        public bool SuaPhieuNhap(DTO_PhieuNhap pn)
        {
            try
            {
                if (pn == null)
                    throw new Exception("Dữ liệu phiếu nhập không hợp lệ (null).");

                // 🔹 Kiểm tra nhân viên có tồn tại
                bool nvExists = da.Db.NhanViens.Any(nv => nv.maNhanVien == pn.MaNhanVien);
                if (!nvExists)
                    throw new Exception("Nhân viên nhập phiếu không tồn tại.");

                // 🔹 Kiểm tra ngày nhập hợp lệ (chỉ cho hôm nay hoặc tương lai)
                if (pn.NgayNhap.Date < DateTime.Today)
                    throw new Exception("Ngày nhập không được nhỏ hơn ngày hôm nay.");

                // 🔹 Tìm phiếu nhập cần sửa theo mã
                var pnn = da.Db.PhieuNhaps.FirstOrDefault(dt => dt.MaPhieuNhap == pn.MaPhieuNhap);
                if (pnn == null)
                    throw new Exception("Không tìm thấy phiếu nhập để sửa.");

                // 🔹 Ràng buộc: không cho trùng ngày + nhân viên (ngoại trừ chính nó)
                bool duplicate = da.Db.PhieuNhaps
                    .Any(x => x.NgayNhap == pn.NgayNhap
                           && x.maNhanVien == pn.MaNhanVien
                           && x.MaPhieuNhap != pn.MaPhieuNhap);
                if (duplicate)
                    throw new Exception("Phiếu nhập của nhân viên này trong ngày đã tồn tại.");

                // 🔹 Cập nhật thông tin
                pnn.NgayNhap = pn.NgayNhap;
                pnn.maNhanVien = pn.MaNhanVien;
                // Không cho sửa thành tiền trực tiếp
                da.Db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public void CapNhatThanhTien(string maPhieuNhap)
        {
            try
            {
                // 🔍 Tìm phiếu nhập theo MÃ
                var phieuNhap = da.Db.PhieuNhaps.FirstOrDefault(p => p.MaPhieuNhap == maPhieuNhap);
                if (phieuNhap == null)
                    throw new Exception("Không tìm thấy phiếu nhập để cập nhật thành tiền.");

                // 🔍 Tính tổng thành tiền từ chi tiết phiếu nhập (theo MÃ)
                var thanhTien = (from ctpn in da.Db.ChiTietPhieuNhaps
                                 where ctpn.maPhieuNhap == maPhieuNhap
                                 select (double?)(ctpn.SoLuong * ctpn.DonGia)) // dùng double? thay vì decimal?
                                .Sum();

                // Nếu null (không có chi tiết), gán 0
                phieuNhap.ThanhTien = thanhTien ?? 0;

                da.Db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_ChiTietPhieuNhap
    {
        private DatabaseAccess da = new DatabaseAccess();
        private DAL_PhieuNhap dalPhieuNhapAll = new DAL_PhieuNhap();

        public IQueryable LayDSChiTietPhieuNhap()
        {
            IQueryable temp = from cl in da.Db.ChiTietPhieuNhaps
                              join pn in da.Db.PhieuNhaps
                              on cl.maPhieuNhap equals pn.MaPhieuNhap
                              join sp in da.Db.SanPhams
                              on cl.maSanPham equals sp.maSanPham
                              orderby cl.maPhieuNhap ascending
                              select new
                              {
                                  id = cl.id,
                                  SoLuong = cl.SoLuong,
                                  DonGia = cl.DonGia,
                                  MaPhieuNhap = pn.MaPhieuNhap,
                                  MaSanPham = sp.tenSanPham,
                                  Mapn = cl.maPhieuNhap,
                                  Masp = cl.maSanPham
                              };
            return temp;
        }
        public IQueryable LayDSSP()
        {
            IQueryable temp = from ll in da.Db.SanPhams
                              select new
                              {
                                  ll.maSanPham,
                                  ll.tenSanPham,
                              };
            return temp;
        }
        public bool ThemChiTiet(DTO_ChiTietPhieuNhap ct)
        {
            try
            {
                if (ct.SoLuong == null)
                    throw new Exception("Số lượng không được để trống.");
                if (ct.DonGia == null)
                    throw new Exception("Đơn giá không được để trống.");

                bool existsPN = da.Db.PhieuNhaps.Any(p => p.MaPhieuNhap == ct.MaPhieuNhap);
                if (!existsPN)
                    throw new Exception($"Mã phiếu nhập '{ct.MaPhieuNhap}' không tồn tại.");

                bool existsSP = da.Db.SanPhams.Any(s => s.maSanPham == ct.MaSanPham);
                if (!existsSP)
                    throw new Exception($"Mã sản phẩm '{ct.MaSanPham}' không tồn tại.");

                if (ct.SoLuong <= 0)
                    throw new Exception("Số lượng phải lớn hơn 0.");
                if (ct.DonGia <= 0)
                    throw new Exception("Đơn giá phải lớn hơn 0.");

                bool duplicate = da.Db.ChiTietPhieuNhaps.Any(x =>
                    x.maPhieuNhap == ct.MaPhieuNhap && x.maSanPham == ct.MaSanPham);
               

                // ✅ Thêm chi tiết phiếu nhập
                ChiTietPhieuNhap chiTiet = new ChiTietPhieuNhap
                {
                    maPhieuNhap = ct.MaPhieuNhap,
                    maSanPham = ct.MaSanPham,
                    SoLuong = ct.SoLuong,
                    DonGia = ct.DonGia
                };

                da.Db.ChiTietPhieuNhaps.InsertOnSubmit(chiTiet);
                da.Db.SubmitChanges(); // ⚡ Lưu thay đổi trước
                                       // ✅ Cập nhật kho hàng
                                       // ✅ Lấy mã chi nhánh từ phiếu nhập
                                       // ✅ Lấy mã chi nhánh từ phiếu nhập
                string maChiNhanh = da.Db.PhieuNhaps
                    .Where(p => p.MaPhieuNhap == ct.MaPhieuNhap)
                    .Select(p => p.MaChiNhanh)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(maChiNhanh))
                    throw new Exception("Không tìm thấy chi nhánh của phiếu nhập!");

                // ✅ Gọi DAL_KhoHang để thêm hoặc cập nhật kho
                DAL_KhoHang dalKho = new DAL_KhoHang();
                dalKho.ThemHoacCapNhatKho(ct.MaSanPham, maChiNhanh, ct.SoLuong);
                // ✅ Làm mới context trước khi cập nhật tổng tiền
                dalPhieuNhapAll = new DAL_PhieuNhap();
                dalPhieuNhapAll.CapNhatThanhTien(ct.MaPhieuNhap);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public void XoaChiTiet(int idChiTiet)
        {
            try
            {
                var chiTiet = da.Db.ChiTietPhieuNhaps.FirstOrDefault(c => c.id == idChiTiet);
                if (chiTiet == null)
                    throw new Exception("Không tìm thấy chi tiết phiếu nhập để xóa.");

                string maPhieuNhap = chiTiet.maPhieuNhap;
                string maSanPham = chiTiet.maSanPham;
                int soLuong = (int)chiTiet.SoLuong;

                // ✅ Lấy mã chi nhánh từ phiếu nhập
                string maChiNhanh = da.Db.PhieuNhaps
                    .Where(p => p.MaPhieuNhap == maPhieuNhap)
                    .Select(p => p.MaChiNhanh)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(maChiNhanh))
                    throw new Exception("Không tìm thấy chi nhánh của phiếu nhập!");

                // ✅ Trừ số lượng trong kho
                DAL_KhoHang dalKho = new DAL_KhoHang();

                // Giảm số lượng trong kho tương ứng
                var kho = da.Db.KhoHangs.FirstOrDefault(k => k.maSanPham == maSanPham && k.maChiNhanh == maChiNhanh);
                if (kho != null)
                {
                    kho.soLuong -= soLuong;
                    if (kho.soLuong < 0) kho.soLuong = 0;
                }


                da.Db.ChiTietPhieuNhaps.DeleteOnSubmit(chiTiet);
                da.Db.SubmitChanges();

                // Cập nhật lại thành tiền
                if (!string.IsNullOrEmpty(maPhieuNhap))
                {
                    dalPhieuNhapAll.CapNhatThanhTien(maPhieuNhap);
                }

                // Làm mới context để tránh cache cũ
                da = new DatabaseAccess();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public bool SuaChiTiet(DTO_ChiTietPhieuNhap ct)
        {
            try
            {
                if (ct == null)
                    throw new Exception("Chi tiết phiếu nhập không hợp lệ.");

                // ✅ Kiểm tra mã phiếu nhập
                bool existsPN = da.Db.PhieuNhaps.Any(p => p.MaPhieuNhap == ct.MaPhieuNhap);
                if (!existsPN)
                    throw new Exception($"Mã phiếu nhập '{ct.MaPhieuNhap}' không tồn tại.");

                // ✅ Kiểm tra mã sản phẩm
                bool existsSP = da.Db.SanPhams.Any(s => s.maSanPham == ct.MaSanPham);
                if (!existsSP)
                    throw new Exception($"Mã sản phẩm '{ct.MaSanPham}' không tồn tại.");

                // ✅ Kiểm tra số lượng và đơn giá
                if (ct.SoLuong <= 0)
                    throw new Exception("Số lượng phải lớn hơn 0.");
                if (ct.DonGia <= 0)
                    throw new Exception("Đơn giá phải lớn hơn 0.");

                // ✅ Tìm chi tiết cần sửa
                var chiTiet = da.Db.ChiTietPhieuNhaps.FirstOrDefault(c => c.id == ct.Id);
                if (chiTiet == null)
                    throw new Exception("Không tìm thấy chi tiết phiếu nhập để sửa.");

                // ✅ Lưu thông tin cũ để cập nhật kho
                string maSPCu = chiTiet.maSanPham;
                string maPhieuNhap = chiTiet.maPhieuNhap;
                int soLuongCu = chiTiet.SoLuong ?? 0;

                // ✅ Lấy mã chi nhánh từ phiếu nhập
                string maChiNhanhCu = da.Db.PhieuNhaps
                    .Where(p => p.MaPhieuNhap == maPhieuNhap)
                    .Select(p => p.MaChiNhanh)
                    .FirstOrDefault();

                // ✅ Cập nhật thông tin mới
                chiTiet.maPhieuNhap = ct.MaPhieuNhap;
                chiTiet.maSanPham = ct.MaSanPham;
                chiTiet.SoLuong = ct.SoLuong;
                chiTiet.DonGia = ct.DonGia;
                da.Db.SubmitChanges();

                // ✅ Cập nhật lại thành tiền phiếu nhập
                dalPhieuNhapAll.CapNhatThanhTien(ct.MaPhieuNhap);

                // ✅ Cập nhật lại kho hàng
                string maChiNhanhMoi = da.Db.PhieuNhaps
                    .Where(p => p.MaPhieuNhap == ct.MaPhieuNhap)
                    .Select(p => p.MaChiNhanh)
                    .FirstOrDefault();

                DAL_KhoHang dalKho = new DAL_KhoHang();

                // Nếu đổi sản phẩm hoặc chi nhánh => xử lý tách biệt
                if (maSPCu != ct.MaSanPham || maChiNhanhCu != maChiNhanhMoi)
                {
                    // Trừ số lượng cũ ở kho cũ
                    dalKho.TruSoLuong(maSPCu, maChiNhanhCu, soLuongCu);

                    // Cộng số lượng mới ở kho mới
                    dalKho.CongSoLuong(ct.MaSanPham, maChiNhanhMoi, ct.SoLuong);
                }
                else
                {
                    // Chỉ thay đổi số lượng => cập nhật chênh lệch
                    int chenhLech = ct.SoLuong - soLuongCu;
                    if (chenhLech > 0)
                        dalKho.CongSoLuong(ct.MaSanPham, maChiNhanhMoi, chenhLech);
                    else if (chenhLech < 0)
                        dalKho.TruSoLuong(ct.MaSanPham, maChiNhanhMoi, Math.Abs(chenhLech));
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa chi tiết phiếu nhập: " + ex.Message);
            }
        }



    }
}

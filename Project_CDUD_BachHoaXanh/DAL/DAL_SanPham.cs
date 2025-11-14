using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_SanPham
    {
        private DatabaseAccess da = new DatabaseAccess();
        public IQueryable LoadNhaCungCap()
        {
            return from sl in da.Db.NhaCungCaps
                   select sl;
        }

        public IQueryable LayDSSanPham()
        {
            try
            {
                return from sp in da.Db.SanPhams
                       select sp;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách sản phẩm: " + ex.Message);
            }
        }

        public List<DTO_SanPhamKhoHang> ListSanPham()
        {
            try
            {
                return (from sp in da.Db.SanPhams
                        join kho in da.Db.KhoHangs
                        on sp.maSanPham equals kho.maSanPham
                        select new DTO_SanPhamKhoHang
                        {
                            MaSanPham = sp.maSanPham,
                            TenSanPham = sp.tenSanPham,
                            MaLoaiHang = sp.maLoaiHang,
                            GiaBan = (double)sp.donGia,
                            SoLuong = (int)kho.soLuong,
                            AnhSanPham = sp.anhSanPham
                        }).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("lỗi khi lấy danh sách sản phẩm: " + ex.Message);
            }
        }

        public List<DTO_SanPhamKhoHang> ListSanPham_BanHang()
        {
            try
            {
                using (var db = new QLBHXDataContext())
                {
                    var query =
                        from sp in db.SanPhams

                            // LEFT JOIN Khuyến Mãi
                        join km in db.KhuyenMais
                            on sp.maKhuyenMai equals km.MaKhuyenMai into kmLeft
                        from km in kmLeft.DefaultIfEmpty()   // km có thể null

                            // GROUP JOIN kho hàng
                        join kho in db.KhoHangs
                            on sp.maSanPham equals kho.maSanPham into khos

                        select new DTO_SanPhamKhoHang
                        {
                            MaSanPham = sp.maSanPham,
                            TenSanPham = sp.tenSanPham,
                            MaLoaiHang = sp.maLoaiHang,

                            // Giá gốc
                            // Giá bán trực tiếp đã áp dụng khuyến mãi
                            GiaBan = ((double?)sp.donGia ?? 0) *
                             (1 - ((km != null && km.GiaTri.HasValue) ? km.GiaTri.Value / 100.0 : 0)),

                            AnhSanPham = sp.anhSanPham,

                            // Tổng số lượng an toàn (nếu không có kho, trả 0)
                            SoLuong = khos.Sum(k => (int?)k.soLuong) ?? 0,

                            // Dữ liệu khuyến mãi an toàn tuyệt đối
                            MaKhuyenMai = sp.maKhuyenMai,
                            PhanTramKM = km != null && km.GiaTri.HasValue
                                          ? km.GiaTri.Value
                                          : 0
                        };

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải sản phẩm bán hàng: " + ex.Message);
            }
        }





        public bool ThemSanPham(DTO_SanPham sanpham)
        {
            try
            {
                //kiểm tra mã sản phẩm có tồn tại chưa
                var query2 = da.Db.SanPhams.OrderByDescending(x => x.maSanPham).FirstOrDefault();
                var data = da.Db.SanPhams.FirstOrDefault(dt => dt.maSanPham == sanpham.MaSanPham);

                var existingMaSP = da.Db.SanPhams
                .Select(cv => cv.maSanPham)
                .ToList();

                int nextNumber = 1;
                string newMa;
                while (true)
                {
                    newMa = nextNumber < 10 ? $"SP00{nextNumber}" :
                                 nextNumber < 100 ? $"SP0{nextNumber}" : $"SP{nextNumber}";
                    if (!existingMaSP.Contains(newMa))
                        break;
                    nextNumber++;
                }
                if (data == null)
                {
                    //Sản phẩm được thêm
                    SanPham sp = new SanPham();
                    sp.maSanPham = newMa;
                    sp.tenSanPham = sanpham.TenSanPham;
                    sp.donViTinh = sanpham.DonViTinh;
                    sp.donGia = sanpham.DonGia;
                    sp.ngaySanXuat = sanpham.NgaySanXuat;
                    sp.hanSuDung = sanpham.HanSuDung;
                    sp.maLoaiHang = sanpham.MaLoaiHang;
                    sp.maNhaCungCap = sanpham.MaNhaCungCap;
                    sp.maKhuyenMai = sanpham.MaKhuyenMai;
                    sp.anhSanPham = sanpham.AnhSanPham;
                    da.Db.SanPhams.InsertOnSubmit(sp);
                    da.Db.SubmitChanges();

                    KhoHang kho = new KhoHang();
                    kho.maSanPham = sp.maSanPham;   // lấy id vừa mới insert
                    kho.soLuong = 0;         // khởi tạo với 0 hoặc số lượng mặc định
                    da.Db.KhoHangs.InsertOnSubmit(kho);
                    da.Db.SubmitChanges();
                    return true;
                }
                else
                {
                    //quăng lỗi
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra: " + ex.Message);
            }
        }

        public bool XoaSanPham(string maSP)
        {
            try
            {
                // Xóa tất cả bản ghi KhoHangs có liên quan đến sản phẩm
                var khoList = da.Db.KhoHangs.Where(k => k.maSanPham == maSP);
                da.Db.KhoHangs.DeleteAllOnSubmit(khoList);

                // Xóa sản phẩm
                var sp = da.Db.SanPhams.FirstOrDefault(s => s.maSanPham == maSP);
                if (sp != null)
                {
                    da.Db.SanPhams.DeleteOnSubmit(sp);
                }

                // Lưu thay đổi
                da.Db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SuaSanPham(DTO_SanPham sanphamdto)
        {
            try
            {
                //kiểm tra mã sp có tồn tại chưa
                var sp = da.Db.SanPhams.FirstOrDefault(dt => dt.maSanPham == sanphamdto.MaSanPham);
                if (sp != null)
                {
                    sp.tenSanPham = sanphamdto.TenSanPham;
                    sp.donViTinh = sanphamdto.DonViTinh;
                    sp.donGia = sanphamdto.DonGia;
                    sp.ngaySanXuat = sanphamdto.NgaySanXuat;
                    sp.hanSuDung = sanphamdto.HanSuDung;
                    sp.maLoaiHang = sanphamdto.MaLoaiHang;
                    sp.maKhuyenMai = sanphamdto.MaKhuyenMai;
                    sp.maNhaCungCap = sanphamdto.MaNhaCungCap;
                    sp.anhSanPham = sanphamdto.AnhSanPham;
                    //cập nhật lại
                    da.Db.SubmitChanges();
                    return true;

                }
                else
                {
                    //quăng lỗi
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra: " + ex.Message);
            }

        }

        public IQueryable LocSpTheoTen(string key)
        {
            var query = from sp in da.Db.SanPhams
                        where sp.tenSanPham.ToLower().Contains(key.ToLower())
                        select new
                        {
                            sp.maSanPham,
                            sp.tenSanPham,
                            sp.donViTinh,
                            sp.donGia,
                        };

            return query;
        }

        public int GetSoLuongSpTrongKho(string maSP)
        {
            var query = (from sp in da.Db.SanPhams
                         join kho in da.Db.KhoHangs
                         on sp.maSanPham equals kho.maSanPham
                         where sp.maSanPham == maSP
                         select new
                         {
                             maSP = sp.maSanPham,
                             TenSanPham = sp.tenSanPham,
                             SoLuong = kho.soLuong
                         }).FirstOrDefault();

            return (int)query.SoLuong;
        }

        public IQueryable GetListSP()
        {
            var query = from sp in da.Db.SanPhams
                        join kho in da.Db.KhoHangs
                        on sp.maSanPham equals kho.maSanPham
                        select new
                        {
                            sp.maSanPham,
                            sp.tenSanPham,
                            sp.donViTinh,
                            sp.donGia,
                            kho.soLuong
                        };

            return query;
        }

        public IQueryable SearchSpByTenSP(string tenSp)
        {
            var query = from sp in da.Db.SanPhams
                        join kho in da.Db.KhoHangs
                        on sp.maSanPham equals kho.maSanPham
                        where sp.tenSanPham.ToLower().Contains(tenSp.ToLower())
                        select new
                        {
                            sp.maSanPham,
                            sp.tenSanPham,
                            sp.donViTinh,
                            sp.donGia,
                            kho.soLuong
                        };

            return query;
        }

        public IQueryable SearchSpByDVT(string donViTinh)
        {
            var query = from sp in da.Db.SanPhams
                        join kho in da.Db.KhoHangs
                        on sp.maSanPham equals kho.maSanPham
                        where sp.donViTinh.ToLower().Contains(donViTinh.ToLower())
                        select new
                        {
                            sp.maSanPham,
                            sp.tenSanPham,
                            sp.donViTinh,
                            sp.donGia,
                            kho.soLuong
                        };

            return query;
        }
        public bool KiemTraTrung(string tenSP, string donViTinh, float donGia, string maSPHienTai = null)
        {
            var query = da.Db.SanPhams.Where(sp =>
                        sp.tenSanPham == tenSP &&
                        sp.donViTinh == donViTinh &&
                        sp.donGia == donGia);

            // Nếu đang sửa → loại trừ chính sản phẩm này
            if (!string.IsNullOrEmpty(maSPHienTai))
            {
                query = query.Where(sp => sp.maSanPham != maSPHienTai);
            }

            return query.Any();
        }
        public int LaySoLuongTonKho(string maSP)
        {
            // Giả sử da.Db.KhoHangs là bảng kho hàng
            var tongSoLuong = da.Db.KhoHangs
                .Where(k => k.maSanPham == maSP)
                .Sum(k => (int?)k.soLuong) ?? 0;

            return tongSoLuong;
        }
        public DTO_SanPham GetSanPhamTheoMa(string maSP)
        {
            var sp = da.Db.SanPhams.SingleOrDefault(s => s.maSanPham == maSP);
            if (sp == null) return null;

            return new DTO_SanPham
            {
                MaSanPham = sp.maSanPham,
                TenSanPham = sp.tenSanPham,
                DonGia = (float)sp.donGia,
                MaKhuyenMai = sp.maKhuyenMai
            };
        }
    }
}

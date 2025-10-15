using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_NhanVien
    {
        DatabaseAccess da = new DatabaseAccess();
         
        public IQueryable LayDSNhanVien()
        {
            IQueryable query = from nv in da.Db.NhanViens
                               join cv in da.Db.ChucVus on nv.maChucVu equals cv.maChucVu
                               select new
                               {
                                   nv.maNhanVien,
                                   nv.tenNhanVien,
                                   nv.soDienThoai,
                                   nv.diaChi,
                                   nv.LoaiNhanVien,
                                   cv.tenChucVu,
                                   nv.maChiNhanh,                                
                                   nv.maTaiKhoan
                               };
            return query;
        }

        public bool AddNV(DTO_NhanVien nhanVien)
        {

            // 🔢 Sinh mã lịch làm mới
            var existingMaCLs = da.Db.NhanViens.Select(nv => nv.maNhanVien).ToList();
            int nextNumber = 1;
            string newMa;

            while (true)
            {
                newMa = nextNumber < 10 ? $"NV00{nextNumber}" :
                            nextNumber < 100 ? $"NV0{nextNumber}" : $"NV{nextNumber}";
                if (!existingMaCLs.Contains(newMa))
                    break;
                nextNumber++;
            }

            NhanVien newNV = new NhanVien
            {
                maNhanVien = newMa.Trim(),
                tenNhanVien = nhanVien.TenNV.Trim(),
                soDienThoai = nhanVien.SoDT1.Trim(),
                diaChi = nhanVien.DiaChi.Trim(),
                LoaiNhanVien = nhanVien.LoaiNhanVien.Trim(),
                maChucVu = nhanVien.MaChucVu.Trim(),
                maChiNhanh = nhanVien.MaChiNhanh.Trim(),
                maTaiKhoan = nhanVien.MaTaiKhoan.Trim()
            };
            da.Db.NhanViens.InsertOnSubmit(newNV);

            da.Db.SubmitChanges();
            return true;
            //           

        }

        public bool UpdateNV(DTO_NhanVien nhanVien)
        {
            try
            {
                string nameNV = string.Empty;

                if (nhanVien != null)
                {
                    NhanVien nv_update = da.Db.NhanViens.Single(nv => nv.maNhanVien == nhanVien.MaNV);
                   
                    nv_update.tenNhanVien = nhanVien.TenNV;
                    nv_update.soDienThoai = nhanVien.SoDT1;
                    nv_update.diaChi = nhanVien.DiaChi;
                    nv_update.LoaiNhanVien = nhanVien.LoaiNhanVien;
                    nv_update.maChucVu = nhanVien.MaChucVu;
                    nv_update.maChiNhanh = nhanVien.MaChiNhanh;
                    nv_update.maTaiKhoan = nhanVien.MaTaiKhoan;

                    // Saved db
                    da.Db.SubmitChanges();
                    nameNV = nv_update.tenNhanVien;
                    return true;
                }

            }
            catch (Exception ex)
            {
                // Messaged
                throw;
                return false;
            }
            return false;
        }

        public bool DeleteNV(string maNVDEL)
        {
            try
            {
                // Initialize Variables
                string nameNV = string.Empty;

                // Checked id nhanvien is saved in db NhanVien
                var query = (from nv in da.Db.NhanViens
                             where nv.maNhanVien == maNVDEL
                             select nv).Count();

                if (query == 1)
                {
                    // Init NhanVien
                    NhanVien nv_update = da.Db.NhanViens.Single(nv => nv.maNhanVien == maNVDEL);

                    // Saved db
                    da.Db.NhanViens.DeleteOnSubmit(nv_update);
                    da.Db.SubmitChanges();
                    nameNV = nv_update.tenNhanVien;

                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        public string GetMaxMaNV()
        {
            var query = da.Db.NhanViens.OrderByDescending(nv => nv.maNhanVien).FirstOrDefault();

            return query.maNhanVien;
        }
        public IQueryable SearchNvByMaNV(string maNV)
        {
            var query = from nv in da.Db.NhanViens
                        where nv.maNhanVien.ToLower().Contains(maNV.ToLower())
                        select new
                        {
                            nv.maNhanVien,
                            nv.tenNhanVien,
                            nv.soDienThoai,
                            nv.diaChi,
                            nv.LoaiNhanVien,
                            nv.maChucVu,
                            nv.maChiNhanh,
                            nv.maTaiKhoan
                        };

            return query;
        }
        public IQueryable SearchNvBytenNV(string tenNV)
        {
            var query = from nv in da.Db.NhanViens
                        where nv.tenNhanVien.ToLower().Contains(tenNV.ToLower())
                        select new
                        {
                            nv.maNhanVien,
                            nv.tenNhanVien,
                            nv.soDienThoai,
                            nv.diaChi,
                            nv.LoaiNhanVien,
                            nv.maChucVu,
                            nv.maChiNhanh,
                            nv.maTaiKhoan
                        };

            return query;
        }
        public IQueryable GetListNV2()
        {
            IQueryable query = from nv in da.Db.NhanViens
                               select new
                               {
                                   nv.maNhanVien,
                                   nv.tenNhanVien,
                                   nv.soDienThoai,
                                   nv.diaChi,
                                   nv.LoaiNhanVien,
                                   nv.maChucVu,
                                   nv.maChiNhanh,
                                   nv.maTaiKhoan
                               };
            return query;
        }
        public DTO_NhanVien getNhanVien(string maTaiKhoan)
        {
            var nhanvien = da.Db.NhanViens.FirstOrDefault(nv => nv.maTaiKhoan == maTaiKhoan);

            if (nhanvien == null)
                return null;

            DTO_NhanVien dto_NhanVien = new DTO_NhanVien
            {
                MaNV = nhanvien.maNhanVien,
                TenNV = nhanvien.tenNhanVien,
                SoDT1 = nhanvien.soDienThoai,
                DiaChi = nhanvien.diaChi,
                LoaiNhanVien = nhanvien.LoaiNhanVien,
                MaChucVu = nhanvien.maChucVu,
                MaChiNhanh = nhanvien.maChiNhanh,
                MaTaiKhoan = nhanvien.maTaiKhoan
            };

            return dto_NhanVien;
        }

        public IQueryable LayDSChucVu()
        {
            IQueryable temp = from cn in da.Db.ChucVus
                              select new
                              {
                                  cn.maChucVu,
                                  cn.tenChucVu,
                              };
            return temp;
        }

        //public IQueryable<DTO_ChucVu> LayDSChucVu()
        //{
        //    var temp = from cn in da.Db.ChucVus
        //               select new DTO_ChucVu
        //               {
        //                   MaChucvu = cn.maChucVu,
        //                   TenChucVu = cn.tenChucVu
        //               };
        //    return temp;
        //}

    }
}

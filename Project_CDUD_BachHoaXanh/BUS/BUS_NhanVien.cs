using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_NhanVien
    {
        DAL_NhanVien dal_nv = new DAL_NhanVien();

        public IQueryable LayDSNhanVien()
        {
            return dal_nv.LayDSNhanVien();
        }
        public bool AddNV(DTO.DTO_NhanVien nhanVien)
        {
            return dal_nv.AddNV(nhanVien);
        }

        public bool UpdateNV(DTO.DTO_NhanVien nhanVien)
        {
            return dal_nv.UpdateNV(nhanVien);
        }
        public bool XoaNV(string maNV)
        {
            return dal_nv.DeleteNV(maNV);
        }


        public string GetMaxIdNV()
        {
            return dal_nv.GetMaxMaNV();
        }

        public IQueryable SearchNvByMaNV(string maNV)
        {
            return dal_nv.SearchNvByMaNV(maNV);
        }

        public IQueryable SearchNvBytenNV(string tenNV)
        {
            return dal_nv.SearchNvBytenNV(tenNV);
        }

        public IQueryable GetListNV2()
        {
            return dal_nv.GetListNV2();
        }
        public DTO_NhanVien getNhanVien(string maTaiKhoan)
        {
            return dal_nv.getNhanVien(maTaiKhoan);
        }

        public IQueryable LayDSChucVu()
        {
            return dal_nv.LayDSChucVu();
        }
        public bool KiemTraSoDienThoaiTrung(string sdt,string maNV)
        {
            return dal_nv.KiemTraSoDienThoaiTrung(sdt,maNV);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_LichLam
    {
        // Fields
        private DAL_LichLam dal_ll = new DAL_LichLam();

        //Method
        // LayDSLichLam()
        public IQueryable LayDSLichLam()
        {
            return dal_ll.LayDSLichLam();
        }

        // ThemLichLam()
        public bool ThemLichLam(DTO_LichLam lichLam)
        {
             return dal_ll.ThemLichLam(lichLam);
        }

        // XoaLichLam()
        public bool XoaLichLam(string id)
        {
            return dal_ll.XoaLichLam(id);
        }

        // SuaLichLam()
        public bool SuaLichLam(DTO_LichLam lichLam)
        {
            return dal_ll.SuaLichLam(lichLam);
        }

        public IQueryable LayDSNV()
        {
            return dal_ll.LayDSNV();
        }

        public IQueryable TimKiemTheoTen(string ten)
        {
            return dal_ll.timkiemTheoTen(ten);
        }

        public IQueryable TimKiemTheoNgay(DateTime ngay)
        {
            return dal_ll.timkiemTheoNgay(ngay);
        }

        public bool DellLL(string id)
        {
            return dal_ll.DellLL(id);
        }
        public LichLam GetLichLamByMa(string maLichLam)
        {
            return dal_ll.GetLichLamByMa(maLichLam);
        }
    }
}

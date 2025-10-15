using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_ChucVu
    {
        DAL_ChucVu dal_cv = new DAL_ChucVu();
        public IQueryable GetListLNV()
        {
            return dal_cv.LayDSChucVu();
        }
        public IQueryable GetListAllLNVByTen()
        {
            return dal_cv.GetListAllLNVByTen();
        }

        public IQueryable GetListOneLNVByTen(string maCV)
        {
            return dal_cv.GetListOneLNVByTen(maCV);
        }

        public void AddLNV(DTO_ChucVu chucVu)
        {
            dal_cv.AddChucVu(chucVu);
        }

        public void UpdateLNV(DTO_ChucVu chucVu)
        {
            dal_cv.UpdateChucVu(chucVu);
        }

        public bool DelLNV(string maCV)
        {
            return dal_cv.DelChucVu(maCV);
        }

        public bool AddLNV2(DTO_ChucVu chucVu)
        {
            return dal_cv.AddChucVu2(chucVu);
        }

        public bool UpdateLNV2(DTO_ChucVu chucVu)
        {
            return dal_cv.UpdateChucVu2(chucVu);
        }

        public string GetMaxMaChucVu()
        {
            return dal_cv.GetMaxMaChucVu();
        }
    }
}

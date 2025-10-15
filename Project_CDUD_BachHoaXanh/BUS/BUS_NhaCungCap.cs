using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BUS
{
    public class BUS_NhaCungCap
    {
        // Fields
        private DAL_NhaCungCap dal_ncc = new DAL_NhaCungCap();

        //Method
        // LayDSNCC()
        public IQueryable LayDSNCC()
        {
            return dal_ncc.LayDSNCC();
        }

        // ThemNCC()
        public void ThemNCC(DTO_NhaCungCap ncc)
        {
            dal_ncc.ThemNhaCungCap(ncc);
        }

        public void DeleteNCC(string maNCC)
        {
            dal_ncc.DelNCC(maNCC);
        }
        public void SuaNCC(DTO_NhaCungCap ncc)
        {
            dal_ncc.SuaNhaCungCap(ncc);
        }
    }
}

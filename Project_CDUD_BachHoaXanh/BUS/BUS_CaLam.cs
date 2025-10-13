using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class BUS_CaLam
    {
        private DAL_CaLam dal_cl = new DAL_CaLam();

        public IQueryable LayDSCaLam()
        {
            return dal_cl.LayDSCaLam();
        }
        //public bool ThemCaLam(DTO.DTO_CaLam caLam)
        //{
        //    return dal_cl.ThemCaLam(caLam);
        //}
        public bool XoaCaLam(string maCaLam)
        {
            return dal_cl.XoaCaLam(maCaLam);
        }
        public bool suaCaLam(DTO.DTO_CaLam caLam)
        {
            return dal_cl.SuaCaLam(caLam);
        }
        public string TinhGioLam(CaLam cl)
        {
            return dal_cl.TinhGioLam(cl);
        }
        public CaLam GetCaLamById(string maCaLam)
        {
            return dal_cl.GetCaLamByMaCaLam(maCaLam);
        }
    }
}

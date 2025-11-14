using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class BUS_KhuyenMai
    {
        DAL_KhuyenMai dal_km = new DAL_KhuyenMai();

        // Methods
        public IQueryable GetListKM()
        {
            return dal_km.GetListKM();
        }
        public void AdddKhuyenMai(DTO_KhuyenMai khuyenMai)
        {
            dal_km.AddKM(khuyenMai);
        }
        public void UpdateKhuyenMai(DTO_KhuyenMai khuyenMai)
        {
            dal_km.UpdateKM(khuyenMai);
        }
        public void DeleteKhuyenMai(string maKM)
        {
            dal_km.DelKM(maKM);
        }
        public void getIdKhuyenMai(string maKM)
        {
            dal_km.GetKhuyenMaiByMa(maKM);
        }
        public bool KiemTraTenTonTai(string tenKhuyenMai, string maKhuyenMai)
        {
            // Giả sử có lớp DAL_KhuyenMai với Db.KhuyenMais
            return dal_km.KiemTraTenTonTai(tenKhuyenMai, maKhuyenMai);
        }
        public DTO_KhuyenMai GetKhuyenMaiTheoMa(string maKH)
        {
            return dal_km.GetKhuyenMaiTheoMa(maKH);
        }
    }
}

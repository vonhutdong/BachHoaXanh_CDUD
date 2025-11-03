using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_KhoHang
    {
        // Fields
        private DatabaseAccess da = new DatabaseAccess();
        // Methods
        public IQueryable LoadKhoHang()
        {
            var result = from kh in da.Db.KhoHangs
                         join sp in da.Db.SanPhams on kh.maSanPham equals sp.maSanPham
                         group kh by new { kh.maSanPham, sp.tenSanPham, kh.maChiNhanh } into g
                        
                         select new
                         {
                             MaSanPham = g.Key.maSanPham,
                             TenSanPham = g.Key.tenSanPham,
                             SoLuong = g.Sum(kh => kh.soLuong),
                             MaChiNhanh = g.Key.maChiNhanh,
                             id = g.Select(kh => kh.id).FirstOrDefault() // hoặc default nếu không cần
                         };
            return result;
        }
    }
}

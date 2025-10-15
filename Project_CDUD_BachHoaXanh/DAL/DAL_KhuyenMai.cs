using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_KhuyenMai
    {
        private DatabaseAccess da = new DatabaseAccess();
        public DatabaseAccess Da { get => da; set => da = value; }

        public DTO_KhuyenMai GetKhuyenMaiByMa(string maKM)
        {
            var km = da.Db.KhuyenMais.FirstOrDefault(t => t.MaKhuyenMai == maKM);
            if (km == null) return null;

            return new DTO_KhuyenMai
            {
                MaKhuyenMai = km.MaKhuyenMai,
                TenKhuyenMai = km.TenKhuyenMai,
                GiaTri = km.GiaTri ?? 0,
            };
        }
        public IQueryable GetListKM()
        {
            IQueryable query = from km in da.Db.KhuyenMais
                               select new
                               {
                                   km.MaKhuyenMai,
                                   km.TenKhuyenMai,
                                   km.GiaTri
                               };
            return query;
        }

        public void AddKM(DTO_KhuyenMai khuyenMai)
        {
            // Kiểm tra trùng tên
            var existed = da.Db.KhuyenMais.FirstOrDefault(km => km.TenKhuyenMai == khuyenMai.TenKhuyenMai);
            if (existed != null)
                throw new Exception("Tên khuyến mãi đã tồn tại.");

            // Lấy tất cả mã khuyến mãi hiện có
            var existingMaKMs = da.Db.KhuyenMais
                .Select(km => km.MaKhuyenMai)
                .ToList();

            // Tìm mã mới (tăng dần)
            int nextNumber = 1;
            string newMaKM;
            while (true)
            {
                newMaKM = nextNumber < 10 ? $"KM00{nextNumber}" :
                           nextNumber < 100 ? $"KM0{nextNumber}" : $"KM{nextNumber}";
                if (!existingMaKMs.Contains(newMaKM))
                    break;
                nextNumber++;
            }

            // Tạo khuyến mãi mới
            KhuyenMai newKM = new KhuyenMai
            {
                MaKhuyenMai = newMaKM,
                TenKhuyenMai = khuyenMai.TenKhuyenMai,
                GiaTri = khuyenMai.GiaTri
            };

            da.Db.KhuyenMais.InsertOnSubmit(newKM);
            da.Db.SubmitChanges();
        }

        // Cập nhật khuyến mãi theo mã
        public void UpdateKM(DTO_KhuyenMai khuyenMai)
        {
            var km = da.Db.KhuyenMais.FirstOrDefault(t => t.MaKhuyenMai == khuyenMai.MaKhuyenMai);
            if (km == null) throw new Exception("Mã khuyến mãi không tồn tại.");

            km.TenKhuyenMai = khuyenMai.TenKhuyenMai;
            km.GiaTri = khuyenMai.GiaTri;
            da.Db.SubmitChanges();
        }

        // Xóa khuyến mãi theo mã
        public void DelKM(string maKM)
        {
            var km = da.Db.KhuyenMais.FirstOrDefault(t => t.MaKhuyenMai == maKM);
            if (km == null) throw new Exception("Mã khuyến mãi không tồn tại.");

            da.Db.KhuyenMais.DeleteOnSubmit(km);
            da.Db.SubmitChanges();
        }
        public bool KiemTraTenTonTai(string tenKhuyenMai, string maKhuyenMai)
        {
            // Giả sử có lớp DAL_KhuyenMai với Db.KhuyenMais
            return da.Db.KhuyenMais.Any(km => km.TenKhuyenMai == tenKhuyenMai && km.MaKhuyenMai != maKhuyenMai);
        }
    }
}
    


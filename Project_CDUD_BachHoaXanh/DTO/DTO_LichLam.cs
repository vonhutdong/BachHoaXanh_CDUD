using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_LichLam
    {
        private string maLichLam;
        private DateTime ngayLam;
        private string maNhanVien;
        private string maCaLam;

        public DTO_LichLam(string maLichLam, DateTime ngayLam, string maNhanVien, string maCaLam)
        {
            this.MaLichLam = maLichLam;
            this.NgayLam = ngayLam;
            this.maNhanVien = maNhanVien;
            this.MaCaLam = maCaLam;
        }

        public DTO_LichLam(DateTime ngayLam, string maNhanVien, string maCaLam)
        {
            this.MaLichLam = maLichLam;
            this.NgayLam = ngayLam;
            this.MaNhanVien = maNhanVien;
            this.MaCaLam = maCaLam;
        }
        public string MaLichLam { get => maLichLam; set => maLichLam = value; }
        public DateTime NgayLam { get => ngayLam; set => ngayLam = value; }
        public string MaNhanVien { get => maNhanVien; set => maNhanVien = value; }
        public string MaCaLam { get => maCaLam; set => maCaLam = value; }
    }
}

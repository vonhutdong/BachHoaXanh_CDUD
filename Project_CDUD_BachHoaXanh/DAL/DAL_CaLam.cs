using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_CaLam
    {
        private DatabaseAccess da = new DatabaseAccess();
        public DatabaseAccess Da { get => da; set => da = value; }

        public IQueryable LayDSCaLam()
        {
            IQueryable temp = from cl in Da.Db.CaLams
                              select new
                              {
                                  cl.maCaLam,
                                  cl.tenCaLam,
                                  cl.gioBatDau,
                                  cl.gioKetThuc
                              };
            return temp;                  
        }

        //public bool ThemCaLam(DTO_CaLam caLam)
        //{
        //    try
        //    {
        //        if(caLam != null)
        //        {
        //            var query = da.Db.CaLams.OrderByDescending(x => x.maCaLam).FirstOrDefault();
        //            da.Db.CaLams.InsertOnSubmit(new CaLam
        //            {
        //                maCaLam = query != null 
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public bool XoaCaLam(string maCaLam)
        {
            try
            {
                var data = da.Db.CaLams.FirstOrDefault(dt => dt.maCaLam == maCaLam);
                if (data == null) { 
                    return false;
                }
                da.Db.CaLams.DeleteOnSubmit(data);
                da.Db.SubmitChanges();

                //thanh cong
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SuaCaLam(DTO_CaLam caLam)
        {
            try
            {
                var cl = da.Db.CaLams.FirstOrDefault(dt => dt.maCaLam == caLam.MaCaLam);
                if (cl != null)
                {
                    cl.tenCaLam = caLam.TenCaLam;
                    cl.gioBatDau = caLam.GioBatDau;
                    cl.gioBatDau = caLam.GioKetThuc;
                    da.Db.SubmitChanges();

                    return true;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }

        public string TinhGioLam(CaLam calam)
        {
            if (calam != null)
            {
                if (TimeSpan.TryParse(calam.gioBatDau, out TimeSpan start) &&
                    TimeSpan.TryParse(calam.gioKetThuc, out TimeSpan end))
                {
                    // Nếu giờ kết thúc nhỏ hơn giờ bắt đầu, cộng thêm 1 ngày
                    if (end < start)
                    {
                        end = end.Add(TimeSpan.FromDays(1));
                    }

                    TimeSpan timeSpan = end - start;
                    return timeSpan.TotalHours.ToString("0.##"); // làm tròn 2 chữ số
                }
                else
                {
                    return "Định dạng giờ bắt đầu/kết thúc không hợp lệ!";
                }
            }
            else
            {
                return "Ca làm không tồn tại!";
            }
        }


        public CaLam GetCaLamByMaCaLam(string maCaLam)
        {
            try
            {
                var caLam = da.Db.CaLams.SingleOrDefault(cl => cl.maCaLam == maCaLam);
                return caLam;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Lỗi khi lấy ca làm: {ex.Message}");
                return null;
            }
        }
    }
}

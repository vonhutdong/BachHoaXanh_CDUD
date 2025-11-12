using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace Project_CDUD_BachHoaXanh.TrongDev
{
    public partial class Frm_DanhSachPhieuNhap : Form
    {
        public Frm_DanhSachPhieuNhap()
        {
            InitializeComponent();
        }

        private void Frm_DanhSachPhieuNhap_Load(object sender, EventArgs e)
        {
            BUS_PhieuNhap bus = new BUS_PhieuNhap();
            DataTable data = bus.LayDSPhieuNhapVaChiTiet_BaoCao();

            InDanhSachPN rpt = new InDanhSachPN(); // file Crystal Report đã tạo sẵn
            rpt.SetDataSource(data);

            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}

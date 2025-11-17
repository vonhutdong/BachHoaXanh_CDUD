using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CDUD_BachHoaXanh.TrongDev
{
    public partial class Frm_ThongKeSpSapHet : Form
    {
        public Frm_ThongKeSpSapHet()
        {
            InitializeComponent();
        }

        private void Frm_ThongKeSpSapHet_Load(object sender, EventArgs e)
        {
            BUS_KhoHang bus = new BUS_KhoHang();
            DataTable data = bus.LayDSSPSapHet();

            ThongKeSPSapHet rpt = new ThongKeSPSapHet(); // file Crystal Report đã tạo sẵn
            rpt.SetDataSource(data);

            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}

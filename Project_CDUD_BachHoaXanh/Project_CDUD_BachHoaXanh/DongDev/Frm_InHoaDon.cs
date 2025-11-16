using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_InHoaDon : Form
    {
        string maHd = string.Empty;
        public Frm_InHoaDon()
        {
            InitializeComponent();
        }
        public Frm_InHoaDon(string id)
        {
            maHd = id;
            InitializeComponent();
        }

        private void Frm_InHoaDon_Load(object sender, EventArgs e)
        {
            if (maHd != "")
            {
                // Khởi tạo đối tượng rpt
                InHDTheoMa rpt = new InHDTheoMa();

                // Khởi tạo ParameterValues
                ParameterValues para = new ParameterValues();

                // Khởi tạo ParameterDiscreteValue
                ParameterDiscreteValue val = new ParameterDiscreteValue();

                // Gán giá trị cho ParameterDiscreteValue
                val.Value = maHd;

                // Thêm val vào para
                para.Add(val);

                // Định nghĩa biến tham gia cho rpt
                rpt.DataDefinition.ParameterFields["@maHoaDon"].ApplyCurrentValues(para);

                // Gọi rpt
                crystalReportViewer1.ReportSource = rpt;
            }
        }
    }
}

using CrystalDecisions.Shared;
using DAL;
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
    
    public partial class Frm_BaoCaoBangLuong : Form
    {
        private string MaBangLuong;
        public Frm_BaoCaoBangLuong(string MaBangLuong)
        {
            InitializeComponent();
            this.MaBangLuong = MaBangLuong;
        }
        public Frm_BaoCaoBangLuong()
        {
            InitializeComponent();
        }



        private void Frm_BaoCaoBangLuong_Load(object sender, EventArgs e)
        {
            InBaoCaoBangLuong rpt = new InBaoCaoBangLuong();
            //tạo tham số
            ParameterValues paraNam = new ParameterValues();

            ParameterDiscreteValue valNam = new ParameterDiscreteValue();

            valNam.Value = this.MaBangLuong;

            paraNam.Add(valNam);

            rpt.DataDefinition.ParameterFields["@maBangLuong"].ApplyCurrentValues(paraNam);
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}

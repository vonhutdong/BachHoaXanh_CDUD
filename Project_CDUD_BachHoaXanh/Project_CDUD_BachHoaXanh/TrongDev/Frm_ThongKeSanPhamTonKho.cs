using BUS;
using CrystalDecisions.Shared;
using DTO;
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
    public partial class Frm_ThongKeSanPhamTonKho : Form
    {
        public Frm_ThongKeSanPhamTonKho()
        {
            InitializeComponent();
        }
        BUS_ChiNhanh bus = new BUS_ChiNhanh();
        private void LoadTenChiNhanh()
        {
            cboTenChiNhanh.DataSource = bus.LayDSChiNhanh();
            cboTenChiNhanh.DisplayMember = "TenChiNhanh";
            cboTenChiNhanh.ValueMember = "MaChiNhanh";
            cboTenChiNhanh.SelectedIndex = 0;
        }

        private void Frm_ThongKeSanPhamTonKho_Load(object sender, EventArgs e)
        {
            LoadTenChiNhanh();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboTenChiNhanh.Text))
            {
                MessageBox.Show("Vui lòng chọn chi nhánh.");
                return;
            }

            // Khởi tạo report
            InThongKeSanPhamTonKho rpt = new InThongKeSanPhamTonKho();

            // Tạo tham số
            ParameterValues para = new ParameterValues();
            ParameterDiscreteValue val = new ParameterDiscreteValue();
            val.Value = cboTenChiNhanh.Text.Trim(); // ✅ Gán đúng giá trị
            para.Add(val);

            // Gán vào report parameter
            rpt.DataDefinition.ParameterFields["@TenChiNhanh"].ApplyCurrentValues(para);

            // Gán cho CrystalReportViewer
            rpt_ThongKeTonKho.ReportSource = rpt;
        }
    }
}

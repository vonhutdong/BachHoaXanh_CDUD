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
    public partial class Frm_ThongKeTheoMaPhieuNhap : Form
    {
        public Frm_ThongKeTheoMaPhieuNhap()
        {
            InitializeComponent();
        }
        BUS_PhieuNhap bus = new BUS_PhieuNhap();
        private void LoadMaPhieuNhap()
        {
            cboMaPhieuNhap.DataSource = bus.LayDSPhieuNhap();
            cboMaPhieuNhap.DisplayMember = "MaPhieuNhap";
            cboMaPhieuNhap.ValueMember = "MaPhieuNhap";
            cboMaPhieuNhap.SelectedIndex = 0;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboMaPhieuNhap.Text))
            {
                MessageBox.Show("Vui lòng chọn mã phiếu nhập.");
                return;
            }

            // Khởi tạo report
            InThongKeTheoMaPhieuNhap rpt = new InThongKeTheoMaPhieuNhap();

            // Tạo tham số
            ParameterValues para = new ParameterValues();
            ParameterDiscreteValue val = new ParameterDiscreteValue();
            val.Value = cboMaPhieuNhap.Text.Trim(); // ✅ Gán đúng giá trị
            para.Add(val);

            // Gán vào report parameter
            rpt.DataDefinition.ParameterFields["@MaPhieuNhap"].ApplyCurrentValues(para);

            // Gán cho CrystalReportViewer
            rpt_ThongKeTheoMaPhieuNhap.ReportSource = rpt;
        }

        private void Frm_ThongKeTheoMaPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadMaPhieuNhap();
        }
    }
}

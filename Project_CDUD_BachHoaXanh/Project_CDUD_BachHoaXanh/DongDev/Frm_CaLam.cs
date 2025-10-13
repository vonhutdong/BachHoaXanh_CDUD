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
using DTO;

namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_CaLam : Form
    {
        BUS_CaLam bus_cl = new BUS_CaLam();
        DTO_CaLam caLam = new DTO_CaLam();
        int currentID = -1;
        public Frm_CaLam()
        {
            InitializeComponent();
        }
        void loadDATA()
        {
            dgvCaLam.DataSource = bus_cl.LayDSCaLam();

            // Đổi tên cột
            dgvCaLam.Columns["MaCaLam"].HeaderText = "Mã ca làm";
            dgvCaLam.Columns["TenCaLam"].HeaderText = "Tên ca làm";
            dgvCaLam.Columns["GioBatDau"].HeaderText = "Giờ bắt đầu";
            dgvCaLam.Columns["GioKetThuc"].HeaderText = "Giờ kết thúc";

            dgvCaLam.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCaLam.ColumnHeadersHeight = 40; // hoặc cao hơn

            // Thiết lập lại style để dữ liệu hiện rõ
            dgvCaLam.DefaultCellStyle.BackColor = Color.White;
            dgvCaLam.DefaultCellStyle.ForeColor = Color.Black;
            dgvCaLam.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvCaLam.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvCaLam.EnableHeadersVisualStyles = false;
            dgvCaLam.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvCaLam.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        }
        private void Frm_CaLam_Load(object sender, EventArgs e)
        {
            loadDATA();
        }
    }
}

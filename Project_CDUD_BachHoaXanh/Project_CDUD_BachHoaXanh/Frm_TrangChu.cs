using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project_CDUD_BachHoaXanh.DongDev;

namespace Project_CDUD_BachHoaXanh
{
    public partial class Frm_TrangChu : Form
    {
        public Frm_TrangChu()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Frm_BanHang frm_BanHang = new Frm_BanHang();
            frm_BanHang.Show(); 
        }
    }
}

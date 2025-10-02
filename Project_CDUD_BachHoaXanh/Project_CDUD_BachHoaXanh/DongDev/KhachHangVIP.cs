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
    public partial class KhachHangVIP : Form
    {
        public KhachHangVIP()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Frm_NapVip fr = new Frm_NapVip();
            fr.Show();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

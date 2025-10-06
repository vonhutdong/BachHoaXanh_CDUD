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
    public partial class Frm_ChiTieHoaDon : Form
    {
        public Frm_ChiTieHoaDon()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgvChiTietHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            Frm_InHoaDon frm_InHoaDon = new Frm_InHoaDon();
            frm_InHoaDon.Show();
        }
    }
}

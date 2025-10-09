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
    public partial class Frm_BangLuong : Form
    {
        public Frm_BangLuong()
        {
            InitializeComponent();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            Frm_BaoCaoBangLuong frm = new Frm_BaoCaoBangLuong();
            frm.Show();
        }
    }
}

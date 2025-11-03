using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DAL;
using BUS;

namespace Project_CDUD_BachHoaXanh.TrongDev
{
    public partial class Frm_KhoHang : Form
    {
        public Frm_KhoHang()
        {
            InitializeComponent();
        }
        //BUS_SanPham bus_sp = new BUS_SanPham();
        BUS_KhoHang bus_kh = new BUS_KhoHang();
        int currentID = 0;

        private void btnIn_Click(object sender, EventArgs e)
        {
            Frm_ThongKeSanPhamTonKho frm_ThongKeSanPhamTonKho = new Frm_ThongKeSanPhamTonKho();
            frm_ThongKeSanPhamTonKho.Show();
        }
    }
}

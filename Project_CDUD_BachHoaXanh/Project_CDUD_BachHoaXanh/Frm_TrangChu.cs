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


        //private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Frm_NhanViencs f = new Frm_NhanViencs();
        //    f.TopLevel = false;
        //    f.FormBorderStyle = FormBorderStyle.None;
        //    f.Dock = DockStyle.Fill;
        //    tabNhanVien.Controls.Clear();
        //    tabNhanVien.Controls.Add(f);
        //    f.Show();
        //}
        private void LoadForm(Form frm)
        {
            panelMain.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_NhanViencs());
        }

        private void btnLoaiNhanVien_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_LoaiNhanVien());
        }

        private void btnBangLuong_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_BangLuong());
        }


    }
}

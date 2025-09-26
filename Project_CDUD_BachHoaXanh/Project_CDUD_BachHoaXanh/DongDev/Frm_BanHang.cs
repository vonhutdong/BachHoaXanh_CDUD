using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project_CDUD_BachHoaXanh.TrongDev;


namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_BanHang : Form
    {
        public Frm_BanHang()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {
                    }

        private void guna2GroupBox4_Click(object sender, EventArgs e)
        {

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            Frm_ApDungMaKhuyenMai frm = new Frm_ApDungMaKhuyenMai();
            frm.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
                    }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Frm_ApDungMaKhuyenMai frm = new Frm_ApDungMaKhuyenMai();
            frm.Show();
        }

        private void cboPhuongThucTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPT = cboPhuongThucTT.SelectedItem.ToString();

            if (selectedPT == "Tiền mặt")
            {
                // Hiện textbox Tiền khách đưa + Tiền thừa
                txtTienKhachDua.Visible = true;
                lblTienKhachDua.Visible = true;

                txtTienThua.Visible = true;
                lblTienThua.Visible = true;
            }
            else
            {
                // Ẩn textbox khi không phải tiền mặt
                txtTienKhachDua.Visible = false;
                lblTienKhachDua.Visible = false;

                txtTienThua.Visible = false;
                lblTienThua.Visible = false;

                // Reset giá trị
                txtTienKhachDua.Text = "";
                txtTienThua.Text = "";
            }
        }

        private void Frm_BanHang_Load(object sender, EventArgs e)
        {
            cboPhuongThucTT.SelectedIndex = 0;
        }
    }
}

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

namespace Project_CDUD_BachHoaXanh
{
    public partial class Frm_DangNhap : Form
    {
        public Frm_DangNhap()
        {
            InitializeComponent();
        }
        BUS_NhanVien bus_NhanVien = new BUS_NhanVien();
        BUS_TaiKhoan bus_TaiKhoan = new BUS_TaiKhoan();
        BUS_TaiKhoan bus_tk = new BUS_TaiKhoan();
        DTO_NhanVien nvLogin;

    

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            if (bus_tk.CheckTaiKhoan(taiKhoan, matKhau))
            {
                try
                {
                    int quyen = bus_tk.GetRole(taiKhoan, matKhau);
                    string maTK = bus_tk.GetMaTaiKhoan(taiKhoan, matKhau);
                    //MessageBox.Show(quyen.ToString(),maTK);
                    if (string.IsNullOrEmpty(maTK))
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Đăng nhập thất bại",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DTO_NhanVien nvLogin = bus_NhanVien.getNhanVien(maTK);
                    //MessageBox.Show(nvLogin.MaTaiKhoan.ToString());
                    if (nvLogin == null)
                    {
                        MessageBox.Show("Không tìm thấy nhân viên tương ứng với tài khoản này!",
                                        "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DTO_Session.MaNhanVien = nvLogin.MaNV;
                    DTO_Session.TenNhanVien = nvLogin.TenNV;
                    this.Hide(); // Ẩn form đăng nhập

                    Frm_TrangChu f = new Frm_TrangChu(taiKhoan, quyen, nvLogin);

                    f.ShowDialog();

                    this.Show(); // Hiện lại nếu cần quay lại đăng nhập
                }
                finally
                {
                    //this.FormClosing += FrmDangNhap_FormClosing;
                }
            }
            else
            {
                MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Frm_DangNhap_Load(object sender, EventArgs e)
        {
            txtTaiKhoan.Focus();
            txtMatKhau.UseSystemPasswordChar = true;
        }

        private void checkHienMK_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu checkbox được tick thì hiện mật khẩu
            if (checkHienMK.Checked)
                txtMatKhau.UseSystemPasswordChar = false;
            else
                txtMatKhau.UseSystemPasswordChar = true;
        }

        private void Frm_DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn thoát chương trình", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}

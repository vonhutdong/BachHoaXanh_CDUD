using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using DTO;
using Project_CDUD_BachHoaXanh.DongDev;
using Project_CDUD_BachHoaXanh.TrongDev;

namespace Project_CDUD_BachHoaXanh
{
    public partial class Frm_TrangChu : Form
    {
        public static DTO_NhanVien NhanVien = null;
        private string tk = string.Empty;
        private int q = 0;
        private Form frmOld = null;
        public Frm_TrangChu()
        {
            InitializeComponent();
        }
        public static DTO_NhanVien getNhanVien()
        {
            return NhanVien;
        }
        public Frm_TrangChu(string taiKhoan, int quyen, DTO_NhanVien nhanVien)
        {
            this.tk = taiKhoan;
            Frm_TrangChu.NhanVien = nhanVien; // Gọi static property
            this.q = quyen;
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
        private void LoadForm1(Form frm)
        {
            panelHeThong.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panelHeThong.Controls.Add(frm);
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

        private void btnCaLam_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_CaLam());

        }

        private void btnLichLam_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_LichLam());

        }

        private void btnkhoHang_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_KhoHang());

        }

        private void btnLoaiHang_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_LoaiHang());

        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_SanPham());

        }

        private void btnKhuyenMai_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_KhuyenMai());

        }

        private void btnNhaCC_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_NhaCungCap());

        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_KhachHang());

        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_TaiKhoang());

        }

        private void btnChiNhanh_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_ChiNhanh());

        }

        private void btnPhieuNhap_Click(object sender, EventArgs e)
        {
            LoadForm(new Frm_PhieuNhap());

        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            LoadForm1(new Frm_HoaDon());

        }

        

        private void btnBaoCaoBangLuong_Click(object sender, EventArgs e)
        {
            LoadForm1(new Frm_BaoCaoBangLuong());

        }

        private void btnThongKePhieuNhapTheoMa_Click(object sender, EventArgs e)
        {
            LoadForm1(new Frm_ThongKeTheoMaPhieuNhap());

        }

        private void btnThongKeDSPhieuNhap_Click(object sender, EventArgs e)
        {
            LoadForm1(new Frm_DanhSachPhieuNhap());

        }

        private void btnThongKeSPTonKho_Click(object sender, EventArgs e)
        {
            LoadForm1(new Frm_ThongKeSanPhamTonKho());

        }

        private void btnChiTietHoaDon_Click(object sender, EventArgs e)
        {
            LoadForm1(new Frm_ChiTietHoaDonFrm());

        }

        private void tabControlQuanLy_SelectedIndexChanged(object sender, EventArgs e)
        {
            Frm_BanHang f = new Frm_BanHang();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            tabBanHang.Controls.Clear();
            tabBanHang.Controls.Add(f);
            f.Show();
        }

        private void Frm_TrangChu_Load(object sender, EventArgs e)
        {
            //Frm_DangNhap f = new Frm_DangNhap();
            //f.TopLevel = false;
            //f.FormBorderStyle = FormBorderStyle.None;
            //f.Dock = DockStyle.Top;
            //tabHeThong.Controls.Clear();
            //tabHeThong.Controls.Add(f);
            //f.Show();
            //tabHeThong.Focus();

        }

        private void btnVip_Click(object sender, EventArgs e)
        {
            LoadForm(new KhachHangVIP());
        }

        private void tabHeThong_Click(object sender, EventArgs e)
        {

        }
    }
}

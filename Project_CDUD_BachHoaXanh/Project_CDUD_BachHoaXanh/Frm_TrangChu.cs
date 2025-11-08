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
using Project_CDUD_BachHoaXanh.TrongDev;
using DAL;
using DTO;

namespace Project_CDUD_BachHoaXanh
{
    public partial class Frm_TrangChu : Form
    {
        private List<TabPage> allTabs = new List<TabPage>();
        
        public static DTO_NhanVien nhanVien = null;
        public Frm_TrangChu()
        {
            InitializeComponent();
        }
        private string tk = string.Empty;
        private int q = 0;
        private Form frmOld = null;
        public static DTO_NhanVien NhanVien = null;
        public Frm_TrangChu(string taiKhoan, int quyen, DTO_NhanVien nhanVien)
        {
            InitializeComponent();
            this.tk = taiKhoan;
            Frm_TrangChu.NhanVien = nhanVien; // Gọi static property
            this.q = quyen;
          
        }

        public static DTO_NhanVien getNhanVien()
        {
            return NhanVien;
        }
        private void Frm_TrangChu_Load(object sender, EventArgs e)
        {
            // Lưu danh sách tất cả tab (chỉ làm 1 lần)
            if (allTabs.Count == 0)
            {
                allTabs.Add(tabHeThong);
                allTabs.Add(tabBanHang);
                allTabs.Add(tabQuanLy);
                allTabs.Add(tabThongKe);
            }

            if (NhanVien != null)
                this.Text = $"Màn hình chính - Xin chào {NhanVien.TenNV}";

            // Phân quyền
            tabControlQuanLy.TabPages.Clear();

            if (q == 1) // Nhân viên
            {
                // Hiện Hệ thống, Bán hàng, Thống kê
                tabControlQuanLy.TabPages.Add(tabHeThong);
                tabControlQuanLy.TabPages.Add(tabBanHang);
                tabControlQuanLy.TabPages.Add(tabThongKe);
                btnHoaDon.Visible = false;
            }
            else if (q == 0) // Admin
            {
                // Hiện tất cả tab
                foreach (var t in allTabs)
                    tabControlQuanLy.TabPages.Add(t);
            }

            // Gọi lại form Bán hàng nếu cần
            LoadBanHangForm();

        }
        private void LoadBanHangForm()
        {
            tabBanHang.Controls.Clear();
            Frm_BanHang f = new Frm_BanHang();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            tabBanHang.Controls.Add(f);
            f.Show();
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

       

        private void btnVip_Click(object sender, EventArgs e)
        {
            LoadForm(new KhachHangVIP());
        }

        private void tabHeThong_Click(object sender, EventArgs e)
        {

        }
    }
}

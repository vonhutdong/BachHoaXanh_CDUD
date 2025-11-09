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
        private string maSPCu;
        private string maCNCu;
        private void LoadKhoHang()
        {
            txtSoLuong.Enabled = false;
            cboChiNhanh.Enabled = false;
            cbTenSP.Enabled = false;
            dgvKhoHang.DataSource = bus_kh.LoadKhoHang();

            // Đổi header
            dgvKhoHang.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvKhoHang.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            dgvKhoHang.Columns["TenChiNhanh"].HeaderText = "Tên Chi Nhánh";
            // Ẩn cột ID và idSanPham
            dgvKhoHang.Columns["id"].Visible = false;
            dgvKhoHang.Columns["MaSanPham"].Visible = false;
            dgvKhoHang.Columns["MaChiNhanh"].Visible = false;

            // --- Thiết lập kích thước & hiển thị ---
            dgvKhoHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvKhoHang.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvKhoHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvKhoHang.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvKhoHang.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvKhoHang.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvKhoHang.DefaultCellStyle.BackColor = Color.White;
            dgvKhoHang.DefaultCellStyle.ForeColor = Color.Black;
            dgvKhoHang.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvKhoHang.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvKhoHang.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvKhoHang.EnableHeadersVisualStyles = false;
            dgvKhoHang.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvKhoHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvKhoHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvKhoHang.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvKhoHang.AllowUserToResizeColumns = false;

            cbTenSP.DataSource = bus_kh.LayDSSP();
            cbTenSP.DisplayMember = "TenSanPham";
            cbTenSP.ValueMember = "MaSanPham";
            cbTenSP.SelectedIndex = -1;

            cboChiNhanh.DataSource = bus_kh.LayDSCN();
            cboChiNhanh.DisplayMember = "TenChiNhanh";
            cboChiNhanh.ValueMember = "MaChiNhanh";
            cboChiNhanh.SelectedIndex = -1;

            cboTimSP.DataSource = bus_kh.LayDSSP();
            cboTimSP.DisplayMember = "TenSanPham";
            cboTimSP.ValueMember = "MaSanPham";
            cboTimSP.SelectedIndex = -1;
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            Frm_ThongKeSanPhamTonKho frm_ThongKeSanPhamTonKho = new Frm_ThongKeSanPhamTonKho();
            frm_ThongKeSanPhamTonKho.Show();
        }

        private void Frm_KhoHang_Load(object sender, EventArgs e)
        {
            LoadKhoHang();
        }



        private void dgvKhoHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhoHang.Rows[e.RowIndex];

                // ✅ Lấy ID
                currentID = Convert.ToInt32(row.Cells["id"].Value);

                // ✅ Gán dữ liệu ra control
                cbTenSP.SelectedValue = row.Cells["MaSanPham"].Value.ToString();
                txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString();
                cboChiNhanh.SelectedValue = row.Cells["MaChiNhanh"].Value.ToString();

                // ✅ Gán giá trị cũ để sửa
                maSPCu = row.Cells["MaSanPham"].Value.ToString();
                maCNCu = row.Cells["MaChiNhanh"].Value.ToString();
            }
        }
        private void reset()
        {
            currentID = 0;
            txtSoLuong.Clear();
            cbTenSP.SelectedIndex = -1;
            cbTenSP.Text = "";           // 🔥 Thêm dòng này
            cboChiNhanh.SelectedIndex = -1;
            cboChiNhanh.Text = "";       // 🔥 Thêm dòng này
            maSPCu = null;
            maCNCu = null;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            //string tuKhoa = cboTimSP.Text.Trim();

            //try
            //{
            //    var ketQua = bus_kh.TimKiemSanPham(tuKhoa);

            //    if (ketQua != null && ketQua.Any())
            //    {
            //        dgvKhoHang.DataSource = ketQua.ToList();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Không tìm thấy sản phẩm phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        dgvKhoHang.DataSource = null; // Xóa dữ liệu cũ
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}

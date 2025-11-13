using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CDUD_BachHoaXanh.TrongDev
{
    public partial class Frm_KhachHang : Form
    {
        public Frm_KhachHang()
        {
            InitializeComponent();
            txtCapBac.Enabled = false;
        }
        BUS_KhachHang bus_kh = new BUS_KhachHang();
        private string currentMaKh = "";

        private void Frm_KhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
           
            //load data
            dgvKhachHang.DataSource = bus_kh.LayDSKH();
            //đổi tên cột
            dgvKhachHang.Columns["MaKH"].HeaderText = "Mã khách hàng";
            dgvKhachHang.Columns["TenKH"].HeaderText = "Tên khách hàng";
            dgvKhachHang.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvKhachHang.Columns["Diem"].HeaderText = "Điểm";
            dgvKhachHang.Columns["CapBac"].HeaderText = "Cấp Bậc";
            // --- Thiết lập kích thước & hiển thị ---
            dgvKhachHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvKhachHang.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvKhachHang.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvKhachHang.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvKhachHang.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvKhachHang.DefaultCellStyle.BackColor = Color.White;
            dgvKhachHang.DefaultCellStyle.ForeColor = Color.Black;
            dgvKhachHang.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvKhachHang.EnableHeadersVisualStyles = false;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvKhachHang.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvKhachHang.AllowUserToResizeColumns = false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DTO_KhachHang kh = new DTO_KhachHang
                {
                    TenKH = txtTenKH.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    CapBac = txtCapBac.Text.Trim(),
                    Diem = string.IsNullOrWhiteSpace(txtDiemTichLuy.Text) ? 0 : float.Parse(txtDiemTichLuy.Text.Trim())
                };

                bus_kh.ThemKhachHang(kh);
                MessageBox.Show("Thêm khách hàng thành công!");
                LoadData();
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiemKH.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            var result = bus_kh.TimKiemTheoTenHoacSDT(keyword).ToList();
            dgvKhachHang.DataSource = result;
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // đảm bảo click vào dòng hợp lệ, không phải header
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                // Lưu lại mã khách hàng đang chọn
                currentMaKh = row.Cells["MaKH"].Value?.ToString();

                // Hiển thị lên các textbox
                txtTenKH.Text = row.Cells["TenKH"].Value?.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value?.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
                txtDiemTichLuy.Text = row.Cells["Diem"].Value?.ToString();
                txtCapBac.Text = row.Cells["CapBac"].Value?.ToString();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentMaKh))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo DTO từ dữ liệu form
                DTO_KhachHang kh = new DTO_KhachHang
                {
                    MaKH = currentMaKh,
                    TenKH = txtTenKH.Text.Trim(),
                    SoDienThoai = txtSDT.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    Diem = float.TryParse(txtDiemTichLuy.Text.Trim(), out float diem) ? diem : 0
                };

                // Gọi BUS để sửa
                bool result = bus_kh.SuaKhachHang(kh);

                if (result)
                {
                    MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Hàm load lại danh sách khách hàng
                    currentMaKh = ""; // reset
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // ⚠️ Kiểm tra có chọn dòng nào chưa
            if (string.IsNullOrEmpty(currentMaKh))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ⚠️ Xác nhận người dùng có chắc muốn xóa không
            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?",
                                                    "Xác nhận xóa",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
            if (confirm == DialogResult.No)
                return;

            try
            {
                // Gọi BLL để xóa
                bool ketQua = bus_kh.XoaKH(currentMaKh);

                if (ketQua)
                {
                    MessageBox.Show("Đã xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại danh sách
                    LoadData();

                    // Xóa trắng form nhập
                    LamMoi();

                    // Reset biến mã hiện tại
                    currentMaKh = "";
                }
                else
                {
                    MessageBox.Show("Không thể xóa khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LamMoi()
        {
            txtTenKH.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtDiemTichLuy.Text = string.Empty;
            txtTimKiemKH.Text = string.Empty;
            txtCapBac.Text = string.Empty;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GroupBox6_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTimKiemKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCapBac_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtDiemTichLuy_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

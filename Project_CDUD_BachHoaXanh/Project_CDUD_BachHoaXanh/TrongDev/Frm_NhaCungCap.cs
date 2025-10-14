using BUS;
using DAL;
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
    public partial class Frm_NhaCungCap : Form
    {
        BUS_NhaCungCap bus_ncc = new BUS_NhaCungCap();
        string data_olds = string.Empty;
        string data_news = string.Empty;
        public Frm_NhaCungCap()
        {
            InitializeComponent();
        }

        private void Frm_NhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            txtTenNCC.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            dgvNCC.DataSource = bus_ncc.LayDSNCC();
            dgvNCC.Columns["MaNCC"].HeaderText = "Mã nhà cung cấp";
            dgvNCC.Columns["TenNCC"].HeaderText = "Tên nhà cung cấp";
            dgvNCC.Columns["SDT"].HeaderText = "Số điện thoại";
            dgvNCC.Columns["DiaChi"].HeaderText = "Địa chỉ";

            // --- Thiết lập kích thước & hiển thị ---
            dgvNCC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvNCC.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvNCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvNCC.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvNCC.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvNCC.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvNCC.DefaultCellStyle.BackColor = Color.White;
            dgvNCC.DefaultCellStyle.ForeColor = Color.Black;
            dgvNCC.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvNCC.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvNCC.EnableHeadersVisualStyles = false;
            dgvNCC.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvNCC.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvNCC.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvNCC.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvNCC.AllowUserToResizeColumns = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu nhập từ form
                string ten = txtTenNCC.Text.Trim();
                string sdt = txtSDT.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();

                // Tạo đối tượng DTO
                DTO_NhaCungCap ncc = new DTO_NhaCungCap
                {
                    TenNhaCungCap = ten,
                    SoDienThoai = sdt,
                    DiaChi = diaChi
                };

                // Gọi BUS để thêm NCC (DAL sẽ tự kiểm tra ràng buộc)
                bus_ncc.ThemNCC(ncc);

                MessageBox.Show("Thêm nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới giao diện
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thông báo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                DataGridViewRow row = dgvNCC.Rows[index];

                // Lấy tên khuyến mãi
                txtTenNCC.Text = row.Cells["TenNCC"].Value?.ToString() ?? string.Empty;
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? string.Empty;
                txtSDT.Text = row.Cells["SDT"].Value?.ToString() ?? string.Empty;

                // Cập nhật trạng thái nút
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void clear()
        {
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();

            // Đặt lại trạng thái nút
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã khuyến mãi từ dòng đang chọn
                if (dgvNCC.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string maNhaCungCap = dgvNCC.CurrentRow.Cells["MaNCC"].Value.ToString();
                // Xác nhận xóa
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác nhận xóa",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.No)
                    return;
                // Gọi BUS để xóa khuyến mãi
                bus_ncc.DeleteNCC(maNhaCungCap);
                MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo",
                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Làm mới form
                LoadData();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Giả sử bạn chọn dòng trong DataGridView để sửa
                if (dgvNCC.CurrentRow != null)
                {
                    string maNCC = dgvNCC.CurrentRow.Cells["MaNCC"].Value.ToString();

                    // Tạo DTO từ dữ liệu form
                    DTO_NhaCungCap ncc = new DTO_NhaCungCap
                    {
                        MaNhaCungCap = maNCC, // lấy từ dòng đang chọn
                        TenNhaCungCap = txtTenNCC.Text.Trim(),
                        SoDienThoai = txtSDT.Text.Trim(),
                        DiaChi = txtDiaChi.Text.Trim()
                    };

                    // Gọi BUS cập nhật
                    bus_ncc.SuaNCC(ncc);

                    MessageBox.Show("Sửa nhà cung cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    clear();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thông Báo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

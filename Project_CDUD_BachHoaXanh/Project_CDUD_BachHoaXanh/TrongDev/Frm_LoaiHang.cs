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
using DAL;
using DTO;

namespace Project_CDUD_BachHoaXanh.TrongDev
{
    public partial class Frm_LoaiHang : Form
    {
        public Frm_LoaiHang()
        {
            InitializeComponent();
        }
        BUS_LoaiHang bus_lh = new BUS_LoaiHang();
        private string currentMaLH = "";
        public void LoadDSLoaiHang()
        {
            dgvLoaiHang.DataSource = bus_lh.LayDSLH();

            dgvLoaiHang.Columns["MaLoaiHang"].HeaderText = "Mã Loại Hàng";
            dgvLoaiHang.Columns["TenLoaiHang"].HeaderText = "Tên Loại Hàng";

            // --- Thiết lập kích thước & hiển thị ---
            dgvLoaiHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvLoaiHang.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvLoaiHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvLoaiHang.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvLoaiHang.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvLoaiHang.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvLoaiHang.DefaultCellStyle.BackColor = Color.White;
            dgvLoaiHang.DefaultCellStyle.ForeColor = Color.Black;
            dgvLoaiHang.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvLoaiHang.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvLoaiHang.EnableHeadersVisualStyles = false;
            dgvLoaiHang.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvLoaiHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvLoaiHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvLoaiHang.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvLoaiHang.AllowUserToResizeColumns = false;
        }

        private void Frm_LoaiHang_Load(object sender, EventArgs e)
        {
            LoadDSLoaiHang();
        }

        private void dgvLoaiHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentMaLH = dgvLoaiHang.Rows[e.RowIndex].Cells["MaLoaiHang"].Value.ToString();
                txtMaLoaiHang.Text = currentMaLH;
                txtTenLoaiHang.Text = dgvLoaiHang.Rows[e.RowIndex].Cells["TenLoaiHang"].Value.ToString();
                
            }

            txtMaLoaiHang.Enabled = false;

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo DTO và gửi đi
                DTO_LoaiHang loaiHang = new DTO_LoaiHang(
                    txtMaLoaiHang.Text.Trim(),
                    txtTenLoaiHang.Text.Trim()
                );

                // Gọi BUS để thêm
                bus_lh.ThemLoaiHang(loaiHang);

                MessageBox.Show("Thêm loại hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới form
                LoadDSLoaiHang();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thông Báo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Clear()
        {
            txtMaLoaiHang.Enabled = true;
            txtMaLoaiHang.Text = "";
            txtTenLoaiHang.Text = "";
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaLoaiHang.Focus();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(currentMaLH))
            {
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa loại hàng {currentMaLH} không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bus_lh.XoaLoaiHang(currentMaLH);
                        MessageBox.Show(" Xóa loại hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDSLoaiHang();
                        Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(" Vui lòng chọn chi nhánh cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                //  Không cho sửa mã chi nhánh
                if (!txtMaLoaiHang.Enabled)
                {
                    // Mã bị khóa (chỉ đọc) → không cần kiểm tra
                }
                else
                {
                    MessageBox.Show("Không được phép thay đổi MÃ loại hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn sửa loại hàng này không?",
                    "Xác nhận sửa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.No)
                    return;

                //  Gửi dữ liệu qua BUS để xử lý
                DTO_LoaiHang lh = new DTO_LoaiHang
                {
                    MaLoaiHang = txtMaLoaiHang.Text.Trim(),
                    TenLoaiHang = txtTenLoaiHang.Text.Trim(),
                    
                };

                // Gọi BUS (đã kiểm tra trong DAL)
                bus_lh.SuaLoaiHang(lh);

                MessageBox.Show(" Sửa loại hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới danh sách
                LoadDSLoaiHang();
                Clear();
                // Bật lại ô mã để thêm mới
                txtMaLoaiHang.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thông Báo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

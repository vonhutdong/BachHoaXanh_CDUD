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
    public partial class Frm_KhuyenMai : Form
    {
        public Frm_KhuyenMai()
        {
            InitializeComponent();
        }
        BUS_KhuyenMai bus_km = new BUS_KhuyenMai();
        string data_olds = string.Empty;
        string data_news = string.Empty;

        private void Frm_KhuyenMai_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void reset()
        {
            txtTenKhuyenMai.Focus();
            txtTenKhuyenMai.Clear();
            txtGiaTri.Clear();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            data_olds = string.Empty;
            data_news = string.Empty;
        }

        public void LoadData()
        {
            // Others
            txtTenKhuyenMai.Focus();

            txtTenKhuyenMai.Text = string.Empty;
            txtGiaTri.Text = "";
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            data_olds = string.Empty;
            data_news = string.Empty;

            // dgvKM
            dgvKhuyenMai.DataSource = bus_km.GetListKM();
            dgvKhuyenMai.Columns["MaKhuyenMai"].Visible = true;
            dgvKhuyenMai.Columns["MaKhuyenMai"].HeaderText = "Mã khuyến mãi";
            dgvKhuyenMai.Columns["TenKhuyenMai"].HeaderText = "Tên khuyến mãi";
            dgvKhuyenMai.Columns["GiaTri"].HeaderText = "Giá trị";
            // --- Thiết lập kích thước & hiển thị ---
            dgvKhuyenMai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvKhuyenMai.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvKhuyenMai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvKhuyenMai.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvKhuyenMai.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvKhuyenMai.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvKhuyenMai.DefaultCellStyle.BackColor = Color.White;
            dgvKhuyenMai.DefaultCellStyle.ForeColor = Color.Black;
            dgvKhuyenMai.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvKhuyenMai.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvKhuyenMai.EnableHeadersVisualStyles = false;
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvKhuyenMai.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvKhuyenMai.AllowUserToResizeColumns = false;
        }

        private void dgvKhuyenMai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                DataGridViewRow row = dgvKhuyenMai.Rows[index];

                // Lấy tên khuyến mãi
                txtTenKhuyenMai.Text = row.Cells["TenKhuyenMai"].Value?.ToString() ?? string.Empty;

                // Lấy giá trị khuyến mãi (xử lý nếu null hoặc không phải số)
                var giaTriValue = row.Cells["GiaTri"].Value;
                if (giaTriValue != null && double.TryParse(giaTriValue.ToString(), out double giaTri))
                {
                    txtGiaTri.Text = giaTri.ToString("0.##"); // định dạng 2 chữ số thập phân
                }
                else
                {
                    txtGiaTri.Text = "0";
                }

                // Cập nhật trạng thái nút
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                // --- Lấy dữ liệu từ form ---
                string tenKhuyenMai = txtTenKhuyenMai.Text.Trim();
                string giaTriText = txtGiaTri.Text.Trim();

                // --- Kiểm tra tên khuyến mãi ---
                if (string.IsNullOrWhiteSpace(tenKhuyenMai))
                {
                    MessageBox.Show("Tên khuyến mãi không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenKhuyenMai.Focus();
                    return;
                }

                if (tenKhuyenMai.Length > 30)
                {
                    MessageBox.Show("Tên khuyến mãi không được vượt quá 30 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenKhuyenMai.Focus();
                    return;
                }
                // Kiểm tra định dạng tên: cho phép chữ (có dấu), số và khoảng trắng đơn giữa các từ
                if (!System.Text.RegularExpressions.Regex.IsMatch(tenKhuyenMai, @"^[A-Za-zÀ-ỹ0-9%]+(?:\s[A-Za-zÀ-ỹ0-9%]+)*$"))
                {
                    MessageBox.Show("Tên khuyến mãi không hợp lệ!!!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenKhuyenMai.Focus();
                    return;
                }

                // --- Kiểm tra giá trị khuyến mãi ---
                if (string.IsNullOrWhiteSpace(giaTriText))
                {
                    MessageBox.Show("Giá trị khuyến mãi không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGiaTri.Focus();
                    return;
                }

                if (giaTriText.Length > 10)
                {
                    MessageBox.Show("Giá trị khuyến mãi không được vượt quá 10 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGiaTri.Focus();
                    return;
                }

                if (!double.TryParse(giaTriText, out double giaTri))
                {
                    MessageBox.Show("Giá trị khuyến mãi không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGiaTri.Focus();
                    return;
                }

                if (giaTri <= 0)
                {
                    MessageBox.Show("Giá trị khuyến mãi phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGiaTri.Focus();
                    return;
                }

                // --- Tạo đối tượng DTO ---
                DTO_KhuyenMai newKM = new DTO_KhuyenMai
                {
                    TenKhuyenMai = tenKhuyenMai,
                    GiaTri = giaTri
                };

                // --- Gọi BUS thêm khuyến mãi ---
                bus_km.AdddKhuyenMai(newKM);

                MessageBox.Show("Thêm khuyến mãi thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // --- Làm mới form ---
                LoadData();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khuyến mãi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // --- Lấy dữ liệu từ form ---
                string tenKhuyenMai = txtTenKhuyenMai.Text.Trim();
                string giaTriText = txtGiaTri.Text.Trim();

                // --- Kiểm tra tên khuyến mãi ---
                if (string.IsNullOrWhiteSpace(tenKhuyenMai))
                {
                    MessageBox.Show("Tên khuyến mãi không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenKhuyenMai.Focus();
                    return;
                }
                if (tenKhuyenMai.Length > 30)
                {
                    MessageBox.Show("Tên khuyến mãi không được vượt quá 30 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenKhuyenMai.Focus();
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(tenKhuyenMai, @"^[A-Za-zÀ-ỹ0-9%]+(?:\s[A-Za-zÀ-ỹ0-9%]+)*$"))
                {
                    MessageBox.Show("Tên khuyến mãi không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenKhuyenMai.Focus();
                    return;
                }

                // --- Kiểm tra giá trị khuyến mãi ---
                if (string.IsNullOrWhiteSpace(giaTriText))
                {
                    MessageBox.Show("Giá trị khuyến mãi không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGiaTri.Focus();
                    return;
                }
                if (giaTriText.Length > 10)
                {
                    MessageBox.Show("Giá trị khuyến mãi không được vượt quá 10 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGiaTri.Focus();
                    return;
                }
                if (!double.TryParse(giaTriText, out double giaTri))
                {
                    MessageBox.Show("Giá trị khuyến mãi phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGiaTri.Focus();
                    return;
                }
                if (giaTri <= 0)
                {
                    MessageBox.Show("Giá trị khuyến mãi phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGiaTri.Focus();
                    return;
                }

                // --- Lấy mã khuyến mãi từ dòng đang chọn ---
                if (dgvKhuyenMai.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một khuyến mãi để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string maKhuyenMai = dgvKhuyenMai.CurrentRow.Cells["MaKhuyenMai"].Value.ToString();

                // --- Hỏi xác nhận người dùng ---
                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn sửa khuyến mãi này không?",
                    "Xác nhận sửa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm == DialogResult.No)
                    return;

                // --- Tạo đối tượng DTO ---
                DTO_KhuyenMai km = new DTO_KhuyenMai
                {
                    MaKhuyenMai = maKhuyenMai,
                    TenKhuyenMai = tenKhuyenMai,
                    GiaTri = giaTri
                };

                // --- Gọi BUS xử lý cập nhật ---
                bus_km.UpdateKhuyenMai(km);

                MessageBox.Show("Cập nhật khuyến mãi thành công!", "Thông báo",
                         MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa khuyến mãi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã khuyến mãi từ dòng đang chọn
                if (dgvKhuyenMai.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một khuyến mãi để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string maKhuyenMai = dgvKhuyenMai.CurrentRow.Cells["MaKhuyenMai"].Value.ToString();
                // Xác nhận xóa
                DialogResult result = MessageBox.Show(
                         $"Bạn có chắc muốn xóa khuyến mãi {maKhuyenMai} không?",
                         "Xác nhận",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question
                     );
                if (result == DialogResult.No)
                    return;
                // Gọi BUS để xóa khuyến mãi
                bus_km.DeleteKhuyenMai(maKhuyenMai);
                MessageBox.Show("Xóa khuyến mãi thành công!", "Thông báo",
                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Làm mới form
                LoadData();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khuyến mãi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}

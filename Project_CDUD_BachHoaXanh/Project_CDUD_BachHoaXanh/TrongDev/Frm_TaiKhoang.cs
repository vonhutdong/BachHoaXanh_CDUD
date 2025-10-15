using BUS;
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

namespace Project_CDUD_BachHoaXanh.TrongDev
{
    public partial class Frm_TaiKhoang : Form
    {

        BUS_TaiKhoan bus_tk = new BUS_TaiKhoan();
        int quyen = -1;
        public Frm_TaiKhoang()
        {
            InitializeComponent();
        }

        private void Frm_TaiKhoang_Load(object sender, EventArgs e)
        {
            
            // Load dữ liệu lên dgv
            LoadData();
            // Làm sạch combobox
            cboQuyen.Items.Clear();
            // Lựa chọn combobox
            cboQuyen.Items.Add("Admin");
            cboQuyen.Items.Add("Nhân viên");
            // Mặc định
            cboQuyen.SelectedIndex = -1;
        }

        public void LoadData()
        {
            //
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            dgvDanhSachTaiKhoan.DataSource = bus_tk.GetListTaiKhoan();
            dgvDanhSachTaiKhoan.Columns["MaTaiKhoan"].Visible = true;
            dgvDanhSachTaiKhoan.Columns[0].HeaderText = "Mã tài khoản";
            dgvDanhSachTaiKhoan.Columns[1].HeaderText = "Tên tài khoản";
            dgvDanhSachTaiKhoan.Columns[2].HeaderText = "Mật khẩu";
            dgvDanhSachTaiKhoan.Columns[3].HeaderText = "Quyền";
            // --- Thiết lập kích thước & hiển thị ---
            dgvDanhSachTaiKhoan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvDanhSachTaiKhoan.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvDanhSachTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvDanhSachTaiKhoan.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvDanhSachTaiKhoan.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvDanhSachTaiKhoan.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvDanhSachTaiKhoan.DefaultCellStyle.BackColor = Color.White;
            dgvDanhSachTaiKhoan.DefaultCellStyle.ForeColor = Color.Black;
            dgvDanhSachTaiKhoan.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvDanhSachTaiKhoan.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvDanhSachTaiKhoan.EnableHeadersVisualStyles = false;
            dgvDanhSachTaiKhoan.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvDanhSachTaiKhoan.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvDanhSachTaiKhoan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvDanhSachTaiKhoan.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvDanhSachTaiKhoan.AllowUserToResizeColumns = false;

        }

        private void dgvDanhSachTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                var row = dgvDanhSachTaiKhoan.Rows[index];

                txtTenTaiKhoan.Text = row.Cells["TenTaiKhoan"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                cboQuyen.SelectedIndex = (int)row.Cells["Quyen"].Value == 0 ? 0 : 1;

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }
        private void clear()
        {
            txtTenTaiKhoan.Clear();
            txtMatKhau.Clear();
            cboQuyen.SelectedIndex = -1;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // --- Lấy dữ liệu từ form ---
                string tenTK = txtTenTaiKhoan.Text.Trim();
                string matKhau = txtMatKhau.Text.Trim();

                // --- Kiểm tra rỗng ---
                if (string.IsNullOrWhiteSpace(tenTK) || string.IsNullOrWhiteSpace(matKhau))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin tài khoản.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --- Kiểm tra quyền ---
                if (cboQuyen.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn quyền cho tài khoản!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                // --- Tạo DTO ---
                int quyen = cboQuyen.SelectedIndex;
                DTO_TaiKhoan newTaiKhoan = new DTO_TaiKhoan
                {
                    TenTaiKhoan = tenTK,
                    MatKhau = matKhau,
                    Quyen = quyen
                };

                // --- Gọi BUS ---
                bus_tk.AddTaiKhoan(newTaiKhoan);

                MessageBox.Show("Thêm tài khoản thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // --- Làm mới ---
                LoadData();
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDanhSachTaiKhoan.CurrentRow != null)
                {
                    // Lấy mã tài khoản từ dòng được chọn
                    string maTK = dgvDanhSachTaiKhoan.CurrentRow.Cells["MaTaiKhoan"].Value.ToString();
                    string tenTK = txtTenTaiKhoan.Text.Trim();
                    string matKhau = txtMatKhau.Text.Trim();
                    int quyen = cboQuyen.SelectedIndex;

                    // Kiểm tra nhập trống
                    if (string.IsNullOrWhiteSpace(tenTK) || string.IsNullOrWhiteSpace(matKhau))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Xác nhận người dùng có muốn sửa không
                    DialogResult confirm = MessageBox.Show(
                        "Bạn có chắc muốn sửa tài khoản này không?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirm == DialogResult.No)
                        return;

                    // Tạo đối tượng DTO để gửi qua BUS
                    DTO_TaiKhoan tk = new DTO_TaiKhoan
                    {
                        MaTaiKhoan = maTK,
                        TenTaiKhoan = tenTK,
                        MatKhau = matKhau,
                        Quyen = quyen
                    };

                    // Gọi BUS để cập nhật
                    bus_tk.UpdateTaiKhoan(tk);

                    MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDanhSachTaiKhoan.CurrentRow != null)
                {
                    // Lấy mã tài khoản từ dòng được chọn
                    string maTK = dgvDanhSachTaiKhoan.CurrentRow.Cells["MaTaiKhoan"].Value.ToString();

                    // Hỏi xác nhận người dùng
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc muốn xóa tài khoản {maTK} không?",
                        "Xác nhận",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        // Gọi BUS để xóa theo mã
                        bus_tk.DeleteTaiKhoan(maTK);

                        MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Tải lại dữ liệu
                        LoadData();
                        clear();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

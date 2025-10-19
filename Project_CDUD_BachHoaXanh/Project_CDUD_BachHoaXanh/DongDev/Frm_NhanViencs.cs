using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using BUS;
using DTO;


namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_NhanViencs : Form
    {
        BUS_NhanVien bus_nv = new BUS_NhanVien();
        BUS_ChucVu bus_cv = new BUS_ChucVu();
        BUS_ChiNhanh bus_cn = new BUS_ChiNhanh();
        BUS_TaiKhoan bus_tk = new BUS_TaiKhoan();
        DataValidation dv = new DataValidation();
        public Frm_NhanViencs()
        {
            InitializeComponent();
        }


        void loadData()
        {
            txtTenNhanVien.Focus();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtTenNhanVien.Text = string.Empty;
            txtSoDienThoai.Text = string.Empty;
            txtDiaChi.Text = string.Empty;

            dgvNV.DataSource = bus_nv.LayDSNhanVien();

            dgvNV.Columns["MaNhanVien"].Visible = true;
            dgvNV.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
            dgvNV.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
            dgvNV.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            dgvNV.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvNV.Columns["LoaiNhanVien"].HeaderText = "Loại nhân viên";
            dgvNV.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvNV.Columns["TenChiNhanh"].HeaderText = "Tên chi nhánh";
            dgvNV.Columns["TenChucVu"].HeaderText = "chức vụ";
            dgvNV.Columns["TenTaiKhoan"].HeaderText = "Tên tài khoản";


            cboChiNhanh.DataSource = bus_cn.LayDSChiNhanh();
            cboChiNhanh.DisplayMember = "TenChiNhanh";
            cboChiNhanh.ValueMember = "MaChiNhanh";

            cboTenTK.DataSource = bus_tk.GetListTaiKhoan();
            cboTenTK.DisplayMember = "TenTaiKhoan";
            cboTenTK.ValueMember = "MaTaiKhoan";

            cboChucVu.DataSource = bus_nv.LayDSChucVu();
            cboChucVu.DisplayMember = "TenChucVu";
            cboChucVu.ValueMember = "MaChucVu";

            string[] arr = { "FullTime", "PartTime" };
            cboLoaiNhanVien.DataSource = arr;

            dgvNV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNV.ColumnHeadersHeight = 30; // hoặc cao hơn

            dgvNV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvNV.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // -    -- Tăng chiều cao hàng ---
            dgvNV.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvNV.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvNV.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvNV.DefaultCellStyle.BackColor = Color.White;
            dgvNV.DefaultCellStyle.ForeColor = Color.Black;
            dgvNV.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvNV.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvNV.EnableHeadersVisualStyles = false;
            dgvNV.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvNV.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvNV.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvNV.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }
        private void Frm_NhanViencs_Load(object sender, EventArgs e)
        {
            loadData();
            dgvNV.ContextMenuStrip = contextMenuStrip1;
            cboTim.SelectedIndex = 0;
        }

        private void dgvNV_Click(object sender, EventArgs e)
        {
            if (dgvNV.CurrentCell != null)
            {
                // Others
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                // Get row index selected
                int n = dgvNV.CurrentCell.RowIndex;

                txtTenNhanVien.Text = dgvNV.Rows[n].Cells[1].Value.ToString();
                txtSoDienThoai.Text = dgvNV.Rows[n].Cells[2].Value.ToString();
                txtDiaChi.Text = dgvNV.Rows[n].Cells[3].Value.ToString();

                string loaiNV = dgvNV.Rows[n].Cells[4].Value?.ToString();
                if (!string.IsNullOrEmpty(loaiNV))
                {
                    cboLoaiNhanVien.SelectedItem = loaiNV;
                }
                else
                {
                    cboLoaiNhanVien.SelectedIndex = -1;
                }

                int n1 = dgvNV.CurrentRow.Index;

                try
                {

                    var tenChucVu = dgvNV.Rows[n1].Cells["TenChucVu"].Value?.ToString();
                    if (!string.IsNullOrEmpty(tenChucVu))
                        cboChucVu.Text = tenChucVu;
                    else
                        cboChucVu.SelectedIndex = -1;


                    var tenChiNhanh = dgvNV.Rows[n1].Cells["TenChiNhanh"].Value?.ToString();
                    if (!string.IsNullOrEmpty(tenChiNhanh))
                        cboChiNhanh.Text = tenChiNhanh;
                    else
                        cboChiNhanh.SelectedIndex = -1;


                    var maTaiKhoan = dgvNV.Rows[n1].Cells["TenTaiKhoan"].Value?.ToString();
                    if (!string.IsNullOrEmpty(maTaiKhoan))
                        cboTenTK.Text = maTaiKhoan;
                    else
                        cboTenTK.SelectedIndex = -1;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn dữ liệu: " + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Messaged
                MessageBox.Show("Vui lòng chọn 1 dòng để xóa hoặc sửa thông tin!", "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }


        // 👉 Hàm xóa khoảng trắng dư (ở đầu, cuối, và giữa nhiều dấu cách)
        string CleanSpace(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            // Chuẩn hóa tất cả loại khoảng trắng đặc biệt về khoảng trắng thường
            string normalized = input;

            // Thay thế tất cả loại space Unicode thành space thường
            normalized = Regex.Replace(normalized, @"[\u0009\u000A\u000B\u000C\u000D\u0020\u0085\u00A0\u1680\u2000-\u200A\u2028\u2029\u202F\u205F\u3000]", " ");

            // Gom nhiều khoảng trắng liên tiếp thành 1
            normalized = Regex.Replace(normalized, @" {2,}", " ");

            // Cắt khoảng trắng đầu và cuối
            return normalized.Trim();
        }

        // 👉 Hàm kiểm tra ký tự đặc biệt (chỉ cho phép chữ, số, khoảng trắng, tiếng Việt)
        bool HasSpecialChar(string input)
        {
            return !Regex.IsMatch(input, @"^[a-zA-ZÀ-ỹ\s]+$");
        }
        private bool CheckData(string maNV, string tenNV, string sdt, string diaChi)
        {
        

            // Làm sạch dữ liệu
            tenNV = CleanSpace(tenNV);
            sdt = CleanSpace(sdt);
            diaChi = CleanSpace(diaChi);

            // 🔹 Kiểm tra tên nhân viên
            if (string.IsNullOrEmpty(tenNV))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tenNV.Length > 100)
            {
                MessageBox.Show("Tên nhân viên không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (HasSpecialChar(tenNV))
            {
                MessageBox.Show("Tên nhân viên không được chứa ký tự đặc biệt hoặc số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 🔹 Kiểm tra số điện thoại
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!Regex.IsMatch(sdt, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại phải bắt đầu bằng 0 và có đúng 10 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (bus_nv.KiemTraSoDienThoaiTrung(sdt,maNV))
            {
                MessageBox.Show("Số điện thoại này đã tồn tại! Vui lòng nhập số khác.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 🔹 Kiểm tra địa chỉ
            if (string.IsNullOrEmpty(diaChi))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (diaChi.Length > 100)
            {
                MessageBox.Show("Địa chỉ không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!Regex.IsMatch(diaChi, @"^[a-zA-Z0-9À-ỹ\s,/.]+$"))
            {
                MessageBox.Show("Địa chỉ không được chứa ký tự đặc biệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }



    private void btnThem_Click(object sender, EventArgs e)
        {
            string maNV = null;
            try
            {
                if (CheckData(maNV,txtTenNhanVien.Text, txtSoDienThoai.Text, txtDiaChi.Text))
                {
                    bool query = bus_nv.AddNV(new DTO_NhanVien(
                        txtTenNhanVien.Text.Trim(),
                        txtSoDienThoai.Text.Trim(),
                        txtDiaChi.Text.Trim(),
                        cboLoaiNhanVien.Text,
                        cboChucVu.SelectedValue.ToString(),
                        cboChiNhanh.SelectedValue.ToString(),
                        cboTenTK.SelectedValue.ToString()));

                    //lay ma nhan vien moi
                    string model_id = bus_nv.GetMaxIdNV();
                    if (query)
                    {
                        MessageBox.Show($"Thêm nhân viên thành công!\n" +
                                    $"Mã nhân viên: {model_id}\n" +
                                    $"Tên nhân viên: {txtTenNhanVien.Text}",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show($"Thêm nhân viên thất bại!\n",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    }

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboTenTK.SelectedIndex = -1;
            cboChucVu.SelectedValue = -1;
            cboChiNhanh.SelectedValue = -1;
            loadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNV.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string currentId = dgvNV.CurrentRow.Cells[0].Value.ToString();

                // Kiểm tra nếu các ô trống -> không cho xóa
                if (string.IsNullOrWhiteSpace(txtTenNhanVien.Text) ||
                    string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ||
                    string.IsNullOrWhiteSpace(txtDiaChi.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên hoặc nhập dữ liệu hợp lệ trước khi xóa!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hỏi xác nhận xóa
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc muốn xóa nhân viên [{txtTenNhanVien.Text}] không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    bool query = bus_nv.XoaNV(currentId);
                    if (query)
                    {
                        MessageBox.Show(
                            $"Xóa nhân viên thành công!\n" +
                            $"Mã nhân viên: {currentId}\n" +
                            $"Tên nhân viên: {txtTenNhanVien.Text}",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show(
                            $"Xóa nhân viên thất bại!\n" +
                            $"Mã nhân viên: {currentId}\n" +
                            $"Tên nhân viên: {txtTenNhanVien.Text}",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Chọn dòng được click chuột phải
                dgvNV.ClearSelection();
                dgvNV.Rows[e.RowIndex].Selected = true;
                dgvNV.CurrentCell = dgvNV.Rows[e.RowIndex].Cells[0];
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string currentId = dgvNV.CurrentRow.Cells[0].Value.ToString();

                if (CheckData(currentId,txtTenNhanVien.Text, txtSoDienThoai.Text, txtDiaChi.Text))
                {
                    DialogResult dr = MessageBox.Show($"Bạn có chắc muốn sửa thông tin: [{txtTenNhanVien.Text}] không?",
                       "Thông báo",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning);

                    if (dr == DialogResult.Yes)
                    {
                        bool query = bus_nv.UpdateNV(new DTO_NhanVien(
                        currentId,
                        txtTenNhanVien.Text,
                        txtSoDienThoai.Text,
                        txtDiaChi.Text,
                        cboLoaiNhanVien.SelectedValue.ToString(),
                        cboChucVu.SelectedValue.ToString(),
                        cboChiNhanh.SelectedValue.ToString(),
                        cboTenTK.SelectedValue.ToString()
                        ));

                        if (query)
                        {
                            MessageBox.Show($"Sửa nhân viên thành công!\n" +
                                        $"Mã nhân viên: {currentId}\n" +
                                        $"Tên nhân viên: {txtTenNhanVien.Text}",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                            loadData();
                        }
                        else
                        {
                            MessageBox.Show($"Sửa nhân viên thất bại!\n" +
                                        $"Mã nhân viên: {currentId}\n" +
                                        $"Tên nhân viên: {txtTenNhanVien.Text}",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            if (cboTim.SelectedIndex == 0)
            {
                // dgvTim
                dgvNV.DataSource = bus_nv.SearchNvByMaNV(txtTim.Text);
                dgvNV.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
                dgvNV.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
                dgvNV.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
                dgvNV.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvNV.Columns["LoaiNhanVien"].HeaderText = "Loại nhân viên";
                dgvNV.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvNV.Columns["TenChiNhanh"].HeaderText = "Tên chi nhánh";
                dgvNV.Columns["TenChucVu"].HeaderText = "chức vụ";
                dgvNV.Columns["TenTaiKhoan"].HeaderText = "Tên tài khoản";
            }
            else
            {
                // dgvTim
                dgvNV.DataSource = bus_nv.SearchNvBytenNV(txtTim.Text);
                dgvNV.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
                dgvNV.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
                dgvNV.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
                dgvNV.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvNV.Columns["LoaiNhanVien"].HeaderText = "Loại nhân viên";
                dgvNV.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvNV.Columns["TenChiNhanh"].HeaderText = "Tên chi nhánh";
                dgvNV.Columns["TenChucVu"].HeaderText = "chức vụ";
                dgvNV.Columns["TenTaiKhoan"].HeaderText = "Tên tài khoản";
            }
        }
    }
    
}

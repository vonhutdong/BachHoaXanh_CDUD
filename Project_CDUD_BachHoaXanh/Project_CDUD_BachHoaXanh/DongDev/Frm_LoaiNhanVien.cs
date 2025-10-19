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

namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_LoaiNhanVien : Form
    {
        BUS_ChucVu bus_lnv = new BUS_ChucVu();
        DataValidation dv = new DataValidation();
        DatabaseAccess da = new DatabaseAccess();
        string data_olds = string.Empty;
        string data_news = string.Empty;
        public Frm_LoaiNhanVien()
        {
            InitializeComponent();
        }
        void loadDATA()
        {
            // Others
            txtTenChucVu.Focus();
            txtTenChucVu.Text = string.Empty;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // dgvLNV
            dgvLoaiNV.DataSource = bus_lnv.GetListLNV();
            dgvLoaiNV.Columns["MaChucVu"].Visible = true;
            dgvLoaiNV.Columns["MaChucVu"].HeaderText = "Mã chức vụ";
            dgvLoaiNV.Columns["TenChucVu"].HeaderText = "Tên chức vụ";

            dgvLoaiNV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvLoaiNV.ColumnHeadersHeight = 45; // Chiều cao tiêu đề

            dgvLoaiNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvLoaiNV.RowTemplate.Height = 45; // Tăng chiều cao từng hàng
            dgvLoaiNV.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvLoaiNV.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvLoaiNV.DefaultCellStyle.BackColor = Color.White;
            dgvLoaiNV.DefaultCellStyle.ForeColor = Color.Black;
            dgvLoaiNV.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvLoaiNV.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvLoaiNV.EnableHeadersVisualStyles = false;
            dgvLoaiNV.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvLoaiNV.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvLoaiNV.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvLoaiNV.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvLoaiNV.AllowUserToResizeColumns = false;
        }
        private void Frm_LoaiNhanVien_Load(object sender, EventArgs e)
        {
            loadDATA();
            dgvLoaiNV.ContextMenuStrip = contextMenuStrip1;

        }
        public bool CheckData(string tenLNV)
        {
            // Initialize Variables
            int count = 0;
            if (!System.Text.RegularExpressions.Regex.IsMatch(tenLNV, @"^[A-Za-zÀ-ỹ0-9]+(?:\s[A-Za-zÀ-ỹ0-9]+)*$"))
            {
                MessageBox.Show("Tên chức vụ không hợp lệ, vui lòng nhập lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Checked tenLNV
            if (dv.CheckString(tenLNV, 50))
            {
                count += 1;
            }
            else
            {
                MessageBox.Show($"Tên chức vụ: [{tenLNV}] không hợp lệ!",
                   "Thông báo",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
            }

            if (count == 1)
            {
                return true;
            }

            return false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string tenCV = txtTenChucVu.Text.Trim();

                


                
                if (CheckData(txtTenChucVu.Text))
                {
                    // Thêm loại nhân viên
                    bool check = bus_lnv.AddLNV2(new DTO_ChucVu(txtTenChucVu.Text));
                    if (check) {

                        // Lấy mã loại nhân viên mới
                        string model_id = bus_lnv.GetMaxMaChucVu();

                        // Thông báo thành công
                        MessageBox.Show($"Thêm chức vụ thành công!\n" +
                                        $"Mã chức vụ: {model_id}\n" +
                                        $"Tên chức vụ: {txtTenChucVu.Text}",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        // Load lại DataGridView
                        loadDATA();

                        // Xoá ô nhập (nếu muốn)
                        txtTenChucVu.Clear();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Thêm chức vụ thất bại!", "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize Variables
                string currentId = dgvLoaiNV.CurrentRow.Cells[0].Value.ToString();


                if (CheckData(txtTenChucVu.Text))
                {
                    DialogResult dr = MessageBox.Show($"Bạn có chắc muốn xóa: [{txtTenChucVu.Text}] không?",
                        "Thông báo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dr == DialogResult.Yes)
                    {
                        bool query = bus_lnv.DelLNV(currentId);
                        if (query)
                        {
                            MessageBox.Show($"Xóa chức vụ thành công!\n" +
                                $"Mã loại nhân viên: {currentId}\n" +
                                $"Tên loại nhân viên: {txtTenChucVu.Text}",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Không thể xoá chức vụ, Vui lòng sửa Chức Vụ nhân viên trước khi xoá !\n" +
                                $"Mã loại nhân viên: {currentId}\n" +
                                $"Tên loại nhân viên: {txtTenChucVu.Text}",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        loadDATA();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập dữ liệu hợp lệ!", "Thông báo",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLoaiNV.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn một loại nhân viên để sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string currentId = dgvLoaiNV.CurrentRow.Cells[0].Value.ToString();
                string currentName = dgvLoaiNV.CurrentRow.Cells[1].Value?.ToString() ?? "";
                string newTenLoai = txtTenChucVu.Text.Trim();

                data_olds = $"TenLoaiNhanVien: {currentName}";
                data_news = $"TenLoaiNhanVien: {newTenLoai}";

                if (CheckData(newTenLoai))
                {
                    DialogResult dr = MessageBox.Show($"Bạn có chắc muốn sửa thông tin: [{newTenLoai}] không?",
                       "Thông báo",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning);

                    if (dr == DialogResult.Yes)
                    {
                        var dto = new DTO_ChucVu(currentId, newTenLoai);
                        bool query = bus_lnv.UpdateLNV2(dto);
                        if (query)
                        {
                            MessageBox.Show($"Sửa loại nhân viên thành công!\n" +
                                $"Mã loại nhân viên: {currentId}\n" +
                                $"Tên loại nhân viên: {newTenLoai}",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            loadDATA();
                        }
                        else
                        {
                            MessageBox.Show($"Sửa loại nhân viên không thành công!\n",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập dữ liệu hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            loadDATA();
        }

        private void dgvLoaiNV_Click(object sender, EventArgs e)
        {
            if (dgvLoaiNV.CurrentCell != null)
            {
                // Get row index selected
                int n = dgvLoaiNV.CurrentCell.RowIndex;

                // Other
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                txtTenChucVu.Text = dgvLoaiNV.Rows[n].Cells[1].Value.ToString();
            }
            else
            {
                // Messaged
                MessageBox.Show("Vui lòng chọn 1 dòng để xóa hoặc sửa thông tin!", "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            }
        }

        private void dgvLoaiNV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Chọn dòng được click chuột phải
                dgvLoaiNV.ClearSelection();
                dgvLoaiNV.Rows[e.RowIndex].Selected = true;
                dgvLoaiNV.CurrentCell = dgvLoaiNV.Rows[e.RowIndex].Cells[0];
            }
        }
    }
}

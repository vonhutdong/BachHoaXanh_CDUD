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

namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_LichLam : Form
    {
        public Frm_LichLam()
        {
            InitializeComponent();
        }
        BUS_LichLam bus_ll = new BUS_LichLam();
        BUS_CaLam bus_cl = new BUS_CaLam();
        int currentID = -1;
        void loadDATA()
        {
            
            dgvLichLam.DataSource = bus_ll.LayDSLichLam();
            string[] arr = { "Theo tên", "Theo ngày" };
            cbTimKiem.DataSource = arr;
            dtpNgay.CustomFormat = "dd/MM/yyyy";
            dtpNgayLam.CustomFormat = "dd/MM/yyyy";


            dgvLichLam.Columns["MaLichLam"].HeaderText = "Mã lịch làm";
            dgvLichLam.Columns["NgayLam"].HeaderText = "Ngày làm";
            dgvLichLam.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";

            dgvLichLam.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
            dgvLichLam.Columns["TenCaLam"].HeaderText = "Tên ca làm";
            dgvLichLam.Columns["MaNhanVien"].Visible = false;

            // Ca làm
            cbMaCaLam.DataSource = bus_cl.LayDSCaLam();
            cbMaCaLam.DisplayMember = "TenCaLam";  // Hiển thị tên ca làm
            cbMaCaLam.ValueMember = "MaCaLam";     // Lưu mã ca làm

            // MaNhân viên
            cbMaNhanVien.DataSource = bus_ll.LayDSNV();
            cbMaNhanVien.DisplayMember = "MaNhanVien"; // Hiển thị mã nhân viên
            cbMaNhanVien.ValueMember = "MaNhanVien";    // Lưu mã nhân viên

            //ten nhan vien
            cboTenNhanVien.DataSource = bus_ll.LayDSNV();
            cboTenNhanVien.DisplayMember = "TenNhanVien"; // Hiển thị tên nhân viên
            cboTenNhanVien.ValueMember = "MaNhanVien"; // Lưu mã nhân viên

            dgvLichLam.Columns["NgayLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichLam.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvLichLam.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvLichLam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvLichLam.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvLichLam.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvLichLam.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvLichLam.DefaultCellStyle.BackColor = Color.White;
            dgvLichLam.DefaultCellStyle.ForeColor = Color.Black;
            dgvLichLam.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvLichLam.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvLichLam.EnableHeadersVisualStyles = false;
            dgvLichLam.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvLichLam.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvLichLam.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvLichLam.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvLichLam.AllowUserToResizeColumns = false;

        }
        private void Reset()
        {
            dtpNgay.Value = DateTime.Now;
            cbMaNhanVien.SelectedIndex = -1;
            cbMaCaLam.SelectedItem = -1;
            //cbMaCaLam.SelectedIndex = -1;
            loadDATA();
        }
        private void Frm_LichLam_Load(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            loadDATA();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DTO_LichLam ll = new DTO_LichLam(dtpNgay.Value,
            cbMaNhanVien.SelectedValue.ToString(), cbMaCaLam.SelectedValue.ToString());
            bool check = bus_ll.ThemLichLam(ll);
            if (check)
            {
                MessageBox.Show("Thêm lịch làm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDATA();
            }
            else
            {
                MessageBox.Show("Nhân viên này đã có lịch làm trong ngày và ca đó!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDATA();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize Variables
                string currentId = dgvLichLam.CurrentRow.Cells[0].Value.ToString();

                if (cbMaNhanVien.Text.Length > 0)
                {
                    DialogResult dr = MessageBox.Show($"Bạn có chắc muốn xóa lịch làm của nhân viên: [{cbMaNhanVien.Text}] không?",
                        "Thông báo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (dr == DialogResult.Yes)
                    {
                        bool check = bus_ll.DellLL(currentId);
                        if (check)
                        {
                            MessageBox.Show("Xoá lịch làm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Xoá lịch làm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            loadDATA();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Hiển thị thông báo xác nhận
            DialogResult r = MessageBox.Show($"Bạn có chắc muốn sửa thông tin lịch làm của nhân viên: {cbMaNhanVien.Text} không?",
                                               "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                // Tạo đối tượng DTO_LichLam từ các giá trị nhập vào
                DTO_LichLam ll = new DTO_LichLam(
                    dtpNgay.Value,
                    cbMaNhanVien.SelectedValue.ToString(),
                    cbMaCaLam.SelectedValue.ToString()
                );

                // Gọi phương thức sửa lịch làm
                bool check = bus_ll.SuaLichLam(ll);

                // Kiểm tra kết quả trả về
                if (check)
                {
                    MessageBox.Show("Sửa lịch làm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDATA();  // Tải lại dữ liệu sau khi sửa thành công
                }
                else
                {
                    MessageBox.Show("Sửa lịch làm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Reset lại giao diện
                Reset();
            }
            else
            {
                // Nếu không xác nhận sửa, không làm gì
                return;
            }
        }

        private void dgvLichLam_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            if (dgvLichLam.CurrentRow == null || dgvLichLam.CurrentRow.Index < 0)
                return;

            int n = dgvLichLam.CurrentRow.Index;

            try
            {
                // Ngày làm
                var ngayLam = dgvLichLam.Rows[n].Cells["NgayLam"].Value?.ToString();
                if (DateTime.TryParse(ngayLam, out DateTime ngay))
                    dtpNgay.Value = ngay;

                // Nhân viên
                var tenNhanVien = dgvLichLam.Rows[n].Cells["ManhanVien"].Value?.ToString();
                if (!string.IsNullOrEmpty(tenNhanVien))
                    cbMaNhanVien.Text = tenNhanVien;
                else
                    cbMaNhanVien.SelectedIndex = -1;

                // Ca làm
                var tenCaLam = dgvLichLam.Rows[n].Cells["TenCaLam"].Value?.ToString();
                if (!string.IsNullOrEmpty(tenCaLam))
                    cbMaCaLam.Text = tenCaLam;
                else
                    cbMaCaLam.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dữ liệu: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTimKiem.SelectedIndex == 0)
            {
                dtpNgayLam.Visible = false;
                txtTimTen.Visible = true;
            }
            else
            {
                dtpNgayLam.Visible = true;
                txtTimTen.Visible = false;
            }
        }

        private void dtpNgayLam_ValueChanged(object sender, EventArgs e)
        {
            DateTime n = dtpNgayLam.Value;           
            dgvLichLam.DataSource = bus_ll.TimKiemTheoNgay(n);
            
        }
        

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            loadDATA();
            Reset();
        }

        private void txtTimTen_TextChanged(object sender, EventArgs e)
        {
            if (cbTimKiem.SelectedValue.ToString() == "Theo tên")
            {
                string n = txtTimTen.Text.Trim();
                if (string.IsNullOrEmpty(n))
                {
                    loadDATA();
                }
                else
                {
                    dgvLichLam.DataSource = bus_ll.TimKiemTheoTen(n);
                }
            }
            else
            {
                //DateTime n;
                //            if (dtpNgayLam.Text.Length == 10)
                //            {
                //	MessageBox.Show(txtTimKiem.Text);
                //	n = DateTime.Parse(txtTimKiem.Text.Trim());
                //                dgvLichLam.DataSource = bus_ll.TimKiemTheoNgay(n);
                //                LoadData();
                //            }
                //            else
                //            {
                //                LoadData();
                //            }
            }
        }

        private void cbMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMaNhanVien.SelectedIndex >= 0 &&
                cbMaNhanVien.SelectedIndex < cboTenNhanVien.Items.Count)
            {
                cboTenNhanVien.SelectedIndex = cbMaNhanVien.SelectedIndex;
            }
        }


        private void cboTenNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class Frm_CaLam : Form
    {
        BUS_CaLam bus_cl = new BUS_CaLam();
        DTO_CaLam caLam = new DTO_CaLam();
        int currentID = -1;
        public Frm_CaLam()
        {
            InitializeComponent();
        }
        void loadDATA()
        {
            dgvCaLam.DataSource = bus_cl.LayDSCaLam();

            // Đổi tên cột
            dgvCaLam.Columns["MaCaLam"].HeaderText = "Mã ca làm";
            dgvCaLam.Columns["TenCaLam"].HeaderText = "Tên ca làm";
            dgvCaLam.Columns["GioBatDau"].HeaderText = "Giờ bắt đầu";
            dgvCaLam.Columns["GioKetThuc"].HeaderText = "Giờ kết thúc";

            // --- Thiết lập kích thước & hiển thị ---
            dgvCaLam.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvCaLam.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvCaLam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvCaLam.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvCaLam.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvCaLam.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvCaLam.DefaultCellStyle.BackColor = Color.White;
            dgvCaLam.DefaultCellStyle.ForeColor = Color.Black;
            dgvCaLam.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvCaLam.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvCaLam.EnableHeadersVisualStyles = false;
            dgvCaLam.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvCaLam.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvCaLam.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvCaLam.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvCaLam.AllowUserToResizeColumns = false;
        }

        private void Frm_CaLam_Load(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            loadDATA();
            dgvCaLam.ContextMenuStrip = contextMenuStrip1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // 🔹 Chuyển chuỗi sang TimeSpan để so sánh
            if (!TimeSpan.TryParse(txtGioBatDau.Text, out TimeSpan gioBD))
            {
                MessageBox.Show("Giờ bắt đầu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TimeSpan.TryParse(txtGioKetThuc.Text, out TimeSpan gioKT))
            {
                MessageBox.Show("Giờ kết thúc không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 Kiểm tra giờ bắt đầu ≤ giờ kết thúc
            if (gioBD > gioKT)
            {
                MessageBox.Show("Giờ bắt đầu không được lớn hơn giờ kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (txtTenCaLam.Text.Length > 0 && txtGioBatDau.Text.Length > 0 && txtGioKetThuc.Text.Length > 0)
                {
                    
                    bus_cl.ThemCaLam(new DTO_CaLam
                    {
                        TenCaLam = txtTenCaLam.Text,
                        GioBatDau = txtGioBatDau.Text,
                        GioKetThuc = txtGioKetThuc.Text
                    });
                    MessageBox.Show("Thêm ca làm thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDATA();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm ca làm thất bại: " , "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtTenCaLam.Text.Length > 0 && txtTenCaLam.Text.Length <= 100)
            {
                if (dgvCaLam.CurrentRow == null || dgvCaLam.CurrentRow.Cells[0].Value == null)
                {
                    MessageBox.Show("Vui lòng chọn một ca làm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string currentId = dgvCaLam.CurrentRow.Cells[0].Value.ToString();
                DialogResult dr = MessageBox.Show($"Bạn có chắc muốn xóa: [{txtTenCaLam.Text}] không?",
                        "Thông báo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {


                    bool result = bus_cl.XoaCaLam(currentId);

                    if (result)
                    {
                        MessageBox.Show("Xóa ca làm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDATA();
                    }
                    else
                    {
                        MessageBox.Show("Bạn không thể xoá Ca Làm khi đang tồn tại trong Lịch Làm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ca làm muốn thao tác!", "Thông báo",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // 🔹 Chuyển chuỗi sang TimeSpan để so sánh
            if (!TimeSpan.TryParse(txtGioBatDau.Text, out TimeSpan gioBD))
            {
                MessageBox.Show("Giờ bắt đầu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TimeSpan.TryParse(txtGioKetThuc.Text, out TimeSpan gioKT))
            {
                MessageBox.Show("Giờ kết thúc không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 Kiểm tra giờ bắt đầu ≤ giờ kết thúc
            if (gioBD > gioKT)
            {
                MessageBox.Show("Giờ bắt đầu không được lớn hơn giờ kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (txtTenCaLam.Text.Length > 0 && txtGioBatDau.Text.Length > 0 && txtGioKetThuc.Text.Length > 0)
                {
                    string maCaLam = dgvCaLam.CurrentRow.Cells["maCaLam"].Value.ToString(); // 👈 Lấy mã ca làm đang chọn

                    DialogResult dr = MessageBox.Show(
                        $"Bạn có chắc muốn sửa: [{txtTenCaLam.Text}] không?",
                        "Thông báo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (dr == DialogResult.Yes)
                    {
                        DTO_CaLam caLam = new DTO_CaLam
                        {
                            MaCaLam = maCaLam,
                            TenCaLam = txtTenCaLam.Text,
                            GioBatDau = txtGioBatDau.Text,
                            GioKetThuc = txtGioKetThuc.Text
                        };

                        bool result = bus_cl.suaCaLam(caLam);
                        if (result)
                        {
                            MessageBox.Show("Sửa ca làm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadDATA();
                        }
                        else
                        {
                            MessageBox.Show("Sửa ca làm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chưa nhập dữ liệu!!", "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Reset();
            loadDATA();
        }
        private void Reset()
        {
            txtTenCaLam.Focus();
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            txtTenCaLam.Text = string.Empty;
            txtGioBatDau.Text = string.Empty;
            txtGioKetThuc.Text = string.Empty;
        }

        private void dgvCaLam_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            // Initialize Variable
            int n = dgvCaLam.CurrentCell.RowIndex;

            if (n >= 0)
            {

                // txtTenCaLam
                txtTenCaLam.Text = dgvCaLam.Rows[n].Cells["TenCaLam"].Value.ToString();

                // txtGioBatDau
                txtGioBatDau.Text = dgvCaLam.Rows[n].Cells["GioBatDau"].Value.ToString();

                //txtGioKetThuc
                txtGioKetThuc.Text = dgvCaLam.Rows[n].Cells["GioKetThuc"].Value.ToString();

            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để xóa hoặc sửa thông tin!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvCaLam_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCaLam_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Chọn dòng được click chuột phải
                dgvCaLam.ClearSelection();
                dgvCaLam.Rows[e.RowIndex].Selected = true;
                dgvCaLam.CurrentCell = dgvCaLam.Rows[e.RowIndex].Cells[0];
            }
        }
    }
}

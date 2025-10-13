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
    public partial class Frm_ChiNhanh : Form
    {
        public Frm_ChiNhanh()
        {
            InitializeComponent();
        }
        BUS_ChiNhanh bus_chinhanh = new BUS_ChiNhanh();
        int currentID = 0;
        private string currentMaCN = "";

        private void Clear()
        {
            //làm mới txt
            txtMaChiNhanh.Enabled = true;
            txtMaChiNhanh.Focus();
            txtMaChiNhanh.Clear();
            txtTenChiNhanh.Clear();
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
            //load lai chi nhanh
            LoadDSChiNhanh();
        }
        private void LoadDSChiNhanh()
        {

            txtMaChiNhanh.Focus();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            dgvChiNhanh.DataSource = bus_chinhanh.LayDSChiNhanh();
            //dổi tên cột
            dgvChiNhanh.Columns["MaChiNhanh"].HeaderText = "Mã chi nhánh";
            dgvChiNhanh.Columns["TenChiNhanh"].HeaderText = "Tên chi nhánh";
            dgvChiNhanh.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvChiNhanh.Columns["SoDienThoai"].HeaderText = "Số điện thoại";

            dgvChiNhanh.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChiNhanh.ColumnHeadersHeight = 40; // hoặc cao hơn

            // Thiết lập lại style để dữ liệu hiện rõ
            dgvChiNhanh.DefaultCellStyle.BackColor = Color.White;
            dgvChiNhanh.DefaultCellStyle.ForeColor = Color.Black;
            dgvChiNhanh.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvChiNhanh.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvChiNhanh.EnableHeadersVisualStyles = false;
            dgvChiNhanh.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvChiNhanh.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        }

        private void Frm_ChiNhanh_Load(object sender, EventArgs e)
        {
            LoadDSChiNhanh();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo DTO và gửi đi
                DTO_ChiNhanh chiNhanh = new DTO_ChiNhanh(
                    txtMaChiNhanh.Text.Trim(),
                    txtTenChiNhanh.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSoDienThoai.Text.Trim()
                );

                // Gọi BUS để thêm
                bus_chinhanh.themChiNhanh(chiNhanh);

                MessageBox.Show("Thêm chi nhánh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới form
                LoadDSChiNhanh();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(currentMaCN))
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa chi nhánh này không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bus_chinhanh.XoaChiNhanh(currentMaCN);
                        MessageBox.Show(" Xóa chi nhánh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDSChiNhanh();
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

        private void dgvChiNhanh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentMaCN = dgvChiNhanh.Rows[e.RowIndex].Cells["MaChiNhanh"].Value.ToString();
                txtMaChiNhanh.Text = currentMaCN;
                txtTenChiNhanh.Text = dgvChiNhanh.Rows[e.RowIndex].Cells["TenChiNhanh"].Value.ToString();
                txtDiaChi.Text = dgvChiNhanh.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();
                txtSoDienThoai.Text = dgvChiNhanh.Rows[e.RowIndex].Cells["SoDienThoai"].Value.ToString();
            }

            txtMaChiNhanh.Enabled = false;

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                //  Không cho sửa mã chi nhánh
                if (!txtMaChiNhanh.Enabled)
                {
                    // Mã bị khóa (chỉ đọc) → không cần kiểm tra
                }
                else
                {
                    MessageBox.Show("Không được phép thay đổi MÃ chi nhánh!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //  Gửi dữ liệu qua BUS để xử lý
                DTO_ChiNhanh cn = new DTO_ChiNhanh
                {
                    MaChiNhanh = txtMaChiNhanh.Text.Trim(),
                    TenChiNhanh = txtTenChiNhanh.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim()
                };

                // Gọi BUS (đã kiểm tra trong DAL)
                bus_chinhanh.suaChinhNhanh(cn);

                MessageBox.Show(" Sửa chi nhánh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Làm mới danh sách
                LoadDSChiNhanh();
                Clear();
                // Bật lại ô mã để thêm mới
                txtMaChiNhanh.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi sửa chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}

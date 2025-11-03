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
    public partial class Frm_HoaDon : Form
    {
        private bool isLoading = false;
        BUS_HoaDon BUS_HoaDon = new BUS_HoaDon();
        BUS_HoaDon bus_hd = new BUS_HoaDon();
        //BUS_KhachHan bus_kh = new BUS_KhachHang();
        BUS_NhanVien bus_nv = new BUS_NhanVien();
        public Frm_HoaDon()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            dgvHoaDon.DataSource = BUS_HoaDon.GetListHD();

            //cboMaKH.DataSource = bus_kh.LayDSKH();
            //cboMaKH.DisplayMember = "TenKH";
            //cboMaKH.ValueMember = "id";

            cboMaNV.DataSource = bus_nv.LayDSNhanVien();
            cboMaNV.DisplayMember = "MaNhanVien";
            cboMaNV.ValueMember = "MaNhanVien";

            cboTenNV.DataSource = bus_nv.LayDSNhanVien();
            cboTenNV.DisplayMember = "TenNhanVien";
            cboTenNV.ValueMember = "MaNhanVien";

            // Đặt tiêu đề các cột
            dgvHoaDon.Columns["MaHD"].HeaderText = "Mã hóa đơn";
            dgvHoaDon.Columns["NgayLapHD"].HeaderText = "Ngày lập hóa đơn";
            dgvHoaDon.Columns["GioLapHD"].HeaderText = "Giờ lập hóa đơn";
            dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvHoaDon.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvHoaDon.Columns["PhuongThucThanhToan"].HeaderText = "Phương Thức TT";
            dgvHoaDon.Columns["MaKhachHang"].HeaderText = "Mã khách hàng";
            dgvHoaDon.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
            dgvHoaDon.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
            dgvHoaDon.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";

            // Format ngày & giờ
            dgvHoaDon.Columns["NgayLapHD"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvHoaDon.Columns["GioLapHD"].DefaultCellStyle.Format = "HH:mm:ss";
            // --- Thiết lập kích thước & hiển thị ---
            dgvHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvHoaDon.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvHoaDon.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvHoaDon.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvHoaDon.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvHoaDon.DefaultCellStyle.BackColor = Color.White;
            dgvHoaDon.DefaultCellStyle.ForeColor = Color.Black;
            dgvHoaDon.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvHoaDon.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvHoaDon.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvHoaDon.AllowUserToResizeColumns = false;
        }
        private void Frm_HoaDon_Load(object sender, EventArgs e)
        {
            isLoading = true;
            LoadData();
            isLoading = false;
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return; // Ngăn sự kiện khi đang load

            if (cboMaNV.Items.Count == 0 || cboTenNV.Items.Count == 0) return;

            if (cboMaNV.SelectedIndex >= 0 && cboMaNV.SelectedIndex < cboTenNV.Items.Count)
            {
                cboTenNV.SelectedIndex = cboMaNV.SelectedIndex;
            }

        }
        void LamMoi()
        {
           
            cboMaKH.SelectedIndex = -1;
            cboMaNV.SelectedIndex = -1;
            cboTenNV.SelectedIndex = -1;
            cboTenKH.SelectedIndex = -1;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
            LoadData();
        }

        private void dgvHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentCell != null)
            {
                int n = dgvHoaDon.CurrentCell.RowIndex;

                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                // Chỉ đặt SelectedValue – KHÔNG đặt lại DataSource nữa
                try
                {
                    //cboMaKH.SelectedValue = Convert.ToInt32(dgvHD.Rows[n].Cells[7].Value); // id khách hàng
                    cboMaNV.SelectedValue = dgvHoaDon.Rows[n].Cells[7].Value; // id nhân viên
                }
                catch
                {
                    MessageBox.Show("Không tìm thấy dữ liệu tương ứng trong ComboBox.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để xóa hoặc sửa!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow != null)
            {
                string maHD = dgvHoaDon.CurrentRow.Cells["MaHD"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa hóa đơn này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    if (bus_hd.DelHD(maHD))
                    {
                        MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Reload danh sách hóa đơn
                    }
                    else
                    {
                        MessageBox.Show("Xóa hóa đơn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow != null)
            {
                string id = dgvHoaDon.CurrentRow.Cells["MaHD"].Value.ToString();

                // Lấy id từ các combobox
                string maKH = cboMaKH.SelectedValue.ToString();
                //string tenKH = cboTenKH.SelectedValue.ToString();
                string maNV = cboMaNV.SelectedValue.ToString();
                //string tenNV = cboTenNV.SelectedValue.ToString();

                // Tạo DTO_HoaDon
                DTO_HoaDon hd = new DTO_HoaDon(id, maKH, maNV);

                // Gọi BUS cập nhật
                if (bus_hd.updateHD(hd))
                {
                    MessageBox.Show("Cập nhật hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật hóa đơn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

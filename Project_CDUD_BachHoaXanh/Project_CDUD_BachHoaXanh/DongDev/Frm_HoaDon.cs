using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAL;
using DTO;

namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_HoaDon : Form
    {
        private bool isLoading = false;
        BUS_HoaDon BUS_HoaDon = new BUS_HoaDon();
        BUS_HoaDon bus_hd = new BUS_HoaDon();
        BUS_KhachHang bus_kh = new BUS_KhachHang();
        BUS_NhanVien bus_nv = new BUS_NhanVien();
        DTO_NhanVien nhanVien = null;
        DAL_TaiKhoan daltk = new DAL_TaiKhoan();
        private string tk = string.Empty;
        private int quyen = -1;
        private Form frmOld = null;
        public static DTO_NhanVien NhanVien = null;
        //public Frm_HoaDon()
        //{
        //    InitializeComponent();
        //}
        void LoadData()
        {
            dgvHoaDon.DataSource = BUS_HoaDon.GetListHD();

            cboMaKH.DataSource = bus_kh.LayDSKH();
            cboMaKH.DisplayMember = "MaKH";
            cboMaKH.ValueMember = "MaKH";

            cboTenKH.DataSource = bus_kh.LayDSKH();
            cboTenKH.DisplayMember = "TenKH";
            cboTenKH.ValueMember = "MaKH";

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

            dgvHoaDon.Columns["MaNhanVien"].Visible = false;
            dgvHoaDon.Columns["MaKhachHang"].Visible = false;
            dgvHoaDon.Columns["ThanhTien"].Visible = true;

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
            dgvHoaDon.ContextMenuStrip = contextMenuStrip1;
            nhanVien = Frm_TrangChu.getNhanVien();
            //MessageBox.Show(quyen.ToString());
            
        }
        public Frm_HoaDon(string taiKhoan, int quyen, DTO_NhanVien nhanVien)
        {
            InitializeComponent();
            this.tk = taiKhoan;
            Frm_TrangChu.NhanVien = nhanVien; // Gọi static property
            this.quyen = quyen;

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
            if (dgvHoaDon.CurrentCell == null)
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để sửa hoặc xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int n = dgvHoaDon.CurrentRow.Index;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

            try
            {
                // 🧠 Kiểm tra tồn tại cột trước khi lấy
                var cellMaKH = dgvHoaDon.Rows[n].Cells["MaKhachHang"]?.Value;
                var cellMaNV = dgvHoaDon.Rows[n].Cells["MaNhanVien"]?.Value;
                var cellPTTT = dgvHoaDon.Rows[n].Cells[5]?.Value;

                if (cellMaKH != null)
                    cboMaKH.SelectedValue = cellMaKH.ToString().Trim();
                else
                    cboMaKH.SelectedIndex = -1;

                if (cellMaNV != null)
                    cboMaNV.SelectedValue = cellMaNV.ToString().Trim();
                else
                    cboMaNV.SelectedIndex = -1;

                if (cellPTTT != null)
                    cboPTTT.Text = cellPTTT.ToString().Trim();
                else
                    cboPTTT.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lấy được dữ liệu từ dòng được chọn.\nChi tiết lỗi: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
            if (quyen == 0)
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
            else
            {
                MessageBox.Show("Bạn không có quyền xóa hóa đơn!, Vui lòng gọi ADMIN để được hổ trợ!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (quyen == 1)
            {
                MessageBox.Show("Bạn không có quyền sửa hóa đơn!, Vui lòng gọi ADMIN để được hổ trợ!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (dgvHoaDon.CurrentRow != null)
                {
                    string id = dgvHoaDon.CurrentRow.Cells["MaHD"].Value.ToString();

                    // Lấy id từ các combobox
                    string maKH = cboMaKH.SelectedValue.ToString();
                    //string tenKH = cboTenKH.SelectedValue.ToString();
                    string maNV = cboMaNV.SelectedValue.ToString();
                    //string tenNV = cboTenNV.SelectedValue.ToString();
                    string pTTT = cboPTTT.Text;
                    // Tạo DTO_HoaDon
                    DTO_HoaDon hd = new DTO_HoaDon(id, maKH, maNV, pTTT);

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

        private void menuToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnTimHD_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimHD.Text.Trim();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                dgvHoaDon.DataSource = bus_hd.TimKiemHD(tuKhoa);
            }
            else
            {
                MessageBox.Show("Không tim thấy Hoa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData(); // nếu không nhập gì thì load lại toàn bộ
            }
        }
        private string RemoveDiacritics(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private void OpenChiTietHDForm(string maHD)
        {
            Frm_ChiTietHoaDonFrm frm = new Frm_ChiTietHoaDonFrm(maHD,quyen);
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            this.Controls.Clear();
            this.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }




        private void dgvHoaDon_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Chọn dòng được click chuột phải
                dgvHoaDon.ClearSelection();
                dgvHoaDon.Rows[e.RowIndex].Selected = true;
                dgvHoaDon.CurrentCell = dgvHoaDon.Rows[e.RowIndex].Cells[0];
            }
        }
        string maHD = "";
        private void xoáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow != null)
            {
                maHD = dgvHoaDon.CurrentRow.Cells["maHD"].Value.ToString();

                OpenChiTietHDForm(maHD);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xem chi tiết!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return; // Ngăn sự kiện khi đang load

            if (cboMaKH.Items.Count == 0 || cboTenKH.Items.Count == 0) return;

            if (cboMaKH.SelectedIndex >= 0 && cboMaKH.SelectedIndex < cboTenKH.Items.Count)
            {
                cboTenKH.SelectedIndex = cboMaKH.SelectedIndex;
            }
        }
    }
}

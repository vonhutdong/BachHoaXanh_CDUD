using DAL;
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
    public partial class Frm_PhieuNhap : Form
    {
        public Frm_PhieuNhap()
        {
            InitializeComponent();
            txtThanhTien.Text = "0";
            txtThanhTien.Enabled = false;
            dtpNgayNhap.Format = DateTimePickerFormat.Custom;
            dtpNgayNhap.CustomFormat = "dd/MM/yyyy";
        }
        BUS_ChiTietPhieuNhap bus_ctpn = new BUS_ChiTietPhieuNhap();
        BUS_PhieuNhap bus_pn = new BUS_PhieuNhap();
        BUS_PhieuNhap bus_pnALL = new BUS_PhieuNhap();
        string currentMaPN = string.Empty;
        int currentIdCTPN = -1;
        string data_olds = string.Empty;
        string data_news = string.Empty;
        private void LoadDataPhieuNhap()
        {
            btnThemPN.Enabled = true;
            btnThemCTPN.Enabled = true;
            btnSuaPN.Enabled = false;
            btnSuaCTPN.Enabled = false;
            btnXoaPN.Enabled = false;
            btnXoaCTPN.Enabled = false;
            dgvPhieuNhap.DataSource = bus_pnALL.LayDSPhieuNhap();
            dtpNgayNhap.Value = DateTime.Now;
            //đổi tên cột
            dgvPhieuNhap.Columns["MaPhieuNhap"].HeaderText = "Mã phiếu nhập";
            dgvPhieuNhap.Columns["NgayNhap"].HeaderText = "Ngày nhập";
            dgvPhieuNhap.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvPhieuNhap.Columns["MaNhanVien"].HeaderText = "Nhân viên";

            dgvPhieuNhap.Columns["NgayNhap"].DefaultCellStyle.Format = "dd/MM/yyyy";



            // --- Thiết lập kích thước & hiển thị ---
            dgvPhieuNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvPhieuNhap.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvPhieuNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvPhieuNhap.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvPhieuNhap.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvPhieuNhap.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvPhieuNhap.DefaultCellStyle.BackColor = Color.White;
            dgvPhieuNhap.DefaultCellStyle.ForeColor = Color.Black;
            dgvPhieuNhap.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvPhieuNhap.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvPhieuNhap.EnableHeadersVisualStyles = false;
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvPhieuNhap.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvPhieuNhap.AllowUserToResizeColumns = false;
            cboNhanVien.DataSource = bus_pnALL.LayDSNhanVien();
            cboNhanVien.DisplayMember = "TenNhanVien";
            cboNhanVien.ValueMember = "MaNhanVien";
            //cb san pham

        }
        private void LoadDataChiTietPN()
        {
            //btnThemPN.Enabled = true;
            //btnThemCTPN.Enabled = true;
            //btnSuaPN.Enabled = false;
            //btnSuaCTPN.Enabled = false;
            //btnXoa.Enabled = false;

            dgvChiTietPhieuNhap.DataSource = bus_ctpn.LayDSCTPN();

            //đổi tên cột
            dgvChiTietPhieuNhap.Columns["maPhieuNhap"].HeaderText = "Mã phiếu nhập";
            dgvChiTietPhieuNhap.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTietPhieuNhap.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvChiTietPhieuNhap.Columns["maSanPham"].HeaderText = "Tên sản phẩm";
            dgvChiTietPhieuNhap.Columns["id"].Visible = false;
            dgvChiTietPhieuNhap.Columns["Mapn"].Visible = false;
            dgvChiTietPhieuNhap.Columns["Masp"].Visible = false;
            // --- Thiết lập kích thước & hiển thị ---
            dgvChiTietPhieuNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvChiTietPhieuNhap.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvChiTietPhieuNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvChiTietPhieuNhap.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvChiTietPhieuNhap.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvChiTietPhieuNhap.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvChiTietPhieuNhap.DefaultCellStyle.BackColor = Color.White;
            dgvChiTietPhieuNhap.DefaultCellStyle.ForeColor = Color.Black;
            dgvChiTietPhieuNhap.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvChiTietPhieuNhap.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvChiTietPhieuNhap.EnableHeadersVisualStyles = false;
            dgvChiTietPhieuNhap.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvChiTietPhieuNhap.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvChiTietPhieuNhap.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvChiTietPhieuNhap.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvChiTietPhieuNhap.AllowUserToResizeColumns = false;

            cboMaPhieuNhap.DataSource = bus_pn.LayDSPhieuNhap();
            cboMaPhieuNhap.DisplayMember = "MaPhieuNhap";
            cboMaPhieuNhap.ValueMember = "MaPhieuNhap";
            cboMaPhieuNhap.SelectedIndex = 0;

            //cbMaPhieuNhap.DataSource = bus_pn.LayDSPhieuNhap();
            //cbMaPhieuNhap.DisplayMember = "idSanPham";
            //cbMaPhieuNhap.ValueMember = "idSanPham";

            cboTenSanPham.DataSource = bus_ctpn.LayDSSP();
            cboTenSanPham.DisplayMember = "TenSanPham";
            cboTenSanPham.ValueMember = "MaSanPham";
            cboTenSanPham.SelectedIndex = 0;
        }

        private void Frm_PhieuNhap_Load(object sender, EventArgs e)
        {
            LoadDataPhieuNhap();
            LoadDataChiTietPN();
        }

        private void inThongKePhieuNhapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ThongKeTheoMaPhieuNhap frm_ThongKeTheoMaPhieuNhap = new Frm_ThongKeTheoMaPhieuNhap();
            frm_ThongKeTheoMaPhieuNhap.Show();
        }

        private void inDanhSachPhieuNhapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_DanhSachPhieuNhap frm_DanhSachPhieuNhap = new Frm_DanhSachPhieuNhap();
            frm_DanhSachPhieuNhap.Show();
        }

        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhieuNhap.Rows[e.RowIndex];

                // Lưu mã phiếu nhập hiện tại
                currentMaPN = row.Cells["MaPhieuNhap"].Value.ToString();

                // Nếu có textbox hiển thị, có thể set thêm
                dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
                cboNhanVien.SelectedValue = row.Cells["MaNhanVien"].Value.ToString();
                txtThanhTien.Text = row.Cells["ThanhTien"].Value.ToString();
                btnThemPN.Enabled = false;
                btnSuaPN.Enabled = true;
                btnXoaPN.Enabled = true;
            }
        }

        private void dgvChiTietPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvChiTietPhieuNhap.Rows[e.RowIndex];
                    currentIdCTPN = Convert.ToInt32(row.Cells["id"].Value);
                    // --- LẤY DỮ LIỆU CHI TIẾT ---
                    string maPhieuNhap = row.Cells["Mapn"].Value?.ToString();
                    string tenSanPham = row.Cells["Masp"].Value?.ToString();
                    string soLuong = row.Cells["soLuong"].Value?.ToString();
                    string donGia = row.Cells["donGia"].Value?.ToString();

                    // --- HIỂN THỊ LÊN PHẦN CHI TIẾT ---
                    cboMaPhieuNhap.SelectedValue = maPhieuNhap;
                    cboTenSanPham.SelectedValue = tenSanPham;
                    txtSoLuong.Text = soLuong;
                    txtDonGia.Text = donGia;

                    // --- HIỂN THỊ LÊN PHẦN PHIẾU NHẬP ---
                    // Tìm dòng tương ứng trong danh sách phiếu nhập
                    var phieuNhapRow = dgvPhieuNhap.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.Cells["MaPhieuNhap"].Value.ToString() == maPhieuNhap);

                    if (phieuNhapRow != null)
                    {
                        // Lấy thông tin phiếu nhập
                        string ngayNhap = phieuNhapRow.Cells["NgayNhap"].Value?.ToString();
                        string thanhTien = phieuNhapRow.Cells["ThanhTien"].Value?.ToString();
                        string nhanVien = phieuNhapRow.Cells["MaNhanVien"].Value?.ToString();

                        // Đổ lên giao diện
                        cboNhanVien.SelectedValue = nhanVien;
                        txtThanhTien.Text = thanhTien;

                        if (DateTime.TryParse(ngayNhap, out DateTime ngay))
                            dtpNgayNhap.Value = ngay;
                    }
                    btnThemCTPN.Enabled = false;
                    btnSuaCTPN.Enabled = true;
                    btnXoaCTPN.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị chi tiết phiếu nhập: " + ex.Message);
            }
        }

        private void btnThemPN_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngayNhap = dtpNgayNhap.Value;

                // 🛑 Kiểm tra ngày nhập
                if (ngayNhap.Date < DateTime.Today)
                {
                    MessageBox.Show("Không thể chọn ngày nhập trong quá khứ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // 1️⃣ Lấy dữ liệu từ giao diện
                DTO_PhieuNhap pn = new DTO_PhieuNhap();
                pn.NgayNhap = dtpNgayNhap.Value;
                pn.MaNhanVien = cboNhanVien.SelectedValue?.ToString(); // lấy mã nhân viên từ combobox
                pn.ThanhTien = 0; // không cho nhập tay

                // 2️⃣ Gọi BUS để thêm
                bool result = bus_pn.ThemPhieuNhap(pn);

                if (result)
                {
                    MessageBox.Show("Thêm phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Cập nhật lại danh sách
                    LoadDataPhieuNhap();     // Cập nhật lại danh sách phiếu nhập
                    LoadDataChiTietPN();     // Cập nhật danh sách chi tiết
                    reset();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaPN_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(currentMaPN))
            {
                
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc muốn xóa phiếu nhập có mã [{currentMaPN}] không?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            bus_pn.XoaPhieuNhap(currentMaPN);
                            MessageBox.Show("Xóa phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataPhieuNhap();     // Cập nhật lại danh sách phiếu nhập
                            LoadDataChiTietPN();     // Cập nhật danh sách chi tiết
                            currentMaPN = string.Empty; // reset
                            reset();
                    }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                
            else
                {
                    MessageBox.Show("Vui lòng chọn phiếu nhập cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            reset();
        }
        private void reset()
        {
            dtpNgayNhap.Value = DateTime.Now;
            txtThanhTien.Text = string.Empty;
            cboNhanVien.SelectedIndex = 0;
            txtThanhTien.Text = "0";
            txtSoLuong.Text = string.Empty;
            txtDonGia.Text = string.Empty;
            cboMaPhieuNhap.SelectedIndex = 0;
            cboTenSanPham.SelectedIndex = 0;
            LoadDataChiTietPN();
            LoadDataPhieuNhap();
        }
        private void resetCT()
        {
            txtSoLuong.Text = string.Empty;
            txtDonGia.Text = string.Empty;
            //cboMaPhieuNhap.SelectedIndex = 0;
            cboTenSanPham.SelectedIndex = 0;
            
        }

        private void btnSuaPN_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có dòng nào được chọn không
                if (dgvPhieuNhap.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn phiếu nhập cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy mã phiếu nhập hiện tại từ DataGridView
                string maPN = dgvPhieuNhap.CurrentRow.Cells["MaPhieuNhap"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(maPN))
                {
                    MessageBox.Show("Không xác định được mã phiếu nhập cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy giá trị từ giao diện
                DateTime ngayNhap = dtpNgayNhap.Value;
                string maNV = cboNhanVien.SelectedValue?.ToString();

                // Ràng buộc dữ liệu nhập
                if (string.IsNullOrWhiteSpace(maNV))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Không cho chọn ngày nhỏ hơn hôm nay
                if (ngayNhap.Date < DateTime.Today)
                {
                    MessageBox.Show("Ngày nhập không được nhỏ hơn hôm nay!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng DTO để cập nhật
                DTO_PhieuNhap pn = new DTO_PhieuNhap
                {
                    MaPhieuNhap = maPN,
                    NgayNhap = ngayNhap,
                    MaNhanVien = maNV
                };

                // Gọi BUS để sửa
                bool result = bus_pnALL.SuaPN(pn);

                if (result)
                {
                    MessageBox.Show("Cập nhật phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataPhieuNhap();     // Cập nhật lại danh sách phiếu nhập
                    LoadDataChiTietPN();     // Cập nhật danh sách chi tiết
                    reset();       // Hàm reset form
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemCTPN_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Lấy dữ liệu từ giao diện
                string maPN = cboMaPhieuNhap.SelectedValue?.ToString();
                string maSP = cboTenSanPham.SelectedValue?.ToString();
                int soLuong = int.Parse(txtSoLuong.Text.Trim());
                float donGia = float.Parse(txtDonGia.Text.Trim());

                // ✅ Tạo DTO
                DTO_ChiTietPhieuNhap ct = new DTO_ChiTietPhieuNhap
                {
                    MaPhieuNhap = maPN,
                    MaSanPham = maSP,
                    SoLuong = soLuong,
                    DonGia = donGia
                };

                // ✅ Gọi BUS (BUS gọi xuống DAL, DAL tự ràng buộc)
                if (bus_ctpn.ThemChiTiet(ct))
                {
                    MessageBox.Show("Thêm chi tiết phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Lưu lại mã phiếu nhập đã chọn
                    var selectedMaPhieuNhap = cboMaPhieuNhap.SelectedValue;

                    LoadDataPhieuNhap();     // Cập nhật lại danh sách phiếu nhập
                    LoadDataChiTietPN();     // Cập nhật danh sách chi tiết

                    // Gán lại giá trị đã chọn
                    cboMaPhieuNhap.SelectedValue = selectedMaPhieuNhap;
                    resetCT();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // ❌ Bắt lỗi từ DAL (ví dụ: null, không tồn tại mã, trùng chi tiết, …)
                MessageBox.Show(ex.Message, "Lỗi khi thêm chi tiết phiếu nhập: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaCTPN_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (currentIdCTPN == -1)
                {
                    MessageBox.Show("Vui lòng chọn chi tiết phiếu nhập cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa chi tiết phiếu nhập này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Gọi BUS xóa
                    bus_ctpn.XoaChiTiet(currentIdCTPN);

                    MessageBox.Show("Xóa chi tiết phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Làm mới DataGridView sau khi xóa
                    LoadDataPhieuNhap();     // Cập nhật lại danh sách phiếu nhập
                    LoadDataChiTietPN();     // Cập nhật danh sách chi tiết

                    // Reset ID hiện tại
                    currentIdCTPN = -1;
                    resetCT();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa chi tiết phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaCTPN_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các ô nhập
                string maPhieuNhap = cboMaPhieuNhap.Text.Trim();
                string maSanPham = cboTenSanPham.SelectedValue.ToString().Trim();
                int soLuong;
                float donGia;

                if (string.IsNullOrEmpty(maPhieuNhap) || string.IsNullOrEmpty(maSanPham))
                {
                    MessageBox.Show("Vui lòng chọn phiếu nhập và sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text, out soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên dương!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!float.TryParse(txtDonGia.Text, out donGia) || donGia <= 0)
                {
                    MessageBox.Show("Đơn giá phải là số dương!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tạo DTO để truyền xuống tầng BUS
                DTO_ChiTietPhieuNhap ct = new DTO_ChiTietPhieuNhap
                {
                    Id = currentIdCTPN,
                    MaPhieuNhap = maPhieuNhap,
                    MaSanPham = maSanPham,
                    SoLuong = soLuong,
                    DonGia = donGia
                };

                // Gọi hàm sửa trong BUS
                if (bus_ctpn.SuaChiTiet(ct))
                {
                    MessageBox.Show("Cập nhật chi tiết phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Lưu lại mã phiếu nhập đã chọn
                    var selectedMaPhieuNhap = cboMaPhieuNhap.SelectedValue;

                    LoadDataPhieuNhap();     // Cập nhật lại danh sách phiếu nhập
                    LoadDataChiTietPN();     // Cập nhật danh sách chi tiết

                    // Gán lại giá trị đã chọn
                    cboMaPhieuNhap.SelectedValue = selectedMaPhieuNhap;
                    resetCT();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật chi tiết phiếu nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa chi tiết phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

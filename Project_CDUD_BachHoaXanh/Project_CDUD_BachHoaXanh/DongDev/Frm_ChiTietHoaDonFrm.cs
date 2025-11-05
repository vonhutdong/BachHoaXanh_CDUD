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



namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_ChiTietHoaDonFrm : Form
    {
        BUS_ChiTietHoa bus_cthd = new BUS_ChiTietHoa();
        BUS_HoaDon bus_hd = new BUS_HoaDon();
        BUS_SanPham bus_sp = new BUS_SanPham();
        //BUS_KhoHang bus_kh = new BUS_KhoHang();
        //BUS_KhachHang bus_khachhang = new BUS_KhachHang();
        BUS_NhanVien bus_nv = new BUS_NhanVien();
        int soLuongInput = 0;
        double tongTien = 0;
        double donGia = 0;
        private string maHD;

        // ✅ Thêm constructor mới nhận mã hóa đơn
        public Frm_ChiTietHoaDonFrm(string maHD)
        {
            InitializeComponent(); // luôn gọi đầu tiên
            this.maHD = maHD;
        }

        public Frm_ChiTietHoaDonFrm()
        {
            InitializeComponent();
        }
        public void LoadFirst()
        {

            // cboMaKH
            //cboMaKH.DataSource = bus_khachhang.LayDSKH();
            //cboMaKH.DisplayMember = "TenKH";
            //cboMaKH.ValueMember = "id";
            //cboMaKH.SelectedIndex = 0;

            // cboMaNV
            cboMaNV.DataSource = bus_nv.LayDSNhanVien();
            cboMaNV.DisplayMember = "TenNhanVien";
            cboMaNV.ValueMember = "MaNhanVien";
            cboMaNV.SelectedIndex = 0;

        }
        public void ResetFirst()
        {
            LoadFirst();
        }
        public void LoadData()
        {

            // cboMaHD
            //cboMaHD.DataSource = bus_hd.GetListHD();
            //cboMaHD.DisplayMember = "MaHoaDon";
            //cboMaHD.ValueMember = "MaHoaDon";
            //cboMaHD.SelectedIndex = 0;

            // cboSanPham
            cboTimKiemSp.SelectedIndex = 0;

            // dgvSP
            dgvSP.DataSource = bus_sp.GetListSP();
            //dgvSP.Columns[4].DefaultCellStyle.Format = "#,###";

        }
        public void Reset()
        {
            LoadData();
        }
        public bool CheckData(string maSanPham, string soLuong)
        {
            int count = 0;

            // Lấy số lượng tồn kho
            int soLuongSpTrongKho = bus_sp.GetSoLuongSpTrongKho(maSanPham);

            // Tự kiểm tra hợp lệ
            if (int.TryParse(soLuong, out int soLuongInt))
            {
                if (soLuongInt >= 1 && soLuongInt <= soLuongSpTrongKho)
                {
                    count++;
                }
                else
                {
                    MessageBox.Show($"Giá trị phải từ 1 đến {soLuongSpTrongKho}!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số nguyên hợp lệ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return count == 1;
        }
        void loadChiTietHoaDonTheoMa()
        {
            txtMaHD.Text = maHD;

            dgvCTHD.DataSource = bus_cthd.GetListCTHDTheoMaHD(maHD);

            cboMaSP.DataSource = bus_cthd.GetListCTHDTheoMaHD(maHD);
            cboMaSP.DisplayMember = "MaSanPham";
            cboMaSP.ValueMember = "MaHD";

            cboMaKH.DataSource = bus_hd.GetHoaDonByMa(maHD);
            cboMaKH.DisplayMember = "TenKhachHang";
            cboMaKH.ValueMember = "MaHD";

            txtThanhTien.Text = bus_cthd.GetTotalCashByIdHd(maHD).ToString("N0");



        }
        private void Frm_ChiTietHoaDonFrm_Load(object sender, EventArgs e)
        {
            LoadFirst();
            loadChiTietHoaDonTheoMa();
            // MessageBox.Show(maHD);
            settingDGV();
            btnLuuCTHD.Enabled = false;

        }

        void settingDGV()
        {
            dgvCTHD.Columns["id"].Visible = false;
            dgvCTHD.Columns["MaHD"].HeaderText = "Mã hoá đơn";
            dgvCTHD.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            dgvCTHD.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvCTHD.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvCTHD.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            dgvCTHD.Columns["MaSanPham"].Visible = false;
            dgvCTHD.Columns["MaHoaDon"].Visible = false;
            dgvCTHD.Columns["SoLuong"].Name = "SoLuong";
            dgvCTHD.Columns["DonGia"].Name = "DonGia";
            dgvCTHD.Columns["ThanhTien"].Name = "ThanhTien";

            // --- Thiết lập kích thước & hiển thị ---

            dgvCTHD.EnableHeadersVisualStyles = false; // Quan trọng: phải đặt TRƯỚC khi đổi màu header
            dgvCTHD.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // --- Thiết lập giao diện header ---
            dgvCTHD.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvCTHD.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvCTHD.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvCTHD.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGreen; // Giữ nguyên khi chọn
            dgvCTHD.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // --- Kích thước ---
            dgvCTHD.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCTHD.ColumnHeadersHeight = 40;
            dgvCTHD.RowTemplate.Height = 35;
            dgvCTHD.AllowUserToResizeRows = false;
            dgvCTHD.AllowUserToResizeColumns = false;
            dgvCTHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvCTHD.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ô dữ liệu ---
            dgvCTHD.DefaultCellStyle.BackColor = Color.White;
            dgvCTHD.DefaultCellStyle.ForeColor = Color.Black;
            dgvCTHD.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dgvCTHD.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCTHD.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            ///DGV SAN PHAM

            dgvSP.Columns["MaSanPham"].HeaderText = "Mã sản phẩm";
            dgvSP.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            dgvSP.Columns["DonGia"].HeaderText = "Đơn giá";

            // --- Thiết lập kích thước & hiển thị ---
            // --- Bật tùy chỉnh header ---
            dgvSP.EnableHeadersVisualStyles = false; // Quan trọng: phải đặt TRƯỚC khi đổi màu header

            // --- Thiết lập giao diện header ---
            dgvSP.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvSP.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvSP.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvSP.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGreen; // Giữ nguyên khi chọn
            dgvSP.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // --- Kích thước ---
            dgvSP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvSP.ColumnHeadersHeight = 40;
            dgvSP.RowTemplate.Height = 35;
            dgvSP.AllowUserToResizeRows = false;
            dgvSP.AllowUserToResizeColumns = false;
            dgvSP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvSP.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ô dữ liệu ---
            dgvSP.DefaultCellStyle.BackColor = Color.White;
            dgvSP.DefaultCellStyle.ForeColor = Color.Black;
            dgvSP.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dgvSP.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvSP.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }





        private void btnCTHD_Click(object sender, EventArgs e)
        {
            //DeactiveControlsHD();
        }

        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maSP = "";
            donGia = 0;
            // Load số lượng từ CTHD vào txtSoLuong
            if (cboMaSP.SelectedValue != null && txtMaHD.Text != "")
            {
                maSP = cboMaSP.Text;
                int soLuong = bus_cthd.GetChiTietTheoMa(maHD, maSP);
                if (soLuong != null)
                {
                    txtSoLuong.Text = soLuong.ToString(); // nếu có dữ liệu
                }
                else
                {
                    txtSoLuong.Text = ""; // nếu không có dữ liệu
                }
            }


            dgvSP.DataSource = bus_cthd.LayDSSanPhamTheoMaHD(maHD);

            // Lấy đơn giá sản phẩm
            if (cboMaSP.SelectedValue == null) return;

            // Giả sử bạn có hàm lấy đơn giá theo mã SP
            donGia = bus_cthd.LayDonGiaTheoMaSP(maSP);
        }

        private void txtTimKiemSp_TextChanged(object sender, EventArgs e)
        {
            IQueryable kqDgvSP = null;
            IQueryable kqDgvCTHD = null;

            string tuKhoa = txtTimKiemSp.Text.Trim().ToLower();
            string luaChon = cboTimKiemSp.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(luaChon) && !string.IsNullOrEmpty(tuKhoa))
            {
                switch (luaChon)
                {
                    case "Tên sản phẩm":
                        kqDgvSP = bus_cthd.SreachSanPhamTheoMaSP(maHD, tuKhoa);
                        kqDgvCTHD = bus_cthd.SreachChiTietHoaDonTheoMaSP(maHD, tuKhoa);
                        break;

                    case "Đơn giá":
                        if (double.TryParse(tuKhoa, out double donGia))
                        {
                            kqDgvSP = bus_cthd.SreachSanPhamTheoDonGia(maHD, donGia);
                            kqDgvCTHD = bus_cthd.SreachChiTietHoaDonTheoDonGia(maHD, donGia);
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng nhập số khi tìm theo đơn giá!");
                            return;
                        }
                        break;
                }
            }

            // Nếu có kết quả → đổ dữ liệu ra DataGridView
            if (txtTimKiemSp.Text != "")
            {
                if (kqDgvSP != null && kqDgvCTHD != null)
                {
                    dgvSP.DataSource = kqDgvSP;
                    dgvCTHD.DataSource = kqDgvCTHD;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                dgvCTHD.DataSource = bus_cthd.GetListCTHDTheoMaHD(maHD);
                cboMaSP.DataSource = bus_cthd.GetListCTHDTheoMaHD(maHD);
            }
        }



        double thanhTien = 0;
        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {



        }


        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMaSP.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text.Trim(), out int soLuong) || soLuong < 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string maSP = cboMaSP.Text.Trim();

                // ✅ Lấy số lượng tồn kho thực tế
                int soLuongTon = bus_sp.LaySoLuongTonKho(maSP);
                if (soLuong > soLuongTon)
                {
                    MessageBox.Show($"Số lượng yêu cầu ({soLuong}) vượt quá số lượng tồn kho ({soLuongTon})!",
                                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy đơn giá
                double donGia = bus_cthd.LayDonGiaTheoMaSP(maSP);
                double thanhTien = soLuong * donGia;
                txtThanhTien.Text = thanhTien.ToString("N0");
                //MessageBox.Show($"Đơn giá: {donGia}, Thành tiền: {thanhTien} , số lượng: {soLuong}, mã hd: {maHD}");
                // Cập nhật chi tiết hoá đơn
                bool isValid_CTHD = bus_cthd.UpdateCTHD(maHD, maSP, soLuong);
                bool isValid_HD = bus_hd.updateThanhTienHD(maHD, thanhTien);

                if (isValid_CTHD && isValid_CTHD)
                {
                    MessageBox.Show("Cập nhật chi tiết hoá đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLuuCTHD.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Cập nhật chi tiết hoá đơn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnLuuCTHD_Click(object sender, EventArgs e)
        {
            try
            {
                string maHD = txtMaHD.Text.Trim();
                string maSP = cboMaSP.Text.Trim();

                if (string.IsNullOrEmpty(maHD) || string.IsNullOrEmpty(maSP))
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn và sản phẩm hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng không hợp lệ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 🔹 Lấy đơn giá từ BUS
                double donGia = bus_cthd.LayDonGiaTheoMaSP(maSP);
                double thanhTien = donGia * soLuong;

                // 🔹 Cập nhật chi tiết hóa đơn
                bool ketQuaCTHD = bus_cthd.UpdateCTHD(maHD, maSP, soLuong);
                bool ketQuaHD = bus_hd.updateThanhTienHD(maHD, thanhTien);
                if (!ketQuaHD && !ketQuaCTHD)
                {
                    MessageBox.Show("Cập nhật chi tiết hoá đơn thất bại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 🔹 Gọi DAL để TÍNH TỔNG TIỀN (dựa trên SoLuong * DonGia)
                decimal tongTien = bus_cthd.TinhTongTienTheoMaHD_TuSoLuongVaDonGia(maHD);

                // 🔹 Cập nhật tổng tiền xuống bảng HóaĐơn
                bus_hd.CapNhatTongTien(maHD, (double)tongTien);

                // 🔹 Hiển thị tổng tiền lên giao diện
                txtTongTien.Text = tongTien.ToString("N0", CultureInfo.CurrentCulture);

                // 🔹 Reload lại chi tiết hóa đơn sau khi cập nhật
                dgvCTHD.DataSource = bus_cthd.GetListCTHDTheoMaHD1(maHD).ToList();

                MessageBox.Show("Cập nhật chi tiết hoá đơn thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLuuCTHD.Enabled = false;
                btnCapNhat.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu chi tiết hóa đơn: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

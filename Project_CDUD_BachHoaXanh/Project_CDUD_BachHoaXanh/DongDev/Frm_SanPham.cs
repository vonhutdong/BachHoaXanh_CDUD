using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_SanPham : Form
    {
        //khoi tao
        int idNhaCungCap = 1;
        //bus nhà loại hàng
        BUS_LoaiHang bus_loaihang = new BUS_LoaiHang();
        BUS_KhuyenMai bus_loaiKM = new BUS_KhuyenMai();
        BUS_NhaCungCap bUS_NhaCungCap = new BUS_NhaCungCap();
        //bus sản phẩm
        BUS_SanPham bus_sanpham = new BUS_SanPham();
        //di san pham hien tai
        string currentID = "";
        //binary img data
        byte[] imageData = null;
        public Frm_SanPham()
        {
            InitializeComponent();
        }

        
       void LoadData()
        {
            //load dữ liệu loại hàng lên combobox
            cbMaNhomHang.DataSource = bus_loaihang.LayDSLH();
            cbMaNhomHang.DisplayMember = "TenLoaiHang";
            cbMaNhomHang.ValueMember = "MaLoaiHang";

            //load dữ liệu nhà cung cấp lên combobox
            cbNhaCungCap.DataSource = bUS_NhaCungCap.LayDSNCC();
            cbNhaCungCap.DisplayMember = "TenNCC";
            cbNhaCungCap.ValueMember = "MaNCC";

            cboKhuyenMai.DataSource = bus_loaiKM.GetListKM();
            cboKhuyenMai.DisplayMember = "GiaTri";
            cboKhuyenMai.ValueMember = "MaKhuyenMai";

            dgvSP.DataSource = bus_sanpham.LoadDSSanPham();
            //ẩn cột không cần thiết
            dgvSP.Columns["MaNhaCungCap"].Visible = false;
            dgvSP.Columns["MaLoaiHang"].Visible = false;
            dgvSP.Columns["KhuyenMai"].Visible = false;
            dgvSP.Columns["MaKhuyenMai"].Visible = false;
            dgvSP.Columns["AnhSanPham"].Visible = false;
            dgvSP.Columns["NhaCungCap"].Visible = false;
            dgvSP.Columns["LoaiHang"].Visible = false;
            //đôi tên header
            dgvSP.Columns["MaSanPham"].HeaderText = "Mã sản phẩm";
            dgvSP.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            dgvSP.Columns["DonViTinh"].HeaderText = "Đơn vị tính";
            dgvSP.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvSP.Columns["NgaySanXuat"].HeaderText = "Ngày sản xuất";
            dgvSP.Columns["HanSuDung"].HeaderText = "Hạn sử dụng";
            dgvSP.Columns["NhaCungCap"].HeaderText = "Nhà cung cấp";
            dgvSP.Columns["MaLoaiHang"].HeaderText = "Loại hàng";
            //format date
            dgvSP.Columns["NgaySanXuat"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvSP.Columns["HanSuDung"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvSP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSP.ColumnHeadersHeight = 30; // hoặc cao hơn

            dgvSP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvSP.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvSP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // -    -- Tăng chiều cao hàng ---
            dgvSP.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvSP.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvSP.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvSP.DefaultCellStyle.BackColor = Color.White;
            dgvSP.DefaultCellStyle.ForeColor = Color.Black;
            dgvSP.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvSP.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvSP.EnableHeadersVisualStyles = false;
            dgvSP.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvSP.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvSP.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvSP.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }
        private void Frm_SanPham_Load(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void dgvSP_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu DataGridView trống hoặc không có dòng hiện tại
                if (dgvSP.CurrentRow == null || dgvSP.Rows.Count == 0)
                    return;

                int dong = dgvSP.CurrentRow.Index;
                if (dong < 0) return;

                DataGridViewRow row = dgvSP.Rows[dong];

                // Hàm phụ để lấy giá trị an toàn
                string GetValue(string columnName) =>
                dgvSP.Columns.Contains(columnName) && row.Cells[columnName].Value != null
                    ? row.Cells[columnName].Value.ToString()
                    : string.Empty;


                // Đổ dữ liệu vào control
                txtMaSanPham.Text = GetValue("MaSanPham");
                txtTenSanPham.Text = GetValue("TenSanPham");
                txtDonViTinh.Text = GetValue("DonViTinh");
                txtDonGia.Text = GetValue("DonGia");

                // Combobox
                string maLoaiHang = GetValue("MaLoaiHang");
                if (!string.IsNullOrEmpty(maLoaiHang))
                    cbMaNhomHang.SelectedValue = maLoaiHang;

                string maNCC = GetValue("MaNhaCungCap");
                if (!string.IsNullOrEmpty(maNCC))
                    cbNhaCungCap.SelectedValue = maNCC;

                string maKM = GetValue("MaKhuyenMai");
                if (!string.IsNullOrEmpty(maKM) && cboKhuyenMai.Items.Count > 0)
                    cboKhuyenMai.SelectedValue = maKM;
                else
                    cboKhuyenMai.SelectedIndex = -1;

                // DateTimePicker
                DateTime ngaySX, hanSD;
                if (DateTime.TryParse(GetValue("NgaySanXuat"), out ngaySX))
                    dtpNgaySanXuat.Value = ngaySX;
                if (DateTime.TryParse(GetValue("HanSuDung"), out hanSD))
                    dtpHanSuDung.Value = hanSD;

                // Ảnh sản phẩm
                // Giả sử dgvSP là tên DataGridView
                var data = dgvSP.Columns.Contains("AnhSanPham")
                    ? row.Cells["AnhSanPham"].Value
                    : null;


                if (data != null && data is System.Data.Linq.Binary binaryData)
                {
                    byte[] byteArray = binaryData.ToArray();
                    imageData = byteArray;

                    using (MemoryStream ms = new MemoryStream(byteArray))
                    {
                        try
                        {
                            imgSanPham.Image = Image.FromStream(ms);
                        }
                        catch
                        {
                            imgSanPham.Image = null; // nếu ảnh lỗi
                        }
                    }
                }
                else
                {
                    imgSanPham.Image = null;
                }

                // Lưu ID hiện tại
                currentID = txtMaSanPham.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn sản phẩm: " + ex.Message,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private byte[] ConvertImageToByteArray(string fileName)
        {
            // Đọc file ảnh và chuyển thành mảng byte
            return File.ReadAllBytes(fileName);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        void LamMoi()
        {
            LoadData();
            cboKhuyenMai.SelectedIndex = -1;
            cbNhaCungCap.SelectedIndex = -1;
            cbMaNhomHang.SelectedIndex = -1;
            txtDonGia.Text = "";
            txtDonViTinh.Text = "";
            txtTenSanPham.Text = "";
            txtTenSanPham.Focus();
            dtpHanSuDung.Value = DateTime.Now;
            dtpNgaySanXuat.Value = DateTime.Now;
            if (imgSanPham.Image != null)
            {
                imgSanPham.Image.Dispose(); // Giải phóng ảnh cũ tránh lỗi file đang mở
            }
            imgSanPham.Image = null;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // 🔹 Kiểm tra dữ liệu rỗng
                if (string.IsNullOrWhiteSpace(txtTenSanPham.Text) ||
                    string.IsNullOrWhiteSpace(txtDonViTinh.Text) ||
                    string.IsNullOrWhiteSpace(txtDonGia.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm!",
                                    "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔹 Kiểm tra giá có hợp lệ không
                if (!float.TryParse(txtDonGia.Text, out float donGia) || donGia <= 0)
                {
                    MessageBox.Show("Đơn giá phải là số hợp lệ và lớn hơn 0!",
                                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔹 Kiểm tra ảnh
                if (imageData == null)
                {
                    MessageBox.Show("Vui lòng chọn hình ảnh sản phẩm!",
                                    "Thiếu hình ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔹 Kiểm tra ngày sản xuất và hạn sử dụng
                DateTime ngaySX = dtpNgaySanXuat.Value.Date;
                DateTime hanSD = dtpHanSuDung.Value.Date;

                if (ngaySX >= hanSD)
                {
                    MessageBox.Show("Ngày sản xuất phải nhỏ hơn hạn sử dụng!",
                                    "Lỗi ngày tháng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔹 Lấy dữ liệu đầu vào
                string tenSP = txtTenSanPham.Text.Trim();
                string donViTinh = txtDonViTinh.Text.Trim();
                string maLoaiHang = cbMaNhomHang.SelectedValue?.ToString();
                string maNCC = cbNhaCungCap.SelectedValue?.ToString();
                string maKM = cboKhuyenMai.SelectedValue?.ToString();

                // 🔹 Kiểm tra trùng sản phẩm
                if (bus_sanpham.KiemTraTrung(tenSP, donViTinh, donGia))
                {
                    MessageBox.Show("Sản phẩm này đã tồn tại (trùng tên, đơn vị tính, đơn giá)!",
                                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🔹 Tạo đối tượng sản phẩm
                DTO_SanPham sp = new DTO_SanPham(
                    tenSP,
                    donViTinh,
                    donGia,
                    ngaySX,
                    hanSD,
                    maLoaiHang,
                    maNCC,
                    maKM,
                    imageData
                );

                // 🔹 Thêm sản phẩm
                bool kq = bus_sanpham.ThemSanPham(sp);

                if (kq)
                {
                    MessageBox.Show($"Thêm sản phẩm '{tenSP}' thành công!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại, vui lòng thử lại!",
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}",
                                "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Chọn ảnh sản phẩm";
                    ofd.Filter = "Ảnh (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                    ofd.Multiselect = false;

                    if (ofd.ShowDialog() != DialogResult.OK)
                        return; // Người dùng bấm Cancel thì thoát luôn

                    // Đọc ảnh mà không khóa file gốc
                    using (var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        imgSanPham.Image = Image.FromStream(fs);
                    }

                    imgSanPham.SizeMode = PictureBoxSizeMode.Zoom; // Giữ tỉ lệ ảnh, tránh méo

                    // Lưu lại đường dẫn nếu cần (tùy mục đích)
                    string filePath = ofd.FileName;

                    // Chuyển đổi ảnh sang byte[] để lưu vào CSDL
                    imageData = File.ReadAllBytes(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string tenSP = txtTenSanPham.Text.Trim();
            string donViTinh = txtDonViTinh.Text.Trim();
            float donGia = float.Parse(txtDonGia.Text);

            try
            {
                // 🟢 Kiểm tra nếu chưa chọn sản phẩm
                if (string.IsNullOrEmpty(currentID))
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🟢 Lấy giá trị từ các combobox (nếu null -> gán rỗng)
                string maLoaiHang = cbMaNhomHang.SelectedValue != null ? cbMaNhomHang.SelectedValue.ToString() : "";
                string maNhaCungCap = cbNhaCungCap.SelectedValue != null ? cbNhaCungCap.SelectedValue.ToString() : "";
                string maKhuyenMai = cboKhuyenMai.SelectedValue != null ? cboKhuyenMai.SelectedValue.ToString() : null;

                // 🟢 Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtTenSanPham.Text) ||
                    string.IsNullOrWhiteSpace(txtDonViTinh.Text) ||
                    string.IsNullOrWhiteSpace(txtDonGia.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //kiem tra trung
                if (bus_sanpham.KiemTraTrung(tenSP, donViTinh, donGia))
                {
                    MessageBox.Show("Sản phẩm này đã tồn tại (trùng tên, đơn vị tính, đơn giá)!",
                                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // 🟢 Kiểm tra định dạng đơn giá
                if (!float.TryParse(txtDonGia.Text, out donGia) || donGia <= 0)
                {
                    MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🟢 Kiểm tra ngày hợp lệ
                if (dtpHanSuDung.Value <= dtpNgaySanXuat.Value)
                {
                    MessageBox.Show("Hạn sử dụng phải lớn hơn ngày sản xuất!", "Lỗi ngày tháng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🟢 Kiểm tra ảnh
                if (imageData == null)
                {
                    MessageBox.Show("Vui lòng chọn ảnh sản phẩm!", "Thiếu ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🟢 Gọi hàm cập nhật
                DTO_SanPham sp = new DTO_SanPham(
                    currentID,
                    txtTenSanPham.Text.Trim(),
                    txtDonViTinh.Text.Trim(),
                    donGia,
                    dtpNgaySanXuat.Value.Date,
                    dtpHanSuDung.Value.Date,
                    maLoaiHang,
                    maNhaCungCap,
                    maKhuyenMai, // có thể null
                    imageData
                );
                
                bool kq = bus_sanpham.SuaSanPham(sp);

                if (kq)
                {
                    MessageBox.Show($"✅ Sửa thành công sản phẩm: {currentID}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("❌ Sửa sản phẩm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // 🟠 Xử lý ngoại lệ tổng quát
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Hỏi người dùng xác nhận
                DialogResult rs = MessageBox.Show("Bạn có chắc xóa không?", "Thông báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rs == DialogResult.Yes)
                {
                    // Kiểm tra xem đã chọn sản phẩm chưa
                    if (string.IsNullOrEmpty(currentID))
                    {
                        MessageBox.Show("Vui lòng chọn dữ liệu cần xóa!", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    

                    // Gọi hàm xóa
                    bool kq = bus_sanpham.XoaSanPham(currentID);

                    if (kq)
                    {
                        MessageBox.Show($"Xóa thành công sản phẩm {txtTenSanPham.Text}", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LamMoi();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa sản phẩm. Có thể đang được sử dụng ở bảng khác!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa sản phẩm: " + ex.Message, "Lỗi hệ thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

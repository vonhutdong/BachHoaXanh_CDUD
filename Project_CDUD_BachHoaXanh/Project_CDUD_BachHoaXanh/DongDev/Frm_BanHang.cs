using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAL;
using DTO;
using Project_CDUD_BachHoaXanh.TrongDev;


namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_BanHang : Form
    {
        //biến cờ;
        bool isClick = false;
        //bus loai hàng
        BUS_LoaiHang bus_loaihang = new BUS_LoaiHang();
        //bus nhà cung cấp
        BUS_NhaCungCap bus_nhacungcap = new BUS_NhaCungCap();
        //bus nhà sản phẩm
        BUS_SanPham bus_SanPham = new BUS_SanPham();
        //bus nhà kho hàng
        BUS_KhoHang bus_khohang = new BUS_KhoHang();
        //bus hóa đơn
        BUS_HoaDon bus_HoaDon = new BUS_HoaDon();
        //bus chi tiết hóa đơn
        BUS_ChiTietHoa bus_chitiethoadon = new BUS_ChiTietHoa();
        //mảng sản phẩm
        List<DTO_SanPhamKhoHang> sanPhams;
        List<DTO_SanPhamKhoHang> TimKiemMasanPhams;
        //format tien
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
        //currentGia
        double giaHientai = 0;
        //idHienTai
        string mSPHienTai = "";
        //thanh tiền
        double thanhTien = 0;
        string maKhachHang = "KH000";
        string maNV = "NV001";
        // bus khach hàng
        BUS_KhachHang bus_KhachHang = new BUS_KhachHang();
        //khach hàng
        DTO_KhachHang khachhang = new DTO_KhachHang();
        //DTO Nhan vien
        DTO_NhanVien nhanvien;
        //bus Khuyen mai
        BUS_KhuyenMai bus_khuyenmai;
        string phuongTTT = "";
        float tongSauGiam = 0;
        float tongTienChuaGiam = 0;
        public Frm_BanHang()
        {
            InitializeComponent();

            sanPhams = bus_SanPham.ListSP_BH();
            //MessageBox.Show("Tổng số sản phẩm: " + sanPhams.Count);

            dgvThongTinHoaDon.Columns["MaSanPham"].Visible = false;
            lbThanhTien.Text = "0 VNĐ";
            txtTienDua.MaxLength = 8;
            nhanvien = Frm_TrangChu.NhanVien;
        }

        private void LoadCbLoaiSanPham()
        {
            //lấy danh sach loại hàng
            cboLoaiSanPham.DataSource = bus_loaihang.LayDSLH();
            cboLoaiSanPham.DisplayMember = "TenLoaiHang";
            cboLoaiSanPham.ValueMember = "MaLoaiHang";

        }

        private void cboPhuongThucTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPT = cboPhuongThucTT.SelectedItem.ToString();

            if (selectedPT == "Tiền mặt")
            {
                // Hiện textbox Tiền khách đưa + Tiền thừa
                txtTienDua.Visible = true;
                lblTienKhachDua.Visible = true;

                txtTienThua.Visible = true;
                lblTienThua.Visible = true;
            }
            else
            {
                // Ẩn textbox khi không phải tiền mặt
                txtTienDua.Visible = false;
                lblTienKhachDua.Visible = false;

                txtTienThua.Visible = false;
                lblTienThua.Visible = false;

                // Reset giá trị
                txtTienDua.Text = "";
                txtTienThua.Text = "";
            }
        }

        private void Frm_BanHang_Load(object sender, EventArgs e)
        {
            cboPhuongThucTT.Items.Add("Tiền mặt");
            cboPhuongThucTT.Items.Add("Chuyển khoản");

            // Thiết lập lại style để dữ liệu hiện rõ
            dgvThongTinHoaDon.DefaultCellStyle.BackColor = Color.White;
            dgvThongTinHoaDon.DefaultCellStyle.ForeColor = Color.Black;
            dgvThongTinHoaDon.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvThongTinHoaDon.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvThongTinHoaDon.EnableHeadersVisualStyles = false;
            dgvThongTinHoaDon.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvThongTinHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            LoadCbLoaiSanPham();
            TaiDanhSachSanPhamTuDatabase();
            LayDSKhachHangMatDinh();
            LoadLayoutSanPham(sanPhams);
            cboPhuongThucTT.SelectedIndex = 0;
        }
        //private void LoadDgvSanPhamKhuyenMai(List<DTO_SanPhamKhoHang> listSP)
        //{
        //    var listCoKM = listSP.Where(sp => sp.MaKhuyenMai != null).ToList();
        //    layoutSanPhamKhuyenMai.DataSource = listCoKM;
        //}
        private void LayDSKhachHangMatDinh()
        {
            khachhang = bus_KhachHang.LayKHMacDinh(maKhachHang);
            if (khachhang != null)
            {
                txtTenKH.Text = khachhang.TenKH;
                txtSDT.Text = khachhang.SoDienThoai;
            }
        }
        private void TaiDanhSachSanPhamTuDatabase()
        {
            try
            {
                sanPhams = bus_SanPham.ListSP_BH(); // lấy danh sách mới từ database
                LoadLayoutSanPham(sanPhams);         // load lại giao diện
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadLayoutSanPham(List<DTO_SanPhamKhoHang> listSanPham)
        {
            //làm sạch
            layoutSanPham.Controls.Clear();
            foreach (DTO_SanPhamKhoHang sanpham in listSanPham)
            {
                double phanTramKM = sanpham.PhanTramKM;
                //tạo thẻ sản phẩm
                ItemSP_new item = new ItemSP_new();
                item.TenSanPham = sanpham.TenSanPham;
                item.Gia = "Giá: " + sanpham.GiaBan.ToString("#,###", cul.NumberFormat) + " VNĐ";
                item.SoLuong = "Sẵn có: " + sanpham.SoLuong.ToString();
                item.Size = new Size(100, 110);
                item.Name = sanpham.MaSanPham.ToString();
                item.sale = "Sale-off: " + phanTramKM + "%";
                var data = sanpham.AnhSanPham;
                // Kiểm tra kiểu dữ liệu của imageData
                if (data != null)
                {
                    if (data is System.Data.Linq.Binary binaryData)
                    {
                        byte[] byteArray = binaryData.ToArray(); // Chuyển đổi sang byte array
                                                                 //gán giá trị để kiểm tra
                                                                 //imageData = byteArray;
                        using (MemoryStream ms = new MemoryStream(byteArray))
                        {
                            Image image = Image.FromStream(ms);
                            item.AnhSanPham = image; // Gán hình ảnh vào PictureBox
                        }
                    }
                }
                // ============================
                // KIỂM TRA SẢN PHẨM KHUYẾN MÃI
                // ============================
                if (phanTramKM > 0)
                {
                    // Hiện sale
                    item.sale = "Sale-off: " + phanTramKM + "%";
                    item.lbSale.Visible = true;

                    // Đưa vào layout sản phẩm khuyến mãi
                    layoutSanPham.Controls.Add(item);
                }
                else
                {
                    // Không có KM → ẩn label sale
                    item.lbSale.Visible = false;

                    // Đưa vào layout sản phẩm thường
                    layoutSanPham.Controls.Add(item);
                }

                // Gán sự kiện click cho toàn bộ item
                item.Click += new EventHandler(Item_Click);

                // Gán sự kiện cho các control bên trong và truyền CardItem làm sender
                item.pbSanPham.Click += (s, e) => Item_Click(item, e);
                item.lbGia.Click += (s, e) => Item_Click(item, e);
                item.lbTenSanPham.Click += (s, e) => Item_Click(item, e);
                item.lbSoLuong.Click += (s, e) => Item_Click(item, e);
                //wq
                layoutSanPham.Controls.Add(item);
            }

        }
        private void Item_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạm thời hủy đăng ký sự kiện txtMaSanPham.TextChanged
                txtMaSanPham.TextChanged -= txtMaSanPham_TextChanged;
                ItemSP_new cardItem = (ItemSP_new)sender;
                DTO_SanPhamKhoHang sp = sanPhams.Find(p => p.MaSanPham == cardItem.Name.ToString());
                txtTenSanPham.Text = sp.TenSanPham;
                txtGia.Text = sp.GiaBan.ToString("#,###", cul.NumberFormat);
                txtMaSanPham.Text = sp.MaSanPham;
                giaHientai = sp.GiaBan;
                mSPHienTai = sp.MaSanPham;
                txtSoLuong.Text = "1";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Xảy ra lỗi: " + ex.Message, "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtMaSanPham.TextChanged += txtMaSanPham_TextChanged;
            }
        }

        private void txtMaSanPham_TextChanged(object sender, EventArgs e)
        {
            LoadLayoutSanPham(timKiemSanPhamBangMa(txtMaSanPham.Text));
        }
        private List<DTO_SanPhamKhoHang> timKiemSanPhamBangMa(string tukhoa)
        {
            var ketQua = sanPhams.Where(sp => sp.MaSanPham.ToLower().Contains(tukhoa.ToLower())).ToList();
            return ketQua;
        }
        private bool KtMaSanPhamCoTrung(string maSanPham)
        {
            bool flag = false;
            for (int i = 0; i < dgvThongTinHoaDon.Rows.Count; i++)
            {
                if (dgvThongTinHoaDon.Rows[i].Cells["MaSanPham"].Value.ToString() == maSanPham)
                {
                    flag = true;
                }
            }
            return flag;
        }
        private void XuLiThemTrungMa(string maSanPham, int soLuong)
        {
            for (int i = 0; i < dgvThongTinHoaDon.Rows.Count; i++)
            {
                if (dgvThongTinHoaDon.Rows[i].Cells["MaSanPham"].Value.ToString() == maSanPham)
                {
                    DTO_SanPhamKhoHang sp = sanPhams.Find(x => x.MaSanPham == maSanPham);
                    dgvThongTinHoaDon.Rows[i].Cells["SoLuong"].Value = soLuong + int.Parse(dgvThongTinHoaDon.Rows[i].Cells["SoLuong"].Value.ToString());
                    dgvThongTinHoaDon.Rows[i].Cells["TongTien"].Value = (int.Parse(dgvThongTinHoaDon.Rows[i].Cells["SoLuong"].Value.ToString()) * sp.GiaBan).ToString("#,###", cul.NumberFormat);

                    //thanhTien += float.Parse(dgvDSHangMua.Rows[i].Cells[4].Value.ToString());

                }
            }
        }
        private string tinhThanhTien()
        {
            double tongTien = 0;
            double thanhTienSauGiam = 0;

            // Lấy khách hàng
            khachhang = bus_KhachHang.LayKhachHang_SDT(txtSDT.Text);

            // 1) TÍNH TỔNG TIỀN
            for (int i = 0; i < dgvThongTinHoaDon.Rows.Count; i++)
            {
                string maSP = dgvThongTinHoaDon.Rows[i].Cells["MaSanPham"].Value.ToString();
                int soLuong = int.Parse(dgvThongTinHoaDon.Rows[i].Cells["SoLuong"].Value.ToString());

                DTO_SanPhamKhoHang sanpham = sanPhams.Find(x => x.MaSanPham == maSP);

                if (sanpham != null)
                    tongTien += soLuong * sanpham.GiaBan;
            }
            tongTienChuaGiam = (float)tongTien;
            // 2) TÍNH GIẢM GIÁ THEO CẤP BẬC
            double giamPhanTram = 0;

            if (khachhang != null)
            {
                switch (khachhang.CapBac?.Trim())
                {
                    case "Bạc":
                        giamPhanTram = 0.02;
                        break;
                    case "Vàng":
                        giamPhanTram = 0.05;
                        break;
                    case "Kim cương":
                        giamPhanTram = 0.10;
                        break;
                }
            }

            // 3) ÁP DỤNG GIẢM GIÁ
            double soTienGiam = tongTien * giamPhanTram;
            thanhTienSauGiam = tongTien - soTienGiam;

            // Gán ra biến toàn cục nếu cần
            thanhTien = thanhTienSauGiam;

            // 4) TRẢ VỀ CHUỖI HIỂN THỊ
            return thanhTienSauGiam.ToString("#,###", cul.NumberFormat) + " VNĐ";
        }

        private void mauTienDua_TinhTienThua()
        {
            if (string.IsNullOrWhiteSpace(txtTienDua.Text))
            {
                txtTienThua.Text = "0 VNĐ";
                return;
            }

            string raw = txtTienDua.Text.Replace(".", "").Trim();

            if (!decimal.TryParse(raw, out decimal tienDua))
                return;

            // 🔥 Nếu đã có giảm giá thì tongSauGiam > 0
            double tongPhaiTra = tongSauGiam > 0 ? tongSauGiam : thanhTien;

            decimal tienThua = tienDua - (decimal)tongPhaiTra;

            // Nếu khách đưa chưa đủ
            if (tienThua < 0)
            {
                txtTienThua.Text = "Chưa đủ";
                return;
            }

            txtTienThua.Text = tienThua.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " VNĐ";
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
            //làm sạch dgv
            //dgvThongTinHoaDon.Rows.Clear();
            //đặt lại thành tiền sau khi thanh toán thành công
            //lbThanhTien.Text = "0 VNĐ";
        }
        private void LamMoi()
        {
            txtGia.Clear();
            txtMaSanPham.Clear();
            txtGia.Clear();
            txtSoLuong.Clear();
            txtTenSanPham.Clear();
            txtTienDua.Clear();
            txtTienThua.Clear();
            ////xem lại tiền đưa
            mauTienDua_TinhTienThua();

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            

            try
            {
                if (txtMaSanPham.Text.Length > 0)
                {
                    DTO_SanPhamKhoHang sanPham = sanPhams.Find(sp => sp.MaSanPham == txtMaSanPham.Text);
                    if (sanPham != null)
                    {
                        if (txtSoLuong.Text.Trim().Length == 0)
                        {
                            throw new Exception("Vui lòng nhập số lượng!");
                        }
                        if (int.Parse(txtSoLuong.Text) <= 0)
                        {
                            throw new Exception("Số lượng không hợp lệ!");
                        }
                        if (sanPham.SoLuong >= int.Parse(txtSoLuong.Text))
                        {
                            if (KtMaSanPhamCoTrung(sanPham.MaSanPham))
                            {
                                XuLiThemTrungMa(sanPham.MaSanPham, int.Parse(txtSoLuong.Text));

                            }
                            else
                            {
                                DataGridViewRow newRow = new DataGridViewRow();
                                newRow.CreateCells(dgvThongTinHoaDon);
                                newRow.Cells[0].Value = txtTenSanPham.Text;
                                newRow.Cells[1].Value = txtGia.Text;
                                newRow.Cells[2].Value = txtSoLuong.Text;
                                newRow.Cells[3].Value = (giaHientai * float.Parse(txtSoLuong.Text)).ToString("#,###", cul.NumberFormat);
                                newRow.Cells[4].Value = sanPham.MaSanPham;

                                dgvThongTinHoaDon.Rows.Add(newRow);
                            }
                            //Cập nhật số lượng trong list
                            sanPham.SoLuong -= int.Parse(txtSoLuong.Text);
                            //cập nhật list
                            //LoadLayoutSanPham(sanPhams);
                            //lb thanh tien
                            

                            lbThanhTien.Text = tinhThanhTien();
                            //làm mới
                            LamMoi();

                        }
                        else
                        {
                            MessageBox.Show("Số lượng trong kho không đủ vui lòng kiểm tra lại!", "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mã sản phẩm không hợp lệ!", "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm", "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTienDua_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTienDua.TextChanged -= txtTienDua_TextChanged;

                if (dgvThongTinHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Thông tin hóa đơn rỗng. Vui lòng kiểm tra lại!",
                        "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtTienDua.Clear();
                    return;
                }

                // 👉 Tính tiền thừa đúng theo giảm giá
                mauTienDua_TinhTienThua();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thoát", MessageBoxButtons.OK);
            }
            finally
            {
                txtTienDua.TextChanged += txtTienDua_TextChanged;
            }
        }


        private void txtTienDua_Leave(object sender, EventArgs e)
        {
            // Lấy chuỗi hiện tại và loại bỏ dấu phân cách
            string text = txtTienDua.Text.Replace(".", "").Trim();

            if (decimal.TryParse(text, out decimal amount))
            {
                // 1) Định dạng lại tiền khách đưa
                txtTienDua.Text = string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:N0}", amount);
                txtTienDua.SelectionStart = txtTienDua.Text.Length;

                // 2) Kiểm tra tổng sau giảm
                double tongPhaiTra = tongSauGiam > 0 ? tongSauGiam : thanhTien;

                // 3) So sánh xem đủ tiền hay chưa
                if (amount < (decimal)tongPhaiTra)
                {
                    MessageBox.Show(
                        "Số tiền khách đưa không đủ so với tổng cần thanh toán!",
                        "Thiếu tiền",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                // 4) Tính tiền thừa và cập nhật label (nếu bạn có label thối tiền)
                decimal tienThua = amount - (decimal)tongPhaiTra;

                txtTienThua.Text = tienThua.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " VNĐ";
            }
        }


        private void txtTienDua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public void ThemChiTietHoaDon(string maHoaDon)
        {
            if (string.IsNullOrWhiteSpace(maHoaDon))
                throw new Exception("Mã hóa đơn không hợp lệ!");

            for (int i = 0; i < dgvThongTinHoaDon.Rows.Count; i++)
            {
                string maSanPham = dgvThongTinHoaDon.Rows[i].Cells["MaSanPham"].Value.ToString();
                int soLuong = int.Parse(dgvThongTinHoaDon.Rows[i].Cells["SoLuong"].Value.ToString());

                DTO_ChiTietHoaDon dong = new DTO_ChiTietHoaDon(soLuong, maHoaDon, maSanPham);
                bus_chitiethoadon.AddCTHD2(dong);

                // Cập nhật tồn kho
                bus_khohang.CapNhatSoLuong(maSanPham, soLuong);
            }

            // 🧾 Cập nhật tổng tiền hóa đơn
            bus_HoaDon.UpdateTotalCash2(maHoaDon,khachhang.MaKH);

            // (Tùy chọn) In hóa đơn
            // frm_InHD f = new frm_InHD(maHoaDon);
            // f.Show();

            MessageBox.Show("Thêm chi tiết hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private List<DTO_SanPhamKhoHang> timKiemSanPhamBangLoaiHang(string maLoaiHang)
        {
            var ketQua = sanPhams.Where(sp => sp.MaLoaiHang == maLoaiHang).ToList();
            return ketQua;
        }

        private void ckLocSP_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckLocSP.Checked)
                {
                    LoadLayoutSanPham(timKiemSanPhamBangLoaiHang(cboLoaiSanPham.SelectedValue.ToString()));
                }
                else
                {
                    LoadLayoutSanPham(sanPhams);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboLoaiSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckLocSP.Checked)
                {
                    LoadLayoutSanPham(timKiemSanPhamBangLoaiHang(cboLoaiSanPham.SelectedValue.ToString()));
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (dgvThongTinHoaDon.Rows.Count == 0)
            {
                MessageBox.Show("Thông tin hóa đơn rỗng. Vui lòng kiểm tra lại!", "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult resuft = MessageBox.Show("Bạn có chắc không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resuft == DialogResult.Yes)
            {
                dgvThongTinHoaDon.Rows.Clear();
                thanhTien = 0;
                lbThanhTien.Text = "0 VNĐ";
                LamMoi();
                LoadLayoutSanPham(sanPhams);
            }
        }
        private string ThanhToanCoKhachHang(string maKhachHang, double tongTruocGiam, double tongSauGiam, double soTienGiam)
        {
            // 🧾 1. Tạo hóa đơn với đủ thông tin
            DTO_HoaDon hoaDon = new DTO_HoaDon(
                maKhachHang,
                DTO_Session.MaNhanVien,
                phuongTTT
            );
            //bus_HoaDon.UpdateTotalCash2(hoaDon);
            // 🧾 2. Gọi hàm thêm hóa đơn
            string maHD = bus_HoaDon.AddHD2(hoaDon);

            // 🧩 3. Thêm chi tiết hóa đơn
            ThemChiTietHoaDon(maHD);

            // 💎 4. Cập nhật điểm thưởng
            int diemCongThem = (int)(tongSauGiam / 10000);
            bus_KhachHang.DiemCong(maKhachHang, diemCongThem);

            // 🎉 5. Thông báo + reset
            MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LamMoi();
            dgvThongTinHoaDon.Rows.Clear();
            lbThanhTien.Text = "0 VNĐ";
            return maHD;
        }



        private void ThanhToan()
        {
            //if (nhanvien != null)
            //{
            //    bus_HoaDon.AddHD2(new DTO_HoaDon(1, 1, nhanvien.Id));
            //}
            //else
            //{
            //    bus_HoaDon.AddHD2(new DTO_HoaDon(1, 1, 1));
            //}
            string maHD = bus_HoaDon.AddHD2(new DTO_HoaDon(maKhachHang, DTO_Session.MaNhanVien, phuongTTT));
            MessageBox.Show("NhanVienId hiện tại: " + DTO_Session.MaNhanVien);

            MessageBox.Show("Thanh toán thành công!", "Thoát", MessageBoxButtons.OK);
            //thêm hóa đơn

            //thêm chi tiết hóa đơn
            ThemChiTietHoaDon(maHD);

            LamMoi();
            //làm sạch dgv
            dgvThongTinHoaDon.Rows.Clear();
            //đặt lại thành tiền sau khi thanh toán thành công
            lbThanhTien.Text = "0 VNĐ";
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string maHoaDonMoiTao = "";
            phuongTTT = cboPhuongThucTT.SelectedItem?.ToString();

            try
            {
                if (dgvThongTinHoaDon.Rows.Count == 0)
                    throw new Exception("Thông tin hóa đơn rỗng. Vui lòng kiểm tra lại!");

                // Kiểm tra phương thức thanh toán
                if (phuongTTT != null && phuongTTT.Equals("Chuyển khoản", StringComparison.OrdinalIgnoreCase))
                {
                    if (MessageBox.Show("Bạn có chắc chắn khách hàng đã chuyển khoản?",
                        "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
                else
                {
                    if (txtTienDua.Text.Length == 0)
                        throw new Exception("Hãy nhập số tiền khách trả!");

                    double tienDua = double.Parse(txtTienDua.Text.Replace(".", ""));
                    if (tienDua < thanhTien) // thanhTien ĐÃ LÀ SAU GIẢM
                        throw new Exception("Vui lòng nhập đủ số tiền!");
                }

                // ⚡ GIÁ TRỊ ĐÃ TÍNH SẴN
                double tongTruocGiam = tongTienChuaGiam;  // bạn tự lấy ở hàm tính tiền
                double tongSauGiam = thanhTien;           // đã giảm
                double soTienGiam = tongTruocGiam - tongSauGiam;

                // 👉 LƯU VÀO HÓA ĐƠN
                if (khachhang != null)
                {
                    maHoaDonMoiTao = ThanhToanCoKhachHang(
                        khachhang.MaKH,
                        tongTruocGiam,
                        tongSauGiam,
                        soTienGiam
                    );
                    Frm_InHoaDon f = new Frm_InHoaDon(maHoaDonMoiTao);
                    f.ShowDialog();
                }
                else
                {
                    ThanhToan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void txtSDT_MouseLeave(object sender, EventArgs e)
        {
            
            khachhang = bus_KhachHang.LayKhachHang_SDT(txtSDT.Text);

            //MessageBox.Show(khachhang.TenKhachHang);
            if (khachhang != null)
            {
                txtTenKH.Text = khachhang.TenKH; 
            }
            else
            {
                txtTenKH.Text = "";
            }
        }
            

        private void dgvThongTinHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvThongTinHoaDon.CurrentRow != null)
            {
                //string maSP = dgvThongTinHoaDon.CurrentRow.Cells["MaSanPham"].Value.ToString();
                txtMaSanPham.Text = dgvThongTinHoaDon.CurrentRow.Cells["MaSanPham"].Value.ToString();
                txtTenSanPham.Text = dgvThongTinHoaDon.CurrentRow.Cells["TenSanPham"].Value.ToString();
                txtGia.Text = txtGia.Text = dgvThongTinHoaDon.CurrentRow.Cells["DonGia"].Value.ToString();
                txtSoLuong.Text = dgvThongTinHoaDon.CurrentRow.Cells["SoLuong"].Value.ToString();
            }
        }

        private void btnXoaSanPhamDangChon_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dgvThongTinHoaDon.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa sản phẩm này khỏi hóa đơn?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    int index = dgvThongTinHoaDon.CurrentRow.Index;
                    dgvThongTinHoaDon.Rows.RemoveAt(index);

                    // Nếu có biến tổng tiền -> cập nhật lại sau khi xóa
                    lbThanhTien.Text = tinhThanhTien();
                    //TinhTongTien(); // (Hàm này bạn có thể đã viết sẵn để tính lại tổng)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

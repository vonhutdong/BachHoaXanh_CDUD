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
        BUS_BangLuong BUS_BangLuong = new BUS_BangLuong();
        BUS_ChiTietBangLuong BUS_ChiTietBangLuong = new BUS_ChiTietBangLuong();
        
        string madangchon = "";
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
            dgvLichLam.Columns["Ten"].Visible = false;

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
            dgvLichLam.ContextMenuStrip = contextMenuStrip1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //DTO_LichLam ll = new DTO_LichLam(dtpNgay.Value,
            //cbMaNhanVien.SelectedValue.ToString(), cbMaCaLam.SelectedValue.ToString());

            DTO_LichLam ll = new DTO_LichLam(
                dtpNgay.Value,
                cbMaNhanVien.SelectedValue.ToString(),
                cbMaCaLam.SelectedValue.ToString()
            );
            bool check = bus_ll.ThemLichLam(ll);

            if (!check)
            {
                MessageBox.Show("Nhân viên này đã có lịch làm trong ngày và ca đó!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDATA();
                return;
            }
            int thang = ll.NgayLam.Month;
            int nam = ll.NgayLam.Year;
            string maNV = ll.MaNhanVien;

            // 4 Lấy hoặc tạo BangLuong tháng của nhân viên
            var bangLuong = BUS_BangLuong.LayHoacTaoBangLuong(maNV, thang, nam);

            // 5️ Tính giờ làm thực tế từ ca
            var calam = bus_cl.LayCa(ll.MaCaLam);
            double gioLam = 0;
            if (calam != null)
            {
                string gioStr = bus_cl.TinhGioLam(calam);
                double.TryParse(gioStr, out gioLam);
            }
            // 6 Tạo DTO ChiTietBangLuong
            DTO_ChiTietBangLuong ctbl = new DTO_ChiTietBangLuong
            {
                MaBangLuong = bangLuong.MaBangLuong,
                MaLichLam = ll.MaLichLam,
                SoGioCongThucTe = (float)gioLam,
                NgayLam = ll.NgayLam
            };
            MessageBox.Show(ctbl.MaLichLam);


            // 7️⃣ Thêm chi tiết bảng lương và cập nhật tổng lương
            BUS_ChiTietBangLuong.ThemChiTietbangLuong(ctbl);

            MessageBox.Show("Thêm lịch làm và cập nhật lương thành công!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadDATA();
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Kiểm tra có dòng được chọn
                if (dgvLichLam.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn lịch làm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2️⃣ Lấy mã lịch làm và tên nhân viên
                string maLichLam = dgvLichLam.CurrentRow.Cells["maLichLam"].Value.ToString();
                string tenNV = cbMaNhanVien.Text;

                if (string.IsNullOrWhiteSpace(maLichLam))
                {
                    MessageBox.Show("Mã lịch làm không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3️⃣ Xác nhận xoá
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc muốn xóa lịch làm của nhân viên: [{tenNV}] không?\n(Các chi tiết bảng lương liên quan cũng sẽ bị xóa)",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dr != DialogResult.Yes) return;

                // 4️⃣ Gọi BUS để xoá lịch làm và chi tiết bảng lương
                bool check = bus_ll.DellLL(maLichLam);

                if (check)
                {
                    MessageBox.Show("Xoá lịch làm và chi tiết bảng lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();      // Reset form
                    loadDATA();   // Load lại dữ liệu
                }
                else
                {
                    MessageBox.Show("Xoá thất bại! Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            // Hiển thị thông báo xác nhận
            DialogResult r = MessageBox.Show($"Bạn có chắc muốn sửa thông tin lịch làm của nhân viên: {cbMaNhanVien.Text} không?",
                                               "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (r != DialogResult.Yes) return;

            // 1️⃣ Tạo DTO_LichLam từ form
            if (cbMaNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbMaCaLam.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn ca làm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo DTO an toàn
            DTO_LichLam ll = new DTO_LichLam(
                maLichLam: madangchon,
                ngayLam: dtpNgay.Value,
                maNhanVien: cbMaNhanVien.SelectedValue.ToString(),
                maCaLam: cbMaCaLam.SelectedValue.ToString()
            );

            // Kiểm tra trùng lịch (ngày + ca) với các lịch khác của nhân viên
            if (bus_ll.KiemTraTrungLich(ll))
            {
                MessageBox.Show("Nhân viên này đã có lịch làm trùng ngày và ca làm!\nKhông thể sửa.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // 2️⃣ Sửa lịch làm
            bool check = bus_ll.SuaLichLam(ll);
            if (!check)
            {
                MessageBox.Show("Sửa lịch làm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3️⃣ Lấy giờ làm thực tế từ ca mới
            var ca = bus_cl.LayCa(ll.MaCaLam);
            double gioLam = 0;
            if (ca != null)
            {
                string gioStr = bus_cl.TinhGioLam(ca);
                double.TryParse(gioStr, out gioLam);
            }
            else
            {
                MessageBox.Show("Không tìm thấy ca làm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5️⃣ Cập nhật hoặc thêm ChiTietBangLuong và tính lương
            int checkCTBL = BUS_ChiTietBangLuong.UpdateChiTietBangLuong_Only(ll.MaLichLam, ll.MaNhanVien, ll.NgayLam, gioLam);
            if (checkCTBL == 0)
            {
                // Không có chi tiết bảng lương gắn với lịch làm → không làm gì cả
                MessageBox.Show("Sửa lịch làm thành công! (Không có chi tiết bảng lương để cập nhật)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (checkCTBL == 1)
            {
                MessageBox.Show("Sửa lịch làm thành công! (Không có bảng lương để cập nhật)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // 6️⃣ Thông báo thành công
                MessageBox.Show("Sửa lịch làm và cập nhật lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            loadDATA();  // Reload dữ liệu
            Reset();
        }


        //private void dgvLichLam_Click(object sender, EventArgs e)
        //{
        //    btnXoa.Enabled = true;
        //    btnSua.Enabled = true;
        //    if (dgvLichLam.CurrentRow == null || dgvLichLam.CurrentRow.Index < 0)
        //        return;

        //    int n = dgvLichLam.CurrentRow.Index;
        //    madangchon = dgvLichLam.Rows[n].Cells["MaLichLam"].Value.ToString();
        //    try
        //    {
        //        // Ngày làm
        //        var ngayLam = dgvLichLam.Rows[n].Cells["NgayLam"].Value?.ToString();
        //        if (DateTime.TryParse(ngayLam, out DateTime ngay))
        //            dtpNgay.Value = ngay;

        //        // Nhân viên
        //        var tenNhanVien = dgvLichLam.Rows[n].Cells["ManhanVien"].Value?.ToString();
        //        if (!string.IsNullOrEmpty(tenNhanVien))
        //            cbMaNhanVien.Text = tenNhanVien;
        //        else
        //            cbMaNhanVien.SelectedIndex = -1;

        //        // Ca làm
        //        var tenCaLam = dgvLichLam.Rows[n].Cells["TenCaLam"].Value?.ToString();
        //        if (!string.IsNullOrEmpty(tenCaLam))
        //            cbMaCaLam.Text = tenCaLam;
        //        else
        //            cbMaCaLam.SelectedIndex = -1;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi khi chọn dữ liệu: " + ex.Message,
        //            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

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



        private void dgvLichLam_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Chọn dòng được click chuột phải
                dgvLichLam.ClearSelection();
                dgvLichLam.Rows[e.RowIndex].Selected = true;
                dgvLichLam.CurrentCell = dgvLichLam.Rows[e.RowIndex].Cells[0];
            }
        }

        private void dgvLichLam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
            btnSua.Enabled = true;

            if (dgvLichLam.CurrentRow == null || dgvLichLam.CurrentRow.Index < 0)
                return;

            try
            {
                int row = dgvLichLam.CurrentRow.Index;

                // 1️⃣ Lấy MÃ LỊCH LÀM đang chọn
                madangchon = dgvLichLam.Rows[row].Cells["MaLichLam"].Value.ToString().Trim();

                // 2️⃣ Lấy ngày làm
                var ngayLamStr = dgvLichLam.Rows[row].Cells["NgayLam"].Value?.ToString();
                if (DateTime.TryParse(ngayLamStr, out DateTime ngayLam))
                    dtpNgay.Value = ngayLam;

                // 3️⃣ Lấy đúng MÃ NHÂN VIÊN (không lấy Text, phải lấy Value)
                var maNV = dgvLichLam.Rows[row].Cells["MaNhanVien"].Value?.ToString();
                if (!string.IsNullOrEmpty(maNV))
                    cbMaNhanVien.SelectedValue = maNV.Trim();
                else
                    cbMaNhanVien.SelectedIndex = -1;

                //Ca làm
                var tenCaLam = dgvLichLam.Rows[row].Cells["TenCaLam"].Value?.ToString();
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

    }
}

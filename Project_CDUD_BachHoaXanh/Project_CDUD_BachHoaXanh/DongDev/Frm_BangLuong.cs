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
using DAL;
using DTO;

namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_BangLuong : Form
    {
        BUS_BangLuong bus_bangluong = new BUS_BangLuong();
        BUS_ChiTietBangLuong bus_chitietbangluong = new BUS_ChiTietBangLuong();
        BUS_CaLam bus_calam = new BUS_CaLam();
        BUS_LichLam bus_lichlam = new BUS_LichLam();
        //id bang lương đang chọn
        string currentIDBangLuong = "";
        string currentIDNhanVien = "";
        //check them
        bool checkbtn;
        public Frm_BangLuong()
        {
            InitializeComponent();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {   
            if (currentIDBangLuong != "")
            {
                Frm_BaoCaoBangLuong frm = new Frm_BaoCaoBangLuong(currentIDBangLuong);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui chọn bảng lương!!", "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void loadDGVBangLuong()
        {
            dgvBangLuong.DataSource = bus_bangluong.LayDSBangLuong();

            //lay danh sách bảng lương ko có điều kiện
            //dổi tên cột
            dgvBangLuong.Columns["TongGioCong"].HeaderText = "Tổng giờ công";
            dgvBangLuong.Columns["Luong"].HeaderText = "Lương";
            dgvBangLuong.Columns["ThangNam"].HeaderText = "Tháng/Năm";
            dgvBangLuong.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
            dgvBangLuong.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
            dgvBangLuong.Columns["MaNhanVien"].Visible = false;

            //format
            dgvBangLuong.Columns["ThangNam"].DefaultCellStyle.Format = "MM/yyyy";
            //ẩn cột

            //load danh sach len dgv

            // --- Thiết lập kích thước & hiển thị ---
            dgvBangLuong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvBangLuong.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvBangLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvBangLuong.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvBangLuong.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvBangLuong.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvBangLuong.DefaultCellStyle.BackColor = Color.White;
            dgvBangLuong.DefaultCellStyle.ForeColor = Color.Black;
            dgvBangLuong.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvBangLuong.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvBangLuong.EnableHeadersVisualStyles = false;
            dgvBangLuong.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvBangLuong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvBangLuong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvBangLuong.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvBangLuong.AllowUserToResizeColumns = false;
        }
        //lay danh sách chi tiết bảng lương ko có điều kiện
        
        private void LoadDSChiTietBangLuong(string maBangLuong)
        {
            dgvChiTietBangLuong.DataSource = bus_chitietbangluong.LayDSCTBangLuong(maBangLuong);
            //dổi tên cột
            dgvChiTietBangLuong.Columns["NgayLam"].HeaderText = "Ngày làm";
            dgvChiTietBangLuong.Columns["SoGioCongThucTe"].HeaderText = "Giờ công thực tế";
            //format
            dgvChiTietBangLuong.Columns["NgayLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
            //dgvBangLuong.Columns["ThangNam"].DefaultCellStyle.Format = "MM/yyyy";
        }
        void LoadDSChiTietBangLuong()
        {
            dgvChiTietBangLuong.DataSource = bus_chitietbangluong.LayDSCTBangLuong();
            //dổi tên cột
            dgvChiTietBangLuong.Columns["NgayLam"].HeaderText = "Ngày làm";
            dgvChiTietBangLuong.Columns["SoGioCongThucTe"].HeaderText = "Giờ công thực tế";
            //format
            dgvChiTietBangLuong.Columns["NgayLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
            //dgvBangLuong.Columns["ThangNam"].DefaultCellStyle.Format = "MM/yyyy";
            dgvChiTietBangLuong.Columns["MaChiTietBangLuong"].Visible = false;
            dgvChiTietBangLuong.Columns["maBangLuong"].Visible = false;

            // --- Thiết lập kích thước & hiển thị ---
            dgvChiTietBangLuong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvChiTietBangLuong.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvBangLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // --- Tăng chiều cao hàng ---
            dgvChiTietBangLuong.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvChiTietBangLuong.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvChiTietBangLuong.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvChiTietBangLuong.DefaultCellStyle.BackColor = Color.White;
            dgvChiTietBangLuong.DefaultCellStyle.ForeColor = Color.Black;
            dgvChiTietBangLuong.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvChiTietBangLuong.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvChiTietBangLuong.EnableHeadersVisualStyles = false;
            dgvChiTietBangLuong.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvChiTietBangLuong.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvChiTietBangLuong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvChiTietBangLuong.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // --- Cố định: không cho người dùng thay đổi độ rộng cột ---
            dgvChiTietBangLuong.AllowUserToResizeColumns = false;
        }
        private void LoadCBLichLam()
        {
            cbLichLam.DataSource = bus_chitietbangluong.LayDSLichLam();
            cbLichLam.DisplayMember = "MaLichLam";
            cbLichLam.ValueMember = "MaLichLam";
        }
        private void LoadCbNhanVien()
        {
            //cb nhan vien
            cboMaNhanVien.DataSource = bus_bangluong.DSNhanVien();
            cboMaNhanVien.DisplayMember = "MaNhanVien";
            cboMaNhanVien.ValueMember = "MaNhanVien";

            cbTenNhanVien.DataSource = bus_bangluong.DSNhanVien();
            cbTenNhanVien.DisplayMember = "TenNhanVien";
            cbTenNhanVien.ValueMember = "MaNhanVien";
            //cb loc
            cbLocBangLuong.DataSource = bus_bangluong.DSNhanVien();
            cbLocBangLuong.DisplayMember = "TenNhanVien";
            cbLocBangLuong.ValueMember = "MaNhanVien";
        }
        private void Frm_BangLuong_Load(object sender, EventArgs e)
        {
            loadDGVBangLuong();
            LoadDSChiTietBangLuong();
            LoadCBLichLam();
            LoadCbNhanVien();
            txtGioCong.Text = soGio;
            
        }

        private void cboMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNhanVien.Items.Count == 0 || cbTenNhanVien.Items.Count == 0)
                return; // Không làm gì nếu 1 trong 2 combobox chưa có dữ liệu

            if (cboMaNhanVien.SelectedIndex >= 0 &&
                cboMaNhanVien.SelectedIndex < cboMaNhanVien.Items.Count &&
                cboMaNhanVien.SelectedIndex < cbTenNhanVien.Items.Count)
            {
                cbTenNhanVien.SelectedIndex = cboMaNhanVien.SelectedIndex;
            }
        }

        private void LamMoi()
        {
            //load danh sách bảng lương k điều kiện
            loadDGVBangLuong();
            //load danh sách bảng lương k điều kiện
            LoadDSChiTietBangLuong();
            //resrt check box
            cbLoc.Checked = false;
            //btn luu
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            //btn thêm, thêm chi tiết
            btnThem.Enabled = true;
            btnThemChiTiet.Enabled = true;
            //txt
            txtGioCong.Enabled = true;
            txtLuong.Enabled = true;
            cbLichLam.Enabled = true;
            cboMaNhanVien.Enabled = true;
            txtMaBangLuong.ReadOnly = false;
            txtGioCong.Clear();
            txtLuong.Clear();
            txtMaBangLuong.Clear();

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();   
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnThemChiTiet.Enabled = true;
            btnThem.Enabled = true;
            cbLichLam.Enabled = true;
            txtGioCong.Enabled = true;
            txtLuong.Enabled = true;
            cboMaNhanVien.Enabled = true;
        }
        string soGio;
        private void cbLichLam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLichLam.SelectedValue != null)
            {
                string maLichLam = cbLichLam.SelectedValue.ToString();

                var lichLam = bus_lichlam.GetLichLamByMa(maLichLam);
                if (lichLam == null)
                {
                    return;
                }

                var caLam = bus_calam.GetCaLamById(lichLam.maCaLam);
                if (caLam == null)
                {
                    return;
                }

                // Tính giờ làm
                soGio = bus_calam.TinhGioLam(caLam);
                txtGioCong.Text = soGio;
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtGioCong.Enabled = false;
            txtLuong.Enabled = false;
            cbLichLam.Enabled = false;
            //cbNhanVien.Enabled = false;
            btnThemChiTiet.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            checkbtn = true;
        }

        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            checkbtn = false;
            cboMaNhanVien.Enabled = false;
            txtMaBangLuong.ReadOnly = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                //thêm bảng lương
                if (checkbtn)
                {
                    if (cboMaNhanVien.SelectedIndex > -1)
                    {
                        bus_bangluong.ThemBangLuong(new DTO_BangLuong(dtNgayNhap.Value, 0, 0, cboMaNhanVien.SelectedValue.ToString()));
                        MessageBox.Show($"Thêm thành công \n" +
                                    $" Bảng lương tháng {dtNgayNhap.Value.Month} cho nhân viên {cbTenNhanVien.SelectedValue}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LamMoi();
                    }
                    else
                    {
                        //thông báo khi chưa đầy đủ dữ liệu
                        MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu!!", "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                //thêm chi tiết
                else
                {
                    if (txtGioCong.Text.Length > 0)
                    {
                        float gioCong = float.Parse(txtGioCong.Text);
                        if (gioCong > 15)
                        {
                            throw new Exception("Giờ công 1 ngày không quá 15 tiếng!");
                        }
                        bus_chitietbangluong.ThemChiTietbangLuong(new DTO_ChiTietBangLuong(float.Parse(txtGioCong.Text), currentIDBangLuong, cbLichLam.SelectedValue.ToString(), dtNgayNhap.Value));
                        MessageBox.Show($"Thêm thành công \n" +
                                    $" Chi tiết bảng lương tháng {dtNgayNhap.Value.Month} cho nhân viên {cbTenNhanVien.SelectedValue}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LamMoi();
                    }
                    else
                    {
                        //thông báo khi chưa đầy đủ dữ liệu
                        MessageBox.Show("Chưa nhập dữ liệu!!", "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBangLuong_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBangLuong.CurrentCell != null)
                {
                    //lấy dòng đang click
                    int dong = dgvBangLuong.CurrentRow.Index;
                    //điền thông tin lên textbox
                    txtLuong.Text = dgvBangLuong.Rows[dong].Cells["Luong"].Value.ToString();
                    //txtTenChiNhanh.Text = dgvChiNhanh.Rows[dong].Cells["TenChiNhanh"].Value.ToString();
                    //txtDiaChi.Text = dgvChiNhanh.Rows[dong].Cells["DiaChi"].Value.ToString();
                    //txtSoDienThoai.Text = dgvChiNhanh.Rows[dong].Cells["SoDienThoai"].Value.ToString();
                    //gán id cho currnentID
                    currentIDBangLuong = dgvBangLuong.Rows[dong].Cells["MaBangLuong"].Value.ToString();
                    //load danh sách chi tiet bang lương có điều kiện
                    LoadDSChiTietBangLuong(currentIDBangLuong);
                    dtNgayNhap.Value = Convert.ToDateTime(dgvBangLuong.Rows[dong].Cells["ThangNam"].Value);
                    txtMaBangLuong.Text = dgvBangLuong.Rows[dong].Cells["MaBangLuong"].Value.ToString();
                    cboMaNhanVien.SelectedValue = dgvBangLuong.Rows[dong].Cells["MaNhanVien"].Value.ToString();
                    currentIDNhanVien = dgvBangLuong.Rows[dong].Cells["MaNhanVien"].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thoát", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void LoadDSBangLuong(string maNhanVien)
        {
            //lay danh sách bảng lương ko có điều kiện
            dgvBangLuong.DataSource = bus_bangluong.LayDSBangLuong(maNhanVien);
            //dổi tên cột
            dgvBangLuong.Columns["TongGioCong"].HeaderText = "Tổng giờ công";
            dgvBangLuong.Columns["Luong"].HeaderText = "Lương";
            dgvBangLuong.Columns["ThangNam"].HeaderText = "Tháng/Năm";
            dgvBangLuong.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
            //format
            dgvBangLuong.Columns["ThangNam"].DefaultCellStyle.Format = "MM/yyyy";
            //ẩn cột
            dgvBangLuong.Columns["MaBangLuong"].Visible = false;
        }
        private void cbLocBangLuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoc.Checked)
            {
                //load danh sách bảng lương có điều kiện
                LoadDSBangLuong(cbLocBangLuong.SelectedValue.ToString());
            }
        }
    }
}

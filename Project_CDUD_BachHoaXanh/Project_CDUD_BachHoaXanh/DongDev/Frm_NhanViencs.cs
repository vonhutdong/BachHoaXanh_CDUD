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

namespace Project_CDUD_BachHoaXanh.DongDev
{
    public partial class Frm_NhanViencs : Form
    {
        BUS_NhanVien bus_nv = new BUS_NhanVien();
        BUS_ChucVu bus_cv = new BUS_ChucVu();

        //BUS_TaiKhoan bus_tk = new BUS_TaiKhoan();
        DataValidation dv = new DataValidation();
        public Frm_NhanViencs()
        {
            InitializeComponent();
        }

        
        void loadData()
        {
            txtTenNhanVien.Focus();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtTenNhanVien.Text = string.Empty;
            txtSoDienThoai.Text = string.Empty;
            txtDiaChi.Text = string.Empty;

            dgvNV.DataSource = bus_nv.LayDSNhanVien();

            dgvNV.Columns["MaTaiKhoan"].Visible = true;
            dgvNV.Columns["MaNhanVien"].Visible = true;
            dgvNV.Columns["MaNhanVien"].HeaderText = "Mã nhân viên";
            dgvNV.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
            dgvNV.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            dgvNV.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvNV.Columns["LoaiNhanVien"].HeaderText = "Loại nhân viên";
            dgvNV.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvNV.Columns["maChiNhanh"].HeaderText = "Tên chi nhánh";
            dgvNV.Columns["TenChucVu"].HeaderText = "chức vụ";
            

            //cboChiNhanh.DataSource = bu.LayDSCaLam();
            //cboChiNhanh.DisplayMember = "TenCaLam";  
            //cboChiNhanh.ValueMember = "MaCaLam";

            cboChucVu.DataSource = bus_nv.LayDSChucVu();
            cboChucVu.DisplayMember = "TenChucVu";
            cboChucVu.ValueMember = "MaChucVu";

            dgvNV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNV.ColumnHeadersHeight = 30; // hoặc cao hơn

            dgvNV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing; // Không cho resize
            dgvNV.ColumnHeadersHeight = 40; // Chiều cao tiêu đề

            dgvNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cột tự giãn đều

            // -    -- Tăng chiều cao hàng ---
            dgvNV.RowTemplate.Height = 35; // Tăng chiều cao từng hàng
            dgvNV.AllowUserToResizeRows = false; // Không cho resize hàng

            // --- Không cho resize cột ---
            foreach (DataGridViewColumn col in dgvNV.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }

            // --- Giao diện ---
            dgvNV.DefaultCellStyle.BackColor = Color.White;
            dgvNV.DefaultCellStyle.ForeColor = Color.Black;
            dgvNV.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvNV.DefaultCellStyle.SelectionForeColor = Color.Black;
                
            dgvNV.EnableHeadersVisualStyles = false;
            dgvNV.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGreen;
            dgvNV.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvNV.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvNV.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }   
        private void Frm_NhanViencs_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}

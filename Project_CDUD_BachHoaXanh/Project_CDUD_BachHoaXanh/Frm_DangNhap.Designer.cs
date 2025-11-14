namespace Project_CDUD_BachHoaXanh
{
    partial class Frm_DangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DangNhap));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.checkHienMK = new System.Windows.Forms.CheckBox();
            this.txtTaiKhoan = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnDangNhap = new Guna.UI2.WinForms.Guna2Button();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkHienMK
            // 
            this.checkHienMK.AutoSize = true;
            this.checkHienMK.BackgroundImage = global::Project_CDUD_BachHoaXanh.Properties.Resources.z7022826337913_da3ef0c62bf5c8c6f738e6b06eccf27c;
            this.checkHienMK.Location = new System.Drawing.Point(216, 382);
            this.checkHienMK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkHienMK.Name = "checkHienMK";
            this.checkHienMK.Size = new System.Drawing.Size(130, 20);
            this.checkHienMK.TabIndex = 3;
            this.checkHienMK.Text = "Hiển thị mật khẩu";
            this.checkHienMK.UseVisualStyleBackColor = true;
            this.checkHienMK.CheckedChanged += new System.EventHandler(this.checkHienMK_CheckedChanged);
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtTaiKhoan.BackgroundImage = global::Project_CDUD_BachHoaXanh.Properties.Resources.z7022826337913_da3ef0c62bf5c8c6f738e6b06eccf27c;
            this.txtTaiKhoan.BorderColor = System.Drawing.Color.Black;
            this.txtTaiKhoan.BorderRadius = 20;
            this.txtTaiKhoan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTaiKhoan.DefaultText = "";
            this.txtTaiKhoan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTaiKhoan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTaiKhoan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTaiKhoan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTaiKhoan.FillColor = System.Drawing.Color.LightGreen;
            this.txtTaiKhoan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTaiKhoan.ForeColor = System.Drawing.Color.Black;
            this.txtTaiKhoan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTaiKhoan.Location = new System.Drawing.Point(216, 231);
            this.txtTaiKhoan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.txtTaiKhoan.PlaceholderText = "Nhập tên tài khoản...";
            this.txtTaiKhoan.SelectedText = "";
            this.txtTaiKhoan.Size = new System.Drawing.Size(205, 48);
            this.txtTaiKhoan.TabIndex = 1;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtMatKhau.BackgroundImage = global::Project_CDUD_BachHoaXanh.Properties.Resources.z7022826337913_da3ef0c62bf5c8c6f738e6b06eccf27c;
            this.txtMatKhau.BorderColor = System.Drawing.Color.Black;
            this.txtMatKhau.BorderRadius = 20;
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.DefaultText = "";
            this.txtMatKhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhau.FillColor = System.Drawing.Color.LightGreen;
            this.txtMatKhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMatKhau.ForeColor = System.Drawing.Color.Black;
            this.txtMatKhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatKhau.Location = new System.Drawing.Point(216, 309);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.txtMatKhau.PlaceholderText = "Nhập mật khẩu...";
            this.txtMatKhau.SelectedText = "";
            this.txtMatKhau.Size = new System.Drawing.Size(205, 48);
            this.txtMatKhau.TabIndex = 2;
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BackColor = System.Drawing.Color.LightGreen;
            this.btnDangNhap.BorderColor = System.Drawing.Color.DimGray;
            this.btnDangNhap.BorderRadius = 20;
            this.btnDangNhap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDangNhap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDangNhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDangNhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDangNhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(252, 423);
            this.btnDangNhap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(133, 44);
            this.btnDangNhap.TabIndex = 4;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Lime;
            this.guna2PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Black;
            this.guna2PictureBox1.Image = global::Project_CDUD_BachHoaXanh.Properties.Resources.z7022822675090_39a357202d04bece2f13b115782590c9;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(0, 0);
            this.guna2PictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(603, 623);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2PictureBox1.TabIndex = 12;
            this.guna2PictureBox1.TabStop = false;
            // 
            // Frm_DangNhap
            // 
            this.AcceptButton = this.btnDangNhap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 623);
            this.Controls.Add(this.checkHienMK);
            this.Controls.Add(this.txtTaiKhoan);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.guna2PictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_DangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_DangNhap_FormClosing);
            this.Load += new System.EventHandler(this.Frm_DangNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private System.Windows.Forms.CheckBox checkHienMK;
        private Guna.UI2.WinForms.Guna2TextBox txtTaiKhoan;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhau;
        private Guna.UI2.WinForms.Guna2Button btnDangNhap;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
    }
}


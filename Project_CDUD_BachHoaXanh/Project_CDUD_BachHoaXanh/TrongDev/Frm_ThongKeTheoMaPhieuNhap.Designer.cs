namespace Project_CDUD_BachHoaXanh.TrongDev
{
    partial class Frm_ThongKeTheoMaPhieuNhap
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboMaPhieuNhap = new System.Windows.Forms.ComboBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.rpt_ThongKeTheoMaPhieuNhap = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.InThongKeTheoMaPhieuNhap2 = new Project_CDUD_BachHoaXanh.TrongDev.InThongKeTheoMaPhieuNhap();
            this.InThongKeTheoMaPhieuNhap1 = new Project_CDUD_BachHoaXanh.TrongDev.InThongKeTheoMaPhieuNhap();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.rpt_ThongKeTheoMaPhieuNhap, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 37);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1326, 658);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboMaPhieuNhap, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTim, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1326, 37);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã phiếu nhập";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboMaPhieuNhap
            // 
            this.cboMaPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMaPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaPhieuNhap.FormattingEnabled = true;
            this.cboMaPhieuNhap.Location = new System.Drawing.Point(268, 3);
            this.cboMaPhieuNhap.Name = "cboMaPhieuNhap";
            this.cboMaPhieuNhap.Size = new System.Drawing.Size(259, 32);
            this.cboMaPhieuNhap.TabIndex = 1;
            // 
            // btnTim
            // 
            this.btnTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTim.Location = new System.Drawing.Point(533, 3);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(106, 31);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // rpt_ThongKeTheoMaPhieuNhap
            // 
            this.rpt_ThongKeTheoMaPhieuNhap.ActiveViewIndex = 0;
            this.rpt_ThongKeTheoMaPhieuNhap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rpt_ThongKeTheoMaPhieuNhap.Cursor = System.Windows.Forms.Cursors.Default;
            this.rpt_ThongKeTheoMaPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpt_ThongKeTheoMaPhieuNhap.Location = new System.Drawing.Point(3, 3);
            this.rpt_ThongKeTheoMaPhieuNhap.Name = "rpt_ThongKeTheoMaPhieuNhap";
            this.rpt_ThongKeTheoMaPhieuNhap.ReportSource = this.InThongKeTheoMaPhieuNhap2;
            this.rpt_ThongKeTheoMaPhieuNhap.Size = new System.Drawing.Size(1320, 652);
            this.rpt_ThongKeTheoMaPhieuNhap.TabIndex = 0;
            // 
            // Frm_ThongKeTheoMaPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 695);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Frm_ThongKeTheoMaPhieuNhap";
            this.Text = "IN THỐNG KÊ THEO MÃ PHIẾU NHẬP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_ThongKeTheoMaPhieuNhap_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private InThongKeTheoMaPhieuNhap InThongKeTheoMaPhieuNhap1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rpt_ThongKeTheoMaPhieuNhap;
        private InThongKeTheoMaPhieuNhap InThongKeTheoMaPhieuNhap2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboMaPhieuNhap;
        private System.Windows.Forms.Button btnTim;
    }
}
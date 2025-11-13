namespace Project_CDUD_BachHoaXanh
{
    partial class ItemSP_new
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemSP_new));
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.imgProduct = new Guna.UI2.WinForms.Guna2PictureBox();
            this.priceItem = new System.Windows.Forms.Label();
            this.amountItem = new System.Windows.Forms.Label();
            this.lbnameSanPham = new System.Windows.Forms.Label();
            this.lblSale = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(0, 0);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(52, 18);
            this.guna2HtmlLabel1.TabIndex = 8;
            this.guna2HtmlLabel1.Text = "Cá chua";
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblSale, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.imgProduct, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.priceItem, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.amountItem, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbnameSanPham, 0, 1);
            this.tableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(498, 405);
            this.tableLayoutPanel1.TabIndex = 7;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // imgProduct
            // 
            this.imgProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgProduct.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imgProduct.ErrorImage")));
            this.imgProduct.Image = ((System.Drawing.Image)(resources.GetObject("imgProduct.Image")));
            this.imgProduct.ImageRotate = 0F;
            this.imgProduct.Location = new System.Drawing.Point(3, 2);
            this.imgProduct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imgProduct.Name = "imgProduct";
            this.imgProduct.Size = new System.Drawing.Size(492, 198);
            this.imgProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgProduct.TabIndex = 5;
            this.imgProduct.TabStop = false;
            this.imgProduct.Click += new System.EventHandler(this.imgProduct_Click);
            // 
            // priceItem
            // 
            this.priceItem.AutoSize = true;
            this.priceItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.priceItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.priceItem.Location = new System.Drawing.Point(4, 262);
            this.priceItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.priceItem.Name = "priceItem";
            this.priceItem.Size = new System.Drawing.Size(490, 60);
            this.priceItem.TabIndex = 3;
            this.priceItem.Text = "Giá: 25,000 đ";
            this.priceItem.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.priceItem.Click += new System.EventHandler(this.priceItem_Click);
            // 
            // amountItem
            // 
            this.amountItem.AutoSize = true;
            this.amountItem.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountItem.ForeColor = System.Drawing.SystemColors.Desktop;
            this.amountItem.Location = new System.Drawing.Point(4, 322);
            this.amountItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.amountItem.Name = "amountItem";
            this.amountItem.Size = new System.Drawing.Size(75, 19);
            this.amountItem.TabIndex = 4;
            this.amountItem.Text = "Sẵn có: 9";
            this.amountItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.amountItem.Click += new System.EventHandler(this.amountItem_Click);
            // 
            // lbnameSanPham
            // 
            this.lbnameSanPham.AutoSize = true;
            this.lbnameSanPham.BackColor = System.Drawing.Color.Transparent;
            this.lbnameSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbnameSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnameSanPham.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lbnameSanPham.Location = new System.Drawing.Point(3, 202);
            this.lbnameSanPham.Name = "lbnameSanPham";
            this.lbnameSanPham.Size = new System.Drawing.Size(492, 60);
            this.lbnameSanPham.TabIndex = 6;
            this.lbnameSanPham.Text = "Chuối";
            this.lbnameSanPham.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbnameSanPham.Click += new System.EventHandler(this.lbnameSanPham_Click);
            // 
            // lblSale
            // 
            this.lblSale.AutoSize = true;
            this.lblSale.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSale.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblSale.Location = new System.Drawing.Point(4, 362);
            this.lblSale.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSale.Name = "lblSale";
            this.lblSale.Size = new System.Drawing.Size(84, 19);
            this.lblSale.TabIndex = 7;
            this.lblSale.Text = "Sale: 15%";
            this.lblSale.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // ItemSP_new
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ItemSP_new";
            this.Size = new System.Drawing.Size(498, 405);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2PictureBox imgProduct;
        private System.Windows.Forms.Label amountItem;
        private System.Windows.Forms.Label lbnameSanPham;
        private System.Windows.Forms.Label priceItem;
        private System.Windows.Forms.Label lblSale;
    }
}

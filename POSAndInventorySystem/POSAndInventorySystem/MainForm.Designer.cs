
namespace POSAndInventorySystem
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelTop = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnStockAdjustment = new System.Windows.Forms.Button();
            this.btnVendor = new System.Windows.Forms.Button();
            this.btnSalesHistory = new System.Windows.Forms.Button();
            this.btnStockIn = new System.Windows.Forms.Button();
            this.btLogout = new System.Windows.Forms.Button();
            this.btDashBoard = new System.Windows.Forms.Button();
            this.btManageProduct = new System.Windows.Forms.Button();
            this.btManageCategory = new System.Windows.Forms.Button();
            this.btManageBrand = new System.Windows.Forms.Button();
            this.btUserSettings = new System.Windows.Forms.Button();
            this.btSystemSettings = new System.Windows.Forms.Button();
            this.btManageSales = new System.Windows.Forms.Button();
            this.btRecords = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAdmin = new System.Windows.Forms.Label();
            this.lblAdminUser = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1198, 32);
            this.panelTop.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 566);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.btnStockAdjustment);
            this.panel3.Controls.Add(this.btnVendor);
            this.panel3.Controls.Add(this.btnSalesHistory);
            this.panel3.Controls.Add(this.btnStockIn);
            this.panel3.Controls.Add(this.btLogout);
            this.panel3.Controls.Add(this.btDashBoard);
            this.panel3.Controls.Add(this.btManageProduct);
            this.panel3.Controls.Add(this.btManageCategory);
            this.panel3.Controls.Add(this.btManageBrand);
            this.panel3.Controls.Add(this.btUserSettings);
            this.panel3.Controls.Add(this.btSystemSettings);
            this.panel3.Controls.Add(this.btManageSales);
            this.panel3.Controls.Add(this.btRecords);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 132);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 434);
            this.panel3.TabIndex = 14;
            // 
            // btnStockAdjustment
            // 
            this.btnStockAdjustment.FlatAppearance.BorderSize = 0;
            this.btnStockAdjustment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btnStockAdjustment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockAdjustment.ForeColor = System.Drawing.Color.Silver;
            this.btnStockAdjustment.Image = ((System.Drawing.Image)(resources.GetObject("btnStockAdjustment.Image")));
            this.btnStockAdjustment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockAdjustment.Location = new System.Drawing.Point(0, 236);
            this.btnStockAdjustment.Name = "btnStockAdjustment";
            this.btnStockAdjustment.Size = new System.Drawing.Size(200, 38);
            this.btnStockAdjustment.TabIndex = 15;
            this.btnStockAdjustment.Text = "  Stock Adjustment";
            this.btnStockAdjustment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStockAdjustment.UseVisualStyleBackColor = true;
            this.btnStockAdjustment.Click += new System.EventHandler(this.btnStockAdjustment_Click);
            // 
            // btnVendor
            // 
            this.btnVendor.FlatAppearance.BorderSize = 0;
            this.btnVendor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btnVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVendor.ForeColor = System.Drawing.Color.Silver;
            this.btnVendor.Image = ((System.Drawing.Image)(resources.GetObject("btnVendor.Image")));
            this.btnVendor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVendor.Location = new System.Drawing.Point(0, 34);
            this.btnVendor.Name = "btnVendor";
            this.btnVendor.Size = new System.Drawing.Size(200, 34);
            this.btnVendor.TabIndex = 14;
            this.btnVendor.Text = "  Vendor";
            this.btnVendor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVendor.UseVisualStyleBackColor = true;
            this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
            // 
            // btnSalesHistory
            // 
            this.btnSalesHistory.FlatAppearance.BorderSize = 0;
            this.btnSalesHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btnSalesHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalesHistory.ForeColor = System.Drawing.Color.Silver;
            this.btnSalesHistory.Image = ((System.Drawing.Image)(resources.GetObject("btnSalesHistory.Image")));
            this.btnSalesHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalesHistory.Location = new System.Drawing.Point(0, 268);
            this.btnSalesHistory.Name = "btnSalesHistory";
            this.btnSalesHistory.Size = new System.Drawing.Size(200, 36);
            this.btnSalesHistory.TabIndex = 13;
            this.btnSalesHistory.Text = "  Sales History";
            this.btnSalesHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalesHistory.UseVisualStyleBackColor = true;
            this.btnSalesHistory.Click += new System.EventHandler(this.btnSalesHistory_Click);
            // 
            // btnStockIn
            // 
            this.btnStockIn.FlatAppearance.BorderSize = 0;
            this.btnStockIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btnStockIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockIn.ForeColor = System.Drawing.Color.Silver;
            this.btnStockIn.Image = ((System.Drawing.Image)(resources.GetObject("btnStockIn.Image")));
            this.btnStockIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockIn.Location = new System.Drawing.Point(0, 205);
            this.btnStockIn.Name = "btnStockIn";
            this.btnStockIn.Size = new System.Drawing.Size(200, 38);
            this.btnStockIn.TabIndex = 12;
            this.btnStockIn.Text = "  Stock Entry";
            this.btnStockIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStockIn.UseVisualStyleBackColor = true;
            this.btnStockIn.Click += new System.EventHandler(this.btnStockIn_Click);
            // 
            // btLogout
            // 
            this.btLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btLogout.FlatAppearance.BorderSize = 0;
            this.btLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLogout.ForeColor = System.Drawing.Color.Silver;
            this.btLogout.Image = ((System.Drawing.Image)(resources.GetObject("btLogout.Image")));
            this.btLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btLogout.Location = new System.Drawing.Point(0, 398);
            this.btLogout.Name = "btLogout";
            this.btLogout.Size = new System.Drawing.Size(200, 36);
            this.btLogout.TabIndex = 10;
            this.btLogout.Text = " [ESC] Logout";
            this.btLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btLogout.UseVisualStyleBackColor = true;
            this.btLogout.Click += new System.EventHandler(this.btLogout_Click);
            // 
            // btDashBoard
            // 
            this.btDashBoard.FlatAppearance.BorderSize = 0;
            this.btDashBoard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btDashBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDashBoard.ForeColor = System.Drawing.Color.Silver;
            this.btDashBoard.Image = ((System.Drawing.Image)(resources.GetObject("btDashBoard.Image")));
            this.btDashBoard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btDashBoard.Location = new System.Drawing.Point(0, 171);
            this.btDashBoard.Name = "btDashBoard";
            this.btDashBoard.Size = new System.Drawing.Size(200, 34);
            this.btDashBoard.TabIndex = 3;
            this.btDashBoard.Text = "  Dashboard";
            this.btDashBoard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btDashBoard.UseVisualStyleBackColor = true;
            this.btDashBoard.Click += new System.EventHandler(this.btDashBoard_Click);
            // 
            // btManageProduct
            // 
            this.btManageProduct.FlatAppearance.BorderSize = 0;
            this.btManageProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btManageProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btManageProduct.ForeColor = System.Drawing.Color.Silver;
            this.btManageProduct.Image = ((System.Drawing.Image)(resources.GetObject("btManageProduct.Image")));
            this.btManageProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btManageProduct.Location = new System.Drawing.Point(0, 137);
            this.btManageProduct.Name = "btManageProduct";
            this.btManageProduct.Size = new System.Drawing.Size(200, 34);
            this.btManageProduct.TabIndex = 5;
            this.btManageProduct.Text = "  Product";
            this.btManageProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btManageProduct.UseVisualStyleBackColor = true;
            this.btManageProduct.Click += new System.EventHandler(this.btManageProduct_Click);
            // 
            // btManageCategory
            // 
            this.btManageCategory.FlatAppearance.BorderSize = 0;
            this.btManageCategory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btManageCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btManageCategory.ForeColor = System.Drawing.Color.Silver;
            this.btManageCategory.Image = ((System.Drawing.Image)(resources.GetObject("btManageCategory.Image")));
            this.btManageCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btManageCategory.Location = new System.Drawing.Point(0, 103);
            this.btManageCategory.Name = "btManageCategory";
            this.btManageCategory.Size = new System.Drawing.Size(200, 34);
            this.btManageCategory.TabIndex = 6;
            this.btManageCategory.Text = "  Category";
            this.btManageCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btManageCategory.UseVisualStyleBackColor = true;
            this.btManageCategory.Click += new System.EventHandler(this.btManageCategory_Click);
            // 
            // btManageBrand
            // 
            this.btManageBrand.FlatAppearance.BorderSize = 0;
            this.btManageBrand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btManageBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btManageBrand.ForeColor = System.Drawing.Color.Silver;
            this.btManageBrand.Image = ((System.Drawing.Image)(resources.GetObject("btManageBrand.Image")));
            this.btManageBrand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btManageBrand.Location = new System.Drawing.Point(0, 69);
            this.btManageBrand.Name = "btManageBrand";
            this.btManageBrand.Size = new System.Drawing.Size(200, 34);
            this.btManageBrand.TabIndex = 11;
            this.btManageBrand.Text = "  Brand";
            this.btManageBrand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btManageBrand.UseVisualStyleBackColor = true;
            this.btManageBrand.Click += new System.EventHandler(this.btManageBrand_Click);
            // 
            // btUserSettings
            // 
            this.btUserSettings.FlatAppearance.BorderSize = 0;
            this.btUserSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btUserSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btUserSettings.ForeColor = System.Drawing.Color.Silver;
            this.btUserSettings.Image = ((System.Drawing.Image)(resources.GetObject("btUserSettings.Image")));
            this.btUserSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btUserSettings.Location = new System.Drawing.Point(0, 302);
            this.btUserSettings.Name = "btUserSettings";
            this.btUserSettings.Size = new System.Drawing.Size(200, 34);
            this.btUserSettings.TabIndex = 9;
            this.btUserSettings.Text = "  User Settings";
            this.btUserSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btUserSettings.UseVisualStyleBackColor = true;
            this.btUserSettings.Click += new System.EventHandler(this.btUserSettings_Click);
            // 
            // btSystemSettings
            // 
            this.btSystemSettings.FlatAppearance.BorderSize = 0;
            this.btSystemSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btSystemSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSystemSettings.ForeColor = System.Drawing.Color.Silver;
            this.btSystemSettings.Image = ((System.Drawing.Image)(resources.GetObject("btSystemSettings.Image")));
            this.btSystemSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSystemSettings.Location = new System.Drawing.Point(0, 333);
            this.btSystemSettings.Name = "btSystemSettings";
            this.btSystemSettings.Size = new System.Drawing.Size(200, 34);
            this.btSystemSettings.TabIndex = 8;
            this.btSystemSettings.Text = "  Store Settings";
            this.btSystemSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btSystemSettings.UseVisualStyleBackColor = true;
            this.btSystemSettings.Click += new System.EventHandler(this.btSystemSettings_Click);
            // 
            // btManageSales
            // 
            this.btManageSales.FlatAppearance.BorderSize = 0;
            this.btManageSales.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btManageSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btManageSales.ForeColor = System.Drawing.Color.Silver;
            this.btManageSales.Image = ((System.Drawing.Image)(resources.GetObject("btManageSales.Image")));
            this.btManageSales.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btManageSales.Location = new System.Drawing.Point(0, 368);
            this.btManageSales.Name = "btManageSales";
            this.btManageSales.Size = new System.Drawing.Size(200, 29);
            this.btManageSales.TabIndex = 4;
            this.btManageSales.Text = "  POS";
            this.btManageSales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btManageSales.UseVisualStyleBackColor = true;
            this.btManageSales.Visible = false;
            this.btManageSales.Click += new System.EventHandler(this.btManageSales_Click);
            // 
            // btRecords
            // 
            this.btRecords.FlatAppearance.BorderSize = 0;
            this.btRecords.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.btRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRecords.ForeColor = System.Drawing.Color.Silver;
            this.btRecords.Image = ((System.Drawing.Image)(resources.GetObject("btRecords.Image")));
            this.btRecords.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btRecords.Location = new System.Drawing.Point(0, 0);
            this.btRecords.Name = "btRecords";
            this.btRecords.Size = new System.Drawing.Size(200, 34);
            this.btRecords.TabIndex = 7;
            this.btRecords.Text = "  Records";
            this.btRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btRecords.UseVisualStyleBackColor = true;
            this.btRecords.Click += new System.EventHandler(this.btRecords_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblAdmin);
            this.panel1.Controls.Add(this.lblAdminUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 132);
            this.panel1.TabIndex = 13;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(52, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(88, 77);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblAdmin
            // 
            this.lblAdmin.ForeColor = System.Drawing.Color.Silver;
            this.lblAdmin.Location = new System.Drawing.Point(0, 105);
            this.lblAdmin.Name = "lblAdmin";
            this.lblAdmin.Size = new System.Drawing.Size(200, 20);
            this.lblAdmin.TabIndex = 2;
            this.lblAdmin.Text = "Administrator";
            this.lblAdmin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAdminUser
            // 
            this.lblAdminUser.ForeColor = System.Drawing.Color.Green;
            this.lblAdminUser.Location = new System.Drawing.Point(0, 85);
            this.lblAdminUser.Name = "lblAdminUser";
            this.lblAdminUser.Size = new System.Drawing.Size(200, 20);
            this.lblAdminUser.TabIndex = 1;
            this.lblAdminUser.Text = "Username";
            this.lblAdminUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(200, 32);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(998, 566);
            this.panelMain.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 598);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btDashBoard;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btManageSales;
        private System.Windows.Forms.Button btUserSettings;
        private System.Windows.Forms.Button btSystemSettings;
        private System.Windows.Forms.Button btRecords;
        private System.Windows.Forms.Button btManageCategory;
        private System.Windows.Forms.Button btManageProduct;
        private System.Windows.Forms.Button btLogout;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btManageBrand;
        private System.Windows.Forms.Button btnStockIn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblAdminUser;
        public System.Windows.Forms.Label lblAdmin;
        private System.Windows.Forms.Button btnSalesHistory;
        private System.Windows.Forms.Button btnVendor;
        private System.Windows.Forms.Button btnStockAdjustment;
    }
}


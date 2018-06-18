namespace FA1811AHS
{
    partial class AccountSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountSet));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkbSystem = new System.Windows.Forms.CheckBox();
            this.chkbRead2D = new System.Windows.Forms.CheckBox();
            this.chkbLaserSet = new System.Windows.Forms.CheckBox();
            this.chkbPartNo = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripbtnCancel = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.chkbSystem);
            this.groupBox1.Controls.Add(this.chkbRead2D);
            this.groupBox1.Controls.Add(this.chkbLaserSet);
            this.groupBox1.Controls.Add(this.chkbPartNo);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 400);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "權限控制項";
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = true;
            this.btnClear.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClear.Location = new System.Drawing.Point(24, 248);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(104, 31);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清空欄位";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkbSystem
            // 
            this.chkbSystem.AutoSize = true;
            this.chkbSystem.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkbSystem.Location = new System.Drawing.Point(24, 191);
            this.chkbSystem.Name = "chkbSystem";
            this.chkbSystem.Size = new System.Drawing.Size(104, 23);
            this.chkbSystem.TabIndex = 3;
            this.chkbSystem.Text = "系統設定";
            this.chkbSystem.UseVisualStyleBackColor = true;
            // 
            // chkbRead2D
            // 
            this.chkbRead2D.AutoSize = true;
            this.chkbRead2D.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkbRead2D.Location = new System.Drawing.Point(24, 135);
            this.chkbRead2D.Name = "chkbRead2D";
            this.chkbRead2D.Size = new System.Drawing.Size(123, 23);
            this.chkbRead2D.TabIndex = 2;
            this.chkbRead2D.Text = "讀取器設定";
            this.chkbRead2D.UseVisualStyleBackColor = true;
            // 
            // chkbLaserSet
            // 
            this.chkbLaserSet.AutoSize = true;
            this.chkbLaserSet.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkbLaserSet.Location = new System.Drawing.Point(24, 87);
            this.chkbLaserSet.Name = "chkbLaserSet";
            this.chkbLaserSet.Size = new System.Drawing.Size(104, 23);
            this.chkbLaserSet.TabIndex = 1;
            this.chkbLaserSet.Text = "雷射設定";
            this.chkbLaserSet.UseVisualStyleBackColor = true;
            // 
            // chkbPartNo
            // 
            this.chkbPartNo.AutoSize = true;
            this.chkbPartNo.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkbPartNo.Location = new System.Drawing.Point(24, 41);
            this.chkbPartNo.Name = "chkbPartNo";
            this.chkbPartNo.Size = new System.Drawing.Size(123, 23);
            this.chkbPartNo.TabIndex = 0;
            this.chkbPartNo.Text = "料號庫設定";
            this.chkbPartNo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 412F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(730, 466);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripbtnAdd,
            this.toolStripbtnUpdate,
            this.toolStripbtnDelete,
            this.toolStripbtnCancel});
            this.toolStrip1.Location = new System.Drawing.Point(1, 1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(728, 51);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripbtnAdd
            // 
            this.toolStripbtnAdd.Font = new System.Drawing.Font("Microsoft JhengHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnAdd.Image")));
            this.toolStripbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnAdd.Name = "toolStripbtnAdd";
            this.toolStripbtnAdd.Size = new System.Drawing.Size(74, 48);
            this.toolStripbtnAdd.Tag = "1";
            this.toolStripbtnAdd.Text = "新增";
            this.toolStripbtnAdd.Click += new System.EventHandler(this.toolStripbtnAdd_Click);
            // 
            // toolStripbtnUpdate
            // 
            this.toolStripbtnUpdate.Font = new System.Drawing.Font("Microsoft JhengHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripbtnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnUpdate.Image")));
            this.toolStripbtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnUpdate.Name = "toolStripbtnUpdate";
            this.toolStripbtnUpdate.Size = new System.Drawing.Size(74, 48);
            this.toolStripbtnUpdate.Text = "修改";
            this.toolStripbtnUpdate.Click += new System.EventHandler(this.toolStripbtnUpdate_Click);
            // 
            // toolStripbtnDelete
            // 
            this.toolStripbtnDelete.Font = new System.Drawing.Font("Microsoft JhengHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnDelete.Image")));
            this.toolStripbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnDelete.Name = "toolStripbtnDelete";
            this.toolStripbtnDelete.Size = new System.Drawing.Size(74, 48);
            this.toolStripbtnDelete.Text = "刪除";
            this.toolStripbtnDelete.Click += new System.EventHandler(this.toolStripbtnDelete_Click);
            // 
            // toolStripbtnCancel
            // 
            this.toolStripbtnCancel.Font = new System.Drawing.Font("Microsoft JhengHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripbtnCancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnCancel.Image")));
            this.toolStripbtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnCancel.Name = "toolStripbtnCancel";
            this.toolStripbtnCancel.Size = new System.Drawing.Size(116, 48);
            this.toolStripbtnCancel.Text = "關閉離開";
            this.toolStripbtnCancel.Click += new System.EventHandler(this.toolStripbtnCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 56);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(722, 406);
            this.splitContainer1.SplitterDistance = 527;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtPassword);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.txtNo);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer2.Size = new System.Drawing.Size(527, 406);
            this.splitContainer2.SplitterDistance = 48;
            this.splitContainer2.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPassword.Location = new System.Drawing.Point(355, 11);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(150, 29);
            this.txtPassword.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "使用者帳號:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(265, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "使用者密碼:";
            // 
            // txtNo
            // 
            this.txtNo.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtNo.Location = new System.Drawing.Point(109, 11);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(150, 29);
            this.txtNo.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(527, 354);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(191, 406);
            this.panel1.TabIndex = 0;
            // 
            // AccountSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 485);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AccountSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "帳戶設定";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkbSystem;
        private System.Windows.Forms.CheckBox chkbRead2D;
        private System.Windows.Forms.CheckBox chkbLaserSet;
        private System.Windows.Forms.CheckBox chkbPartNo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripbtnAdd;
        private System.Windows.Forms.ToolStripButton toolStripbtnUpdate;
        private System.Windows.Forms.ToolStripButton toolStripbtnDelete;
        private System.Windows.Forms.ToolStripButton toolStripbtnCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
    }
}
namespace FA1811AHS
{
    partial class Reader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reader));
            this.liveviewForm1 = new Keyence.AutoID.SDK.LiveviewForm();
            this.btnRead = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripbtnExit = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblText = new System.Windows.Forms.Label();
            this.btnReadWrite = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // liveviewForm1
            // 
            this.liveviewForm1.BackColor = System.Drawing.Color.Black;
            this.liveviewForm1.BinningType = Keyence.AutoID.SDK.LiveviewForm.ImageBinningType.OneQuarter;
            this.liveviewForm1.ImageFormat = Keyence.AutoID.SDK.LiveviewForm.ImageFormatType.Jpeg;
            this.liveviewForm1.ImageQuality = 5;
            this.liveviewForm1.IpAddress = "192.168.100.100";
            this.liveviewForm1.Location = new System.Drawing.Point(18, 95);
            this.liveviewForm1.Name = "liveviewForm1";
            this.liveviewForm1.PullTimeSpan = 100;
            this.liveviewForm1.Size = new System.Drawing.Size(450, 250);
            this.liveviewForm1.TabIndex = 17;
            this.liveviewForm1.TimeoutMs = 2000;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(26, 15);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(100, 30);
            this.btnRead.TabIndex = 18;
            this.btnRead.Text = "讀取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 374F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(503, 425);
            this.tableLayoutPanel1.TabIndex = 65;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripbtnExit});
            this.toolStrip1.Location = new System.Drawing.Point(1, 1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(501, 48);
            this.toolStrip1.TabIndex = 82;
            this.toolStrip1.Text = "toolStrip2";
            // 
            // toolStripbtnExit
            // 
            this.toolStripbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripbtnExit.Image")));
            this.toolStripbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripbtnExit.Name = "toolStripbtnExit";
            this.toolStripbtnExit.Size = new System.Drawing.Size(116, 45);
            this.toolStripbtnExit.Text = "關閉離開";
            this.toolStripbtnExit.Click += new System.EventHandler(this.toolStripbtnExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.liveviewForm1);
            this.panel1.Controls.Add(this.lblText);
            this.panel1.Controls.Add(this.btnReadWrite);
            this.panel1.Controls.Add(this.btnRead);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 368);
            this.panel1.TabIndex = 83;
            // 
            // lblText
            // 
            this.lblText.BackColor = System.Drawing.Color.White;
            this.lblText.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblText.Location = new System.Drawing.Point(94, 57);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(368, 25);
            this.lblText.TabIndex = 24;
            // 
            // btnReadWrite
            // 
            this.btnReadWrite.AutoSize = true;
            this.btnReadWrite.Location = new System.Drawing.Point(144, 15);
            this.btnReadWrite.Name = "btnReadWrite";
            this.btnReadWrite.Size = new System.Drawing.Size(119, 30);
            this.btnReadWrite.TabIndex = 21;
            this.btnReadWrite.Text = "手動讀取/寫入Hioki ";
            this.btnReadWrite.UseVisualStyleBackColor = true;
            this.btnReadWrite.Click += new System.EventHandler(this.btnReadWrite_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(22, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "Barcode:";
            // 
            // Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 449);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Reader";
            this.Text = "條碼讀取功能";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Keyence.AutoID.SDK.LiveviewForm liveviewForm1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripbtnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnReadWrite;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label label3;
    }
}
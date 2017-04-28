namespace WinForm.房态图
{
    partial class Form_Main
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRoom = new System.Windows.Forms.Label();
            this.txtRoom = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.签订合同ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.预定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.缴费ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnVacantHousingUnit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnMovedOut = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRent = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNotRent = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAll = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(683, 481);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "租赁房源列表";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(677, 461);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Location = new System.Drawing.Point(24, 16);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(65, 12);
            this.lblRoom.TabIndex = 2;
            this.lblRoom.Text = "当前房间：";
            // 
            // txtRoom
            // 
            this.txtRoom.Location = new System.Drawing.Point(14, 41);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Size = new System.Drawing.Size(157, 21);
            this.txtRoom.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.签订合同ToolStripMenuItem,
            this.预定ToolStripMenuItem,
            this.缴费ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            // 
            // 签订合同ToolStripMenuItem
            // 
            this.签订合同ToolStripMenuItem.Name = "签订合同ToolStripMenuItem";
            this.签订合同ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.签订合同ToolStripMenuItem.Text = "签订合同";
            this.签订合同ToolStripMenuItem.Click += new System.EventHandler(this.签订合同ToolStripMenuItem_Click);
            // 
            // 预定ToolStripMenuItem
            // 
            this.预定ToolStripMenuItem.Name = "预定ToolStripMenuItem";
            this.预定ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.预定ToolStripMenuItem.Text = "预定";
            // 
            // 缴费ToolStripMenuItem
            // 
            this.缴费ToolStripMenuItem.Name = "缴费ToolStripMenuItem";
            this.缴费ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.缴费ToolStripMenuItem.Text = "缴费";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnVacantHousingUnit);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnMovedOut);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnRent);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnNotRent);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnAll);
            this.panel1.Controls.Add(this.lblRoom);
            this.panel1.Controls.Add(this.txtRoom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(711, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 481);
            this.panel1.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(113, 258);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "label5";
            // 
            // btnVacantHousingUnit
            // 
            this.btnVacantHousingUnit.BackColor = System.Drawing.Color.SlateGray;
            this.btnVacantHousingUnit.Location = new System.Drawing.Point(18, 253);
            this.btnVacantHousingUnit.Name = "btnVacantHousingUnit";
            this.btnVacantHousingUnit.Size = new System.Drawing.Size(75, 23);
            this.btnVacantHousingUnit.TabIndex = 12;
            this.btnVacantHousingUnit.Text = "空置房";
            this.btnVacantHousingUnit.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(113, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "label4";
            // 
            // btnMovedOut
            // 
            this.btnMovedOut.BackColor = System.Drawing.Color.Purple;
            this.btnMovedOut.Location = new System.Drawing.Point(18, 212);
            this.btnMovedOut.Name = "btnMovedOut";
            this.btnMovedOut.Size = new System.Drawing.Size(75, 23);
            this.btnMovedOut.TabIndex = 10;
            this.btnMovedOut.Text = "已迁出";
            this.btnMovedOut.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "label3";
            // 
            // btnRent
            // 
            this.btnRent.BackColor = System.Drawing.Color.ForestGreen;
            this.btnRent.Location = new System.Drawing.Point(17, 174);
            this.btnRent.Name = "btnRent";
            this.btnRent.Size = new System.Drawing.Size(75, 23);
            this.btnRent.TabIndex = 8;
            this.btnRent.Text = "不可租";
            this.btnRent.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "label2";
            // 
            // btnNotRent
            // 
            this.btnNotRent.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnNotRent.Location = new System.Drawing.Point(17, 135);
            this.btnNotRent.Name = "btnNotRent";
            this.btnNotRent.Size = new System.Drawing.Size(75, 23);
            this.btnNotRent.TabIndex = 6;
            this.btnNotRent.Text = "可租";
            this.btnNotRent.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnAll.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnAll.Location = new System.Drawing.Point(17, 91);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 4;
            this.btnAll.Text = "全部";
            this.btnAll.UseVisualStyleBackColor = false;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 481);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Main";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblRoom;
        public System.Windows.Forms.TextBox txtRoom;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 签订合同ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 预定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缴费ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNotRent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnMovedOut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnVacantHousingUnit;
    }
}
namespace WinForm.双缓冲实例
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNormal = new System.Windows.Forms.Button();
            this.btnCloseNormal = new System.Windows.Forms.Button();
            this.btnDoubleBuffering = new System.Windows.Forms.Button();
            this.btnCloseDoubleBuffering = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // btnNormal
            // 
            this.btnNormal.Location = new System.Drawing.Point(212, 185);
            this.btnNormal.Name = "btnNormal";
            this.btnNormal.Size = new System.Drawing.Size(75, 23);
            this.btnNormal.TabIndex = 1;
            this.btnNormal.Text = "普通绘制";
            this.btnNormal.UseVisualStyleBackColor = true;
            this.btnNormal.Click += new System.EventHandler(this.btnNormal_Click);
            // 
            // btnCloseNormal
            // 
            this.btnCloseNormal.Location = new System.Drawing.Point(293, 185);
            this.btnCloseNormal.Name = "btnCloseNormal";
            this.btnCloseNormal.Size = new System.Drawing.Size(92, 23);
            this.btnCloseNormal.TabIndex = 2;
            this.btnCloseNormal.Text = "关闭普通绘制";
            this.btnCloseNormal.UseVisualStyleBackColor = true;
            this.btnCloseNormal.Click += new System.EventHandler(this.btnCloseNormal_Click);
            // 
            // btnDoubleBuffering
            // 
            this.btnDoubleBuffering.Location = new System.Drawing.Point(212, 245);
            this.btnDoubleBuffering.Name = "btnDoubleBuffering";
            this.btnDoubleBuffering.Size = new System.Drawing.Size(75, 23);
            this.btnDoubleBuffering.TabIndex = 3;
            this.btnDoubleBuffering.Text = "双缓冲绘制";
            this.btnDoubleBuffering.UseVisualStyleBackColor = true;
            this.btnDoubleBuffering.Click += new System.EventHandler(this.btnDoubleBuffering_Click);
            // 
            // btnCloseDoubleBuffering
            // 
            this.btnCloseDoubleBuffering.Location = new System.Drawing.Point(293, 245);
            this.btnCloseDoubleBuffering.Name = "btnCloseDoubleBuffering";
            this.btnCloseDoubleBuffering.Size = new System.Drawing.Size(92, 23);
            this.btnCloseDoubleBuffering.TabIndex = 4;
            this.btnCloseDoubleBuffering.Text = "关闭双缓冲绘制";
            this.btnCloseDoubleBuffering.UseVisualStyleBackColor = true;
            this.btnCloseDoubleBuffering.Click += new System.EventHandler(this.btnCloseDoubleBuffering_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 287);
            this.Controls.Add(this.btnCloseDoubleBuffering);
            this.Controls.Add(this.btnDoubleBuffering);
            this.Controls.Add(this.btnCloseNormal);
            this.Controls.Add(this.btnNormal);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNormal;
        private System.Windows.Forms.Button btnCloseNormal;
        private System.Windows.Forms.Button btnDoubleBuffering;
        private System.Windows.Forms.Button btnCloseDoubleBuffering;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}


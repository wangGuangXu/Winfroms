namespace EasyPhoto.ColorProcess
{
  partial class GrayDialog
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
        this.btnCancel = new System.Windows.Forms.Button();
        this.btnOk = new System.Windows.Forms.Button();
        this.groupBox = new System.Windows.Forms.GroupBox();
        this.rad3 = new System.Windows.Forms.RadioButton();
        this.rad2 = new System.Windows.Forms.RadioButton();
        this.rad1 = new System.Windows.Forms.RadioButton();
        this.panel1 = new System.Windows.Forms.Panel();
        this.groupBox.SuspendLayout();
        this.SuspendLayout();
        // 
        // btnCancel
        // 
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.Location = new System.Drawing.Point(75, 122);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(51, 23);
        this.btnCancel.TabIndex = 2;
        this.btnCancel.Text = "ȡ��";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        // 
        // btnOk
        // 
        this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.btnOk.Location = new System.Drawing.Point(12, 122);
        this.btnOk.Name = "btnOk";
        this.btnOk.Size = new System.Drawing.Size(51, 23);
        this.btnOk.TabIndex = 1;
        this.btnOk.Text = "ȷ��";
        this.btnOk.UseVisualStyleBackColor = true;
        this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
        // 
        // groupBox
        // 
        this.groupBox.Controls.Add(this.rad3);
        this.groupBox.Controls.Add(this.rad2);
        this.groupBox.Controls.Add(this.rad1);
        this.groupBox.Location = new System.Drawing.Point(12, 19);
        this.groupBox.Name = "groupBox";
        this.groupBox.Size = new System.Drawing.Size(114, 88);
        this.groupBox.TabIndex = 14;
        this.groupBox.TabStop = false;
        this.groupBox.Text = "�Ҷȷ���";
        // 
        // rad3
        // 
        this.rad3.AutoSize = true;
        this.rad3.Location = new System.Drawing.Point(16, 64);
        this.rad3.Name = "rad3";
        this.rad3.Size = new System.Drawing.Size(71, 16);
        this.rad3.TabIndex = 5;
        this.rad3.Text = "ƽ��ֵ��";
        this.rad3.UseVisualStyleBackColor = true;
        this.rad3.CheckedChanged += new System.EventHandler(this.rad3_CheckedChanged);
        // 
        // rad2
        // 
        this.rad2.AutoSize = true;
        this.rad2.Location = new System.Drawing.Point(16, 42);
        this.rad2.Name = "rad2";
        this.rad2.Size = new System.Drawing.Size(71, 16);
        this.rad2.TabIndex = 4;
        this.rad2.Text = "���ֵ��";
        this.rad2.UseVisualStyleBackColor = true;
        this.rad2.CheckedChanged += new System.EventHandler(this.rad2_CheckedChanged);
        // 
        // rad1
        // 
        this.rad1.AutoSize = true;
        this.rad1.Checked = true;
        this.rad1.Location = new System.Drawing.Point(16, 20);
        this.rad1.Name = "rad1";
        this.rad1.Size = new System.Drawing.Size(95, 16);
        this.rad1.TabIndex = 3;
        this.rad1.TabStop = true;
        this.rad1.Text = "��Ȩƽ��ֵ��";
        this.rad1.UseVisualStyleBackColor = true;
        this.rad1.CheckedChanged += new System.EventHandler(this.rad1_CheckedChanged);
        // 
        // panel1
        // 
        this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panel1.Location = new System.Drawing.Point(132, 12);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(190, 166);
        this.panel1.TabIndex = 15;
        // 
        // GrayDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(338, 189);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnOk);
        this.Controls.Add(this.groupBox);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "GrayDialog";
        this.ShowIcon = false;
        this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "�Ҷ�";
        this.Load += new System.EventHandler(this.GrayDialog_Load);
        this.groupBox.ResumeLayout(false);
        this.groupBox.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.RadioButton rad3;
    private System.Windows.Forms.RadioButton rad2;
    private System.Windows.Forms.RadioButton rad1;
    private System.Windows.Forms.Panel panel1;
  }
}
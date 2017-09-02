namespace EasyPhoto.ColorProcess
{
  partial class DirectionDialog
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
        this.btnOk = new System.Windows.Forms.Button();
        this.groupBox = new System.Windows.Forms.GroupBox();
        this.angleTextBox = new System.Windows.Forms.TextBox();
        this.angleChooser = new EasyPhoto.EPControl.AngleChooser();
        this.btnCancel = new System.Windows.Forms.Button();
        this.panel1 = new System.Windows.Forms.Panel();
        this.groupBox.SuspendLayout();
        this.SuspendLayout();
        // 
        // btnOk
        // 
        this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.btnOk.Location = new System.Drawing.Point(13, 110);
        this.btnOk.Name = "btnOk";
        this.btnOk.Size = new System.Drawing.Size(51, 23);
        this.btnOk.TabIndex = 1;
        this.btnOk.Text = "ȷ��";
        this.btnOk.UseVisualStyleBackColor = true;
        this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
        // 
        // groupBox
        // 
        this.groupBox.Controls.Add(this.angleTextBox);
        this.groupBox.Controls.Add(this.angleChooser);
        this.groupBox.Location = new System.Drawing.Point(12, 12);
        this.groupBox.Name = "groupBox";
        this.groupBox.Size = new System.Drawing.Size(111, 88);
        this.groupBox.TabIndex = 10;
        this.groupBox.TabStop = false;
        this.groupBox.Text = "����";
        // 
        // angleTextBox
        // 
        this.angleTextBox.Location = new System.Drawing.Point(72, 40);
        this.angleTextBox.Name = "angleTextBox";
        this.angleTextBox.ReadOnly = true;
        this.angleTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.angleTextBox.Size = new System.Drawing.Size(29, 21);
        this.angleTextBox.TabIndex = 4;
        this.angleTextBox.Text = "E";
        // 
        // angleChooser
        // 
        this.angleChooser.Angle = 0;
        this.angleChooser.Location = new System.Drawing.Point(6, 20);
        this.angleChooser.Name = "angleChooser";
        this.angleChooser.Size = new System.Drawing.Size(60, 60);
        this.angleChooser.TabIndex = 3;
        this.angleChooser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.angleChooser_MouseMove);
        // 
        // btnCancel
        // 
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.Location = new System.Drawing.Point(72, 110);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(51, 23);
        this.btnCancel.TabIndex = 2;
        this.btnCancel.Text = "ȡ��";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        // 
        // panel1
        // 
        this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panel1.Location = new System.Drawing.Point(129, 12);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(189, 178);
        this.panel1.TabIndex = 11;
        // 
        // DirectionDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(330, 202);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.btnOk);
        this.Controls.Add(this.groupBox);
        this.Controls.Add(this.btnCancel);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "DirectionDialog";
        this.ShowIcon = false;
        this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "�˷��򸡵�";
        this.Load += new System.EventHandler(this.DirectionDialog_Load);
        this.groupBox.ResumeLayout(false);
        this.groupBox.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.TextBox angleTextBox;
    private EasyPhoto.EPControl.AngleChooser angleChooser;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Panel panel1;
  }
}
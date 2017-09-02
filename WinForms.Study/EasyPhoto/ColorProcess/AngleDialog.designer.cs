namespace EasyPhoto.ColorProcess
{
  partial class AngleDialog
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
        this.angleNumericUpDown = new System.Windows.Forms.NumericUpDown();
        this.angleChooser = new EasyPhoto.EPControl.AngleChooser();
        this.panel1 = new System.Windows.Forms.Panel();
        this.groupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.angleNumericUpDown)).BeginInit();
        this.SuspendLayout();
        // 
        // btnCancel
        // 
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.Location = new System.Drawing.Point(84, 110);
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
        this.btnOk.Location = new System.Drawing.Point(25, 110);
        this.btnOk.Name = "btnOk";
        this.btnOk.Size = new System.Drawing.Size(51, 23);
        this.btnOk.TabIndex = 1;
        this.btnOk.Text = "ȷ��";
        this.btnOk.UseVisualStyleBackColor = true;
        this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
        // 
        // groupBox
        // 
        this.groupBox.Controls.Add(this.angleNumericUpDown);
        this.groupBox.Controls.Add(this.angleChooser);
        this.groupBox.Location = new System.Drawing.Point(12, 12);
        this.groupBox.Name = "groupBox";
        this.groupBox.Size = new System.Drawing.Size(130, 86);
        this.groupBox.TabIndex = 10;
        this.groupBox.TabStop = false;
        this.groupBox.Text = "�Ƕ�";
        // 
        // angleNumericUpDown
        // 
        this.angleNumericUpDown.Location = new System.Drawing.Point(72, 40);
        this.angleNumericUpDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
        this.angleNumericUpDown.Name = "angleNumericUpDown";
        this.angleNumericUpDown.Size = new System.Drawing.Size(48, 21);
        this.angleNumericUpDown.TabIndex = 4;
        this.angleNumericUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
        this.angleNumericUpDown.ValueChanged += new System.EventHandler(this.angleNumericUpDown_ValueChanged);
        // 
        // angleChooser
        // 
        this.angleChooser.Angle = 30;
        this.angleChooser.Location = new System.Drawing.Point(6, 20);
        this.angleChooser.Name = "angleChooser";
        this.angleChooser.Size = new System.Drawing.Size(60, 60);
        this.angleChooser.TabIndex = 3;
        this.angleChooser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.angleChooser_MouseMove);
        // 
        // panel1
        // 
        this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panel1.Location = new System.Drawing.Point(148, 12);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(209, 199);
        this.panel1.TabIndex = 11;
        // 
        // AngleDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(369, 223);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnOk);
        this.Controls.Add(this.groupBox);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "AngleDialog";
        this.ShowIcon = false;
        this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "�Ƕ�";
        this.Load += new System.EventHandler(this.AngleDialog_Load);
        this.groupBox.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.angleNumericUpDown)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private EasyPhoto.EPControl.AngleChooser angleChooser;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.NumericUpDown angleNumericUpDown;
    private System.Windows.Forms.Panel panel1;
  }
}
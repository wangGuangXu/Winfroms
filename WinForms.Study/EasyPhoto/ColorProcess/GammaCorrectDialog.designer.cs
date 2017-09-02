namespace EasyPhoto.ColorProcess
{
  partial class GammaCorrectDialog
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
        this.groupBox = new System.Windows.Forms.GroupBox();
        this.degreeUpDown = new System.Windows.Forms.NumericUpDown();
        this.degreeTrackBar = new System.Windows.Forms.TrackBar();
        this.btnCancel = new System.Windows.Forms.Button();
        this.btnOk = new System.Windows.Forms.Button();
        this.panel1 = new System.Windows.Forms.Panel();
        this.groupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.degreeUpDown)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.degreeTrackBar)).BeginInit();
        this.SuspendLayout();
        // 
        // groupBox
        // 
        this.groupBox.Controls.Add(this.degreeUpDown);
        this.groupBox.Controls.Add(this.degreeTrackBar);
        this.groupBox.Location = new System.Drawing.Point(12, 167);
        this.groupBox.Name = "groupBox";
        this.groupBox.Size = new System.Drawing.Size(254, 60);
        this.groupBox.TabIndex = 14;
        this.groupBox.TabStop = false;
        this.groupBox.Text = "Gamma ����";
        // 
        // degreeUpDown
        // 
        this.degreeUpDown.DecimalPlaces = 2;
        this.degreeUpDown.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
        this.degreeUpDown.Location = new System.Drawing.Point(191, 24);
        this.degreeUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
        this.degreeUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            131072});
        this.degreeUpDown.Name = "degreeUpDown";
        this.degreeUpDown.Size = new System.Drawing.Size(50, 21);
        this.degreeUpDown.TabIndex = 4;
        this.degreeUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
        this.degreeUpDown.ValueChanged += new System.EventHandler(this.degreeUpDown_ValueChanged);
        // 
        // degreeTrackBar
        // 
        this.degreeTrackBar.AutoSize = false;
        this.degreeTrackBar.Location = new System.Drawing.Point(4, 24);
        this.degreeTrackBar.Maximum = 100;
        this.degreeTrackBar.Minimum = 1;
        this.degreeTrackBar.Name = "degreeTrackBar";
        this.degreeTrackBar.Size = new System.Drawing.Size(184, 26);
        this.degreeTrackBar.TabIndex = 3;
        this.degreeTrackBar.TickFrequency = 0;
        this.degreeTrackBar.Value = 50;
        this.degreeTrackBar.Scroll += new System.EventHandler(this.degreeTrackBar_Scroll);
        // 
        // btnCancel
        // 
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.Location = new System.Drawing.Point(194, 233);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(51, 23);
        this.btnCancel.TabIndex = 13;
        this.btnCancel.Text = "ȡ��";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        // 
        // btnOk
        // 
        this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.btnOk.Location = new System.Drawing.Point(128, 233);
        this.btnOk.Name = "btnOk";
        this.btnOk.Size = new System.Drawing.Size(51, 23);
        this.btnOk.TabIndex = 12;
        this.btnOk.Text = "ȷ��";
        this.btnOk.UseVisualStyleBackColor = true;
        // 
        // panel1
        // 
        this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panel1.Location = new System.Drawing.Point(40, 12);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(200, 149);
        this.panel1.TabIndex = 15;
        // 
        // GammaCorrectDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(278, 264);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.groupBox);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnOk);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "GammaCorrectDialog";
        this.ShowIcon = false;
        this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Gamma ����";
        this.groupBox.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.degreeUpDown)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.degreeTrackBar)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.TrackBar degreeTrackBar;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.NumericUpDown degreeUpDown;
    private System.Windows.Forms.Panel panel1;
  }
}
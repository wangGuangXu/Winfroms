namespace EasyPhoto.ColorProcess
{
  partial class MotionBlurDialog
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
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.distanceUpDown = new System.Windows.Forms.NumericUpDown();
        this.distanceTrackBar = new System.Windows.Forms.TrackBar();
        this.panel1 = new System.Windows.Forms.Panel();
        this.groupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.angleNumericUpDown)).BeginInit();
        this.groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.distanceUpDown)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.distanceTrackBar)).BeginInit();
        this.SuspendLayout();
        // 
        // btnCancel
        // 
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.Location = new System.Drawing.Point(258, 160);
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
        this.btnOk.Location = new System.Drawing.Point(201, 160);
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
        this.angleNumericUpDown.TabIndex = 6;
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
        this.angleChooser.TabIndex = 5;
        this.angleChooser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.angleChooser_MouseMove);
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.distanceUpDown);
        this.groupBox1.Controls.Add(this.distanceTrackBar);
        this.groupBox1.Location = new System.Drawing.Point(12, 131);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(183, 53);
        this.groupBox1.TabIndex = 32;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "����";
        // 
        // distanceUpDown
        // 
        this.distanceUpDown.Location = new System.Drawing.Point(123, 22);
        this.distanceUpDown.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
        this.distanceUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
        this.distanceUpDown.Name = "distanceUpDown";
        this.distanceUpDown.Size = new System.Drawing.Size(48, 21);
        this.distanceUpDown.TabIndex = 4;
        this.distanceUpDown.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
        this.distanceUpDown.ValueChanged += new System.EventHandler(this.distanceUpDown_ValueChanged);
        // 
        // distanceTrackBar
        // 
        this.distanceTrackBar.AutoSize = false;
        this.distanceTrackBar.Location = new System.Drawing.Point(6, 20);
        this.distanceTrackBar.Maximum = 200;
        this.distanceTrackBar.Minimum = 1;
        this.distanceTrackBar.Name = "distanceTrackBar";
        this.distanceTrackBar.Size = new System.Drawing.Size(114, 26);
        this.distanceTrackBar.TabIndex = 3;
        this.distanceTrackBar.TickFrequency = 0;
        this.distanceTrackBar.Value = 25;
        this.distanceTrackBar.Scroll += new System.EventHandler(this.distanceTrackBar_Scroll);
        // 
        // panel1
        // 
        this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panel1.Location = new System.Drawing.Point(163, 12);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(139, 110);
        this.panel1.TabIndex = 33;
        // 
        // MotionBlurDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(325, 200);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnOk);
        this.Controls.Add(this.groupBox);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "MotionBlurDialog";
        this.ShowIcon = false;
        this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "�˶�ģ��";
        this.Load += new System.EventHandler(this.MotionBlurDialog_Load);
        this.groupBox.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.angleNumericUpDown)).EndInit();
        this.groupBox1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.distanceUpDown)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.distanceTrackBar)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private EasyPhoto.EPControl.AngleChooser angleChooser;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TrackBar distanceTrackBar;
    private System.Windows.Forms.NumericUpDown distanceUpDown;
    private System.Windows.Forms.NumericUpDown angleNumericUpDown;
    private System.Windows.Forms.Panel panel1;
  }
}
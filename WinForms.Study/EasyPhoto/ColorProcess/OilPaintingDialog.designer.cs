namespace EasyPhoto.ColorProcess
{
  partial class OilPaintingDialog
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
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.btnCancel = new System.Windows.Forms.Button();
        this.coarsenessTrackBar = new System.Windows.Forms.TrackBar();
        this.btnOk = new System.Windows.Forms.Button();
        this.brushSizeTrackBar = new System.Windows.Forms.TrackBar();
        this.groupBox = new System.Windows.Forms.GroupBox();
        this.coarsenessUpDown = new System.Windows.Forms.NumericUpDown();
        this.brushSizeUpDown = new System.Windows.Forms.NumericUpDown();
        this.panel1 = new System.Windows.Forms.Panel();
        ((System.ComponentModel.ISupportInitialize)(this.coarsenessTrackBar)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.brushSizeTrackBar)).BeginInit();
        this.groupBox.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.coarsenessUpDown)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.brushSizeUpDown)).BeginInit();
        this.SuspendLayout();
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(6, 61);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(47, 12);
        this.label2.TabIndex = 5;
        this.label2.Text = "�ֲڶ�:";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(6, 29);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(59, 12);
        this.label1.TabIndex = 5;
        this.label1.Text = "��ˢ�ߴ�:";
        // 
        // btnCancel
        // 
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.Location = new System.Drawing.Point(185, 328);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(51, 23);
        this.btnCancel.TabIndex = 2;
        this.btnCancel.Text = "ȡ��";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        // 
        // coarsenessTrackBar
        // 
        this.coarsenessTrackBar.AutoSize = false;
        this.coarsenessTrackBar.Location = new System.Drawing.Point(65, 56);
        this.coarsenessTrackBar.Maximum = 255;
        this.coarsenessTrackBar.Minimum = 1;
        this.coarsenessTrackBar.Name = "coarsenessTrackBar";
        this.coarsenessTrackBar.Size = new System.Drawing.Size(159, 26);
        this.coarsenessTrackBar.TabIndex = 5;
        this.coarsenessTrackBar.TickFrequency = 0;
        this.coarsenessTrackBar.Value = 50;
        this.coarsenessTrackBar.Scroll += new System.EventHandler(this.coarsenessTrackBar_Scroll);
        // 
        // btnOk
        // 
        this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.btnOk.Location = new System.Drawing.Point(77, 328);
        this.btnOk.Name = "btnOk";
        this.btnOk.Size = new System.Drawing.Size(51, 23);
        this.btnOk.TabIndex = 1;
        this.btnOk.Text = "ȷ��";
        this.btnOk.UseVisualStyleBackColor = true;
        this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
        // 
        // brushSizeTrackBar
        // 
        this.brushSizeTrackBar.AutoSize = false;
        this.brushSizeTrackBar.Location = new System.Drawing.Point(65, 24);
        this.brushSizeTrackBar.Maximum = 8;
        this.brushSizeTrackBar.Minimum = 1;
        this.brushSizeTrackBar.Name = "brushSizeTrackBar";
        this.brushSizeTrackBar.Size = new System.Drawing.Size(159, 26);
        this.brushSizeTrackBar.TabIndex = 3;
        this.brushSizeTrackBar.TickFrequency = 0;
        this.brushSizeTrackBar.Value = 3;
        this.brushSizeTrackBar.Scroll += new System.EventHandler(this.brushSizeTrackBar_Scroll);
        // 
        // groupBox
        // 
        this.groupBox.Controls.Add(this.coarsenessUpDown);
        this.groupBox.Controls.Add(this.brushSizeUpDown);
        this.groupBox.Controls.Add(this.coarsenessTrackBar);
        this.groupBox.Controls.Add(this.brushSizeTrackBar);
        this.groupBox.Controls.Add(this.label2);
        this.groupBox.Controls.Add(this.label1);
        this.groupBox.Location = new System.Drawing.Point(12, 230);
        this.groupBox.Name = "groupBox";
        this.groupBox.Size = new System.Drawing.Size(302, 92);
        this.groupBox.TabIndex = 10;
        this.groupBox.TabStop = false;
        this.groupBox.Text = "�ͻ�Ч������";
        // 
        // coarsenessUpDown
        // 
        this.coarsenessUpDown.Location = new System.Drawing.Point(232, 59);
        this.coarsenessUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
        this.coarsenessUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
        this.coarsenessUpDown.Name = "coarsenessUpDown";
        this.coarsenessUpDown.Size = new System.Drawing.Size(50, 21);
        this.coarsenessUpDown.TabIndex = 6;
        this.coarsenessUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
        this.coarsenessUpDown.ValueChanged += new System.EventHandler(this.coarsenessUpDown_ValueChanged);
        // 
        // brushSizeUpDown
        // 
        this.brushSizeUpDown.Location = new System.Drawing.Point(232, 26);
        this.brushSizeUpDown.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
        this.brushSizeUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
        this.brushSizeUpDown.Name = "brushSizeUpDown";
        this.brushSizeUpDown.Size = new System.Drawing.Size(50, 21);
        this.brushSizeUpDown.TabIndex = 4;
        this.brushSizeUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
        this.brushSizeUpDown.ValueChanged += new System.EventHandler(this.brushSizeUpDown_ValueChanged);
        // 
        // panel1
        // 
        this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panel1.Location = new System.Drawing.Point(24, 12);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(274, 204);
        this.panel1.TabIndex = 11;
        // 
        // OilPaintingDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(325, 361);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnOk);
        this.Controls.Add(this.groupBox);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "OilPaintingDialog";
        this.ShowIcon = false;
        this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "�ͻ�";
        this.Load += new System.EventHandler(this.OilPaintingDialog_Load);
        ((System.ComponentModel.ISupportInitialize)(this.coarsenessTrackBar)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.brushSizeTrackBar)).EndInit();
        this.groupBox.ResumeLayout(false);
        this.groupBox.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.coarsenessUpDown)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.brushSizeUpDown)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TrackBar coarsenessTrackBar;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.TrackBar brushSizeTrackBar;
    private System.Windows.Forms.GroupBox groupBox;
    private System.Windows.Forms.NumericUpDown brushSizeUpDown;
    private System.Windows.Forms.NumericUpDown coarsenessUpDown;
    private System.Windows.Forms.Panel panel1;
  }
}
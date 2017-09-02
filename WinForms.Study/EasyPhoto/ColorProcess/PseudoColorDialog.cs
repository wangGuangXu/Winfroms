using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;

namespace EasyPhoto.ColorProcess
{
  public partial class PseudoColorDialog : Form
  {
      Bitmap srcImage = null;
      public bool IsFinish = false;
      public Bitmap FinalImage = null;

    Color[] colorTable = new Color[16];

    /// <summary>
    /// ��ȡɫ��ӳ���
    /// </summary>
    public Color[] ColorTable
    {
      get
      {
        return colorTable;
      }
    }

    public PseudoColorDialog(Bitmap image)
    {
      InitializeComponent();

      this.srcImage = image;
      this.panel1.BackgroundImage = srcImage;
    }

    private void PseudoColorDialog_Load(object sender, EventArgs e)
    {
      // ��ʼ��ɫ��ӳ���
      colorTable[0] = Color.FromArgb(0x00, 0x00, 0x00);
      colorTable[1] = Color.FromArgb(0x00, 0x00, 0x55);
      colorTable[2] = Color.FromArgb(0x00, 0x55, 0x00);
      colorTable[3] = Color.FromArgb(0x55, 0x00, 0x00);
      colorTable[4] = Color.FromArgb(0x3F, 0x3F, 0x3F);
      colorTable[5] = Color.FromArgb(0x55, 0x00, 0x55);
      colorTable[6] = Color.FromArgb(0x00, 0x00, 0xFF);
      colorTable[7] = Color.FromArgb(0x55, 0x55, 0x00);
      colorTable[8] = Color.FromArgb(0x00, 0xFF, 0x00);
      colorTable[9] = Color.FromArgb(0xFF, 0x00, 0x00);
      colorTable[10] = Color.FromArgb(0x80, 0x80, 0x80);
      colorTable[11] = Color.FromArgb(0x00, 0xFF, 0xFF);
      colorTable[12] = Color.FromArgb(0xFF, 0xFF, 0x00);
      colorTable[13] = Color.FromArgb(0xFF, 0xFF, 0xFF);
      colorTable[14] = Color.FromArgb(0x00, 0x55, 0x55);
      colorTable[15] = Color.FromArgb(0xFF, 0x00, 0xFF);

      // ��ʼ����屳��ɫ
      this.label11.BackColor = colorTable[0];
      this.label12.BackColor = colorTable[1];
      this.label13.BackColor = colorTable[2];
      this.label14.BackColor = colorTable[3];
      this.label21.BackColor = colorTable[4];
      this.label22.BackColor = colorTable[5];
      this.label23.BackColor = colorTable[6];
      this.label24.BackColor = colorTable[7];
      this.label31.BackColor = colorTable[8];
      this.label32.BackColor = colorTable[9];
      this.label33.BackColor = colorTable[10];
      this.label34.BackColor = colorTable[11];
      this.label41.BackColor = colorTable[12];
      this.label42.BackColor = colorTable[13];
      this.label43.BackColor = colorTable[14];
      this.label44.BackColor = colorTable[15];

      UpdateCanvas();
    }

    private void colorTableRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      this.groupBox.Enabled = this.colorTableRadioButton.Checked;
      UpdateCanvas();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.IsFinish = false;
        this.Dispose();
    }

    private void ChangeBackColor( System.Windows.Forms.Label sender)
    {
      if (colorDialog.ShowDialog() == DialogResult.OK)
      {
        // ����屳��ɫ��Ϊָ��ɫ
        sender.BackColor = colorDialog.Color;

        // ��ɫ�ʱ���Ϊָ��ɫ
        string RowCol = sender.Name.Substring(sender.Name.Length - 2);
        int iRowCol = Convert.ToInt32(RowCol);
        int idx = (iRowCol / 10 - 1) * 4 + (iRowCol % 10 - 1);
        colorTable[idx] = colorDialog.Color;

        UpdateCanvas();
      }
    }

    private void UpdateCanvas()
    {
      Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
      Adjustment a = new Adjustment();

      if (this.curveRadioButton.Checked)
      {
        dstImage = a.PseudoColor((Bitmap)srcImage.Clone());
      }
      else
      {
        dstImage = a.PseudoColor((Bitmap)srcImage.Clone(), this.ColorTable);
      }

      this.panel1.BackgroundImage = dstImage;
      this.FinalImage = (Bitmap)dstImage.Clone();
    }

    private void label11_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label12_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label13_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label14_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label21_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label22_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label23_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label24_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label31_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label32_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label33_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label34_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label41_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label42_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label43_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void label44_Click(object sender, EventArgs e)
    {
      ChangeBackColor((System.Windows.Forms.Label)sender);
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        this.IsFinish = true;
        this.Dispose();
    }


  }
}
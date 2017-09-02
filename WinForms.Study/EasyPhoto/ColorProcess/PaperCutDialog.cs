using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;

namespace EasyPhoto.ColorProcess
{
    public partial class PaperCutDialog : Form
    {
        Bitmap srcImage = null;
        public bool IsFinish = false;
        public Bitmap FinalImage = null;

        public PaperCutDialog(Bitmap image)
        {
            InitializeComponent();

            this.srcImage = image;
            this.panel1.BackgroundImage = image;
        }

        private void PaperCutDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsFinish = false;
            this.Dispose();
        }

        private void thresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            this.thresholdUpDown.Value = this.thresholdTrackBar.Value;
        }

        private void thresholdUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.thresholdTrackBar.Value = (int)this.thresholdUpDown.Value;
            UpdateCanvas();
        }

        private void bgLabel_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.bgLabel.BackColor = colorDialog.Color;
                UpdateCanvas();
            }
        }

        private void fgLabel_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.fgLabel.BackColor = colorDialog.Color;
                UpdateCanvas();
            }
        }

        private void UpdateCanvas()
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
            Effect e = new Effect();

            dstImage = e.PaperCut((Bitmap)srcImage.Clone(), this.Threshold, this.BgColor, this.FgColor);

            this.FinalImage = (Bitmap)dstImage.Clone();
            this.panel1.BackgroundImage = dstImage;
        }


        /// <summary>
        /// 获取阈值
        /// </summary>
        public byte Threshold
        {
            get
            {
                return (byte)this.thresholdTrackBar.Value;
            }
        }

        /// <summary>
        /// 获取背景色
        /// </summary>
        public Color BgColor
        {
            get
            {
                return this.bgLabel.BackColor;
            }
        }

        /// <summary>
        /// 获取前景色
        /// </summary>
        public Color FgColor
        {
            get
            {
                return this.fgLabel.BackColor;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.IsFinish = true;
            this.Dispose();
        }

    }
}
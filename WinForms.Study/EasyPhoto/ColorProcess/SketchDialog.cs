using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;

namespace EasyPhoto.ColorProcess
{
    public partial class SketchDialog : Form
    {
        Bitmap srcImage = null;
        public bool IsFinish = false;
        public Bitmap FinalImage = null;

        public SketchDialog(Bitmap image)
        {
            InitializeComponent();

            this.srcImage = image;
            this.panel1.BackgroundImage = image;
        }

        private void SketchDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsFinish = false;
            this.Dispose();
        }

        private void radPencil_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void radSketch_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
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

        private void UpdateCanvas()
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
            Effect e = new Effect();

            if (this.radSketch.Checked)
                dstImage = e.Sketch((Bitmap)srcImage.Clone(), this.Threshold);
            else
                dstImage = e.Pencil((Bitmap)srcImage.Clone(), this.Threshold);

            this.FinalImage = (Bitmap)dstImage.Clone();
            this.panel1.BackgroundImage = dstImage;
        }

        /// <summary>
        /// 获取颜色值
        /// </summary>
        public byte Threshold
        {
            get
            {
                return (byte)this.thresholdTrackBar.Value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.IsFinish = true;
            this.Dispose();
        }

    }
}
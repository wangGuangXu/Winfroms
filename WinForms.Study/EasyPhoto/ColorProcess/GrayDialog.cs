using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;

namespace EasyPhoto.ColorProcess
{
    public partial class GrayDialog : Form
    {
        Bitmap srcImage = null;
        public bool IsFinish = false;
        public Bitmap FinalImage = null;

        public GrayDialog(Bitmap image)
        {
            InitializeComponent();

            this.srcImage = image;
            this.panel1.BackgroundImage = image;
        }

        private void GrayDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsFinish = false;
            this.Dispose();
        }

        private void rad1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void rad2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void rad3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
            GrayProcessing gp = new GrayProcessing();

            dstImage = gp.Gray((Bitmap)srcImage.Clone(), this.GrayMethod);

            this.FinalImage = (Bitmap)dstImage.Clone();
            this.panel1.BackgroundImage = dstImage;
        }

        /// <summary>
        /// 获取灰度方法
        /// </summary>
        public GrayProcessing.GrayMethod GrayMethod
        {
            get
            {
                GrayProcessing.GrayMethod grayMethod = GrayProcessing.GrayMethod.WeightAveraging;

                if (this.rad2.Checked)
                    grayMethod = GrayProcessing.GrayMethod.Maximum;
                if (this.rad3.Checked)
                    grayMethod = GrayProcessing.GrayMethod.Average;

                return grayMethod;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.IsFinish = true;
            this.Dispose();
        }

    }
}
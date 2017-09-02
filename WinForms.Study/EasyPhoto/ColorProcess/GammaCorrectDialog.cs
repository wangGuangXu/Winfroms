using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;

namespace EasyPhoto.ColorProcess
{
    public partial class GammaCorrectDialog : Form
    {
        Bitmap srcImage = null;
        public bool IsFinish = false;
        public Bitmap FinalImage = null;

        public GammaCorrectDialog(Bitmap image)
        {
            InitializeComponent();


            this.srcImage = image;
            this.panel1.BackgroundImage = image;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsFinish = false;
            this.Dispose();
        }

        private void degreeTrackBar_Scroll(object sender, EventArgs e)
        {
            int pixel = this.degreeTrackBar.Value;
            double gamma = 0;
            if (pixel <= 50)
            {
                gamma = pixel / 50.0;
            }
            else
            {
                gamma = (0.08 * pixel - 3);
            }

            this.degreeUpDown.Value = (decimal)gamma;
            UpdateCanvas();
        }

        private void degreeUpDown_ValueChanged(object sender, EventArgs e)
        {
            double gamma = (double)this.degreeUpDown.Value;
            int pixel = 0;
            if (gamma <= 1)
            {
                pixel = (int)(gamma * 50.0);
            }
            else
            {
                pixel = (int)(12.5 * gamma + 37.5);
            }

            this.degreeTrackBar.Value = pixel;
            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
            Adjustment a = new Adjustment();

            dstImage = a.GammaCorrect((Bitmap)srcImage.Clone(), this.Degree);

            this.panel1.BackgroundImage = dstImage;
            this.FinalImage = (Bitmap)dstImage.Clone();
        }

        /// <summary>
        /// 获取 Gamma 矫正值
        /// </summary>
        public double Degree
        {
            get
            {
                return (double)(this.degreeUpDown.Value);
            }
        }

    }
}
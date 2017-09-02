using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;

namespace EasyPhoto.ColorProcess
{
    public partial class ColorBalanceDialog : Form
    {
        public Bitmap srcImage = null;
        public Bitmap finalImage = null;
        public bool IsFinish = false;

        public ColorBalanceDialog(Bitmap image)
        {
            InitializeComponent();

            this.srcImage = image;
            this.finalImage = image;
            this.panel1.BackgroundImage = this.srcImage;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsFinish = false;
            this.Dispose();
        }

        private void redTrackBar_Scroll(object sender, EventArgs e)
        {
            this.redUpDown.Value = this.redTrackBar.Value;
        }

        private void greenTrackBar_Scroll(object sender, EventArgs e)
        {
            this.greenUpDown.Value = this.greenTrackBar.Value;
        }

        private void blueTrackBar_Scroll(object sender, EventArgs e)
        {
            this.blueUpDown.Value = this.blueTrackBar.Value;
        }

        private void redUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.redTrackBar.Value = (int)this.redUpDown.Value;
            UpdateCanvas();
        }

        private void greenUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.greenTrackBar.Value = (int)this.greenUpDown.Value;
            UpdateCanvas();
        }

        private void blueUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.blueTrackBar.Value = (int)this.blueUpDown.Value;
            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
            Adjustment a = new Adjustment();

            dstImage = a.ColorBalance((Bitmap)srcImage.Clone(), this.Red, this.Green, this.Blue);
            this.panel1.BackgroundImage = dstImage;
            this.finalImage = (Bitmap)dstImage.Clone();
        }

        /// <summary>
        /// 获取红色分量值
        /// </summary>
        public int Red
        {
            get
            {
                return this.redTrackBar.Value;
            }
        }

        /// <summary>
        /// 获取绿色分量值
        /// </summary>
        public int Green
        {
            get
            {
                return this.greenTrackBar.Value;
            }
        }

        /// <summary>
        /// 获取蓝色分量值
        /// </summary>
        public int Blue
        {
            get
            {
                return this.blueTrackBar.Value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            
            this.IsFinish = true;
            this.Dispose();
        }


    }
}
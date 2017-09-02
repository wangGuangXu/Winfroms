using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;


namespace EasyPhoto.ColorProcess
{
    public partial class MotionBlurDialog : Form
    {
        Bitmap srcImage = null;
        public bool IsFinish = false;
        public Bitmap FinalImage = null;

        public MotionBlurDialog(Bitmap image)
        {
            InitializeComponent();

            this.srcImage = image;
            this.panel1.BackgroundImage = image;
        }

        private void MotionBlurDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsFinish = false;
            this.Dispose();
        }

        private void distanceTrackBar_Scroll(object sender, EventArgs e)
        {
            this.distanceUpDown.Value = this.distanceTrackBar.Value;
            UpdateCanvas();
        }

        private void distanceUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.distanceTrackBar.Value = (int)this.distanceUpDown.Value;
            UpdateCanvas();
        }

        private void angleChooser_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.angleNumericUpDown.Value = this.angleChooser.Angle;
                UpdateCanvas();
            }
        }

        private void angleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.angleChooser.Angle = (int)this.angleNumericUpDown.Value;
            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
            Effect e = new Effect();

            dstImage = e.MotionBlur((Bitmap)srcImage.Clone(), this.Angle, this.Distance);
            this.panel1.BackgroundImage = dstImage;
            this.FinalImage = (Bitmap)dstImage.Clone();
        }

        /// <summary>
        /// 获取方向角度
        /// </summary>
        public int Angle
        {
            get
            {
                return this.angleChooser.Angle;
            }

        }

        /// <summary>
        /// 获取距离
        /// </summary>
        public int Distance
        {
            get
            {
                return this.distanceTrackBar.Value;
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.IsFinish = true;
            this.Dispose();
        }

    }
}
using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;

namespace EasyPhoto.ColorProcess
{
    public partial class AngleDialog : Form
    {
        /// <summary>
        /// 支持方法
        /// </summary>
        public enum SupportMethod
        {
            /// <summary>
            /// 旋转
            /// </summary>
            Rotate,

            /// <summary>
            /// 径向模糊
            /// </summary>
            RadialBlur,

            /// <summary>
            /// 灰度浮雕
            /// </summary>
            Emboss,

            /// <summary>
            /// 彩色浮雕
            /// </summary>
            Relief,

            /// <summary>
            /// 查找边缘
            /// </summary>
            FindEdge,

            /// <summary>
            /// 无
            /// </summary>
            None
        }

        Bitmap srcImage = null;
        public bool IsFinish = false;
        public Bitmap FinalImage = null;
        private SupportMethod support = SupportMethod.None;

        public AngleDialog(Bitmap image)
        {
            InitializeComponent();

            this.srcImage = image;
            this.panel1.BackgroundImage = image;
        }

        private void AngleDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsFinish = false;
            this.Dispose();
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
            ImageTransform it = new ImageTransform();
            Effect e = new Effect();

            if (support == SupportMethod.Rotate)
            {
                dstImage = it.Rotate((Bitmap)srcImage.Clone(), this.Angle);
                this.srcImage = dstImage;
                return;
            }

            switch (support)
            {
                case SupportMethod.RadialBlur:
                    dstImage = e.RadialBlur((Bitmap)srcImage.Clone(), this.Angle);
                    break;

                case SupportMethod.Emboss:
                    dstImage = e.Emboss((Bitmap)srcImage.Clone(), this.Angle);
                    break;

                case SupportMethod.Relief:
                    dstImage = e.Relief((Bitmap)srcImage.Clone(), this.Angle);
                    break;

                case SupportMethod.FindEdge:
                    dstImage = e.FindEdge((Bitmap)srcImage.Clone(), this.Angle);
                    break;

                default:
                    break;
            }
            this.panel1.BackgroundImage = dstImage;
            this.FinalImage = (Bitmap)dstImage.Clone();
        }

        /// <summary>
        /// 获取或设置用户指定的角度值
        /// </summary>
        public int Angle
        {
            get
            {
                return this.angleChooser.Angle;
            }
            set
            {
                this.angleChooser.Angle = value;
                this.angleNumericUpDown.Value = value;
            }
        }

        /// <summary>
        /// 获取或设置描述
        /// </summary>
        public string Description
        {
            get
            {
                return this.groupBox.Text;
            }
            set
            {
                this.groupBox.Text = value;
            }
        }

        /// <summary>
        /// 获取或设置支持方法
        /// </summary>
        public SupportMethod Support
        {
            get
            {
                return support;
            }
            set
            {
                support = value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.IsFinish = true;
            this.Dispose();
        }

    }
}
using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;

namespace EasyPhoto.ColorProcess
{
    public partial class DegreeDialog : Form
    {
        /// <summary>
        /// ֧�ַ���
        /// </summary>
        public enum SupportMethod
        {
            /// <summary>
            /// ����
            /// </summary>
            Brightness,

            /// <summary>
            /// �Աȶ�
            /// </summary>
            Contrast,

            /// <summary>
            /// ��ֵ��
            /// </summary>
            Thresholding,

            /// <summary>
            /// ��
            /// </summary>
            Sharpen,

            /// <summary>
            /// �ۻ��ɰ�
            /// </summary>
            UnsharpMask,

            /// <summary>
            /// �����ӵ�
            /// </summary>
            AddNoise,

            /// <summary>
            /// ѩ���ӵ�
            /// </summary>
            Sprinkle,

            /// <summary>
            /// ��ѹ
            /// </summary>
            Pinch,

            /// <summary>
            /// ����
            /// </summary>
            Swirl,

            /// <summary>
            /// ����
            /// </summary>
            Wave,

            /// <summary>
            /// Ħ����
            /// </summary>
            MoireFringe,

            /// <summary>
            /// ��ɢ
            /// </summary>
            Diffuse,

            /// <summary>
            /// �ƹ�
            /// </summary>
            Lighting,

            /// <summary>
            /// ������
            /// </summary>
            Mosaic,

            /// <summary>
            /// ����С����
            /// </summary>
            ClearSmallArea,

            /// <summary>
            /// ��Ե��ǿ
            /// </summary>
            EdgeEnhance,

            /// <summary>
            /// ��Ե���⻯
            /// </summary>
            EdgeHomogenize,

            /// <summary>
            /// ��
            /// </summary>
            None
        }

        Bitmap srcImage = null;
        private SupportMethod support = SupportMethod.None;
        public bool IsFinish = false;
        public Bitmap FinalImage = null;

        public DegreeDialog(Bitmap image)
        {
            InitializeComponent();

            this.srcImage = image;
            this.panel1.BackgroundImage = image;
        }

        private void DegreeDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsFinish = false;
            this.Dispose();
        }

        private void degreeTrackBar_Scroll(object sender, EventArgs e)
        {
            this.degreeUpDown.Value = this.degreeTrackBar.Value;
        }

        private void degreeUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.degreeTrackBar.Value = (int)this.degreeUpDown.Value;
            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
            GrayProcessing gp = new GrayProcessing();
            Adjustment a = new Adjustment();
            Effect e = new Effect();
            Segmentation s = new Segmentation();
            EdgeDetect ed = new EdgeDetect();

            switch (support)
            {
                case SupportMethod.Brightness:
                    dstImage = a.Brightness((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.Contrast:
                    dstImage = a.Contrast((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.Thresholding:
                    dstImage = gp.Thresholding((Bitmap)srcImage.Clone(), (byte)(255-this.Degree));
                    break;

                case SupportMethod.Sharpen:
                    dstImage = e.Sharpen((Bitmap)srcImage.Clone(), (byte)this.Degree);
                    break;

                case SupportMethod.UnsharpMask:
                    dstImage = e.UnsharpMask((Bitmap)srcImage.Clone(), (byte)this.Degree);
                    break;

                case SupportMethod.AddNoise:
                    dstImage = e.AddNoise((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.Sprinkle:
                    dstImage = e.Sprinkle((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.Pinch:
                    dstImage = e.Pinch((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.Swirl:
                    dstImage = e.Swirl((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.Wave:
                    dstImage = e.Wave((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.MoireFringe:
                    dstImage = e.MoireFringe((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.Diffuse:
                    dstImage = e.Diffuse((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.Lighting:
                    dstImage = e.Lighting((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.Mosaic:
                    dstImage = e.Mosaic((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.ClearSmallArea:
                    dstImage = s.ClearSmallArea((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.EdgeEnhance:
                    dstImage = ed.EdgeEnhance((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                case SupportMethod.EdgeHomogenize:
                    dstImage = ed.EdgeHomogenize((Bitmap)srcImage.Clone(), this.Degree);
                    break;

                default:
                    break;
            }

            this.panel1.BackgroundImage = dstImage;
            this.FinalImage = (Bitmap)dstImage.Clone();
        }


        /// <summary>
        /// ��ȡ�����÷���
        /// </summary>
        public int Degree
        {
            get
            {
                return this.degreeTrackBar.Value;
            }
            set
            {
                this.degreeTrackBar.Value = value;
                this.degreeUpDown.Value = value;
            }
        }

        /// <summary>
        /// ��ȡ�����÷������ֵ
        /// </summary>
        public int Maximum
        {
            get
            {
                return this.degreeTrackBar.Maximum;
            }
            set
            {
                this.degreeTrackBar.Maximum = value;
                this.degreeUpDown.Maximum = value;
            }
        }

        /// <summary>
        /// ��ȡ�����÷�����Сֵ
        /// </summary>
        public int Minimum
        {
            get
            {
                return this.degreeTrackBar.Minimum;
            }
            set
            {
                this.degreeTrackBar.Minimum = value;
                this.degreeUpDown.Minimum = value;
            }
        }

        /// <summary>
        /// ��ȡ����������
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
        /// ��ȡ������֧�ַ���
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
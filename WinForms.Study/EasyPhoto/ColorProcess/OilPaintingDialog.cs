using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;

namespace EasyPhoto.ColorProcess
{
    public partial class OilPaintingDialog : Form
    {
        Bitmap srcImage = null;
        public bool IsFinish = false;
        public Bitmap FinalImage = null;

        public OilPaintingDialog(Bitmap image)
        {
            InitializeComponent();

            this.srcImage = image;
            this.panel1.BackgroundImage = image;
        }

        private void OilPaintingDialog_Load(object sender, EventArgs e)
        {
            UpdateCanvas();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsFinish = false;
            this.Dispose();
        }

        private void brushSizeTrackBar_Scroll(object sender, EventArgs e)
        {
            this.brushSizeUpDown.Value = this.brushSizeTrackBar.Value;
        }

        private void coarsenessTrackBar_Scroll(object sender, EventArgs e)
        {
            this.coarsenessUpDown.Value = this.coarsenessTrackBar.Value;
        }

        private void brushSizeUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.brushSizeTrackBar.Value = (int)this.brushSizeUpDown.Value;
            UpdateCanvas();
        }

        private void coarsenessUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.coarsenessTrackBar.Value = (int)this.coarsenessUpDown.Value;
            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
            Effect e = new Effect();

            dstImage = e.OilPainting((Bitmap)srcImage.Clone(), this.BrushSize, this.Coarseness);

            this.FinalImage = (Bitmap)dstImage.Clone();
            this.panel1.BackgroundImage = dstImage;
        }


        /// <summary>
        /// 获取画刷尺寸
        /// </summary>
        public int BrushSize
        {
            get
            {
                return this.brushSizeTrackBar.Value;
            }
        }

        /// <summary>
        /// 获取粗糙度
        /// </summary>
        public byte Coarseness
        {
            get
            {
                return (byte)this.coarsenessTrackBar.Value;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.IsFinish = true;
            this.Dispose();
        }

    }
}
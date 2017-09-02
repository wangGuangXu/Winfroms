using System;
using System.Windows.Forms;
using System.Drawing;
using EasyPhoto.ImageProcess;

namespace EasyPhoto.ColorProcess
{
    public partial class DirectionDialog : Form
    {
        Bitmap srcImage = null;
        public bool IsFinish = false;
        public Bitmap FinalImage = null;

        public DirectionDialog(Bitmap image)
        {
            InitializeComponent();

            this.srcImage = image;
            this.panel1.BackgroundImage = image;
        }

        private void DirectionDialog_Load(object sender, EventArgs e)
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
            string[] ADirection = { "E", "NE", "N", "NW", "W", "SW", "S", "SE" };
            this.angleTextBox.Text = ADirection[((angleChooser.Angle + 22) / 45) % 8];
            UpdateCanvas();
        }

        private void UpdateCanvas()
        {
            Bitmap dstImage = new Bitmap(srcImage.Width, srcImage.Height);
            Effect e = new Effect();

            dstImage = e.Emboss((Bitmap)srcImage.Clone(), this.Direction);

            this.FinalImage = (Bitmap)dstImage.Clone();
            this.panel1.BackgroundImage = dstImage;
        }

        /// <summary>
        /// 获取用户指定的方向
        /// </summary>
        public Effect.Direction Direction
        {
            get
            {
                Effect.Direction direction = Effect.Direction.E;
                switch ((angleChooser.Angle - 22) / 45)
                {
                    case 0:
                        direction = Effect.Direction.E;
                        break;

                    case 1:
                        direction = Effect.Direction.NE;
                        break;

                    case 2:
                        direction = Effect.Direction.N;
                        break;

                    case 3:
                        direction = Effect.Direction.NW;
                        break;

                    case 4:
                        direction = Effect.Direction.W;
                        break;

                    case 5:
                        direction = Effect.Direction.SW;
                        break;

                    case 6:
                        direction = Effect.Direction.S;
                        break;

                    case 7:
                        direction = Effect.Direction.SE;
                        break;
                }

                return direction;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.IsFinish = true;
            this.Dispose();
        }

    }
}
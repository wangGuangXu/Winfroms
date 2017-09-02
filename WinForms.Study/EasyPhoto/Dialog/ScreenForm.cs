using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyPhoto.Dialog
{
    public partial class ScreenForm : Form
    {
        public bool IsSelect = false;
        public Image ScreenImage;
        public Image FinalImage;
        public Image SelectImage;
        public bool IsMoveDown = false;
        public Graphics g;
        public Point currentMouseDownPosition = new Point();
        public Rectangle temprectangle;

        public ScreenForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserMouse, true);

            ScreenImage = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            Graphics g = Graphics.FromImage(ScreenImage);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size);

            this.Width = ScreenImage.Width;
            this.Height = ScreenImage.Height;
            this.SelectImage=(Bitmap)this.ScreenImage.Clone();
            this.BackgroundImage = SelectImage;
            this.g = this.CreateGraphics();

            this.MouseDoubleClick += new MouseEventHandler(ScreenForm_MouseDoubleClick);
            this.MouseDown += new MouseEventHandler(ScreenForm_MouseDown);
            this.MouseMove += new MouseEventHandler(ScreenForm_MouseMove);
            this.MouseUp += new MouseEventHandler(ScreenForm_MouseUp);
        }

        void ScreenForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMoveDown)
            {
                this.IsMoveDown = false;
                if (e.Location == this.currentMouseDownPosition)
                {
                    return;
                }
                this.IsSelect = true;
                Graphics selectg = Graphics.FromImage(this.SelectImage);
                selectg.DrawRectangle(new Pen(new SolidBrush(Color.Red)), temprectangle);
                this.FinalImage = new Bitmap(this.temprectangle.Width, this.temprectangle.Height);
                Graphics fianlg = Graphics.FromImage(this.FinalImage);
                fianlg.DrawImage(this.ScreenImage, new Rectangle(0, 0, this.FinalImage.Width, this.FinalImage.Height), this.temprectangle, GraphicsUnit.Pixel);
            }
        }

        void ScreenForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMoveDown)
            {
                this.Refresh();
                if ((e.X > this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(this.currentMouseDownPosition, new Size(e.X - this.currentMouseDownPosition.X, e.Y - this.currentMouseDownPosition.Y));
                else if ((e.X > this.currentMouseDownPosition.X) && (e.Y < currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(this.currentMouseDownPosition.X, e.Y, e.X - this.currentMouseDownPosition.X, this.currentMouseDownPosition.Y - e.Y);
                else if ((e.X < this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(e.X, this.currentMouseDownPosition.Y, this.currentMouseDownPosition.X - e.X, e.Y - this.currentMouseDownPosition.Y);
                else
                    temprectangle = new Rectangle(e.X, e.Y, this.currentMouseDownPosition.X - e.X, this.currentMouseDownPosition.Y - e.Y);
                g.DrawRectangle(new Pen(new SolidBrush(Color.Red)), temprectangle);
            }
            
        }

        void ScreenForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!IsMoveDown)
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.Refresh();
                    this.SelectImage = (Bitmap)this.ScreenImage.Clone();
                    this.IsMoveDown = true;
                    this.currentMouseDownPosition = e.Location;
                }
            }
        }

        void ScreenForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                this.IsSelect = false;
            }
            this.Dispose();
        }

        
    }
}

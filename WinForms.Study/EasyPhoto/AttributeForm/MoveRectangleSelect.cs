using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyPhoto.AttributeForm
{
    public partial class MoveRectangleSelect : UserControl
    {
        public MainForm SubParent;
        private int paperX;
        private int paperY;
        private int paperWidth;
        private int paperHeight;
        private Brush paperBrush;

        public Brush PaperBrush
        {
            set { this.paperBrush = (Brush)value.Clone(); }
            get { return this.paperBrush; }
        }

        public int PaperX
        {
            set { paperX = value; }
            get { return paperX; }
        }

        public int PaperY
        {
            set { this.paperY = value; }
            get { return this.paperY; }
        }

        public int PaperWidth
        {
            set { this.paperWidth = value; }
            get { return this.paperWidth; }
        }

        public int PaperHeight
        {
            set { this.paperHeight = value; }
            get { return this.paperHeight; }
        }

        public MoveRectangleSelect()
        {
            InitializeComponent();
        }

        private void MoveRectangleSelect_Load(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = this.paperX;
            this.numericUpDown2.Value = this.paperY;
            this.numericUpDown3.Value = this.paperWidth;
            this.numericUpDown4.Value = this.paperHeight;
            this.panel1.BackColor = ((SolidBrush)this.paperBrush).Color;
            this.XYMax();
            this.BoundsMax();

            this.panel1.BackColorChanged += new EventHandler(panel1_BackColorChanged);
            this.numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_LostFocus);
            this.numericUpDown2.ValueChanged += new EventHandler(numericUpDown2_LostFocus);
            this.numericUpDown3.ValueChanged += new EventHandler(numericUpDown3_LostFocus);
            this.numericUpDown4.ValueChanged += new EventHandler(numericUpDown4_LostFocus);
            this.panel1.MouseClick += new MouseEventHandler(panel1_MouseClick);
        }

        private void XYMax()
        {
            this.numericUpDown1.Maximum = this.SubParent.currentPaper.baseImage.Width - this.numericUpDown3.Value;
            this.numericUpDown2.Maximum = this.SubParent.currentPaper.baseImage.Height - this.numericUpDown4.Value;
        }

        private void BoundsMax()
        {
            this.numericUpDown3.Maximum = this.SubParent.currentPaper.baseImage.Width - this.numericUpDown1.Value;
            this.numericUpDown4.Maximum = this.SubParent.currentPaper.baseImage.Height - this.numericUpDown2.Value;
        }

        void numericUpDown4_LostFocus(object sender, EventArgs e)
        {
            ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).CHeight = (int)this.numericUpDown4.Value;
            this.SubParent.currentPaper.RePaint();
            this.XYMax();
        }

        void numericUpDown3_LostFocus(object sender, EventArgs e)
        {
            ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).CWidth = (int)this.numericUpDown3.Value;
            this.SubParent.currentPaper.RePaint();
            this.XYMax();
        }

        void numericUpDown2_LostFocus(object sender, EventArgs e)
        {
            ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).StartPoint = new Point((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);
            this.SubParent.currentPaper.RePaint();
            this.BoundsMax();
        }

        void numericUpDown1_LostFocus(object sender, EventArgs e)
        {
            ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).StartPoint = new Point((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);
            this.SubParent.currentPaper.RePaint();
            this.BoundsMax();
        }

        void panel1_BackColorChanged(object sender, EventArgs e)
        {
            ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).CBrush = new SolidBrush(this.panel1.BackColor);
            this.SubParent.currentPaper.RePaint();
        }

        void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Dialog.RGBSelectDlg rgbselect = new Dialog.RGBSelectDlg(this.panel1.BackColor.R, this.panel1.BackColor.G, this.panel1.BackColor.B);
            Point temppoint = new Point();
            int scrwidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            int scrheight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            temppoint.X = this.Parent.Parent.Parent.Location.X + this.Parent.Parent.Location.X + this.Parent.Location.X + +this.Location.X + this.panel1.Location.X - rgbselect.Width;
            temppoint.Y = this.Parent.Parent.Parent.Location.Y + this.Parent.Parent.Location.Y + this.Parent.Location.Y + this.panel1.Location.Y + this.Location.Y + this.panel1.Height - rgbselect.Height;
            rgbselect.SetLocation(temppoint);
            rgbselect.ShowDialog();
            if (rgbselect.IsFinish)
            {
                this.panel1.BackColor = rgbselect.SelectColor;
            }
        }
    }
}

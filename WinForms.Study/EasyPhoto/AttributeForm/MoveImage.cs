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
    public partial class MoveImage : UserControl
    {
        public MainForm SubParent;
        private int paperX;
        private int paperY;
        private int paperWidth;
        private int paperHeight;
        private float diaph;

        public float Diaph
        {
            set { this.diaph = value; }
            get { return this.diaph; }
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

        public MoveImage()
        {
            InitializeComponent();
        }

        private void MoveImage_Load(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = this.paperX;
            this.numericUpDown2.Value = this.paperY;
            this.numericUpDown3.Value = this.paperWidth;
            this.numericUpDown4.Value = this.paperHeight;
            this.numericUpDown5.Value = (int)(this.diaph*100);
            this.XYMax();
            this.BoundsMax();

            this.numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_LostFocus);
            this.numericUpDown2.ValueChanged += new EventHandler(numericUpDown2_LostFocus);
            this.numericUpDown3.ValueChanged += new EventHandler(numericUpDown3_LostFocus);
            this.numericUpDown4.ValueChanged += new EventHandler(numericUpDown4_LostFocus);
            this.numericUpDown5.ValueChanged += new EventHandler(numericUpDown5_ValueChanged);
        }

        void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).CDiaph = ((float)this.numericUpDown5.Value) / 100;
            this.SubParent.currentPaper.RePaint();
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
    }
}

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
    public partial class MoveFont : UserControl
    {
        public MainForm SubParent;

        private int paperX;
        private int paperY;
        private string paperString;
        private Color paperColor;
        private Font paperFont;

        public Font PaperFont
        {
            set { this.paperFont = (Font)value.Clone(); }
            get { return this.paperFont; }
        }

        public Color PaperColor
        {
            set { this.paperColor = value; }
            get { return this.paperColor; }
        }

        public int PaperX
        {
            set { this.paperX = value; }
            get { return this.paperX; }
        }

        public int PaperY
        {
            set { this.paperY = value; }
            get { return this.paperY; }
        }

        public string PaperString
        {
            set { this.paperString = value; }
            get { return this.paperString; }
        }

        public MoveFont()
        {
            InitializeComponent();
        }

        private void MoveFont_Load(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = this.paperX;
            this.numericUpDown2.Value = this.paperY;
            this.panel1.BackColor = this.paperColor;
            this.textBox1.Text = this.paperString;

            this.numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_ValueChanged);
            this.numericUpDown2.ValueChanged += new EventHandler(numericUpDown2_ValueChanged);
            this.textBox1.LostFocus += new EventHandler(textBox1_LostFocus);
            this.panel1.BackColorChanged += new EventHandler(panel1_BackColorChanged);
            this.panel1.MouseClick += new MouseEventHandler(panel1_MouseClick);

            MaxXY();
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

        void MaxXY()
        {
            Graphics g = this.SubParent.currentPaper.CreateGraphics();
            SizeF tempsizef = g.MeasureString(this.paperString, this.paperFont);
            this.numericUpDown1.Maximum = (decimal)(this.SubParent.currentPaper.baseImage.Width - tempsizef.Width);
            this.numericUpDown2.Maximum = (decimal)(this.SubParent.currentPaper.baseImage.Height - tempsizef.Height);
        }

        void panel1_BackColorChanged(object sender, EventArgs e)
        {
            ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).CBrush = new SolidBrush(this.panel1.BackColor);
            this.SubParent.currentPaper.RePaint();
        }

        void textBox1_LostFocus(object sender, EventArgs e)
        {
            ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).CString = this.textBox1.Text.Trim();
            this.SubParent.currentPaper.RePaint();
        }

        void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).StartPoint = new Point((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);
            this.SubParent.currentPaper.RePaint();
            MaxXY();
        }

        void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).StartPoint = new Point((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);
            this.SubParent.currentPaper.RePaint();
            MaxXY();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FontDialog fd = new FontDialog();
            fd.Font = this.paperFont;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                ((CustomClass.Cell)this.SubParent.currentPaper.arrayList[this.SubParent.currentPaper.ActiveNum]).CFont = fd.Font;
                this.SubParent.currentPaper.RePaint();
            }
        }
    }
}

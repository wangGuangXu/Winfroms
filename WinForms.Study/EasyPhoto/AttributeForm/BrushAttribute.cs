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
    public partial class BrushAttribute : UserControl
    {
        public MainForm SubParent = null;
        public Brush PaperBrush = null;

        public BrushAttribute(Color color)
        {
            InitializeComponent();
            this.panel1.BackColor = color;
            this.panel1.BackColorChanged += new EventHandler(panel1_BackColorChanged);
            this.numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_ValueChanged);
        }

        void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.SubParent.BrushRadius = (int)this.numericUpDown1.Value;
        }

        void panel1_BackColorChanged(object sender, EventArgs e)
        {
            this.SubParent.CurrentBrush = new SolidBrush(this.panel1.BackColor);
        }

        void panel1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Dialog.RGBSelectDlg rgbselect = new Dialog.RGBSelectDlg(this.panel1.BackColor.R, this.panel1.BackColor.G, this.panel1.BackColor.B);
            Point temppoint = new Point();
            int scrwidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            int scrheight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            temppoint.X = this.Parent.Parent.Parent.Location.X + this.Parent.Parent.Location.X + this.Parent.Location.X + +this.Location.X+this.panel1.Location.X - rgbselect.Width;
            temppoint.Y = this.Parent.Parent.Parent.Location.Y + this.Parent.Parent.Location.Y + this.Parent.Location.Y + this.panel1.Location.Y+this.Location.Y + this.panel1.Height - rgbselect.Height;
            rgbselect.SetLocation(temppoint);
            rgbselect.ShowDialog();
            if (rgbselect.IsFinish)
            {
                this.panel1.BackColor = rgbselect.SelectColor;
            }
        }

        private void BrushAttribute_Load(object sender, EventArgs e)
        {
            this.numericUpDown1.Value = this.SubParent.BrushRadius;
            this.panel1.BackColor = ((SolidBrush)this.PaperBrush).Color;
        }        
    }
}

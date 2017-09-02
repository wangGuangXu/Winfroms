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
    public partial class StageMoveAttribute : UserControl
    {
        public MainForm SubParent = null;
        private Color stageBackColor;
        private int stageWidth;
        private int stageHeight;
        private int zoom;
        private string stageName;

        public Color StageBackColor
        {
            set { this.stageBackColor = value; }
            get { return stageBackColor; }
        }

        public int StageWidth
        {
            set { this.stageWidth = value; }
            get { return stageWidth; }
        }

        public int StageHeight
        {
            set { this.stageHeight = value; }
            get { return stageHeight; }
        }

        public int Zoom
        {
            set { this.zoom = value; }
            get { return zoom; }
        }

        public string StageName
        {
            set { this.stageName = value; }
            get { return this.stageName; }
        }

        public StageMoveAttribute()
        {
            InitializeComponent();
        }

        private void StageMoveAttribute_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = this.stageName;
            this.numericUpDown1.Value = this.StageWidth;
            this.numericUpDown2.Value = this.StageHeight;
            this.panel1.BackColor = this.StageBackColor;
            if ((this.zoom == 1) || (this.zoom == 2) || (this.zoom == 4) || (this.zoom == 8))
            {
                this.comboBox1.Text = this.zoom.ToString();
            }
            else
            {
                this.comboBox1.Items.Add(this.zoom.ToString());
                this.comboBox1.Text = this.zoom.ToString();
            }

            this.panel1.MouseClick += new MouseEventHandler(panel1_MouseClick);
            this.panel1.BackColorChanged += new EventHandler(panel1_BackColorChanged);
            this.numericUpDown1.LostFocus += new EventHandler(numericUpDown1_LostFocus);
            this.numericUpDown2.LostFocus += new EventHandler(numericUpDown2_LostFocus);
            this.textBox1.LostFocus += new EventHandler(textBox1_LostFocus);
            this.comboBox1.TextChanged += new EventHandler(comboBox1_TextChanged);
        }

        void numericUpDown2_LostFocus(object sender, EventArgs e)
        {
            ((EasyPhoto.EPControl.Stage)this.SubParent.currentPaper).SetBackgroundImage((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value, this.SubParent.currentPaper.BackgroundColor);
        }

        void numericUpDown1_LostFocus(object sender, EventArgs e)
        {
            ((EasyPhoto.EPControl.Stage)this.SubParent.currentPaper).SetBackgroundImage((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value, this.SubParent.currentPaper.BackgroundColor);
        }

        void comboBox1_TextChanged(object sender, EventArgs e)
        {
            float temp = int.Parse(this.comboBox1.Text.Trim());
            float x = this.SubParent.Bounds.Width * (temp - 1) / (2 * temp);
            float y = this.SubParent.Bounds.Height * (temp - 1) / (2 * temp);
            this.SubParent.currentPaper.SetZoom((int)temp, new Point((int)x,(int)y));
        }

        void textBox1_LostFocus(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("画纸名称不能为空！", "错误");
                textBox1.Focus();
                return;
            }
            for (int i = 0; i < this.SubParent.LayerArrayList.Count; i++)
            {
                if (((EasyPhoto.EPControl.Layer)this.SubParent.LayerArrayList[i]).PaperName == textBox1.Text.Trim())
                {
                    MessageBox.Show("图层中已存在该名称", "错误");
                    textBox1.Text = this.SubParent.currentPaper.PaperName;
                    textBox1.Focus();
                    return;
                }
            }
            this.SubParent.currentPaper.PaperName = this.textBox1.Text.Trim();
            this.SubParent.currentPaper.Parent.Text = this.textBox1.Text.Trim();
            this.SubParent.CheckLayerInformaiton();
        }

        void panel1_BackColorChanged(object sender, EventArgs e)
        {
            ((EasyPhoto.EPControl.Stage)this.SubParent.currentPaper).ResetBackColor(this.panel1.BackColor);
            this.SubParent.currentPaper.BackgroundColor = this.panel1.BackColor;
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

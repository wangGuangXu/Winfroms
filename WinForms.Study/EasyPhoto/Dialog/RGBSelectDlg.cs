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
    public partial class RGBSelectDlg : Form
    {
        private bool isfinish = false;
        /// <summary>
        /// 获取是否完成选择
        /// </summary>
        public bool IsFinish
        {
            get { return isfinish; }
        }
        /// <summary>
        /// 获取选择的色彩
        /// </summary>
        public Color SelectColor
        {
            get { return this.panel1.BackColor; }
            set
            {
                this.panel1.BackColor = value;
            }
        }

        

        public RGBSelectDlg(int r, int g, int b)
        {
            InitializeComponent();
            this.panel1.BackColor = Color.FromArgb(r, g, b);
            this.numericUpDown1.Value = r;
            this.numericUpDown2.Value = g;
            this.numericUpDown3.Value = b;
            this.redTrackBar.Value = r;
            this.gTrackBar.Value = g;
            this.bTrackBar.Value = b;
        }

        public void SetLocation(Point location)
        {
            this.Location = location;
        }

        void numericUpDown3_ValueChanged(object sender, System.EventArgs e)
        {
            this.bTrackBar.Value = (int)this.numericUpDown3.Value;
            this.panel1.BackColor = Color.FromArgb(this.redTrackBar.Value,this.gTrackBar.Value,this.bTrackBar.Value);
        }

        void numericUpDown2_ValueChanged(object sender, System.EventArgs e)
        {
            this.gTrackBar.Value = (int)this.numericUpDown2.Value;
            this.panel1.BackColor = Color.FromArgb(this.redTrackBar.Value, this.gTrackBar.Value, this.bTrackBar.Value);
        }

        void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
        {
            this.redTrackBar.Value = (int)this.numericUpDown1.Value;
            this.panel1.BackColor = Color.FromArgb(this.redTrackBar.Value, this.gTrackBar.Value, this.bTrackBar.Value);
        }


        void gTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.numericUpDown2.Value = this.gTrackBar.Value;
            this.panel1.BackColor = Color.FromArgb(this.redTrackBar.Value, this.gTrackBar.Value, this.bTrackBar.Value);
        }

        void redTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.numericUpDown1.Value = this.redTrackBar.Value;
            this.panel1.BackColor = Color.FromArgb(this.redTrackBar.Value, this.gTrackBar.Value, this.bTrackBar.Value);
        }

        void bTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            this.numericUpDown3.Value = this.bTrackBar.Value;
            this.panel1.BackColor = Color.FromArgb(this.redTrackBar.Value, this.gTrackBar.Value, this.bTrackBar.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.isfinish = false;
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.isfinish = true;
            this.Dispose();
        }
    }
}

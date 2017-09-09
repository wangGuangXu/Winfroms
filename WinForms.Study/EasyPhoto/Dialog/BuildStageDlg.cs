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
    public partial class BuildStageDlg : Form
    {
        private int stagewidth;
        private int stageheight;
        private bool buildflag;                  //标志是否成功建立
        private string stageName;                //画纸名称
        private Color stageColor=Color.White;    //画纸底色
        //private RGBSelectDlg rgbselect;

        /// <summary>
        /// 获取设置的宽度
        /// </summary>
        public int StageWidth
        {
            set
            {
                stagewidth = value;
            }
            get { return stagewidth; }
        }

        /// <summary>
        /// 获取设置的长度
        /// </summary>
        public int StageHeight
        {
            set
            {
                stageheight = value;
            }
            get { return stageheight; }
        }

        /// <summary>
        /// 获取是否建立标志
        /// </summary>
        public bool BuildFlag
        {
            get { return buildflag; }
        }

        /// <summary>
        /// 获取画纸名称
        /// </summary>
        public string StageName
        {
            set
            {
                stageName = value;
            }
            get { return stageName; }
        }

        /// <summary>
        /// 获取或设置画纸底色
        /// </summary>
        public Color StageColor
        {
            set
            {
                stageColor = value;
            }
            get { return stageColor; }
        }

        public BuildStageDlg()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            buildflag = false;
            txtCanvasName.Text = DateTime.Now.ToString("MMddHHmmss");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtCanvasName.Text == "")
            {
                MessageBox.Show("未输入画纸名称！", "错误");
                return;
            }
            this.stagewidth = (int)this.numericUpDown1.Value;
            this.stageheight = (int)this.numericUpDown2.Value;
            stageName = txtCanvasName.Text.Trim();
            this.stageColor = panel1.BackColor;

            buildflag = true;
            this.Dispose();
        }

        void panel1_Click(object sender, System.EventArgs e)
        {
            var rgbselect = new RGBSelectDlg(255, 255, 255);
            Point temppoint = new Point();
            int scrwidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            int scrheight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Location.Y < (scrheight - this.Bounds.Height) / 2)
            {
                if (this.Location.X < (scrwidth - this.Bounds.Width) / 2)
                {
                    temppoint.X = this.Location.X + this.panel1.Location.X + this.panel1.Width;
                    temppoint.Y = this.Location.Y + this.panel1.Location.Y;
                }
                else
                {
                    temppoint.X = this.Location.X + this.panel1.Location.X - rgbselect.Bounds.Width;
                    temppoint.Y = this.Location.Y + this.panel1.Location.Y;
                }
            }
            else
            {
                if (this.Location.X < (scrwidth - this.Bounds.Width) / 2)
                {
                    temppoint.X = this.Location.X + this.panel1.Location.X + this.panel1.Width;
                    temppoint.Y = this.Location.Y + this.panel1.Location.Y + this.panel1.Height-rgbselect.Bounds.Height;
                }
                else
                {
                    temppoint.X = this.Location.X + this.panel1.Location.X - rgbselect.Bounds.Width;
                    temppoint.Y = this.Location.Y + this.panel1.Location.Y + this.panel1.Height - rgbselect.Bounds.Height;
                }
            }
            rgbselect.SetLocation(temppoint);
            rgbselect.ShowDialog();
            if (rgbselect.IsFinish)
            {
                this.panel1.BackColor = rgbselect.SelectColor;
                this.stageColor = rgbselect.SelectColor;
            }
        }
        
    }
}

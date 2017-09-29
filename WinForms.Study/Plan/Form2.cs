using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plan
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// 程序运行的目录
        /// </summary>
        public static string applicationDirectory = System.Windows.Forms.Application.StartupPath;
        private List<Point> tempPoints = new List<Point>();
        private bool edit=false;
        public Pen pen;
             
        public Form2()
        {
            InitializeComponent();
            //splitContainer1.Panel2.Paint += Panel2_Paint;
            this.MouseMove += Form2_MouseMove;
        }

        void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            var tempPoint = tempPoints[tempPoints.Count - 1];
            splitContainer1.Panel2.CreateGraphics().DrawLine(new Pen(Color.Yellow, 3), tempPoint, e.Location);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string imgPath = applicationDirectory + @"\Images\background.jpg";

            if (File.Exists(imgPath))
            {
                splitContainer1.Panel2.BackgroundImage = Image.FromFile(imgPath);
                splitContainer1.Panel2.BackgroundImageLayout = ImageLayout.None;
            }
        }

        private void splitContainer1_Panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (edit) return;

            tempPoints.Add(e.Location);
        }

        private void splitContainer1_Panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && edit) return;

            tempPoints.Add(e.Location);
            edit = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            edit = false;
            tempPoints = new List<Point>(); ;
        }

    }
}

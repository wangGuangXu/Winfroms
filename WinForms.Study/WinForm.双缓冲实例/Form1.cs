using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForm.双缓冲实例
{
    /// <summary>
    /// 此示例只是提供一个双缓冲思路的应用，不一定很严谨
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 正常绘图方式
        private void btnNormal_Click(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NormalDraw();
        }

        private void btnCloseNormal_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        } 
        #endregion

        #region 正常绘图
        /// <summary>
        /// 正常绘图
        /// </summary>
        private void NormalDraw()
        {
            DateTime t1 = DateTime.Now;
            Graphics g = this.CreateGraphics();
            //g.SmoothingMode = SmoothingMode.HighQuality;//设置显示的质量
            Brush brush = null;

            bool flag = true;
            if (flag)
            {
                brush = new LinearGradientBrush(new PointF(0.0f, 0.0f), new PointF(700.0f, 300.0f), Color.Red, Color.Blue);
                flag = false;
            }
            else
            {
                brush = new LinearGradientBrush(new PointF(0.0f, 0.0f), new PointF(700.0f, 300.0f), Color.Blue, Color.Red);
                flag = true;
            }
            for (int j = 0; j < 60; j++)
            {
                for (int i = 0; i < 60; i++)
                {
                    g.FillEllipse(brush, i * 10, j * 10, 10, 10);
                }
            }
            DateTime t2 = DateTime.Now;
            TimeSpan sp = t2 - t1;
            float per = 1000 / (sp.Milliseconds == 0 ? 1 : sp.Milliseconds);
            this.label1.Text = "速度：" + per.ToString() + "帧/秒";
        }
        #endregion

        #region 启用双缓冲的绘图
        /// <summary>
        /// 启用双缓冲的绘图
        /// </summary>
        private void EnableDoubleBuffer()
        {
            DateTime t1 = DateTime.Now;

            Bitmap bmp = new Bitmap(6000, 6000);    //在内存中建立一块画布
            Graphics g = Graphics.FromImage(bmp);   //获取这块内存画布的Graphics引用：
            Brush brush = null;

            bool flag = true;
            if (flag)
            {
                brush = new LinearGradientBrush(new PointF(0.0f, 0.0f), new PointF(700.0f, 300.0f), Color.Red, Color.Blue);
                flag = false;
            }
            else
            {
                brush = new LinearGradientBrush(new PointF(0.0f, 0.0f), new PointF(700.0f, 300.0f), Color.Blue, Color.Red);
                flag = true;
            }
            for (int j = 0; j < 60; j++)
            {
                for (int i = 0; i < 60; i++)
                {
                    //在这块内存画布上绘图
                    g.FillEllipse(brush, i * 10, j * 10, 10, 10);
                }
            }

            //将内存画布画到窗口中
            this.CreateGraphics().DrawImage(bmp, 0, 0);

            DateTime t2 = DateTime.Now;
            TimeSpan sp = t2 - t1;
            float per = 1000 / (sp.Milliseconds == 0 ? 1 : sp.Milliseconds);
            this.label1.Text = "速度：" + per.ToString() + "帧/秒";
        }
        #endregion

        #region 双缓冲绘图
        private void btnDoubleBuffering_Click(object sender, EventArgs e)
        {
            timer2.Interval = 10;
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            EnableDoubleBuffer();
        }

        private void btnCloseDoubleBuffering_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        } 
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WinForm.房态图
{
    public partial class RoomLable : UserControl
    {
        #region 基本变量
        private string text = null;//房间文本
        private Brush bgBrush = null;//背景状态
        /// <summary>
        /// 房间状态
        /// </summary>
        public string State { get; set; }
        private string areas = null;//房间面积
        private Form_Main formMain = null;
        #endregion

        #region 构造函数
        public RoomLable()
        {
            InitializeComponent();
        }
        public RoomLable(int width, int height, string text)
        {
            this.Width = width;
            this.Height = height;
            this.text = text;
            this.Paint += new PaintEventHandler(RoomLable_Paint);

            InitializeComponent();
        }
        public RoomLable(Form_Main formMain, int width, int height, string text)
        {
            //启用双缓冲绘图
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);      //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.formMain = formMain;
            this.Height = height;
            this.Width = width;
            this.text =text;
            this.Paint += new PaintEventHandler(RoomLable_Paint);

            InitializeComponent();
        }
        #endregion

        #region 绘制圆角矩形
        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="label"></param>
        /// <param name="brush"></param>
        public void DrawRoundRect(Graphics graphics, Label label, Brush brush)
        {
            float X = float.Parse(label.Width.ToString()) -1;
            float Y = float.Parse(label.Height.ToString()) -1;

            //圆角点坐标
            PointF[] points = {
                new PointF(2,0),
                new PointF(X-2,0),
                new PointF(X-1,1),
                new PointF(X,2),
                new PointF(X,Y-2),
                new PointF(X-1, Y-1),
                new PointF(X-2,Y),
                new PointF(2, Y),
                new PointF(1, Y-1),
                new PointF(0, Y-2),
                new PointF(0, 2),
                new PointF(1,1)
            };

            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);

            Pen pen = new Pen(Color.FromArgb(150, Color.Gray), 1);
            pen.DashStyle = DashStyle.Solid;
            graphics.DrawPath(pen, path);

            graphics.FillPath(brush, path);
        }
        #endregion

        #region 绘制文本
        /// <summary>
        /// 绘制文本
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="label"></param>
        private void DrawString(Graphics graphics, Label label)
        {
            Font font = new Font("宋体", 9.0f, FontStyle.Regular);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            Brush brush = new SolidBrush(Color.White);

            RectangleF rect = new RectangleF(0, 0, label.Width, label.Height - 20);
            
            RectangleF rect2 = new RectangleF(0, 30, label.Width, label.Height -30);//状态

            RectangleF rect3 = new RectangleF(0, 40, label.Width, label.Height -40);//面积

            //绘制房间标题信息
            if (text == null)
            {
                graphics.DrawString(label.Text, font, brush, rect, stringFormat);
            }
            else
            {
                graphics.DrawString(this.text, font, brush, rect, stringFormat);
            }
            //绘制房间状态信息
            //  if (state == null)
            //   graphics.DrawString("空闲", font, brush, label1.Width / 2-10, label1.Height - 15);label1.Width / 2-20, label1.Height - 15
            // else
            graphics.DrawString(State, font, brush, rect2, new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far });
            //graphics.DrawString(this.areas, font, brush, rect3, new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
        }
        #endregion

        #region label事件注册
        /// <summary>
        /// 控件重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RoomLable_Paint(object sender, PaintEventArgs e)
        {
            if (this.bgBrush != null)
            {
                DrawRoundRect(e.Graphics, label1, this.bgBrush);
            }
            else
            {
                DrawRoundRect(e.Graphics, label1, Brushes.Pink);
            }
            DrawString(e.Graphics, label1);
        }

        //鼠标指针进入控件时
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.Red;
            this.BorderStyle = BorderStyle.Fixed3D;
        }
        //鼠标单击控件
        private void label1_Click(object sender, EventArgs e)
        {

        }
        //鼠标离开控件
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.None;
        }
        //鼠标位于控件
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button.ToString() == "Left")
            if (this.text == null)
            {
                this.formMain.txtRoom.Text = label1.Text;
            }
            else
            {
                this.formMain.txtRoom.Text = this.text;
            }
            this.formMain.Refresh();

            if (e.Button.ToString() == "Right")
            {
                Point p = new Point(e.X, e.Y);
                this.formMain.contextMenuStrip1.Show(this, p);
            }
        }
        #endregion

        #region 方法
        public void setMargin(int padding)
        {
            this.Margin = new Padding(padding);
        }
        //设置座标点
        public void setPoint(int x, int y)
        {
            this.Location = new Point(x, y);
        }
        //设置文本
        public void setText(String text)
        {
            this.text ="房号："+ text;
        }
        //设置背景填充色
        public void setBgBrush(Brush brush)
        {
            this.bgBrush = brush;
        }
        //设置状态
        public void setState(String state)
        {
            State = "状态：" + state;
        }

        //设置面积
        public void setAreas(String areas)
        {
            this.areas ="面积："+ areas;
        }

        /// <summary>
        /// 设置房源标签信息
        /// </summary>
        /// <param name="brush">颜色</param>
        /// <param name="state">状态</param>
        /// <param name="areas">面积</param>
        public void SetRoomInfo(Brush brush,string state,string areas)
        {
            this.bgBrush = brush;
            State =state;
            this.areas = "面积：" + areas;
        }

        #endregion

        #region 图片单击事件
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("当前选择房号：" + this.text);
        }
        #endregion

        /// <summary>
        /// 重写
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //用双缓冲从下到上绘制窗口的所有子孙
                cp.ExStyle = 0x02000000;

                return cp;
            }
        }
    }
}

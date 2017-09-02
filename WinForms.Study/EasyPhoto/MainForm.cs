using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyPhoto
{
    public partial class MainForm : Form
    {
        public static string DefaultPath="";
        //程序运行的目录
        public static string ApplicationDirectory = System.Windows.Forms.Application.StartupPath;
        //当前屏幕分别率
        public int ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
        public int ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
        //当前绘画空间大小
        public int DrawClientWidth;
        public int DrawClientHeight;

        //当前项目名称
        public string paperName;
        //当前画纸底色                        
        public Color paperColor;
        //记录图层编号，用于各图层的识别
        public int defaultTabPageNum;
        //新建图层的默认名                
        public string defaultTabPageName;
        //记录当前所在位置
        public EPControl.Paper currentPaper;
        //当前放大系数
        public int Zoom;
        //临时图层
        public EasyPhoto.EPControl.Layer TempLayer;
        //临时元件
        public EasyPhoto.CustomClass.Cell TempCell;
        //是否复制图层
        public bool IsCopyLayer = false;
        //是否复制元件
        public bool IsCopyCell = false;
        
        
        

        #region 工具栏所要使用的成员
        /// <summary>
        /// 工具栏上的工具
        /// </summary>
        public enum ToolSelected
        {
            /// <summary>
            /// 未选定
            /// </summary>
            None,
            /// <summary>
            /// 移动工具
            /// </summary>
            Move,
            /// <summary>
            /// 缩放工具
            /// </summary>
            Zoom,
            /// <summary>
            /// 方形选择工具
            /// </summary>
            RectangleSelect,
            /// <summary>
            /// 椭圆选择工具
            /// </summary>
            EllipseSelect,
            /// <summary>
            /// 直线工具
            /// </summary>
            Line,
            /// <summary>
            /// 多直线工具
            /// </summary>
            Lines,
            /// <summary>
            /// 方形工具
            /// </summary>
            Rectangle,
            /// <summary>
            /// 椭圆工具
            /// </summary>
            Ellipse,
            /// <summary>
            /// 文字工具
            /// </summary>
            Text,
            /// <summary>
            /// 铅笔工具
            /// </summary>
            Pencil,
            /// <summary>
            /// 刷子工具
            /// </summary>
            Brush,
            /// <summary>
            /// 橡皮工具
            /// </summary>
            Eraser,
            /// <summary>
            /// 染料桶工具
            /// </summary>
            Dyestuff,
            /// <summary>
            /// 吸管工具
            /// </summary>
            Sucker,
            /// <summary>
            /// 手型工具
            /// </summary>
            Hand,
            /// <summary>
            /// 放大镜
            /// </summary>
            Magnifier,
            //新增
            MoveLine,
            MoveRectangle,
            MoveEllipse,
            MoveRectangleSelect,
            MoveEllipseSelect,
            MoveText,
            MoveImage,
            MoveNone
        }


        //当前工具选择
        public ToolSelected toolSelected;
        //记录鼠标按下的坐标点
        public Point currentMouseDownPosition;
        //当前画纸上的Graphics实例
        public Graphics currentgraphics;
        //鼠标是否按下
        public bool IsMouseDown = false;
        //鼠标是否单击过
        public bool IsMouseClick = false;
        //当前的画刷
        public Brush CurrentBrush;
        //画刷原型的大小半径
        public int BrushRadius;
        //初当前的铅笔
        public Pen CurrentPen ;
        //临时铅笔
        public Pen TempPen;
        //当前的矩形
        public Rectangle CurrentRectangle;
        //文字工具的输入框
        public TextBox tempTextBox;
        //保存当前文字样式
        public Font CurrentFont;
        //保存临时数值
        public int temprecord;
        //保存临时对象的数组
        public System.Collections.ArrayList tempArrayList = new System.Collections.ArrayList();
        //保存最终结果的数组
        public System.Collections.ArrayList finalArrayList = new System.Collections.ArrayList();
        //保存图层
        public System.Collections.ArrayList LayerArrayList = new System.Collections.ArrayList();
        //保存基层
        public EPControl.Stage CurrentStage;

        //计时器
        public System.Windows.Forms.Timer timer = null;

        //手型
        public Cursor HandCursor;
        //铅笔
        public Cursor PencilCursor;
        //放大镜
        public Cursor MagnifierCursor;
        //橡皮擦
        public Cursor EraserCursor;
        //画刷工具
        public Cursor BrushCursor;



        #endregion



        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.timer = new Timer();
            timer.Interval = 400;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            if (this.CurrentStage == null)
            {
                this.pictureBox1.Image = null;
                this.pictureBox2.Image = null;
            }
            else
            {
                Bitmap temp;
                try
                {
                    temp = (Bitmap)this.CurrentStage.ExportCustomImage();
                }
                catch (Exception e1)
                {
                    this.timer.Start();
                    return;
                }
                if (this.imageTabControl.SelectedIndex == 0)
                {
                    this.pictureBox1.Image = temp;
                    //if (this.pictureBox2.Image != null)
                        //this.pictureBox2.Image.Dispose();
                }
                else
                {
                    //pictureBox1.Image.Dispose();
                    int[] x = new int[256];
                    int xmax = 0;
                    Bitmap xbmp;
                    // 灰度直方图
                    if (this.radioButton1.Checked)
                    {
                        for (int i = 0; i < temp.Width; i++)
                        {
                            for (int j = 0; j < temp.Height; j++)
                            {
                                Color c = temp.GetPixel(i, j);
                                int huiduzhi = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                                x[huiduzhi] += 1;
                            }
                        }
                        for (int i = 0; i < 256; i++)
                        {
                            if (x[i] > xmax)
                                xmax = x[i];
                        }
                        xbmp = new Bitmap(256, 256);
                        for (int i = 0; i < 256; i++)
                        {
                            x[i] = x[i] * 256 / xmax;
                            int j;
                            for (j = 1; j < x[i]; j++)
                            {
                                xbmp.SetPixel(i, 256 - j, Color.FromArgb(i, i, i));
                            }
                            xbmp.SetPixel(i, 256 - j, Color.FromArgb(0, 0, 0));
                        }
                        this.pictureBox2.Image = xbmp;
                    }
                    // R直方图
                    else if (this.radioButton2.Checked)
                    {
                        for (int i = 0; i < temp.Width; i++)
                        {
                            for (int j = 0; j < temp.Height; j++)
                            {
                                x[(temp.GetPixel(i, j).R)] += 1;
                            }
                        }
                        for (int i = 0; i < 256; i++)
                        {
                            if (x[i] > xmax)
                                xmax = x[i];
                        }
                        xbmp = new Bitmap(256, 256);
                        for (int i = 0; i < 256; i++)
                        {
                            x[i] = x[i] * 256 / xmax;
                            int j;
                            for (j = 1; j < x[i]; j++)
                            {
                                xbmp.SetPixel(i, 256 - j, Color.FromArgb(i, 0, 0));
                            }
                            xbmp.SetPixel(i, 256 - j, Color.FromArgb(0, 0, 0));
                        }
                        this.pictureBox2.Image = xbmp;
                    }
                    //G直方图
                    else if (this.radioButton3.Checked)
                    {
                        for (int i = 0; i < temp.Width; i++)
                        {
                            for (int j = 0; j < temp.Height; j++)
                            {
                                x[(temp.GetPixel(i, j).G)] += 1;
                            }
                        }
                        for (int i = 0; i < 256; i++)
                        {
                            if (x[i] > xmax)
                                xmax = x[i];
                        }
                        xbmp = new Bitmap(256, 256);
                        for (int i = 0; i < 256; i++)
                        {
                            x[i] = x[i] * 256 / xmax;
                            int j;
                            for (j = 1; j < x[i]; j++)
                            {
                                xbmp.SetPixel(i, 256 - j, Color.FromArgb(0, i, 0));
                            }
                            xbmp.SetPixel(i, 256 - j, Color.FromArgb(0, 0, 0));
                        }
                        this.pictureBox2.Image = xbmp;
                    }
                    //B直方图
                    else
                    {
                        for (int i = 0; i < temp.Width; i++)
                        {
                            for (int j = 0; j < temp.Height; j++)
                            {
                                x[(temp.GetPixel(i, j).B)] += 1;
                            }
                        }
                        for (int i = 0; i < 256; i++)
                        {
                            if (x[i] > xmax)
                                xmax = x[i];
                        }
                        xbmp = new Bitmap(256, 256);
                        for (int i = 0; i < 256; i++)
                        {
                            x[i] = x[i] * 256 / xmax;
                            int j;
                            for (j = 1; j < x[i]; j++)
                            {
                                xbmp.SetPixel(i, 256 - j, Color.FromArgb(0, 0, i));
                            }
                            xbmp.SetPixel(i, 256 - j, Color.FromArgb(0, 0, 0));
                        }
                        this.pictureBox2.Image = xbmp;
                    }
                }
            }
            this.timer.Start();
        }

        //当选项卡发生变化时
        void mainTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (this.mainTabControl.SelectedTab.Text == this.CurrentStage.PaperName)
            {
                this.currentPaper = this.CurrentStage;
                this.currentPaper.RePaint();
            }
            else
            {
                for (int i = 0; i < this.LayerArrayList.Count; i++)
                {
                    if (this.mainTabControl.SelectedTab.Text == ((EasyPhoto.EPControl.Layer)this.LayerArrayList[i]).PaperName)
                    {
                        this.currentPaper = (EasyPhoto.EPControl.Layer)this.LayerArrayList[i];
                        this.currentPaper.RePaint();
                        break;
                    }
                }
            }
            foreach (ToolStripButton tsbutton in this.mainToolStrip.Items)
            {
                tsbutton.CheckState = CheckState.Unchecked;
            }
            this.toolSelected = ToolSelected.None;
            this.initializeToolSelected();
            this.CheckLayerInformaiton();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            EasyPhoto.FileInfo fileinfo = new FileInfo(".hdk", Application.StartupPath + @"\EasyPhoto.ico", "EasyPhoto文件", Application.StartupPath + @"\EasyPhoto.exe");
            EasyPhoto.FileTypeRegister.RegisterFileType(fileinfo);
            if (MainForm.DefaultPath != "")
            {
                string temp = (string)MainForm.DefaultPath.Clone();
                MainForm.DefaultPath = "";
                this.LoadFile(temp);
                return;
            }

            this.mainTabControl.Visible = false;

            this.DrawClientWidth = this.ClientSize.Width - this.mainToolStrip.Width - this.imageTabControl.Width - 8;
            this.DrawClientHeight = this.ClientSize.Height - this.mainMenuStrip.Height - this.mainStatusStrip.Height - 5;

            //工具栏各工具的初始化
            this.mainTabControl.Visible = false;
            this.treeView1.Visible = false;
            this.CurrentPen = new Pen(new SolidBrush(Color.Black));
            this.TempPen = (Pen)this.CurrentPen.Clone();
            TempPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            this.CurrentBrush = new SolidBrush(Color.Black);
            this.UpdateFont(new FontFamily("宋体"), 12, FontStyle.Regular);
            this.toolSelected = ToolSelected.None;
            this.currentMouseDownPosition = new Point();
            this.currentgraphics = null;
            this.BrushRadius = 1;
            this.IsMouseClick = false;
            this.IsMouseDown = false;
            this.IsCopyCell = false;
            this.IsCopyLayer = false;
            this.TempCell = null;
            this.TempLayer = null;
            this.Zoom = 1;
            defaultTabPageName = "图层";
            currentPaper = null;
            defaultTabPageNum = 0;
            this.paperName = "";
            this.CurrentStage = null;
            this.LayerArrayList.Clear();
            this.tempArrayList.Clear();
            this.finalArrayList.Clear();
            try
            {
                HandCursor = new Cursor(Application.StartupPath + @"\Resources\Hand.cur");
                PencilCursor = new Cursor(Application.StartupPath + @"\Resources\Pencil.cur");
                MagnifierCursor = new Cursor(Application.StartupPath + @"\Resources\Magnifier.cur");
                EraserCursor = new Cursor(Application.StartupPath + @"\Resources\Eraser.cur");
                BrushCursor = new Cursor(Application.StartupPath + @"\Resources\Brush.cur");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "图标加载失败");
                HandCursor = Cursors.Default;
                PencilCursor = Cursors.Default;
                MagnifierCursor = Cursors.Default;
                EraserCursor = Cursors.Default;
                BrushCursor = Cursors.Default;
            }
            
            this.CheckLayerInformaiton();

        }
          
        void treeView1_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            foreach (TabPage tb in this.mainTabControl.Controls)
            {
                if (tb.Text == e.Node.Name)
                {
                    this.mainTabControl.SelectedTab = tb;
                    return;
                }
            }

            for (int i = 0; i < this.LayerArrayList.Count; i++)
            {
                if (e.Node.Name == ((EPControl.Layer)this.LayerArrayList[i]).PaperName)
                {
                    EPControl.Layer temp;
                    temp = (EPControl.Layer)this.LayerArrayList[i];
                    System.Windows.Forms.TabPage newtabpage = new System.Windows.Forms.TabPage();
                    newtabpage.Text = temp.PaperName;
                    newtabpage.Name = temp.PaperName;
                    this.mainTabControl.Controls.Add(newtabpage);
                    newtabpage.Controls.Add(temp);
                    this.currentPaper = temp;
                    this.mainTabControl.SelectedTab = newtabpage;
                    break;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.currentPaper != null)
            {
                this.currentPaper.Invalidate();
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.LayerArrayList.Count; i++)
            {
                if (this.currentPaper == this.LayerArrayList[i])
                {
                    this.LayerArrayList.RemoveAt(i);
                    this.LayerArrayList.Insert(i - 1, this.currentPaper);
                    break;
                }
            }
            this.CheckLayerInformaiton();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.LayerArrayList.Count; i++)
            {
                if (this.currentPaper == this.LayerArrayList[i])
                {
                    this.LayerArrayList.RemoveAt(i);
                    this.LayerArrayList.Insert(i+1, this.currentPaper);
                    break;
                }
            }
            this.CheckLayerInformaiton();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ((EasyPhoto.EPControl.Layer)this.currentPaper).CanSee = !((EasyPhoto.EPControl.Layer)this.currentPaper).CanSee;
            this.CheckLayerInformaiton();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.currentPaper.GetType().ToString() == "EasyPhoto.EPControl.Stage")
            {
                MessageBox.Show("无法删除基层", "错误！");
                return;
            }
            this.LayerArrayList.Remove(this.currentPaper);
            this.mainTabControl.Controls.Remove(this.mainTabControl.SelectedTab);
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Manual.doc");
            }
            catch
            {
                MessageBox.Show("打开用户手册失败，请检查以下原因：\n1、是否安装Microsoft Office Word，帮助文档需要此打开。\n2、请确保同目录下的Manual文件是否存在或损坏。", "加载失败");
            }
            
        }
    }
}
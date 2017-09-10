using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyPhoto.EPControl
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class Paper : UserControl
    {
        public MainForm SuperParent;
        protected int backgroundWidth = 800;
        protected int backgroundHeight = 600;
        protected Color backgroundColor = Color.White;
        protected string paperName = "";
        protected Point startPoint;
        protected int activeNum = -1;

        //背景颜色
        protected SolidBrush backPaperColor = new SolidBrush(Color.DarkGray);
        public Rectangle showRectangle;
        public Bitmap baseImage = null;
        protected Bitmap backImage = null;
        protected Bitmap finalImage = null;
        
        //保存临时元件
        public CustomClass.Cell tempCell = null;
        
        //放大系数
        protected int zoom = 1;

        public System.Collections.ArrayList arrayList = new System.Collections.ArrayList();

        #region 获取或设置属性
        

        public System.Collections.ArrayList GetArrayList()
        {
            if (this.arrayList.Count == 0)
                return null;
            System.Collections.ArrayList temp = new System.Collections.ArrayList();
            for (int i = 0; i < this.arrayList.Count; i++)
            {
                temp.Add(((CustomClass.Cell)this.arrayList[i]).CopyCell());
            }
            return temp;
        }

        public CustomClass.Cell GetTempCell
        {
            get
            {
                if (this.tempCell == null)
                    return null;
                else
                    return this.tempCell.CopyCell();
            }
        }

        /// <summary>
        /// 获取或设置图像开始位置
        /// </summary>
        public Point StartPosition
        {
            get { return this.startPoint; }
            set { this.startPoint = value; }
        }
        /// <summary>
        /// 获取或设置当前激活的元件编号
        /// </summary>
        public int ActiveNum
        {
            set { this.activeNum = value; }
            get { return this.activeNum; }
        }
        //获取或设置背景颜色
        public SolidBrush BackPaperColor
        {
            set { backPaperColor = value; }
            get 
            {
                if (this.backPaperColor != null)
                    return (SolidBrush)backPaperColor.Clone();
                else
                    return null;
            }
        }
        /// <summary>
        /// 获取显示矩形的坐标
        /// </summary>
        public Point ShowRectanglePoint
        {
            get {return this.showRectangle.Location; }
        }
        /// <summary>
        /// 获取或设置背景图片
        /// </summary>
        public Bitmap BackImage
        {
            get
            {
                if (this.backImage != null)
                    return backImage;
                else
                    return null;
            }
            set { backImage = value; }
        }
        /// <summary>
        /// 获取放大系数
        /// </summary>
        public int Zoom
        {
            get { return zoom; }
            set { this.zoom = value; }
        }

        /// <summary>
        /// 获取或设置背景图片的宽度
        /// </summary>
        public int BackgroundWidth
        {
            set { backgroundWidth = value; }
            get { return backgroundWidth; }
        }

        /// <summary>
        /// 获取或设置背景图片的长度
        /// </summary>
        public int BackgroundHeight
        {
            set { backgroundHeight = value; }
            get { return backgroundHeight; }
        }

        /// <summary>
        /// 获取或设置背景图片的颜色
        /// </summary>
        public Color BackgroundColor
        {
            set { backgroundColor = value; }
            get 
            {
                    return backgroundColor;
            }
        }

        /// <summary>
        /// 获取或设置Paper名称
        /// </summary>
        public string PaperName
        {
            set { paperName = value; }
            get { return paperName; }
        }

        public Bitmap FinalImage
        {
            get
            {
                if (this.finalImage != null)
                    return (Bitmap)this.finalImage.Clone();
                else
                    return null;
            }
            set
            {
                this.finalImage = value;
            }
        }
        #endregion


        public Paper(MainForm parent, Size PaperSize)
        {
            InitializeComponent();
            this.SuperParent = parent;
            this.Size = PaperSize;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserMouse, true);
            this.MouseMove += new MouseEventHandler(Paper_MouseMove);

            this.showRectangle = new Rectangle(0, 0, this.Width, this.Height);
        }

        void Paper_MouseMove(object sender, MouseEventArgs e)
        {
            switch (this.SuperParent.toolSelected)
            {
                case EasyPhoto.MainForm.ToolSelected.Brush:
                    Cursor.Current = this.SuperParent.BrushCursor;
                    break;
                case MainForm.ToolSelected.Eraser:
                    Cursor.Current = this.SuperParent.EraserCursor;
                    break;
                case MainForm.ToolSelected.Hand:
                    Cursor.Current = this.SuperParent.HandCursor;
                    break;
                case MainForm.ToolSelected.Line:
                    Cursor.Current = Cursors.Cross;
                    break;
                case MainForm.ToolSelected.Lines:
                    Cursor.Current = Cursors.Cross;
                    break;
                case MainForm.ToolSelected.Magnifier:
                    Cursor.Current = this.SuperParent.MagnifierCursor;
                    break;
                case MainForm.ToolSelected.Pencil:
                    Cursor.Current = this.SuperParent.PencilCursor;
                    break;
                case MainForm.ToolSelected.Rectangle:
                    Cursor.Current = Cursors.Cross;
                    break;
                case MainForm.ToolSelected.RectangleSelect:
                    Cursor.Current = Cursors.Cross;
                    break;
                case MainForm.ToolSelected.Text:
                    Cursor.Current = Cursors.IBeam;
                    break;
                case MainForm.ToolSelected.Ellipse:
                    Cursor.Current = Cursors.Cross;
                    break;
                case MainForm.ToolSelected.EllipseSelect:
                    Cursor.Current = Cursors.Cross;
                    break;
                default:
                    Cursor.Current = Cursors.Default;
                    break;
            }
        }

        public Paper()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserMouse, true);
        }

        

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        #region 向ArrayList添加各种图像单元
        public void AddGraphics(EasyPhoto.GraphicsType type,Point startPoint,int width,int height,Pen pen)
        {
            CustomClass.Cell newCell = new EasyPhoto.CustomClass.Cell(type, startPoint, width, height, pen);
            arrayList.Add(newCell);
            Graphics backg = Graphics.FromImage(backImage);
            newCell.CellPaint(backg);
            this.OnPaint(null);
             //this.Parent.Invalidate();
        }

        public void AddGraphics(EasyPhoto.GraphicsType type, Point startPoint, int width, int height, Brush brush)
        {
            CustomClass.Cell newCell = new EasyPhoto.CustomClass.Cell(type, startPoint, width, height,brush);
            arrayList.Add(newCell);
            Graphics backg = Graphics.FromImage(backImage);
            newCell.CellPaint(backg);
            this.OnPaint(null);
            //this.Parent.Invalidate();
        }

        public void AddGraphics(EasyPhoto.GraphicsType type, System.Collections.ArrayList arraylist, Pen pen, Brush brush)
        {
            CustomClass.Cell newCell = new EasyPhoto.CustomClass.Cell(type, arraylist, pen, brush);
            this.arrayList.Add(newCell);
            Graphics backg = Graphics.FromImage(backImage);
            newCell.CellPaint(backg);
            this.OnPaint(null);
            //this.Parent.Invalidate();
        }

        public void AddGraphics(GraphicsType type, System.Collections.ArrayList arraylist)
        {
            CustomClass.Cell newCell = new EasyPhoto.CustomClass.Cell(type,this.baseImage,arraylist);
            this.arrayList.Add(newCell);
            Graphics backg = Graphics.FromImage(backImage);
            newCell.CellPaint(backg);
            this.OnPaint(null);
            //this.Parent.Invalidate();
        }

        public void AddGraphics(EasyPhoto.GraphicsType type, string str, Font font,Brush brush, Point p)
        {
            CustomClass.Cell newCell = new EasyPhoto.CustomClass.Cell(type, str, font, brush, p);
            arrayList.Add(newCell);
            Graphics backg = Graphics.FromImage(backImage);
            newCell.CellPaint(backg);
            this.OnPaint(null);
            //this.Parent.Invalidate();
        }

        public void AddGraphics(EasyPhoto.GraphicsType type, Image image, Point point, int width, int height)
        {
            CustomClass.Cell newCell = new EasyPhoto.CustomClass.Cell(type, image, point, width, height);
            arrayList.Add(newCell);
            Graphics backg = Graphics.FromImage(backImage);
            newCell.CellPaint(backg);
            this.OnPaint(null);
            //this.Parent.Invalidate();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (this.backImage != null)
            {
                Graphics g = Graphics.FromImage(finalImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.DrawImage(backImage, this.Bounds, this.showRectangle, GraphicsUnit.Pixel);
            }
        }

        void hScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            this.showRectangle.X = e.NewValue;
            this.OnPaint(null);
            this.Parent.Refresh();
        }

        void vScrollBar1_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            this.showRectangle.Y = e.NewValue;
            this.OnPaint(null);
            this.Parent.Refresh();
        }

        //改变放大系数
        public void SetZoom(int zoomvalue,Point startpoint)
        {
            this.SuperParent.Zoom = zoomvalue;
            this.SuperParent.TempPen.Width = this.SuperParent.CurrentPen.Width * zoomvalue;
            this.zoom = zoomvalue;
            //设置显示矩形的参数
            this.showRectangle.Width = this.Bounds.Width / zoom;
            this.showRectangle.Height = this.Bounds.Height / zoom;
            if ((this.Bounds.Width - startpoint.X) < this.Width / zoom)
                this.showRectangle.X = this.Bounds.Width - this.Bounds.Width / zoom;
            else
                this.showRectangle.X = startpoint.X;
            if ((this.Bounds.Height - startpoint.Y) < this.Bounds.Height / zoom)
                this.showRectangle.X = this.Bounds.Height - this.Bounds.Height / zoom;
            else
                this.showRectangle.Y = startpoint.Y;
            //显示滚动条的参数
            if (this.backgroundWidth * this.zoom > this.Bounds.Width)
            {
                this.hScrollBar1.Visible = true;                             //以后再补充
                this.hScrollBar1.Maximum = this.Bounds.Width;
                this.hScrollBar1.LargeChange = this.Bounds.Width / zoom;
                this.hScrollBar1.Value = this.showRectangle.X;
            }
            else
            {
                this.hScrollBar1.Visible = false;
            }
            if (this.backgroundHeight * this.zoom > this.Bounds.Height)
            {
                this.vScrollBar1.Visible = true;                               //以后再补充
                this.vScrollBar1.Maximum = this.Bounds.Height;
                this.vScrollBar1.LargeChange = this.Bounds.Height / zoom;
                this.vScrollBar1.Value = this.showRectangle.Y;
            }
            else
            {
                this.vScrollBar1.Visible = false;
            }

            Graphics g = Graphics.FromImage(this.finalImage);
            g.DrawImage(backImage, this.Bounds, this.showRectangle, GraphicsUnit.Pixel);
            this.Refresh();
        }

        //响应手型工具
        public void ScrMove(int x, int y)
        {
            if (x < 0)
            {
                this.showRectangle.X =this.showRectangle.X + x;
                if (this.showRectangle.X < 0)
                    this.showRectangle.X = 0; 
            }
            else
            {
                this.showRectangle.X += x;
                if ((this.showRectangle.X + this.showRectangle.Width) > this.Width)
                    this.showRectangle.X = this.Width - this.showRectangle.Width;
            }
            if (y < 0)
            {
                this.showRectangle.Y =this.showRectangle.Y+ y;
                if (this.showRectangle.Y < 0)
                    this.showRectangle.Y = 0;
            }
            else
            {
                this.showRectangle.Y += y;
                if ((this.showRectangle.Y + this.showRectangle.Height) > this.Height)
                    this.showRectangle.Y = this.Height - this.showRectangle.Height;
            }
            this.hScrollBar1.Value = this.showRectangle.X;
            this.vScrollBar1.Value = this.showRectangle.Y;
            this.OnPaint(null);
            this.Parent.Refresh();
        }

        //响应移动工具
        public bool IsSelectCell(Point selectpoint)
        {
            bool flag = false;
            for (int i = 0; i < this.arrayList.Count; i++)
            {
                if (((EasyPhoto.CustomClass.Cell)arrayList[i]).IsSelect(selectpoint))
                {
                    this.activeNum = i;
                    this.tempCell = (EasyPhoto.CustomClass.Cell)this.arrayList[i];
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        //将原件描粗
        public void IsSelectPaint(bool flag)
        {
            if ((flag)&&(this.activeNum!=-1))
            {
                ((EasyPhoto.CustomClass.Cell)arrayList[this.activeNum]).SelectPaint(this.CreateGraphics());
            }
            else
            {
                this.activeNum = -1;
                this.OnPaint(null);
                this.Parent.Refresh();
            }
        }

        //当元件移动的时候
        public void MoveSelectCell(int x, int y)
        {
            if (this.tempCell != null)
            {
                if (this.tempCell.CType == GraphicsType.Line)
                {
                    //int x1 = tempCell.StartPoint.X < tempCell.CWidth ? tempCell.StartPoint.X : (int)tempCell.CWidth;
                    //int x2 = tempCell.StartPoint.X >= tempCell.CWidth ? tempCell.StartPoint.X : (int)tempCell.CWidth;
                    //int y1 = tempCell.StartPoint.Y < tempCell.CHeight ? tempCell.StartPoint.Y : (int)tempCell.CHeight;
                    //int y2 = tempCell.StartPoint.Y >= tempCell.CHeight ? tempCell.StartPoint.Y : (int)tempCell.CHeight;
                    int x1 = tempCell.StartPoint.X;
                    int y1 = tempCell.StartPoint.Y;
                    int x2 = (int)tempCell.CWidth;
                    int y2 = (int)tempCell.CHeight;
                    Point newpoint = new Point(x1 + x, y1 + y);
                    Point endpoint = new Point(x2 + x, y2 + y);
                    if ((x1 + x <= 0)||(x2 + x >= this.Bounds.Width)||(y1 + y <= 0)||(y2 + y > this.Bounds.Height))
                    {
                        newpoint.X = x1;
                        newpoint.Y = y1;
                        endpoint.X = x2;
                        endpoint.Y = y2;
                    }
                    this.tempCell.StartPoint = newpoint;
                    this.tempCell.CWidth = endpoint.X;
                    this.tempCell.CHeight = endpoint.Y;
                    
                }
                else
                {
                    Point newpoint = new Point(tempCell.StartPoint.X + x, tempCell.StartPoint.Y + y);
                    if (newpoint.X <= 0)
                        newpoint.X = 0;
                    if (newpoint.X >= this.Bounds.Width - this.tempCell.CWidth)
                        newpoint.X = this.Bounds.Width - (int)this.tempCell.CWidth;
                    if (newpoint.Y <= 0)
                        newpoint.Y = 0;
                    if (newpoint.Y >= this.Bounds.Height - this.tempCell.CHeight)
                        newpoint.Y = this.Bounds.Height - (int)this.tempCell.CHeight;
                    this.tempCell.StartPoint = newpoint;
                }
                this.Parent.Refresh();
                this.tempCell.SelectPaint(this.CreateGraphics());
            }
        }

        /// <summary>
        /// 移除元件
        /// </summary>
        /// <param name="flag">是否将移除元件保存到临时元件中</param>
        public void RemoveCell()
        {
            if (this.activeNum == -1)
                return;
            this.arrayList.RemoveAt(this.activeNum);
            this.RePaint();
            //this.Parent.Invalidate();
        }

        /// <summary>
        /// 将临时元件保存到队列中
        /// </summary>
        public void AddCell()
        {
            this.arrayList.Insert(this.activeNum, this.tempCell);
            this.RePaint();
        }

        /// <summary>
        /// 重画
        /// </summary>
        public void RePaint()
        {
            this.backImage = (Bitmap)this.baseImage.Clone();
            Graphics backg = Graphics.FromImage(backImage);

            if (this.GetType().ToString() == "EasyPhoto.EPControl.Layer")
            {
                ((EasyPhoto.CustomClass.Cell)this.arrayList[0]).CImage = this.SuperParent.CurrentStage.ExportBackImage();
            }

            if (this.arrayList.Count > 0)
            {
                for (int i = 0; i < this.arrayList.Count; i++)
                {
                    ((EasyPhoto.CustomClass.Cell)this.arrayList[i]).CellPaint(backg);
                }
            }

            if (this.GetType().ToString() == "EasyPhoto.EPControl.Stage")
            {
                for (int i = 0; i < this.SuperParent.LayerArrayList.Count; i++)
                {
                    if (((EasyPhoto.EPControl.Layer)this.SuperParent.LayerArrayList[i]).CanSee)
                    {
                        for (int j = 1; j < ((EasyPhoto.EPControl.Layer)this.SuperParent.LayerArrayList[i]).arrayList.Count; j++)
                        {
                            ((EasyPhoto.CustomClass.Cell)((EasyPhoto.EPControl.Layer)this.SuperParent.LayerArrayList[i]).arrayList[j]).CellPaint(backg);
                        }
                    }
                }
            }

            this.OnPaint(null);
        }

        /// <summary>
        /// 导出基层规定大小的图像
        /// </summary>
        /// <returns></returns>
        public Image ExportCustomImage()
        {
            Image exportImage = new Bitmap(this.backgroundWidth, this.backgroundHeight);
            Image tempImage = (Bitmap)this.baseImage.Clone();
            Graphics tempg = Graphics.FromImage(tempImage);
            if (this.arrayList.Count > 0)
            {
                for (int i = 0; i < this.arrayList.Count; i++)
                {
                    ((EasyPhoto.CustomClass.Cell)this.arrayList[i]).CellPaint(tempg);
                }
            }

            if (this.GetType().ToString() == "EasyPhoto.EPControl.Stage")
            {
                for (int i = 0; i < this.SuperParent.LayerArrayList.Count; i++)
                {
                    for (int j = 1; j < ((EasyPhoto.EPControl.Layer)this.SuperParent.LayerArrayList[i]).arrayList.Count; j++)
                    {
                        ((EasyPhoto.CustomClass.Cell)((EasyPhoto.EPControl.Layer)this.SuperParent.LayerArrayList[i]).arrayList[j]).CellPaint(tempg);
                    }
                }
            }
            Graphics g = Graphics.FromImage(exportImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.DrawImage(tempImage, new Rectangle(0, 0, this.backgroundWidth, this.backgroundHeight), new Rectangle(this.startPoint, exportImage.Size), GraphicsUnit.Pixel);
            tempImage.Dispose();
            tempg.Dispose();
            g.Dispose();
            return exportImage;
        }

        public Image ExportBackImage()
        {
            Image tempImage = (Image)this.baseImage.Clone();
            Graphics g = Graphics.FromImage(tempImage);
            for (int i = 0; i < this.arrayList.Count; i++)
            {
                ((EasyPhoto.CustomClass.Cell)this.arrayList[i]).CellPaint(g);
            }
            return tempImage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EasyPhoto.CustomClass
{
    [Serializable]
    public class Cell
    {
        private Point startPoint;
        private float cWidth;
        private float cHeight;
        [NonSerialized]
        private Pen cPen;
        [NonSerialized]
        private Brush cBrush;
        private EasyPhoto.GraphicsType cType;
        private string cString;
        private Font cFont;
        private Image cImage;
        private float[][] cDiaph = new float[][] { new float[] { 1, 0, 0, 0, 0 }, new float[] { 0, 1, 0, 0, 0 }, new float[] { 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, 1, 0 }, new float[] { 0, 0, 0, 0, 1 } };
        private bool canDelete = true;
        private System.Collections.ArrayList cArrayList;
        private Bitmap cBackImage = null;
        
        //记录画刷和画笔参数 用于序列化
        public Color cPenColor;
        public Color cBrushColor;
        public float cPenWidth = 0;
        public bool ExistPen = true;
        public bool ExistBrush = true;
        

        public Bitmap CBackImage
        {
            get { return this.cBackImage; }
        }

        public System.Collections.ArrayList CArrayList
        {
            get 
            {
                if (this.cArrayList!= null)
                    return (System.Collections.ArrayList)this.cArrayList.Clone();
                else
                    return null;
            }
        }
        public Image CImage
        {
            set { this.cImage = value; }
            get
            {
                if (this.cImage != null)
                    return (Bitmap)this.cImage.Clone();
                else
                    return null;
            }
        }
        public bool CanDelete
        {
            set { this.canDelete = value; }
            get { return this.canDelete; }
        }
        public float CDiaph
        {
            set { this.cDiaph[3][3] = value; }
            get { return cDiaph[3][3]; }
        }
        /// <summary>
        /// 获取或设置字符样式
        /// </summary>
        public Font CFont
        {
            set { this.cFont = value; }
            get
            {
                if (this.cFont != null)
                    return (Font)this.cFont.Clone();
                else
                    return null;
            }
        }
        /// <summary>
        /// 获取或设置字符串
        /// </summary>
        public string CString
        {
            set { this.cString = value; }
            get { return cString; }
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        public EasyPhoto.GraphicsType CType
        {
            get { return cType; }
        }
        /// <summary>
        /// 获取或设置宽度
        /// </summary>
        public float CWidth
        {
            set 
            { 
                cWidth = value;
                if (this.cType == GraphicsType.Image)
                    this.RedrawImage();
            }
            get { return cWidth; }
        }
        /// <summary>
        /// 获取或设置长度
        /// </summary>
        public float CHeight
        {
            set
            { 
                cHeight = value;
                if (this.cType == GraphicsType.Image)
                    this.RedrawImage();
            }
            get { return cHeight; }
        }
        /// <summary>
        /// 获取或设置画笔
        /// </summary>
        public Pen CPen
        {
            set { cPen = value; }
            get
            {
                if (this.cPen != null)
                    return (Pen)cPen.Clone();
                else
                    return null;
            }
        }
        /// <summary>
        /// 设置或获取画刷
        /// </summary>
        public Brush CBrush
        {
            set { cBrush = value; }
            get
            {
                if (this.cBrush != null)
                    return (Brush)cBrush.Clone();
                else
                    return null;
            }
        }
        /// <summary>
        /// 获取设置开始位置
        /// </summary>
        public Point StartPoint
        {
            set
            {
                startPoint = value;
            }
            get { return startPoint; }
        }

        # region 根据元件不同执行不同的构造函数
        public Cell(Cell temp)
        {
            this.startPoint=temp.StartPoint;
            this.cWidth = temp.CWidth;
            this.cHeight = temp.CHeight;
            this.cPen = temp.CPen;
            this.cBrush = temp.CBrush;
            this.cType = temp.CType;
            this.cString = temp.CString;
            this.cFont = temp.CFont;
            this.cImage = temp.CImage;
            this.canDelete = temp.CanDelete;
            this.cDiaph[3][3] = temp.CDiaph;
            
            this.cBackImage = temp.cBackImage;
            if (temp.CArrayList != null)
            {
                this.cArrayList = new System.Collections.ArrayList();
                for (int i = 0; i < temp.CArrayList.Count; i++)
                {
                    this.cArrayList.Add(temp.CArrayList[i]);
                }
            }

        }

        public Cell(EasyPhoto.GraphicsType type, Point startPoint, int width, int height, Pen pen)        //如是是直线时，width和height分别是结束点的坐标
        {
            cType = type;
            this.startPoint = startPoint;
            this.cWidth = width;
            this.cHeight = height;
            this.cPen = (Pen)pen.Clone();
        }

        public Cell(EasyPhoto.GraphicsType type, Point startPoint, int width, int height, Brush brush)
        {
            cType = type;
            this.startPoint = startPoint;
            this.cWidth = width;
            this.cHeight = height;
            this.cBrush = (Brush)brush.Clone();
        }

        public Cell(EasyPhoto.GraphicsType type, System.Collections.ArrayList arraylist,Pen pen,Brush brush)
        {
            cType = type;
            this.cPen = (Pen)pen.Clone();
            this.cBrush = (Brush)brush.Clone();
            this.cArrayList = new System.Collections.ArrayList(arraylist);
        }

        public Cell(EasyPhoto.GraphicsType type,Bitmap image, System.Collections.ArrayList arraylist)
        {
            this.cType = type;
            this.cBackImage = image;
            this.cArrayList = new System.Collections.ArrayList(arraylist);
        }

        public Cell(EasyPhoto.GraphicsType type, string str,Font font, Brush brush,Point p)
        {
            cType = type;
            this.cBrush = (Brush)brush.Clone();
            this.cFont = (Font)font.Clone();
            this.cString = (string)str.Clone();
            this.startPoint = p;
        }

        public Cell(EasyPhoto.GraphicsType type, Image image, Point point, int width, int height)
        {
            this.cType = type;
            this.startPoint = point;
            this.cWidth = width;
            this.cHeight = height;
            this.cImage = (Bitmap)image.Clone();

            this.cBackImage = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(this.cBackImage);
            g.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
        }
        #endregion

        /// <summary>
        /// 将文件绘画出来
        /// </summary>
        /// <param name="g">绘画的地方</param>
        public void CellPaint(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                switch (cType)
                {
                    case GraphicsType.Rectangle:
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.DrawRectangle(cPen, new Rectangle(startPoint, new Size((int)this.cWidth, (int)this.cHeight)));
                        break;
                    case GraphicsType.Ellipse:
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.DrawEllipse(cPen, new Rectangle(startPoint, new Size((int)this.cWidth, (int)this.cHeight)));
                        break;
                    case GraphicsType.RectangleSelect:
                        g.FillRectangle(this.cBrush, new Rectangle(startPoint, new Size((int)this.cWidth, (int)this.cHeight)));
                        break;
                    case GraphicsType.EllipseSelect:
                        g.FillEllipse(this.cBrush, new Rectangle(startPoint, new Size((int)this.cWidth, (int)this.cHeight)));
                        break;
                    case GraphicsType.Line:
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.DrawLine(this.cPen, this.startPoint, new Point((int)this.cWidth, (int)this.cHeight));
                        break;
                    case GraphicsType.Lines:
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.DrawLines(this.cPen, (Point[])this.cArrayList.ToArray(typeof(Point)));
                        break;
                    case GraphicsType.Pencil:
                        g.DrawLines(this.cPen, (Point[])this.cArrayList.ToArray(typeof(Point)));
                        break;
                    case GraphicsType.Brush:
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                        for (int i = 0; i < cArrayList.Count; i++)
                        {
                            g.FillEllipse(this.cBrush, (Rectangle)cArrayList[i]);
                        }
                        break;
                    case GraphicsType.Text:
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        g.DrawString(this.cString, this.cFont, this.cBrush, this.startPoint);
                        this.cWidth = (g.MeasureString(this.cString, this.cFont)).Width;
                        this.cHeight = (g.MeasureString(this.cString, this.cFont)).Height;
                        break;
                    case GraphicsType.Image:
                        System.Drawing.Imaging.ColorMatrix colormatrix = new System.Drawing.Imaging.ColorMatrix(this.cDiaph);
                        System.Drawing.Imaging.ImageAttributes imageattributes = new System.Drawing.Imaging.ImageAttributes();
                        imageattributes.SetColorMatrix(colormatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        g.DrawImage(this.cBackImage, new Rectangle(this.startPoint, new Size((int)this.cWidth, (int)this.cHeight)), 0, 0, (int)this.cWidth, (int)this.cHeight, GraphicsUnit.Pixel, imageattributes);
                        break;
                    case GraphicsType.Eraser:
                        for (int i = 0; i < cArrayList.Count; i++)
                        {
                            g.DrawImage(this.cBackImage, (Rectangle)this.cArrayList[i], (Rectangle)this.cArrayList[i], GraphicsUnit.Pixel);
                        }
                        break;
                }
            }
            catch (InvalidOperationException e1)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 被选择的时候绘画
        /// </summary>
        /// <param name="g">绘画的地方</param>
        public void SelectPaint(Graphics g)
        {
            try
            {
                switch (cType)
                {
                    case GraphicsType.Rectangle:
                        this.cPen.Width += 2;
                        g.DrawRectangle(cPen, new Rectangle(startPoint, new Size((int)this.cWidth, (int)this.cHeight)));
                        this.cPen.Width -= 2;
                        break;
                    case GraphicsType.Ellipse:
                        this.cPen.Width += 2;
                        g.DrawEllipse(cPen, new Rectangle(startPoint, new Size((int)this.cWidth, (int)this.cHeight)));
                        this.cPen.Width -= 2;
                        break;
                    case GraphicsType.RectangleSelect:
                        g.FillRectangle(this.cBrush, new Rectangle(startPoint, new Size((int)this.cWidth, (int)this.cHeight)));
                        break;
                    case GraphicsType.EllipseSelect:
                        g.FillEllipse(this.cBrush, new Rectangle(startPoint, new Size((int)this.cWidth, (int)this.cHeight)));
                        break;
                    case GraphicsType.Line:
                        this.cPen.Width += 2;
                        g.DrawLine(this.cPen, this.startPoint, new Point((int)this.cWidth, (int)this.cHeight));
                        this.cPen.Width -= 2;
                        break;
                    case GraphicsType.Text:
                        g.DrawString(this.cString, this.cFont, this.cBrush, this.startPoint);
                        this.cWidth = (g.MeasureString(this.cString, this.cFont)).Width;
                        this.cHeight = (g.MeasureString(this.cString, this.cFont)).Height;
                        break;
                    case GraphicsType.Image:
                        System.Drawing.Imaging.ColorMatrix colormatrix = new System.Drawing.Imaging.ColorMatrix(this.cDiaph);
                        System.Drawing.Imaging.ImageAttributes imageattributes = new System.Drawing.Imaging.ImageAttributes();
                        imageattributes.SetColorMatrix(colormatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                        g.DrawImage(this.cBackImage, new Rectangle(this.startPoint, new Size((int)this.cWidth, (int)this.cHeight)), 0, 0, (int)this.cWidth, (int)this.cHeight, GraphicsUnit.Pixel, imageattributes);
                        g.DrawRectangle(new Pen(new SolidBrush(Color.Black), 2), new Rectangle(this.startPoint, new Size((int)this.cWidth, (int)this.cHeight)));
                        break;
                }
            }
            catch (InvalidOperationException e1)
            {
                return;
            }
        }

        /// <summary>
        /// 是否选中元件
        /// </summary>
        /// <param name="ClickPoint">判断坐标</param>
        /// <returns></returns>
        public bool IsSelect(Point ClickPoint)
        {
            if (!this.canDelete)
            {
                return false;
            }
            bool flag = false;
            switch (cType)
            {
                case GraphicsType.Rectangle:
                    {
                        for (int i = startPoint.X; i < startPoint.X + this.cWidth; i++)
                        {
                            if (Math.Abs(ClickPoint.X - i) < this.cPen.Width + 2)
                            {
                                if ((Math.Abs(ClickPoint.Y - this.startPoint.Y) < this.cPen.Width + 2) || (Math.Abs(ClickPoint.Y - (this.startPoint.Y + this.cHeight)) < this.cPen.Width + 2))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        if (!flag)
                        {
                            for (int j = startPoint.Y; j < this.startPoint.Y+this.cHeight; j++)
                            {
                                if (Math.Abs(ClickPoint.Y - j) < this.cPen.Width + 2)
                                {
                                    if ((Math.Abs(ClickPoint.X - this.startPoint.X) < this.cPen.Width + 2) || (Math.Abs(ClickPoint.X - (this.startPoint.X + this.cWidth)) < this.cPen.Width + 2))
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case GraphicsType.RectangleSelect:
                    {
                        if ((ClickPoint.X >= this.startPoint.X) && (ClickPoint.X <= this.startPoint.X + this.cWidth))
                        {
                            if ((ClickPoint.Y >= this.startPoint.Y) && (ClickPoint.Y <= this.startPoint.Y + this.cHeight))
                                flag = true;
                        }
                        break;
                    }
                case GraphicsType.Ellipse:
                    {
                        bool temp1 = false;
                        bool temp2 = false;
                        float x = this.startPoint.X + this.cWidth / 2;
                        float y = this.startPoint.Y + this.cHeight / 2;
                        float result = ((ClickPoint.X - x) * (ClickPoint.X - x)) / ((this.cWidth / 2 - this.cPen.Width / 2 - 5) * (this.cWidth / 2 - this.cPen.Width / 2 - 5)) + ((ClickPoint.Y - y) * (ClickPoint.Y - y)) / ((this.cHeight / 2 - this.cPen.Width / 2 - 5) * (this.cHeight / 2 - this.cPen.Width / 2 - 5));
                        if (result > 1)
                            temp1 = true;
                        result = ((ClickPoint.X - x) * (ClickPoint.X - x)) / ((this.cWidth / 2 + this.cPen.Width / 2 + 5) * (this.cWidth / 2 + this.cPen.Width / 2 + 5)) + ((ClickPoint.Y - y) * (ClickPoint.Y - y)) / ((this.cHeight / 2 + this.cPen.Width / 2 + 5) * (this.cHeight / 2 + this.cPen.Width / 2 + 5));
                        if (result < 1)
                            temp2 = true;
                        if ((temp1 == true) && (temp2 == true))
                            flag = true;
                        break;
                    }
                case GraphicsType.EllipseSelect:
                    {
                        float x = this.startPoint.X + this.cWidth / 2;
                        float y = this.startPoint.Y + this.cHeight / 2;
                        float result = ((ClickPoint.X - x) * (ClickPoint.X - x)) / ((this.cWidth / 2) * (this.cWidth / 2)) + ((ClickPoint.Y - y) * (ClickPoint.Y - y)) / ((this.cHeight / 2) * (this.cHeight / 2));
                        if (result < 1)
                            flag = true;
                        break;
                    }
                case GraphicsType.Image:
                    if ((ClickPoint.X >= this.startPoint.X) && (ClickPoint.X <= this.startPoint.X + this.cWidth))
                    {
                        if ((ClickPoint.Y >= this.startPoint.Y) && (ClickPoint.Y <= this.startPoint.Y + this.cHeight))
                            flag = true;
                    }
                    break;
                case GraphicsType.Line:
                    {
                        float k = (startPoint.Y - this.cHeight) / (this.startPoint.X - this.cWidth);
                        for (int i = (this.startPoint.X > (int)this.cWidth) ? (int)this.cWidth : this.startPoint.X; i < ((this.startPoint.X > (int)this.cWidth) ? this.startPoint.X : (int)this.cWidth); i++)
                        {
                            if (Math.Abs(i - ClickPoint.X) < this.cPen.Width + 2)
                            {
                                float j = k * (i - startPoint.X) + startPoint.Y;
                                if (Math.Abs(j - ClickPoint.Y) < this.cPen.Width + 2)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                case GraphicsType.Text:
                    if ((ClickPoint.X >= this.startPoint.X) && (ClickPoint.X <= this.startPoint.X + this.cWidth))
                    {
                        if ((ClickPoint.Y >= this.startPoint.Y) && (ClickPoint.Y <= this.startPoint.Y + this.cHeight))
                            flag = true;
                    }
                    break;
            }
            return flag;
        }

        public Cell CopyCell()
        {
            Cell temp = new Cell(this);
            return temp;
        }

        public void Serial()
        {
            if (this.cPen != null)
            {
                this.cPenWidth = this.cPen.Width;
                this.cPenColor = this.cPen.Color;
                this.ExistPen = true;
            }
            else
                this.ExistPen = false;
            if (this.cBrush != null)
            {
                this.cBrushColor = ((SolidBrush)this.cBrush).Color;
                this.ExistBrush = true;
            }
            else
                this.ExistBrush = false;
        }

        public void Deserial()
        {
            if (this.ExistPen)
            {
                this.cPen = new Pen(this.cPenColor, this.cPenWidth);
            }
            else
                this.cPen = null;
            if (this.ExistBrush)
            {
                this.cBrush = new SolidBrush(this.cBrushColor);
            }
            else
                this.cBrush = null;
        }

        public void RedrawImage()
        {
            try
            {
                this.cBackImage.Dispose();
                this.cBackImage = new Bitmap((int)this.cWidth, (int)this.cHeight);
                Graphics g = Graphics.FromImage(this.cBackImage);
                g.DrawImage(this.cImage, new Rectangle(0, 0, (int)this.cWidth,(int)this.cHeight), new Rectangle(0, 0, this.cImage.Width,this.cImage.Height), GraphicsUnit.Pixel);
            }
            catch (InvalidOperationException e1)
            {
                RedrawImage();
            }
        }
    }
}

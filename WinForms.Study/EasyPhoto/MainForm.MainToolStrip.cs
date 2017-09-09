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
        #region 工具按钮的响应函数

        private void MoveToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Move)
                return;
            toolSelected = ToolSelected.Move;
            this.MoveToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.MoveToolStrip)
                {
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
                }
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void RectangleSelectToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.RectangleSelect)
                return;
            toolSelected = ToolSelected.RectangleSelect;
            this.RectangleSelectToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.RectangleSelectToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void EllipseSelectToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.EllipseSelect)
                return;
            toolSelected = ToolSelected.EllipseSelect;
            this.EllipseSelectToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.EllipseSelectToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void LineToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Line)
                return;
            toolSelected = ToolSelected.Line;
            this.LineToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.LineToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void LinesToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Lines)
                return;
            toolSelected = ToolSelected.Lines;
            this.LinesToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.LinesToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void RectangleToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Rectangle)
                return;
            toolSelected = ToolSelected.Rectangle;
            this.RectangleToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.RectangleToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void TextToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Text)
                return;
            toolSelected = ToolSelected.Text;
            this.TextToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.TextToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void PencilToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Pencil)
                return;
            toolSelected = ToolSelected.Pencil;
            this.PencilToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.PencilToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void BrushToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Brush)
                return;
            toolSelected = ToolSelected.Brush;
            this.BrushToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.BrushToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void eraserToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Eraser)
                return;
            toolSelected = ToolSelected.Eraser;
            this.eraserToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.eraserToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void dyestuffToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Dyestuff)
                return;
            toolSelected = ToolSelected.Move;
            this.dyestuffToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.dyestuffToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
        }

        private void suckerToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Sucker)
                return;
            toolSelected = ToolSelected.Sucker;
            this.suckerToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.suckerToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void handToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Hand)
                return;
            toolSelected = ToolSelected.Hand;
            this.handToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.handToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void zoomToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Zoom)
                return;
            toolSelected = ToolSelected.Zoom;
            this.zoomToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.zoomToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void magnifierToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Magnifier)
                return;
            toolSelected = ToolSelected.Magnifier;
            this.magnifierToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.magnifierToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }

        private void EllipseToolStrip_Click(object sender, EventArgs e)
        {
            if (toolSelected == ToolSelected.Ellipse)
                return;
            toolSelected = ToolSelected.Ellipse;
            this.EllipseToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            foreach (System.Windows.Forms.ToolStripButton toolstripbutton in this.mainToolStrip.Items)
            {
                if (toolstripbutton != this.EllipseToolStrip)
                    toolstripbutton.CheckState = System.Windows.Forms.CheckState.Unchecked;
            }
            this.initializeToolSelected();
            this.showOpiton();
        }
        #endregion

        //各工具具体的实现方法
        #region 移动工具的鼠标响应函数
        void MoveTool_OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (this.IsMouseDown)
            {
                this.toolSelected = ToolSelected.MoveNone;
                this.showOpiton();
                return;
            }
            if (this.currentPaper.IsSelectCell(e.Location))
            {
                this.temprecord = this.currentPaper.ActiveNum;
                this.currentPaper.IsSelectPaint(true);
                this.IsMouseClick = true;
                if (this.currentPaper.tempCell.CType == GraphicsType.Ellipse)
                    this.toolSelected = ToolSelected.MoveEllipse;
                else if (this.currentPaper.tempCell.CType == GraphicsType.EllipseSelect)
                    this.toolSelected = ToolSelected.MoveEllipseSelect;
                else if (this.currentPaper.tempCell.CType == GraphicsType.Rectangle)
                    this.toolSelected = ToolSelected.MoveRectangle;
                else if (this.currentPaper.tempCell.CType == GraphicsType.RectangleSelect)
                    this.toolSelected = ToolSelected.MoveRectangleSelect;
                else if (this.currentPaper.tempCell.CType == GraphicsType.Line)
                    this.toolSelected = ToolSelected.MoveLine;
                else if (this.currentPaper.tempCell.CType == GraphicsType.Text)
                    this.toolSelected = ToolSelected.MoveText;
                else if (this.currentPaper.tempCell.CType == GraphicsType.Image)
                    this.toolSelected = ToolSelected.MoveImage;
                else
                    this.toolSelected = ToolSelected.MoveNone;
            }
            else if(this.currentPaper.GetType().ToString()=="EasyPhoto.EPControl.Layer")
            {
                this.toolSelected = ToolSelected.MoveImage;
            }
            else
            {
                this.IsMouseClick = false;
                this.currentPaper.IsSelectPaint(false);
                this.toolSelected = ToolSelected.MoveNone;
            }
            this.IsMouseDown = false;
            this.showOpiton();
        }

        public void MoveTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (this.IsMouseClick)
            {
                if (this.currentPaper.IsSelectCell(e.Location))
                {
                    if (this.currentPaper.ActiveNum != this.temprecord)
                    {
                        this.IsMouseDown = false;
                        this.currentPaper.IsSelectPaint(false);
                        this.IsMouseClick = false;
                        return;
                    }
                    else
                    {
                        this.currentPaper.RemoveCell();
                        this.IsMouseDown = true;
                        this.currentMouseDownPosition = e.Location;
                    }
                }
            }
        }

        public void MoveTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                this.currentPaper.MoveSelectCell(e.Location.X - this.currentMouseDownPosition.X, e.Location.Y - this.currentMouseDownPosition.Y);
                this.currentMouseDownPosition = e.Location;
            }
        }

        public void MoveTool_OnMouseUp(object sender, EventArgs e)
        {
            if (IsMouseDown)
            {
                this.currentPaper.AddCell();
                this.currentPaper.IsSelectPaint(false);
                this.IsMouseDown = false;
                this.IsMouseClick = false;
                this.Refresh();
            }
        }
        #endregion

        #region 缩放工具的鼠标响应函数
        public void ZoomTool_OnMouseDown(object sender, MouseEventArgs e)
        { }

        public void ZoomTool_OnMouseMove(object sender, EventArgs e)
        { }

        public void ZoomTool_OnMouseUp(object sender, EventArgs e)
        { }
        #endregion

        #region 矩形内部填充工具的鼠标响应函数
        public void RectangleSelectTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
            {
                if (e.Button != MouseButtons.Left)
                {
                    this.IsMouseDown = false;
                    return;
                }
                else
                {
                    this.IsMouseDown = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                }
            }
            else
            {
                this.currentgraphics.FillRectangle(this.CurrentBrush, this.CurrentRectangle);
            }
        }

        public void RectangleSelectTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
                return;
            else if ((e.X == this.currentMouseDownPosition.X) || (e.Y == this.currentMouseDownPosition.Y))
                return;
            else
            {
                Rectangle temprectangle;
                if ((e.X > this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(this.currentMouseDownPosition, new Size(e.X - this.currentMouseDownPosition.X, e.Y - this.currentMouseDownPosition.Y));
                else if ((e.X > this.currentMouseDownPosition.X) && (e.Y < currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(this.currentMouseDownPosition.X, e.Y, e.X - this.currentMouseDownPosition.X, this.currentMouseDownPosition.Y - e.Y);
                else if ((e.X < this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(e.X, this.currentMouseDownPosition.Y, this.currentMouseDownPosition.X - e.X, e.Y - this.currentMouseDownPosition.Y);
                else
                    temprectangle = new Rectangle(e.X, e.Y, this.currentMouseDownPosition.X - e.X, this.currentMouseDownPosition.Y - e.Y);

                this.currentPaper.Refresh();
                this.CurrentRectangle = temprectangle;
                this.currentgraphics.FillRectangle(this.CurrentBrush, this.CurrentRectangle);
            }
        }

        public void RectangleSelectTool_OnMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
            this.currentPaper.AddGraphics(GraphicsType.RectangleSelect, new Point(this.CurrentRectangle.X/this.Zoom+this.currentPaper.ShowRectanglePoint.X, this.CurrentRectangle.Y/this.Zoom+this.currentPaper.ShowRectanglePoint.Y), this.CurrentRectangle.Width/this.Zoom, this.CurrentRectangle.Height/this.Zoom, this.CurrentBrush);
            this.Refresh();
        }
        #endregion

        #region 椭圆内部填充工具的鼠标响应函数
        public void EllipseSelectTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
            {
                if (e.Button != MouseButtons.Left)
                {
                    this.IsMouseDown = false;
                    return;
                }
                else
                {
                    this.IsMouseDown = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                }
            }
            else
            {
                this.currentgraphics.FillEllipse(this.CurrentBrush, this.CurrentRectangle);
            }
        }

        public void EllipseSelectTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
                return;
            else if ((e.X == this.currentMouseDownPosition.X) || (e.Y == this.currentMouseDownPosition.Y))
                return;
            else
            {
                Rectangle temprectangle;
                if ((e.X > this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(this.currentMouseDownPosition, new Size(e.X - this.currentMouseDownPosition.X, e.Y - this.currentMouseDownPosition.Y));
                else if ((e.X > this.currentMouseDownPosition.X) && (e.Y < currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(this.currentMouseDownPosition.X, e.Y, e.X - this.currentMouseDownPosition.X, this.currentMouseDownPosition.Y - e.Y);
                else if ((e.X < this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(e.X, this.currentMouseDownPosition.Y, this.currentMouseDownPosition.X - e.X, e.Y - this.currentMouseDownPosition.Y);
                else
                    temprectangle = new Rectangle(e.X, e.Y, this.currentMouseDownPosition.X - e.X, this.currentMouseDownPosition.Y - e.Y);

                this.currentPaper.Refresh();
                this.CurrentRectangle = temprectangle;
                this.currentgraphics.FillEllipse(this.CurrentBrush, this.CurrentRectangle);
            }
        }

        public void EllipseSelectTool_OnMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
            this.currentPaper.AddGraphics(GraphicsType.EllipseSelect, new Point(this.CurrentRectangle.X/this.Zoom+this.currentPaper.ShowRectanglePoint.X, this.CurrentRectangle.Y/this.Zoom+this.currentPaper.ShowRectanglePoint.Y), this.CurrentRectangle.Width/this.Zoom, this.CurrentRectangle.Height/this.Zoom, this.CurrentBrush);
            this.Refresh();
        }
        #endregion

        #region 直线工具的鼠标响应函数
        public void LineTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
            {
                if (e.Button != MouseButtons.Left)
                {
                    this.IsMouseDown = false;
                    return;
                }
                else
                {
                    this.IsMouseDown = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                }
            }
            else
            {
                this.currentgraphics.DrawLine(TempPen, this.currentMouseDownPosition, e.Location);
            }
            this.TempPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            
        }

        public void LineTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
                return;
            else if ((e.X == this.currentMouseDownPosition.X) || (e.Y == this.currentMouseDownPosition.Y))
                return;
            else
            {
                this.currentPaper.Refresh();
                this.currentgraphics.DrawLine(TempPen, this.currentMouseDownPosition, e.Location);
            }
        }

        public void LineTool_OnMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
            this.currentPaper.AddGraphics(GraphicsType.Line, new Point(this.currentMouseDownPosition.X/this.Zoom+this.currentPaper.ShowRectanglePoint.X,this.currentMouseDownPosition.Y/this.Zoom+this.currentPaper.ShowRectanglePoint.Y), e.X/this.Zoom+this.currentPaper.ShowRectanglePoint.X, e.Y/this.Zoom+this.currentPaper.ShowRectanglePoint.Y, this.CurrentPen);
            this.Refresh();
            this.TempPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
        }
        #endregion

        #region 多直线工具的鼠标响应函数
        public void LinesTool_OnMouseClick(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseClick)
            {
                if (e.Button != MouseButtons.Left)
                {
                    this.IsMouseClick = false;
                    return;
                }
                else
                {
                    this.TempPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    this.TempPen.Color = Color.Red;
                    this.LinesToolStrip.CheckStateChanged += new EventHandler(LinesToolStrip_CheckStateChanged);
                    this.tempArrayList.Clear();
                    this.finalArrayList.Clear();
                    this.IsMouseClick = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                    tempArrayList.Add(new Point(e.X, e.Y));
                    finalArrayList.Add(new Point(e.X / this.Zoom + this.currentPaper.ShowRectanglePoint.X, e.Y / this.Zoom + this.currentPaper.ShowRectanglePoint.Y));
                }
            }
            else
            {
                tempArrayList.Add(e.Location);
                finalArrayList.Add(new Point(e.X / this.Zoom + this.currentPaper.ShowRectanglePoint.X, e.Y / this.Zoom + this.currentPaper.ShowRectanglePoint.Y));
            }
            
        }

        public void LinesTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsMouseClick)
            {
                this.currentPaper.Refresh();
                if (tempArrayList.Count > 1)
                { 
                    this.currentgraphics.DrawLines(this.TempPen, (Point[])tempArrayList.ToArray(typeof(Point)));
                }
                this.currentgraphics.DrawLine(this.TempPen, (Point)tempArrayList[tempArrayList.Count - 1], e.Location);
            }
        }

        public void LinesTool_OnDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.IsMouseClick)
            {
                this.IsMouseClick = false;
                this.currentPaper.AddGraphics(GraphicsType.Lines, this.finalArrayList, this.CurrentPen, this.CurrentBrush);
                this.Refresh();
                this.tempArrayList.Clear();
                this.finalArrayList.Clear();
                this.TempPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            }
        }

        void LinesToolStrip_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.IsMouseClick)
            {
                this.IsMouseClick = false;
                this.currentPaper.AddGraphics(GraphicsType.Lines, this.finalArrayList, this.CurrentPen, this.CurrentBrush);
                this.LinesToolStrip.CheckStateChanged -= new EventHandler(LinesToolStrip_CheckStateChanged);
                this.Refresh();
                this.tempArrayList.Clear();
                this.finalArrayList.Clear();
            }
        }

        #endregion

        #region 矩形工具的鼠标响应函数
        public void RectangleTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
            {
                if (e.Button != MouseButtons.Left)
                {
                    this.IsMouseDown = false;
                    return;
                }
                else
                {
                    this.IsMouseDown = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                }
            }
            else
            {
                this.currentgraphics.DrawRectangle(TempPen, this.CurrentRectangle);
            }
        }

        public void RectangleTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
                return;
            else if ((e.X == this.currentMouseDownPosition.X) || (e.Y == this.currentMouseDownPosition.Y))
                return;
            else
            {
                Rectangle temprectangle;
                if ((e.X > this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(this.currentMouseDownPosition, new Size(e.X - this.currentMouseDownPosition.X, e.Y - this.currentMouseDownPosition.Y));
                else if ((e.X > this.currentMouseDownPosition.X) && (e.Y < currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(this.currentMouseDownPosition.X, e.Y, e.X - this.currentMouseDownPosition.X, this.currentMouseDownPosition.Y - e.Y);
                else if ((e.X < this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(e.X, this.currentMouseDownPosition.Y, this.currentMouseDownPosition.X - e.X, e.Y - this.currentMouseDownPosition.Y);
                else
                    temprectangle = new Rectangle(e.X, e.Y, this.currentMouseDownPosition.X - e.X, this.currentMouseDownPosition.Y - e.Y);
                
                this.currentPaper.Refresh();
                this.CurrentRectangle = temprectangle;
                this.currentgraphics.DrawRectangle(TempPen, temprectangle);
            }
        }

        public void RectangleTool_OnMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
            this.currentPaper.AddGraphics(GraphicsType.Rectangle, new Point(this.CurrentRectangle.X / this.currentPaper.Zoom + this.currentPaper.ShowRectanglePoint.X, this.CurrentRectangle.Y / this.currentPaper.Zoom + this.currentPaper.ShowRectanglePoint.Y), this.CurrentRectangle.Width / this.currentPaper.Zoom, this.CurrentRectangle.Height / this.currentPaper.Zoom, this.CurrentPen);
            this.Refresh();
        }
        #endregion

        #region 椭圆工具的鼠标响应函数
        public void EllipseTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
            {
                if (e.Button != MouseButtons.Left)
                {
                    this.IsMouseDown = false;
                    return;
                }
                else
                {
                    this.IsMouseDown = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                }
            }
            else
            {
                this.currentgraphics.DrawEllipse(TempPen, this.CurrentRectangle);
            }
        }

        public void EllipseTool_OnMouseMove(object sender, MouseEventArgs e)
        {
             if (!this.IsMouseDown)
                return;
             else if ((e.X == this.currentMouseDownPosition.X) || (e.Y == this.currentMouseDownPosition.Y))
                 return;
             else
             {
                 Rectangle temprectangle;
                 if ((e.X > this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                     temprectangle = new Rectangle(this.currentMouseDownPosition, new Size(e.X - this.currentMouseDownPosition.X, e.Y - this.currentMouseDownPosition.Y));
                 else if ((e.X > this.currentMouseDownPosition.X) && (e.Y < currentMouseDownPosition.Y))
                     temprectangle = new Rectangle(this.currentMouseDownPosition.X, e.Y, e.X - this.currentMouseDownPosition.X, this.currentMouseDownPosition.Y - e.Y);
                 else if ((e.X < this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                     temprectangle = new Rectangle(e.X, this.currentMouseDownPosition.Y, this.currentMouseDownPosition.X - e.X, e.Y - this.currentMouseDownPosition.Y);
                 else
                     temprectangle = new Rectangle(e.X, e.Y, this.currentMouseDownPosition.X - e.X, this.currentMouseDownPosition.Y - e.Y);

                 this.currentPaper.Refresh();
                 this.CurrentRectangle = temprectangle;
                 this.currentgraphics.DrawEllipse(TempPen, temprectangle);
             }
        }

        public void EllipseTool_OnMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
            this.currentPaper.AddGraphics(GraphicsType.Ellipse, new Point(this.CurrentRectangle.X / this.currentPaper.Zoom + this.currentPaper.ShowRectanglePoint.X, this.CurrentRectangle.Y / this.currentPaper.Zoom + this.currentPaper.ShowRectanglePoint.Y), this.CurrentRectangle.Width / this.currentPaper.Zoom, this.CurrentRectangle.Height / this.currentPaper.Zoom, this.CurrentPen);
            this.Refresh();
        }
        #endregion

        #region 文字工具的鼠标响应函数
        public void TextTool_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseClick)
            {
                
                if (e.Button != MouseButtons.Left)
                    return;
                else
                {
                    this.IsMouseClick = true;
                    this.tempTextBox = new TextBox();
                    this.tempTextBox.BorderStyle = BorderStyle.None;
                    this.tempTextBox.Font = new Font(this.CurrentFont.FontFamily, this.CurrentFont.Size * this.Zoom, this.CurrentFont.Style, GraphicsUnit.Pixel);
                    this.tempTextBox.Size = new Size(5 * this.Zoom, this.CurrentFont.Height * this.Zoom);
                    this.currentPaper.Controls.Add(this.tempTextBox);
                    this.tempTextBox.Location = e.Location;
                    this.tempTextBox.Focus();
                    this.tempTextBox.TextChanged += new EventHandler(tempTextBox_TextChanged);
                    this.tempTextBox.LostFocus += new EventHandler(tempTextBox_LostFocus);
                    this.tempTextBox.Show();
                    this.Refresh();

                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;

                    this.TextToolStrip.CheckStateChanged += new EventHandler(TextToolStrip_CheckStateChanged);
                }
            }
            else
            {
                this.currentPaper.Focus();
                this.IsMouseClick = false;
            }
        }

        void TextToolStrip_CheckStateChanged(object sender, EventArgs e)
        {
            this.tempTextBox.TextChanged -= new EventHandler(tempTextBox_TextChanged);
            this.tempTextBox.LostFocus -= new EventHandler(tempTextBox_LostFocus);
            if (this.tempTextBox.Text == "")
            {
                this.tempTextBox.Dispose();
                return;
            }
            else
            {
                this.currentPaper.AddGraphics(GraphicsType.Text, this.tempTextBox.Text.Trim(), this.CurrentFont, this.CurrentBrush, new Point(this.currentMouseDownPosition.X / this.Zoom + this.currentPaper.ShowRectanglePoint.X, this.currentMouseDownPosition.Y / this.Zoom + this.currentPaper.ShowRectanglePoint.Y));
                this.tempTextBox.Dispose();
                this.Refresh();
            }
        }

        void tempTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.tempTextBox.Text == "")
            {
                this.tempTextBox.Size = new Size(5*this.Zoom, this.CurrentFont.Height*this.Zoom);
                return;
            }
            else
            {
                
                System.Drawing.SizeF pSize=this.currentgraphics.MeasureString(this.tempTextBox.Text.Trim(), this.tempTextBox.Font);
                this.tempTextBox.Size = new Size((int)pSize.Width + 2, (int)pSize.Height+1);
                this.currentPaper.Invalidate();
                this.Refresh();
            }
        }

        void tempTextBox_LostFocus(object sender, EventArgs e)
        {
            this.tempTextBox.TextChanged -= new EventHandler(tempTextBox_TextChanged);
            this.tempTextBox.LostFocus -= new EventHandler(tempTextBox_LostFocus);
            if (this.tempTextBox.Text == "")
            {
                this.tempTextBox.Dispose();
                return;
            }
            else
            {
                this.currentPaper.AddGraphics(GraphicsType.Text, this.tempTextBox.Text.Trim(), this.CurrentFont, this.CurrentBrush, new Point(this.currentMouseDownPosition.X / this.Zoom + this.currentPaper.ShowRectanglePoint.X, this.currentMouseDownPosition.Y / this.Zoom + this.currentPaper.ShowRectanglePoint.Y));
                this.tempTextBox.Dispose();
                this.Refresh();
            }
        }
        #endregion

        #region 铅笔工具的鼠标响应函数
        public void PencilTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
            {
                if (e.Button != MouseButtons.Left)
                {
                    this.IsMouseDown = false;
                    return;
                }
                else
                {
                    this.IsMouseDown = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                    this.tempArrayList.Clear();
                    tempArrayList.Add(new Point(e.X,e.Y));
                    finalArrayList.Add(new Point(e.X / this.currentPaper.Zoom + this.currentPaper.ShowRectanglePoint.X, e.Y / this.currentPaper.Zoom + this.currentPaper.ShowRectanglePoint.Y));
                    
                }
                this.TempPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            }
        }

        public void PencilTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
                return;
            if((e.X==((Point)tempArrayList[tempArrayList.Count-1]).X)&&((e.Y==((Point)tempArrayList[tempArrayList.Count-1]).Y)))
                return;
            this.tempArrayList.Add(new Point(e.X,e.Y));
            finalArrayList.Add(new Point(e.X / this.currentPaper.Zoom + this.currentPaper.ShowRectanglePoint.X, e.Y / this.currentPaper.Zoom + this.currentPaper.ShowRectanglePoint.Y));
            this.currentgraphics.DrawLines(this.TempPen, (Point[])tempArrayList.ToArray(typeof(Point)));
        }

        public void PencilTool_OnMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
            this.currentPaper.AddGraphics(GraphicsType.Pencil, this.finalArrayList, this.CurrentPen, this.CurrentBrush);
            tempArrayList.Clear();
            finalArrayList.Clear();
            this.Refresh();
            this.TempPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
        }
        #endregion

        #region 刷子工具的鼠标响应函数
        public void BrushTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
            {
                if (e.Button != MouseButtons.Left)
                {
                    this.IsMouseDown = false;
                    return;
                }
                else
                {
                    this.IsMouseDown = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                    tempArrayList.Clear();
                    finalArrayList.Clear();
                    Rectangle temp = new Rectangle(this.currentMouseDownPosition.X - this.BrushRadius*this.Zoom, this.currentMouseDownPosition.Y - this.BrushRadius*Zoom, 2 * this.BrushRadius*this.Zoom, 2 * this.BrushRadius*this.Zoom);
                    Rectangle temp1 = new Rectangle(this.currentMouseDownPosition.X / this.Zoom + this.currentPaper.ShowRectanglePoint.X - this.BrushRadius, this.currentMouseDownPosition.Y / this.Zoom + this.currentPaper.ShowRectanglePoint.Y - this.BrushRadius, this.BrushRadius * 2, this.BrushRadius * 2);
                    tempArrayList.Add(temp);
                    finalArrayList.Add(temp1);
                    this.currentgraphics.FillEllipse(this.CurrentBrush,temp);
                }
            }
        }

        public void BrushTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
                return;
            if ((e.X == this.currentMouseDownPosition.X) && (e.Y == this.currentMouseDownPosition.Y))
                return;
            this.currentMouseDownPosition.X = e.X;
            this.currentMouseDownPosition.Y = e.Y;
            Rectangle temp = new Rectangle(this.currentMouseDownPosition.X - this.BrushRadius * this.Zoom, this.currentMouseDownPosition.Y - this.BrushRadius * Zoom, 2 * this.BrushRadius * this.Zoom, 2 * this.BrushRadius * this.Zoom);
            Rectangle temp1 = new Rectangle(this.currentMouseDownPosition.X / this.Zoom + this.currentPaper.ShowRectanglePoint.X - this.BrushRadius, this.currentMouseDownPosition.Y / this.Zoom + this.currentPaper.ShowRectanglePoint.Y - this.BrushRadius, this.BrushRadius * 2, this.BrushRadius * 2);
            tempArrayList.Add(temp);
            finalArrayList.Add(temp1);
            this.currentgraphics.FillEllipse(this.CurrentBrush,temp);
        }

        public void BrushTool_OnMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
            this.currentPaper.AddGraphics(GraphicsType.Brush, finalArrayList, this.CurrentPen, this.CurrentBrush);
            this.Refresh();
        }
        #endregion

        #region 橡皮工具的鼠标响应函数
        public void EraserTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
            {
                if (e.Button != MouseButtons.Left)
                {
                    this.IsMouseDown = false;
                    return;
                }
                else
                {
                    this.IsMouseDown = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                    tempArrayList.Clear();
                    finalArrayList.Clear();
                    Rectangle temp = new Rectangle(this.currentMouseDownPosition.X - this.BrushRadius*this.Zoom, this.currentMouseDownPosition.Y - this.BrushRadius*Zoom, 2 * this.BrushRadius*this.Zoom, 2 * this.BrushRadius*this.Zoom);
                    Rectangle temp1 = new Rectangle(this.currentMouseDownPosition.X / this.Zoom + this.currentPaper.ShowRectanglePoint.X - this.BrushRadius, this.currentMouseDownPosition.Y / this.Zoom + this.currentPaper.ShowRectanglePoint.Y - this.BrushRadius, this.BrushRadius * 2, this.BrushRadius * 2);
                    tempArrayList.Add(temp);
                    finalArrayList.Add(temp1);
                    this.currentgraphics.FillEllipse(this.CurrentBrush,temp);
                }
            }
        }

        public void EraserTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
                return;
            if ((e.X == this.currentMouseDownPosition.X) && (e.Y == this.currentMouseDownPosition.Y))
                return;
            this.currentMouseDownPosition.X = e.X;
            this.currentMouseDownPosition.Y = e.Y;
            Rectangle temp = new Rectangle(this.currentMouseDownPosition.X - this.BrushRadius * this.Zoom, this.currentMouseDownPosition.Y - this.BrushRadius * Zoom, 2 * this.BrushRadius * this.Zoom, 2 * this.BrushRadius * this.Zoom);
            Rectangle temp1 = new Rectangle(this.currentMouseDownPosition.X / this.Zoom + this.currentPaper.ShowRectanglePoint.X - this.BrushRadius, this.currentMouseDownPosition.Y / this.Zoom + this.currentPaper.ShowRectanglePoint.Y - this.BrushRadius, this.BrushRadius * 2, this.BrushRadius * 2);
            tempArrayList.Add(temp);
            finalArrayList.Add(temp1);
            this.currentgraphics.FillEllipse(this.CurrentBrush, temp);
        }

        public void EraserTool_OnMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
            this.currentPaper.AddGraphics(GraphicsType.Eraser, finalArrayList);
            this.Refresh();
        }
        #endregion

        #region 染料桶工具的鼠标响应函数
        public void DyestuffTool_OnMouseDown(object sender, EventArgs e)
        { }

        public void DyestuffTool_OnMouseMove(object sender, EventArgs e)
        { }

        public void DyestuffTool_OnMouseUp(object sender, EventArgs e)
        { }
        #endregion

        #region 吸管工具的鼠标响应函数
        public void SuckerTool_OnMouseDown(object sender, EventArgs e)
        { }

        public void SuckerTool_OnMouseMove(object sender, EventArgs e)
        { }

        public void SuckerTool_OnMouseUp(object sender, EventArgs e)
        { }
        #endregion

        #region 放大镜工具的鼠标响应函数
        public void MagnifierTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
            {
                if (e.Button != MouseButtons.Left)
                {
                    return;
                }
                else
                {
                    this.IsMouseDown = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                    this.TempPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                }
            }
            else
            {
                this.currentgraphics.DrawRectangle(this.TempPen, this.CurrentRectangle);
            }
        }

        public void MagnifierTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsMouseDown)
            {
                Rectangle temprectangle;
                if ((e.X > this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(this.currentMouseDownPosition, new Size(e.X - this.currentMouseDownPosition.X, e.Y - this.currentMouseDownPosition.Y));
                else if ((e.X > this.currentMouseDownPosition.X) && (e.Y < currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(this.currentMouseDownPosition.X, e.Y, e.X - this.currentMouseDownPosition.X, this.currentMouseDownPosition.Y - e.Y);
                else if ((e.X < this.currentMouseDownPosition.X) && (e.Y > currentMouseDownPosition.Y))
                    temprectangle = new Rectangle(e.X, this.currentMouseDownPosition.Y, this.currentMouseDownPosition.X - e.X, e.Y - this.currentMouseDownPosition.Y);
                else
                    temprectangle = new Rectangle(e.X, e.Y, this.currentMouseDownPosition.X - e.X, this.currentMouseDownPosition.Y - e.Y);

                this.currentPaper.Refresh();
                this.CurrentRectangle = temprectangle;
                this.currentgraphics.DrawRectangle(TempPen, temprectangle);
            }
        }

        public void MagnifierTool_OnMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
            this.currentPaper.SetZoom((this.currentPaper.Width / this.CurrentRectangle.Width) * this.Zoom, new Point(this.currentMouseDownPosition.X/this.Zoom+this.currentPaper.ShowRectanglePoint.X,this.currentMouseDownPosition.Y/this.Zoom+this.currentPaper.ShowRectanglePoint.Y));
            this.Refresh();
        }
        #endregion

        #region 手型工具的鼠标响应函数
        public void HandTool_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.IsMouseDown)
            {
                if (e.Button != MouseButtons.Left)
                {
                    return;
                }
                else
                {
                    this.IsMouseDown = true;
                    this.currentMouseDownPosition.X = e.X;
                    this.currentMouseDownPosition.Y = e.Y;
                }
            }
        }

        public void HandTool_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsMouseDown)
            {
                this.currentPaper.ScrMove((e.X - this.currentMouseDownPosition.X)/this.Zoom, (e.Y - this.currentMouseDownPosition.Y)/this.Zoom);
                this.currentMouseDownPosition.X = e.X;
                this.currentMouseDownPosition.Y = e.Y;
            }
        }

        public void HandTool_OnMouseUp(object sender, MouseEventArgs e)
        {
            this.IsMouseDown = false;
        }
        #endregion

        #region 将选择的工具进行初始化
        /// <summary>
        /// 将选择的工具进行初始化
        /// </summary>
        private void initializeToolSelected()
        {
            if (currentPaper == null)
            {
                MessageBox.Show("未建立任何画纸或图层，工具选择失效！", "提示");
                return;
            }
            else
            {
                if (this.currentgraphics != null)
                    currentgraphics.Dispose();
                currentgraphics = this.currentPaper.CreateGraphics();
                switch (this.toolSelected)
                {
                    case ToolSelected.None:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);
                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Move:
                        this.currentPaper.MouseClick += new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown += new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Zoom:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.RectangleSelect:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.EllipseSelect:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Line:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Lines:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick += new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove += new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick += new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Rectangle:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown += new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Ellipse:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Text:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick += new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Pencil:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown += new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Brush:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Dyestuff:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Sucker:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Magnifier:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Hand:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                    case ToolSelected.Eraser:
                        this.currentPaper.MouseClick -= new MouseEventHandler(MoveTool_OnMouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MoveTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MoveTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MoveTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(ZoomTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(ZoomTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(ZoomTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseSelectTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseSelectTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseSelectTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(LineTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LineTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(LineTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(LinesTool_OnMouseClick);
                        this.currentPaper.MouseMove -= new MouseEventHandler(LinesTool_OnMouseMove);
                        this.currentPaper.MouseDown -= new MouseEventHandler(RectangleTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(RectangleTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(RectangleTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(EllipseTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(EllipseTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(EllipseTool_OnMouseUp);
                        this.currentPaper.MouseClick -= new MouseEventHandler(TextTool_MouseClick);
                        this.currentPaper.MouseDown -= new MouseEventHandler(PencilTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(PencilTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(PencilTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(BrushTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(BrushTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(BrushTool_OnMouseUp);
                        this.currentPaper.MouseDown += new MouseEventHandler(EraserTool_OnMouseDown);
                        this.currentPaper.MouseMove += new MouseEventHandler(EraserTool_OnMouseMove);
                        this.currentPaper.MouseUp += new MouseEventHandler(EraserTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(DyestuffTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(DyestuffTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(DyestuffTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(SuckerTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(SuckerTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(SuckerTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(MagnifierTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(MagnifierTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(MagnifierTool_OnMouseUp);
                        this.currentPaper.MouseDown -= new MouseEventHandler(HandTool_OnMouseDown);
                        this.currentPaper.MouseMove -= new MouseEventHandler(HandTool_OnMouseMove);
                        this.currentPaper.MouseUp -= new MouseEventHandler(HandTool_OnMouseUp);

                        this.currentPaper.MouseDoubleClick -= new MouseEventHandler(LinesTool_OnDoubleClick);
                        break;
                }
            }
        } 
        #endregion


    }
}
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
        private void showOpiton()
        {
            switch (this.toolSelected)
            {
                case ToolSelected.Move:
                    this.attributeInfoTab.Controls.Clear();
                    break;
                case ToolSelected.MoveNone:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        if (this.currentPaper.GetType().ToString() == "EasyPhoto.EPControl.Stage")
                        {
                            AttributeForm.StageMoveAttribute stagemoveattribute = new EasyPhoto.AttributeForm.StageMoveAttribute();
                            stagemoveattribute.SubParent = this;
                            stagemoveattribute.StageName = this.currentPaper.PaperName;
                            stagemoveattribute.StageWidth = this.currentPaper.BackgroundWidth;
                            stagemoveattribute.StageHeight = this.currentPaper.BackgroundHeight;
                            stagemoveattribute.StageBackColor = this.currentPaper.BackgroundColor;
                            stagemoveattribute.Zoom = this.currentPaper.Zoom;
                            stagemoveattribute.Location = new Point(5, 5);
                            this.attributeInfoTab.Controls.Add(stagemoveattribute);
                        }
                        
                        break;
                    }
                case ToolSelected.MoveRectangle:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.MoveRectangle moveattribute = new EasyPhoto.AttributeForm.MoveRectangle();
                        moveattribute.SubParent = this;
                        moveattribute.PaperX = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.X;
                        moveattribute.PaperY = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.Y;
                        moveattribute.PaperWidth = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CWidth;
                        moveattribute.PaperHeight = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CHeight;
                        moveattribute.PaperColor = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CPen.Color;
                        moveattribute.ColorWidth = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CPen.Width;
                        moveattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(moveattribute);
                        break;
                    }
                case ToolSelected.MoveEllipse:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.MoveRectangle moveattribute = new EasyPhoto.AttributeForm.MoveRectangle();
                        moveattribute.SubParent = this;
                        moveattribute.PaperX = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.X;
                        moveattribute.PaperY = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.Y;
                        moveattribute.PaperWidth = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CWidth;
                        moveattribute.PaperHeight = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CHeight;
                        moveattribute.PaperColor = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CPen.Color;
                        moveattribute.ColorWidth = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CPen.Width;
                        moveattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(moveattribute);
                        break;
                    }
                case ToolSelected.MoveEllipseSelect:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.MoveRectangleSelect moveattribute = new EasyPhoto.AttributeForm.MoveRectangleSelect();
                        moveattribute.SubParent = this;
                        moveattribute.PaperX = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.X;
                        moveattribute.PaperY = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.Y;
                        moveattribute.PaperWidth = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CWidth;
                        moveattribute.PaperHeight = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CHeight;
                        moveattribute.PaperBrush = new SolidBrush(((SolidBrush)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CBrush).Color);
                        this.attributeInfoTab.Controls.Add(moveattribute);
                        break;
                    }
                case ToolSelected.MoveImage:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        if (this.currentPaper.GetType().ToString() == "EasyPhoto.EPControl.Stage")
                        {
                            AttributeForm.MoveImage moveattribute = new EasyPhoto.AttributeForm.MoveImage();
                            moveattribute.SubParent = this;
                            moveattribute.PaperX = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.X;
                            moveattribute.PaperY = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.Y;
                            moveattribute.PaperWidth = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CWidth;
                            moveattribute.PaperHeight = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CHeight;
                            moveattribute.Diaph = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CDiaph;
                            this.attributeInfoTab.Controls.Add(moveattribute);
                        }
                        else
                        {
                            AttributeForm.LayerMoveAttribute layermouveattribute = new EasyPhoto.AttributeForm.LayerMoveAttribute();
                            layermouveattribute.SubParent = this;
                            layermouveattribute.LayerName = this.currentPaper.PaperName;
                            layermouveattribute.Zoom = this.currentPaper.Zoom;
                            layermouveattribute.Diaph = ((EasyPhoto.CustomClass.Cell)this.currentPaper.arrayList[0]).CDiaph;
                            layermouveattribute.Cansee = ((EasyPhoto.EPControl.Layer)this.currentPaper).CanSee;
                            layermouveattribute.Location = new Point(5, 5);
                            this.attributeInfoTab.Controls.Add(layermouveattribute);
                        }
                        break;
                    }
                case ToolSelected.MoveLine:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.MoveRectangle moveattribute = new EasyPhoto.AttributeForm.MoveRectangle();
                        moveattribute.SubParent = this;
                        moveattribute.PaperX = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.X;
                        moveattribute.PaperY = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.Y;
                        moveattribute.PaperWidth = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CWidth;
                        moveattribute.PaperHeight = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CHeight;
                        moveattribute.PaperColor = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CPen.Color;
                        moveattribute.ColorWidth = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CPen.Width;
                        moveattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(moveattribute);
                        break;
                    }
                case ToolSelected.MoveRectangleSelect:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.MoveRectangleSelect moveattribute = new EasyPhoto.AttributeForm.MoveRectangleSelect();
                        moveattribute.SubParent = this;
                        moveattribute.PaperX = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.X;
                        moveattribute.PaperY = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.Y;
                        moveattribute.PaperWidth = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CWidth;
                        moveattribute.PaperHeight = (int)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CHeight;
                        moveattribute.PaperBrush = new SolidBrush(((SolidBrush)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CBrush).Color);
                        this.attributeInfoTab.Controls.Add(moveattribute);
                        break;
                    }
                case ToolSelected.MoveText:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.MoveFont movefont = new EasyPhoto.AttributeForm.MoveFont();
                        movefont.SubParent = this;
                        movefont.PaperX = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.X;
                        movefont.PaperY = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).StartPoint.Y;
                        movefont.PaperString = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CString;
                        movefont.PaperColor = ((SolidBrush)((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CBrush).Color;
                        movefont.PaperFont = ((CustomClass.Cell)this.currentPaper.arrayList[this.currentPaper.ActiveNum]).CFont;
                        this.attributeInfoTab.Controls.Add(movefont);
                        break;
                    }
                case ToolSelected.Zoom:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        break;
                    }
                case ToolSelected.Rectangle:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.RectangleAttribute rectangleattribute = new EasyPhoto.AttributeForm.RectangleAttribute(this.CurrentPen.Color);
                        rectangleattribute.SubParent = this;
                        rectangleattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(rectangleattribute);
                        break;
                    }
                case ToolSelected.Ellipse:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.RectangleAttribute rectangleattribute = new EasyPhoto.AttributeForm.RectangleAttribute(this.CurrentPen.Color);
                        rectangleattribute.SubParent = this;
                        rectangleattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(rectangleattribute);
                        break;
                    }
                case ToolSelected.RectangleSelect:
                    {
                        
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.RectangleSelectAttribute rectangleselectattribute = new EasyPhoto.AttributeForm.RectangleSelectAttribute(this.CurrentPen.Color);
                        rectangleselectattribute.SubParent = this;
                        rectangleselectattribute.PaperBrush = (SolidBrush)this.CurrentBrush.Clone();
                        rectangleselectattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(rectangleselectattribute);
                        break;
                    }
                case ToolSelected.EllipseSelect:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.RectangleSelectAttribute rectangleselectattribute = new EasyPhoto.AttributeForm.RectangleSelectAttribute(this.CurrentPen.Color);
                        rectangleselectattribute.SubParent = this;
                        rectangleselectattribute.PaperBrush = (SolidBrush)this.CurrentBrush.Clone();
                        rectangleselectattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(rectangleselectattribute);
                        break;
                    }
                case ToolSelected.Pencil:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.RectangleAttribute rectangleattribute = new EasyPhoto.AttributeForm.RectangleAttribute(this.CurrentPen.Color);
                        rectangleattribute.SubParent = this;
                        rectangleattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(rectangleattribute);
                        break;
                    }
                case ToolSelected.Brush:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.BrushAttribute Brushattribute = new EasyPhoto.AttributeForm.BrushAttribute(this.CurrentPen.Color);
                        Brushattribute.SubParent = this;
                        Brushattribute.PaperBrush = (SolidBrush)this.CurrentBrush.Clone();
                        Brushattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(Brushattribute);
                        break;
                    }
                case ToolSelected.Eraser:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        break;
                    }
                case ToolSelected.Hand:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        break;
                    }
                case ToolSelected.Line:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.RectangleAttribute rectangleattribute = new EasyPhoto.AttributeForm.RectangleAttribute(this.CurrentPen.Color);
                        rectangleattribute.SubParent = this;
                        rectangleattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(rectangleattribute);
                        break;
                    }
                case ToolSelected.Lines:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.RectangleAttribute rectangleattribute = new EasyPhoto.AttributeForm.RectangleAttribute(this.CurrentPen.Color);
                        rectangleattribute.SubParent = this;
                        rectangleattribute.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(rectangleattribute);
                        break;
                    }
                case ToolSelected.Magnifier:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        break;
                    }
                case ToolSelected.Sucker:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        break;
                    }
                case ToolSelected.Text:
                    {
                        this.attributeInfoTab.Controls.Clear();
                        AttributeForm.FontAttribute fa = new EasyPhoto.AttributeForm.FontAttribute(this.CurrentPen.Color);
                        fa.SubParent = this;
                        fa.Location = new Point(5, 5);
                        this.attributeInfoTab.Controls.Add(fa);
                        break;
                    }
                case ToolSelected.None:
                    break;
            }
        }

        
    }
}
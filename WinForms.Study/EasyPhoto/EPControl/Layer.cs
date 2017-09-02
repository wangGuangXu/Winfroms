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
    [Serializable]
    public partial class Layer : Paper
    {
        public bool CanSee = true;
        public Layer(MainForm parent, String name, Size papersize, int backimagewidth, int backimageheight, Color backcolor)
            : base(parent, papersize)
        {
            this.paperName = name;
            if (papersize.Width > backimagewidth)
                base.hScrollBar1.Visible = false;
            else
            {
                base.hScrollBar1.Visible = true;                               //以后还需要补充
            }
            if (papersize.Height > backimageheight)
                base.vScrollBar1.Visible = false;
            else
            {
                base.vScrollBar1.Visible = true;                                   //以后还需要补充
            }
            this.hScrollBar1.Maximum = this.Width;
            this.vScrollBar1.Maximum = this.Height;

            SetBackgroundImage(backimagewidth, backimageheight, backcolor);
        }

        public Layer(Layer temp)
        {
            this.SuperParent = temp.SuperParent;
            this.backgroundColor = temp.BackgroundColor;
            this.backgroundHeight = temp.BackgroundHeight;
            this.backgroundWidth = temp.BackgroundWidth;
            this.Size = temp.Size;
            this.paperName = temp.PaperName;
            this.startPoint = temp.StartPosition;
            this.activeNum = temp.ActiveNum;
            this.backPaperColor = temp.BackPaperColor;
            this.showRectangle = temp.showRectangle;
            this.baseImage = (Bitmap)temp.baseImage.Clone();
            this.backImage = temp.BackImage;
            this.finalImage = temp.FinalImage;
            this.tempCell = temp.GetTempCell;
            this.zoom = temp.Zoom;
            this.arrayList = temp.GetArrayList();
            this.BackgroundImage = this.finalImage;

            if (this.Size.Width > this.backgroundWidth)
                base.hScrollBar1.Visible = false;
            else
            {
                base.hScrollBar1.Visible = true;                               //以后还需要补充
            }
            if (this.Size.Height > this.backgroundHeight)
                base.vScrollBar1.Visible = false;
            else
            {
                base.vScrollBar1.Visible = true;                                   //以后还需要补充
            }
            this.hScrollBar1.Maximum = this.Width;
            this.vScrollBar1.Maximum = this.Height;
            this.RePaint();
        }

        public Layer(EasyPhoto.LayerCase temp,MainForm superParent)
        {
            this.SuperParent = superParent;
            this.backgroundColor = temp.backgroundColor;
            this.backgroundHeight = temp.backgroundHeight;
            this.backgroundWidth = temp.backgroundWidth;
            this.Size = temp.size;
            this.paperName = temp.paperName;
            this.startPoint = temp.startPoint;
            this.activeNum = temp.activeNum;
            this.backPaperColor = new SolidBrush(temp.backPaperColor);
            this.showRectangle = temp.showRectangle;
            this.baseImage = (Bitmap)temp.baseImage.Clone();
            this.backImage = temp.backImage;
            this.finalImage = temp.finalImage;
            this.tempCell = temp.tempCell;
            this.zoom = temp.zoom;
            this.arrayList = temp.arrayList;
            this.CanSee = temp.Cansee;
            this.BackgroundImage = this.finalImage;

            if (this.Size.Width > this.backgroundWidth)
                base.hScrollBar1.Visible = false;
            else
            {
                base.hScrollBar1.Visible = true;                               //以后还需要补充
            }
            if (this.Size.Height > this.backgroundHeight)
                base.vScrollBar1.Visible = false;
            else
            {
                base.vScrollBar1.Visible = true;                                   //以后还需要补充
            }
            this.hScrollBar1.Maximum = this.Width;
            this.vScrollBar1.Maximum = this.Height;
            this.RePaint();
        }


        public void SetBackgroundImage(int width, int height, Color color)
        {
            this.backgroundColor = color;
            this.backgroundHeight = height;
            this.backgroundWidth = width;
            this.backImage = new Bitmap(this.Width, this.Height);
            this.finalImage = new Bitmap(this.Width, this.Height);

            this.startPoint.X = (this.Width - width) / 2;
            this.startPoint.Y = (this.Height - height) / 2;

            Graphics backg = Graphics.FromImage(this.backImage);
            backg.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, this.Width, this.Height));
            this.baseImage = (Bitmap)this.backImage.Clone();

            Graphics finalg = Graphics.FromImage(finalImage);
            finalg.DrawImage(backImage, this.Bounds, this.showRectangle, GraphicsUnit.Pixel);
            this.BackgroundImage = finalImage;

            this.AddGraphics(GraphicsType.Image, this.SuperParent.CurrentStage.ExportBackImage(), new Point(0, 0), this.SuperParent.CurrentStage.ExportBackImage().Width, this.SuperParent.CurrentStage.ExportBackImage().Height);
            ((EasyPhoto.CustomClass.Cell)this.arrayList[0]).CanDelete = false;
            ((EasyPhoto.CustomClass.Cell)this.arrayList[0]).CDiaph = 0.3f;
            this.RePaint();
        }

        public void SetDiaph(float value)
        {
            ((EasyPhoto.CustomClass.Cell)this.arrayList[0]).CDiaph = value;
            this.RePaint();
        }

        public Layer Copylayer()
        {
            Layer temp = new Layer(this);
            return temp;
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EasyPhoto.EPControl
{
    [Serializable]
    public class Stage:Paper
    {
        
        public Stage(MainForm parent,String name,Size papersize, int backimagewidth, int backimageheight, Color backcolor):base(parent,papersize)
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

        public void SetBackgroundImage(int width, int height, Color color)
        {
            this.backgroundColor = color;
            this.backgroundHeight = height;
            this.backgroundWidth = width;
            this.backImage = new Bitmap(this.Width, this.Height);
            this.finalImage = new Bitmap(this.Width, this.Height);

            Bitmap backPaper = new Bitmap((int)(width + 5), (int)(height + 5));
            Graphics g = System.Drawing.Graphics.FromImage(backPaper);
            g.FillRectangle(new SolidBrush(color), 0, 0, width , height );
            g.FillRectangle(new SolidBrush(Color.Black), width, 5, 5, height);
            g.FillRectangle(new SolidBrush(Color.Black), 5, height, width, 5);

            this.startPoint.X = (this.Width - backPaper.Width) / 2;
            this.startPoint.Y = (this.Height - backPaper.Height) / 2;
            Graphics backg = Graphics.FromImage(backImage);
            backg.FillRectangle(this.backPaperColor, new Rectangle(0, 0, this.Width, this.Height));
            backg.DrawImage(backPaper, this.startPoint);
            this.baseImage = (Bitmap)backImage.Clone();

            Graphics finalg = Graphics.FromImage(finalImage);
            finalg.DrawImage(backImage, this.Bounds, this.showRectangle, GraphicsUnit.Pixel);
            this.BackgroundImage = finalImage;

            this.RePaint();
        }

        public Stage(EasyPhoto.StageCase temp,MainForm superParent)
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
            if (temp.arrayList == null)
                this.arrayList = new System.Collections.ArrayList();
            else
                this.arrayList = temp.arrayList;
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

        /// <summary>
        /// 重置画纸背景色
        /// </summary>
        /// <param name="color">背景颜色</param>
        public void ResetBackColor(Color color)
        {
            Graphics baseg = Graphics.FromImage(this.baseImage);
            baseg.FillRectangle(new SolidBrush(color), new Rectangle(this.startPoint, new Size(this.backgroundWidth, this.backgroundHeight)));
            this.backImage = (Bitmap)this.baseImage.Clone();
            Graphics backg = Graphics.FromImage(backImage);
            for (int i = 0; i < this.arrayList.Count; i++)
            {
                ((EasyPhoto.CustomClass.Cell)this.arrayList[i]).CellPaint(backg);
            }
            this.OnPaint(null);
            this.Parent.Refresh();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;

namespace Canvas
{
    /// <summary>
    /// 网格层
    /// </summary>
	public class GridLayer : ICanvasLayer, ISerialize
	{
		public enum eStyle
		{
            /// <summary>
            /// 点
            /// </summary>
			Dots,
            /// <summary>
            /// 线
            /// </summary>
			Lines,
		}

		public SizeF spacing = new SizeF(1f, 1f); // 12"
		private bool enabled = true;
		private int minSize = 15;
        /// <summary>
        /// 网格样式
        /// </summary>
		private eStyle gridStyle = eStyle.Lines;
        /// <summary>
        /// 
        /// </summary>
		private Color color = Color.FromArgb(90, Color.Gray);

        /// <summary>
        /// 间距
        /// </summary>
		[XmlSerializable]
		public SizeF Spacing
		{
			get { return spacing; }
			set { spacing = value; }
		}


		[XmlSerializable]
		public int MinSize
		{
			get { return minSize; }
			set { minSize = value; }
		}

        /// <summary>
        /// 网格样式
        /// </summary>
		[XmlSerializable]
		public eStyle GridStyle
		{
			get { return gridStyle; }
            set { gridStyle = value; }
		}

        /// <summary>
        /// 颜色
        /// </summary>
		[XmlSerializable]
		public Color Color
		{
			get { return color; }
            set { color = value; }
		}


		public void Copy(GridLayer acopy)
		{
			enabled = acopy.enabled;
            spacing = acopy.spacing;
            minSize = acopy.minSize;
            gridStyle = acopy.gridStyle;
            color = acopy.color;
		}

		#region ICanvasLayer Members

        /// <summary>
        /// 绘制网格
        /// </summary>
        /// <param name="canvas">画布</param>
        /// <param name="unitrect">单元矩形</param>
		public void Draw(ICanvas canvas, RectangleF unitrect)
		{
            if (Enabled == false) return;

			float gridX = Spacing.Width;
			float gridY = Spacing.Height;

			float gridscreensizeX = canvas.ToScreen(gridX);
			float gridscreensizeY = canvas.ToScreen(gridY);

            if (gridscreensizeX < MinSize || gridscreensizeY < MinSize) return;

			PointF leftpoint = unitrect.Location;
			PointF rightpoint = ScreenUtils.RightPoint(canvas, unitrect);

			float left = (float)Math.Round(leftpoint.X / gridX) * gridX;
			float top = unitrect.Height + unitrect.Y;
			float right = rightpoint.X;
			float bottom = (float)Math.Round(leftpoint.Y / gridY) * gridY;

			if (GridStyle == eStyle.Dots)
			{
				GDI gdi = new GDI();
				gdi.BeginGDI(canvas.Graphics);
				for (float x = left; x <= right; x += gridX)
				{
					for (float y = bottom; y <= top; y += gridY)
					{
						PointF p1 = canvas.ToScreen(new UnitPoint(x, y));
                        gdi.SetPixel((int)p1.X, (int)p1.Y, color.ToArgb());
					}
				}
				gdi.EndGDI();
			}

			if (GridStyle == eStyle.Lines)
			{
                Pen pen = new Pen(color);
				GraphicsPath path = new GraphicsPath();

				// 画垂直线条
				while (left < right)
				{
					PointF p1 = canvas.ToScreen(new UnitPoint(left, leftpoint.Y));
					PointF p2 = canvas.ToScreen(new UnitPoint(left, rightpoint.Y));
					path.AddLine(p1, p2);
					path.CloseFigure();
					left += gridX;
				}

				// 画水平线条
				while (bottom < top)
				{
					PointF p1 = canvas.ToScreen(new UnitPoint(leftpoint.X, bottom));
					PointF p2 = canvas.ToScreen(new UnitPoint(rightpoint.X, bottom));
					path.AddLine(p1, p2);
					path.CloseFigure();
					bottom += gridY;
				}
				canvas.Graphics.DrawPath(pen, path);
			}
		}

		public string Id
		{
			get { return "grid"; }
		}


		public IEnumerable<IDrawObject> Objects
		{
			get { return null; }
		}

        /// <summary>
        /// 启用
        /// </summary>
		[XmlSerializable]
		public bool Enabled
		{
			get { return enabled; }
            set { enabled = value; }
		}

        /// <summary>
        /// 可见
        /// </summary>
		public bool Visible
		{
			get { return true; }
		}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="point"></param>
        /// <param name="otherobj"></param>
        /// <returns></returns>
        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, List<IDrawObject> otherobj)
        {
            if (Enabled == false)
            {
                return null;
            }

            UnitPoint snappoint = new UnitPoint();
            UnitPoint mousepoint = point;
            float gridX = Spacing.Width;
            float gridY = Spacing.Height;
            snappoint.X = (float)(Math.Round(mousepoint.X / gridX)) * gridX;
            snappoint.Y = (float)(Math.Round(mousepoint.Y / gridY)) * gridY;
            double threshold = canvas.ToUnit(/*ThresholdPixel*/6);
            if ((snappoint.X < point.X - threshold) || (snappoint.X > point.X + threshold))
                return null;
            if ((snappoint.Y < point.Y - threshold) || (snappoint.Y > point.Y + threshold))
                return null;
            return new GridSnapPoint(canvas, snappoint);
        }
		#endregion

		#region ISerialize
		public void GetObjectData(XmlWriter wr)
		{
			wr.WriteStartElement("gridlayer");
			XmlUtil.WriteProperties(this, wr);
			wr.WriteEndElement();
		}
		public void AfterSerializedIn()
		{
		}
		#endregion
	}
}

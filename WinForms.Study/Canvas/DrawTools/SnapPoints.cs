using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Canvas
{
    #region 捕捉点基类
    /// <summary>
    /// 捕捉点基类
    /// </summary>
    class SnapPointBase : ISnapPoint
    {
        protected UnitPoint m_snappoint;
        protected RectangleF m_boundingRect;
        protected IDrawObject m_owner;
        public IDrawObject Owner
        {
            get { return m_owner; }
        }
        public SnapPointBase(ICanvas canvas, IDrawObject owner, UnitPoint snappoint)
        {
            m_owner = owner;
            m_snappoint = snappoint;
            float size = (float)canvas.ToUnit(14);
            m_boundingRect.X = (float)(snappoint.X - size / 2);
            m_boundingRect.Y = (float)(snappoint.Y - size / 2);
            m_boundingRect.Width = size;
            m_boundingRect.Height = size;
        }
        #region ISnapPoint Members
        public virtual UnitPoint SnapPoint
        {
            get { return m_snappoint; }
        }
        public virtual RectangleF BoundingRect
        {
            get { return m_boundingRect; }
        }
        public virtual void Draw(ICanvas canvas)
        {
        }
        #endregion

        protected void DrawPoint(ICanvas canvas, Pen pen, Brush fillBrush)
        {
            Rectangle screenrect = ScreenUtils.ConvertRect(ScreenUtils.ToScreenNormalized(canvas, m_boundingRect));
            canvas.Graphics.DrawRectangle(pen, screenrect);
            screenrect.X++;
            screenrect.Y++;
            screenrect.Width--;
            screenrect.Height--;
            if (fillBrush != null)
                canvas.Graphics.FillRectangle(fillBrush, screenrect);
        }
    }
    #endregion

    #region 网格捕捉点
    /// <summary>
    /// 网格捕捉点
    /// </summary>
    class GridSnapPoint : SnapPointBase
    {
        public GridSnapPoint(ICanvas canvas, UnitPoint snappoint)
            : base(canvas, null, snappoint)
        {
        }
        #region ISnapPoint Members
        public override void Draw(ICanvas canvas)
        {
            DrawPoint(canvas, Pens.Gray, null);
        }
        #endregion
    }
    #endregion

    #region 捕捉顶点
    /// <summary>
    /// 捕捉顶点
    /// </summary>
    class VertextSnapPoint : SnapPointBase
    {
        public VertextSnapPoint(ICanvas canvas, IDrawObject owner, UnitPoint snappoint)
            : base(canvas, owner, snappoint)
        {
        }
        public override void Draw(ICanvas canvas)
        {
            DrawPoint(canvas, Pens.Blue, Brushes.YellowGreen);
        }
    }
    #endregion

    #region 捕捉中点
    /// <summary>
    /// 捕捉中点
    /// </summary>
    class MidpointSnapPoint : SnapPointBase
    {
        public MidpointSnapPoint(ICanvas canvas, IDrawObject owner, UnitPoint snappoint)
            : base(canvas, owner, snappoint)
        {
        }
        public override void Draw(ICanvas canvas)
        {
            DrawPoint(canvas, Pens.White, Brushes.YellowGreen);
        }
    } 
    #endregion
    
    /// <summary>
    /// 捕捉相交点
    /// </summary>
    class IntersectSnapPoint : SnapPointBase
	{
		public IntersectSnapPoint(ICanvas canvas, IDrawObject owner, UnitPoint snappoint)
			: base(canvas, owner, snappoint)
		{
		}
		public override void Draw(ICanvas canvas)
		{
			DrawPoint(canvas, Pens.White, Brushes.YellowGreen);
		}
	}

    /// <summary>
    /// 捕捉最近点
    /// </summary>
	class NearestSnapPoint : SnapPointBase
	{
		public NearestSnapPoint(ICanvas canvas, IDrawObject owner, UnitPoint snappoint)
			: base(canvas, owner, snappoint)
		{
		}
		#region ISnapPoint Members
		public override void Draw(ICanvas canvas)
		{
			DrawPoint(canvas, Pens.White, Brushes.YellowGreen);
		}
		#endregion
	}

    /// <summary>
    /// 扇形捕捉
    /// </summary>
	class QuadrantSnapPoint : SnapPointBase
	{
		public QuadrantSnapPoint(ICanvas canvas, IDrawObject owner, UnitPoint snappoint)
			: base(canvas, owner, snappoint)
		{
		}
		public override void Draw(ICanvas canvas)
		{
			DrawPoint(canvas, Pens.White, Brushes.YellowGreen);
		}
	}

    /// <summary>
    /// 分开捕捉
    /// </summary>
	class DivisionSnapPoint : SnapPointBase
	{
		public DivisionSnapPoint(ICanvas canvas, IDrawObject owner, UnitPoint snappoint)
			: base(canvas, owner, snappoint)
		{
		}
		public override void Draw(ICanvas canvas)
		{
			DrawPoint(canvas, Pens.White, Brushes.YellowGreen);
		}
	}

    /// <summary>
    /// 中心捕捉
    /// </summary>
	class CenterSnapPoint : SnapPointBase
	{
		public CenterSnapPoint(ICanvas canvas, IDrawObject owner, UnitPoint snappoint)
			: base(canvas, owner, snappoint)
		{
		}
		public override void Draw(ICanvas canvas)
		{
			DrawPoint(canvas, Pens.White, Brushes.YellowGreen);
		}
	}

    /// <summary>
    /// 垂直捕捉
    /// </summary>
	class PerpendicularSnapPoint : SnapPointBase
	{
		public PerpendicularSnapPoint(ICanvas canvas, IDrawObject owner, UnitPoint snappoint)
			: base(canvas, owner, snappoint)
		{
		}
		public override void Draw(ICanvas canvas)
		{
			DrawPoint(canvas, Pens.White, Brushes.YellowGreen);
		}
	}

    /// <summary>
    /// 正切捕捉点
    /// </summary>
	class TangentSnapPoint : SnapPointBase
	{
		public TangentSnapPoint(ICanvas canvas, IDrawObject owner, UnitPoint snappoint)
			: base(canvas, owner, snappoint)
		{
		}
		public override void Draw(ICanvas canvas)
		{
			DrawPoint(canvas, Pens.White, Brushes.YellowGreen);
		}
	}
}

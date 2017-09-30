using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Canvas
{

    /// <summary>
    /// 画布所有者接口
    /// </summary>
	public interface ICanvasOwner
	{
        /// <summary>
        /// 设置位置信息
        /// </summary>
        /// <param name="unitpos"></param>
		void SetPositionInfo(UnitPoint unitpos);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="snap"></param>
		void SetSnapInfo(ISnapPoint snap);
	}

    /// <summary>
    /// 画布接口
    /// </summary>
	public interface ICanvas
	{
        /// <summary>
        /// 数据模型
        /// </summary>
		IModel DataModel { get; }
		UnitPoint ScreenTopLeftToUnitPoint();
		UnitPoint ScreenBottomRightToUnitPoint();
		PointF ToScreen(UnitPoint unitpoint);
		float ToScreen(double unitvalue);
		double ToUnit(float screenvalue);
		UnitPoint ToUnit(PointF screenpoint);

		void Invalidate();

        /// <summary>
        /// 当前对象
        /// </summary>
		IDrawObject CurrentObject { get; }
        /// <summary>
        /// 
        /// </summary>
		Rectangle ClientRectangle { get; }
        /// <summary>
        /// 绘图对象
        /// </summary>
		Graphics Graphics { get; }
        /// <summary>
        /// 创建画笔
        /// </summary>
        /// <param name="color"></param>
        /// <param name="unitWidth"></param>
        /// <returns></returns>
		Pen CreatePen(Color color, float unitWidth);
        /// <summary>
        /// 绘制线
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="pen"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
		void DrawLine(ICanvas canvas, Pen pen, UnitPoint p1, UnitPoint p2);
        /// <summary>
        /// 画弧线
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="pen"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="beginangle"></param>
        /// <param name="angle"></param>
		void DrawArc(ICanvas canvas, Pen pen, UnitPoint center, float radius, float beginangle, float angle);
	}

    #region 模型接口
    /// <summary>
    /// 模型接口
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// 缩放值
        /// </summary>
        float Zoom { get; set; }
        /// <summary>
        /// 背景画布
        /// </summary>
        ICanvasLayer BackgroundLayer { get; }
        /// <summary>
        /// 表格画布
        /// </summary>
        ICanvasLayer GridLayer { get; }
        /// <summary>
        /// 
        /// </summary>
        ICanvasLayer[] Layers { get; }
        /// <summary>
        /// 活动画布
        /// </summary>
        ICanvasLayer ActiveLayer { get; set; }
        /// <summary>
        /// 获取画布
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICanvasLayer GetLayer(string id);
        /// <summary>
        /// 创建绘制对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="point"></param>
        /// <param name="snappoint"></param>
        /// <returns></returns>
        IDrawObject CreateObject(string type, UnitPoint point, ISnapPoint snappoint);
        void AddObject(ICanvasLayer layer, IDrawObject drawobject);
        void DeleteObjects(IEnumerable<IDrawObject> objects);
        void MoveObjects(UnitPoint offset, IEnumerable<IDrawObject> objects);
        void CopyObjects(UnitPoint offset, IEnumerable<IDrawObject> objects);
        void MoveNodes(UnitPoint position, IEnumerable<INodePoint> nodes);

        IEditTool GetEditTool(string id);
        void AfterEditObjects(IEditTool edittool);

        List<IDrawObject> GetHitObjects(ICanvas canvas, RectangleF selection, bool anyPoint);
        List<IDrawObject> GetHitObjects(ICanvas canvas, UnitPoint point);
        bool IsSelected(IDrawObject drawobject);
        void AddSelectedObject(IDrawObject drawobject);
        void RemoveSelectedObject(IDrawObject drawobject);
        IEnumerable<IDrawObject> SelectedObjects { get; }
        int SelectedCount { get; }
        void ClearSelectedObjects();

        ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, Type[] runningsnaptypes, Type usersnaptype);

        bool CanUndo();
        bool DoUndo();
        bool CanRedo();
        bool DoRedo();
    }
    #endregion

    #region 画布层接口
    /// <summary>
    /// 画布层接口
    /// </summary>
    public interface ICanvasLayer
    {
        string Id { get; }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="unitrect"></param>
        void Draw(ICanvas canvas, RectangleF unitrect);
        ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, List<IDrawObject> otherobj);
        IEnumerable<IDrawObject> Objects { get; }
        /// <summary>
        /// 启用
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool Visible { get; }
    } 
    #endregion

    #region 捕捉点接口
    /// <summary>
    /// 捕捉点接口
    /// </summary>
    public interface ISnapPoint
    {
        /// <summary>
        /// 绘制对象所有者
        /// </summary>
        IDrawObject Owner { get; }
        UnitPoint SnapPoint { get; }
        /// <summary>
        /// 外接矩形
        /// </summary>
        RectangleF BoundingRect { get; }
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="canvas"></param>
        void Draw(ICanvas canvas);
    } 
    #endregion

    /// <summary>
    /// 
    /// </summary>
	public enum eDrawObjectMouseDown
	{
        /// <summary>
        /// 这个绘图对象是完整的
        /// </summary>
		Done,		// this draw object is complete
        /// <summary>
        /// 这个对象是完整的，但创建相同类型的新对象
        /// </summary>
		DoneRepeat,	// this object is complete, but create new object of same type
        /// <summary>
        /// 此对象需要额外的鼠标输入
        /// </summary>
		Continue,	// this object requires additional mouse inputs
	}

    #region MyRegion
    /// <summary>
    /// 
    /// </summary>
    public interface INodePoint
    {
        IDrawObject GetClone();
        IDrawObject GetOriginal();
        void Cancel();
        void Finish();
        void SetPosition(UnitPoint pos);
        void Undo();
        void Redo();
        void OnKeyDown(ICanvas canvas, KeyEventArgs e);
    } 
    #endregion

    #region 绘制对象接口
    /// <summary>
    /// 绘制对象接口
    /// </summary>
    public interface IDrawObject
    {
        string Id { get; }
        /// <summary>
        /// 克隆绘制对象
        /// </summary>
        /// <returns></returns>
        IDrawObject Clone();
        bool PointInObject(ICanvas canvas, UnitPoint point);
        bool ObjectInRectangle(ICanvas canvas, RectangleF rect, bool anyPoint);
        void Draw(ICanvas canvas, RectangleF unitrect);
        RectangleF GetBoundingRect(ICanvas canvas);
        void OnMouseMove(ICanvas canvas, UnitPoint point);
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="point"></param>
        /// <param name="snappoint"></param>
        /// <returns></returns>
        eDrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snappoint);
        /// <summary>
        /// 鼠标按下弹起
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="point"></param>
        /// <param name="snappoint"></param>
        void OnMouseUp(ICanvas canvas, UnitPoint point, ISnapPoint snappoint);
        /// <summary>
        /// 键盘按下
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="e"></param>
        void OnKeyDown(ICanvas canvas, KeyEventArgs e);
        UnitPoint RepeatStartingPoint { get; }
        INodePoint NodePoint(ICanvas canvas, UnitPoint point);
        ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, List<IDrawObject> otherobj, Type[] runningsnaptypes, Type usersnaptype);
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="offset"></param>
        void Move(UnitPoint offset);

        string GetInfoAsString();
    } 
    #endregion

    #region 工具条编辑接口
    /// <summary>
    /// 工具条编辑接口
    /// </summary>
    public interface IEditTool
    {
        IEditTool Clone();

        bool SupportSelection { get; }
        void SetHitObjects(UnitPoint mousepoint, List<IDrawObject> list);

        void OnMouseMove(ICanvas canvas, UnitPoint point);
        eDrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snappoint);
        void OnMouseUp(ICanvas canvas, UnitPoint point, ISnapPoint snappoint);
        void OnKeyDown(ICanvas canvas, KeyEventArgs e);
        void Finished();
        void Undo();
        void Redo();
    } 
    #endregion

    /// <summary>
    /// 
    /// </summary>
	public interface IEditToolOwner
	{
		void SetHint(string text);
	}
}

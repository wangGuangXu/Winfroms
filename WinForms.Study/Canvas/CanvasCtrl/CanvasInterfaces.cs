using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Canvas
{

    /// <summary>
    /// ���������߽ӿ�
    /// </summary>
	public interface ICanvasOwner
	{
        /// <summary>
        /// ����λ����Ϣ
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
    /// �����ӿ�
    /// </summary>
	public interface ICanvas
	{
        /// <summary>
        /// ����ģ��
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
        /// ��ǰ����
        /// </summary>
		IDrawObject CurrentObject { get; }
        /// <summary>
        /// 
        /// </summary>
		Rectangle ClientRectangle { get; }
        /// <summary>
        /// ��ͼ����
        /// </summary>
		Graphics Graphics { get; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="color"></param>
        /// <param name="unitWidth"></param>
        /// <returns></returns>
		Pen CreatePen(Color color, float unitWidth);
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="pen"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
		void DrawLine(ICanvas canvas, Pen pen, UnitPoint p1, UnitPoint p2);
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="pen"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="beginangle"></param>
        /// <param name="angle"></param>
		void DrawArc(ICanvas canvas, Pen pen, UnitPoint center, float radius, float beginangle, float angle);
	}

    #region ģ�ͽӿ�
    /// <summary>
    /// ģ�ͽӿ�
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// ����ֵ
        /// </summary>
        float Zoom { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        ICanvasLayer BackgroundLayer { get; }
        /// <summary>
        /// ��񻭲�
        /// </summary>
        ICanvasLayer GridLayer { get; }
        /// <summary>
        /// 
        /// </summary>
        ICanvasLayer[] Layers { get; }
        /// <summary>
        /// �����
        /// </summary>
        ICanvasLayer ActiveLayer { get; set; }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICanvasLayer GetLayer(string id);
        /// <summary>
        /// �������ƶ���
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

    #region ������ӿ�
    /// <summary>
    /// ������ӿ�
    /// </summary>
    public interface ICanvasLayer
    {
        string Id { get; }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="unitrect"></param>
        void Draw(ICanvas canvas, RectangleF unitrect);
        ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, List<IDrawObject> otherobj);
        IEnumerable<IDrawObject> Objects { get; }
        /// <summary>
        /// ����
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool Visible { get; }
    } 
    #endregion

    #region ��׽��ӿ�
    /// <summary>
    /// ��׽��ӿ�
    /// </summary>
    public interface ISnapPoint
    {
        /// <summary>
        /// ���ƶ���������
        /// </summary>
        IDrawObject Owner { get; }
        UnitPoint SnapPoint { get; }
        /// <summary>
        /// ��Ӿ���
        /// </summary>
        RectangleF BoundingRect { get; }
        /// <summary>
        /// ����
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
        /// �����ͼ������������
        /// </summary>
		Done,		// this draw object is complete
        /// <summary>
        /// ��������������ģ���������ͬ���͵��¶���
        /// </summary>
		DoneRepeat,	// this object is complete, but create new object of same type
        /// <summary>
        /// �˶�����Ҫ������������
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

    #region ���ƶ���ӿ�
    /// <summary>
    /// ���ƶ���ӿ�
    /// </summary>
    public interface IDrawObject
    {
        string Id { get; }
        /// <summary>
        /// ��¡���ƶ���
        /// </summary>
        /// <returns></returns>
        IDrawObject Clone();
        bool PointInObject(ICanvas canvas, UnitPoint point);
        bool ObjectInRectangle(ICanvas canvas, RectangleF rect, bool anyPoint);
        void Draw(ICanvas canvas, RectangleF unitrect);
        RectangleF GetBoundingRect(ICanvas canvas);
        void OnMouseMove(ICanvas canvas, UnitPoint point);
        /// <summary>
        /// ��갴��
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="point"></param>
        /// <param name="snappoint"></param>
        /// <returns></returns>
        eDrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snappoint);
        /// <summary>
        /// ��갴�µ���
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="point"></param>
        /// <param name="snappoint"></param>
        void OnMouseUp(ICanvas canvas, UnitPoint point, ISnapPoint snappoint);
        /// <summary>
        /// ���̰���
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="e"></param>
        void OnKeyDown(ICanvas canvas, KeyEventArgs e);
        UnitPoint RepeatStartingPoint { get; }
        INodePoint NodePoint(ICanvas canvas, UnitPoint point);
        ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, List<IDrawObject> otherobj, Type[] runningsnaptypes, Type usersnaptype);
        /// <summary>
        /// �ƶ�
        /// </summary>
        /// <param name="offset"></param>
        void Move(UnitPoint offset);

        string GetInfoAsString();
    } 
    #endregion

    #region �������༭�ӿ�
    /// <summary>
    /// �������༭�ӿ�
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

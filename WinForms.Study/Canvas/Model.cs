using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;

namespace Canvas
{
    /// <summary>
    /// 数据模型
    /// </summary>
	public class DataModel : IModel
	{
		static Dictionary<string, Type> m_toolTypes = new Dictionary<string,Type>();
		static public IDrawObject NewDrawObject(string objecttype)
		{
			if (m_toolTypes.ContainsKey(objecttype))
			{
				string type = m_toolTypes[objecttype].ToString();
				return Assembly.GetExecutingAssembly().CreateInstance(type) as IDrawObject;
			}
			return null;
		}

		Dictionary<string, IDrawObject> m_drawObjectTypes = new Dictionary<string, IDrawObject>(); 
		DrawTools.DrawObjectBase CreateObject(string objecttype)
		{
			if (m_drawObjectTypes.ContainsKey(objecttype))
			{
				return m_drawObjectTypes[objecttype].Clone() as DrawTools.DrawObjectBase;
			}
			return null;
		}


		Dictionary<string, IEditTool> m_editTools = new Dictionary<string, IEditTool>(); 
		public void AddEditTool(string key, IEditTool tool)
		{
			m_editTools.Add(key, tool);
		}

		public bool IsDirty
		{
			get { return m_undoBuffer.Dirty; }
		}
		UndoRedoBuffer m_undoBuffer = new UndoRedoBuffer();
		
		UnitPoint m_centerPoint = UnitPoint.Empty;
        /// <summary>
        /// 中心点
        /// </summary>
		[XmlSerializable]
		public UnitPoint CenterPoint
		{
			get { return m_centerPoint; }
			set { m_centerPoint = value; }
		}

		float m_zoom = 1;
        /// <summary>
        /// 表格属性
        /// </summary>
		GridLayer m_gridLayer = new GridLayer();
        /// <summary>
        /// 背景属性
        /// </summary>
		BackgroundLayer m_backgroundLayer = new BackgroundLayer();
		List<ICanvasLayer> m_layers = new List<ICanvasLayer>();
		ICanvasLayer m_activeLayer;
		Dictionary<IDrawObject, bool> m_selection = new Dictionary<IDrawObject, bool>();
		public DataModel()
		{
			m_toolTypes.Clear();
			m_toolTypes[DrawTools.Line.ObjectType] = typeof(DrawTools.Line);
			m_toolTypes[DrawTools.Circle.ObjectType] = typeof(DrawTools.Circle);
			m_toolTypes[DrawTools.Arc.ObjectType] = typeof(DrawTools.Arc);
			m_toolTypes[DrawTools.Arc3Point.ObjectType] = typeof(DrawTools.Arc3Point);
			DefaultLayer();
			m_centerPoint = new UnitPoint(0,0);
		}

        /// <summary>
        /// 默认层
        /// </summary>
        void DefaultLayer()
        {
            m_layers.Clear();
            m_layers.Add(new DrawingLayer("layer0", "Hairline Layer", Color.White, 0.0f));
            m_layers.Add(new DrawingLayer("layer1", "0.005 Layer", Color.Red, 0.005f));
            m_layers.Add(new DrawingLayer("layer2", "0.025 Layer", Color.Green, 0.025f));
        }

		public void AddDrawTool(string key, IDrawObject drawtool)
		{
			m_drawObjectTypes[key] = drawtool;
		}

        /// <summary>
        /// 保存为xml文件
        /// </summary>
        /// <param name="filename"></param>
		public void Save(string filename)
		{
			try
			{
				XmlTextWriter wr = new XmlTextWriter(filename, null);
				wr.Formatting = Formatting.Indented;
				wr.WriteStartElement("CanvasDataModel");
				m_backgroundLayer.GetObjectData(wr);
				m_gridLayer.GetObjectData(wr);

				foreach (ICanvasLayer layer in m_layers)
				{
                    if (layer is ISerialize)
                    {
                        ((ISerialize)layer).GetObjectData(wr);
                    }
				}

				XmlUtil.WriteProperties(this, wr);
				wr.WriteEndElement();
				wr.Close();
				m_undoBuffer.Dirty = false;
			}
			catch { }
		}


		public bool Load(string filename)
		{
			try
			{
				StreamReader sr = new StreamReader(filename);
				//XmlTextReader rd = new XmlTextReader(sr);
				XmlDocument doc = new XmlDocument();
				doc.Load(sr);
				sr.Dispose();
				XmlElement root = doc.DocumentElement;
				if (root.Name != "CanvasDataModel")
					return false;

				m_layers.Clear();
				m_undoBuffer.Clear();
				m_undoBuffer.Dirty = false;
				foreach (XmlElement childnode in root.ChildNodes)
				{
					if (childnode.Name == "backgroundlayer")
					{
						XmlUtil.ParseProperties(childnode, m_backgroundLayer);
						continue;
					}
					if (childnode.Name == "gridlayer")
					{
						XmlUtil.ParseProperties(childnode, m_gridLayer);
						continue;
					}
					if (childnode.Name == "layer")
					{
						DrawingLayer l = DrawingLayer.NewLayer(childnode as XmlElement);
						m_layers.Add(l);
					}
					if (childnode.Name == "property")
						XmlUtil.ParseProperty(childnode, this);
				}
				return true;
			}
			catch (Exception e)
			{
				DefaultLayer();
				Console.WriteLine("Load exception - {0}", e.Message);
			}
			return false;
		}





		public IDrawObject GetFirstSelected()
		{
			if (m_selection.Count > 0)
			{
				Dictionary<IDrawObject, bool>.KeyCollection.Enumerator e = m_selection.Keys.GetEnumerator();
				e.MoveNext();
				return e.Current;
			}
			return null;
		}


		#region IModel Members
        /// <summary>
        /// 缩放值
        /// </summary>
		[XmlSerializable]
		public float Zoom
		{
			get { return m_zoom; }
			set { m_zoom = value; }
		}

        /// <summary>
        /// 
        /// </summary>
		public ICanvasLayer BackgroundLayer
		{
			get { return m_backgroundLayer; }
		}

        /// <summary>
        /// 
        /// </summary>
		public ICanvasLayer GridLayer
		{
			get { return m_gridLayer; }
		}

        /// <summary>
        /// 
        /// </summary>
		public ICanvasLayer[] Layers
		{
			get { return m_layers.ToArray(); }
		}

        /// <summary>
        /// 活动层
        /// </summary>
		public ICanvasLayer ActiveLayer
		{
			get
			{
                if (m_activeLayer == null)
                {
                    m_activeLayer = m_layers[0];
                }
				return m_activeLayer;
			}
			set
			{
				m_activeLayer = value;
			}
		}

		public ICanvasLayer GetLayer(string id)
		{
			foreach (ICanvasLayer layer in m_layers)
			{
                if (layer.Id == id)
                {
                    return layer;
                }
			}
			return null;
		}

        /// <summary>
        /// 创建绘制对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="point"></param>
        /// <param name="snappoint"></param>
        /// <returns></returns>
		public IDrawObject CreateObject(string type, UnitPoint point, ISnapPoint snappoint)
		{
			DrawingLayer layer = ActiveLayer as DrawingLayer;
            if (layer.Enabled == false)
            {
                return null;
            }

			DrawTools.DrawObjectBase newobj = CreateObject(type);
			if (newobj != null)
			{
				newobj.Layer = layer;
				newobj.InitializeFromModel(point, layer, snappoint);
			}
			return newobj as IDrawObject;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="drawobject"></param>
		public void AddObject(ICanvasLayer layer, IDrawObject drawobject)
		{
            if (drawobject is DrawTools.IObjectEditInstance)
            {
                drawobject = ((DrawTools.IObjectEditInstance)drawobject).GetDrawObject();
            }

            if (m_undoBuffer.CanCapture)
            {
                m_undoBuffer.AddCommand(new EditCommandAdd(layer, drawobject));
            }

			((DrawingLayer)layer).AddObject(drawobject);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objects"></param>
		public void DeleteObjects(IEnumerable<IDrawObject> objects)
		{
			EditCommandRemove undocommand = null;
            if (m_undoBuffer.CanCapture)
            {
                undocommand = new EditCommandRemove();
            }
				
			foreach (ICanvasLayer layer in m_layers)
			{
				List<IDrawObject> removedobjects = ((DrawingLayer)layer).DeleteObjects(objects);
                if (removedobjects != null && undocommand != null)
                {
                    undocommand.AddLayerObjects(layer, removedobjects);
                }
			}

            if (undocommand != null)
            {
                m_undoBuffer.AddCommand(undocommand);
            }
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="objects"></param>
		public void MoveObjects(UnitPoint offset, IEnumerable<IDrawObject> objects)
		{
            if (m_undoBuffer.CanCapture)
            {
                m_undoBuffer.AddCommand(new EditCommandMove(offset, objects));
            }

            foreach (IDrawObject obj in objects)
            {
                obj.Move(offset);
            }
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="objects"></param>
		public void CopyObjects(UnitPoint offset, IEnumerable<IDrawObject> objects)
		{
			ClearSelectedObjects();
			List<IDrawObject> newobjects = new List<IDrawObject>();

			foreach (IDrawObject obj in objects)
			{
				IDrawObject newobj = obj.Clone();
				newobjects.Add(newobj);
				newobj.Move(offset);
				((DrawingLayer)ActiveLayer).AddObject(newobj);
				AddSelectedObject(newobj);
			}

            if (m_undoBuffer.CanCapture)
            {
                m_undoBuffer.AddCommand(new EditCommandAdd(ActiveLayer, newobjects));
            }
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edittool"></param>
		public void AfterEditObjects(IEditTool edittool)
		{
			edittool.Finished();
            if (m_undoBuffer.CanCapture)
            {
                m_undoBuffer.AddCommand(new EditCommandEditTool(edittool));
            }
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edittoolid"></param>
        /// <returns></returns>
		public IEditTool GetEditTool(string edittoolid)
		{
            if (m_editTools.ContainsKey(edittoolid))
            {
                return m_editTools[edittoolid].Clone();
            }
			return null;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="nodes"></param>
		public void MoveNodes(UnitPoint position, IEnumerable<INodePoint> nodes)
		{
            if (m_undoBuffer.CanCapture)
            {
                m_undoBuffer.AddCommand(new EditCommandNodeMove(nodes));
            }

			foreach (INodePoint node in nodes)
			{
				node.SetPosition(position);
				node.Finish();
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="selection"></param>
        /// <param name="anyPoint"></param>
        /// <returns></returns>
		public List<IDrawObject> GetHitObjects(ICanvas canvas, RectangleF selection, bool anyPoint)
		{
			List<IDrawObject> selected = new List<IDrawObject>();
			foreach (ICanvasLayer layer in m_layers)
			{
                if (layer.Visible == false)
                {
                    continue;
                }

				foreach (IDrawObject drawobject in layer.Objects)
				{
                    if (drawobject.ObjectInRectangle(canvas, selection, anyPoint))
                    {
                        selected.Add(drawobject);
                    }
				}
			}
			return selected;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="point"></param>
        /// <returns></returns>
		public List<IDrawObject> GetHitObjects(ICanvas canvas, UnitPoint point)
		{
			List<IDrawObject> selected = new List<IDrawObject>();
			foreach (ICanvasLayer layer in m_layers)
			{
                if (layer.Visible == false)
                {
                    continue;
                }

				foreach (IDrawObject drawobject in layer.Objects)
				{
                    if (drawobject.PointInObject(canvas, point))
                    {
                        selected.Add(drawobject);
                    }
				}
			}
			return selected;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drawobject"></param>
        /// <returns></returns>
		public bool IsSelected(IDrawObject drawobject)
		{
			return m_selection.ContainsKey(drawobject);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drawobject"></param>
		public void AddSelectedObject(IDrawObject drawobject)
		{
			DrawTools.DrawObjectBase obj = drawobject as DrawTools.DrawObjectBase;
			RemoveSelectedObject(drawobject);
			m_selection[drawobject] = true;
			obj.Selected = true;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drawobject"></param>
		public void RemoveSelectedObject(IDrawObject drawobject)
		{
			if (m_selection.ContainsKey(drawobject))
			{
				DrawTools.DrawObjectBase obj = drawobject as DrawTools.DrawObjectBase;
				obj.Selected = false;
				m_selection.Remove(drawobject);
			}
		}


		public IEnumerable<IDrawObject> SelectedObjects
		{
			get
			{
				return m_selection.Keys;
			}
		}


		public int SelectedCount
		{
			get { return m_selection.Count; }
		}

        /// <summary>
        /// 
        /// </summary>
		public void ClearSelectedObjects()
		{
			IEnumerable<IDrawObject> x = SelectedObjects;
			foreach (IDrawObject drawobject in x)
			{
				DrawTools.DrawObjectBase obj = drawobject as DrawTools.DrawObjectBase;
				obj.Selected = false;
			}
			m_selection.Clear();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="point"></param>
        /// <param name="runningsnaptypes"></param>
        /// <param name="usersnaptype"></param>
        /// <returns></returns>
		public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, Type[] runningsnaptypes, Type usersnaptype)
		{
			List<IDrawObject> objects = GetHitObjects(canvas, point);
            if (objects.Count == 0)
            {
                return null;
            }

			foreach (IDrawObject obj in objects)
			{
				ISnapPoint snap = obj.SnapPoint(canvas, point, objects, runningsnaptypes, usersnaptype);
                if (snap != null)
                {
                    return snap;
                }
			}
			return null;
		}


		public bool CanUndo()
		{
			return m_undoBuffer.CanUndo;
		}

		public bool DoUndo()
		{
			return m_undoBuffer.DoUndo(this);
		}

		public bool CanRedo()
		{
			return m_undoBuffer.CanRedo;
		}


		public bool DoRedo()
		{
			return m_undoBuffer.DoRedo(this);
		}
		#endregion
	}
}

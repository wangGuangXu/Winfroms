﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Plan.Rooms;

namespace Plan
{
    public partial class Form1 : Form
    {
        #region 变量
        //程序运行的目录
        public static string applicationDirectory = System.Windows.Forms.Application.StartupPath;
        /// <summary>
        /// 是否编辑
        /// </summary>
        private bool _isEdit = false;
        /// <summary>
        /// 当前选择房源
        /// </summary>
        private RoomBase _selctedRoom = null;
        List<Point> _tempPoint = new List<Point>();
        /// <summary>
        /// 房屋图形列表
        /// </summary>
        private List<RoomBase> _roomControls = new List<RoomBase>();
        /// <summary>
        /// 工具条
        /// </summary>
        private RoomTools _roomTools = new RoomTools();
        /// <summary>
        /// 图形点左表数组列表
        /// </summary>
        public List<Point[]> _roomPoints = new List<Point[]>();
        #endregion

        #region 构造函数
        public Form1()
        {
            this.DoubleBuffered = true;
            InitializeComponent();

            _roomTools.Hide();
            panelRight.Controls.Add(_roomTools);

            string imgPath = applicationDirectory + @"\Images\background.jpg";

            if (File.Exists(imgPath))
            {
                this.panelRight.BackgroundImage = Image.FromFile(imgPath);
                this.panelRight.BackgroundImageLayout = ImageLayout.None;
            }
            this.Resize += Form1_Resize;
            this.panelRight.Paint += Panel1_Paint;
        } 
        #endregion

        #region 窗体大小调整
        private void Form1_Resize(object sender, EventArgs e)
        {
            panelLeft.Top = 0;
            panelLeft.Left = 0;
            panelLeft.Width = 150;
            panelLeft.Height = this.Height;

            panelRight.Top = 0;
            panelRight.Left = panelLeft.Width;
            panelRight.Width = this.Width - panelLeft.Width;
            panelRight.Height = this.Height;
        }
        #endregion

        #region 面板容器重绘
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (RoomBase r in this._roomControls)
            {
                r.SetRefresh(e.Graphics);
            }
        }
        #endregion

        

        #region 加载图形坐标信息
        private void btnLoad_Click(object sender, EventArgs e)
        {
            Init();
        }
        private void Init()
        {
            var points = new Point[5];

            for (int i = 1; i < 5; i++)
            {
                switch (i)
                {
                    case 1:
                        points[0] = new Point(178, 355);
                        points[1] = new Point(335, 412);
                        points[2] = new Point(454, 359);
                        points[3] = new Point(366, 301);
                        points[4] = new Point(250, 354);
                        break;
                    case 2:
                        points[0] = new Point(414, 299);
                        points[1] = new Point(503, 354);
                        points[2] = new Point(667, 269);
                        points[3] = new Point(577, 219);
                        points[4] = new Point(414, 300);
                        break;
                    case 3:
                        points[0] = new Point(556, 387);
                        points[1] = new Point(675, 462);
                        points[2] = new Point(845, 365);
                        points[3] = new Point(720, 297);
                        points[4] = new Point(555, 385);
                        break;
                    case 4:
                        points[0] = new Point(718, 216);
                        points[1] = new Point(807, 265);
                        points[2] = new Point(921, 203);
                        points[3] = new Point(837, 159);
                        points[4] = new Point(719, 215);
                        break;
                }
                _roomPoints.Add(points);

                LoadChart(new List<Point[]>() { _tempPoint.ToArray() }, new RoomEntity { Number = i.ToString("1000") });
            }
        }

        /// <summary>
        /// 根据点坐标列表加载图形
        /// </summary>
        /// <param name="points"></param>
        private void LoadChart(List<Point[]> points, RoomEntity roomInfo)
        {
            foreach (var item in _roomPoints)
            {
                var room = new VectorRoomControl();
                room.Points = item.ToArray();// _tempPoint.ToArray();

                if (roomInfo == null)
                {
                    roomInfo = new RoomEntity();
                }
                roomInfo.ID = Guid.NewGuid();
                roomInfo.CouldYouRent = true;
                roomInfo.HaveToRent = true;

                room.RoomInfo = roomInfo;
                _roomControls.Add(room);
            }
            panelRight.Refresh();
        }
        #endregion

        #region 添加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isEdit = true;
            _tempPoint.Clear();
            _tempPoint = new List<Point>();
        } 
        #endregion

        #region 删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selctedRoom == null) return;

            _roomControls.Remove(_selctedRoom);
            _selctedRoom = null;
            _roomTools.Hide();
            panelRight.Refresh();
        }
        #endregion

        #region 判断点是否在不规则区域内
        /// <summary>
        /// 判断点是否在不规则区域内
        /// </summary>
        /// <param name="pnt1"></param>
        /// <param name="fencePnts"></param>
        /// <returns></returns>
        private bool PointInFences(Point pnt1, Point[] fencePnts)
        {
            int wn = 0, j = 0; //wn 计数器 j第二个点指针
            for (int i = 0; i < fencePnts.Length; i++)
            {
                //开始循环
                if (i == fencePnts.Length - 1)
                {
                    j = 0;//如果 循环到最后一点 第二个指针指向第一点
                }
                else
                {
                    j = j + 1; //如果不是 ，则找下一点
                }
                if (fencePnts[i].Y <= pnt1.Y) // 如果多边形的点 小于等于 选定点的 Y 坐标
                {
                    if (fencePnts[j].Y > pnt1.Y) // 如果多边形的下一点 大于于 选定点的 Y 坐标
                    {
                        if (IsLeft(fencePnts[i], fencePnts[j], pnt1) > 0)
                        {
                            wn++;
                        }
                    }
                }
                else
                {
                    if (fencePnts[j].Y <= pnt1.Y)
                    {
                        if (IsLeft(fencePnts[i], fencePnts[j], pnt1) < 0)
                        {
                            wn--;
                        }
                    }
                }
            }
            if (wn == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int IsLeft(Point P0, Point P1, Point P2)
        {
            int abc = ((P1.X - P0.X) * (P2.Y - P0.Y) - (P2.X - P0.X) * (P1.Y - P0.Y));
            return abc;
        }



        #endregion

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && _isEdit)
            {
                _tempPoint.Add(e.Location);
            }
            else if (_isEdit)
            {
                _tempPoint.Add(e.Location);
                _isEdit = false;

                _roomPoints.Add(_tempPoint.ToArray());
                LoadChart(new List<Point[]>() { _tempPoint.ToArray() }, null);
            }

            if (this.ContextMenuStrip != null)
            {
                this.ContextMenuStrip.Hide();
            }

            if (!_isEdit)
            {
                foreach (var item in _roomControls)
                {
                    VectorRoomControl room = item as VectorRoomControl;
                    room.Selected = PointInFences(e.Location, room.Points);
                    if (room.Selected)
                    {
                        this._selctedRoom = room;
                        _roomTools.Room = this._selctedRoom;
                        _roomTools.Location = new Point(e.Location.X - 70, e.Location.Y + 60);
                        _roomTools.Show();

                        //右键菜单
                        //if (e.Button == System.Windows.Forms.MouseButtons.Right)
                        //{
                        //    this.ContextMenuStrip = room.ContextMenuStrip;
                        //}
                    }
                }
                panelRight.Refresh();
            }
        }
    }
}

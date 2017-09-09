using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plan
{
    public partial class RoomStateImage : Form
    {
        /// <summary>
        /// 是否编辑
        /// </summary>
        private bool _isEdit = false;
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
        /// 当前选择房源
        /// </summary>
        private RoomBase _selctedRoom = null;
        public RoomStateImage()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            _roomTools.Hide();
            this.Controls.Add(_roomTools);

            string imgPath = @"E:\img\background.jpg";
            if (!File.Exists(imgPath)) return;

            this.BackgroundImage = Image.FromFile(imgPath);
            this.BackgroundImageLayout = ImageLayout.Center;
        }

        private void LoadImg()
        {
            //BaseRoomControl r1 = new VectorRoomControl();

            //RENTRoomStateEntity roomInfo = new RENTRoomStateEntity();
            //roomInfo.CouldYouRent = true;
            //roomInfo.HaveToRent = true;
           
            //r1.SetParements("0,0;30,0;50,30;50,60");
            //_roomControls.Add(r1);
            //r1.Parent = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _isEdit = true;
            _tempPoint.Clear();
            _tempPoint = new List<Point>();
        }

        private void RoomStateImage_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button== System.Windows.Forms.MouseButtons.Left && _isEdit)
            {
                _tempPoint.Add(e.Location);
            }
            else if ( _isEdit)
            {
                _tempPoint.Add(e.Location);
                _isEdit = false;

                VectorRoomControl room = new VectorRoomControl();
                room.Points = _tempPoint.ToArray();
                //room. = Guid.NewGuid().ToString();
                //room.BaseRoomPanelConfig.ShowDialog();

                RENTRoomStateEntity roomInfo = new RENTRoomStateEntity();
                roomInfo.ID = Guid.NewGuid();
                roomInfo.CouldYouRent = true;
                roomInfo.HaveToRent = true;
              
                room.RoomInfo = roomInfo;
                _roomControls.Add(room);
                this.Refresh();
                //room.Location = room.Points[0];
                //this.Controls.Add(room);
                //this.LoadImg();
         
            }
            if(!_isEdit)
            {
                foreach (var item in _roomControls)
                {
                    VectorRoomControl room = item as VectorRoomControl;
                    room.Selected = PointInFences(e.Location, room.Points);
                    if (room.Selected) {
                        this._selctedRoom = room;
                        _roomTools.Room = this._selctedRoom;
                        _roomTools.Location = e.Location;
                        _roomTools.Show();
                    }
                }
                this.Refresh();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (RoomBase r in this._roomControls)
            {
                r.SetRefresh(e.Graphics);
            }
        }
         
        private void RoomStateImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void RoomStateImage_Click(object sender, EventArgs e)
        {
            
        }

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
                        if (isLeft(fencePnts[i], fencePnts[j], pnt1) > 0)
                        {
                            wn++;
                        }
                    }
                }
                else
                {
                    if (fencePnts[j].Y <= pnt1.Y)
                    {
                        if (isLeft(fencePnts[i], fencePnts[j], pnt1) < 0)
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
        public int isLeft(Point P0, Point P1, Point P2)
        {
            int abc = ((P1.X - P0.X) * (P2.Y - P0.Y) - (P2.X - P0.X) * (P1.Y - P0.Y));
            return abc;
        }
        #endregion



        private void button2_Click(object sender, EventArgs e)
        {
            if (_selctedRoom == null) return;

            _roomControls.Remove(_selctedRoom);
            _selctedRoom = null;
            _roomTools.Hide();
            this.Refresh();
        }
    }
}

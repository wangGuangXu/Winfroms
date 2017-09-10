using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Plan
{
    /// <summary>
    /// 矢量图基类
    /// </summary>
    public class VectorRoomControl:  RoomBase
    {
        /// <summary>
        /// 点数组
        /// </summary>
        private Point[] _points;

        public Point[] Points
        {
            get { return _points; }
            set { _points = value; }
        }

        /// <summary>
        /// 画刷
        /// </summary>
        public Brush _brush;
        public Pen _myPen;
        /// <summary>
        /// 分解参数
        /// </summary>
        /// <param name="args"></param>
        public override void SetParements(string args)
        {
            string[] pointArry=args.Split(';');

            _points = new Point[pointArry.Length];

            //取出点坐标
            for (int i = 0; i < pointArry.Length; i++)
            {
                var currParement = pointArry[i].Split(',');
                if (currParement.Length==2)
                {
                    _points[i].X = int.Parse(currParement[0]);
                    _points[i].Y = int.Parse(currParement[1]);
                }
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void SetRefresh(Graphics g)
        {
            _myPen = new Pen(Color.Gray, 3);

            //可租
            if (this.RoomInfo.CouldYouRent.HasValue && this.RoomInfo.CouldYouRent.Value)
            {
                _brush = new SolidBrush(Color.Blue);
                //_myPen = new Pen(Color.Blue, 3);
            }

            //已租
            if (this.RoomInfo.HaveToRent.HasValue && this.RoomInfo.HaveToRent.Value)
            {
                _brush = new SolidBrush(Color.Red);
                //_myPen = new Pen(Color.Red, 3);
            }

            //填充图形
            if (g == null) return;

            if (Selected)
            {
                //让所选择区域为透明
                _brush = new SolidBrush(Color.Transparent);
                _myPen = new Pen(Color.Yellow, 3);

                //_brush = new SolidBrush(Color.Yellow);
                //_myPen = new Pen(Color.Red, 3);
            }
            g.FillPolygon(_brush, _points);
            g.DrawPolygon(_myPen, _points);

            //绘制文字
            g.DrawString(this.RoomInfo.Number, new Font("微软雅黑", 16, FontStyle.Bold), new SolidBrush(Color.White), _points[0].X + 50, _points[0].Y - 30);
        }

        public void Acce(Graphics g, Point location)
        {
            _myPen = new Pen(Color.Blue, 3);
            var ace = _points[_points.Length - 1];
            g.DrawLine(_myPen, ace, location);
        }
        
    }
}

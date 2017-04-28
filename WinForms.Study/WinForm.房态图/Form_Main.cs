using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm.房态图
{
    public partial class Form_Main : Form
    {
        #region 属性
        /// <summary>
        /// 创建数据委托
        /// </summary>
        public delegate void CreateDelegate();
        /// <summary>
        /// 笔刷数组
        /// </summary>
        Brush[] brushstr = {Brushes.DeepSkyBlue,Brushes.ForestGreen,Brushes.Purple,Brushes.SlateGray};
        string[] RoomState = { "可租", "不可租", "已迁出", "空置房" };
        /// <summary>
        /// 房源列表
        /// </summary>
        public List<RoomLable> RoomLables { get; set; }
        
        #endregion

        #region 构造函数
        public Form_Main()
        {
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            //SetStyle(ControlStyles.UserPaint, true);                //自行绘制控件
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);     //禁止擦除背景
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);    //启用双缓冲绘图
            //SetStyle(ControlStyles.ResizeRedraw |ControlStyles.UserPaint| ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();

            //Type type = flowLayoutPanel1.GetType();
            //PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);//
            //pi.SetValue(flowLayoutPanel1, true, null);

            //临时挂起控件布局逻辑
            this.flowLayoutPanel1.SuspendLayout();
            //移除所有控件
            this.flowLayoutPanel1.Controls.Clear();
            //启用双缓冲绘制控件
            DoubleBuffered = true;
            //动态创建控件
            this.BeginInvoke(new CreateDelegate(ShowRoom));

            //恢复正常布局逻辑
            this.flowLayoutPanel1.ResumeLayout();

            btnAll.Click += BtnAll_Click;
            btnVacantHousingUnit.Click += BtnVacantHousingUnit_Click;
            btnNotRent.Click += BtnNotRent_Click;
            btnRent.Click += BtnRent_Click;
            btnMovedOut.Click += BtnMovedOut_Click;
        }
        #endregion

        #region 左侧按钮事件注册
        //全部
        private void BtnAll_Click(object sender, EventArgs e)
        {
            GetRoomListByState("");
        }
        //可租
        private void BtnNotRent_Click(object sender, EventArgs e)
        {
            GetRoomListByState(RoomState[0]);
        }
        //不可租
        private void BtnRent_Click(object sender, EventArgs e)
        {
            GetRoomListByState(RoomState[1]);
        }
        //已迁出
        private void BtnMovedOut_Click(object sender, EventArgs e)
        {
            GetRoomListByState(RoomState[2]);
        }
        //空置房
        private void BtnVacantHousingUnit_Click(object sender, EventArgs e)
        {
            GetRoomListByState(RoomState[3]);
        }
        #endregion

        #region 房源右键菜单事件
        private void 签订合同ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoomEdit edit = new RoomEdit();
            edit.Show();
        } 
        #endregion

        #region 显示房源信息
        /// <summary>
        /// 显示房源信息
        /// </summary>
        private void ShowRoom()
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.Controls.Clear();
            }
            CreateRoomLable();
            flowLayoutPanel1.Controls.AddRange(RoomLables.ToArray());
        } 
        #endregion

        #region 动态生成房源控件
        /// <summary>
        /// 动态生成房源控件
        /// </summary>
        private void CreateRoomLable()
        {
            RoomLables = new List<RoomLable>(50);
            //string state = string.Empty;
            for (int i = 1; i <= 50; i++)
            {
                var my = new RoomLable(this, 100, 120, i.ToString("10000"));
                //int s = GetState(ref state, i);
                //my.SetRoomInfo(brushstr[s], state, "20.00");

                RoomLables.Add(my);
            }
        } 
        #endregion

        #region 获取某个状态的房源
        public void GetRoomListByState(string state)
        {
            //state = "状态：" + state;
            if (RoomLables == null || !RoomLables.Any())
            {
                ShowRoom();
            }

            flowLayoutPanel1.Controls.Clear();

            RoomLable[] roomArry = new RoomLable[] { };
            if (string.IsNullOrEmpty(state))
            {
                roomArry = RoomLables.ToArray();
            }
            else
            {
                roomArry = RoomLables.Where(a => a.State == state).ToArray();
            }

            flowLayoutPanel1.Controls.AddRange(roomArry);
        }
        
        #endregion

        #region 获取某个房源的状态
        /// <summary>
        /// 获取某个房源的状态
        /// </summary>
        /// <param name="state"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private int GetState(ref string state, int i)
        {
            int s = 0;
            int zl = 5;
            if (i <= 10 * zl)
            {
                s = 0;
            }
            else if (i >= 10 * zl && i < 20 * zl)
            {
                s = 1;
            }
            else if (i >= 20 * zl && i < 30 * zl)
            {
                s = 2;
            }
            else if (i >= 30 * zl && i <= 40 * zl)
            {
                s = 3;
            }

            state = RoomState[s];

            return s;
        } 
        #endregion

        protected override bool DoubleBuffered
        {
            get
            {
                return base.DoubleBuffered;
            }
            set
            {
                base.DoubleBuffered = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plan
{
    /// <summary>
    /// 房间可视化基类
    /// </summary>
    public abstract  class RoomBase
    {
        private RoomEntity _roomInfo;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 房屋基本信息
        /// </summary>
        public RoomEntity RoomInfo 
        {
            get 
            {
                return _roomInfo;
            }
            set
            {
                if (value==null)
                {
                    throw new Exception("房屋基本信息不能为空");
                }
               _roomInfo =value;

               SetRefresh(null);
            } 
        }

        /// <summary>
        /// 参数配置面板窗体
        /// </summary>
        private BaseRoomPanel _baseRoomPanelConfig;

        public BaseRoomPanel BaseRoomPanelConfig
        {
            get { return _baseRoomPanelConfig; }
            set { _baseRoomPanelConfig = value; }
        }

        public ContextMenuStrip ContextMenuStrip = new ContextMenuStrip();

        public RoomBase()
        {
            this.Selected = false;

            var toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            toolStripMenuItem1.Text = "铺位属性";
            toolStripMenuItem1.Click += ToolStripMenuItem1_Click;
            ContextMenuStrip.Items.Add(toolStripMenuItem1);

            var toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem2.Name = "toolStripMenuItem1";
            toolStripMenuItem2.Size = new System.Drawing.Size(192, 22);
            toolStripMenuItem2.Text = "刷新";
            ContextMenuStrip.Items.Add(toolStripMenuItem2);
        }

        /// <summary>
        /// 配置铺位属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (BaseRoomPanelConfig==null)
            {
                BaseRoomPanelConfig = new BaseRoomPanel();
            }
            BaseRoomPanelConfig.Text = "铺位属性设置";
            BaseRoomPanelConfig.RoomInfo = RoomInfo;
            BaseRoomPanelConfig.ShowDialog();
        }

        /// <summary>
        /// 设置刷新
        /// </summary>
        public virtual void SetRefresh(Graphics g)
        {

        }

        /// <summary>
        /// 设置绘图参数
        /// </summary>
        /// <param name="args"></param>
        public virtual void SetParements(string args)
        {

        }

    }
}

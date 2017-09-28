using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plan
{
    /// <summary>
    /// 工具条
    /// </summary>
    public partial class RoomTools : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public RoomBase Room { get; set; }
        public RoomTools()
        {
            InitializeComponent();
        }

        private void toolStripMsg_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.ToolTipTitle = "友情提示：";
            this.toolTip1.IsBalloon = false;
            this.toolTip1.UseFading = true;

            //this.toolTip1.SetToolTip(this.toolStrip1, "收费消息提醒");
        }

        private void toolStripMsg_Click(object sender, EventArgs e)
        {
            if (Room == null || Room.RoomInfo == null) return;

            MessageBox.Show(string.Format("当前房源ID：{0}",Room.RoomInfo.ID));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plan
{
    /// <summary>
    /// 配置参数面板窗体
    /// </summary>
    public partial class BaseRoomPanel : Form
    {
        /// <summary>
        /// 房屋基本信息
        /// </summary>
        public RoomEntity RoomInfo { get; set; }

        public BaseRoomPanel()
        {
            InitializeComponent();
        }

        private void BaseRoomPanel_Load(object sender, EventArgs e)
        {
            if (RoomInfo != null)
            {
                txtID.Text = RoomInfo.ID.ToString();
                txtNumber.Text = RoomInfo.Number;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (RoomInfo == null) return;

            RoomInfo.Number = txtNumber.Text.Trim();

            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}

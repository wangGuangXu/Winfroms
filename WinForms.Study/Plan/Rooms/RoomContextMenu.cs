using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Plan.Rooms
{
    /// <summary>
    /// 右键菜单
    /// </summary>
    public partial class RoomContextMenu : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public RoomBase Room { get; set; }
        public RoomContextMenu()
        {
            InitializeComponent();
        }

    }
}

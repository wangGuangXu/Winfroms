using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plan
{
    /// <summary>
    /// 租赁房源状态表
    /// </summary>
    [Serializable]
    public class RENTRoomStateEntity
    {
        #region 公共属性
        /// <summary>
        /// 主键 ID(NOT NULL)
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 套户编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 套户ID
        /// </summary>
        public Guid? RoomID { get; set; }
        /// <summary>
        /// 是否定价
        /// </summary>
        public bool? PricingState { get; set; }
        /// <summary>
        /// 是否可租
        /// </summary>
        public bool? CouldYouRent { get; set; }
        /// <summary>
        /// 是否已租
        /// </summary>
        public bool? HaveToRent { get; set; }
        /// <summary>
        /// 是否预定
        /// </summary>
        public bool? WhetherReserve { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid? CreatePerson { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid? UpdatePerson { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 可返租
        /// </summary>
        public bool? AllowLeaseback { get; set; }
        /// <summary>
        /// 已返租
        /// </summary>
        public bool? HaveLeaseback { get; set; }
        #endregion


    }
}

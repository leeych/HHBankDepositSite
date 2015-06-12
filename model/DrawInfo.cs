using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DrawInfo
    {
        /// <summary>
        /// 协议编号
        /// </summary>
        public string ProtocolId { get; set; }

        /// <summary>
        /// 支取日期
        /// </summary>
        public DateTime DrawDate { get; set; }

        /// <summary>
        /// 支取金额
        /// </summary>
        public decimal DrawMoney { get; set; }

        /// <summary>
        /// 剩余金额
        /// </summary>
        public decimal RemainMoney { get; set; }

        /// <summary>
        /// 靠档利息
        /// </summary>
        public decimal SectionInterest { get; set; }

        /// <summary>
        /// 补息金额
        /// </summary>
        public decimal MarginInterest { get; set; }

        /// <summary>
        /// 综合业务系统利息
        /// </summary>
        public decimal SystemInterest { get; set; }

        /// <summary>
        /// 支取状态
        /// </summary>
        public DrawFlag DrawStatus { get; set; }

        /// <summary>
        /// 如果支取状态为“部分提前支取”，此字段表示第二次全部支取，否则不填
        /// </summary>
        public DateTime? FinalDrawDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CalcResult
    {
        /// <summary>
        /// 约定到期日期
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// 综合业务系统的利息
        /// </summary>
        public decimal SystemInterest { get; set; }

        /// <summary>
        /// 靠档利息
        /// </summary>
        public decimal SectionInterest { get; set; }

        /// <summary>
        /// 应补利息
        /// </summary>
        public decimal MarginInterest { get; set; }

        /// <summary>
        /// 靠档方案
        /// </summary>
        public string SectionDesc { get; set; }
    }
}

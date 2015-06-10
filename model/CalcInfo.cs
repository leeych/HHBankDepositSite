using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class CalcInfo
    {
        /// <summary>
        /// 存入日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 支取日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 本金
        /// </summary>
        public decimal CapitalMoney { get; set; }

        /// <summary>
        /// 存期
        /// </summary>
        public Period DepositPeriod { get; set; }
    }
}

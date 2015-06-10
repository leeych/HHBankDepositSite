using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DrawRecord
    {
        /// <summary>
        /// 协议编号
        /// </summary>
        public string ProtocolID { get; set; }

        /// <summary>
        /// 存单账号
        /// </summary>
        public string BillAccount { get; set; }

        /// <summary>
        /// 凭证号
        /// </summary>
        public string BillCode { get; set; }

        /// <summary>
        /// 约定存期
        /// </summary>
        public Period BillPeriod { get; set; }

        /// <summary>
        /// 约定利率
        /// </summary>
        public BankRate Rate { get; set; }

        /// <summary>
        /// 存入日期
        /// </summary>
        public DateTime DepositDate { get; set; }

        /// <summary>
        /// 应到期日期
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// 本金
        /// </summary>
        public decimal CapticalMoney { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string DepositorName { get; set; }

        /// <summary>
        /// 客户身份证号
        /// </summary>
        public string DepositorIDCard { get; set; }

        /// <summary>
        /// 存款经办柜员
        /// </summary>
        public string TellerCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 绑定账号
        /// </summary>
        public string BindAccount { get; set; }

        /// <summary>
        /// 支取日期
        /// </summary>
        public DateTime DrawDate { get; set; }

        /// <summary>
        /// 支取金额
        /// </summary>
        public decimal DrawMoney { get; set; }

        /// <summary>
        /// 综合业务系统利息
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
    }
}
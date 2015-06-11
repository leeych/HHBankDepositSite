using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DepositRecord
    {
        /// <summary>
        /// 协议编号
        /// </summary>
        public string ProtocolID { get; set; }

        /// <summary>
        /// 存入日期
        /// </summary>
        public DateTime DepositDate { get; set; }

        /// <summary>
        /// 存单账号
        /// </summary>
        public string BillAccount { get; set; }

        /// <summary>
        /// 存单凭证号+50
        /// </summary>
        public string BillCode { get; set; }

        /// <summary>
        /// 机构号
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 柜员号
        /// </summary>
        public string TellerCode { get; set; }

        /// <summary>
        /// 柜员姓名
        /// </summary>
        public string TellerName { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string DepositorName { get; set; }

        /// <summary>
        /// 客户身份证号
        /// </summary>
        public string DepositorIDCard { get; set; }

        /// <summary>
        /// 存入金额
        /// </summary>
        public decimal DepositMoney { get; set; }

        /// <summary>
        /// 约定存期
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// 存款利率
        /// </summary>
        public BankRate Rate { get; set; }

        ///// <summary>
        ///// 提前支取金额
        ///// </summary>
        //public decimal EarlierDrawMoney { get; set; }

        ///// <summary>
        ///// 结存金额
        ///// </summary>
        //public decimal RemainMoney { get; set; }

        ///// <summary>
        ///// 提前支取日期
        ///// </summary>
        //public DateTime EarlierDrawDate { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public DateTime CalcDueDate { get; set; }

        ///// <summary>
        ///// 到期支取金额
        ///// </summary>
        //public decimal DueDateDrawMoney { get; set; }

        ///// <summary>
        ///// 靠档利息
        ///// </summary>
        //public decimal EarlierInterest { get; set; }

        /// <summary>
        /// 综合业务系统计算的利息
        /// </summary>
        public decimal SystemInterest { get; set; }

        ///// <summary>
        ///// 靠档利息 - 综合业务系统利息
        ///// </summary>
        //public decimal MarginInterest { get; set; }

        /// <summary>
        /// 补息账号
        /// </summary>
        public string BindAccount { get; set; }

        /// <summary>
        /// 存取标志（存入、支取、部提）
        /// </summary>
        public int DepositFlag { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}

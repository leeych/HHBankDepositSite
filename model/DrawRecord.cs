﻿using System;
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
        public DateTime FirstDrawDate { get; set; }

        /// <summary>
        /// 支取金额
        /// </summary>
        public decimal FirstDrawMoney { get; set; }

        /// <summary>
        /// 综合业务系统利息
        /// </summary>
        public decimal FirstSysInterest { get; set; }

        /// <summary>
        /// 靠档利息
        /// </summary>
        public decimal FirstSectionInterest { get; set; }

        /// <summary>
        /// 应补利息
        /// </summary>
        public decimal FirstMarginInterest { get; set; }

        /// <summary>
        /// 存入||部分提前支取||支取
        /// </summary>
        public DrawFlag Status { get; set; }

        /// <summary>
        /// 部分提前支取剩余金额
        /// </summary>
        public decimal RemainMoney { get; set; }

        /// <summary>
        /// 最后一次支取日期
        /// </summary>
        public DateTime FinalDrawDate { get; set; }

        /// <summary>
        /// 最后一次支取金额
        /// </summary>
        public decimal FinalDrawMoney { get; set; }

        /// <summary>
        /// 最后一次支取：系统利息
        /// </summary>
        public decimal FinalSysInterest { get; set; }

        /// <summary>
        /// 最后一次支取：靠档利息
        /// </summary>
        public decimal FinalSectionInterest { get; set; }

        /// <summary>
        /// 最后一次支取补息金额
        /// </summary>
        public decimal FinalMarginInterest { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ExcelRecordInfo
    {
        public string ProtocolID { get; set; }
        public string BillAccount { get; set; }
        public string BillCode { get; set; }
        public DateTime DepositDate { get; set; }
        public decimal DepositMoney { get; set; }
        public string BillPeriod { get; set; }
        public string ClientName { get; set; }
        public string ClientIDCard { get; set; }
        public string DepositStatus { get; set; }
        public string BindAccount { get; set; }
        public DateTime DueDate { get; set; }

        public decimal Y05Rate { get; set; }
        public decimal Y03Rate { get; set; }
        public decimal Y02Rate { get; set; }
        public decimal Y01Rate { get; set; }
        public decimal M06Rate { get; set; }
        public decimal M03Rate { get; set; }
        public decimal CurrRate { get; set; }

        public string TellerCode { get; set; }
        public string TellerName { get; set; }
        public string Remark { get; set; }

        public string FirstDrawDate { get; set; }
        public string FirstDrawMoney { get; set; }
        public string FirstSysInterest { get; set; }
        public string FirstCalcInterest { get; set; }
        public string FirstMarginInterest { get; set; }

        public decimal RemainMoney { get; set; }

        public string FinalDrawDate { get; set; }
        public string FinalDrawMoney { get; set; }
        public string FinalSysInterest { get; set; }
        public string FinalCalcInterest { get; set; }
        public string FinalMarginInterest { get; set; }    
    }
}

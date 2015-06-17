using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SearchInfo
    {
        public string ProtocolID { get; set; }
        public string BillAccount { get; set; }
        public string BillCode { get; set; }
        public decimal DepositMoney { get; set; }
        public DateTime DepositDate { get; set; }
        public Period BillPeriod { get; set; }
        public BankRate ExecRate { get; set; }
        public string ClientName { get; set; }
        public string ClientID { get; set; }
        public decimal DueInterest { get; set; }
        public string TellerCode { get; set; }
        public DrawFlag Status { get; set; }
    }
}

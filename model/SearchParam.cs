using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SearchParam
    {
        public string ProtocolID { get; set; }
        public string BillAccount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

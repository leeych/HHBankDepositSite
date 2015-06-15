using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum RoleFlag
    {
        Teller,
        Credit,
        Director,
        Governor
    }

    public class TellerInfo
    {
        public string TellerCode { get; set; }

        public string TellerName { get; set; }

        public string OrgCode { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public RoleFlag Role { get; set; }
    }
}

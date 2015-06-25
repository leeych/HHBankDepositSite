using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BankRateInfo
    {
        public DateTime EffectDate { get; set; }
        public BankRate Rate { get; set; }
    }
}

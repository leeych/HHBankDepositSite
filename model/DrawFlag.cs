using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum DrawFlag
    {
        Deposit = 0,    // 存入
        Draw,           // 部分提前支取
        Remain,         // 已支取
        ElseDraw,       // 他行支取
        Other
    }
}

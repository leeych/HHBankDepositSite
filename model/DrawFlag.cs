using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum DrawFlag
    {
        Deposit = 0,    // 存入未支取
        Draw,           // 已全部支取
        Remain,         // 部分提前支取
        ElseDraw,       // 他行支取
        Other           // 其他
    }
}

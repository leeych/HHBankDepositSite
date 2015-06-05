using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PeriodDescriptor
    {
        /// <summary>
        /// 天数
        /// </summary>
        public int D01 { get; set; }

        /// <summary>
        /// 存期三个月的数目
        /// </summary>
        public int M03 { get; set; }

        /// <summary>
        /// 存期为六个月的数目
        /// </summary>
        public int M06 { get; set; }

        /// <summary>
        /// 存期为1年的数目
        /// </summary>
        public int Y01 { get; set; }

        /// <summary>
        /// 存期为2年的数目
        /// </summary>
        public int Y02 { get; set; }

        /// <summary>
        /// 存期为3年的数目
        /// </summary>
        public int Y03 { get; set; }

        /// <summary>
        /// 存期为5年的数目
        /// </summary>
        public int Y05 { get; set; }

        public void Reset()
        {
            D01 = 0;
            M03 = 0;
            M06 = 0;
            Y01 = 0;
            Y02 = 0;
            Y03 = 0; 
            Y05 = 0;
        }

        public override string ToString()
        {
            string desc = "";
            if (Y05 != 0)
            {
                desc += Y05 + "*Y05 ";
            }
            if (Y03 != 0)
            {
                desc += Y03 + "*Y03 ";
            }
            if (Y02 != 0)
            {
                desc += Y02 + "*Y02 ";
            }
            if (Y01 != 0)
            {
                desc += Y01 + "*Y01 ";
            }
            if (M06 != 0)
            {
                desc += M06 + "*M06 ";
            }
            if (M03 != 0)
            {
                desc += M03 + "*M03 ";
            }
            if (D01 != 0)
            {
                desc += D01 + "*D01";
            }
            return desc;
        }
    }
}

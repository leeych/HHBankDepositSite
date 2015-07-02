using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHBankDepositSite
{
    public class DataPagerInfo
    {
        /// <summary>
        /// 每页显示的记录条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总的记录数
        /// </summary>
        public int RecordsCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PagesCount { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrengPage { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// 跳转页码
        /// </summary>
        public int JumpPage { get; set; }
    }
}

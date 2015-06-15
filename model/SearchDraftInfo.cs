using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SearchDraftInfo
    {
        /// <summary>
        /// 机构号
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 最大协议编号
        /// </summary>
        public string MaxProtocolId { get; set; }

        /// <summary>
        /// 截至目前总笔数
        /// </summary>
        public long RecordCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class UserInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 机构号
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public UserRole Role { get; set; }
    }
}

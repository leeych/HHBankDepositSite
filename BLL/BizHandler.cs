using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BLL
{
    public sealed class BizHandler
    {
        private static BizHandler instance = null;
        private static readonly object padLock = new object();

        private BizHandler() { }

        public static BizHandler Handler
        {
            get
            {
                lock (padLock)
                {
                    if (instance == null)
                    {
                        instance = new BizHandler();
                    }
                    return instance;
                }
            }
        }

        private DBHandler dbHandler = new DBHandler();

        public DrawRecord DrawRecordInfo { get; set; }

        /// <summary>
        /// 读取.xml利率表中的利率
        /// </summary>
        /// <param name="fileName">利率表所在路径</param>
        /// <returns></returns>
        public BankRate GetBankRateTable(string fileName)
        {
            BankRate rate = new BankRate();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            string xpath = "/" + BankRateNode.BankRate + "/" + BankRateNode.ExecRate + "/";
            XmlNode node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Current);
            rate.CurrRate = decimal.Parse(node.InnerText.Trim()) / 100;
            rate.D01 = decimal.Parse(node.InnerText.Trim()) / 360 / 100;
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Month03);
            rate.M03 = decimal.Parse(node.InnerText.Trim()) / 100;
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Month06);
            rate.M06 = decimal.Parse(node.InnerText.Trim()) / 100;
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Year01);
            rate.Y01 = decimal.Parse(node.InnerText.Trim()) / 100;
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Year02);
            rate.Y02 = decimal.Parse(node.InnerText.Trim()) / 100;
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Year03);
            rate.Y03 = decimal.Parse(node.InnerText.Trim()) / 100;
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Year05);
            rate.Y05 = decimal.Parse(node.InnerText.Trim()) / 100;
            return rate;
        }

        /// <summary>
        /// 判断用户名和密码是否在数据库里
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回是否在数据库里</returns>
        public bool IsUserInDB(string userName, string password)
        {
            DBHandler dbHandler = new DBHandler();
            int count = dbHandler.GetUserCount(userName, password);
            if (count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 确定userName是否在数据库中 
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回用户名是否在数据库中</returns>
        public bool IsUserNameExits(string userName)
        {
            DBHandler dbHandler = new DBHandler();
            UserInfo userInfo = dbHandler.GetUserInfo(userName);
            if (userInfo != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否是管理员
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool IsAdminUser(string userName, string password)
        {
            UserInfo user = dbHandler.GetUserInfo(userName, password);
            if (user.Role == UserRole.Admin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户名和密码获取数据库中的用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(string userName, string password)
        {
            return dbHandler.GetUserInfo(userName, password);
        }

        /// <summary>
        /// 数据库中增加一条存款记录
        /// </summary>
        /// <param name="record">存款记录</param>
        /// <returns>返回影响的行数</returns>
        public int AddDepositRecord(DepositRecord record)
        {
            if (string.IsNullOrEmpty(record.OrgCode))
            {
                return -2;
            }
            int rows = dbHandler.AddDepositRecord(record, record.OrgCode);
            return rows;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="oldPwd">老密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns>返回影响的行数</returns>
        public int ChangePassword(string userName, string oldPwd, string newPwd)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd))
            {
                return -2;
            }
            int rows = dbHandler.ChangePassword(userName, oldPwd, newPwd);
            return rows;
        }

        /// <summary>
        /// 获得机构信息
        /// </summary>
        /// <param name="orgCode">机构号</param>
        /// <returns>机构信息</returns>
        public OrgInfo GetOrgInfo(string orgCode)
        {
            if (string.IsNullOrEmpty(orgCode))
            {
                return null;
            }
            OrgInfo orgInfo = dbHandler.GetOrgInfoByOrgCode(orgCode);
            return orgInfo;
        }

        /// <summary>
        /// 根据协议号、存单账号、凭证号查询存款记录
        /// </summary>
        /// <param name="protocolId"></param>
        /// <param name="billAccount"></param>
        /// <param name="billCode"></param>
        /// <param name="orgCode"></param>
        /// <returns>保利存存款记录</returns>
        public DepositRecord GetDepositRecord(string protocolId, string billAccount, string billCode, string orgCode)
        {
            if (string.IsNullOrEmpty(protocolId) || string.IsNullOrEmpty(billAccount) || string.IsNullOrEmpty(billCode))
            {
                return null;
            }
            DepositRecord record = dbHandler.GetRecordByProtocolIdAccountCode(protocolId, billAccount, billCode, orgCode);
            return record;
        }

        public DrawRecord GetDrawRecord(string protocolId, string account, string orgCode)
        {
            if (string.IsNullOrEmpty(protocolId) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(orgCode))
            {
                return null;
            }
            DrawRecordInfo = dbHandler.GetDrawRecordByProtocolIdAccountCode(protocolId, account, orgCode);
            return DrawRecordInfo;
        }

        public bool DrawDepositRecord(DrawInfo info, string orgCode)
        {
            if (string.IsNullOrEmpty(orgCode))
            {
                return false;
            }
            bool res = dbHandler.FirstDrawRecord(info, orgCode);
            return res;
        }

        public bool FinalDrawDepositRecord(DrawInfo info, string orgCode)
        {
            if (string.IsNullOrEmpty(orgCode))
            {
                return false;
            }
            bool res = dbHandler.FinalDrawDepositRecord(info, orgCode);
            return res;
        }

        public string GetMaxProtocolId(string orgCode)
        {
            if (string.IsNullOrEmpty(orgCode))
            {
                return string.Empty;
            }
            return dbHandler.GetMaxProtocolID(orgCode);
        }

        public SearchDraftInfo GetDraftSearchInfo(string orgCode)
        {
            if (string.IsNullOrEmpty(orgCode))
            {
                return null;
            }
            return dbHandler.GetDraftSearchInfo(orgCode);
        }
    }
}

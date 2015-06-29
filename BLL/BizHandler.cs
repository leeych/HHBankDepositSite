using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
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

        public bool SetNewBankRateTable(string fileName, BankRate rate)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            string xpath = "/" + BankRateNode.BankRate + "/" + BankRateNode.ExecRate + "/";
            XmlNode node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Current);
            node.InnerText = (rate.CurrRate * 100).ToString();
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Month03);
            node.InnerText = (rate.M03 * 100).ToString();
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Month06);
            node.InnerText = (rate.M06 * 100).ToString();
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Year01);
            node.InnerText = (rate.Y01 * 100).ToString();
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Year02);
            node.InnerText = (rate.Y02 * 100).ToString();
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Year03);
            node.InnerText = (rate.Y03 * 100).ToString();
            node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Year05);
            node.InnerText = (rate.Y05 * 100).ToString();
            xmlDoc.Save(fileName);
            return true;
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

        public string GetOrgNameByOrgCode(string orgCode)
        {
            if (string.IsNullOrEmpty(orgCode))
            {
                return null;
            }
            return dbHandler.GetOrgName(orgCode);
        }

        public SearchInfo SearchRecordByProtocolID(string protocolID, string orgCode)
        {
            if (string.IsNullOrEmpty(protocolID))
            {
                return null;
            }
            return dbHandler.GetSearchRecordByProtocolID(protocolID, orgCode);
        }

        public List<SearchInfo> SearchRecordByIDCard(string idCard, string orgCode)
        {
            if (string.IsNullOrEmpty(idCard))
            {
                return null;
            }
            return dbHandler.GetSearchRecordByIDCard(idCard, orgCode);
        }

        public SearchInfo SearchRecordByBillAccount(string account, string orgCode)
        {
            if (string.IsNullOrEmpty(account))
            {
                return null;
            }
            return dbHandler.GetSearchRecordByBillAccount(account, orgCode);
        }

        public List<SearchInfo> SearchRecordByDuration(DateTime start, DateTime end, string orgCode)
        {
            if (start.Date > end.Date)
            {
                return new List<SearchInfo>();
            }
            if (orgCode == "3404151476")
            {
                return dbHandler.GetAllOrgRecord(start, end);
            }
            return dbHandler.GetSearchRecordByDuration(start, end, orgCode);
        }

        public List<TellerInfo> GetTellerInfoListByOrgCode(string orgCode)
        {
            if (string.IsNullOrEmpty(orgCode))
            {
                return null;
            }
            return dbHandler.GetTellerInfoList(orgCode);
        }

        public bool ChangeTellerOrg(TellerInfo teller)
        {
            return dbHandler.ChangeTellerOrg(teller);
        }

        public bool AddTellerInfo(TellerInfo teller)
        {
            return dbHandler.AddTellerInfo(teller);
        }

        public bool ResetUserPassword(string userName, string password, string orgCode)
        {
            return dbHandler.ResetUserPassword(userName, password, orgCode);
        }

        public List<BankRateInfo> GetBankRateInfoList()
        {
            return dbHandler.GetAllBankRate();
        }

        public List<OrgInfo> GetAllOrgInfoList()
        {
            List<OrgInfo> orgList = dbHandler.GetOrgInfoList();
            if (orgList == null || orgList.Count == 0)
            {
                return null;
            }
            return orgList;
        }

        public List<TellerInfo> GetAllTellerInfoList()
        {
            List<TellerInfo> tellerList = dbHandler.GetAllTellerInfo();
            if (tellerList == null || tellerList.Count == 0)
            {
                return null;
            }
            return tellerList;
        }

        public List<BankRateInfo> GetAllBankRateInfo()
        {
            return dbHandler.GetAllBankRateInfoList();
        }

        public bool AddBankRateInfo(BankRateInfo info)
        {
            return dbHandler.AddBankRateInfo(info);
        }

        public bool AddNewTeller(string tellerCode, string tellerName, string orgCode)
        {
            return dbHandler.AddNewTeller(tellerCode, tellerName, orgCode);
        }

        public bool ChangeTellerOrg(string tellerCode, string tellerName, string orgCode)
        {
            return dbHandler.ChangeTellerOrg(tellerCode, tellerName, orgCode);
        }

        public List<SearchInfo> GetRecord(string orgCode, DateTime start, DateTime end)
        {
            if (orgCode == "3404151476")
            {
                List<SearchInfo> list = dbHandler.GetAllOrgRecord(start, end);
                return list;
            }
            List<SearchInfo> recordList = dbHandler.GetSearchRecordByDuration(start, end, orgCode);
            return recordList;
        }

        public static List<ExcelRecordInfo> GenExcelRecordInfoList(List<SearchInfo> sinfoList)
        {
            List<ExcelRecordInfo> excelList = new List<ExcelRecordInfo>();
            for (int i = 0; i < sinfoList.Count; i++)
            {
                ExcelRecordInfo record = new ExcelRecordInfo();
                record.ProtocolID = sinfoList[i].ProtocolID;
                record.BillAccount = sinfoList[i].BillAccount;
                record.BillCode = sinfoList[i].BillCode;
                record.DepositDate = sinfoList[i].DepositDate;
                record.DepositMoney = sinfoList[i].DepositMoney;
                record.BillPeriod = GetBillPeriodDesc(sinfoList[i].BillPeriod);
                record.ClientName = sinfoList[i].ClientName;
                record.ClientIDCard = sinfoList[i].ClientID;
                record.DepositStatus = GetDepositStatusDesc(sinfoList[i].Status);
                record.BindAccount = sinfoList[i].BindAccount;
                record.DueDate = CalcDueDate(sinfoList[i].DepositDate, sinfoList[i].BillPeriod);
                record.TellerCode = sinfoList[i].TellerCode;

                record.Y05Rate = sinfoList[i].ExecRate.Y05;
                record.Y03Rate = sinfoList[i].ExecRate.Y03;
                record.Y02Rate = sinfoList[i].ExecRate.Y02;
                record.Y01Rate = sinfoList[i].ExecRate.Y01;
                record.M06Rate = sinfoList[i].ExecRate.M06;
                record.M03Rate = sinfoList[i].ExecRate.M03;
                record.CurrRate = sinfoList[i].ExecRate.CurrRate;

                if (sinfoList[i].Status == DrawFlag.Remain)
                {
                    record.RemainMoney = (sinfoList[i].DepositMoney - sinfoList[i].FirstDrawMoney);
                }
                else if (sinfoList[i].Status == DrawFlag.Draw)
                {
                    record.RemainMoney = decimal.Zero;
                }
                else if (sinfoList[i].Status == DrawFlag.Deposit)
                {
                    record.RemainMoney = sinfoList[i].DepositMoney;
                }

                if (sinfoList[i].FirstDrawDate == DateTime.MaxValue)
                {
                    record.FirstDrawDate = "NULL";
                    record.FirstDrawMoney = "NULL";
                    record.FirstSysInterest = "NULL";
                    record.FirstCalcInterest = "NULL";
                    record.FirstMarginInterest = "NULL";
                }
                else
                {
                    record.FirstDrawDate = sinfoList[i].FirstDrawDate.ToString("yyyy-MM-dd");
                    record.FirstDrawMoney = sinfoList[i].FirstDrawMoney.ToString("f2");
                    record.FirstSysInterest = sinfoList[i].FirstSysInterest.ToString("f2");
                    record.FirstCalcInterest = sinfoList[i].FirstCalcInterest.ToString("f2");
                    record.FirstMarginInterest = sinfoList[i].FirstMarginInterest.ToString("f2");
                }

                if (sinfoList[i].FinalDrawDate == DateTime.MaxValue)
                {
                    record.FinalDrawDate = "NULL";
                    record.FinalDrawMoney = "NULL";
                    record.FinalSysInterest = "NULL";
                    record.FinalCalcInterest = "NULL";
                    record.FinalMarginInterest = "NULL";
                }
                else
                {
                    record.FinalDrawDate = sinfoList[i].FirstDrawDate.ToString("yyyy-MM-dd");
                    record.FinalDrawMoney = sinfoList[i].FinalDrawMoney.ToString("f2");
                    record.FinalSysInterest = sinfoList[i].FinalSysInterest.ToString("f2");
                    record.FinalCalcInterest = sinfoList[i].FinalCalcInterest.ToString("f2");
                    record.FinalMarginInterest = sinfoList[i].FinalMarginInterest.ToString("f2");
                }
                excelList.Add(record);
            }
            return excelList;
        }

        public static AdminSketchInfo GetSummaryInfo(List<SearchInfo> infoList)
        {
            AdminSketchInfo sumInfo = new AdminSketchInfo()
                                            {
                                                NewRecord = new SumSketch(),
                                                DRecord = new SumSketch(),
                                                AdDrawRecord = new SumSketch(),
                                                DueDrawRecord = new SumSketch(),
                                                RemainRecord = new SumSketch(),
                                                SysPayfee = new SumSketch(),
                                                CalcPayfee = new SumSketch(),
                                                MarginPayfee = new SumSketch()
                                            };
            for (int i = 0; i < infoList.Count; i++)
            {
                if (IsDeposit(infoList[i]))
                {
                    sumInfo.NewRecord.Num += 1;
                    sumInfo.NewRecord.Money += infoList[i].DepositMoney;
                }
                if (IsRemain(infoList[i]))
                {
                    sumInfo.RemainRecord.Num += 1;
                    sumInfo.RemainRecord.Money += (infoList[i].DepositMoney - infoList[i].FirstDrawMoney);
                    sumInfo.AdDrawRecord.Num += 1;
                    sumInfo.AdDrawRecord.Money += infoList[i].FirstDrawMoney;
                    sumInfo.SysPayfee.Num += 1;
                    sumInfo.SysPayfee.Money += infoList[i].FirstSysInterest;
                    sumInfo.CalcPayfee.Num += 1;
                    sumInfo.CalcPayfee.Money += infoList[i].FirstCalcInterest;
                    sumInfo.MarginPayfee.Num += 1;
                    sumInfo.MarginPayfee.Money += infoList[i].FirstMarginInterest;
                }
                if (IsDrawAll(infoList[i]))
                {
                    sumInfo.DRecord.Num += 1;
                    sumInfo.DRecord.Money += infoList[i].FinalDrawMoney;
                    sumInfo.SysPayfee.Num += 1;
                    sumInfo.SysPayfee.Money += infoList[i].FinalSysInterest;
                    sumInfo.CalcPayfee.Num += 1;
                    sumInfo.CalcPayfee.Money += infoList[i].FinalCalcInterest;
                    sumInfo.MarginPayfee.Num += 1;
                    sumInfo.MarginPayfee.Money += infoList[i].FinalMarginInterest;
                    if (IsDueDraw(infoList[i]))
                    {
                        sumInfo.DueDrawRecord.Num += 1;
                        sumInfo.DueDrawRecord.Money += infoList[i].FinalDrawMoney;
                    }
                }
            }
            return sumInfo;
        }

        private static bool IsEarlierDraw(DateTime agreed, DateTime actual)
        {
            return (actual >= agreed);
        }

        private static bool IsRemain(SearchInfo info)
        {
            if (info.Status == DrawFlag.Remain)
            {
                return true;
            }
            return false;
        }

        private static bool IsDrawAll(SearchInfo info)
        {
            if (info.Status == DrawFlag.Draw)
            {
                return true;
            }
            return false;
        }

        private static bool IsDeposit(SearchInfo info)
        {
            if (info.Status == DrawFlag.Deposit)
            {
                return true;
            }
            return false;
        }

        private static bool IsDueDraw(SearchInfo info)
        {
            if (info.Status == DrawFlag.Draw)
            {
                if (info.FinalDrawDate >= CalcDueDate(info.DepositDate, info.BillPeriod))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private static DateTime CalcDueDate(DateTime depositDate, Period period)
        {
            DateTime dt = depositDate;
            switch (period)
            {
                case Period.M03:
                    dt = depositDate.AddMonths(3);
                    break;
                case Period.M06:
                    dt = depositDate.AddMonths(6);
                    break;
                case Period.Y01:
                    dt = depositDate.AddYears(1);
                    break;
                case Period.Y02:
                    dt = depositDate.AddYears(2);
                    break;
                case Period.Y03:
                    dt = depositDate.AddYears(3);
                    break;
                case Period.Y05:
                    dt = depositDate.AddYears(5);
                    break;
                default:
                    break;
            }
            return dt;
        }

        public static string GetDepositStatusDesc(DrawFlag flag)
        {
            string desc = "--";
            switch (flag)
            {
                case DrawFlag.Deposit:
                    desc = "存入未支取";
                    break;
                case DrawFlag.Draw:
                    desc = "已全部支取";
                    break;
                case DrawFlag.Remain:
                    desc = "部分提前支取";
                    break;
                default:
                    desc = "未知";
                    break;
            }
            return desc;
        }

        public static string GetBillPeriodDesc(Period period)
        {
            string desc = string.Empty;
            switch (period)
            {
                case Period.M03:
                    desc = "三个月";
                    break;
                case Period.M06:
                    desc = "六个月";
                    break;
                case Period.Y01:
                    desc = "一年";
                    break;
                case Period.Y02:
                    desc = "二年";
                    break;
                case Period.Y03:
                    desc = "三年";
                    break;
                case Period.Y05:
                    desc = "五年";
                    break;
                default:
                    desc = "--";
                    break;
            }
            return desc;
        }

        public static decimal GetExecRate(Period period, BankRate rate)
        {
            decimal r = 0;
            switch (period)
            {
                case Period.M03:
                    r = rate.M03;
                    break;
                case Period.M06:
                    r = rate.M06;
                    break;
                case Period.Y01:
                    r = rate.Y01;
                    break;
                case Period.Y02:
                    r = rate.Y02;
                    break;
                case Period.Y03:
                    r = rate.Y03;
                    break;
                case Period.Y05:
                    r = rate.Y05;
                    break;
                default:
                    break;
            }
            return r;
        }
    }
}

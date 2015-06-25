using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Web;

namespace HHBankDepositSite.Data
{
    public class WebDataCenter
    {
        public static List<OrgInfo> OrgList 
        {
            get { return BizHandler.Handler.GetAllOrgInfoList(); }
            set { }
        }

        public static Dictionary<string, string> OrgDict 
        {
            get 
            { 
                List<OrgInfo> orgList = BizHandler.Handler.GetAllOrgInfoList();
                Dictionary<string, string> orgDict = new Dictionary<string, string>();
                for (int i = 0; i < orgList.Count; i++)
                {
                    orgDict.Add(orgList[i].OrgName, orgList[i].OrgCode);
                }
                return orgDict;
            }
            set { }
        }

        public static List<TellerInfo> TellerList
        {
            get { return BizHandler.Handler.GetAllTellerInfoList(); }
            set { }
        }

        public static Dictionary<string, TellerInfo> TellerDict
        {
            get
            {
                List<TellerInfo> tellerList = TellerList;
                Dictionary<string, TellerInfo> tellerDict = new Dictionary<string, TellerInfo>();
                for (int i = 0; i < tellerList.Count; i++)
                {
                    tellerDict.Add(tellerList[i].TellerCode, tellerList[i]);
                }
                return tellerDict;
            }
            set { }
        }

        public static List<BankRateInfo> BankRateInfoList
        {
            get 
            {
                return BizHandler.Handler.GetAllBankRateInfo();
            }
            set { }
        }
    }
}
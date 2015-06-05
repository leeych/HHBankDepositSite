using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BLL
{
    public class BizHandler
    {
        /// <summary>
        /// 读取.xml利率表中的利率
        /// </summary>
        /// <param name="fileName">利率表所在路径</param>
        /// <returns></returns>
        public static BankRate GetBankRateTable(string fileName)
        {
            BankRate rate = new BankRate();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            string xpath = "/" + BankRateNode.BankRate + "/" + BankRateNode.ExecRate + "/";
            XmlNode node = xmlDoc.SelectSingleNode(xpath + "/" + BankRateNode.Current);
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
        /// 判断是否是管理员
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool IsAdminUser(string userName, string password)
        {
            DBHandler handler = new DBHandler();
            UserInfo user = handler.GetUserInfo(userName, password);
            if (user.Role == UserRole.Admin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}

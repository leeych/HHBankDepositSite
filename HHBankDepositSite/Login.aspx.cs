using HHBankDepositSite;
using BLL;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace HHBankDepositSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            userNameTxt.Text = string.Empty;
            pwdTxt.Text = string.Empty;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                Session["UserName"] = null;
            }
            string userName = userNameTxt.Text.Trim();
            string password = pwdTxt.Text.Trim();
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                BizHandler handler = new BizHandler();
                if (handler.IsUserInDB(userName, password))
                {
                    Response.Write("<script language='javascript'>alert('登录成功!');</script>");
                    Session["UserName"] = userName;
                    Session["Password"] = password;
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('登录失败!');</script>");
                }
            }
            else
            {
                Response.Write("<script language='javascript'>alert('用户名和密码不能空！');</script>");
            }
        }

        protected void testBtn_Click(object sender, EventArgs e)
        {
            DBHandler handler = new DBHandler();
            List<DepositRecord> recordList = handler.GetRecordByAccount("10003638572510200000141", "3404157871");
            foreach (DepositRecord record in recordList)
            {
                Response.Write(record);
            }
        }
    }
}
using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;

namespace HHBankDepositSite
{
    public partial class Login2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                Session["UserName"] = null;
            }

            string userName = userNameTxt.Text.Trim();
            string password = passwordTxt.Text.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                Response.Write("<script language='javascript'>alert('用户名不能为空！')</script>");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                Response.Write("<script language='javascript'>alert('密码不能为空！')</script>");
                return;
            }

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                if (!BizHandler.Handler.IsUserInDB(userName, password))
                {
                    Response.Write("<script language='javascript'>alert('用户名不存在！')</script>");
                }
                else if (BizHandler.Handler.IsAdminUser(userName, password))
                {
                    Session["UserName"] = userName;
                    Session["Password"] = password;
                    Response.Redirect("~/Admin/Administor.aspx");
                }
                else
                {
                    Session["UserName"] = userName;
                    Session["Password"] = password;
                    Response.Redirect("Default.aspx");
                }
            }
        }
    }
}
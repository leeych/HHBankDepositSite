using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;
using Common;

namespace HHBankDepositSite
{
    public partial class Login : System.Web.UI.Page
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
                //Response.Write("<script>function window.onload() { alert('用户名不能为空！');}</script>");
                TMessageBox.ShowMsg(this, "UserNameEmpty", "请输入用户名！");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                TMessageBox.ShowMsg(this, "OldPwdEmpty", "请输入密码！");
                return;
            }
            if (!PageValidator.IsDigitAlpha(userName))
            {
                TMessageBox.ShowMsg(this, "UserNameInvalid", "用户名只能包含数字和字母！");
                return;
            }
            if (!PageValidator.IsDigitAlpha(password))
            {
                TMessageBox.ShowMsg(this, "OldPwdInvalid", "密码只能包含数字和字母！");
                return;
            }

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                if (!BizHandler.Handler.IsUserNameExits(userName))
                {
                    TMessageBox.ShowMsg(this, "UserNameNotExists", "用户名不存在！");
                    return;
                }
                if (!BizHandler.Handler.IsUserInDB(userName, password))
                {
                    TMessageBox.Show(this, "PwdNotExists", "密码不正确！");
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
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
    }
}
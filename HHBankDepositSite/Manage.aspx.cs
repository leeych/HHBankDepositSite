using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;
using Common;

namespace HHBankDepositSite
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                userNameTxt.Text = Session["UserName"].ToString();
            }
        }

        protected void okBtn_Click(object sender, EventArgs e)
        {
            string userName = userNameTxt.Text.Trim();
            string password = oldpwdTxt.Text.Trim();
            string newpwd = newpwdTxt.Text.Trim();
            string surepwd = surepwdTxt.Text.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                TMessageBox.ShowMsg(this, "UserNameEmpty", "请输入用户名！");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                TMessageBox.Show(this, "OldPwdEmpty", "请输入原密码！");
                return;
            }
            if (string.IsNullOrEmpty(newpwd))
            {
                TMessageBox.Show(this, "NewPwdEmpty", "请输入新密码！");
                return;
            }
            if (string.IsNullOrEmpty(surepwd))
            {
                TMessageBox.Show(this, "SurePwdEmpty", "请确认新密码！");
                return;
            }
            if (newpwd != surepwd)
            {
                TMessageBox.Show(this, "NewPwdNotSame", "新密码两次输入不一致！");
                surepwdTxt.Text = string.Empty;
            }
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (BizHandler.Handler.ChangePassword(userName, password, newpwd) == 1)
            {
                TMessageBox.ShowMsg(this, "ChangePwd", "密码修改成功！");
                return;
            }
            else
            {
                TMessageBox.ShowMsg(this, "OldPwdError", "原密码错误！");
            }
            TextBoxDataBind();
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Session["Password"] = null;
                Response.Redirect("~/Login.aspx");
            }
            ClearTextBox();
            TextBoxDataBind();
        }

        private void TextBoxDataBind()
        {
            userNameTxt.DataBind();
            oldpwdTxt.DataBind();
            newpwdTxt.DataBind();
            surepwdTxt.DataBind();
        }

        private void ClearTextBox()
        {
            //userNameTxt.Text = string.Empty;
            oldpwdTxt.Text = string.Empty;
            newpwdTxt.Text = string.Empty;
            surepwdTxt.Text = string.Empty;
        }
    }
}
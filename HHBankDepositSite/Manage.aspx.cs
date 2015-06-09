using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;

namespace HHBankDepositSite
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxDataBind();
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
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
                Response.Write("<script language='javascript'>alert('请输入用户名！')</script>");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                Response.Write("<script language='javascript'>alert('请输入原密码！')</script>");
                return;
            }
            if (string.IsNullOrEmpty(newpwd))
            {
                Response.Write("<script language='javascript'>alert('请输入新密码！')</script>");
                return;
            }
            if (string.IsNullOrEmpty(surepwd))
            {
                Response.Write("<script language='javascript'>alert('请输入确认密码！')</script>");
                return;
            }
            if (newpwd != surepwd)
            {
                Response.Write("<script language='javascript'>alert('新密码两次输入不一致！')</script>");
                surepwdTxt.Text = string.Empty;
            }
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (BizHandler.Handler.ChangePassword(userName, password, newpwd) == 1)
            {
                Response.Write("<script language='javascript'>alert('密码修改成功！')</script>");
                return;
            }
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Session["Password"] = null;
                Response.Redirect("~/Login.aspx");
            }
            ClearTextBox();
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
            userNameTxt.Text = string.Empty;
            oldpwdTxt.Text = string.Empty;
            newpwdTxt.Text = string.Empty;
            surepwdTxt.Text = string.Empty;
        }
    }
}
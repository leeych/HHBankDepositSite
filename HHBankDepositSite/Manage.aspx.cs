using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL;
using Common;
using Model;
using HHBankDepositSite.Data;

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
            if (!IsPostBack)
            {
                string fileName = ConfigUtil.GetValue(WebConfigName.BankRateTable, "");
                fileName += Session["UserName"].ToString() + ".xml";
                LoadBankRate(fileName);
            }
        }

        private void LoadBankRate(string fileName)
        {
            BankRate rate = BizHandler.Handler.GetBankRateTable(fileName);
            currentRateTxt.Text = (rate.CurrRate * 100).ToString("f3");
            m03RateTxt.Text = (rate.M03 * 100).ToString("f3");
            m06RateTxt.Text = (rate.M06 * 100).ToString("f3");
            y01RateTxt.Text = (rate.Y01 * 100).ToString("f3");
            y02RateTxt.Text = (rate.Y02 * 100).ToString("f3");
            y03RateTxt.Text = (rate.Y03 * 100).ToString("f3");
            y05RateTxt.Text = (rate.Y05 * 100).ToString("f3");
        }

        protected void okBtn_Click(object sender, EventArgs e)
        {
            string userName = userNameTxt.Text.Trim();
            string password = oldpwdTxt.Text.Trim();
            string newpwd = newpwdTxt.Text.Trim();
            string surepwd = surepwdTxt.Text.Trim();

            if (!PageValidator.IsDigitAlpha(password))
            {
                TMessageBox.ShowMsg(this, "OldPwdNotValid", "密码只能包含数字和字母！");
            }
            if (!PageValidator.IsDigitAlpha(newpwd))
            {
                TMessageBox.ShowMsg(this, "NewPwdNotValid", "新密码只能包含数字和字母！");
                return;
            }
            if (newpwd != surepwd)
            {
                TMessageBox.Show(this, "NewPwdNotSame", "新密码两次输入不一致！");
                surepwdTxt.Text = string.Empty;
                return;
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

        protected void changeRateBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                TMessageBox.ShowMsg(this, "ChangeRateLoginExpired", "登录超时，请重新登录！");
                return;
            }
            BankRate rate = new BankRate();
            rate.CurrRate = decimal.Parse(currentRateTxt.Text.Trim()) / 100;
            rate.M03 = decimal.Parse(m03RateTxt.Text.Trim()) / 100;
            rate.M06 = decimal.Parse(m06RateTxt.Text.Trim()) / 100;
            rate.Y01 = decimal.Parse(y01RateTxt.Text.Trim()) / 100;
            rate.Y02 = decimal.Parse(y02RateTxt.Text.Trim()) / 100;
            rate.Y03 = decimal.Parse(y03RateTxt.Text.Trim()) / 100;
            rate.Y05 = decimal.Parse(y05RateTxt.Text.Trim()) / 100;

            string orgCode = Session["UserName"].ToString();
            string fileName = ConfigUtil.GetValue(WebConfigName.BankRateTable, "");
            fileName += orgCode + ".xml";
            if (BizHandler.Handler.SetNewBankRateTable(fileName, rate))
            {
                TMessageBox.ShowMsg(this, "SetBankRateSuccess", "利率修改成功！");
                return;
            }
            else
            {
                TMessageBox.ShowMsg(this, "SetBankRateFailed", "利率修改失败！");
                return;
            }
        }
    }
}
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
                string orgCode = Session["UserName"].ToString();
                string fileName = ConfigUtil.GetValue(WebConfigName.BankRateTable, "");
                fileName += orgCode + ".xml";
                LoadBankRate(fileName);
                periodDrop.SelectedIndex = 0;
                periodDrop_SelectedIndexChanged(sender, e);
                //GenTellerCodeDrop(orgCode);
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

        protected void recordBtn_Click(object sender, EventArgs e)
        {
            SearchInfo info = new SearchInfo();
            info.ProtocolID = protocolIDTxt.Text.Trim();
            info.BillCode = billCodeTxt.Text.Trim();
            info.DepositDate = DateTime.Parse(dateTxt.Text.Trim());
            info.DepositMoney = decimal.Parse(moneyTxt.Text.Trim());

            info.ExecRate = GetCurrentBankRate();
            info.BillPeriod = GetBillPeriodDrop();
            info.ClientID = clientIDTxt.Text.Trim();
            info.ClientName = clientNameTxt.Text.Trim();
            info.TellerCode = tellerCodeTxt.Text.Trim();
            info.ClientName = tellerNameTxt.Text.Trim();
            info.Status = GetRecordStatus();
            info.BillAccount = billAccountTxt.Text.Trim();
            if (info.Status == DrawFlag.ElseDraw || info.Status == DrawFlag.Draw)
            {
                info.FinalDrawDate = DateTime.Parse(drawDateTxt.Text.Trim());
            }
            else
            {
                info.FinalDrawDate = DateTime.MaxValue;
            }
            info.BindAccount = bindAccountTxt.Text.Trim();
            if (!ValidateRecordParam())
            {
                return;
            }
            if (BizHandler.Handler.ModifyRecord(info, Session["UserName"].ToString()))
            {
                TMessageBox.ShowMsg(this, "MnRecordModifySuccess", "存款记录修改成功！");
                return;
            }
            else
            {
                TMessageBox.ShowMsg(this, "MnRecordModifyFailed", "存款记录修改失败！请确认输入无误后再试！");
                return;
            }
        }

        private bool ValidateRecordParam()
        {
            string errMsg = string.Empty;
            string orgCode = Session["UserName"].ToString();
            if (!BizValidator.CheckProtocolID(orgCode, protocolIDTxt.Text.Trim()))
            {
                string str = @"协议编号格式不对！其编码规则为：{0}{1}+6位顺序号！\n";
                errMsg += string.Format(str, orgCode.Substring(6), DateTime.Now.Year.ToString());
            }
            if (!BizValidator.CheckBillAccount(billAccountTxt.Text.Trim()))
            {
                errMsg += @"存单账号错误！\n";
            }
            if (!BizValidator.CheckBillCode(billCodeTxt.Text.Trim()))
            {
                errMsg += @"凭证号码必须是以“50”开始的12位数字！\n";
            }
            if (!PageValidator.IsHasCHZN(clientNameTxt.Text.Trim()))
            {
                errMsg += @"客户姓名必须含有汉字！\n";
            }
            if (!BizValidator.CheckIDCard(clientIDTxt.Text.Trim()))
            {
                errMsg += @"客户身份证号码非法！\n";
            }
            if (string.IsNullOrEmpty(errMsg))
            {
                return true;
            }
            return false;
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            string orgCode = Session["UserName"].ToString();
            string protocolId = protocolIDTxt.Text.Trim();
            string billAccount = billAccountTxt.Text.Trim();
            if (!BizValidator.CheckProtocolID(orgCode, protocolId))
            {
                TMessageBox.ShowMsg(this, "MnProtocolIDInvalid", "协议编号格式不对！");
                return;
            }
            SearchInfo info = BizHandler.Handler.SearchRecordByProtocolID(protocolId, orgCode);
            if (info == null)
            {
                TMessageBox.ShowMsg(this, "MnCannotFind", "找不到这笔记录！");
                return;
            }
            // TODO: left to be done.
            UpdatePageInfo(info);
        }

        private void UpdatePageInfo(SearchInfo info)
        {
            billCodeTxt.Text = info.BillCode;
            moneyTxt.Text = info.DepositMoney.ToString("f2");
            dateTxt.Text = info.DepositDate.ToString("yyyy-MM-dd");
            statusDrop.SelectedIndex = (int)info.Status;
            if (info.Status == DrawFlag.Draw || info.Status == DrawFlag.ElseDraw)
            {
                drawDateTxt.Text = info.FinalDrawDate.ToString("yyyy-MM-dd");
            }
            else if (info.Status == DrawFlag.Remain)
            {
                drawDateTxt.Text = info.FirstDrawDate.ToString("yyyy-MM-dd");
            }
            else
            {
                drawDateTxt.Text = "--";
            }

            SetBillPeriodDrop(info.BillPeriod);
            clientNameTxt.Text = info.ClientName;
            clientIDTxt.Text = info.ClientID;
            bindAccountTxt.Text = info.BindAccount;
            tellerCodeTxt.Text = info.TellerCode;
            tellerNameTxt.Text = info.TellerName;
        }

        private BankRate GetCurrentBankRate()
        {
            string fileName = ConfigUtil.GetValue(WebConfigName.BankRateTable, "");
            fileName += Session["UserName"].ToString() + ".xml";
            BankRate rate = BizHandler.Handler.GetBankRateTable(fileName);
            return rate;
        }

        private Period GetBillPeriodDrop()
        {
            Period dp = Period.M03;
            switch (periodDrop.SelectedIndex)
            {
                case 0:
                    dp = Period.M03;
                    break;
                case 1:
                    dp = Period.M06;
                    break;
                case 2:
                    dp = Period.Y01;
                    break;
                case 3:
                    dp = Period.Y02;
                    break;
                case 4:
                    dp = Period.Y03;
                    break;
                case 5:
                    dp = Period.Y05;
                    break;
                default:
                    break;
            }
            return dp;
        }

        private DrawFlag GetRecordStatus()
        {
            DrawFlag state = DrawFlag.Deposit;
            switch (statusDrop.SelectedIndex)
            {
                case 0:
                    state = DrawFlag.Deposit;
                    break;
                case 1:
                    state = DrawFlag.Draw;
                    break;
                case 2:
                    state = DrawFlag.Remain;
                    break;
                case 3:
                    state = DrawFlag.ElseDraw;
                    break;
                default:
                    state = DrawFlag.Other;
                    break;
            }
            return state;
        }

        private void SetBillPeriodDrop(Period period)
        {
            switch (period)
            {
                case Period.M03:
                    periodDrop.SelectedIndex = 0;
                    break;
                case Period.M06:
                    periodDrop.SelectedIndex = 1;
                    break;
                case Period.Y01:
                    periodDrop.SelectedIndex = 2;
                    break;
                case Period.Y02:
                    periodDrop.SelectedIndex = 3;
                    break;
                case Period.Y03:
                    periodDrop.SelectedIndex = 4;
                    break;
                case Period.Y05:
                    periodDrop.SelectedIndex = 5;
                    break;
                default:
                    break;
            }
        }

        protected void periodDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fileName = ConfigUtil.GetValue(WebConfigName.BankRateTable,"");
            fileName += Session["UserName"].ToString() + ".xml";
            BankRate rate = BizHandler.Handler.GetBankRateTable(fileName);
            if (rate == null)
            {
                return;
            }
            switch (periodDrop.SelectedIndex)
            {
                case 0:
                    rateTxt.Text = (rate.M03 * 100).ToString("f3");
                    break;
                case 1:
                    rateTxt.Text = (rate.M06 * 100).ToString("f3");
                    break;
                case 2:
                    rateTxt.Text = (rate.Y01 * 100).ToString("f3");
                    break;
                case 3:
                    rateTxt.Text = (rate.Y02 * 100).ToString("f3");
                    break;
                case 4:
                    rateTxt.Text = (rate.Y03 * 100).ToString("f3");
                    break;
                case 5:
                    rateTxt.Text = (rate.Y05 * 100).ToString("f3");
                    break;
                default:
                    rateTxt.Text = "--";
                    break;
            }
        }
    }
}
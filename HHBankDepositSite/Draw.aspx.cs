using BLL;
using Common;
using HHBankDepositSite.Data;
using Model;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHBankDepositSite
{
    public partial class Draw : System.Web.UI.Page
    {
        private string bankRatePath = ConfigUtil.GetValue(WebConfigName.BankRateTable, "");
        private BankRate bankRate = BizHandler.Handler.GetBankRateTable(ConfigUtil.GetValue(WebConfigName.BankRateTable, ""));

        protected void Page_Load(object sender, EventArgs e)
        {
            periodDrop.SelectedIndex = 2;
            periodDrop_SelectedIndexChanged(sender, e);
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            drawDateTxt.DataBind();
            drawDateTxt.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void calcBtn_Click(object sender, EventArgs e)
        {

        }

        protected void okBtn_Click(object sender, EventArgs e)
        {

        }

        protected void periodDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (periodDrop.SelectedIndex)
            {
                case 0:
                    execRateTxt.Text = (bankRate.M03 * 100).ToString();
                    break;
                case 1:
                    execRateTxt.Text = (bankRate.M06 * 100).ToString();
                    break;
                case 2:
                    execRateTxt.Text = (bankRate.Y01 * 100).ToString();
                    break;
                case 3:
                    execRateTxt.Text = (bankRate.Y02 * 100).ToString();
                    break;
                case 4:
                    execRateTxt.Text = (bankRate.Y03 * 100).ToString();
                    break;
                case 5:
                    execRateTxt.Text = (bankRate.Y05 * 100).ToString();
                    break;
                default:
                    execRateTxt.Text = "--";
                    break;
            }
            execRateTxt.DataBind();
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            string protocolId = protocolIDTxt.Text.Trim();
            string billAccount = billAccountTxt.Text.Trim();
            string billCode = billCodeTxt.Text.Trim();
            if (string.IsNullOrEmpty(protocolId) || string.IsNullOrEmpty(billAccount) || string.IsNullOrEmpty(billCode))
            {
                TMessageBox.ShowMsg(this, "DrawSearch", "请务必输入协议号、存单账号、凭证号！");
                return;
            }
            string errMsg = string.Empty;
            if (!PageValidator.IsNumber(protocolId))
            {
                errMsg += "协议编号必须全部为数字！\n";
            }
            if (!PageValidator.IsNumber(billAccount))
            {
                errMsg += "存单账号必须全部为数字！\n";
            }
            if (billAccount.Length != 23)
            {
                errMsg += "存单账号长度必须为23位！\n";
            }
            if (!PageValidator.IsNumber(billCode))
            {
                errMsg += "凭证号码必须全部为数字！\n";
            }
            if (billCode.Length != 12)
            {
                errMsg += "凭证号码长度必须为12位！";
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                TMessageBox.ShowMsg(this, "PageValidator", errMsg);
                return;
            }
            DrawRecord record = BizHandler.Handler.GetDrawRecord(protocolId, billAccount, billCode, Session["UserName"].ToString());
            if (record == null)
            {
                TMessageBox.ShowMsg(this, "TradeRecordNotExists", "没有满足条件的交易记录！");
                return;
            }
            UpdatePage(record);
        }

        private void UpdatePage(DrawRecord record)
        {
            dueDateTxt.Text = record.DueDate.ToString("yyyy-MM-dd");
            moneyTxt.Text = record.CapticalMoney.ToString();
            clientIDTxt.Text = record.DepositorIDCard;
            clientNameTxt.Text = record.DepositorName;
            tellerCodeTxt.Text = record.TellerCode;
            remarkTxt.Text = record.Remark;
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            bool start = e.Day.Date >= DateTime.Now.Date.AddDays(-1);
            bool end = e.Day.Date <= DateTime.Now.Date.AddDays(1);
            e.Day.IsSelectable = (start && end);
        }
    }
}
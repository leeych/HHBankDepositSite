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
            DepositRecord record = BizHandler.Handler.GetDepositRecord(protocolId, billAccount, billCode, Session["UserName"].ToString());
            
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            bool start = e.Day.Date >= DateTime.Now.Date.AddDays(-1);
            bool end = e.Day.Date <= DateTime.Now.Date.AddDays(1);
            e.Day.IsSelectable = (start && end);
        }
    }
}
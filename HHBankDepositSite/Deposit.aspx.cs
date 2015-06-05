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
    public partial class Deposit : System.Web.UI.Page
    {
        private string bankRatePath = ConfigUtil.GetValue(WebConfigName.BankRateTable, "");

        private BankRate bankRate = BizHandler.Handler.GetBankRateTable(ConfigUtil.GetValue(WebConfigName.BankRateTable, ""));

        protected void Page_Load(object sender, EventArgs e)
        {
            bankRate = BizHandler.Handler.GetBankRateTable(bankRatePath);
            periodDrop_SelectedIndexChanged(sender, e);
        }

        protected void periodDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            rateTxt.DataBind();
            switch (periodDrop.SelectedIndex)
            {
                case 0:
                    rateTxt.Text = (bankRate.M03 * 100).ToString();
                    break;
                case 1:
                    rateTxt.Text = (bankRate.M06 * 100).ToString();
                    break;
                case 2:
                    rateTxt.Text = (bankRate.Y01 * 100).ToString();
                    break;
                case 3:
                    rateTxt.Text = (bankRate.Y02 * 100).ToString();
                    break;
                case 4:
                    rateTxt.Text = (bankRate.Y03 * 100).ToString();
                    break;
                case 5:
                    rateTxt.Text = (bankRate.Y05 * 100).ToString();
                    break;
                default:
                    rateTxt.Text = "--";
                    break;
            }
        }

        protected void rateTxt_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Calendar1_SelectionChanged1(object sender, EventArgs e)
        {
            dateTxt.DataBind();
            dateTxt.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
        }
    }
}
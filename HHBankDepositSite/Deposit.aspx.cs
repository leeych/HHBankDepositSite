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

        protected void depositBtn_Click(object sender, EventArgs e)
        {
            string protocolID = protocolTxt.Text.Trim();
            string billAccount = billAccountTxt.Text.Trim();
            string billCode = billCodeTxt.Text.Trim();
            int period = periodDrop.SelectedIndex;
            decimal rate = decimal.Parse(rateTxt.Text.Trim());
            decimal money = decimal.Parse(moneyTxt.Text.Trim());
            DateTime depositDate = DateTime.Parse(dateTxt.Text.Trim() + "T00:00:00");
            string bindAccount = bindAccountTxt.Text.Trim();
            string orgCode = Session["UserName"].ToString();
            string tellerCode = tellerCodeTxt.Text.Trim();
            string idCard = IDCardTxt.Text.Trim();
            string name = nameTxt.Text.Trim();
            string remark = remarkTxt.Text.Trim();

            DepositRecord record = new DepositRecord 
                                        {
                                            ProtocolID = protocolID,

                                        };
            // TODO: left to be done.
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            // TODO: left to be done.
        }
    }
}
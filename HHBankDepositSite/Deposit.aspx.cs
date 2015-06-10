﻿using BLL;
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
            periodDrop.SelectedIndex = 2;
            periodDrop_SelectedIndexChanged(sender, e);
            dateTxt.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dateTxt.DataBind();
        }

        protected void periodDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            //rateTxt.DataBind();
        }

        protected void rateTxt_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Calendar1_SelectionChanged1(object sender, EventArgs e)
        {
            dateTxt.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void depositBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!ValidatePage())
            {
                TMessageBox.ShowMsg(this, "RequiredEmpty", "标 * 的为必填项！");
                return;
            }
            DateTime t = new DateTime();
            if (!DateTime.TryParse(dateTxt.Text.Trim() + "T00:00:00", out t))
            {
                TMessageBox.ShowMsg(this, "DateError", "存入日期无效！");
                return;
            }

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
                                            BillAccount = billAccount,
                                            BillCode = billCode,
                                            DepositDate = depositDate,
                                            OrgCode = orgCode,
                                            Period = period,
                                            TellerCode = tellerCode,
                                            DepositorIDCard = idCard,
                                            DepositorName = name,
                                            DepositMoney = money,
                                            BindAccount = bindAccount,
                                            Remark = remark,
                                            Rate = bankRate
                                        };
            if (BizHandler.Handler.AddDepositRecord(record) == 1)
            {
                TMessageBox.ShowMsg(this, "AddRecord", "存款记录添加成功！");
                EnableEditableCtrls(false);
                return;
            }
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            ClearEditableCtrls();
            EnableEditableCtrls(true);
        }

        private bool ValidatePage()
        {
            if (string.IsNullOrEmpty(protocolTxt.Text.Trim()) || string.IsNullOrEmpty(billAccountTxt.Text.Trim()) || string.IsNullOrEmpty(billCodeTxt.Text.Trim()) 
                || string.IsNullOrEmpty(rateTxt.Text.Trim()) || string.IsNullOrEmpty(dateTxt.Text.Trim()) || string.IsNullOrEmpty(bindAccountTxt.Text.Trim()) 
                || string.IsNullOrEmpty(IDCardTxt.Text.Trim()) || string.IsNullOrEmpty(nameTxt.Text.Trim()) || string.IsNullOrEmpty(tellerCodeTxt.Text.Trim()))
            {
                return false;
            }
            return true;
        }

        private void TextBoxDataBind()
        {
            protocolTxt.DataBind();
            billAccountTxt.DataBind();
            billCodeTxt.DataBind();
            //periodDrop.DataBind();
            rateTxt.DataBind();
            moneyTxt.DataBind();
            dateTxt.DataBind();
            bindAccountTxt.DataBind();
            tellerCodeTxt.DataBind();
            IDCardTxt.DataBind();
            nameTxt.DataBind();
            remarkTxt.DataBind();
        }

        private int GetDropDownListSelectionIndex(string selValue)
        {
            int index = 0;
            switch (selValue)
            {
                case "三个月":
                    index = 0;
                    break;
                case "六个月":
                    index = 1;
                    break;
                case "一年":
                    index = 2;
                    break;
                case "二年":
                    index = 3;
                    break;
                case "三年":
                    index = 4;
                    break;
                case "五年":
                    index = 5;
                    break;
                default:
                    index = -1;
                    break;
            }
            return index;
        }

        private void ClearEditableCtrls()
        {
            protocolTxt.Text = string.Empty;
            billAccountTxt.Text = string.Empty;
            billCodeTxt.Text = string.Empty;
            periodDrop.SelectedIndex = 0;
            //rateTxt.Text = string.Empty;
            moneyTxt.Text = string.Empty;
            dateTxt.Text = string.Empty;
            bindAccountTxt.Text = string.Empty;
            tellerCodeTxt.Text = string.Empty;
            IDCardTxt.Text = string.Empty;
            nameTxt.Text = string.Empty;
            remarkTxt.Text = string.Empty;
        }

        private void EnableEditableCtrls(bool enable)
        {
            protocolTxt.Enabled = enable;
            billAccountTxt.Enabled = enable;
            billCodeTxt.Enabled = enable;
            periodDrop.Enabled = enable;
            rateTxt.Enabled = enable;
            moneyTxt.Enabled = enable;
            dateTxt.Enabled = enable;
            bindAccountTxt.Enabled = enable;
            tellerCodeTxt.Enabled = enable;
            IDCardTxt.Enabled = enable;
            nameTxt.Enabled = enable;
            remarkTxt.Enabled = enable;
            depositBtn.Enabled = enable;
            cancelBtn.Enabled = enable;
        }

        protected void dateTxt_TextChanged(object sender, EventArgs e)
        {
            dateTxt.DataBind();
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            bool startDate = e.Day.Date >= DateTime.Now.Date.AddDays(-1);
            bool endDate = e.Day.Date <= DateTime.Now.Date;
            e.Day.IsSelectable = (startDate && endDate);
        }
    }
}
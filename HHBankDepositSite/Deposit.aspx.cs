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
        private BankRate bankRate= new BankRate();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.apsx");
                return;
            }
            string fileName = ConfigUtil.GetValue(WebConfigName.BankRateTable, "");
            fileName += Session["UserName"].ToString() + ".xml";
            this.bankRate = BizHandler.Handler.GetBankRateTable(fileName);
            if (!IsPostBack)
            {
                periodDrop_SelectedIndexChanged(sender, e);
                GenTellerCodeDropDownList(Session["UserName"].ToString());
                tellerCodeDrop_SelectedIndexChanged(sender, e);
            }
        }

        protected void periodDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (periodDrop.SelectedIndex)
            {
                case 0:
                    rateTxt.Text = (bankRate.M03 * 100).ToString("f3");
                    break;
                case 1:
                    rateTxt.Text = (bankRate.M06 * 100).ToString("f3");
                    break;
                case 2:
                    rateTxt.Text = (bankRate.Y01 * 100).ToString("f3");
                    break;
                case 3:
                    rateTxt.Text = (bankRate.Y02 * 100).ToString("f3");
                    break;
                case 4:
                    rateTxt.Text = (bankRate.Y03 * 100).ToString("f3");
                    break;
                case 5:
                    rateTxt.Text = (bankRate.Y05 * 100).ToString("f3");
                    break;
                default:
                    rateTxt.Text = "--";
                    break;
            }
        }

        protected void depositBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (!ValidateText())
            {
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

            string tellerCode = tellerCodeDrop.SelectedValue.Trim();
            string tellerName = tellerNameTxt.Text.Trim();

            string idCard = IDCardTxt.Text.Trim();
            string name = nameTxt.Text.Trim();
            string remark = remarkTxt.Text.Trim();
            DateTime dueDate = SectionCalculator.GetDueDateByPeriod(depositDate, (Period)period);
            
            DepositRecord record = new DepositRecord 
                                        {
                                            ProtocolID = protocolID,
                                            BillAccount = billAccount,
                                            BillCode = billCode,
                                            DepositDate = depositDate,
                                            OrgCode = orgCode,
                                            Period = period,
                                            TellerCode = tellerCode,
                                            TellerName = tellerName,
                                            DepositorIDCard = idCard,
                                            DepositorName = name,
                                            DepositMoney = money,
                                            BindAccount = bindAccount,
                                            Remark = remark,
                                            Rate = bankRate,
                                            DueDate = dueDate,
                                            DepositFlag = (int)DrawFlag.Deposit
                                        };
            if (BizHandler.Handler.AddDepositRecord(record) == 1)
            {
                TMessageBox.ShowMsg(this, "AddRecord", "存款记录添加成功！");
                return;
            }
            else
            {
                TMessageBox.ShowMsg(this, "AddRecordErr", "协议编号已存在！单笔交易不得重复提交！");
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

        private bool ValidateText()
        {
            string protocolId = protocolTxt.Text.Trim();
            string account = billAccountTxt.Text.Trim();
            string billCode = billCodeTxt.Text.Trim();
            string rate = rateTxt.Text.Trim();
            string date = dateTxt.Text.Trim();
            string bindAccount = bindAccountTxt.Text.Trim();
            string tellerName = tellerNameTxt.Text.Trim();
            string tellerCode = tellerCodeDrop.SelectedValue.Trim();
            string idCard = IDCardTxt.Text.Trim();
            string name = nameTxt.Text.Trim();
            if (string.IsNullOrEmpty(protocolId) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(billCode) 
                || string.IsNullOrEmpty(rate) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(bindAccount) 
                || string.IsNullOrEmpty(idCard) || string.IsNullOrEmpty(name))
            {
                TMessageBox.ShowMsg(this, "InputValidate", "请输入必填项！标“*”为必填项！");
                return false;
            }
            string errMsg = string.Empty;
            string orgCode = Session["UserName"].ToString();

            if (!BizValidator.CheckProtocolID(orgCode, protocolId))
            {
                string str = @"协议编号编码规则：{0}{1}+6位顺序号！\n";
                errMsg += string.Format(str, orgCode.Substring(6), DateTime.Now.Year.ToString());
            }
            if (!BizValidator.CheckBillAccount(account))
            {
                errMsg += @"存单账号格式错误！\n";
            }
            if (!BizValidator.CheckBillCode(billCode))
            {
                errMsg += @"凭证号码必须是以“50”开头的12位数字！\n";
            }
            if (!PageValidator.IsHasCHZN(name))
            {
                errMsg += @"客户姓名必须含汉字！\n";
            }
            if (!BizValidator.CheckIDCard(idCard))
            {
                errMsg += @"客户身份证号码非法！\n";
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                TMessageBox.ShowMsg(this, "NumberValidateTotal", errMsg);
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
            //tellerNameTxt.DataBind();
            IDCardTxt.DataBind();
            nameTxt.DataBind();
            remarkTxt.DataBind();
        }

        private void GenTellerCodeDropDownList(string orgCode)
        {
            List<TellerInfo> tellerList = BizHandler.Handler.GetTellerInfoListByOrgCode(orgCode);
            if (tellerList != null && tellerList.Count > 0)
            {
                for (int i = 0; i < tellerList.Count; i++)
                {
                    if (tellerCodeDrop.Items.Contains(new ListItem(tellerList[i].TellerCode)))
                    {
                        continue;
                    }
                    tellerCodeDrop.Items.Add(tellerList[i].TellerCode);
                }
            }
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
            tellerNameTxt.Text = string.Empty;
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
            tellerNameTxt.Enabled = enable;
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
            //bool startDate = e.Day.Date >= DateTime.Now.Date.AddDays(-1);
            //bool endDate = e.Day.Date <= DateTime.Now.Date;
            //e.Day.IsSelectable = (startDate && endDate);
        }

        public string TextInputCheck()
        {
            string content = protocolTxt.Text.Trim();
            if (!PageValidator.IsNumber(content))
            {
                return "协议编号不正确";
            }
            return "";
        }

        private bool NumberCheck(string tag, string desc, TextBox ctrl)
        {
            string content = ctrl.Text.Trim();
            if (!PageValidator.IsNumber(content))
            {
                TMessageBox.ShowMsg(this, tag, desc);
                return false;
            }
            return true;
        }

        private bool DecimalCheck(string tag, string desc, TextBox ctrl)
        {
            string content = ctrl.Text.Trim();
            if (!PageValidator.IsDecimal(content))
            {
                TMessageBox.ShowMsg(this, tag, desc);
                return false;
            }
            return true;
        }

        private bool MaxLenCheckTemplate(string tag, string desc, TextBox ctrl)
        {
            string content = ctrl.Text.Trim();
            if (!PageValidator.IsDecimal(content))
            {
                TMessageBox.ShowMsg(this, tag, desc);
                return false;
            }
            return true;
        }

        private bool ZHCNCheckTemplate(string tag, string desc, TextBox ctrl)
        {
            string content = ctrl.Text.Trim();
            if (!PageValidator.IsHasCHZN(content))
            {
                TMessageBox.ShowMsg(this, tag, desc);
                return false;
            }
            return true;
        }

        protected void tellerCodeDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tellerCode = tellerCodeDrop.SelectedValue.Trim();
            tellerNameTxt.Text = WebDataCenter.TellerDict[tellerCode].TellerName;
        }
    }
}
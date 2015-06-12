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

        private DrawRecord drawRecord = new DrawRecord();

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
            if (Session["UserName"] == null)
            {
                TMessageBox.ShowMsg(this, "SessionExpired", "超时，请重新登录！");
                Response.Redirect("~/Login.aspx");
                return;
            }

            string protocolId= protocolIDTxt.Text.Trim();
            string billAccount = billAccountTxt.Text.Trim();
            string billCode = billCodeTxt.Text.Trim();
            if (string.IsNullOrEmpty(drawDateTxt.Text.Trim()))
            {
                TMessageBox.ShowMsg(this, "DrawDateEmpty", "请输入支取日期！");
                return;
            }
            if (string.IsNullOrEmpty(moneyDrawTxt.Text.Trim()))
            {
                TMessageBox.ShowMsg(this, "DrawMoneyEmpty", "请输入支取金额！");
                return;
            }
            if (!PageValidator.IsDecimal(moneyDrawTxt.Text.Trim()))
            {
                TMessageBox.ShowMsg(this, "DrawMoneyInvalid", "请输入取款金额！");
                moneyDrawTxt.Text = string.Empty;
                TMessageBox.SetFocus(moneyDrawTxt, this);
                return;
            }

            DateTime drawDate = Calendar1.SelectedDate;
            decimal drawMoney = decimal.Parse(moneyDrawTxt.Text.Trim());

            DrawRecord record = BizHandler.Handler.GetDrawRecord(protocolId, billAccount, billCode, Session["UserName"].ToString());
            drawRecord = record;
            if (record == null)
            {
                TMessageBox.ShowMsg(this, "RecordNotExists", "存款记录不存在！");
                return;
            }
            if (record.Status == DrawFlag.Draw)
            {
                TMessageBox.ShowMsg(this, "DrawRecordErr", "存款已被支取！");
                return;
            }
            if (record.Status == DrawFlag.Remain && record.CapticalMoney < 5000)
            {
                TMessageBox.ShowMsg(this, "Draw5000Less", "存款已被部分提前支取，余额不足5000，不再享受保利存补息！");
                return;
            }
            if (record.Status == DrawFlag.Deposit)
            {
                if (drawMoney > record.CapticalMoney)
                {
                    TMessageBox.ShowMsg(this, "DrawMoneyOverflowCalc", "支取金额不能大于账户留存金额！");
                    moneyDrawTxt.Text = string.Empty;
                }
            }
            if (record.Status == DrawFlag.Remain)
            {
                if (drawMoney > record.RemainMoney)
                {
                    TMessageBox.ShowMsg(this, "DrawMoneyOverflowRemain", "支取金额不能大于账户金额！");
                    moneyDrawTxt.Text = string.Empty;
                }
            }

            BankRate depositRate = new BankRate {
                                            CurrRate = record.Rate.CurrRate,
                                            D01 = record.Rate.D01,
                                            M03 = record.Rate.M03,
                                            M06 = record.Rate.M06,
                                            Y01 = record.Rate.Y01,
                                            Y02 = record.Rate.Y02,
                                            Y03 = record.Rate.Y03,
                                            Y05 = record.Rate.Y05
                                        };
            CalcInfo calcInfo = new CalcInfo { 
                                        StartDate = record.DepositDate,
                                        EndDate = drawDate,
                                        CapitalMoney = drawMoney,
                                        DepositPeriod = record.BillPeriod
                                    };
            SectionCalculator calculator = new SectionCalculator();
            CalcResult result = calculator.CalcTotalResult(calcInfo, depositRate);
            sectionTxt.Text = (string.IsNullOrEmpty(result.SectionDesc) ? "--" : result.SectionDesc);
            systemTxt.Text = (result.SystemInterest + drawMoney).ToString();
            totalInterestTxt.Text = (result.SectionInterest + drawMoney).ToString();
            marginTxt.Text = result.MarginInterest.ToString();
        }

        protected void okBtn_Click(object sender, EventArgs e)
        {
            DrawInfo info = new DrawInfo();
            info.DrawDate = drawRecord.DrawDate;
            info.DrawMoney = drawRecord.DrawMoney;
            info.ProtocolId = drawRecord.ProtocolID;
            info.SystemInterest = drawRecord.SystemInterest;
            info.SectionInterest = drawRecord.SectionInterest;
            info.RemainMoney = drawRecord.CapticalMoney - drawRecord.DrawMoney;
            info.MarginInterest = drawRecord.SectionInterest - drawRecord.SystemInterest;
            bool res = false;
            if (info.RemainMoney > decimal.Zero)
            {
                info.DrawStatus = DrawFlag.Draw;
                info.FinalDrawDate = drawRecord.DrawDate;
                res = BizHandler.Handler.FinalDrawDepsoitRecord(info, Session["UserName"].ToString());
            }
            else
            {
                res = BizHandler.Handler.DrawDepositRecord(info, Session["UserName"].ToString());
            }
            
            if (!res)
            {
                TMessageBox.ShowMsg(this, "DrawMoneyErr", "支取失败！");
                return;
            }
            else
            {
                TMessageBox.ShowMsg(this, "DrawMoneySuccess", "支取成功！");
                return;
            }
        }

        protected void periodDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (periodDrop.SelectedIndex)
            //{
            //    case 0:
            //        execRateTxt.Text = (bankRate.M03 * 100).ToString();
            //        break;
            //    case 1:
            //        execRateTxt.Text = (bankRate.M06 * 100).ToString();
            //        break;
            //    case 2:
            //        execRateTxt.Text = (bankRate.Y01 * 100).ToString();
            //        break;
            //    case 3:
            //        execRateTxt.Text = (bankRate.Y02 * 100).ToString();
            //        break;
            //    case 4:
            //        execRateTxt.Text = (bankRate.Y03 * 100).ToString();
            //        break;
            //    case 5:
            //        execRateTxt.Text = (bankRate.Y05 * 100).ToString();
            //        break;
            //    default:
            //        execRateTxt.Text = "--";
            //        break;
            //}
            //execRateTxt.DataBind();
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
                errMsg += @"协议编号必须全部为数字！\n";
            }
            if (!PageValidator.IsNumber(billAccount))
            {
                errMsg += @"存单账号必须全部为数字！\n";
            }
            if (billAccount.Length != 23)
            {
                errMsg += @"存单账号长度必须为23位！\n";
            }
            if (!PageValidator.IsNumber(billCode))
            {
                errMsg += @"凭证号码必须全部为数字！\n";
            }
            if (billCode.Length != 12)
            {
                errMsg += @"凭证号码长度必须为12位！";
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                TMessageBox.ShowMsg(this, "TotalPageValidator", errMsg);
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
            if (record.DueDate.Date == DateTime.MaxValue.Date)
            {
                record.DueDate = SectionCalculator.GetDueDateByPeriod(record.DepositDate, record.BillPeriod);
            }
            depositDateTxt.Text = record.DepositDate.ToString("yyyy-MM-dd");
            dueDateTxt.Text = record.DueDate.ToString("yyyy-MM-dd");
            //moneyTxt.Text = record.CapticalMoney.ToString();
            if (record.Status == DrawFlag.Remain)
            {
                moneyTxt.Text = record.RemainMoney.ToString();
            }
            else
            {
                moneyTxt.Text = record.CapticalMoney.ToString();
            }
            clientIDTxt.Text = record.DepositorIDCard;
            clientNameTxt.Text = record.DepositorName;
            tellerCodeTxt.Text = record.TellerCode;
            remarkTxt.Text = record.Remark;
            bindAccountTxt.Text = record.BindAccount;
            periodTxt.Text = GetPeriodDesc(record.BillPeriod);
            execRateTxt.Text = GetBankRateDesc(record.BillPeriod, record.Rate);
            systemInterestTxt.Text = SectionCalculator.CalcDueDrawInterest(record.CapticalMoney, record.BillPeriod, record.Rate).ToString();
            drawStatusTxt.Text = GetDrawStatus(record.Status);
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            bool start = e.Day.Date >= DateTime.Now.Date.AddDays(-1);
            bool end = e.Day.Date <= DateTime.Now.Date.AddDays(1);
            e.Day.IsSelectable = (start && end);
        }

        private string GetPeriodDesc(Period period)
        {
            string desc = string.Empty;
            switch (period)
            {
                case Period.M03:
                    desc = "三个月";
                    break;
                case Period.M06:
                    desc = "六个月";
                    break;
                case Period.Y01:
                    desc = "一年";
                    break;
                case Period.Y02:
                    desc = "二年";
                    break;
                case Period.Y03:
                    desc = "三年";
                    break;
                case Period.Y05:
                    desc = "五年";
                    break;
                default:
                    desc = "--";
                    break;
            }
            return desc;
        }

        private string GetBankRateDesc(Period period, BankRate bankRate)
        {
            string desc = string.Empty;
            switch (period)
            {
                case Period.M03:
                    desc = (bankRate.M03 * 100).ToString();
                    break;
                case Period.M06:
                    desc = (bankRate.M06 * 100).ToString();
                    break;
                case Period.Y01:
                    desc = (bankRate.Y01 * 100).ToString();
                    break;
                case Period.Y02:
                    desc = (bankRate.Y02 * 100).ToString();
                    break;
                case Period.Y03:
                    desc = (bankRate.Y03 * 100).ToString();
                    break;
                case Period.Y05:
                    desc = (bankRate.Y05 * 100).ToString();
                    break;
                default:
                    desc = "--";
                    break;
            }
            return desc;
        }

        private string GetDrawStatus(DrawFlag flag)
        {
            string desc = "--";
            switch (flag)
            {
                case DrawFlag.Deposit:
                    desc = "存入未支取";
                    break;
                case DrawFlag.Draw:
                    desc = "已全部支取";
                    break;
                case DrawFlag.Remain:
                    desc = "部分提前支取";
                    break;
                default:
                    desc = "未知";
                    break;
            }
            return desc;
        }

        protected void moneyDrawTxt_TextChanged(object sender, EventArgs e)
        {
            string content = moneyDrawTxt.Text.Trim();
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            if (!PageValidator.IsDecimal(content))
            {
                TMessageBox.ShowMsg(this, "DrawMoneyNotDecimal", "请输入取款金额！");
                moneyDrawTxt.Text = string.Empty;
                TMessageBox.SetFocus(moneyDrawTxt, this);
                return;
            }
            if (!string.IsNullOrEmpty(content))
            {
                decimal money = decimal.Parse(content);
                decimal principal = decimal.Parse(moneyTxt.Text.Trim());
                if (money > principal)
                {
                    TMessageBox.ShowMsg(this, "MoneyOverflow", "取款金额不能大于存入金额！");
                    moneyDrawTxt.Text = string.Empty;
                    TMessageBox.SetFocus(moneyDrawTxt, this);
                }
            }
        }
    }
}
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
            if (Session["UserName"] == null)
            {
                TMessageBox.ShowMsg(this, "SessionExpired", "超时，请重新登录！");
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (string.IsNullOrEmpty(depositDateTxt.Text.Trim()))
            {
                TMessageBox.ShowMsg(this, "NotSearchYet", "请先根据协议编号和存单账号查询此交易！");
                return;
            }

            DrawRecord record = BizHandler.Handler.DrawRecordInfo;
            if (string.IsNullOrEmpty(record.ProtocolID))
            {
                TMessageBox.ShowMsg(this, "DrawRecordExistsSure", "请先点“查询”！");
                return;
            }
            string protocolId= protocolIDTxt.Text.Trim();
            string billAccount = billAccountTxt.Text.Trim();
            if (string.IsNullOrEmpty(drawDateTxt.Text.Trim()))
            {
                TMessageBox.ShowMsg(this, "DrawDateEmpty", "请输入支取日期！");
                return;
            }
            if (string.IsNullOrEmpty(moneyDrawTxt.Text.Trim()))
            {
                TMessageBox.ShowMsg(this, "DrawMoneyEmpty", "请输入取款金额！");
                return;
            }
            if (!PageValidator.IsDecimal(moneyDrawTxt.Text.Trim()))
            {
                TMessageBox.ShowMsg(this, "DrawMoneyInvalid", "请输入取款金额！");
                moneyDrawTxt.Text = string.Empty;
                return;
            }

            DateTime drawDate = DateTime.Parse(drawDateTxt.Text.Trim());
            decimal drawMoney = decimal.Parse(moneyDrawTxt.Text.Trim());
            record = BizHandler.Handler.GetDrawRecord(protocolId, billAccount, Session["UserName"].ToString());
            if (record == null)
            {
                TMessageBox.ShowMsg(this, "RecordNotExists", "存款记录不存在！");
                return;
            }
            if (record.Status == DrawFlag.Draw)
            {
                TMessageBox.ShowMsg(this, "DrawRecordErr", "存款已被全部支取！");
                return;
            }
            if (record.Status == DrawFlag.Remain && record.CapticalMoney < 5000)
            {
                TMessageBox.ShowMsg(this, "Draw5000Less", "存款已被部分提前支取，余额不足5000元，不再享受保利存补息！");
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
                    TMessageBox.ShowMsg(this, "DrawMoneyOverflowRemain", "支取金额不能大于账户留存金额！");
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
            if (BizHandler.Handler.DrawRecordInfo.Status == DrawFlag.Remain)
            {
                if (drawMoney < 5000)
                {
                    result.SectionInterest = result.SystemInterest;
                    result.MarginInterest = 0;
                }
            }
            if (BizHandler.Handler.DrawRecordInfo.Status == DrawFlag.Deposit)
            {
                BizHandler.Handler.DrawRecordInfo.FirstDrawDate = drawDate;
                BizHandler.Handler.DrawRecordInfo.FirstDrawMoney = drawMoney;
                BizHandler.Handler.DrawRecordInfo.FirstSysInterest = result.SystemInterest;
                BizHandler.Handler.DrawRecordInfo.FirstSectionInterest = result.SectionInterest;
                BizHandler.Handler.DrawRecordInfo.FirstMarginInterest = result.MarginInterest;
                BizHandler.Handler.DrawRecordInfo.RemainMoney = BizHandler.Handler.DrawRecordInfo.CapticalMoney - drawMoney;
            }
            else if (BizHandler.Handler.DrawRecordInfo.Status == DrawFlag.Remain)
            {
                BizHandler.Handler.DrawRecordInfo.FinalDrawDate = drawDate;
                BizHandler.Handler.DrawRecordInfo.FinalDrawMoney = drawMoney;
                BizHandler.Handler.DrawRecordInfo.FinalSysInterest = result.SystemInterest;
                BizHandler.Handler.DrawRecordInfo.FinalSectionInterest = result.SectionInterest;
                BizHandler.Handler.DrawRecordInfo.FinalMarginInterest = result.MarginInterest;
                BizHandler.Handler.DrawRecordInfo.RemainMoney -= drawMoney;
            }
            sectionTxt.Text = (string.IsNullOrEmpty(result.SectionDesc) ? "--" : result.SectionDesc);
            systemTxt.Text = (result.SystemInterest + drawMoney).ToString("f2");
            totalInterestTxt.Text = (result.SectionInterest + drawMoney).ToString("f2");
            marginTxt.Text = result.MarginInterest.ToString("f2");
        }

        protected void okBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                TMessageBox.ShowMsg(this, "SessionExiredDraw", "登录超时，请重新登录！");
                Session["UserName"] = null;
                Session["Password"] = null;
                Response.Redirect("~/Login.aspx");
                return;
            }
            DrawRecord record = BizHandler.Handler.DrawRecordInfo;
            if (string.IsNullOrEmpty(record.ProtocolID))
            {
                TMessageBox.ShowMsg(this, "DrawRecordNotSearch2", "请先点“查询”！");
                return;
            }
            DrawInfo info = new DrawInfo();
            if (record.Status == DrawFlag.Deposit)
            {
                info.DrawDate = record.FirstDrawDate;
                info.DrawMoney = record.FirstDrawMoney;
                info.ProtocolId = record.ProtocolID;
                info.SystemInterest = record.FirstSysInterest;
                info.SectionInterest = record.FirstSectionInterest;
                info.RemainMoney = record.CapticalMoney - record.FirstDrawMoney;
                info.MarginInterest = record.FirstSectionInterest - record.FirstSysInterest;
                if (info.RemainMoney > decimal.Zero)
                {
                    info.DrawStatus = DrawFlag.Remain;
                }
                else
                {
                    info.DrawStatus = DrawFlag.Draw;
                }
            }
            else if (record.Status == DrawFlag.Remain)
            {
                info.DrawDate = record.FinalDrawDate;
                info.DrawMoney = record.FinalDrawMoney;
                info.ProtocolId = record.ProtocolID;
                info.SystemInterest = record.FinalSysInterest;
                info.SectionInterest = record.FinalSectionInterest;
                info.RemainMoney = record.RemainMoney - record.FirstDrawMoney;
                info.MarginInterest = record.FinalSectionInterest - record.FinalSysInterest;
                info.DrawStatus = DrawFlag.Draw;
            }
            bool res = false;
            if (info.RemainMoney > decimal.Zero)
            {
                info.DrawStatus = DrawFlag.Remain;
                info.FinalDrawDate = record.FirstDrawDate;
                res = BizHandler.Handler.DrawDepositRecord(info, Session["UserName"].ToString());
            }
            else
            {
                res = BizHandler.Handler.FinalDrawDepositRecord(info, Session["UserName"].ToString());
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
                TMessageBox.ShowMsg(this, "SessionExpiredSearchRecord", "登录超时，请重新登录！");
                Session["Password"] = null;
                Response.Redirect("~/Login.aspx");
                return;
            }
            string protocolId = protocolIDTxt.Text.Trim();
            string billAccount = billAccountTxt.Text.Trim();
            if (string.IsNullOrEmpty(protocolId) || string.IsNullOrEmpty(billAccount))
            {
                TMessageBox.ShowMsg(this, "DrawSearch", "请务必输入协议号、存单账号！");
                return;
            }
            string errMsg = string.Empty;
            string orgcode = Session["UserName"].ToString();
            if (!BizValidator.IsProtocolId(orgcode, protocolId))
            {
                string str = @"协议编号编码规则：{0}{1}+6位顺序号！\n";
                errMsg += string.Format(str, orgcode.Substring(6), DateTime.Now.Year.ToString());
            }
            if (!BizValidator.CheckBillAccount(billAccount))
            {
                errMsg += @"存单账号格式不对！\n";
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                TMessageBox.ShowMsg(this, "TotalPageValidator1", errMsg);
                return;
            }
            DrawRecord record = BizHandler.Handler.GetDrawRecord(protocolId, billAccount, Session["UserName"].ToString());
            if (record == null)
            {
                TMessageBox.ShowMsg(this, "TradeRecordNotExists", "没有满足条件的交易记录！");
                ClearTextBox();
                return;
            }
            UpdatePage(record);
        }

        private void UpdatePage(DrawRecord record)
        {
            depositDateTxt.Text = record.DepositDate.ToString("yyyy-MM-dd");
            dueDateTxt.Text = record.DueDate.ToString("yyyy-MM-dd");
            moneyTxt.Text = record.CapticalMoney.ToString("f2");
            clientIDTxt.Text = record.DepositorIDCard;
            clientNameTxt.Text = record.DepositorName;
            tellerCodeTxt.Text = record.TellerCode;
            remarkTxt.Text = record.Remark;
            bindAccountTxt.Text = record.BindAccount;
            periodTxt.Text = GetPeriodDesc(record.BillPeriod);
            execRateTxt.Text = GetBankRateDesc(record.BillPeriod, record.Rate);
            systemInterestTxt.Text = SectionCalculator.CalcDueDrawInterest(record.CapticalMoney, record.BillPeriod, record.Rate).ToString("f2");
            drawStatusTxt.Text = GetDrawStatus(record.Status);
            if (record.Status == DrawFlag.Remain)
            {
                adDrawMoneyTxt.Text = record.FirstDrawMoney.ToString("f2");
                adDrawDateTxt.Text = record.FirstDrawDate.ToString("yyyy-MM-dd");
                remainMoneyTxt.Text = record.RemainMoney.ToString("f2");
                adSysInterestTxt.Text = record.FirstSysInterest.ToString("f2");
                adSectionInterestTxt.Text = record.FirstSectionInterest.ToString("f2");
                adMarginInterestTxt.Text = record.FirstMarginInterest.ToString("f2");
            }
            else if (record.Status == DrawFlag.Deposit)
            {
                adDrawMoneyTxt.Text = "--";
                adDrawDateTxt.Text = "--";
                adSysInterestTxt.Text = "--";
                adSectionInterestTxt.Text = "--";
                adMarginInterestTxt.Text = "--";
                remainMoneyTxt.Text = record.CapticalMoney.ToString("f2");
            }
            else
            {
                adDrawMoneyTxt.Text = record.FinalDrawMoney.ToString("f2");
                adDrawDateTxt.Text = record.FinalDrawDate.ToString("yyyy-MM-dd");
                remainMoneyTxt.Text = record.RemainMoney.ToString("f2");
                adSysInterestTxt.Text = record.FinalSysInterest.ToString("f2");
                adSectionInterestTxt.Text = record.FinalSectionInterest.ToString("f2");
                adMarginInterestTxt.Text = record.FinalMarginInterest.ToString("f2");
            }
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            bool start = e.Day.Date >= DateTime.Now.Date.AddDays(-1);
            bool end = e.Day.Date <= DateTime.Now.Date.AddDays(1);
            e.Day.IsSelectable = (start && end);
        }

        private void ClearTextBox()
        {
            depositDateTxt.Text = string.Empty;
            dueDateTxt.Text = string.Empty;
            moneyTxt.Text = string.Empty;
            clientIDTxt.Text = string.Empty;
            clientNameTxt.Text = string.Empty;
            tellerCodeTxt.Text = string.Empty;
            remarkTxt.Text = string.Empty;
            remainMoneyTxt.Text = string.Empty;
            bindAccountTxt.Text = string.Empty;
            periodTxt.Text = string.Empty;
            execRateTxt.Text = string.Empty;
            systemInterestTxt.Text = string.Empty;
            drawStatusTxt.Text = string.Empty;
            adDrawDateTxt.Text = string.Empty;
            adDrawMoneyTxt.Text = string.Empty;
            adSysInterestTxt.Text = string.Empty;
            adSectionInterestTxt.Text = string.Empty;
            adMarginInterestTxt.Text = string.Empty;
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
    }
}
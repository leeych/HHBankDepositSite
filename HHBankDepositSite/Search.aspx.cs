using BLL;
using Common;
using HHBankDepositSite.Data;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHBankDepositSite
{
    public partial class Search : System.Web.UI.Page
    {
        protected static Dictionary<string, string> BillPeriodDict = new Dictionary<string, string>() 
        {
            {"0", "三个月"}, {"1","六个月"}, {"2", "一年"}, {"3", "二年"}, {"4", "三年"}, {"5", "五年"} 
        };

        protected static Dictionary<string, string> StatusDict = new Dictionary<string, string>() 
        { 
            {"0", "存入未支取"}, {"1", "已全部支取"}, {"2", "部分提前支取"}, {"3", "他行支取"}, {"4", "其他"}
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                orgCodeTxt.Text = Session["UserName"].ToString();
                SearchDraftInfo info = BizHandler.Handler.GetDraftSearchInfo(Session["UserName"].ToString());
                UpdateDraftInfo(info);
            }
        }

        private void UpdateDraftInfo(SearchDraftInfo info)
        {
            if (info != null)
            {
                orgCodeTxt.Text = info.OrgCode;
                orgNameTxt.Text = info.OrgName;
                maxProtocolIdTxt.Text = info.MaxProtocolId;
                protocolCountTxt.Text = info.RecordCount.ToString();
            }
            else
            {
                orgCodeTxt.Text = Session["UserName"].ToString();
                orgNameTxt.Text = BizHandler.Handler.GetOrgNameByOrgCode(Session["UserName"].ToString());
                maxProtocolIdTxt.Text = "--";
                protocolCountTxt.Text = "0";
            }
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            SearchParam param = new SearchParam();
            string startDateStr = startDateTxt.Text.Trim();
            string endDateStr = endDateTxt.Text.Trim();
            param.ProtocolID = protocolIdTxt.Text.Trim();
            param.BillAccount = billAccountTxt.Text.Trim();
            param.ClientID = idCardTxt.Text.Trim();
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            string orgCode = Session["UserName"].ToString();

            if (string.IsNullOrEmpty(param.ProtocolID) && string.IsNullOrEmpty(param.BillAccount) 
                && string.IsNullOrEmpty(startDateStr) && string.IsNullOrEmpty(endDateStr))
            {
                orgRecordGv.DataSource = BizHandler.Handler.GetOrgRecordDataSource(new DateTime(DateTime.Now.Year,01,01), DateTime.Now, orgCode);
                orgRecordGv.DataBind();
            }
            else if (!string.IsNullOrEmpty(param.ProtocolID))
            {
                if (!BizValidator.CheckProtocolID(Session["UserName"].ToString(),param.ProtocolID))
                {
                    string errmsg = @"协议编号格式不对！{0} + {1} + 6位顺序号！";
                    TMessageBox.ShowMsg(this, "SearchProtocolIDInvalid", string.Format(errmsg, orgCode.Substring(6), DateTime.Now.Year));
                    return;
                }
                else
                {
                    orgRecordGv.DataSource = BizHandler.Handler.GetOrgRecordDataSource(param.ProtocolID, orgCode);
                    orgRecordGv.DataBind();
                }
            }
            else if (!string.IsNullOrEmpty(param.BillAccount))
            {
                if (!BizValidator.CheckBillAccount(param.BillAccount))
                {
                    TMessageBox.ShowMsg(this, "SearchBillAccountInvalid", "存单账号无效！请确认后重新输入！");
                    return;
                }
                else
                {
                    orgRecordGv.DataSource = BizHandler.Handler.GetOrgRecordDataSourceByBillAccount(param.BillAccount, orgCode);
                    orgRecordGv.DataBind();
                }
            }
            else if (!string.IsNullOrEmpty(param.ClientID))
            {
                if (!BizValidator.CheckIDCard(param.ClientID))
                {
                    TMessageBox.ShowMsg(this, "SearchIDInvalid", "身份证号码无效！");
                    return;
                }
                else
                {
                    orgRecordGv.DataSource = BizHandler.Handler.GetOrgRecordDataSourceByIDCard(param.ClientID, orgCode);
                    orgRecordGv.DataBind();
                }
            }
            else if (DateTime.TryParse(startDateStr, out startDate) && DateTime.TryParse(endDateStr, out endDate))
            {
                if (endDate.Date < startDate.Date)
                {
                    TMessageBox.ShowMsg(this, "DateDurationInvalid", "截止日期不能早于开始日期！");
                    return;
                }
                else
                {
                    orgRecordGv.DataSource = BizHandler.Handler.GetOrgRecordDataSource(startDate, endDate, orgCode);
                    orgRecordGv.DataBind();
                }
            }
        }

        private void ShowRecord(List<SearchInfo> infoList)
        {
            string filePath = Server.MapPath("~/Downloads/" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
            StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
            sw.WriteLine("协议编号         存单账号                 凭证号码      存入本金     存入日期    存期          利率      状态            姓名           身份证号码           补息账号             柜员号  首次支取日期   首次支取金额   系统利息       靠档利息      补息金额      最后支取日期   最后支取金额  系统利息       靠档利息       补息");
            for (int i = 0; i < infoList.Count; i++)
            {
                sw.WriteLine(GenString(infoList[i]));
            }
            sw.Flush();
            sw.Close();
            this.ClientScript.RegisterStartupScript(this.GetType(), "download", "<script language='javascript' defer='defer'> if (confirm('记录条数较多是否下载继续查看？')){ document.getElementById('" + linkBtn.ClientID.ToString() + "').click();}</script>");
            string fileName = Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        }

        protected void linkBtn_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/Downloads/"+ Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
            FileInfo fileInfo = new FileInfo(filePath);
            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filePath, System.Text.Encoding.UTF8));
            Response.AppendHeader("Content-Length", fileInfo.Length.ToString());
            Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.TransmitFile(filePath);
            Response.Flush();
            Response.End();
        }

        private string InvalidDate(DateTime date)
        {
            if (date.Date == DateTime.MaxValue.Date)
            {
                return "NULL";
            }
            return date.ToString("yyyy-MM-dd");
        }

        private string NoneMoney(decimal money)
        {
            if (money == decimal.Zero)
            {
                return "NULL";
            }
            return money.ToString("f2");
        }

        private string GenString(SearchInfo info)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}  {1}  {2}  {3}  {4}  {5}  {6}  {7}  {8}  {9}  {10}  {11}", info.ProtocolID, info.BillAccount, info.BillCode,
                string.Format("{0,-10}",info.DepositMoney.ToString("f2")), string.Format("{0,-10}",info.DepositDate.ToString("yyyy-MM-dd")), string.Format("{0,-10}", BizHandler.GetBillPeriodDesc(info.BillPeriod)),
                BizHandler.GetExecRate(info.BillPeriod, info.ExecRate).ToString("f5"), string.Format("{0,-10}", BizHandler.GetDepositStatusDesc(info.Status)),
                string.Format("{0,-10}", info.ClientName), info.ClientID, info.BindAccount, info.TellerCode);
            StringBuilder sb2 = new StringBuilder();
            sb2.AppendFormat("  {0}    {1}    {2}    {3}    {4}    {5}    {6}    {7}    {8}    {9}", string.Format("{0,-10}", InvalidDate(info.FirstDrawDate)), string.Format("{0,-10}", NoneMoney(info.FirstDrawMoney)),
                string.Format("{0,-10}", NoneMoney(info.FirstSysInterest)), string.Format("{0,-10}", NoneMoney(info.FirstCalcInterest)), string.Format("{0,-10}", NoneMoney(info.FirstMarginInterest)), string.Format("{0,-10}", InvalidDate(info.FinalDrawDate)), string.Format("{0,-10}",NoneMoney(info.FinalDrawMoney)),
                string.Format("{0,-10}", NoneMoney(info.FinalSysInterest)), string.Format("{0,-10}", NoneMoney(info.FinalCalcInterest)), string.Format("{0,-10}", NoneMoney(info.FinalMarginInterest)));
            sb.Append(sb2);
            return sb.ToString();
        }

        protected void orgRecordGv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DateTime start = DateTime.Parse(startDateTxt.Text.Trim());
            DateTime end = DateTime.Parse(endDateTxt.Text.Trim());
            string orgCode = Session["UserName"].ToString();
            orgRecordGv.DataSource = BizHandler.Handler.GetOrgRecordDataSource(start, end, orgCode);
            orgRecordGv.DataBind();
        }
    }
}
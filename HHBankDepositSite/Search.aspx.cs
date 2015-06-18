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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                orgCodeTxt.Text = Session["UserName"].ToString();
                SearchDraftInfo info = BizHandler.Handler.GetDraftSearchInfo(Session["UserName"].ToString());
                UpdateDraftInfo(info);
                footerCell.Visible = false;
            }
            //GridView1.DataSource = BizHandler.Handler.SearchRecord("14772015000001", Session["UserName"].ToString());
            //GridView1.DataBind();
            //Control table = GridView1.Controls[0];

            //if (table != null)
            //{
            //    for (int i = 0; i < GridView1.PageSize - GridView1.Rows.Count; i++)
            //    {
            //        int rowIndex = GridView1.Rows.Count + i + 1;
            //        GridViewRow row = new GridViewRow(rowIndex, -1, DataControlRowType.Separator, DataControlRowState.Normal);
            //        row.CssClass = (rowIndex % 2 == 0) ? "alternate" : "item";
            //        for (int j = 0; j < GridView1.Columns.Count; j++)
            //        {
            //            TableCell cell = new TableCell();
            //            cell.Text = "&nbsp;";
            //            row.Controls.Add(cell);
            //        }
            //        table.Controls.AddAt(rowIndex, row);
            //    }
            //}
            //sgv.DataSource = BizHandler.Handler.SearchRecord("14772015000001", Session["UserName"].ToString());
            //sgv.DataBind();
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
            ClearTable();
            SearchParam param = new SearchParam();
            string startDateStr = startDateTxt.Text.Trim();
            string endDateStr = endDateTxt.Text.Trim();
            param.ProtocolID = protocolIdTxt.Text.Trim();
            param.BillAccount = billAccountTxt.Text.Trim();
            param.ClientID = idCardTxt.Text.Trim();
            //param.StartDate = DateTime.Parse(startDateTxt.Text.Trim());
            //param.EndDate = DateTime.Parse(endDateTxt.Text.Trim());
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            if (string.IsNullOrEmpty(param.ProtocolID) && string.IsNullOrEmpty(param.BillAccount) 
                && string.IsNullOrEmpty(startDateStr) && string.IsNullOrEmpty(endDateStr))
            {
                List<SearchInfo> infoList = BizHandler.Handler.SearchRecordByDuration(new DateTime(DateTime.Now.Year, 01, 01), DateTime.Now, Session["UserName"].ToString());
                if (infoList == null || infoList.Count == 0)
                {
                    return;
                }
                ShowRecord(infoList);
            }
            else if (!string.IsNullOrEmpty(param.ProtocolID))
            {
                if (!BizValidator.CheckProtocolID(Session["UserName"].ToString(),param.ProtocolID))
                {
                    string errmsg = @"协议编号格式不对！{0} + {1} + 6位顺序号！";
                    TMessageBox.ShowMsg(this, "SearchProtocolIDInvalid", string.Format(errmsg, Session["UserName"].ToString().Substring(6), DateTime.Now.Year));
                    return;
                }
                else
                {
                    SearchInfo info = BizHandler.Handler.SearchRecordByProtocolID(param.ProtocolID, Session["UserName"].ToString());
                    if (info == null)
                    {
                        TMessageBox.ShowMsg(this, "SearchByProtocolIDNone", "找不到存款记录！");
                        return;
                    }
                    else
                    {
                        InsertToTable(info, 1);
                    }
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
                    SearchInfo info = BizHandler.Handler.SearchRecordByBillAccount(param.BillAccount, Session["UserName"].ToString());
                    if (info == null)
                    {
                        TMessageBox.ShowMsg(this, "SearchBillAccountNone", "找不到存款记录！");
                        return;
                    }
                    else
                    {
                        InsertToTable(info, 1);
                    }
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
                    List<SearchInfo> infoList = BizHandler.Handler.SearchRecordByIDCard(param.ClientID, Session["UserName"].ToString());
                    if (infoList == null || infoList.Count == 0)
                    {
                        TMessageBox.ShowMsg(this, "SearchIDNone", "找不到存款记录！");
                        return;
                    }
                    else
                    {
                        UpdateTable(infoList);
                    }
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
                    List<SearchInfo> infoLst = BizHandler.Handler.SearchRecordByDuration(startDate, endDate, Session["UserName"].ToString());
                    if (infoLst == null || infoLst.Count == 0)
                    {
                        TMessageBox.ShowMsg(this, "DurationNone", "此时间段里无存款记录！");
                        return;
                    }
                    else
                    {
                        ShowRecord(infoLst);
                    }
                }
            }
            else
            {
                TMessageBox.ShowMsg(this, "StartOrEndDateInvalid", "请输入合法的开始日期/截止日期！");
                return;
            }
        }

        private void ShowRecord(List<SearchInfo> infoList)
        {
            if (infoList.Count > 10)
            {
                string filePath = Server.MapPath("~/Downloads/" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
                for (int i = 0; i < infoList.Count; i++)
                {
                    sw.WriteLine(GenString(infoList[i]));
                }
                sw.Flush();
                sw.Close();
                this.ClientScript.RegisterStartupScript(this.GetType(), "download", "<script language='javascript' defer='defer'> if (confirm('记录条数较多是否下载继续查看？')){ document.getElementById('" + linkBtn.ClientID.ToString() + "').click();}</script>");
                for (int i = 1; i <= 10; i++)
                {
                    InsertToTable(infoList[i - 1], i);
                }
                string fileName = Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string hyperUrl = "仅显示10条记录，详细请点此下载...<a href=\"{0}\">{1}</a>";
                footerCell.Text = string.Format(hyperUrl, "./Downloads/" + fileName, fileName);
                footerCell.Visible = true;
            }
            else
            {
                UpdateTable(infoList);
            }
        }

        private void UpdateTable(List<SearchInfo> infoList)
        {
            for (int i = 1; i <= infoList.Count; i++)
            {
                InsertToTable(infoList[i-1], i);
            }
        }

        private TableRow GenerateTableRow(SearchInfo info)
        {
            if (info == null)
            {
                return null;
            }
            TableCell protocolCell = new TableCell();
            protocolCell.Text = info.ProtocolID;
            TableCell accountCell = new TableCell();
            accountCell.Text = info.BillAccount;
            TableCell billCodeCell = new TableCell();
            billCodeCell.Text = info.BillCode; 
            TableCell money = new TableCell();
            money.Text = info.DepositMoney.ToString("f2");
            TableCell date = new TableCell();
            date.Text = info.DepositDate.ToString("yyyy-MM-dd");
            TableCell period = new TableCell();
            period.Text = BizHandler.GetBillPeriodDesc(info.BillPeriod);
            TableCell rate = new TableCell();
            rate.Text = BizHandler.GetExecRate(info.BillPeriod, info.ExecRate).ToString("f5");
            TableCell status = new TableCell();
            status.Text = BizHandler.GetDepositStatusDesc(info.Status);
            TableCell clientName = new TableCell();
            clientName.Text = info.ClientName;
            TableCell clientID = new TableCell();
            clientID.Text = info.ClientID;
            TableCell teller = new TableCell();
            teller.Text = info.TellerCode;

            TableRow tr = new TableRow();
            tr.Cells.Add(protocolCell);
            tr.Cells.Add(accountCell);
            tr.Cells.Add(billCodeCell);
            tr.Cells.Add(date);
            tr.Cells.Add(money);
            tr.Cells.Add(period);
            tr.Cells.Add(rate);
            tr.Cells.Add(status);
            tr.Cells.Add(clientName);
            tr.Cells.Add(clientID);
            tr.Cells.Add(teller);
            return tr;
        }

        private void InsertToTable(SearchInfo info, int row)
        {
            TableRow tr = resultTable.Rows[row];
            tr.Cells[0].Text = info.ProtocolID;
            tr.Cells[1].Text = info.BillAccount;
            tr.Cells[2].Text = info.BillCode;
            tr.Cells[3].Text = info.DepositMoney.ToString("f2");
            tr.Cells[4].Text = info.DepositDate.ToString("yyyy-MM-dd");
            tr.Cells[5].Text = BizHandler.GetBillPeriodDesc(info.BillPeriod);
            tr.Cells[6].Text = BizHandler.GetExecRate(info.BillPeriod, info.ExecRate).ToString("f5");
            tr.Cells[7].Text = BizHandler.GetDepositStatusDesc(info.Status);
            tr.Cells[8].Text = info.ClientName;
            tr.Cells[9].Text = info.ClientID;
            tr.Cells[10].Text = info.TellerCode;
        }

        private void ClearTableCell(int row, int col)
        {
            resultTable.Rows[row].Cells[col].Text = string.Empty;
        }

        private void ClearTable()
        {
            for (int i = 1; i < resultTable.Rows.Count - 1; i++)
            {
                for (int j = 0; j < resultTable.Rows[0].Cells.Count; j++)
                {
                    resultTable.Rows[i].Cells[j].Text = string.Empty;
                }
            }
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

        private string GenString(SearchInfo info)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}  {1}  {2}  {3}  {4}  {5}  {6}  {7}  {8}  {9}  {10}", info.ProtocolID, info.BillAccount, info.BillCode,
                info.DepositMoney.ToString("f2"), info.DepositDate.ToString("yyyy-MM-dd"), BizHandler.GetBillPeriodDesc(info.BillPeriod),
                BizHandler.GetExecRate(info.BillPeriod, info.ExecRate).ToString("f5"), BizHandler.GetDepositStatusDesc(info.Status),
                info.ClientName, info.ClientID, info.TellerCode);
            StringBuilder sb2 = new StringBuilder();
            sb2.AppendFormat("  {0}  {1}  {2}  {3}  {4}  {5}  {6}  {7}", info.FirstDrawDate.ToString("yyyy-MM-dd"), info.FirstDrawMoney.ToString("f2"), info.FirstSysInterest.ToString("f2"),
                info.FirstCalcInterest.ToString("f2"), info.FirstMarginInterest.ToString("f2"), info.FinalDrawDate.ToString("yyyy-MM-dd"), info.FinalDrawMoney.ToString("f2"),
                info.FinalSysInterest.ToString("f2"), info.FinalCalcInterest.ToString("f2"), info.FinalMarginInterest.ToString("f2"));
            sb.Append(sb2);
            return sb.ToString();
        }
    }
}
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
            param.ProtocolID = protocolIdTxt.Text.Trim();
            param.BillAccount = billAccountTxt.Text.Trim();
            //param.StartDate = DateTime.Parse(startDateTxt.Text.Trim());
            //param.EndDate = DateTime.Parse(endDateTxt.Text.Trim());
            if (string.IsNullOrEmpty(param.ProtocolID) && string.IsNullOrEmpty(param.BillAccount))
            {
                List<SearchInfo> infoList = BizHandler.Handler.SearchRecordByDuration(new DateTime(DateTime.Now.Year, 01, 01), DateTime.Now, Session["UserName"].ToString());
                if (infoList == null)
                {
                    return;
                }
                if (infoList.Count > 10)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "download", "<script language='javascript' defer='defer'> if (confirm('记录条数较多是否下载继续查看？')){ document.getElementById('" + linkBtn.ClientID.ToString() + "').click();}</script>");
                    string filePath = Server.MapPath("~/Downloads/" + Session["UserName"].ToString() + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                    StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
                    for (int i = 0; i < infoList.Count; i++)
                    {
                        sw.WriteLine(GenString(infoList[i]));
                    }
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    UpdateTable(infoList);
                }
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
            TMessageBox.ShowMsg(this, "ExportMsg", "导出成功！");
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
            sb.AppendFormat("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}", info.ProtocolID, info.BillAccount, info.BillCode,
                info.DepositMoney.ToString("f2"), info.DepositDate.ToString("yyyy-MM-dd"), BizHandler.GetBillPeriodDesc(info.BillPeriod),
                BizHandler.GetExecRate(info.BillPeriod, info.ExecRate).ToString("f5"), BizHandler.GetDepositStatusDesc(info.Status),
                info.ClientName, info.ClientID, info.TellerCode);
            return sb.ToString();
        }
    }
}
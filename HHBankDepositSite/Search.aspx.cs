using BLL;
using HHBankDepositSite.Data;
using Model;
using System;
using System.Collections.Generic;
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
                orgNameTxt.Text = "--";
                maxProtocolIdTxt.Text = "--";
                protocolCountTxt.Text = "--";
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
            string protocolId = maxProtocolIdTxt.Text.Trim();
            string orgCode = Session["UserName"].ToString();
            SearchInfo info = BizHandler.Handler.SearchRecordByProtocolID(protocolId, orgCode);
            if (info != null)
            {
                InsertToTable(info, 1);
            }
        }

        private void UpdateTable(List<SearchInfo> infoList)
        {
            for (int i = 1; i <= infoList.Count; i++)
            {
                InsertToTable(infoList[i], i);
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
            for (int i = 1; i < resultTable.Rows.Count; i++)
            {
                for (int j = 0; j < resultTable.Rows[0].Cells.Count; j++)
                {
                    resultTable.Rows[i].Cells[j].Text = string.Empty;
                }
            }
        }
    }
}
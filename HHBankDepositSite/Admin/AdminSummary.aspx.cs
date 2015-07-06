using BLL;
using HHBankDepositSite.Data;
using Model;
using Common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;

namespace HHBankDepositSite.Admin
{
    public partial class AdminSummary : System.Web.UI.Page
    {
        protected static Dictionary<string, string> BillPeriodDict = new Dictionary<string, string>() 
        {
            {"0", "三个月"}, {"1","六个月"}, {"2", "一年"}, {"3", "二年"}, {"4", "三年"}, {"5", "五年"} 
        };
        protected static Dictionary<string, string> StatusDict = new Dictionary<string, string>() 
        { 
            {"0", "存入未支取"}, {"1", "已全部支取"}, {"2", "部分提前支取"}, {"3", "他行支取"}, {"4", "其他"}
        };

        private static DataTable ExcelDataSource { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<OrgInfo> orgList = WebDataCenter.OrgList;
                for (int i = 0; i < orgList.Count; i++)
                {
                    orgNameDrop.Items.Add(orgList[i].OrgName);
                }
                orgNameDrop_SelectedIndexChanged(sender, e);
            }
        }

        protected void orgNameDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            orgCodeTxt.Text = WebDataCenter.OrgDict[orgNameDrop.SelectedValue.Trim()];
            orgCodeTxt.DataBind();
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            ClearDataSource();
            string orgCode = orgCodeTxt.Text.Trim();
            DateTime startDate = DateTime.Parse(startDateAdminTxt.Text.Trim());
            DateTime endDate = DateTime.Parse(endDateAdminTxt.Text.Trim());
            List<SearchInfo> recordList = BizHandler.Handler.SearchRecordByDuration(startDate, endDate, orgCode);
            if (recordList == null || recordList.Count == 0)
            {
                TMessageBox.ShowMsg(this, "AdminSearchListEmpty", "没有满足条件的记录！");
                return;
            }
            AdminSketchInfo sketchInfo = BizHandler.GetSummaryInfo(recordList);
            UpdateSummary(sketchInfo);
            ExcelDataSource = BizHandler.Handler.GetOrgRecordDataSource(startDate, endDate, orgCode);
            GridView1.DataSource = ExcelDataSource;
            GridView1.DataBind();
        }

        private bool ValidatePage()
        {
            DateTime startDate = DateTime.Parse(startDateAdminTxt.Text.Trim());
            DateTime endDate = DateTime.Parse(endDateAdminTxt.Text.Trim());
            if (startDate > endDate)
            {
                TMessageBox.ShowMsg(this, "DateTimeValidate", "起始时间不能大于结束时间！");
                return false;
            }
            return true;
        }

        private void UpdateSummary(AdminSketchInfo info)
        {
            newNumTxt.Text = info.NewRecord.Num.ToString();
            newMoneyTxt.Text = info.NewRecord.Money.ToString("#.##");

            remainNumTxt.Text = info.RemainRecord.Num.ToString();
            remainMoneyTxt.Text = info.RemainRecord.Money.ToString("#.#");

            drawNumTxt.Text = info.DRecord.Num.ToString();
            drawMoneyTxt.Text = info.DRecord.Money.ToString("#.#");

            dueDrawNumTxt.Text = info.DueDrawRecord.Num.ToString();
            dueDrawMoneyTxt.Text = info.DueDrawRecord.Money.ToString("#.#");

            sysNumTxt.Text = info.SysPayfee.Num.ToString();
            sysMoneyTxt.Text = info.SysPayfee.Money.ToString("#.#");

            calcNumTxt.Text = info.CalcPayfee.Num.ToString();
            calcMoneyTxt.Text = info.CalcPayfee.Money.ToString("#.#");

            marginNumTxt.Text = info.MarginPayfee.Num.ToString();
            marginMoneyTxt.Text = info.MarginPayfee.Money.ToString("#.#");
        }

        private void ClearDataSource()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            DateTime start = DateTime.Parse(startDateAdminTxt.Text.Trim());
            DateTime end = DateTime.Parse(endDateAdminTxt.Text.Trim());
            string orgCode = orgCodeTxt.Text.Trim();
            GridView1.DataSource = BizHandler.Handler.GetOrgRecordDataSource(start, end, orgCode);
            GridView1.DataBind();
        }

        protected void exportTxtBtn_Click(object sender, EventArgs e)
        {
        }

        protected void exportExcelBtn_Click(object sender, EventArgs e)
        {
            GridView1.AllowPaging = false;
            GridView1.DataSource = ExcelDataSource;
            GridView1.DataBind();
            GridViewToExcel();
            GridView1.AllowPaging = true;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        private void GridViewToExcel()
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".xls";

            fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).ToString());
            Response.ContentType = "application/ms-excel";
            GridView1.Page.EnableViewState = false;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
            GridView1.Page.EnableViewState = true;
        }

        private void GridViewToTXT()
        {
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            DataTable dt = ExcelDataSource;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sb.AppendLine(dt.Rows[i][j].ToString() + ";");
                }
            }
        }

        private void CreateExcel(DataSet ds, string fileName)
        {
            fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            HttpResponse resp = Page.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            string colHeaders = "", lsItem = "";
            DataTable dt = ds.Tables[0];
            DataRow[] myRow = dt.Select();
            
            int cl = dt.Columns.Count;
            for (int i = 0; i < cl; i++)
            {
                if (i == (cl - 1))
                {
                    colHeaders += dt.Columns[i].Caption.ToString() + "\n";
                }
                else
                {
                    colHeaders += dt.Columns[i].Caption.ToString() + "\t";
                }
            }
            resp.Write(colHeaders);

            foreach (DataRow row in myRow)
            {
                for (int i = 0; i < cl; i++)
                {
                    if (i == (cl - 1))
                    {
                        lsItem += row[i].ToString() + "\n";
                    }
                    else
                    {
                        lsItem += row[i].ToString() + "\t";
                    }
                }
                resp.Write(lsItem);
                lsItem = "";
            }
            resp.End();
        }

        private void InsertDB()
        {
            string sql = @"insert into Jiuhuashanlu(protocolid,billaccount,billcode,depositdate,orgcode,tellercode,tellername,depositorname,depositoridcard,depositmoney,billperiod,duedate,remainmoney,bindaccount,depositflag,currentRate,d01rate,m03rate,m06rate,y01rate,y02rate,y03rate,y05rate) " +
                " values('{0}','10021629952710200000101','503515208801','2015-06-09','3404157871','787103','李杰','李院成','340121198907221314',20000,3,'2017-06-09','20000','6217788311500094100',0,0.0042,0.00001,0.02405,0.02665,0.02925,0.03705,0.0455,0.05225)";
            long protocolid = 78712015000000;

            for (int i = 0; i < 10000; i++)
            {
                string sqlString = string.Format(sql, protocolid++);
                SqlHelper.ExecuteSql(sqlString);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
                }
            }
        }
    }
}
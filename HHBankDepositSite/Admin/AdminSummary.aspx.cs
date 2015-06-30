using BLL;
using HHBankDepositSite.Data;
using Model;
using Common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHBankDepositSite.Admin
{
    public partial class AdminSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<OrgInfo> orgList = WebDataCenter.OrgList;
                for (int i = 0; i < orgList.Count; i++)
                {
                    orgNameDrop.Items.Add(orgList[i].OrgName);
                }
                //startDateTxt.Attributes.Add("ReadOnly", "true");
                //endDateTxt.Attributes.Add("ReadOnly", "true");
            }
        }

        protected void orgNameDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            orgCodeTxt.Text = WebDataCenter.OrgDict[orgNameDrop.SelectedValue.Trim()];
            orgCodeTxt.DataBind();
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
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
            GridView1.DataSource = BizHandler.GenExcelRecordInfoList(recordList);
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
            newMoneyTxt.Text = info.NewRecord.Money.ToString("f2");

            remainNumTxt.Text = info.RemainRecord.Num.ToString();
            remainMoneyTxt.Text = info.RemainRecord.Money.ToString("f2");

            drawNumTxt.Text = info.DRecord.Num.ToString();
            drawMoneyTxt.Text = info.DRecord.Money.ToString("f2");

            dueDrawNumTxt.Text = info.DueDrawRecord.Num.ToString();
            dueDrawMoneyTxt.Text = info.DueDrawRecord.Money.ToString("f2");

            sysNumTxt.Text = info.SysPayfee.Num.ToString();
            sysMoneyTxt.Text = info.SysPayfee.Money.ToString("f2");

            calcNumTxt.Text = info.CalcPayfee.Num.ToString();
            calcMoneyTxt.Text = info.CalcPayfee.Money.ToString("f2");

            marginNumTxt.Text = info.MarginPayfee.Num.ToString();
            marginMoneyTxt.Text = info.MarginPayfee.Money.ToString("f2");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }
    }   
}
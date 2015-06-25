using BLL;
using Common;
using HHBankDepositSite.Data;
using Model;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHBankDepositSite.Admin
{
    public partial class BankRateManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenAllBankRateRecord();
                BankRateInfo rateInfo = WebDataCenter.BankRateInfoList[0];
                currentRateTxt.Text = (rateInfo.Rate.CurrRate * 100).ToString("f3");
                m03RateTxt.Text = (rateInfo.Rate.M03 * 100).ToString("f3");
                m06RateTxt.Text = (rateInfo.Rate.M06 * 100).ToString("f3");
                y01RateTxt.Text = (rateInfo.Rate.Y01 * 100).ToString("f3");
                y02RateTxt.Text = (rateInfo.Rate.Y02 * 100).ToString("f3");
                y03RateTxt.Text = (rateInfo.Rate.Y03 * 100).ToString("f3");
                y05RateTxt.Text = (rateInfo.Rate.Y05 * 100).ToString("f3");
            }
        }

        private void GenAllBankRateRecord()
        {
            List<BankRateInfo> rateList = BizHandler.Handler.GetAllBankRateInfo();
            for (int i = 0; i < rateList.Count; i++)
            {
                if (i + 1 > 10)
                {
                    InsertTableRow(rateList[i], i + 1);
                }
                else
                {
                    bankRateTable.Rows[i + 1].Cells[0].Text = rateList[i].EffectDate.ToString("yyyy-MM-dd");
                    bankRateTable.Rows[i + 1].Cells[1].Text = (rateList[i].Rate.CurrRate * 100).ToString("f3");
                    bankRateTable.Rows[i + 1].Cells[2].Text = (rateList[i].Rate.M03 * 100).ToString("f3");
                    bankRateTable.Rows[i + 1].Cells[3].Text = (rateList[i].Rate.M06 * 100).ToString("f3");
                    bankRateTable.Rows[i + 1].Cells[4].Text = (rateList[i].Rate.Y01 * 100).ToString("f3");
                    bankRateTable.Rows[i + 1].Cells[5].Text = (rateList[i].Rate.Y02 * 100).ToString("f3");
                    bankRateTable.Rows[i + 1].Cells[6].Text = (rateList[i].Rate.Y03 * 100).ToString("f3");
                    bankRateTable.Rows[i + 1].Cells[7].Text = (rateList[i].Rate.Y05 * 100).ToString("f3");
                }
            }
        }

        private void InsertTableRow(BankRateInfo rateInfo, int row)
        {
            if (bankRateTable.Rows.Count > 10)
            {
                TableCell dateCell = new TableCell();
                dateCell.Text = rateInfo.EffectDate.ToString("yyyy-MM-dd");
                TableCell currCell = new TableCell();
                currCell.Text = (rateInfo.Rate.CurrRate * 100).ToString("f3");
                TableCell m03Cell = new TableCell();
                m03Cell.Text = (rateInfo.Rate.M03 * 100).ToString("f3");
                TableCell m06Cell = new TableCell();
                m06Cell.Text = (rateInfo.Rate.M06 * 100).ToString("f3");
                TableCell y01Cell = new TableCell();
                y01Cell.Text = (rateInfo.Rate.Y01 * 100).ToString("f3");
                TableCell y02Cell = new TableCell();
                y02Cell.Text = (rateInfo.Rate.Y02 * 100).ToString("f3");
                TableCell y03Cell = new TableCell();
                y03Cell.Text = (rateInfo.Rate.Y03 * 100).ToString("f3");
                TableCell y05Cell = new TableCell();
                y05Cell.Text = (rateInfo.Rate.Y05 * 100).ToString("f3");
                
                
                TableRow tr = new TableRow();
                tr.Style["HorizontalAlign"] = "Center";
                tr.Cells.Add(dateCell);
                tr.Cells.Add(currCell);
                tr.Cells.Add(m03Cell);
                tr.Cells.Add(m06Cell);
                tr.Cells.Add(y01Cell);
                tr.Cells.Add(y02Cell);
                tr.Cells.Add(y03Cell);
                tr.Cells.Add(y05Cell);
                bankRateTable.Rows.Add(tr);
            }
        }

        protected void okBtn_Click(object sender, EventArgs e)
        {
            BankRateInfo rate = new BankRateInfo();
            rate.EffectDate = DateTime.Now.Date;
            rate.Rate = new BankRate();
            rate.Rate.CurrRate = decimal.Parse(currentRateTxt.Text.Trim()) / 100;
            rate.Rate.M03 = decimal.Parse(m03RateTxt.Text.Trim()) / 100;
            rate.Rate.M06 = decimal.Parse(m06RateTxt.Text.Trim()) / 100;
            rate.Rate.Y01 = decimal.Parse(y01RateTxt.Text.Trim()) / 100;
            rate.Rate.Y02 = decimal.Parse(y02RateTxt.Text.Trim()) / 100;
            rate.Rate.Y03 = decimal.Parse(y03RateTxt.Text.Trim()) / 100;
            rate.Rate.Y05 = decimal.Parse(y05RateTxt.Text.Trim()) / 100;

            string filePath = ConfigUtil.GetValue(WebConfigName.BankRateTable, "");
            ;
            ;
            if (BizHandler.Handler.SetNewBankRateTable(filePath, rate.Rate) && BizHandler.Handler.AddBankRateInfo(rate))
            {
                TMessageBox.ShowMsg(this, "SetNewBankRate", "利率修改成功！");
            }
            else
            {
                TMessageBox.ShowMsg(this, "SetNewBankRateFailed", "利率修改失败！");
            }
        }
    }
}
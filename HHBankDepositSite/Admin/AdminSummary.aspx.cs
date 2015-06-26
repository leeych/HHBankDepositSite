using HHBankDepositSite.Data;
using Model;
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
            }
            //ScriptManager smg = ScriptManager.GetCurrent(this.Page);
            //if (smg.IsInAsyncPostBack)
            //{
            //    orgCodeTxt.Text = "hello";
            //}
        }

        protected void orgNameDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            orgCodeTxt.Text = WebDataCenter.OrgDict[orgNameDrop.SelectedValue.Trim()];
            orgCodeTxt.DataBind();
        }
    }
    
}
using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHBankDepositSite.Admin
{
    public partial class AdminLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                adminUserNameTxt.Text = Session["UserName"].ToString();
                adminOrgCodeTxt.Text = Session["UserName"].ToString();
                OrgInfo org = BizHandler.Handler.GetOrgInfo(adminUserNameTxt.Text.Trim());
                adminOrgNameTxt.Text = org.OrgName;
                addressTxt.Text = org.OrgAddress;
                phoneTxt.Text = org.OrgPhone;
            }
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Session["UserName"] = null;
            Session["Password"] = null;
            Response.Redirect("~/Login.aspx");
        }
    }
}
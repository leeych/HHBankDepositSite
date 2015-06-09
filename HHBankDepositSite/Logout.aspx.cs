using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHBankDepositSite
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxDataBind();
            userNameTxt.Text = Session["UserName"].ToString();
            orgCodeTxt.Text = userNameTxt.Text;
            OrgInfo org = BizHandler.Handler.GetOrgInfo(userNameTxt.Text.Trim());
            orgNameTxt.Text = org.OrgName;
            orgAddressTxt.Text = org.OrgAddress;
            phoneTxt.Text = org.OrgPhone;
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                Session["UserName"] = null;
                Session["Password"] = null;
            }
            Response.Write("<script language='javascript'>alert('退出成功！')</script>");
            Response.Redirect("~/Login.aspx");
        }

        protected void resetBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void ClearTextBox()
        {
            userNameTxt.Text = string.Empty;
            orgCodeTxt.Text = string.Empty;
            orgNameTxt.Text = string.Empty;
            orgAddressTxt.Text = string.Empty;
            phoneTxt.Text = string.Empty;
        }

        private void TextBoxDataBind()
        {
            userNameTxt.Text = string.Empty;
            orgCodeTxt.Text = string.Empty;
            orgNameTxt.Text = string.Empty;
            orgAddressTxt.Text = string.Empty;
            phoneTxt.Text = string.Empty;
        }
    }
}
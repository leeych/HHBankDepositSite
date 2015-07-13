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
    public partial class UserManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<OrgInfo> orgList = WebDataCenter.OrgList;
                if (orgList == null)
                {
                    return;
                }
                for (int i = 0; i < orgList.Count; i++)
                {
                    if (orgDrop.Items.Contains(new ListItem(orgList[i].OrgName)))
                    {
                        continue;
                    }
                    orgDrop.Items.Add(orgList[i].OrgName);
                    tellerOrgNameDrop.Items.Add(orgList[i].OrgName);
                }
                orgDrop_SelectedIndexChanged(sender, e);
                orgNameDrop_SelectedIndexChanged(sender, e);
                tellerOrgNameDrop_SelectedIndexChanged(sender, e);
            }
            orgCodeTxt.Visible = false;
            //adminUserNameTxt.Text = Session["UserName"].ToString();
            adminUserNameTxt.Text = "3404151476";
            oldpwdCompareValidator.ValueToCompare = Session["Password"].ToString();
        }

        protected void pwdBtn_Click(object sender, EventArgs e)
        {
            string userName = userNameTxt.Text.Trim();
            string password = passwordTxt.Text.Trim();

            BizHandler.Handler.ResetUserPassword(userName, password, userName);
            TMessageBox.ShowMsg(this, "Resetpwd", "密码已重置为 " + password + "!");
        }

        protected void orgNameDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            userNameTxt.Text = WebDataCenter.OrgDict[orgDrop.SelectedValue.Trim()];
        }

        private void GenTellerTableContent(string orgCode)
        {
            List<TellerInfo> totaltellerList = WebDataCenter.TellerList;
            List<TellerInfo> tellerList = new List<TellerInfo>();
            for (int i = 0; i < totaltellerList.Count; i++)
            {
                if (totaltellerList[i].OrgCode == orgCode)
	            {
                    tellerList.Add(totaltellerList[i]);
	            }
            }
            for (int j = 0; j < tellerList.Count; j++)
            {
                tellerTable.Rows[j+1].Cells[0].Text = tellerList[j].TellerCode;
                tellerTable.Rows[j+1].Cells[1].Text = tellerList[j].TellerName;
                tellerTable.Rows[j+1].Cells[2].Text = tellerList[j].OrgCode;
                tellerTable.Rows[j+1].Cells[3].Text = orgDrop.SelectedValue.Trim();
            }
        }

        private void ClearTellerTable()
        {
            for (int i = 1; i < tellerTable.Rows.Count-1; i++)
            {
                for (int j = 0; j < tellerTable.Rows[0].Cells.Count; j++)
                {
                    tellerTable.Rows[i].Cells[j].Text = string.Empty;
                }
            }
        }

        protected void orgDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTellerTable();
            string orgCode = WebDataCenter.OrgDict[orgDrop.SelectedValue.Trim()];
            GenTellerTableContent(orgCode);
            userNameTxt.Text = WebDataCenter.OrgDict[orgDrop.SelectedValue.Trim()];
        }

        protected void tellerOrgNameDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            orgCodeTxt.Text = WebDataCenter.OrgDict[tellerOrgNameDrop.SelectedValue.Trim()];
            orgCodeTxt.Visible = true;
        }

        protected void tellerBtn_Click(object sender, EventArgs e)
        {
            string tellerCode = newTellerCodeTxt.Text.Trim();
            string tellerName = newTellerNameTxt.Text.Trim();
            string orgCode = orgCodeTxt.Text.Trim();
            if (addTellerRbn.Checked)
            {
                if (BizHandler.Handler.AddNewTeller(tellerCode, tellerName, orgCode))
                {
                    TMessageBox.ShowMsg(this, "AddNewTeller", "添加成功！");
                    // TODO: update tellertable
                }
                else
                {
                    TMessageBox.ShowMsg(this, "AddNewTellerFailed", "添加失败！请确认无误后再添加！");
                }
            }
            else if (changeTellerRbn.Checked)
            {
                if (BizHandler.Handler.ChangeTellerOrg(tellerCode, tellerName, orgCode))
                {
                    TMessageBox.ShowMsg(this, "ChangeTellerOrg", "修改成功！");
                    // TODO: update tellertable
                }
            }
        }

        protected void adminPwdBtn_Click(object sender, EventArgs e)
        {
            string adminUserName = adminUserNameTxt.Text.Trim();
            string oldpwd = oldpwdTxt.Text.Trim();
            string newpwd = newpwdTxt.Text.Trim();

            if (BizHandler.Handler.ChangePassword(adminUserName, oldpwd, newpwd) == 1)
            {
                TMessageBox.ShowMsg(this, "AdminChangePwd", "密码修改成功！");
                Session["Password"] = newpwd;
                return;
            }
            else
            {
                TMessageBox.ShowMsg(this, "AdminChangePwdFailed", "密码修改失败！请确认原密码后再试！");
                return;
            }
        }
    }
}
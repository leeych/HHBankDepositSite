<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminLogout.aspx.cs" Inherits="HHBankDepositSite.Admin.AdminLogout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminHeader" runat="server">
    <style type="text/css">
        .mainBody { width:640px; height:400px; text-align: center; margin: 0 auto; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentPlaceHolder" runat="server">
    <div class="mainBody">
        <br />
        <br />
        <table style="width:90%; margin:5px;" cellpadding="5px" cellspacing="5px" align="center">
            <tr>
                <td align="right">用户名：</td>
                <td align="left"><asp:TextBox ID="adminUserNameTxt" runat="server" CssClass="aspTextBoxShort" Width="200px" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">机构号：</td>
                <td align="left"><asp:TextBox ID="adminOrgCodeTxt" runat="server" CssClass="aspTextBoxShort" Width="200px" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">机构名：</td>
                <td align="left"><asp:TextBox ID="adminOrgNameTxt" runat="server" CssClass="aspTextBoxShort" Width="200px" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">地址：</td>
                <td align="left"><asp:TextBox ID="addressTxt" runat="server" CssClass="aspTextBoxShort" Width="200px" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">电话：</td>
                <td align="left"><asp:TextBox ID="phoneTxt" runat="server" CssClass="aspTextBoxShort" Width="200px" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td><br /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="logoutBtn" runat="server" CssClass="aspBtn" Text="退出" OnClick="logoutBtn_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>

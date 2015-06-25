<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminSummary.aspx.cs" Inherits="HHBankDepositSite.Admin.AdminSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentPlaceHolder" runat="server">
   
    <div>
        <table>
            <tr>
                <td>机构名：</td>
                <td><asp:DropDownList ID="orgNameDrop" runat="server" AutoPostBack="true"></asp:DropDownList></td>
                <td>机构号：</td>
                <td><asp:TextBox ID="orgCodeTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                <td>起始日期：</td>
                <td><asp:TextBox ID="startDateTxt" runat="server"></asp:TextBox></td>
                <td>截止日期：</td>
                <td><asp:TextBox ID="endDateTxt" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="searchBtn" runat="server" Text="查询" /></td>
            </tr>
        </table>
        <br />
        <fieldset>
            <legend>概述</legend>

        </fieldset>
    </div>

</asp:Content>

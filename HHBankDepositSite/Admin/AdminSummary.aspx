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
            <legend>概览</legend>
            <table>
                <tr>
                    <td>新存入</td>
                    <td>笔数：<asp:TextBox ID="newNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="newMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>

                    <td>部提</td>
                    <td>笔数：<asp:TextBox ID="adNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="adMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>支取</td>
                    <td>笔数：<asp:TextBox ID="drawNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="drawMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>
                    <td>到期支取</td>
                    <td>笔数：<asp:TextBox ID="dueDrawNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="dueDrawMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>余留本金：</td>
                    <td><asp:TextBox ID="remainTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>
                    <td>补息</td>
                    <td>笔数：<asp:TextBox ID="marginNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="marginMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>明细</legend>
            <table></table>
        </fieldset>
    </div>

</asp:Content>

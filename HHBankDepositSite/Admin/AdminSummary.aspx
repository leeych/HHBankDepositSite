<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminSummary.aspx.cs" Inherits="HHBankDepositSite.Admin.AdminSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminHeader" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 64px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentPlaceHolder" runat="server">
   
    <div>
        <table>
            <tr>
                <td>机构名：</td>
                <td>
                    <asp:DropDownList ID="orgNameDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="orgNameDrop_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>机构号：</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="orgCodeTxt" runat="server" ReadOnly="true"></asp:TextBox>
                        </ContentTemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="orgNameDrop"/>
                        </triggers>
                     </asp:UpdatePanel>
                </td>


                <td>起始日期：</td>
                <td>
                    <asp:TextBox ID="startDateTxt" runat="server"></asp:TextBox>
                    <cc1:calendarextender id="calendarExtenderStart" runat="server" TargetControlID="startDateTxt"
                         Format="yyyy-MM-dd"></cc1:calendarextender>
                </td>
                <td></td>
                <td>截止日期：</td>
                <td>
                    <asp:TextBox ID="endDateTxt" runat="server"></asp:TextBox>
                    <cc1:calendarextender ID="calendarExtenderEnd" runat="server" TargetControlID="endDateTxt"></cc1:calendarextender>
                </td>
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

                    <td class="auto-style1">部提</td>
                    <td>笔数：<asp:TextBox ID="adNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="adMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>支取</td>
                    <td>笔数：<asp:TextBox ID="drawNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="drawMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>
                    <td class="auto-style1">到期支取</td>
                    <td>笔数：<asp:TextBox ID="dueDrawNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="dueDrawMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>余留本金</td>
                    <td>笔数：<asp:TextBox ID="remainNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="remainMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>
                    <td class="auto-style1">补息</td>
                    <td>笔数：<asp:TextBox ID="marginNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="marginMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>系统利息</td>
                    <td>笔数：<asp:TextBox ID="sysNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="sysMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>
                    <td>靠档利息</td>
                    <td>笔数：<asp:TextBox ID="calcNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="calcMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>明细</legend>
            <table></table>
        </fieldset>
    </div>

</asp:Content>

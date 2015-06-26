<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminSummary.aspx.cs" Inherits="HHBankDepositSite.Admin.AdminSummary" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminHeader" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 64px;
        }
.myCalendar .ajax__calendar_container
{
    border:1px solid #646464;
    background-color:Lemonchiffon;
    width:200px;
    text-align:center;
    color:purple;   
    }
    .myCalendar .ajax__calendar_header
    {
        background-color:#0060F8;
        color:white;
    }
.myCalendar .ajax__calendar_other .ajax__calendar_day
{
    font-size:12px;
    color:#faac38;
}
.myCalendar .ajax__calendar_other .ajax__calendar_year
{
    color:green;
}
.myCalendar .ajax__calendar_hover .ajax__calendar_day
{
color:red;
background-color:yellow;
}

.myCalendar .ajax__calendar_active .ajax__calendar_day
{
color:maroon;
font-weight:bolder;
background-color:#e8e8e8;
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
                    <asp:TextBox ID="startDateTxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <ajaxToolkit:calendarextender id="calendarExtenderStart" runat="server" TargetControlID="startDateTxt"
                         Format="yyyy-MM-dd"></ajaxToolkit:calendarextender>
                </td>
                <td></td>
                <td>截止日期：</td>
                <td>
                    <asp:TextBox ID="endDateTxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <ajaxToolkit:calendarextender ID="calendarExtenderEnd" runat="server" TargetControlID="endDateTxt" Format="yyyy-MM-dd"></ajaxToolkit:calendarextender>
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

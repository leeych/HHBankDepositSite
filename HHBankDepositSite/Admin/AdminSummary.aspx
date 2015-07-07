<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminSummary.aspx.cs" EnableEventValidation="false" Inherits="HHBankDepositSite.Admin.AdminSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHeader" runat="server">
    <style type="text/css">
        .auto-style1 {
            color: #FF6600;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentPlaceHolder" runat="server">
    <div>
        <table>
            <tr>
                <td><span class="notetag">机构名：</span></td>
                <td>
                    <asp:DropDownList ID="orgNameDrop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="orgNameDrop_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td><span class="notetag">机构号：</span></td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="orgCodeTxt" runat="server" ReadOnly="true"></asp:TextBox>
                            <asp:RequiredFieldValidator id="orgCodeValidator" runat="server" ErrorMessage="必填！" CssClass="validator" Display="Dynamic" ControlToValidate="orgCodeTxt"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="orgNameDrop"/>
                        </triggers>
                     </asp:UpdatePanel>
                </td>
                <td><span class="notetag">起始日期：</span></td>
                <td>
                    <div style="display:inline;">
                        <span>
                        <asp:TextBox ID="startDateAdminTxt" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderStart" runat="server" TargetControlID="startDateAdminTxt" Format="yyyy-MM-dd" PopupPosition="BottomLeft"></ajaxToolkit:CalendarExtender>
                            </span>
                        <span><asp:RequiredFieldValidator ID="startDateValidator" runat="server" ErrorMessage="必填！" Display="Dynamic" CssClass="validator" ControlToValidate="startDateAdminTxt"></asp:RequiredFieldValidator></span>
                    </div>
                </td>
                <td></td>
                <td><span class="notetag">截止日期：</span></td>
                <td>
                    <span><asp:TextBox ID="endDateAdminTxt" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtenderEnd" runat="server" TargetControlID="endDateAdminTxt" Format="yyyy-MM-dd" PopupPosition="BottomLeft"></ajaxToolkit:CalendarExtender>
                    </span>
                    <span><asp:RequiredFieldValidator ID="endDateValidator" runat="server" ErrorMessage="必填！" Display="Dynamic" CssClass="validator" ControlToValidate="endDateAdminTxt"></asp:RequiredFieldValidator></span>
                </td>
                <td><asp:Button ID="searchBtn" runat="server" Text="查询" OnClick="searchBtn_Click" /></td>
            </tr>

        </table>
        <br />
        <fieldset>
            <legend>概览</legend>
            <table>
                <tr>
                    <td><span class="notetag">新存入</span></td>
                    <td><span class="notetag">笔数：</span><asp:TextBox ID="newNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td><span class="notetag">金额：</span><asp:TextBox ID="newMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>

                    <td><span class="notetag">部提</span></td>
                    <td><span class="notetag">笔数：</span><asp:TextBox ID="adNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td><span class="notetag">金额：</span><asp:TextBox ID="adMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="notetag">支取</span></td>
                    <td><span class="notetag">笔数：</span><asp:TextBox ID="drawNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td><span class="notetag">金额：</span><asp:TextBox ID="drawMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>
                    <td><span class="notetag">到期支取</span></td>
                    <td><span class="notetag">笔数：</span><asp:TextBox ID="dueDrawNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td><span class="notetag">金额：</span><asp:TextBox ID="dueDrawMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="notetag">余留本金</span></td>
                    <td><span class="notetag">笔数：</span><asp:TextBox ID="remainNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td><span class="notetag">金额：</span><asp:TextBox ID="remainMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>
                    <td><span class="notetag">补息</span></td>
                    <td><span class="notetag">笔数：</span><asp:TextBox ID="marginNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td><span class="notetag">金额：</span><asp:TextBox ID="marginMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="notetag">系统利息</span></td>
                    <td><span class="notetag">笔数：</span><asp:TextBox ID="sysNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td><span class="notetag">金额：</span><asp:TextBox ID="sysMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>
                    <td><span class="notetag">靠档利息</span></td>
                    <td><span class="notetag">笔数：</span><asp:TextBox ID="calcNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td><span class="notetag">金额：</span><asp:TextBox ID="calcMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>明细</legend>
            <div style="text-align:center; vertical-align:middle;">
                <table>
                    <tr>
                        <td><asp:Button ID="exportExcelBtn" runat="server" CssClass="aspBtn" Text="导出Excel" OnClick="exportExcelBtn_Click" /></td>
                        <td></td>
                        <td><asp:Button ID="exportTxtBtn" runat="server" CssClass="aspBtn" Text="导出Txt" OnClick="exportTxtBtn_Click" Visible="false" /></td>
                    </tr>
                </table>
                <div style="overflow:scroll; width:100%;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="没有满足条件的记录！" AllowPaging="True" ShowHeaderWhenEmpty="True" OnPageIndexChanging="GridView1_PageIndexChanging" style="margin-bottom: 0px" PageSize="20" OnRowDataBound="GridView1_RowDataBound">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:BoundField DataField="ProtocolID" HeaderText="协议编号" ItemStyle-Wrap="false" HeaderStyle-Wrap="False">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="BillAccount" HeaderText="存单账号" ItemStyle-Wrap="false" HeaderStyle-Wrap="False">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="BillCode" HeaderText="凭证号码" ItemStyle-Wrap="false" HeaderStyle-Wrap="False">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DepositDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="存入日期" ItemStyle-Wrap="false" HeaderStyle-Wrap="False">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DepositMoney" DataFormatString="{0:N2}" HeaderText="本金" ItemStyle-Wrap="false" HeaderStyle-Wrap="False">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="存期" ItemStyle-Wrap="false" HeaderStyle-Wrap="False">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# BillPeriodDict[Eval("BillPeriod").ToString()] %>'></asp:Label>
                            </ItemTemplate>

<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DepositorName" HeaderText="客户姓名" ItemStyle-Wrap="false" HeaderStyle-Wrap="False">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DepositorIDCard" HeaderText="客户身份证号" ItemStyle-Wrap="false" HeaderStyle-Wrap="False">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="存款状态" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# StatusDict[Eval("DepositFlag").ToString()] %>'></asp:Label>
                            </ItemTemplate>

<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RemainMoney" DataFormatString="{0:N2}" HeaderText="余留金额" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="BindAccount" HeaderText="绑定账号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TellerCode" HeaderText="经办柜员号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TellerName" HeaderText="柜员姓名" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CurrentRate" DataFormatString="{0:0.###%}" HeaderText="活期" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="M03Rate" DataFormatString="{0:#.###%}" HeaderText="三个月" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="M06Rate" DataFormatString="{0:#.###%}" HeaderText="六个月" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Y01Rate" DataFormatString="{0:#.###%}" HeaderText="一年" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Y02Rate" DataFormatString="{0:#.###%}" HeaderText="二年" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Y03Rate" DataFormatString="{0:#.###%}" HeaderText="三年" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Y05Rate" DataFormatString="{0:#.###%}" HeaderText="五年" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FirstDrawDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="部提日期" HeaderStyle-Wrap="False" ItemStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FirstDrawMoney" DataFormatString="{0:N2}" HeaderText="部提金额" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FirstSysInterest" DataFormatString="{0:N2}" HeaderText="系统利息" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FirstCalcInterest" DataFormatString="{0:N2}" HeaderText="靠档利息" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FirstMarginInterest" DataFormatString="{0:N2}" HeaderText="补息金额" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FinalDrawDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="支取日期" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FinalDrawMoney" DataFormatString="{0:N2}" HeaderText="支取金额" ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FinalSysInterest" DataFormatString="{0:N2}" HeaderText="系统利息" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FinalCalcInterest" DataFormatString="{0:N2}" HeaderText="靠档利息" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FinalMarginInterest" DataFormatString="{0:N2}" HeaderText="补息金额" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Remark" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <span class="auto-style1"><strong>没有满足条件的记录！</strong></span>
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                    
                </asp:GridView>
                    </div>
            </div>
        </fieldset>
    </div>

</asp:Content>

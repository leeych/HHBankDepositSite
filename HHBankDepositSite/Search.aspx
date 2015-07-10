<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="HHBankDepositSite.Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tableHeader {
            padding: 3px 6px 3px 6px;
        }
        .tableTr {
            border-collapse:collapse;
        }
        .note {
            font-size:13px;
            color: blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div style="text-align: center; vertical-align: middle;" align="center">
        <div style="margin-bottom:10px; text-align: center;" align="center">
        <table align="center" style="width:90%">
            <tr>
                <td><span class="label">机构号：</span></td>
                <td><asp:TextBox runat="server" ID="orgCodeTxt" CssClass="aspTextBox" ReadOnly="true" MaxLength="10" BackColor="#ddf1fe"></asp:TextBox></td>
                <td><span class="label">机构名称：</span></td>  
                <td><asp:TextBox runat="server" ID="orgNameTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
                <td><span class="label">已用最大协议编号：</span></td>
                <td><asp:TextBox runat="server" ID="maxProtocolIdTxt" CssClass="aspTextBox" MaxLength="14" ReadOnly="True" BackColor="#ddf1fe"></asp:TextBox></td>
                <td><span class="label">“保利存”总笔数：</span></td>
                <td><asp:TextBox runat="server" ID="protocolCountTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
            </tr>
        </table>
            <hr />
        </div>

        <div align="center" style="width: 100%; margin: 0 auto;">
            <table style="border: 1px solid #E5E5E5; text-align: center; width:90%" align="center">
                <tr>
                    <td><span class="label">协议编号：</label></td>
                    <td align="left"><asp:TextBox runat="server" ID="protocolIdTxt" CssClass="aspTextBox" MaxLength="14" ></asp:TextBox></td>
                    <td><span class="label">存单账号：</span></td>
                    <td align="left"><asp:TextBox runat="server" ID="billAccountTxt" CssClass="aspBillAccount" MaxLength="23" ></asp:TextBox></td>
                    <td><span class="label">身份证号：</span></td>
                    <td align="left"><asp:TextBox runat="server" ID="idCardTxt" CssClass="aspTextBox" MaxLength="18" ></asp:TextBox></td>            
                    <td><span class="note">(仅需输入一项)</span></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><span class="label">起始日期：</span></td>
                    <td align="left">
                        <span>
                            <asp:TextBox runat="server" ID="startDateTxt" CssClass="aspTextBoxShort"></asp:TextBox>
                            <ajaxtoolkit:calendarextender id="calendarExStartDate" runat="server" TargetControlID="startDateTxt" Format="yyyy-MM-dd" PopupPosition="BottomLeft"></ajaxtoolkit:calendarextender>
                        </span>
                        <span>
                            <%--<asp:RequiredFieldValidator ID="startDateValidator" runat="server" ControlToValidate="startDateTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>--%>
                        </span>
                    </td>
                    <td><span class="label">截止日期：</span></td>
                    <td align="left">
                        <span>
                            <asp:TextBox runat="server" ID="endDateTxt" CssClass="aspTextBoxShort"></asp:TextBox>
                            <ajaxtoolkit:calendarextender id="calendarExEndDate"  runat="server" TargetControlID="endDateTxt" Format="yyyy-MM-dd" PopupPosition="BottomLeft"></ajaxtoolkit:calendarextender>
                        </span>
                        <span>
                            <%--<asp:RequiredFieldValidator ID="endDateValidator" runat="server" ControlToValidate="endDateTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>--%>
                        </span>
                    </td>
                    <td><span class="note"></span></td>
                    <td align="left">
                        <asp:LinkButton ID="linkBtn" runat="server" OnClick="linkBtn_Click" Width="1px"></asp:LinkButton>
                    </td>
                    <td><asp:Button runat="server" ID="searchBtn" CssClass="aspBtn" Text="查询" Height="30px" Width="100px" OnClick="searchBtn_Click" /></td>
                </tr>
            </table>
            <hr />
            <br />
        </div>
        
        <div style="margin:0 auto; text-align:center; width:90%; overflow:auto; vertical-align:middle;">
        <fieldset class="fieldSetStyle" style="text-align:center;margin:3px 5px;" align="center">
            <legend class="legendStyle" align="center">查询结果</legend>
            <asp:GridView ID="orgRecordGv" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" AllowPaging="True" PageSize="15" OnPageIndexChanging="orgRecordGv_PageIndexChanging" OnRowDataBound="orgRecordGv_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ProtocolID" HeaderText="协议编号" Visible="True" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="BillAccount" HeaderText="存单账号" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="BillCode" HeaderText="凭证号码" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DepositDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="存入日期" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DepositMoney" DataFormatString="{0:N2}" HeaderText="本金" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="存期" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# BillPeriodDict[Eval("BillPeriod").ToString()] %>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="DepositorName" HeaderText="客户姓名" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DepositorIDCard" HeaderText="客户身份证号" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="存款状态" ItemStyle-Wrap="False" HeaderStyle-Wrap="False">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# StatusDict[Eval("DepositFlag").ToString()] %>'>'></asp:Label>
                    </ItemTemplate>

<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="RemainMoney" DataFormatString="{0:N2}" HeaderText="余留金额" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="BindAccount" HeaderText="绑定账号" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TellerCode" HeaderText="经办柜员号" HeaderStyle-Wrap="False" ItemStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TellerName" HeaderText="柜员姓名" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CurrentRate" DataFormatString="{0:0.###%}" HeaderText="活期" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="M03Rate" DataFormatString="{0:#.###%}" HeaderText="三个月" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="M06Rate" DataFormatString="{0:#.###%}" HeaderText="六个月" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Y01Rate" DataFormatString="{0:#.###%}" HeaderText="一年" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Y02Rate" DataFormatString="{0:#.###%}" HeaderText="二年" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Y03Rate" DataFormatString="{0:#.###%}" HeaderText="三年" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Y05Rate" DataFormatString="{0:#.###%}" HeaderText="五年" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FirstDrawDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="部提日期" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FirstDrawMoney" DataFormatString="{0:N2}" HeaderText="部提金额" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FirstSysInterest" DataFormatString="{0:N2}" HeaderText="系统利息" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FirstCalcInterest" DataFormatString="{0:N2}" HeaderText="靠档利息" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FirstMarginInterest" DataFormatString="{0:N2}" HeaderText="补息金额" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FinalDrawDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="支取日期" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FinalSysInterest" DataFormatString="{0:N2}" HeaderText="系统利息" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FinalCalcInterest" DataFormatString="{0:N2}" HeaderText="靠档利息" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FinalMarginInterest" DataFormatString="{0:N2}" HeaderText="补息金额" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Remark" HeaderText="备注" ItemStyle-Wrap="False" HeaderStyle-Wrap="False" >
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
                <EmptyDataTemplate>
                    <strong>没有满足条件的记录！</strong>
                </EmptyDataTemplate>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        </fieldset>
        </div>
        <div style="margin:0 auto; clear: both; width:90%; text-align:left;">
            <asp:Button ID="exportExcelBtn" runat="server" Text="导出Excel" CssClass="aspBtn" OnClick="exportExcelBtn_Click" />
        </div>
    </div>
</asp:Content>

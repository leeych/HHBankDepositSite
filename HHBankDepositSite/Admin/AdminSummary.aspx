<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="AdminSummary.aspx.cs" EnableEventValidation="false" Inherits="HHBankDepositSite.Admin.AdminSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminHeader" runat="server">
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
                            <asp:RequiredFieldValidator id="orgCodeValidator" runat="server" ErrorMessage="必填！" CssClass="validator" Display="Dynamic" ControlToValidate="orgCodeTxt"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="orgNameDrop"/>
                        </triggers>
                     </asp:UpdatePanel>
                </td>
                <td>起始日期：</td>
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
                <td>截止日期：</td>
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
                    <td>余留本金</td>
                    <td>笔数：<asp:TextBox ID="remainNumTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td>金额：<asp:TextBox ID="remainMoneyTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td></td>
                    <td>补息</td>
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
            <div style="text-align:center; vertical-align:middle;">
                <table>
                    <tr>
                        <td><asp:Button ID="exportExcelBtn" runat="server" CssClass="aspBtn" Text="导出Excel" /></td>
                        <td></td>
                        <td><asp:Button ID="exportTxtBtn" runat="server" CssClass="aspBtn" Text="导出Txt" OnClick="exportTxtBtn_Click" /></td>
                    </tr>
                </table>

                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="没有满足条件的记录！" AllowPaging="True" ShowHeaderWhenEmpty="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="ProtocolID" HeaderText="协议编号"></asp:BoundField>
                        <asp:BoundField DataField="BillAccount" HeaderText="存单账号"></asp:BoundField>
                        <asp:BoundField DataField="BillCode" HeaderText="凭证号码"></asp:BoundField>
                        <asp:BoundField DataField="DepositDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="存入日期"></asp:BoundField>
                        <asp:BoundField DataField="DepositMoney" DataFormatString="{0:N2}" HeaderText="本金"></asp:BoundField>
                        <asp:BoundField DataField="BillPeriod" HeaderText="存期"></asp:BoundField>
                        <asp:BoundField DataField="ClientName" HeaderText="客户姓名"></asp:BoundField>
                        <asp:BoundField DataField="ClientIDCard" HeaderText="客户身份证号"></asp:BoundField>
                        <asp:BoundField DataField="DepositStatus" HeaderText="状态"></asp:BoundField>
                        <asp:BoundField DataField="BindAccount" HeaderText="绑定账号"></asp:BoundField>
                        <asp:BoundField DataField="TellerCode" HeaderText="经办柜员号"></asp:BoundField>
                        <asp:BoundField DataField="TellerName" HeaderText="柜员姓名"></asp:BoundField>
                        <asp:BoundField DataField="Remark" HeaderText="备注"></asp:BoundField>
                        <asp:BoundField></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table id="emptyTable" border="1" cellpadding="3" cellspacing="3" rules="all" style="background-color:White;border-color:#999999;border-width:1px;border-style:solid;border-collapse:collapse;">
                            <tr style="color:White;background-color:#000084;font-weight:bold;">
                                <th scope="col">协议编号</th>
                                <th scope="col">存单账号</th>
                                <th scope="col">凭证号码</th>
                                <th scope="col">存入日期</th>
                                <th scope="col">本金</th>
                                <th scope="col">存期</th>
                                <th scope="col">客户姓名</th>
                                <th scope="col">客户身份证号</th>
                                <th scope="col">状态</th>
                                <th scope="col">绑定账号</th>
                                <th scope="col">经办柜员号</th>
                                <th scope="col">柜员姓名</th>
                                <th scope="col">备注</th>
                            </tr>
                            <tr>
                                <td colspan="13"><h4 style="color:red;">没有满足条件的记录！</h4></td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </div>
        </fieldset>
    </div>

</asp:Content>

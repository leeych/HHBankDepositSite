<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="HHBankDepositSite.Search" %>
<%--<%@ Register Assembly="HHBankDepositSite" Namespace="HHBankDepositSite" TagPrefix="Lee"%>--%>
<%--<%@ Register Assembly="jzlib" Namespace="jzlib.asp.net.Controls" TagPrefix="Lee" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tableHeader {
            padding: 3px 6px 3px 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div style="float: left;">
        <div id="gridHeader" style="margin-bottom:10px;">
        <table>
            <tr>
                <td>机构号：</td>
                <td><asp:TextBox runat="server" ID="orgCodeTxt" CssClass="aspTextBox" ReadOnly="true" MaxLength="10" BackColor="#ddf1fe"></asp:TextBox></td>
                <td>机构名称：</td>  
                <td><asp:TextBox runat="server" ID="orgNameTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
                <td>已用最大协议编号：</td>
                <td><asp:TextBox runat="server" ID="maxProtocolIdTxt" CssClass="aspTextBox" MaxLength="14" ReadOnly="True" BackColor="#ddf1fe"></asp:TextBox></td>
                <td>“保利存”总笔数：</td>
                <td><asp:TextBox runat="server" ID="protocolCountTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
            </tr>
        </table>
        </div>

        <div>
        <table style="border: 1px solid #E5E5E5; text-align: center; vertical-align: middle;" align="center">
            <tr>
                <td>协议编号：</td>
                <td><asp:TextBox runat="server" ID="protocolIdTxt" CssClass="aspTextBox" MaxLength="14" PlaceHolder="协议编号"></asp:TextBox></td>
            
            
                <td><span class="red-star">*</span>存单账号：</td>
                <td><asp:TextBox runat="server" ID="billAccountTxt" CssClass="aspBillAccount" MaxLength="23" PlaceHolder="存单账号"></asp:TextBox></td>
            
            
                <td><span class="red-star">*</span>身份证号：</td>
                <td><asp:TextBox runat="server" ID="idCardTxt" CssClass="aspTextBox" MaxLength="18" PlaceHolder="身份证号码"></asp:TextBox></td>
            
            
                <td><span class="red-star">*</span>客户姓名：</td>
                <td><asp:TextBox runat="server" ID="clientNameTxt" CssClass="aspTextBox"></asp:TextBox></td>
            </tr>
        </table>
            </div>
        <div>
<%--                <Lee:TGridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" Height="321px" Width="710px" HeaderStyle-CssClass="fixedHeader" Font-Size="12px">
                <Columns>
                    <asp:BoundField DataField="ProtocolID" HeaderText="协议编号" />
                    <asp:BoundField DataField="BillAccount" HeaderText="存单账号" />
                    <asp:BoundField DataField="BillCode" HeaderText="凭证号码" />
                    <asp:BoundField DataField="DepositMoney" HeaderText="本金" />
                    <asp:BoundField DataField="ClientName" HeaderText="客户姓名" />
                    <asp:BoundField DataField="ClientID" HeaderText="客户身份证" />
                    <asp:BoundField DataField="DepositDate" HeaderText="存入日期" />
                    <asp:BoundField DataField="BillPeriod" HeaderText="存期" />
                    <asp:BoundField DataField="ExecRate" HeaderText="利率" />
                    <asp:BoundField DataField="DueInterest" HeaderText="到期利息" />
                    <asp:BoundField DataField="TellerCode" HeaderText="经办人" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            </Lee:TGridView>--%>

<%--            <Lee:SmartGridView ID="sgv" runat="server" AllowPaging="true" AllowSorting="true" PageSize="20" MouseOverCssClass="OverRow" OnPageIndexChanging="sgv_PageIndexChanging"
                 DataKeyNames="ProtocolID" Width="1200" BoundRowDoubleClickCommandName="DoubleClick" OnRowCommand="sgv_RowCommand" ContextMenuCssClass="RightMenu">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="50px">
                        <HeaderTemplate>
                            序号
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="50px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="all2" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="item2" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProtocolID" HeaderText="协议编号" SortExpression="ProtocolID" />
                    <asp:BoundField DataField="BillAccount" HeaderText="存单账号" />
                    <asp:BoundField DataField="BillCode" HeaderText="凭证号" />
                    <asp:BoundField DataField="DepositDate" HeaderText="存入日期" />
                    <asp:BoundField DataField="DepositMoney" HeaderText="本金" />
                    <asp:BoundField DataField="BillPeriod" HeaderText="存期" />
                    <asp:BoundField DataField="ExecRate" HeaderText="利率" />
                    <asp:BoundField DataField="DueInterest" HeaderText="到期利息" />
                    <asp:BoundField DataField="ClientName" HeaderText="客户姓名" />
                    <asp:BoundField DataField="ClientID" HeaderText="身份证号" />
                    <asp:BoundField DataField="Status" HeaderText="状态" />
                    <asp:ButtonField CommandName="DoubleClick" Visible="false" />
                    <asp:ButtonField CommandName="RightMenuClick" Visible="false" />
                </Columns>
                <SmartSorting AllowMultiSorting="true" AllowSortTip="true" />
                <ClientButtons>
                    <Lee:ClientButton BoundCommandName="Sort" Position="First" AttributeKey="onclick"
                         AttributeValue="return confirm('确认对字段“{1}”排序吗？')" />
                </ClientButtons>
                <CascadeCheckboxes>
                    <Lee:CascadeCheckbox ChildCheckboxID="item2" ParentCheckboxID="all2" />
                </CascadeCheckboxes>
                <FixRowColumn FixRowType="Header,Pager" FixRows="0" FixColumns="0,1" />
                <CheckedRowCssClass CheckBoxID="item2" CssClass="SelectedRow" />
                <ContextMenus>
                    <Lee:ContextMenu Text="RightMenuClick" BoundCommandName="RightMenuClick" />
                    <Lee:ContextMenu Text="<br />" />
                </ContextMenus>
                <CustomPagerSettings PagingMode="Webabcd" TextFormat="每页{0}条/共{1}条&nbsp;&nbsp;&nbsp;&nbsp;第{2}页/共{3}页&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                <PagerSettings Position="Top" PageButtonCount="13" FirstPageText="首页" PreviousPageText="上一页" NextPageText="下一页"
                     LastPageText="尾页" />
            </Lee:SmartGridView>--%>
            <asp:Table ID="resultTable" runat="server" CellSpacing="3" CellPadding="3" Width="100%" GridLines="Both" BorderColor="Black" BorderStyle="Solid">
                <asp:TableHeaderRow ID="resHeader" runat="server" BackColor="#669cc0" ForeColor="#FFFFFF" CssClass="tableHeader">
                    <asp:TableHeaderCell ID="protocolIdCell" Wrap="false" runat="server" Text="协议编号"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="billAccountCell" Wrap="false" runat="server" Text="存单账号"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="billCodeCell" Wrap="false" runat="server" Text="凭证号码"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="depositMoneyCell" Wrap="false" runat="server" Text="存入金额"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="depositDateCell" Wrap="false" runat="server" Text="存入日期"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="billPeriodCell" Wrap="false" runat="server" Text="存期"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="rateCell" Wrap="false" runat="server" Text="利率"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="depositStatus" Wrap="false" runat="server" Text="存款状态"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="clientNameCell" Wrap="false" runat="server" Text="客户姓名"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="clientIDCell" Wrap="false" runat="server" Text="身份证号码"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="tellerCodeCell" Wrap="false" runat="server" Text="经办人"></asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow ID="TableRow1" runat="server" Height="30px">
                    <asp:TableCell ID="TableCell1" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell2" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell3" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell4" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell5" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell6" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell7" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell8" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell9" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell10" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell11" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server" Height="30px">
                    <asp:TableCell ID="TableCell12" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell13" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell14" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell15" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell16" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell17" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell18" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell19" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell20" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell21" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell22" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow3" runat="server" Height="30px">
                    <asp:TableCell ID="TableCell23" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell24" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell25" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell26" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell27" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell28" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell29" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell30" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell31" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell32" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell33" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow4" runat="server" Height="30px">
                    <asp:TableCell ID="TableCell34" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell35" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell36" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell37" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell38" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell39" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell40" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell41" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell42" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell43" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell44" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow5" runat="server" Height="30px">
                    <asp:TableCell ID="TableCell45" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell46" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell47" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell48" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell49" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell50" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell51" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell52" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell53" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell54" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell55" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow6" runat="server" Height="30px">
                    <asp:TableCell ID="TableCell56" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell57" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell58" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell59" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell60" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell61" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell62" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell63" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell64" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell65" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell66" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow7" runat="server" Height="30px">
                    <asp:TableCell ID="TableCell67" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell68" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell69" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell70" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell71" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell72" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell73" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell74" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell75" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell76" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell77" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow8" runat="server" Height="30px">
                    <asp:TableCell ID="TableCell78" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell79" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell80" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell81" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell82" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell83" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell84" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell85" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell86" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell87" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell88" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow9" runat="server" Height="30px">
                    <asp:TableCell ID="TableCell89" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell90" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell91" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell92" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell93" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell94" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell95" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell96" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell97" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell98" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell99" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow10" runat="server" Height="30px">
                    <asp:TableCell ID="TableCell100" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell101" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell102" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell103" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell104" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell105" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell106" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell107" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell108" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell109" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell110" runat="server"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>

        <div style="clear: both;">
            <div style="text-align:center;"><asp:Button runat="server" ID="searchBtn" CssClass="s" Text="查询" OnClick="searchBtn_Click" /></div>
        </div>
        </div>
</asp:Content>

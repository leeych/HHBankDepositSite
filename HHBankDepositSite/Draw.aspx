<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Draw.aspx.cs" Inherits="HHBankDepositSite.Draw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">
            function displayCalendar() {
                if (datePicker.style.display == 'block') {
                    datePicker.style.display = 'none';
                    return;
                }
                var dataPicker = document.getElementById('datePicker');
                datePicker.style.display = 'block';
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <fieldset class="fieldSetStyle">
        <legend class="legendStyle">查询</legend>
        <table style="border: 1px solid #E5E5E5; text-align: center; vertical-align: middle;" align="center">
            <tr>
                <td><span class="red-star">*</span>协议编号：</td>
                <td><asp:TextBox runat="server" CssClass="aspTextBox" ID="protocolIDTxt" MaxLength="14"></asp:TextBox></td>
                <td><span class="red-star">*</span>存单账号：</td>
                <td><asp:TextBox runat="server" CssClass="aspBillAccount" ID="billAccountTxt" Width="200px" MaxLength="23"></asp:TextBox></td>
                <td><span class="red-star">*</span>凭证号：</td>
                <td><asp:TextBox runat="server" CssClass="aspTextBox" ID="billCodeTxt" MaxLength="12"></asp:TextBox></td>
                <td><asp:Button runat="server" Height="30px" Width="100px" CssClass="aspBtn" ID="searchBtn" Text="查询" OnClick="searchBtn_Click"/></td>
            </tr>
            <tr>
                <td colspan="7">
                    <%--<asp:GridView ID="recordGridView" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" Height="121px" Width="867px" AllowPaging="True" EmptyDataText="没有记录！">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundField HeaderText="协议号" />
                            <asp:BoundField HeaderText="账号" />
                            <asp:BoundField HeaderText="凭证号" />
                            <asp:BoundField HeaderText="本金" />
                            <asp:BoundField HeaderText="存期" />
                            <asp:BoundField HeaderText="利率" />
                            <asp:BoundField HeaderText="姓名" />
                            <asp:BoundField HeaderText="身份证" />
                            <asp:BoundField HeaderText="经办人" />
                        </Columns>
                        <EmptyDataTemplate>
                            <table style="width:100%;">
                                <tr>
                                    <td>协议号</td>
                                    <td>账号</td>
                                    <td>凭证号</td>
                                    
                                    <td>本金</td>
                                    <td>存期</td>
                                    <td>利率</td>

                                    <td>姓名</td>
                                    <td>身份证</td>
                                    <td>经办人</td>
                                </tr>
                                <tr>
                                    <td colspan="9">没有记录！</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    </asp:GridView>--%>
                </td>
            </tr>
            </table>

        <table style="border: 1px solid #E5E5E5; text-align: center; vertical-align: middle;" align="center">
            <tr>
                <td>存入日期：</td>
                <td><asp:TextBox runat="server" ID="depositDateTxt" CssClass="aspTextBox" ReadOnly="true"></asp:TextBox></td>
                <td>约定存期：</td>
                <%--<td><asp:DropDownList runat="server" ID="periodDrop" CssClass="aspTextBox" OnSelectedIndexChanged="periodDrop_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>三个月</asp:ListItem>
                    <asp:ListItem>六个月</asp:ListItem>
                    <asp:ListItem>一年</asp:ListItem>
                    <asp:ListItem>二年</asp:ListItem>
                    <asp:ListItem>三年</asp:ListItem>
                    <asp:ListItem>五年</asp:ListItem>
                    </asp:DropDownList></td>--%>
                <td><asp:TextBox runat="server" ID="periodTxt" CssClass="aspTextBox" ReadOnly="true"></asp:TextBox></td>
                <td>执行利率：</td>
                <td><span><asp:TextBox runat="server" ID="execRateTxt" CssClass="aspTextBox" ReadOnly="True"></asp:TextBox></span><span class="per-cent">%</span></td>
            </tr>
            <tr>
                <td>到期日期：</td>
                <td><asp:TextBox runat="server" ID="dueDateTxt" CssClass="aspTextBox" ReadOnly="True"></asp:TextBox></td>
                <td>存入本金：</td>
                <td><asp:TextBox runat="server" ID="moneyTxt" CssClass="aspTextBox" ReadOnly="True"></asp:TextBox></td>
                <td>系统利息：</td>
                <td><asp:TextBox runat="server" ID="systemInterestTxt" CssClass="aspTextBox" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td>客户姓名：</td>
                <td><asp:TextBox runat="server" ID="clientNameTxt" CssClass="aspTextBox" ReadOnly="True"></asp:TextBox></td>
                <td>身份证号：</td>
                <td><asp:TextBox runat="server" ID="clientIDTxt" CssClass="aspTextBox" ReadOnly="True" MaxLength="18"></asp:TextBox></td>
                <td>是否支取：</td>
                <td><asp:TextBox runat="server" ID="drawStatusTxt" CssClass="aspTextBox" ReadOnly="true"></asp:TextBox></td>
                
            </tr>
            <tr style="height: 200%">
                <td>经办柜员：</td>
                <td><asp:TextBox runat="server" ID="tellerCodeTxt" CssClass="aspTextBox" ReadOnly="True" MaxLength="6"></asp:TextBox></td>
                <td><asp:Label runat="server" ID="tellerNameTxt"></asp:Label></td>
                <td align="right">补息账号：</td>
                <td colspan="2"><asp:TextBox runat="server" ID="bindAccountTxt" CssClass="aspBillAccount" MaxLength="23" Width="90%"></asp:TextBox></td>
                </tr>
            <tr>
                <td>备注：</td>
                <td colspan="7"><asp:TextBox runat="server" ID="remarkTxt" TextMode="MultiLine" Width="100%" ReadOnly="True"></asp:TextBox></td>
            </tr>
        </table>
    </fieldset>
    </div>

    <fieldset class="fieldSetStyle">
        <legend class="legendStyle">支取</legend>
        <table  style="border: 1px solid #E5E5E5; text-align: center;" align="center" >
            <tr>
                <td><span class="red-star">*</span>支取日期：</td>
                <td>
                    <div style="display: inline;">
                        <span><asp:TextBox ID="drawDateTxt" runat="server" TextMode="SingleLine" CssClass="aspTextBox"></asp:TextBox></span>
                        <span><img src="Images/calendar.png" width="24px" height="24px" alt="Calendar" onclick="displayCalendar()" style="vertical-align: middle;"/></span>
                        <div id="datePicker">
                            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" OnDayRender="Calendar1_DayRender">
                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                            <WeekendDayStyle BackColor="#CCCCFF" />
                            </asp:Calendar>
                        </div>
                    </div>
                 </td>
                <td><span class="red-star">*</span>支取金额：</td>
                <td><asp:TextBox runat="server" ID="moneyDrawTxt" CssClass="aspTextBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td>靠档方案：</td>
                <td align="left" colspan="3"><asp:TextBox runat="server" ID="sectionTxt" CssClass="aspTextBox" ReadOnly="True" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td>系统本息：</td>
                <td><asp:TextBox runat="server" ID="systemTxt" CssClass="aspTextBox" ReadOnly="True"></asp:TextBox></td>
                <td>靠档本息：</td>
                <td><asp:TextBox runat="server" ID="totalInterestTxt" CssClass="aspTextBox" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td><h3 style="color:red;">利息差额：</h3></td>
                <td><asp:TextBox runat="server" ID="marginTxt" CssClass="aspMargin" ReadOnly="True"></asp:TextBox></td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset class="fieldSetStyle" style="text-align: center;">
        <legend class="legendStyle"></legend>
        <div style="display: inline; margin: 0px auto;">
            <span><asp:Button ID="calcBtn" runat="server" Text="计算" Height="30px" Width="100px" CssClass="aspBtn" OnClick="calcBtn_Click" /></span>
            <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
            <span><asp:Button ID="okBtn" runat="server" Text="支取" Height="30px" Width="100px" CssClass="aspBtn" OnClick="okBtn_Click" /></span>
        </div>
    </fieldset>
</asp:Content>

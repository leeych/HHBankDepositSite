<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Draw.aspx.cs" Inherits="HHBankDepositSite.Draw" MaintainScrollPositionOnPostback="true" %>
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
    <div width="100%">
        <br />
    <fieldset class="fieldSetStyle">
        <legend class="legendStyle">查询</legend>
        <table width="80%" style="border: 1px solid #E5E5E5; text-align: center; vertical-align: middle;" align="center">
            <tr>
                <td><span class="red-star">*</span>协议编号：</td>
                <td align="left"><asp:TextBox runat="server" CssClass="aspTextBox" ID="protocolIDTxt" MaxLength="14"></asp:TextBox><span><asp:RequiredFieldValidator runat="server" ID="protocolIdValidator" CssClass="validator" ControlToValidate="protocolIDTxt" Display="Dynamic" ErrorMessage="必填！" ValidationGroup="grpSearch"></asp:RequiredFieldValidator></span></td>
                <td><span class="red-star">*</span>存单账号：</td>
                <td align="left"><asp:TextBox runat="server" CssClass="aspBillAccount" ID="billAccountTxt" Width="200px" MaxLength="23"></asp:TextBox><span><asp:RequiredFieldValidator runat="server" ID="accountValidator" CssClass="validator" ControlToValidate="billAccountTxt" Display="Dynamic" ErrorMessage="必填！" ValidationGroup="grpSearch"></asp:RequiredFieldValidator></span></td>
                <td align="left"><asp:Button runat="server" Height="30px" Width="100px" CssClass="aspBtn" ID="searchBtn" Text="查询" ValidationGroup="grpSearch" OnClick="searchBtn_Click"/></td>
            </tr>
            </table>

        <table width="80%" style="border: 1px solid #E5E5E5; text-align: center; vertical-align: middle;" align="center">
            <tr>
                <td align="right">存入日期：</td>
                <td><asp:TextBox runat="server" ID="depositDateTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
                <td align="right">约定存期：</td>
                <td><asp:TextBox runat="server" ID="periodTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
                <td align="right">执行利率：</td>
                <td><span><asp:TextBox runat="server" ID="execRateTxt" CssClass="aspTextBox" ReadOnly="True" BackColor="#ddf1fe"></asp:TextBox></span><span class="per-cent">%</span></td>
            </tr>
            <tr>
                <td align="right">到期日期：</td>
                <td><asp:TextBox runat="server" ID="dueDateTxt" CssClass="aspTextBox" ReadOnly="True" BackColor="#ddf1fe"></asp:TextBox></td>
                <td align="right">存入本金：</td>
                <td><asp:TextBox runat="server" ID="moneyTxt" CssClass="aspTextBox" ReadOnly="True" BackColor="#ddf1fe"></asp:TextBox></td>
                <td align="right">到期利息：</td>
                <td><span><asp:TextBox runat="server" ID="systemInterestTxt" CssClass="aspTextBox" ReadOnly="True" BackColor="#ddf1fe"></asp:TextBox></span></td>
            </tr>
            <tr>
                <td align="right">客户姓名：</td>
                <td><asp:TextBox runat="server" ID="clientNameTxt" CssClass="aspTextBox" ReadOnly="True" BackColor="#ddf1fe"></asp:TextBox></td>
                <td align="right">身份证号：</td>
                <td><asp:TextBox runat="server" ID="clientIDTxt" CssClass="aspTextBox" ReadOnly="True" MaxLength="18" BackColor="#ddf1fe"></asp:TextBox></td>
                <td align="right">支取标志：</td>
                <td><asp:TextBox runat="server" ID="drawStatusTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td align="right">经办柜员：</td>
                <td><asp:TextBox runat="server" ID="tellerCodeTxt" CssClass="aspTextBox" ReadOnly="True" MaxLength="6" BackColor="#ddf1fe"></asp:TextBox></td>
                <td><asp:Label runat="server" ID="tellerNameTxt"></asp:Label></td>
                <td align="right">补息账号：</td>
                <td colspan="2"><asp:TextBox runat="server" ID="bindAccountTxt" CssClass="aspBillAccount" MaxLength="23" Width="90%" BackColor="#ddf1fe"></asp:TextBox></td>
                </tr>
            <tr>
                <td align="right">已部提本金：</td>
                <td><asp:TextBox runat="server" ID="adDrawMoneyTxt" CssClass="aspTextBox"  ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
                <td align="right">部提日期：</td>
                <td><asp:TextBox runat="server" ID="adDrawDateTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
                <td align="right">余留本金：</td>
                <td><asp:TextBox runat="server" ID="remainMoneyTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">系统利息：</td>
                <td><asp:TextBox runat="server" ID="adSysInterestTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
                <td align="right">已靠档利息：</td>
                <td><asp:TextBox runat="server" ID="adSectionInterestTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
                <td align="right">已补息金额：</td>
                <td><asp:TextBox runat="server" ID="adMarginInterestTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
            </tr>
            <tr>
                <td>备注：</td>
                <td colspan="7"><asp:TextBox runat="server" ID="remarkTxt" TextMode="MultiLine" Width="95%" ReadOnly="True" BackColor="#ddf1fe"></asp:TextBox></td>
            </tr>
        </table>
    </fieldset>
    </div>

    <fieldset class="fieldSetStyle">
        <legend class="legendStyle">支取</legend>
        <table width="80%" style="border: 1px solid #E5E5E5; text-align: center;" align="center" >
            <tr>
                <td align="right"><span class="red-star">*</span>支取日期：</td>
                <td align="left">
                    <div style="display: inline;">
                        <span><asp:TextBox ID="drawDateTxt" runat="server" TextMode="SingleLine" CssClass="aspTextBox" ReadOnly="True" Width="130px"></asp:TextBox></span>
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
                    <span><asp:RequiredFieldValidator runat="server" ID="dateValidator" CssClass="validator" ControlToValidate="drawDateTxt" Display="Dynamic" ErrorMessage="必填！"></asp:RequiredFieldValidator></span>
                 </td>
                <td align="left"></td>
                <td align="right"><span class="red-star">*</span>支取金额：</td>
                <td align="left"><span><asp:TextBox runat="server" ID="moneyDrawTxt" CssClass="aspTextBox"></asp:TextBox></span><span><asp:RequiredFieldValidator runat="server" ID="moneyValidator" CssClass="validator" ControlToValidate="moneyDrawTxt" Display="Dynamic" ErrorMessage="必填！"></asp:RequiredFieldValidator></span></td>
                <%--<td align="left"></td>--%>
                <td align="right"><asp:Button ID="calcBtn" runat="server" Text="计算" Height="30px" Width="100px" CssClass="aspBtn" OnClick="calcBtn_Click" /></td>
                <td align="left">&nbsp;</td>
            </tr>
            <tr>
                <td align="right" style="height:200%">靠档方案：</td>
                <td align="left" colspan="7"><asp:TextBox runat="server" ID="sectionTxt" CssClass="aspTextBox" ReadOnly="True" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">系统本息：</td>
                <td align="left"><asp:TextBox runat="server" ID="systemTxt" CssClass="aspTextBox" ReadOnly="True"></asp:TextBox></td>
                <td></td>
                <td align="right">靠档本息：</td>
                <td align="left"><asp:TextBox runat="server" ID="totalInterestTxt" CssClass="aspTextBox" ReadOnly="True"></asp:TextBox></td>
                <%--<td></td>--%>
                <td align="right"><h3 style="color:red;">应补利息：</h3></td>
                <td align="left"><asp:TextBox runat="server" ID="marginTxt" CssClass="aspTextBox" ReadOnly="True"></asp:TextBox></td>
            </tr>
            </table>
    </fieldset>
    <br />
    <fieldset class="fieldSetStyle" style="text-align: center;">
        <legend class="legendStyle"></legend>
        <div style="display: inline; margin: 0px auto;">
            <span></span>
            <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
            <span><asp:Button ID="okBtn" runat="server" Text="支取" Height="30px" Width="100px" CssClass="aspBtn" OnClick="okBtn_Click" /></span>
        </div>
    </fieldset>
</asp:Content>

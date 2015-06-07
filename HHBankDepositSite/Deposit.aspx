<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Deposit.aspx.cs" Inherits="HHBankDepositSite.Deposit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <div style=" width:100%; height:100%;">
        <fieldset style="height:inherit; text-align: center;">
            <legend>交易信息</legend>
            <table align="center" cellpadding="5px" cellspacing="5px">
                <tr>
                    <td>协议号：</td>
                    <td><asp:TextBox ID="protocolTxt" runat="server"></asp:TextBox></td>
                    <td>存单账号：</td>
                    <td><asp:TextBox ID="billAccountTxt" runat="server"></asp:TextBox></td>
                    <td>凭证号码：</td>
                    <td><asp:TextBox ID="billCodeTxt" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>约定存期：</td>
                    <td>
                        <asp:DropDownList ID="periodDrop" runat="server" Height="19px" style="margin-left: 0px" Width="148px" OnSelectedIndexChanged="periodDrop_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>三个月</asp:ListItem>
                            <asp:ListItem>六个月</asp:ListItem>
                            <asp:ListItem>一年</asp:ListItem>
                            <asp:ListItem>二年</asp:ListItem>
                            <asp:ListItem>三年</asp:ListItem>
                            <asp:ListItem>五年</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>约定利率：</td>
                    <td><asp:TextBox ID="rateTxt" runat="server" Width="131px" OnTextChanged="rateTxt_TextChanged"></asp:TextBox>%</td>
                    <td>存入金额：</td>
                    <td><asp:TextBox ID="moneyTxt" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>存入日期：</td>
                    <td style="width: 148px;">
                        <div style="display: inline;">
                            <asp:TextBox ID="dateTxt" runat="server" TextMode="SingleLine" Width="108px"></asp:TextBox>
                            <img src="Images/calendar.png" width="20px" height="20px" alt="Calendar" onclick="displayCalendar()" style="vertical-align: middle;"/>
                            <div id="datePicker">
                                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged1" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px">
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
                    <td>补息账号：</td>
                    <td colspan="5" align="left"><asp:TextBox ID="bindAccountTxt" runat="server" style="margin-left: 0px;" Width="247px"></asp:TextBox>

                    </td>
                </tr>
            </table>
        </fieldset>
            <br />
            <br />
        <fieldset>
        <legend>经办信息</legend>
        <table align="center" cellpadding="5px" cellspacing="5px">
            <tr>
                <td>客户身份证：</td>
                <td><asp:TextBox ID="IDCardTxt" runat="server"></asp:TextBox></td>
                <td>客户姓名：</td>
                <td><asp:TextBox ID="nameTxt" runat="server"></asp:TextBox></td>
                <td>经办柜员号：</td>
                <td><asp:TextBox ID="tellerCodeTxt" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>备注</td>
                <td colspan="5"><asp:TextBox runat="server" Height="63px" Width="623px" TextMode="MultiLine" ID="remarkTxt"></asp:TextBox></td>
            </tr>
        </table>
        </fieldset>
            <fieldset style="vertical-align: middle; text-align: center;">
                <legend></legend>
                <div id="bottom" style="display: inline; margin: 0px auto;">
                    <span><asp:Button ID="depositBtn" runat="server" Text="存入" Height="30px" Width="100px" CssClass="aspBtn" OnClick="depositBtn_Click"/></span>
                    <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                    <span><asp:Button ID="cancelBtn" runat="server" Text="取消" Height="30px" Width="100px" CssClass="aspBtn" OnClick="cancelBtn_Click"/></span>
                </div>
            </fieldset>
    </div>

</asp:Content>

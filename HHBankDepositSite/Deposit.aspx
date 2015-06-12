<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Deposit.aspx.cs" Inherits="HHBankDepositSite.Deposit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">
        function displayCalendar() {
            if (datePicker.style.display == 'block') {
                datePicker.style.display = 'none';
                return;
            }
            var dataPicker = document.getElementById('datePicker');
            datePicker.style.display = 'block';
        }

    </script>

        <div style="width:100%; height:100%;">
            <br />
        <fieldset class="fieldSetStyle">
            <legend class="legendStyle">交易信息</legend>
            <table align="center" cellpadding="5px" cellspacing="5px" style="border: 1px solid #E5E5E5; border-spacing: 0px;">
                <tr>
                    <td><span class="red-star">*</span>协议编号：</td>
                    <td><span><asp:TextBox ID="protocolTxt" runat="server" CssClass="aspTextBox" AutoPostBack="True" MaxLength="14" OnTextChanged="protocolTxt_TextChanged" onblur="checkID()"></asp:TextBox></span></td>
                    <td><span class="red-star">*</span>存单账号：</td>
                    <td><asp:TextBox ID="billAccountTxt" runat="server" CssClass="aspBillAccount" Width="200px" AutoPostBack="True" MaxLength="23" OnTextChanged="billAccountTxt_TextChanged"></asp:TextBox></td>
                    <td><span class="red-star">*</span>凭证号码：</td>
                    <td><asp:TextBox ID="billCodeTxt" runat="server" CssClass="aspTextBox" AutoPostBack="True" MaxLength="12" OnTextChanged="billCodeTxt_TextChanged"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span class="red-star">*</span>约定存期：</td>
                    <td>
                        <asp:DropDownList ID="periodDrop" runat="server" OnSelectedIndexChanged="periodDrop_SelectedIndexChanged" AutoPostBack="True" CssClass="aspTextBox">
                            <asp:ListItem>三个月</asp:ListItem>
                            <asp:ListItem>六个月</asp:ListItem>
                            <asp:ListItem>一年</asp:ListItem>
                            <asp:ListItem>二年</asp:ListItem>
                            <asp:ListItem>三年</asp:ListItem>
                            <asp:ListItem>五年</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>约定利率：</td>
                    <td><asp:TextBox ID="rateTxt" runat="server" Width="131px" OnTextChanged="rateTxt_TextChanged" CssClass="aspTextBoxShort" ReadOnly="True"></asp:TextBox><span class="per-cent">%</span></td>
                    <td><span class="red-star">*</span>存入金额：</td>
                    <td><span class="per-cent">￥</span><span><asp:TextBox ID="moneyTxt" runat="server" CssClass="aspTextBoxShort" AutoPostBack="True" OnTextChanged="moneyTxt_TextChanged"></asp:TextBox></span><span class="per-cent">元</span></td>
                </tr>
                <tr>
                    <td><span class="red-star">*</span>存入日期：</td>
                    <td style="width: 148px;">
                        <div style="display: inline;">
                            <asp:TextBox ID="dateTxt" runat="server" TextMode="SingleLine" Width="108px" CssClass="aspTextBoxShort" ReadOnly="True"></asp:TextBox>
                            <img src="Images/calendar.png" width="24px" height="24px" alt="Calendar" onclick="displayCalendar()" style="vertical-align: middle;"/>
                            <div id="datePicker">
                                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged1" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px" OnDayRender="Calendar1_DayRender">
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
                    <td><span class="red-star">*</span>补息账号：</td>
                    <td colspan="5" align="left"><asp:TextBox ID="bindAccountTxt" runat="server" style="margin-left: 0px;" Width="299px" CssClass="aspTextBox" AutoPostBack="True" MaxLength="23" OnTextChanged="bindAccountTxt_TextChanged"></asp:TextBox>

                    </td>
                </tr>
            </table>
        </fieldset>
            <br />
        <fieldset class="fieldSetStyle">
        <legend class="legendStyle">经办信息</legend>
        <table align="center" cellpadding="5px" cellspacing="5px" style="border: 1px solid #E5E5E5;">
            <tr>
                <td><span class="red-star">*</span>客户姓名：</td>
                <td><asp:TextBox ID="nameTxt" runat="server" CssClass="aspTextBox" AutoPostBack="True" OnTextChanged="nameTxt_TextChanged"></asp:TextBox></td>
                <td><span class="red-star">*</span>客户身份证：</td>
                <td><asp:TextBox ID="IDCardTxt" runat="server" CssClass="aspTextBox" AutoPostBack="True" MaxLength="18" OnTextChanged="IDCardTxt_TextChanged"></asp:TextBox></td>
                <td><span class="red-star">*</span>经办柜员：</td>
                <td><asp:TextBox ID="tellerCodeTxt" runat="server" CssClass="aspTextBox" Width="152px" AutoPostBack="True" MaxLength="6" OnTextChanged="tellerCodeTxt_TextChanged"></asp:TextBox></td>
            </tr>
            <tr>
                <td>备注</td>
                <td colspan="5"><asp:TextBox runat="server" Height="63px" Width="100%" TextMode="MultiLine" ID="remarkTxt"></asp:TextBox></td>
            </tr>
        </table>
        </fieldset>
            <br />
        <fieldset class="fieldSetStyle" style="text-align: center; vertical-align: middle;">
            <legend class="legendStyle" style="text-align: center; vertical-align: middle; width: 0px;"></legend>
            <div id="bottom" style="display: inline; margin: 0px auto;">
                <span><asp:Button ID="depositBtn" runat="server" Text="存入" Height="30px" Width="100px" CssClass="aspBtn" OnClick="depositBtn_Click"/></span>
                <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                <span><asp:Button ID="cancelBtn" runat="server" Text="取消" Height="30px" Width="100px" CssClass="aspBtn" OnClick="cancelBtn_Click"/></span>
            </div>
        </fieldset>
    </div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Deposit.aspx.cs" Inherits="HHBankDepositSite.Deposit" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript" language="javascript">
            function displayCalendar() {
                if (datePicker.style.display == 'block') {
                    datePicker.style.display = 'none';
                    return;
                }
                var dataPicker = document.getElementById('datePicker');
                datePicker.style.display = 'block';
            }

            function onlyNumber() {
                var key = window.event.keyCode;
                if ((key == 46) || (key==8) || (key == 189) || (key==109) || (key==190)||(key==110)||(key>=48 && key<=57) 
                    || (key >= 96 && key <= 105) || (key >= 37 && key <= 40)) {
                }
                else if (key == 13) {
                    window.event.keyCode = 9;
                }
                else {
                    window.event.returnValue = false;
                }
            }

            function mouse_leave(){
                var t = '<%= TextInputCheck() %>'
                alert(t)
            }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div style="width:100%; height:100%;">
            <br />
        <fieldset class="fieldSetStyle">
            <legend class="legendStyle" align="center">交易信息</legend>
            <table width="85%" align="center" cellpadding="5px" cellspacing="5px" style="border: 1px solid #E5E5E5; border-spacing: 0px;">
                <tr>
                    <td align="right"><span class="red-star">*</span><span class="label">协议编号：</span></td>
                    <td width="22%" align="left"><span><asp:TextBox ID="protocolTxt" runat="server" CssClass="aspTextBox" MaxLength="14"></asp:TextBox></span><span><asp:RequiredFieldValidator runat="server" ID="protocolIdValidator" ControlToValidate="protocolTxt" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator></span></td>
                    <td align="right"><span class="red-star">*</span><span class="label">存单账号：</span></td>
                    <td width="25%" align="left"><span><asp:TextBox ID="billAccountTxt" runat="server" CssClass="aspBillAccount" Width="200px" MaxLength="23"></asp:TextBox></span><span><asp:RequiredFieldValidator runat="server" ID="billAccountValidator" ControlToValidate="billAccountTxt" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator></span></td>
                    <td align="right"><span class="red-star">*</span><span class="label">凭证号码：</span></td>
                    <td align="left"><span><asp:TextBox ID="billCodeTxt" runat="server" CssClass="aspTextBox" MaxLength="12"></asp:TextBox></span><span><asp:RequiredFieldValidator runat="server" ID="billCodeValidator" ControlToValidate="billCodeTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator></span></td>
                </tr>
                <tr>
                    <td align="right"><span class="red-star">*</span><span class="label">约定存期：</span></td>
                    <td align="left"><span>
                        <asp:DropDownList ID="periodDrop" runat="server" OnSelectedIndexChanged="periodDrop_SelectedIndexChanged" AutoPostBack="True" CssClass="aspTextBox">
                            <asp:ListItem>三个月</asp:ListItem>
                            <asp:ListItem>六个月</asp:ListItem>
                            <asp:ListItem>一年</asp:ListItem>
                            <asp:ListItem>二年</asp:ListItem>
                            <asp:ListItem>三年</asp:ListItem>
                            <asp:ListItem>五年</asp:ListItem>
                        </asp:DropDownList>
                        </span>
                        <span><asp:RequiredFieldValidator runat="server" ID="periodDropValidator" ControlToValidate="periodDrop" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator></span>
                    </td>
                    <td align="right"><span class="red-star">*</span><span class="label">约定利率：</span></td>
                    <td align="left"><span><asp:TextBox ID="rateTxt" runat="server" Width="131px" OnTextChanged="rateTxt_TextChanged" CssClass="aspTextBoxShort" ReadOnly="True"></asp:TextBox></span><span class="per-cent">%</span></td>
                    <td align="right"><span class="red-star">*</span><span class="label">存入金额：</span></td>
                    <td align="left"><span><asp:TextBox ID="moneyTxt" runat="server" CssClass="aspTextBoxShort"></asp:TextBox></span><span><asp:RequiredFieldValidator runat="server" ID="moneyValidator" ControlToValidate="moneyTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator></span></td>
                </tr>
                <tr>
                    <td align="right"><span class="red-star">*</span><span class="lable">存入日期：</span></td>
                    <td align="left">
                        <div style="display: inline;">
                            <asp:TextBox ID="dateTxt" runat="server" TextMode="SingleLine" CssClass="aspTextBox" ReadOnly="True" Width="130px"></asp:TextBox>
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
                        <span><asp:RequiredFieldValidator runat="server" ID="depositDateValidator" ControlToValidate="dateTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator></span>
                    </td>
                    <td align="right"><span class="red-star">*</span><span class="label">补息账号：</span></td>
                    <td align="left" colspan="2"><asp:TextBox ID="bindAccountTxt" runat="server" style="margin-left: 0px;" Width="200px" CssClass="aspBillAccount" MaxLength="23" ></asp:TextBox><span><asp:RequiredFieldValidator runat="server" ID="bindAccountValidator" ControlToValidate="bindAccountTxt" Display="Dynamic" ErrorMessage="必填！" SetFocusOnError="True" CssClass="validator"></asp:RequiredFieldValidator></span></td>
                </tr>
            </table>
        </fieldset>
            <br />
        <fieldset class="fieldSetStyle">
        <legend class="legendStyle" align="center">经办信息</legend>
        <table width="85%" align="center" cellpadding="5px" cellspacing="5px" style="border: 1px solid #E5E5E5;">
            <tr>
                <td align="right"><span class="red-star">*</span><span class="label">客户姓名：</span></td>
                <td align="left"><asp:TextBox ID="nameTxt" runat="server" CssClass="aspTextBox" ></asp:TextBox><span><asp:RequiredFieldValidator runat="server" ID="nameValidator" CssClass="validator" Display="Dynamic" ErrorMessage="必填！" ControlToValidate="nameTxt"></asp:RequiredFieldValidator></span></td>
                <td align="right"><span class="red-star">*</span><span class="label">客户身份证：</span></td>
                <td align="left"><asp:TextBox ID="IDCardTxt" runat="server" CssClass="aspTextBox" MaxLength="18"></asp:TextBox><span><asp:RequiredFieldValidator runat="server" ID="idValidator" CssClass="validator" Display="Dynamic" ErrorMessage="必填！" ControlToValidate="IDCardTxt"></asp:RequiredFieldValidator></span></td>
                <td align="right"><span class="red-star">*</span><span class="label">经办柜员号：</span></td>
                <td align="left"><asp:TextBox ID="tellerCodeTxt" runat="server" CssClass="aspTextBox" Width="152px" MaxLength="6" ></asp:TextBox><span><asp:RequiredFieldValidator runat="server" ID="tellerValidator" CssClass="validator" Display="Dynamic" ErrorMessage="必填！" ControlToValidate="tellerCodeTxt"></asp:RequiredFieldValidator></span></td>
            </tr>
            <tr>
                <td align="right">备注</td>
                <td align="left" colspan="5"><asp:TextBox runat="server" Height="63px" Width="100%" TextMode="MultiLine" ID="remarkTxt"></asp:TextBox></td>
            </tr>
        </table>
        </fieldset>
            <br />
        <fieldset class="fieldSetStyle" style="text-align: center; vertical-align: middle;">
            <legend class="legendStyle" style="text-align: center; vertical-align: middle; width: 0px;"></legend>
            <div style="border: 1px;">
                <h5 align="left">协议编号：机构号末4位 + 年份4位 + 6位编号</h5>
                <h5 align="left">存单账号：综合业务系统生成的23位账号</h5>
                <h5 align="left">凭证号码：“50” + 10位编码</h5>
            </div>
            <div id="bottom" style="display: inline; margin: 0px auto;">
                <span><asp:Button ID="depositBtn" runat="server" Text="存入" Height="30px" Width="100px" CssClass="aspBtn" OnClick="depositBtn_Click"/></span>
                <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                <span><asp:Button ID="cancelBtn" runat="server" Text="取消" Height="30px" Width="100px" CssClass="aspBtn" OnClick="cancelBtn_Click"/></span>
            </div>
        </fieldset>
    </div>

</asp:Content>

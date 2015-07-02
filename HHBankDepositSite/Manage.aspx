<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="HHBankDepositSite.Manage" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .mainContent { background:#DDF1FE; width:640px; height:400px; border:1px dotted #000; text-align: center; margin: 0 auto; }
        .txtBox { text-align: center; width: 80%; height: 30px; font-size: 18px;}
        .tdLabel { width: 30%; text-align: right; }
        .tdContent { width: 60%; }
        .auto-style5 { width: 30%; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div>
          <br />
          <div style="clear:both;">
          <fieldset class="fieldSetStyle">
              <legend class="legendStyle">记录修改</legend>
              <table width="80%" style="border:1px solid #E5E5E5;">
                  <tr>
                      <td>协议编号：</td>
                      <td>
                          <asp:TextBox ID="protocolIDTxt" runat="server" MaxLength="14" CssClass="aspTextBox"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="protocolIDNonValidator" runat="server" ControlToValidate="protocolIDTxt" ValidationGroup="search" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                          <asp:RegularExpressionValidator ID="protocolIDRegValidator" runat="server" ControlToValidate="protocolIDTxt" ValidationExpression="^[0-9]+$" ValidationGroup="search" Display="Dynamic" ErrorMessage="非法字符！" CssClass="validator"></asp:RegularExpressionValidator>
                      </td>
                      <td>存单账号：</td>
                      <td>
                          <asp:TextBox ID="billAccountTxt" runat="server" MaxLength="23" CssClass="aspBillAccount"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="billAccountNonValidator" runat="server" ControlToValidate="billAccountTxt" ValidationGroup="search" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                          <asp:RegularExpressionValidator ID="billAccountRegValidator" runat="server" ControlToValidate="billAccountTxt" ValidationExpression="^[0-9]+$" ValidationGroup="search" Display="Dynamic" ErrorMessage="非法字符！" CssClass="validator"></asp:RegularExpressionValidator>
                      </td>
                      <td><asp:Button ID="searchBtn" runat="server" Text="查找" ValidationGroup="search" CssClass="aspBtn" OnClick="searchBtn_Click" /></td>
                  </tr>
                  </table>
              <hr />
              <br />
              <table width="80%" style="border: 1px solid #E5E5E5; border-spacing: 0">
                  <tr>
                      <td>凭证号码：</td>
                      <td><asp:TextBox ID="billCodeTxt" runat="server" MaxLength="12" CssClass="aspTextBox"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="billCodeValidator" runat="server" ControlToValidate="billCodeTxt" ValidationGroup="record" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                      </td>
                      <td>本金：</td>
                      <td>
                          <asp:TextBox ID="moneyTxt" runat="server" CssClass="aspTextBox"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="moneyValidator" runat="server" ControlToValidate="moneyTxt" ValidationGroup="record" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                      </td>
                      <td>存入日期：</td>
                      <td><asp:TextBox ID="dateTxt" runat="server" CssClass="aspTextBox"></asp:TextBox>
                          <ajaxToolkit:CalendarExtender ID="dateTxt_CalendarExtender" runat="server" BehaviorID="dateTxt_CalendarExtender" TargetControlID="dateTxt" Format="yyyy-MM-dd" PopupPosition="BottomLeft">
                          </ajaxToolkit:CalendarExtender>
                          <asp:RequiredFieldValidator ID="depositDateValidator" runat="server" ControlToValidate="dateTxt" ValidationGroup="record" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                      </td>
                  </tr>
                  <tr>
                      <td>存期：</td>
                      <td><asp:DropDownList ID="periodDrop" runat="server" CssClass="aspDrop" AutoPostBack="True" OnSelectedIndexChanged="periodDrop_SelectedIndexChanged">
                              <asp:ListItem>三个月</asp:ListItem>
                              <asp:ListItem>六个月</asp:ListItem>
                              <asp:ListItem>一年</asp:ListItem>
                              <asp:ListItem>二年</asp:ListItem>
                              <asp:ListItem>三年</asp:ListItem>
                              <asp:ListItem>五年</asp:ListItem>
                          </asp:DropDownList></td>
                      <td>利率：</td>
                      <td>
                          <asp:UpdatePanel ID="UpdatePanelRate" runat="server">
                              <ContentTemplate>
                                  <asp:TextBox ID="rateTxt" runat="server" CssClass="aspTextBox" ReadOnly="true"></asp:TextBox>%
                              </ContentTemplate>
                              <Triggers>
                                  <asp:AsyncPostBackTrigger ControlID="periodDrop" />
                              </Triggers>
                          </asp:UpdatePanel>
                      </td>
                      <td>记录状态：</td>
                      <td>
                          <asp:DropDownList ID="statusDrop" runat="server" CssClass="aspDrop">
                              <asp:ListItem>存入未支取</asp:ListItem>
                              <asp:ListItem>部分提前支取</asp:ListItem>
                              <asp:ListItem>全部支取</asp:ListItem>
                              <asp:ListItem>他行支取</asp:ListItem>
                              <asp:ListItem>其他</asp:ListItem>
                          </asp:DropDownList>
                      </td>
                  </tr>
                  <tr>
                      <td>客户姓名：</td>
                      <td>
                          <asp:TextBox ID="clientNameTxt" runat="server" CssClass="aspTextBox"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="clientNameValidator" runat="server" ControlToValidate="clientNameTxt" ValidationGroup="record" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                      </td>
                      <td>身份证号：</td>
                      <td>
                          <asp:TextBox ID="clientIDTxt" runat="server" CssClass="aspTextBox"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="clientIDValidator" runat="server" ControlToValidate="clientIDTxt" ValidationGroup="record" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                      </td>
                      <td>经办柜员：</td>
                      <td>
                          <asp:TextBox ID="tellerCodeTxt" runat="server" CssClass="aspTextBox" Width="80px"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="tellerCodeValidator" runat="server" ControlToValidate="tellerCodeTxt" ValidationGroup="record" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                          <asp:TextBox ID="tellerNameTxt" runat="server" CssClass="aspTextBoxShort" Width="80px"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="tellerNameValidator" runat="server" ControlToValidate="tellerNameTxt" ValidationGroup="record" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                      </td>
                  </tr>
                  <tr>
                      <td>支取日期：</td>
                      <td>
                          <asp:TextBox ID="drawDateTxt" runat="server" CssClass="aspTextBox"></asp:TextBox>
                          <ajaxToolkit:CalendarExtender ID="drawCalendarEx" runat="server" TargetControlID="drawDateTxt" Format="yyyy-MM-dd" />
                      </td>
                      <td>补息账号：</td>
                      <td colspan="2" align="left">
                          <asp:TextBox ID="bindAccountTxt" runat="server" MaxLength="23" CssClass="aspBillAccount"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="bindAccountValidator" runat="server" ControlToValidate="bindAccountTxt" ValidationGroup="record" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                      </td>
                  </tr>
                  <tr>
                      <td><br /></td>
                  </tr>
                  <tr>
                      <td colspan="6" align="center"><asp:Button ID="recordBtn" runat="server" ValidationGroup="record" CssClass="aspBtn" Text="修改" OnClick="recordBtn_Click" /></td>
                  </tr>
              </table>
          </fieldset>
             </div>

          <div style="width:100%; margin-bottom:30px;">
                        <br />
          <div style="float: left; margin: 0 20px 0 20px; width:45%">
        <fieldset class="fieldSetStyle">
            <legend class="legendStyle">当前利率</legend>
            <table width="90%" cellpadding="5px" cellspacing="5px" align="center" style="border:1px solid #E5E5E5;">
                <tr>
                    <td align="right">三个月：</td>
                    <td><asp:TextBox ID="m03RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="m03RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="m03RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="m03RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="m03RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    <td align="right">六个月：</td>
                    <td><asp:TextBox ID="m06RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="m06RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="m06RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="m06RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="m06RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="right">一年：</td>
                    <td><asp:TextBox ID="y01RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="y01RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="y01RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="y01RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="y01RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    <td align="right">二年：</td>
                    <td><asp:TextBox ID="y02RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="y02RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="y02RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="y02RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="y02RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    </tr>
                <tr>
                    <td align="right">三年：</td>
                    <td><asp:TextBox ID="y03RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="y03RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="y03RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="y03RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="y03RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    <td align="right">五年：</td>
                    <td><asp:TextBox ID="y05RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="y05RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="y05RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="y05RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="y05RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="right">活期年利率：</td>
                    <td><asp:TextBox ID="currentRateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="currRateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="currentRateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="currRateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="currentRateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">
                    <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center"><asp:Button ID="changeRateBtn" runat="server" ValidationGroup="bankrate" CssClass="aspBtn" Text="修改" OnClick="changeRateBtn_Click"/></td>
                </tr>
            </table>
        </fieldset>
          </div>

          <div style="float:right; width:45%; margin-right: 20px;">
         <fieldset class="fieldSetStyle">
            <legend class="legendStyle">修改密码</legend>
            <table width="90%" cellpadding="5" cellspacing="5" align="center" style="border:1px solid #E5E5E5;">
                <tr>
                    <td class="tdLabel">用户名：</td>
                    <td class="tdContent">
                        <asp:TextBox runat="server" ID="userNameTxt" Font-Names="Arial" Font-Size="20px" CssClass="aspTextBoxShort" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="userNameValidator" ErrorMessage="必填！" Display="Dynamic" ControlToValidate="userNameTxt" ValidationGroup="password" CssClass="validator"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">原 密 码：</td>
                    <td class="tdContent">
                        <asp:TextBox runat="server" ID="oldpwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="aspTextBoxShort"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="oldpwdValidator" ErrorMessage="必填！" Display="Dynamic" ControlToValidate="oldpwdTxt" ValidationGroup="password" CssClass="validator"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">新 密 码：</td>
                    <td class="tdContent">
                        <asp:TextBox runat="server" ID="newpwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="aspTextBoxShort"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="newpwdValidator" ErrorMessage="必填！" Display="Dynamic" ControlToValidate="newpwdTxt" ValidationGroup="password" CssClass="validator"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdLabel">确认新密码：</td>
                    <td class="tdContent">
                        <asp:TextBox runat="server" ID="surepwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="aspTextBoxShort"></asp:TextBox><span><asp:RequiredFieldValidator runat="server" ID="surepwdValidator" ErrorMessage="必填！" Display="Dynamic" ControlToValidate="surepwdTxt" CssClass="validator"></asp:RequiredFieldValidator></span>
                        <span><asp:CompareValidator ID="compareValidator" runat="server" ErrorMessage="两次密码不一致！" Display="Dynamic" ControlToCompare="newpwdTxt" ValidationGroup="password" ControlToValidate="surepwdTxt" CssClass="validator"></asp:CompareValidator></span>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button runat="server" ID="okBtn" CssClass="aspBtn" Text="确定" Width="100px" Height="30px" ValidationGroup="password" OnClick="okBtn_Click"/>
                    </td>
                </tr>
            </table>
         </fieldset>
          </div>
         </div>
          <hr style="clear:both;"/>
    </div>
</asp:Content>

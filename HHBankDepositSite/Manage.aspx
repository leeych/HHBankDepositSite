<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="HHBankDepositSite.Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .mainContent { background:#DDF1FE; width:640px; height:400px; border:1px dotted #000; text-align: center; margin: 0 auto; }
        .txtBox { text-align: center; width: 80%; height: 30px; font-size: 18px;}
        .tdLabel { width: 30%; text-align: right; height: 30px; }
        .tdContent { width: 60%; height: 30px; }
        .auto-style5 { width: 30%; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div>
          <br />
        <fieldset style="text-align: center;">
            <legend>当前利率</legend>
            <table style="margin: 5px; padding: 3px;" align="center">
                <tr>
                    <td>活期年利率：</td>
                    <td><asp:TextBox ID="currentRateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="currRateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="currentRateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="currRateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="currentRateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    <td>三个月：</td>
                    <td><asp:TextBox ID="m03RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="m03RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="m03RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="m03RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="m03RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    <td>六个月：</td>
                    <td><asp:TextBox ID="m06RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="m06RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="m06RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="m06RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="m06RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    <td>一年：</td>
                    <td><asp:TextBox ID="y01RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="y01RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="y01RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="y01RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="y01RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>二年：</td>
                    <td><asp:TextBox ID="y02RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="y02RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="y02RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="y02RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="y02RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    <td>三年：</td>
                    <td><asp:TextBox ID="y03RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="y03RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="y03RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="y03RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="y03RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    <td>五年：</td>
                    <td><asp:TextBox ID="y05RateTxt" runat="server" CssClass="aspBankrate"></asp:TextBox>%
                        <span>
                            <asp:RequiredFieldValidator ID="y05RateValidator" runat="server" ErrorMessage="必填！" ControlToValidate="y05RateTxt" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="y05RateRegValidator" runat="server" ErrorMessage="非法字符！" ControlToValidate="y05RateTxt" ValidationExpression="^[0-9]+[.]?[0-9]+$" ValidationGroup="bankrate" Display="Dynamic" CssClass="validator"></asp:RegularExpressionValidator>
                        </span>
                    </td>
                    <td colspan="2" align="center"><asp:Button ID="changeRateBtn" runat="server" ValidationGroup="bankrate" CssClass="aspBtn" Text="修改" OnClick="changeRateBtn_Click"/></td>
                </tr>
            </table>
        </fieldset>


          <br />
          <br />
         <fieldset>
             <legend>修改密码</legend>
        <table width="100%" cellpadding="5" cellspacing="5" align="center">
            <tr>
                <td class="tdLabel">用户名：</td>
                <td class="tdContent">
                    <asp:TextBox runat="server" ID="userNameTxt" Font-Names="Arial" Font-Size="20px" CssClass="aspTextBoxShort" ReadOnly="True" Width="220px"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="userNameValidator" ErrorMessage="必填！" Display="Dynamic" ControlToValidate="userNameTxt" CssClass="validator"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdLabel">原 密 码：</td>
                <td class="tdContent">
                    <asp:TextBox runat="server" ID="oldpwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="aspTextBoxShort"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="oldpwdValidator" ErrorMessage="必填！" Display="Dynamic" ControlToValidate="oldpwdTxt" CssClass="validator"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdLabel">新 密 码：</td>
                <td class="tdContent">
                    <asp:TextBox runat="server" ID="newpwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="aspTextBoxShort"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="newpwdValidator" ErrorMessage="必填！" Display="Dynamic" ControlToValidate="newpwdTxt" CssClass="validator"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdLabel">确认新密码：</td>
                <td class="tdContent">
                    <asp:TextBox runat="server" ID="surepwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="aspTextBoxShort"></asp:TextBox><span><asp:RequiredFieldValidator runat="server" ID="surepwdValidator" ErrorMessage="必填！" Display="Dynamic" ControlToValidate="surepwdTxt" CssClass="validator"></asp:RequiredFieldValidator></span>
                    <span><asp:CompareValidator ID="compareValidator" runat="server" ErrorMessage="两次密码不一致！" Display="Dynamic" ControlToCompare="newpwdTxt" ControlToValidate="surepwdTxt" CssClass="validator"></asp:CompareValidator></span>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <br />
                </td>
            </tr>
                <table align="center" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="okBtn" CssClass="aspBtn" Text="确定" Width="100px" Height="30px" OnClick="okBtn_Click"/>
                        </td>
                        <td>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;</td>
                        <td>
                            <asp:Button runat="server" ID="cancelBtn" CssClass="aspBtn" Text="重置" Width="100px" Height="30px" OnClick="cancelBtn_Click" />
                        </td>
                    </tr>
                </table>
            </tr>
        </table>
             </fieldset>
    </div>
</asp:Content>

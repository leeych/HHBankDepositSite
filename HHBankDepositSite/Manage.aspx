<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="HHBankDepositSite.Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .mainContent { background:#DDF1FE; width:640px; height:400px; border:1px dotted #000; text-align: center; margin: 0 auto; }
        .txtBox { text-align: center; width: 80%; height: 30px; font-size: 18px;}
        .tdLabel { width: 30%; text-align: right; height: 30px; }
        .tdContent { width: 70%; height: 30px; }
        .auto-style5 { width: 30%; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="mainContent">
          <br />
          <br />
          <br />
        <table width="100%" cellpadding="5" cellspacing="5" align="center">
            <tr>
                <td class="tdLabel">用户名：</td>
                <td width="162" class="tdContent">
                    <asp:TextBox runat="server" ID="userNameTxt" Font-Names="Arial" Font-Size="20px" CssClass="txtBox" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel">原 密 码：</td>
                <td width="162" class="tdContent">
                    <asp:TextBox runat="server" ID="oldpwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="txtBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel">新 密 码：</td>
                <td width="162" class="tdContent">
                    <asp:TextBox runat="server" ID="newpwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="txtBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdLabel">确认新密码：</td>
                <td width="162" class="tdContent">
                    <asp:TextBox runat="server" ID="surepwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="txtBox"></asp:TextBox>
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
    </div>
</asp:Content>

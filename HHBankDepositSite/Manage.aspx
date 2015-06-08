<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="HHBankDepositSite.Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        /** { margin:0 auto; padding:0; border:0;}*/
        /*body { background:#0462A5; font:12px "宋体"; color:#004C7E;}*/
        .login { background:#DDF1FE; width:640px; height:400px; border:1px solid #000; text-align: center; margin: 0 auto; }
        .submit { background:url(images/btn-bg.png) no-repeat; width:71px; height:24px; border:0;} 
        .reset { background:url(images/btn-bg.png) no-repeat; width:71px; height:24px; border:0;} 
        .txtBox { text-align: center; width: 90%; height: 38px; font-size: 18px;}
         .auto-style1 {
             width: 30%;
             text-align: right;
         }
         .auto-style2 {
             width: 70%;
         }
         .auto-style3 {
             width: 30%;
             text-align: right;
             height: 40px;
         }
         .auto-style4 {
             width: 70%;
             height: 40px;
         }
         .auto-style5 {
             width: 30%;
         }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="login">
          <br />
          <br />
          <br />
        <table width="100%" cellpadding="5" cellspacing="5" align="center">
            <tr>
                <td class="auto-style3">用户名：</td>
                <td width="162" class="auto-style4">
                    <asp:TextBox runat="server" ID="userNameTxt" Font-Names="Arial" Font-Size="20px" CssClass="txtBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="40" class="auto-style1">原 密 码：</td>
                <td width="162" class="auto-style2">
                    <asp:TextBox runat="server" ID="oldpwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="txtBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="40" class="auto-style1">新 密 码：</td>
                <td width="162" class="auto-style2">
                    <asp:TextBox runat="server" ID="newpwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="txtBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">确认新密码：</td>
                <td width="162" class="auto-style4">
                    <asp:TextBox runat="server" ID="surepwdTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="txtBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
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

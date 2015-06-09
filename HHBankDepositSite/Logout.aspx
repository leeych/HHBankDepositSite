<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="HHBankDepositSite.Logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .mainBody { background:#DDF1FE; width:640px; height:400px; border:1px dotted #000; text-align: center; margin: 0 auto; }
        .txtBox { text-align: center; width: 80%; height: 30px; font-size: 18px;}
        .tdLabel { width: 30%; text-align: right; }
        .tdContent { width: 70%; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <script type="text/javascript">
        if (confirm("exit or not ?")) {
            __doPostBack('hiddenBtn', '1');
        }
        else {
            alert('online');
        }
    </script>--%>
    <div class="mainBody">
          <br />
          <br />
        <table width="100%" cellpadding="5" cellspacing="5" align="center">
            <tr>
                <td class="tdLabel">用户名：</td>
                <td class="tdContent"><asp:TextBox runat="server" ID="userNameTxt" CssClass="txtBox" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdLabel">机构号：</td>
                <td class="tdContent"><asp:TextBox runat="server" ID="orgCodeTxt" CssClass="txtBox" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdLabel">机构名：</td>
                <td class="tdContent"><asp:TextBox runat="server" ID="orgNameTxt" CssClass="txtBox" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdLabel">地址：</td>
                <td class="tdContent"><asp:TextBox runat="server" ID="orgAddressTxt" CssClass="txtBox" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tdLabel">电话：</td>
                <td class="tdContent"><asp:TextBox runat="server" ID="phoneTxt" CssClass="txtBox" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
                <table align="center">
                    <tr>
                        <td><asp:Button runat="server" ID="logoutBtn" Text="退出登录" CssClass="aspBtn" Height="30px" Width="100px" OnClick="logoutBtn_Click" /></td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="hiddenLinkBtn" runat="server" OnClick="hiddenLinkBtn_Click" Width="0px">LinkButton</asp:LinkButton> </td>
                        <td><asp:Button runat="server" ID="resetBtn" Text="重置" CssClass="aspBtn" Height="30px" Width="100px" OnClick="resetBtn_Click" /></td>
                    </tr>
                </table>
            </tr>
        </table>

    </div>
</asp:Content>

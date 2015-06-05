<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HHBankDepositSite._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登录页</title>
    <style type="text/css">
        .loginBtn {
            border-radius: 5px 5px;
            background-color: rgb(134,220,33);
        }

        .textBox {
            border-radius: 5px 5px;
            text-align: center;
        }

        .central {
            margin: 0 auto;
            width: 800px;
            height: 600px;
            background-color: lightgrey;
            background-image: url(~/Images/loginbg.png);
        }

    </style>
</head>
<body style="vertical-align: middle;">
    <form id="form1" runat="server">
    <div class="central">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

        <div id="userName" style="width: 800px; height:50px; text-align:center;">
            &nbsp;<asp:Label ID="userNameLabel" runat="server" Font-Names="华文楷体" Font-Size="25px" Text="用户名："></asp:Label>
            <asp:TextBox ID="userNameTxt" runat="server" Height="26px" Width="200px" Font-Names="微软雅黑" Font-Size="23px" CssClass="textBox"></asp:TextBox>
        </div>
        <div id="password" style="width: 800px; height:50px; text-align: center;">
            &nbsp;<asp:Label ID="pwdLabel" runat="server" Font-Names="华文楷体" Font-Size="25px" Text="密　码："></asp:Label>
            <asp:TextBox ID="pwdTxt" runat="server" Height="26px" TextMode="Password" Width="200px" Font-Size="23px" CssClass="textBox"></asp:TextBox>
        </div>
        <div id="btn" style="width:800px; height: 50px; text-align:center; padding-top:5px;">
            <asp:Button ID="btnLogin" runat="server" Text="登 录" Font-Size="20px" Height="40px" Width="90px" CssClass="loginBtn" Font-Names="华文楷体" OnClick="btnLogin_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="取 消" Font-Size="20px" Height="40px" Width="90px" CssClass="loginBtn" Font-Names="华文楷体" OnClick="btnCancel_Click" />
            <asp:Button ID="testBtn" runat="server" OnClick="testBtn_Click" Text="test" />
        </div>
    </div>
    </form>
</body>
</html>

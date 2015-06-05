<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="HHBankDepositSite.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>保利存</title>
    <link href="./CSS/navStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .menuTopMenu {
            background-color: transparent;
            font-family: 宋体;
            font-size: 12px;
            position: relative;
            top: 34px;
        }

        .dynamicMenuStyle {
            background-color: white;
            border: solid 1px #ACC3DF;
            padding: 1px 1px 1px 1px;
            text-align: left;
        }

        .dynamicHoverStyle {
            background-color: #F7DFA5;
            color: #333333;
        }

        .dynamicSelectedStyle {
            color: red;
        }

        .dynamicMenuItemStyle {
            padding: 2px 5px 2px 5px;
            color: #333333;
        }

        .staticSelectedStyle {
            color: red;
        }

        .staticMenuItemStyle {
            cursor: pointer;
            padding: 2px 5px 2px 5px;
            color: #333333;
            background-color: transparent;
        }

        .staticHoverStyle {
            background-color: #84BCCD;
            cursor:pointer;
            color: #333333;
        }

    </style>
</head>
<body style="background: rgb(200,220,234)">
    <div id="header"><img src="./Images/logo.png" alt="header" /></div>
    <div id="navgator" style="float: left; width: 80%;">
        <ul id="nav">
            <li><a href="http://www.divcss5.com/">存入</a></li> 
            <li><a href="http://www.divcss5.com/html/">支取</a></li> 
            <li><a href="http://www.divcss5.com/rumen/">查询</a></li> 
            <li><a href="http://www.divcss5.com/css-tool/">管理</a></li> 
        </ul>
    </div>
    <div class="nav" style="float:right; width: 20%; background: #00A2CA; height: 60px; text-align:center; vertical-align: middle">
       <span><h4>用户名： <% =Session["UserName"] %></h4></span>
    </div>
    <form id="form1" runat="server">
        <div style="clear: both; text-align: center; vertical-align:middle">
    <div style="float: left; width:80%">
        <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" Font-Bold="True" Font-Names="仿宋" Font-Size="22px" Font-Strikeout="False" Font-Underline="False" Height="53px" Width="600px">
            <DynamicHoverStyle CssClass="dynamicHoverStyle" />
            <DynamicMenuItemStyle CssClass="dynamicMenuItemStyle" />
            <DynamicMenuStyle CssClass="dynamicMenuStyle" />
            <DynamicSelectedStyle CssClass="dynamicSelectedStyle" />
            <StaticHoverStyle CssClass="staticHoverStyle" />
            <StaticMenuItemStyle CssClass="staticMenuItemStyle" />
            <StaticSelectedStyle CssClass="staticSelectedStyle" />
            <Items>
                <asp:MenuItem Text="首页" ToolTip="首页" Value="Home"></asp:MenuItem>
                <asp:MenuItem Text="存入" ToolTip="存款" Value="Deposit"></asp:MenuItem>
                <asp:MenuItem Text="查询" ToolTip="查询已办业务" Value="Search"></asp:MenuItem>
                <asp:MenuItem Text="支取" ToolTip="保利存支取" Value="Draw"></asp:MenuItem>
                <asp:MenuItem Text="管理" ToolTip="机构柜员管理" Value="Manage"></asp:MenuItem>
            </Items>
        </asp:Menu>
    </div>
        <div style="float: right; width:20%"><h4>用户名：<% =Session["UserName"] %></h4></div>
            </div>
    </form>
</body>
</html>

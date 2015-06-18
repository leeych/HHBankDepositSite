<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HHBankDepositSite.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>保利存登录页</title>
    <style>
        * { margin:0 auto; padding:0; border:0;}
        body { background:#0462A5; font:12px "宋体"; color:#004C7E;}
        input { border:1px solid #004C7E;
            height: 28px;
            width: 187px;
        }
        .main { background:url(images/bg.jpg) repeat-x; height:600px;}
        .login { background:#DDF1FE; width:468px; height:262px; border:1px solid #000;}
        .top { background:url(images/login_bg.jpg) repeat-x; width:464px; height:113px; border:1px solid #2376B1; margin-top:1px;}
        .logo { background:url(images/hhbank.png) no-repeat; width:214px; height:42px; float:left; margin:29px 0 0 14px;}
        .lable { background:url(images/label.png) no-repeat; width:157px; height:32px; float:right; margin:81px 31px 0 0;}
        .submit { background:url(images/btn-bg.png) no-repeat; width:71px; height:24px; border:0;} 
        .reset { background:url(images/btn-bg.png) no-repeat; width:71px; height:24px; border:0;} 
        .auto-style1 {
            width: 275px;
        }

        .txtBox {
            text-align: center;
            vertical-align: middle;
            padding-top: 5px;
        }
        .auto-style2 {
            height: 24px;
        }
        .auto-style3 {
            height: 36px;
        }
    </style>

    <script type="text/javascript" lang="zh-cn">
        function is_digit(c_check) {
            return (('0' <= c_check) && (c_check <= '9'))
        }

        function is_alpha(c_check) {
            return ((('a' <= c_check) && (c_check <= 'z')) || (('A' <= c_check) && (c_check <= 'Z')))
        }

        function is_null(c_check) {
            return (c_check != "")
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" class="main" cellpadding="0" cellspacing="0">
  <tr>
    <td>
      <div class="login">
        <div class="top">
          <div class="logo"></div>
          <div class="lable"></div>
        </div>
        <table width="468" cellpadding="0" cellspacing="0">
          <tr>
            <td style="padding-top:17px;" class="auto-style1">
              <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="right" height="27">用户名：</td>
                  <td align="right" width="161">
                      <asp:TextBox runat="server" ID="userNameTxt" Font-Names="Arial" Font-Size="20px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td align="right" height="27">密 码：</td>
                  <td align="right" width="161">
                      <asp:TextBox runat="server" ID="passwordTxt" TextMode="Password" Font-Names="Arial" Font-Size="20px" CssClass="txtBox" MaxLength="20"></asp:TextBox>
                  </td>
                </tr>
              </table>
            </td>
            <td style="padding-top:17px;">
              <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="center" class="auto-style2">
                      <asp:Button runat="server" CssClass="submit" ID="loginBtn" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="登录" OnClick="loginBtn_Click" />
                  </td>
                </tr>
                <tr>
                  <td align="center" height="30" class="auto-style3">
                    <%--<input name="reset" type="button" class="reset" />--%>
                      <asp:Button runat="server" CssClass="reset" ID="resetBtn" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="取消" />
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
        <table width="100%" cellpadding="0" cellspacing="0" style="margin-top:28px;">
          <tr>
            <td align="center"><h4>Copyright 2015 淮河银行</h4></td>
          </tr>
        </table>
      </div>
      <!--login -->
    </td>
  </tr>
</table>
    </div>
    </form>
</body>
</html>

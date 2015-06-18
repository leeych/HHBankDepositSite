<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyGVPage.ascx.cs" Inherits="HHBankDepositSite.UserControl.MyGVPage" %>
<script language="javascript">
    function callButtonEvent<%=btnGo.ClientID%>() {
        var keycode = window.event.keyCode;
        if (keycode == 13) {
            if (check<%btnGo.ClientID%>() == true) {
                event.cancelBubble = true;
                event.returnValue = false;
                document.getElementById('<%=btnGo.ClientID%>').click();
            }
        }
    }

    function check<%=btnGo.ClientID%>() {
        var count = parseInt(document.getElementById('<%=lblTotal.ClientID%>').outerText);
        var txt = document.getElementById('<%=txtCurrentPage.ClientID%>').value;
        var cur = parseInt(txt);
        if ((cur | NaN) == 0) {
            alert('请输入1到<%=lblTotal.Text%>之间的数字！');
            event.cancelPostBack = true;
            return false;
        }
        if (cur > count || cur < 1) {
            alert('请输入1到<%=lblTotal.Text%>之间的数字！');
            event.cancelPostBack = true;
            return false;
        }
    }
</script>
<table id="tbJump" cellspacing="0" cellPadding="0" width="100%" border="0" class="table">
     <TR align="right">
  <TD>
   <asp:linkbutton id="btnFirstPage" runat="server" CommandArgument="First">首页</asp:linkbutton>&nbsp;&nbsp;
   <asp:linkbutton id="btnPrevPage" runat="server" CommandArgument="Prev">上一页</asp:linkbutton>&nbsp;&nbsp;
   <asp:linkbutton id="btnNextPage" runat="server" CommandArgument="Next">下一页</asp:linkbutton>&nbsp;&nbsp;
   <asp:linkbutton id="btnLastPage" runat="server" CommandArgument="Last">尾页</asp:linkbutton>&nbsp;&nbsp;
   页次：<asp:Label ID="lblCurrentPage" Runat="server">0</asp:Label>/<ASP:LABEL id="lblTotal" RUNAT="server">0</ASP:LABEL>页&nbsp;&nbsp;
   共<asp:Label ID="lblTotalPage" Runat="server">0</asp:Label>条&nbsp;&nbsp; 每页<asp:Label ID="lblRecordOfPage" Runat="server">0</asp:Label>条&nbsp;&nbsp;
   第<ASP:TEXTBOX id="txtCurrentPage" RUNAT="server" Width="20px" CssClass="text">1</ASP:TEXTBOX>页
   <ASP:BUTTON id="btnGo" RUNAT="server" TEXT="跳转" COMMANDARGUMENT="Go" ToolTip="跳转" CssClass="button"></ASP:BUTTON>
  </TD>
 </TR>
</table>

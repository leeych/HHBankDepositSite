<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="BankRateManage.aspx.cs" Inherits="HHBankDepositSite.Admin.BankRateManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminHeader" runat="server">
    <style type="text/css">
        .tableHeader {
            padding: 3px 6px 3px 6px;
        }
        .tableTr {
            border-collapse:collapse;
        }
        .note {
            font-size:13px;
            color: blue;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentPlaceHolder" runat="server">
    <div>
        <fieldset style="text-align: center;">
            <legend>当前利率</legend>
            <table style="margin: 5px; padding: 3px;" align="center">
                <tr>
                    <td>活期年利率：</td>
                    <td><asp:TextBox ID="currentRateTxt" runat="server"></asp:TextBox>%
                        <span><asp:RequiredFieldValidator ID="currRateValidator" runat="server" ErrorMessage="*" ControlToValidate="currentRateTxt" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator></span>
                    </td>
                    <td>三个月：</td>
                    <td><asp:TextBox ID="m03RateTxt" runat="server"></asp:TextBox>%
                        <span><asp:RequiredFieldValidator ID="m03RateValidator" runat="server" ErrorMessage="*" ControlToValidate="m03RateTxt" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator></span>
                    </td>
                    <td>六个月：</td>
                    <td><asp:TextBox ID="m06RateTxt" runat="server"></asp:TextBox>%
                        <span><asp:RequiredFieldValidator ID="m06RateValidator" runat="server" ErrorMessage="*" ControlToValidate="m06RateTxt" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator></span>
                    </td>
                    <td>一年：</td>
                    <td><asp:TextBox ID="y01RateTxt" runat="server"></asp:TextBox>%
                        <span><asp:RequiredFieldValidator ID="y01RateValidator" runat="server" ErrorMessage="*" ControlToValidate="y01RateTxt" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator></span>
                    </td>
                </tr>
                <tr>
                    <td>二年：</td>
                    <td><asp:TextBox ID="y02RateTxt" runat="server"></asp:TextBox>%
                        <span><asp:RequiredFieldValidator ID="y02RateValidator" runat="server" ErrorMessage="*" ControlToValidate="y02RateTxt" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator></span>
                    </td>
                    <td>三年：</td>
                    <td><asp:TextBox ID="y03RateTxt" runat="server"></asp:TextBox>%
                        <span><asp:RequiredFieldValidator ID="y03RateValidator" runat="server" ErrorMessage="*" ControlToValidate="y03RateTxt" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator></span>
                    </td>
                    <td>五年：</td>
                    <td><asp:TextBox ID="y05RateTxt" runat="server"></asp:TextBox>%
                        <span><asp:RequiredFieldValidator ID="y05RateValidator" runat="server" ErrorMessage="*" ControlToValidate="y05RateTxt" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator></span>
                    </td>
                    <td colspan="2" align="center"><asp:Button ID="okBtn" runat="server" CssClass="aspBtn" Text="修改" OnClick="okBtn_Click"/></td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div>
        <fieldset>
            <legend>历史利率</legend>
            <asp:Table ID="bankRateTable" runat="server" CellSpacing="3" CellPadding="3" Width="100%" GridLines="Both" BorderColor="Black" BorderStyle="Solid" CssClass="tableTr">
                <asp:TableHeaderRow ID="resHeader" runat="server" BackColor="#669cc0" ForeColor="#FFFFFF" CssClass="tableHeader">
                    <asp:TableHeaderCell ID="bankRateDateCell" Wrap="false" runat="server" Text="生效日期"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="currentRateCell" Wrap="false" runat="server" Text="活期利率(%)"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="m03RateCell" Wrap="false" runat="server" Text="三个月(%)"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="m06RateCell" Wrap="false" runat="server" Text="六个月(%)"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="y01RateCell" Wrap="false" runat="server" Text="一年(%)"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="y02RateCell" Wrap="false" runat="server" Text="二年(%)"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="y03RateCell" Wrap="false" runat="server" Text="三年(%)"></asp:TableHeaderCell>
                    <asp:TableHeaderCell ID="y05RateCell" Wrap="false" runat="server" Text="五年(%)"></asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow ID="TableRow1" runat="server" Height="30px" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell1" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell2" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell3" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell4" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell5" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell6" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell7" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell8" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server" Height="30px" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell9" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell10" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell11" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell12" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell13" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell14" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell15" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell16" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow3" runat="server" Height="30px" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell17" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell18" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell19" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell20" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell21" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell22" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell23" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell24" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow4" runat="server" Height="30px" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell25" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell26" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell27" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell28" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell29" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell30" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell31" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell32" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow5" runat="server" Height="30px" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell33" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell34" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell35" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell36" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell37" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell38" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell39" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell40" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow6" runat="server" Height="30px" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell41" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell42" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell43" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell44" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell45" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell46" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell47" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell48" runat="server"></asp:TableCell>
                </asp:TableRow>
               <asp:TableRow ID="TableRow7" runat="server" Height="30px" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell49" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell50" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell51" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell52" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell53" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell54" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell55" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell56" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow8" runat="server" Height="30px" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell57" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell58" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell59" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell60" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell61" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell62" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell63" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell64" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow9" runat="server" Height="30px" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell65" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell66" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell67" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell68" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell69" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell70" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell71" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell72" runat="server"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow10" runat="server" Height="30px" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell73" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell74" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell75" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell76" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell77" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell78" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell79" runat="server"></asp:TableCell>
                    <asp:TableCell ID="TableCell80" runat="server"></asp:TableCell>
                </asp:TableRow>
                </asp:Table>
        </fieldset>
    </div>
</asp:Content>

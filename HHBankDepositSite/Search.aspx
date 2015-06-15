<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="HHBankDepositSite.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div style="float: left;">
        <table>
            <tr>
                <td>机构号：</td>
                <td><asp:TextBox runat="server" ID="orgCodeTxt" CssClass="aspTextBox" ReadOnly="true" MaxLength="10" BackColor="#ddf1fe"></asp:TextBox></td>
                <td>机构名称：</td>  
                <td><asp:TextBox runat="server" ID="orgNameTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
                <td>已用最大协议编号：</td>
                <td><asp:TextBox runat="server" ID="maxProtocolIdTxt" CssClass="aspTextBox" MaxLength="14" ReadOnly="True" BackColor="#ddf1fe"></asp:TextBox></td>
                <td>“保利存”总笔数：</td>
                <td><asp:TextBox runat="server" ID="protocolCountTxt" CssClass="aspTextBox" ReadOnly="true" BackColor="#ddf1fe"></asp:TextBox></td>
            </tr>
        </table>
        <div style="float: left;">
        <table style="border: 1px solid #E5E5E5; text-align: center; vertical-align: middle;" align="center">
            <tr style="margin-bottom: 10px;">
                <td><span class="red-star">*</span>协议编号：</td>
                <td><asp:TextBox runat="server" ID="protocolIdTxt" CssClass="aspTextBox" MaxLength="14"></asp:TextBox></td>
            </tr>
            <tr>
                <td><span class="red-star">*</span>存单账号：</td>
                <td><asp:TextBox runat="server" ID="billAccountTxt" CssClass="aspBillAccount" MaxLength="23"></asp:TextBox></td>
            </tr>
            <tr>
                <td><span class="red-star">*</span>身份证号：</td>
                <td><asp:TextBox runat="server" ID="idCardTxt" CssClass="aspTextBox" MaxLength="18"></asp:TextBox></td>
            </tr>
            <tr>
                <td><span class="red-star">*</span>客户姓名：</td>
                <td><asp:TextBox runat="server" ID="clientNameTxt" CssClass="aspTextBox"></asp:TextBox></td>
            </tr>
        </table>
            </div>
        </div>
</asp:Content>

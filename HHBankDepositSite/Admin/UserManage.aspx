<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" EnableEventValidation="false" Inherits="HHBankDepositSite.Admin.UserManage" %>

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
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="AdminContentPlaceHolder" runat="server">
    <div style="width:100%">
        <fieldset>
            <legend>管理员密码修改</legend>
            <table>
                <tr>
                    <td><span class="notetag">用户名：</span></td>
                    <td><asp:TextBox ID="adminUserNameTxt" runat="server" ReadOnly="true"></asp:TextBox></td>
                    <td><span class="notetag">原密码：</span></td>
                    <td>
                        <asp:TextBox ID="oldpwdTxt" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="oldpwdTxtValidator" runat="server" ControlToValidate="oldpwdTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="oldpwdCompareValidator" runat="server" ControlToValidate="oldpwdTxt" Display="Dynamic" ErrorMessage="原密码不对！" CssClass="validator"></asp:CompareValidator>
                    </td>
                    <td><span class="notetag">新密码</span></td>
                    <td>
                        <asp:TextBox ID="newpwdTxt" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="newpwdTxtValidator" runat="server" ControlToValidate="newpwdTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                    </td>
                    <td><span class="notetag">确认新密码：</span></td>
                    <td>
                        <asp:TextBox ID="surenewpwdTxt" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="surenewpwdTxtValidator" runat="server" ControlToValidate="surenewpwdTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="adminpwdCompareValidator" runat="server" ControlToCompare="newpwdTxt" ControlToValidate="surenewpwdTxt" ErrorMessage="两次密码不一致！" CssClass="validator"></asp:CompareValidator>
                    </td>
                    <td><asp:Button ID="adminPwdBtn" runat="server" Text="确定" OnClick="adminPwdBtn_Click" /></td>
                </tr>
            </table>
        </fieldset>
        </div>

    <div style="width: 100%">        
        <fieldset>
            <legend>机构管理</legend>
            
            <div style="width: 60%; float: left; text-align: center; vertical-align: middle; padding-left: 30px;">
                <div style="clear:both;">
                    <table align="left">
                        <tr>
                            <td><span class="notetag">机构名：</span></td>
                            <td><asp:DropDownList ID="orgDrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="orgDrop_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                    </table>
                </div>
                <div style="clear:both;">
                <asp:UpdatePanel id="UpdatePanelBankRateTable" runat="server">
                    <ContentTemplate>
                <asp:Table ID="tellerTable" runat="server" CellSpacing="3" CellPadding="3" Width="90%" GridLines="Both" BorderColor="Black" BorderStyle="Solid" CssClass="tableTr">
                    <asp:TableHeaderRow ID="resHeader" runat="server" BackColor="#669cc0" ForeColor="#FFFFFF" CssClass="tableHeader">
                        <asp:TableHeaderCell ID="tellerCodeCell" runat="server" Wrap="false" Text="柜员号"></asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="tellerNameCell" runat="server" Wrap="false" Text="柜员姓名"></asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="orgCodeCell" runat="server" Wrap="false" Text="机构号"></asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="orgNameCell" runat="server" Wrap="false" Text="所属机构"></asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow ID="tableRow1" runat="server" Height="30px">
                        <asp:TableCell ID="TableCell1" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell2" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell3" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell4" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="tableRow2" runat="server" Height="30px">
                        <asp:TableCell ID="TableCell5" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell6" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell7" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell8" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="tableRow3" runat="server" Height="30px">
                        <asp:TableCell ID="TableCell9" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell10" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell11" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell12" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="tableRow4" runat="server" Height="30px">
                        <asp:TableCell ID="TableCell13" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell14" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell15" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell16" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="tableRow5" runat="server" Height="30px">
                        <asp:TableCell ID="TableCell17" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell18" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell19" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell20" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="tableRow6" runat="server" Height="30px">
                        <asp:TableCell ID="TableCell21" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell22" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell23" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell24" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="tableRow7" runat="server" Height="30px">
                        <asp:TableCell ID="TableCell25" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell26" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell27" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell28" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="tableRow8" runat="server" Height="30px">
                        <asp:TableCell ID="TableCell29" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell30" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell31" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell32" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="tableRow9" runat="server" Height="30px">
                        <asp:TableCell ID="TableCell33" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell34" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell35" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell36" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="tableRow10" runat="server" Height="30px">
                        <asp:TableCell ID="TableCell37" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell38" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell39" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell40" runat="server"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                        </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="orgDrop" />
                    </Triggers>
                    </asp:UpdatePanel>
                 </div>
            </div>
            <div style="width: 35%; float: right;">
                        <br />
        <fieldset>
            <legend>机构密码重置</legend>
            <table cellpadding="3px" cellspacing="3px" align="center">
                <tr>
                    <td><span class="notetag">用户名：</span></td>
                    <td>
                        <asp:TextBox ID="userNameTxt" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="userNameValidator" runat="server" ControlToValidate="userNameTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator" ValidationGroup="org"></asp:RequiredFieldValidator>
                    </td>
                    </tr>
                <tr>
                    <td><span class="notetag">新密码：</span></td>
                    <td>
                        <asp:TextBox ID="passwordTxt" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="passwordTxtValidator" runat="server" ControlToValidate="passwordTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator" ValidationGroup="org"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="passwordTxtRegValidator" runat="server" ControlToValidate="passwordTxt" Display="Dynamic" ErrorMessage="密码只能是数字或字母！" ValidationExpression="^[a-zA-Z0-9]+$" CssClass="validator"></asp:RegularExpressionValidator>
                    </td>
                    </tr>
                <tr>
                    <td><span class="notetag">确认密码：</span></td>
                    <td>
                        <asp:TextBox ID="surepwdTxt" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="surepwdTxtValidator" runat="server" ControlToValidate="surepwdTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator" ValidationGroup="org"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="pwdCompareValidator" runat="server" ControlToCompare="passwordTxt" ControlToValidate="surepwdTxt" ErrorMessage="两次密码不一致！" CssClass="validator"></asp:CompareValidator>
                    </td>
                    </tr>
                <tr>
                    <td colspan="2" align="center"><asp:Button ID="pwdBtn" runat="server" Text="密码重置" CssClass="aspBtn" OnClick="pwdBtn_Click" ValidationGroup="org"/></td>
                </tr>
            </table>
        </fieldset>
                <br />
                <fieldset>
                    <legend>柜员管理</legend>
                    <table cellpadding="3px" cellspacing="3px">
                        <tr>
                            <td><span class="notetag">柜员号：</span></td>
                            <td>
                                <asp:TextBox ID="newTellerCodeTxt" runat="server" MaxLength="6"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="newTellerCodeTxtValidator" runat="server" ControlToValidate="newTellerCodeTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator" ValidationGroup="teller"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="notetag">柜员姓名：</span></td>
                            <td>
                                <asp:TextBox ID="newTellerNameTxt" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="newTellerNameTxtValidator" runat="server" ControlToValidate="newTellerNameTxt" Display="Dynamic" ErrorMessage="必填！" CssClass="validator" ValidationGroup="teller"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td><span class="notetag">所属机构：</span></td>
                            <td><asp:DropDownList ID="tellerOrgNameDrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="tellerOrgNameDrop_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:UpdatePanel id="UpdatePanelOrgCode" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="orgCodeTxt" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="orgCodeHiddenValidator" runat="server" ControlToValidate="orgCodeTxt" Display="Dynamic" ErrorMessage="请选定一个机构！" CssClass="validator" ValidationGroup="teller"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="tellerOrgNameDrop" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:RadioButton ID="addTellerRbn" runat="server" Text="新增" GroupName="TellerManage" /></td>
                            <td><asp:RadioButton ID="changeTellerRbn" runat="server" Text="调动" GroupName="TellerManage" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center"><asp:Button ID="tellerBtn" runat="server" Text="确定" CssClass="aspBtn" OnClick="tellerBtn_Click" ValidationGroup="teller" /></td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </fieldset>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HHBankDepositSite.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p><h2 style="text-align:center;">淮河农村商业银行“保利存”储蓄存款存入业务操作流程</h2></p>
    
        <ol style="padding:10px 100px 10px 100px;">
            <li style="padding: 10px 0px;">
                 客户出具身份证证明；填写个人业务凭证和保利存存款开户协议书；注：开户协议中客户须约定一个本人结算账户为主账户的借记卡与定期存单账户关联。开户起存金额5000元，期限在1年以上(含)。
                <br />
            </li>
            <li style="padding: 10px 0px;">
                 经办柜员通过综合业务系统对私存单开户（要核实身份证件与个人业务凭证、协议书信息一致性），录入存款信息（系统内无该客户信息，应新建），提示客户输入密码，打印存单，加盖柜员核算用章、本人印章和保利存专用章。
                <br />
            </li>
            <li style="padding: 10px 0px;">
                 经办柜员将个人业务凭证交客户签字确认后，连同存单、个人业务凭证、协议书核对一致无误后，加盖柜员核算用章、本人印章、保利存专用章。及时把相关信息录入保利存管理系统，换人复核。（注：产品协议书一式三份，一份专夹保管，一份给客户，一份上报运营管理部。“保利存”定期存款专用章加盖在定期存单后面）。
                <br />
            </li>
            <li style="padding: 10px 0px;">
                 柜员核对存单与存入金额相符，确认所签印章齐全后，交客户。
                <br />
            </li>
            <li style="padding: 10px 0px;">
                 待客户确认存单存入金额无误后，柜员将协议书专夹保管，日终交会计主管收存核对。<br />
                <h5>业务编号（协议号）规则：机构号后四位+年份+序列编号，例如营业部14772015000001）标注在协议书上。</h5>
            </li>
        </ol>

</asp:Content>

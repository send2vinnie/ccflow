<%@ Page Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="Show.aspx.cs" Inherits="Lizard.OA.Web.OA_SMS.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            SmsId ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSmsId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发送号码 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSenderNumber" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            接收号码 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblReciveNumber" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发送内容 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSendContent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            接收内容 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblReciveConent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发送时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSendTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            接收时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblReciveTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>

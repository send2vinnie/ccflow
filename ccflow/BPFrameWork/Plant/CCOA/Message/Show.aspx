<%@ Page Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="Show.aspx.cs" Inherits="Lizard.OA.Web.OA_Message.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            主键Id ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMessageId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            消息名称（标题） ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMessageName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            消息类型 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMeaageType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发布人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblAuthor" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发布时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            最后更新时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblUpDT" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            状态：1-有效0-无效 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

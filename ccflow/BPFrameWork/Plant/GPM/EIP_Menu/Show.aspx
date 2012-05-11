<%@ Page Language="C#" MasterPageFile="~/GPM/GPMMaster.master" AutoEventWireup="true"
    CodeFile="Show.aspx.cs" Inherits="Lizard.GPM.Web.EIP_Menu.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            MenuId ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMenuId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            MenuNo ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMenuNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            MenuName ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMenuName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            Title ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            Img ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblImg" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            Url ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblUrl" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            Path ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPath" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            Pid ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPid" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            Status ：
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
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>

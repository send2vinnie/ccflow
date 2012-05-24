<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="BP.EIP.Web.Port_Menu.Show"
    Title="显示页" %>

<%@ Register Src="../../CCOA/Controls/MiniToolBar.ascx" TagName="MiniToolBar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td height="25" width="30%" align="right">
                No ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblNo" runat="server"></asp:Label>
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
                Pid ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblPid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                FK_Function ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblFK_Function" runat="server"></asp:Label>
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
                Status ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    </td> </tr> </table>
    </form>
</body>
</html>

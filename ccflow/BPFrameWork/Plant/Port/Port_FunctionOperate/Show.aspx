<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="BP.EIP.Web.Port_FunctionOperate.Show"
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
                主键 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblNo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                所属功能ID ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblFK_Function" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                操作名称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblOperateName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                功能描述 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblOperateDesc" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                控件名称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblControl_Name" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    </td> </tr> </table>
    </form>
</body>
</html>

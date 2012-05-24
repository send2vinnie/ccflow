﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="BP.EIP.Web.Port_FunctionOperate.Add"
    Title="增加页" %>

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
                <asp:TextBox ID="txtNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                所属功能ID ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFK_Function" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                操作名称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtOperateName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                功能描述 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtOperateDesc" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                控件名称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtControl_Name" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </td> </tr>
    <tr>
        <td class="tdbg" align="center" valign="bottom">
            <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="inputbutton"
                onmouseover="this.className='inputbutton_hover'" onmouseout="this.className='inputbutton'">
            </asp:Button>
            <asp:Button ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" class="inputbutton"
                onmouseover="this.className='inputbutton_hover'" onmouseout="this.className='inputbutton'">
            </asp:Button>
        </td>
    </tr>
    </table>
    <br />
    </form>
</body>
</html>

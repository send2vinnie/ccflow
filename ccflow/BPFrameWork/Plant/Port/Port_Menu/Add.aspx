<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="BP.EIP.Web.Port_Menu.Add"
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
                MenuNo ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtMenuNo" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                Pid ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPid" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                FK_Function ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFK_Function" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                MenuName ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtMenuName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                Title ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                Img ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtImg" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                Url ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtUrl" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                Path ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPath" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                所属系统 ：
            </td>
            <td height="25" width="*" align="left">
                <lizard:XDropDownList ID="ddlApp" runat="server" Width="100px" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <lizard:XButton ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" 
                    onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'">
                </lizard:XButton>
                <lizard:XButton ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click" 
                    onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'">
                </lizard:XButton>
            </td>
        </tr>
    </table>
    <br />
    </form>
</body>
</html>

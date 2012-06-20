<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modify.aspx.cs" Inherits="BP.EIP.Web.Port_Rule.Modify"
    Title="修改页" %>

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
                需要控制的权限名称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPermission" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                RulePolicy ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtRulePolicy" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                规则的分组 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtRuleGroup" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                Description ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtDescription" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                Perfix ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPerfix" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                RuleType ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtRuleType" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                所属管理域 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFK_Domain" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </td> </tr>
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
    </form>
</body>
</html>

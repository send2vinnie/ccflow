<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modify.aspx.cs" Inherits="BP.EIP.Web.Port_Emp.Modify"
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
                No ：
            </td>
            <td height="25" width="*" align="left">
                <asp:Label ID="lblNo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                名称 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                密码 ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPass" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                部门, 外键:对应物理表:Po ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFK_Dept" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                PID ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPID" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                PIN ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtPIN" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                KeyPass ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtKeyPass" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                IsUSBKEY ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtIsUSBKEY" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                FK_Emp ：
            </td>
            <td height="25" width="*" align="left">
                <asp:TextBox ID="txtFK_Emp" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td height="25" width="30%" align="right">
                Status ：
            </td>
            <td height="25" width="*" align="left">
                <asp:CheckBox ID="chkStatus" Text="Status" runat="server" Checked="False" />
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
    </form>
</body>
</html>

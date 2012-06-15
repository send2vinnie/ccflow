<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="BP.EIP.Web.Port_Emp.Add"
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
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td height="25" width="30%" align="right">
                            用户名 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtNo" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            姓名 ：
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
                            部门：
                        </td>
                        <td height="25" width="*" align="left">
                            <lizard:XDropDownList ID="ddlFK_Dept" runat="server" Width="100" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td height="25" width="30%" align="right">
                            PID ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtPID" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td height="25" width="30%" align="right">
                            PIN ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtPIN" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            USB认证登录 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:CheckBox ID="chkIsUSBKEY" runat="server" Checked="False" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            认证密码 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtKeyPass" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            对应员工 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <lizard:XDropDownList ID="ddlEmp" runat="server"  Width="100"/>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td height="25" width="30%" align="right">
                            状态 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:CheckBox ID="chkStatus" runat="server" Checked="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
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

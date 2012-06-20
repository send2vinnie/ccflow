<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Modify.aspx.cs" Inherits="BP.EIP.Web.PORT_STAFF.Modify"
    Title="修改页" %>

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
                    <tr style="display: none;">
                        <td height="25" width="30%" align="right">
                            NO ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtNo" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            职工编号 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtEmpNo" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            年龄 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtAge" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            身份证号 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtIDCard" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            电话 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtPhone" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            电子邮件 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            部门 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <lizard:XDropDownList ID="ddlDept" runat="server" Width="100" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            姓名 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtEmpName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            性别 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <lizard:XCheckBoxList ID="chklstSex" runat="server">
                                <asp:ListItem Text="男" Value="1" Selected="True" />
                                <asp:ListItem Text="女" Value="0" />
                            </lizard:XCheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            生日 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <lizard:XDatePicker ID="txtBirthday" runat="server" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            地址 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtAddress" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <lizard:XButton ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" onmouseover="this.className='lizard-button-hover'"
                    onmouseout="this.className='lizard-button'"></lizard:XButton>
                <lizard:XButton ID="btnCancle" runat="server" Text="取消" OnClick="btnCancle_Click"
                    onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'">
                </lizard:XButton>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="Lizard.OA.Web.OA_AddrBook.Show"
    Title="显示页" %>

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
                            主键Id ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblAddrBookId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            姓名 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            NickName ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblNickName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            性别 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSex" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            生日 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblBirthday" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            电子邮件 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            手机 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblMobile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            QQ ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblQQ" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            工作单位 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblWorkUnit" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            工作电话 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblWorkPhone" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            工作地址 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblWorkAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            家庭电话 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblHomePhone" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            家庭地址 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblHomeAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            分组 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblGrouping" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            状态0-停用1-启用 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            Remarks ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

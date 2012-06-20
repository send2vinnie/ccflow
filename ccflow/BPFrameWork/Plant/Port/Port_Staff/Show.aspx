<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="BP.EIP.Web.PORT_STAFF.Show"
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
                    <tr>
                        <td height="25" width="30%" align="right">
                            NO ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblNO" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            EMPNO ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblEMPNO" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            AGE ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblAGE" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            IDCARD ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblIDCARD" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            PHONE ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPHONE" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            EMAIL ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblEMAIL" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            UPUSER ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblUPUSER" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            FK_DEPT ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblFK_DEPT" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            EMPNAME ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblEMPNAME" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            SEX ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSEX" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            BIRTHDAY ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblBIRTHDAY" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            ADDRESS ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblADDRESS" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            CREATEDATE ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblCREATEDATE" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            STATUS ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSTATUS" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="BP.EIP.Web.PORT_STAFF.Add"
    Title="增加页" %>
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
                        <asp:textbox id="txtNO" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        EMPNO ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtEMPNO" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        AGE ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtAGE" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        IDCARD ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtIDCARD" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        PHONE ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtPHONE" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        EMAIL ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtEMAIL" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        UPUSER ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtUPUSER" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        FK_DEPT ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtFK_DEPT" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        EMPNAME ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtEMPNAME" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        SEX ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtSEX" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        BIRTHDAY ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtBIRTHDAY" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        ADDRESS ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtADDRESS" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        CREATEDATE ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtCREATEDATE" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        UPDT ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtUPDT" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td height="25" width="30%" align="right">
                        STATUS ：
                    </td>
                    <td height="25" width="*" align="left">
                        <asp:textbox id="txtSTATUS" runat="server" width="200px"></asp:textbox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="tdbg" align="center" valign="bottom">
            <lizard:XButton id="btnSave" runat="server" text="保存" onclick="btnSave_Click" 
                onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'"></lizard:XButton>
            <lizard:XButton id="btnCancle" runat="server" text="取消" onclick="btnCancle_Click" 
                onmouseover="this.className='lizard-button-hover'" onmouseout="this.className='lizard-button'"></lizard:XButton>
        </td>
    </tr>
</table>
</form></body></html>
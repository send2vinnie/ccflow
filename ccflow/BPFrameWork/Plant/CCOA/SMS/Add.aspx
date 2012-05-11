<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Lizard.OA.Web.OA_SMS.Add"
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
                            SmsId ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtSmsId" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发送号码 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtSenderNumber" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            接收号码 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtReciveNumber" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发送内容 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtSendContent" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            接收内容 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtReciveConent" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            发送时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtSendTime" runat="server" Width="70px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" width="30%" align="right">
                            接收时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:TextBox ID="txtReciveTime" runat="server" Width="70px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <script src="/js/calendar1.js" type="text/javascript"></script>
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

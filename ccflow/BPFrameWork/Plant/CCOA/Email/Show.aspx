<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="CCOA_Email_Show"
    Title="显示页" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .emailShow
        {
            text-align: left;
            width: 13%;
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: 12px;
        }
        .emailTitle
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: 12px;
            background-color: #e5e5e5;
            border-bottom-width: 2px;
            border-bottom-color: #333300;
            border-bottom-style: solid;
            padding: 5px;
        }
        .emailBottom
        {
            font-family: 宋体, Arial, Helvetica, sans-serif;
            font-size: 12px;
            background-color: #e5e5e5;
            padding: 5px;
            border-top-style: solid;
            border-top-width: 2px;
            border-top-color: #333300;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <table class="emailTitle" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr style="display: none;">
                        <td height="25" class="style1">
                            主键Id ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" class="style1">
                            主题 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSubject" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" class="style1">
                            发件人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblAddresser" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" class="style1">
                            收件人 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblAddressee" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" style="height: 300px;">
                    <tr>
                        <td colspan="2" height="80" width="*" align="left" style="vertical-align: top;">
                            <asp:Label ID="lblContent" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" cellpadding="0" width="100%" border="0" class="emailBottom">
                    <tr>
                        <td height="25">
                            类型：0-普通1-重要2-紧急 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblPriorityLevel" runat="server"></asp:Label>
                        </td>
                        <td height="25" class="style1">
                            分类：0-收件箱1-草稿箱2- ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblCategory" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" class="style1">
                            创建时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                        </td>
                        <td height="25" class="style1">
                            发送时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblSendTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" class="style1">
                            更新时间 ：
                        </td>
                        <td height="25" width="*" align="left">
                            <asp:Label ID="lblUpDT" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="bottom" colspan="4">
                            <asp:Button ID="btnSave" runat="server" Text="发送" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                                onmouseout="this.className='inputbutton'" OnClick="btnSave_Click1"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

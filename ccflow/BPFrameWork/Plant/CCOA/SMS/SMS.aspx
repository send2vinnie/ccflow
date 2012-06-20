<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="SMS.aspx.cs" Inherits="CCOA_SMS_SMS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
    <%@ register src="~/CCOA/AddressBook/AddrBook.ascx" tagname="AddrBook" tagprefix="uc" %>
    <xuc:XToolBar ID="XToolbar1" runat="server" title="短信平台" />
    <table width="100%">
        <tr>
            <td>
                <uc:AddrBook ID="AddrBook" runat="server" />
            </td>
            <td style="vertical-align: top;">
                <table width="100%">
                    <tr>
                        <th>
                            手机号码
                        </th>
                        <td>
                            <asp:TextBox ID="txtPhoneNumber" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            短信内容
                        </th>
                        <td>
                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Height="80" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <lizard:XButton ID="btnSend" runat="server" Text="发送" OnClick="btnSend_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

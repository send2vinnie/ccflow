<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddrTree.aspx.cs" Inherits="CCOA_AddressBook_AddrTree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 172px;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    通讯录（共123人）
                </td>
                <td>
                    收件人（已添加0人）
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <input id="Text1" type="text" /><input id="Button1" type="button" value="查找联系人" />
                </td>
                <td class="style1" rowspan="2">
                    <asp:ListBox ID="ListBox2" runat="server" Height="250px" Width="100%"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:ListBox ID="ListBox1" runat="server" Height="280px" Width="100%"></asp:ListBox>
                </td>
            </tr>
        </table>
        <div style="text-align: right">
            <input id="Button2" type="button" value="确定" /><input id="Button3" type="button"
                value="取消" />
        </div>
    </div>
    </form>
</body>
</html>

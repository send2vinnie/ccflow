<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="CCOA_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .login
        {
            background-image: url('Images/login.jpg') no-repeat;
            border: solid 1px #e5e5e5;
            margin-left: auto;
            margin-right: auto;
            width:300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="login">
            <table>
                <tr>
                    <td>
                        用户名：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        密码：
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnLogin" runat="server" Text="登录" /><asp:Button ID="btnReset" runat="server"
                            Text="重置" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

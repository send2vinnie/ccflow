<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="CCOA_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/master.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 75px;
        }
    </style>
</head>
<body style="background: #D6E2EE;">
    <form id="form1" runat="server">
    <div id="loginBody" style="background: url('Images/login.jpg') no-repeat;">
        <div id="theme">
            <div class="login" style="position:absolute;">
                <div style="height:24px;">
                    <asp:TextBox ID="txtUser" runat="server" CssClass="inputText" Width="223px" Height="14px"></asp:TextBox>
                </div>
                <div style="height:24px; margin-top:5px;">
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="inputText"  Width="223px" Height="14px"></asp:TextBox>
                </div>
                <div style="height:24px; margin-top:82px; margin-left:24px;">
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" CssClass="loginButton" />
                </div>
                <asp:Label ID="lblMsg" runat="server" CssClass="ErrorMsg" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>

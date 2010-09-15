<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddFavorite.aspx.cs" Inherits="GE_Favorite_AddFavorite" %>

<html>
<head runat="server">
    <title>添加收藏</title>
    <base target="_self" />
</head>
<body style="margin-left:10px; margin-top:10px; margin-right:10px; margin-bottom:10px;">
    <form id="form1" runat="server">
    <div style="border-style: solid; border-color: #ccc; border-width: 1px; padding-left:10px;" cellspacing="0"
        cellpadding="0">
        <p style="text-align: center; font-weight: bold">
            添加到收藏夹</p>
        <p>
            文件名:&nbsp;&nbsp;
            <asp:TextBox ID="txtFileName" runat="server" Width="150px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFileName"
                ErrorMessage="文件名不能为空!"></asp:RequiredFieldValidator>
        </p>
        <p>
            收藏到:&nbsp;&nbsp;
            <asp:DropDownList ID="ddlFavName" runat="server" Width="150px">
            </asp:DropDownList>
        </p>
        <p style="text-align:center;">
            <asp:Button ID="Button1" runat="server" Text=" 收 藏 " OnClick="Button1_Click" />
            <input id="Button2" type="button" value=" 关 闭 " onclick="window.close()" style="margin-left:20px" /></p>
    </div>
    </form>
</body>
</html>

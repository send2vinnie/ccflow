<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailList.aspx.cs" Inherits="EIP_EmailList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/main.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-size: small">
    <form id="form1" runat="server">
    <div>
        <asp:LinkButton ID="LinkButton1" runat="server">全部</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="LinkButton2" runat="server">未读</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="LinkButton3" runat="server">已读</asp:LinkButton>
        <div class="newslist">
            <ul>
                <% foreach (BP.CCOA.OA_Email item in EmailList)
                   {%>
                <li>
                    <img src="Images/gif/nav_title_sign.gif" />
                    <a href="Email/Add.aspx">
                        <%=item.Addresser %></a>&nbsp; <a href="Email/Show.aspx?id=<%=item.No%>" target="_blank">
                            <%=item.Subject%></a></li>
                <%} %>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>

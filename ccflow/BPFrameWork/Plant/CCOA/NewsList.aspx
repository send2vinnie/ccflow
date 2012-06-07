<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsList.aspx.cs" Inherits="EIP_NewsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/main.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-size: small">
    <form id="form1" runat="server">
    <div>
        <div class="newslist">
            <ul>
                <% foreach (BP.CCOA.OA_News item in NewsList)
                   {%>
                <li><img src="Images/gif/nav_title_sign.gif" /><a href="News/Show.aspx?id=<%=item.No%>" target="_blank">
                    <%=item.NewsTitle %></a>&nbsp;（<%=item.Clicks %>）&nbsp;&nbsp;&nbsp;&nbsp;<%=item.Author %>&nbsp;&nbsp;&nbsp;&nbsp;<%=item.CreateTime.ToShortDateString()%></li>
                <%} %>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>

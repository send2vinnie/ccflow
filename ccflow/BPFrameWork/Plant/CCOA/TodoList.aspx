<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TodoList.aspx.cs" Inherits="CCOA_TodoList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="newslist">
            <ul>
                <% foreach (BP.CCOA.OA_TodoWork item in WorkList)
                   {%>
                <li>
                    <img src="Images/gif/nav_title_sign.gif" /><a href="<%=item.Url%>"
                        target="_blank">
                        <%=item.FlowName%></a>&nbsp;（<%=item.Title %>）&nbsp;&nbsp;&nbsp;&nbsp;<%=item.Starter %>&nbsp;&nbsp;&nbsp;&nbsp;<%=item.NodeName%></li>
                <%} %>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>

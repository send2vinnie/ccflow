<%@ Control Language="C#" AutoEventWireup="true" CodeFile="XToolBar.ascx.cs" Inherits="CCOA_Controls_XToolBar" %>
<link href="../Style/main.css" rel="stylesheet" type="text/css" />
<div class="xtoolbar">
    <table width="100%">
        <tr>
            <td style="width: 150px; ">
                <div id="divTitle" style="vertical-align:middle;">
                   <%--<img src="<%=ImgScr %>" width="24px" height="24px" />--%> <%= Title %></div>
            </td>
            <td>
            </td>
            <td style="width: 250px;">
                <ul>
                    <li><a href="<%= AddUrl %>">
                        <img src="../Images/ico/plus.png" style="width: 20px; height: 20px;" alt="增加" /></a></li>
                    <%-- <li><a href="<%= ViewUrl %>">查看</a></li>
                    <li><a href="<%= EditUrl %>">修改</a></li>
                    <li><a href="<%= SearchUrl %>">查询</a></li>--%>
                </ul>
            </td>
        </tr>
    </table>
</div>

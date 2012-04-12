<%@ Control Language="C#" AutoEventWireup="true" CodeFile="XToolBar.ascx.cs" Inherits="CCOA_Controls_XToolBar" %>
<link href="../Style/main.css" rel="stylesheet" type="text/css" />
<div class="xtoolbar">
    <table width="96%">
        <tr>
            <td style="width: 100px;">
                <div id="divTitle">
                    <%= Title %></div>
            </td>
            <td>
            </td>
            <td style="width: 250px;">
                <ul>
                    <li><a href="<%= AddUrl %>">增加</a></li>
                    <li><a href="<%= ViewUrl %>">查看</a></li>
                    <li><a href="<%= EditUrl %>">修改</a></li>
                    <li><a href="<%= SearchUrl %>">查询</a></li>
                </ul>
            </td>
        </tr>
    </table>
</div>

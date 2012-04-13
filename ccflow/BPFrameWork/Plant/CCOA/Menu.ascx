<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="CCOA_Menu" %>
<div>
    <ul>
        <% foreach (BP.CCOA.Menu item in MenuList)
           { %>
        <li>
            <img src="<%= item.Img %>" alt="" /><a href="<%= item.Url %>"><%=item.Name %></a>
        </li>
        <%} %>
    </ul>
    <div id="divPop" runat="server" visible="false" style="width: 100px; height: 50px;">
        <asp:Label ID="lbl" runat="server" />
        
    </div>
</div>

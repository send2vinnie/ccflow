<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Article_Newest.ascx.cs"
    Inherits="CCOA_ComUC_Article_Newest" %>
<div class="Article_Newest">
    <h3>
        <%= Title %>
    </h3>
    <ul>
        <%foreach (BP.CCOA.Article item in Articles)
          {%>
        <li><a href="<%= item.Url %>">
            <%= item.Title %></a>&nbsp;(<%=item.Clicks %>)&nbsp;(<%=item.Created.ToShortDateString() %>)</li>
        <% } %>
    </ul>
</div>

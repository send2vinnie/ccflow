<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChannelTree.ascx.cs" Inherits="CCOA_Admin_ChannelTree" %>
<div style="border: 1px solid #e5e5e5; width: 300px; height: 500px;">
    <% foreach (BP.CCOA.Channel item in Channels)
       {%>
       <ul>
       <li><%= item.Name %></li>
       </ul>
    <% } %>
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageButton.ascx.cs" Inherits="CCOA_Controls_ImageButton" %>
<div class="ImageButton">
    <a href="#" onclick="<%=ClickEvent %>" title="<%=Title %>" >
        <img src="<%=ImageUrl %>" alt="<%=AlertText %>" height="48px" width="48px"/>
        <span>
            <%=Title%></span> </a>
</div>

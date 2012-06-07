<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageButton.ascx.cs" Inherits="CCOA_Controls_ImageButton" %>
<div class="ImageButton" >
    <a href="<%=LinkUrl %>">
      
            <img src="<%=ImageUrl %>" alt="<%=AlertText %>" height="48px" width="48px" />
       
            <span>
                <%=Text%></span></div>
       
    </a>
</div>

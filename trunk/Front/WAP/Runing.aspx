﻿<%@ Page Language="C#" MasterPageFile="~/WAP/MasterPage.master" AutoEventWireup="true" CodeFile="Runing.aspx.cs" Inherits="WAP_Runing" Title="Untitled Page" %>

<%@ Register src="../WF/UC/Runing.ascx" tagname="Runing" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language=javascript>
		function Do(warning, url)
		{
		  if (window.confirm(warning)==false)
		    return;
		 
		 window.location.href=url;
		 // WinOpen(url);
		}
		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Runing ID="Runing1" runat="server" />
</asp:Content>


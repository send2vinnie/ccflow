<%@ page language="C#" masterpagefile="~/WF/MasterPage.master" autoeventwireup="true" inherits="WF_Runing, App_Web_u0dbgd5p" title="无标题页" %>

<%@ Register src="UC/Runing.ascx" tagname="Runing" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language=javascript>
		function Do(warning, url)
		{
		  if (window.confirm(warning)==false)
		    return;
		 
		 window.location.href=url;
		 // WinOpen(url);
		}
		</script>
    <uc1:Runing ID="Runing1" runat="server" />
</asp:Content>


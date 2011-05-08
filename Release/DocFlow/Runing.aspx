<%@ page language="C#" masterpagefile="MasterPage.master" autoeventwireup="true" inherits="GovDoc_Runing, App_Web_vmqi53kc" title="无标题页" %>
<%@ Register src="../WF/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language=javascript>
		function Do(warning, url)
		{
		  if (window.confirm(warning)==false)
		    return;
		  WinOpen(url);
		}
		function ToDo(warning, url)
		{
		  if (window.confirm(warning)==false)
		    return;
		    window.location.href =url;
		  //WinOpen(url);
		}
		</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub2" runat="server" />
</asp:Content>


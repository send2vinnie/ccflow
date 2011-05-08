<%@ page title="" language="C#" masterpagefile="~/WF/Admin/WinOpen.master" autoeventwireup="true" inherits="WF_Admin_StartFlow, App_Web_h5o3zino" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width='80%' align=center>
<tr>
<td valign=top width='30%'><uc1:Pub ID="Pub2" runat="server" /></td>
<td valign=top width='70%'><uc1:Pub ID="Pub1" runat="server" /></td>
</tr>
</table>
    
</asp:Content>


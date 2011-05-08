<%@ control language="C#" autoeventwireup="true" inherits="WF_Admin_UC_CondExt, App_Web_jafgmhfl" %>
<%@ Register src="CondBar.ascx" tagname="CondBar" tagprefix="uc1" %>
<%@ Register src="Cond.ascx" tagname="Cond" tagprefix="uc2" %>
<table border=0 width='100%' >
<tr>
<td>
    <uc1:CondBar ID="CondBar1" runat="server" />
    </td>
</tr>

<tr>
<td>
    <uc2:Cond ID="Cond1" runat="server" />
    </td>
</tr>

</table>

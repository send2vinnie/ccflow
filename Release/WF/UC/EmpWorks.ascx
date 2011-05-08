<%@ control language="C#" autoeventwireup="true" inherits="WF_UC_EmpWorks, App_Web_dwfygdvu" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>
<table border="0"  width='100%' >
<tr>
<td valign=top width='20%' nowarp=true>
    <uc1:Pub ID="Left" runat="server" />
    </td>
<td valign=top width='80%' align=left>
    <uc1:Pub ID="Pub1" runat="server" />
    </td>
</tr>
</table>

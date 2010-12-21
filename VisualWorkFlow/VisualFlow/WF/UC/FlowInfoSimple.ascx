<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlowInfoSimple.ascx.cs" Inherits="WF_UC_FlowInfoSimple" %>
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


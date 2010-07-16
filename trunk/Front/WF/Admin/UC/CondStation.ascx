<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CondStation.ascx.cs" Inherits="WF_Admin_UC_CondSta" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="CondBar.ascx" tagname="CondBar" tagprefix="uc2" %>
<table border=0 width='100%' >
<tr>
<td>
    <uc2:CondBar ID="CondBar1" runat="server" />
    </td>
</tr>
<tr>
<td>
    <uc1:Pub ID="Pub1" runat="server" />
    </td>
</tr>
</table>
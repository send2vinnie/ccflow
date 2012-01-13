<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CalendarUC.ascx.cs" Inherits="WF_UC_CalendarUC" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<script type="text/javascript">
    function CelldblClick(v1, v2) {

    }
</script>
<style type="text/css">
.WorkCell
{
    font-weight:bolder;
    text-align:center;
    vertical-align:middle;
    background-color:Orange;
}
.Week
{
    background-color:ButtonFace;
    text-align:center;
    vertical-align:middle;
}
.Day
{
    text-align:center;
    vertical-align:middle;
}
</style>
<table border=0 style="width:80%">
<tr>
<td valign=top style="width:20%">
    <uc1:Pub ID="Left" runat="server" />
    </td>
<td valign=top>
    <uc1:Pub ID="Pub1" runat="server" />
    </td>
</tr>
</table>

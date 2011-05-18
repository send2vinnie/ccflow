<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MExt.ascx.cs" Inherits="WF_MapDef_UC_MExt" %>
<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<script type="text/javascript">
    function DoDel(mypk,fk_mapdata,extType) {
        if (window.confirm('您确定要删除吗？') == false)
            return;

        window.location.href = 'MapExt.aspx?DoType=Del&FK_MapData=' + fk_mapdata + '&ExtType=' + extType + '&MyPK=' + mypk;
    }
</script>
<table border=0 width='700px' height='300px' >
<tr>
<td valign=top align=left width='30%' >
    <uc1:Pub ID="Pub1" runat="server" />
    </td>
<td valign=top align=left width='70%'>
    <uc1:Pub ID="Pub2" runat="server" />
    </td>
</tr>
</table>

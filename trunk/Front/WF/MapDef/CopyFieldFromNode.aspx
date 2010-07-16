<%@ Page Language="C#" MasterPageFile="WinOpen.master" AutoEventWireup="true" CodeFile="CopyFieldFromNode.aspx.cs" Inherits="Comm_MapDef_CopyFieldFromNode" Title="无标题页" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<base target=_self />
<script language=javascript>
function Go(fk_node, cid,  seleNd)
{
    var   province=document.getElementById("_ctl0_ContentPlaceHolder1_Pub2_DDL1");
                var   pindex   =   province.selectedIndex;
                var   pValue   =   province.options[pindex].value;
                var   pText     =   province.options[pindex].text;
 // alert(pValue + pText);
  window.location.href='CopyFieldFromNode.aspx?FK_Node='+fk_node+'&NodeOfSelect='+pValue;
}

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width='100%'>
<tr>
    <TD valign=top >
        <uc1:Pub ID="Pub2" runat="server" />
    </TD>
    </tr>
    </table>
</asp:Content>


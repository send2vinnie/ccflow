﻿<%@ page language="C#" masterpagefile="WinOpen.master" autoeventwireup="true" inherits="Comm_MapDef_CopyFieldFromNode, App_Web_gihorolk" title="无标题页" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language=javascript>
function Go(FK_Node, cid,  seleNd)
{
    var   province=document.getElementById("_ctl0_ContentPlaceHolder1_Pub2_DDL1");
                var   pindex   =   province.selectedIndex;
                var   pValue   =   province.options[pindex].value;
                var   pText     =   province.options[pindex].text;
  window.location.href='CopyFieldFromNode.aspx?FK_Node='+FK_Node+'&NodeOfSelect='+pValue;
}
function GroupClick(groupID)
{
  
}
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width='100%'>
<tr>
    <TD valign=top   >
        <uc1:Pub ID="Pub2" runat="server" />
    </TD>
    </tr>
    </table>
</asp:Content>


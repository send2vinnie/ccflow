﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MapDef/WinOpen.master" AutoEventWireup="true" CodeFile="ExpImp.aspx.cs" Inherits="WF_MapDef_ExpImp" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function LoadFrm(fk_flow, refno, fk_Frm) {
        if (confirm('您确定吗？') == false)
            return;
        window.location.target = '_self';
        window.location.href = 'ExpImp.aspx?DoType=Imp&FK_Flow=" + fk_flow + "&RefNo=" +refno + "&FromMap=' + fk_Frm;
    }
</script>
<base target=_self />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


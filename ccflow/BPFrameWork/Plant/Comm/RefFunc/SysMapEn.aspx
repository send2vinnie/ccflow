﻿<%@ Page Language="C#" MasterPageFile="SysMapEn.master" AutoEventWireup="true" CodeFile="SysMapEn.aspx.cs" Inherits="Comm_RefFunc_SysMapEn" Title="Untitled Page" %>
<%@ Register src="SysMapEnUC.ascx" tagname="SysMapEnUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="Style/JScript.js"></script>
    <script language="javascript">
    function GroupBarClick(rowIdx) {
        var alt = document.getElementById('Img' + rowIdx).alert;
        var sta = 'block';
        if (alt == 'Max') {
            sta = 'block';
            alt = 'Min';
        } else {
            sta = 'none';
            alt = 'Max';
        }
        document.getElementById('Img' + rowIdx).src = './Img/' + alt + '.gif';
        document.getElementById('Img' + rowIdx).alert = alt;
        var i = 0
        for (i = 0; i <= 40; i++) {
            if (document.getElementById(rowIdx + '_' + i) == null)
                continue;
            document.getElementById(rowIdx + '_' + i).style.display = sta;
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:SysMapEnUC ID="SysMapEnUC1" runat="server" />
</asp:Content>


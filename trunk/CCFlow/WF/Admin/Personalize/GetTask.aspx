﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Admin/WinOpen.master" AutoEventWireup="true" CodeFile="GetTask.aspx.cs" Inherits="WF_Admin_Personalize_GetTask" %>
<%@ Register src="../UC/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function EditIt(fk_flow, nodeid) {
        var url = 'GetTask.aspx?RefNo=' + fk_flow + '&Step=3&FK_Node=' + nodeid;
        var v = window.showModalDialog(url, 'sd', 'dialogHeight: 550px; dialogWidth: 650px; dialogTop: 100px; dialogLeft: 150px; center: yes; help: no');
        window.location.href = window.location.href;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>
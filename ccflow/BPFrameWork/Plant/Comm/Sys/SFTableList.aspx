﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Comm/WinOpen.master" AutoEventWireup="true" CodeFile="SFTableList.aspx.cs" 
Inherits="Comm_Sys_SFTableList" %>

<%@ Register src="../UC/UCSys.ascx" tagname="UCSys" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Style/Table.css" rel="stylesheet" type="text/css" />
    <link href="../Style/Table0.css" rel="stylesheet" type="text/css" />
		<script language="JavaScript" src="../JScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:UCSys ID="UCSys1" runat="server" />
</asp:Content>


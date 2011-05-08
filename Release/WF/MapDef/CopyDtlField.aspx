<%@ page language="C#" masterpagefile="~/WF/MapDef/WinOpen.master" autoeventwireup="true" inherits="WF_MapDef_CopyDtlField, App_Web_gihorolk" title="Untitled Page" %>

<%@ Register src="UC/CopyDtlField.ascx" tagname="CopyDtlField" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="JS.js"></script>
	<base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:CopyDtlField ID="CopyDtlField1" runat="server" />
</asp:Content>


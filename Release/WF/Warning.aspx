<%@ page title="" language="C#" masterpagefile="WinOpen.master" autoeventwireup="true" inherits="WF_Warning, App_Web_5dpdp204" %>

<%@ Register src="UC/Warning.ascx" tagname="Warning" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<script language="JavaScript" src="./../Comm/JScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Warning ID="Warning1" runat="server" />
</asp:Content>
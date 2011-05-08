<%@ page title="" language="C#" masterpagefile="WinOpen.master" autoeventwireup="true" inherits="WF_ReturnWorkSmall, App_Web_5dpdp204" %>

<%@ Register src="UC/ReturnWork.ascx" tagname="ReturnWork" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="./../Comm/JScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ReturnWork ID="ReturnWork1" runat="server" />
</asp:Content>


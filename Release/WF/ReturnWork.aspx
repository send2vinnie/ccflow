<%@ page language="C#" masterpagefile="~/WF/MasterPage.master" autoeventwireup="true" inherits="WF_ReturnWork, App_Web_u0dbgd5p" title="Untitled Page" %>
<%@ Register src="UC/ReturnWork.ascx" tagname="ReturnWork" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="./../Comm/JScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ReturnWork ID="ReturnWork1" runat="server" />
</asp:Content>


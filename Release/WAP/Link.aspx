<%@ page language="C#" masterpagefile="~/WAP/MasterPage.master" autoeventwireup="true" inherits="WAP_Link, App_Web_lha3j40n" title="Link" %>

<%@ Register src="../WF/UC/Link.ascx" tagname="Link" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Link ID="Link1" runat="server" />
</asp:Content>


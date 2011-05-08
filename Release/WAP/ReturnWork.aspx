<%@ page language="C#" masterpagefile="~/WAP/MasterPage.master" autoeventwireup="true" inherits="WAP_ReturnWork, App_Web_lha3j40n" title="Untitled Page" %>
<%@ Register src="../WF/UC/ReturnWork.ascx" tagname="ReturnWork" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<base target=_self />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ReturnWork ID="ReturnWork1" runat="server" />
</asp:Content>


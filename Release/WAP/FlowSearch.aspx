<%@ page language="C#" masterpagefile="~/WAP/MasterPage.master" autoeventwireup="true" inherits="WAP_Search, App_Web_lha3j40n" title="Untitled Page" %>
<%@ Register src="../WF/UC/FlowSearch.ascx" tagname="FlowSearch" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:FlowSearch ID="FlowSearch1" runat="server" />
</asp:Content>


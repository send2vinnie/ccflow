<%@ page language="C#" masterpagefile="~/WAP/MasterPage.master" autoeventwireup="true" inherits="WAP_MyFlow, App_Web_lha3j40n" title="My Flow" %>
<%@ Register src="UC/MyFlowWap.ascx" tagname="MyFlowWap" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:MyFlowWap ID="MyFlowWap1" runat="server" />
</asp:Content>


<%@ page language="C#" masterpagefile="~/WAP/MasterPage.master" autoeventwireup="true" inherits="WAP_Msg, App_Web_lha3j40n" title="Untitled Page" %>

<%@ Register src="../WF/UC/Msg.ascx" tagname="Msg" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Msg ID="Msg1" runat="server" />
</asp:Content>


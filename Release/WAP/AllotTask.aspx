<%@ page language="C#" masterpagefile="~/WAP/MasterPage.master" autoeventwireup="true" inherits="WAP_AllotTask, App_Web_lha3j40n" title="Untitled Page" %>
<%@ Register src="../WF/UC/AllotTask.ascx" tagname="AllotTask" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:AllotTask ID="AllotTask1" runat="server" />
</asp:Content>


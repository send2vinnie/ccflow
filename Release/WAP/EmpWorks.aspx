<%@ page language="C#" masterpagefile="~/WAP/MasterPage.master" autoeventwireup="true" inherits="WAP_EmpWorks, App_Web_lha3j40n" title="Untitled Page" %>
<%@ Register src="../WF/UC/EmpWorksWap.ascx" tagname="EmpWorksWap" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:EmpWorksWap ID="EmpWorksWap1" runat="server" />
</asp:Content>


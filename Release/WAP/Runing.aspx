<%@ page title="" language="C#" masterpagefile="~/WAP/MasterPage.master" autoeventwireup="true" inherits="WAP_Runing, App_Web_lha3j40n" %>

<%@ Register src="../WF/UC/Runing.ascx" tagname="Runing" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Runing ID="Runing1" runat="server" />
</asp:Content>


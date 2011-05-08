<%@ page language="C#" masterpagefile="~/WAP/MasterPage.master" autoeventwireup="true" inherits="WAP_Login, App_Web_lha3j40n" title="Untitled Page" %>
<%@ Register src="../WF/UC/Login.ascx" tagname="Login" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align=center>
    <uc1:Login ID="Login1" runat="server" />
    </div>
</asp:Content>


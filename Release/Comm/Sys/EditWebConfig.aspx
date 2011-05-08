<%@ page language="C#" masterpagefile="~/Comm/MasterPage.master" autoeventwireup="true" inherits="Comm_Sys_EditWebconfig, App_Web_kzgztv25" title="System Seting" %>
<%@ Register src="../UC/UCSys.ascx" tagname="UCSys" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Style/Table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UCSys ID="UCSys1" runat="server" />
</asp:Content>


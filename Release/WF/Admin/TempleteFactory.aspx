<%@ page language="C#" masterpagefile="~/WF/Admin/WinOpen.master" autoeventwireup="true" inherits="WF_Admin_TempleteFactory, App_Web_h5o3zino" title="Untitled Page" %>

<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../../Comm/UC/UCEn.ascx" tagname="UCEn" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
    <uc2:UCEn ID="UCEn1" runat="server" />
</asp:Content>


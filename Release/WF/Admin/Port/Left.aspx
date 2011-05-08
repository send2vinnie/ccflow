<%@ page language="C#" masterpagefile="~/WF/Admin/Port/MasterPage.master" autoeventwireup="true" inherits="WF_Admin_Port_Left, App_Web_pe2il2mf" title="Untitled Page" %>

<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


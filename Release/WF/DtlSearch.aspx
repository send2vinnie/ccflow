<%@ page language="C#" masterpagefile="~/WF/MapDef/WinOpen.master" autoeventwireup="true" inherits="WF_DtlSearch, App_Web_5dpdp204" title="Untitled Page" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="./../Comm/JScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


<%@ page title="" language="C#" masterpagefile="~/WF/WinOpen.master" autoeventwireup="true" inherits="WF_MapDef_M2MDe, App_Web_gihorolk" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
<script src="../../Comm/JScript.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


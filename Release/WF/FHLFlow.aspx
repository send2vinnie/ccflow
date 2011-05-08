<%@ page title="" language="C#" masterpagefile="WinOpen.master" autoeventwireup="true" inherits="WF_FHLFlow, App_Web_5dpdp204" %>
<%@ Register src="UC/FHLFlow.ascx" tagname="FHLFlow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:FHLFlow ID="FHLFlow1" runat="server" />
</asp:Content>


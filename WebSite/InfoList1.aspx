<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InfoList1.aspx.cs" Inherits="InfoList1" Title="Untitled Page" %>

<%@ Register src="GE/InfoList/InfoList1/InfoList1.ascx" tagname="InfoList1" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:InfoList1 ID="InfoList11" runat="server" />
</asp:Content>


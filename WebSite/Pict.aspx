<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Pict.aspx.cs" Inherits="Pict" Title="Untitled Page" %>

<%@ Register src="GE/Pict/Pict.ascx" tagname="Pict" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pict ID="Pict1" runat="server" />
</asp:Content>


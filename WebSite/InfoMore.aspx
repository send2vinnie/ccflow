<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InfoMore.aspx.cs" Inherits="InfoMore" Title="Untitled Page" %>

<%@ Register src="GE/Info/Info/InfoMore.ascx" tagname="InfoMore" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <uc1:InfoMore ID="InfoMore1" runat="server" />
</asp:Content>


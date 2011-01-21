<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PictMore.aspx.cs" Inherits="PictMore" Title="Untitled Page" %>

<%@ Register src="GE/Pict/PictMore.ascx" tagname="PictMore" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:PictMore ID="PictMore1" runat="server" />
</asp:Content>


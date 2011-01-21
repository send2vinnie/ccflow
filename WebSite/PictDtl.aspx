<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PictDtl.aspx.cs" Inherits="PictDtl" Title="Untitled Page" %>

<%@ Register src="GE/Pict/PictDtl.ascx" tagname="PictDtl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:PictDtl ID="PictDtl1" runat="server" />
</asp:Content>


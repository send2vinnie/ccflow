<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SoftMore.aspx.cs" Inherits="SoftMore" Title="Untitled Page" %>

<%@ Register src="GE/Soft/SoftMore.ascx" tagname="SoftMore" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:SoftMore ID="SoftMore1" runat="server" />
</asp:Content>


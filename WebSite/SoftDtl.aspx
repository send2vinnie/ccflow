<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SoftDtl.aspx.cs" Inherits="SoftDtl" Title="Untitled Page" %>

<%@ Register src="GE/Soft/SoftDtl.ascx" tagname="SoftDtl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:SoftDtl ID="SoftDtl1" runat="server" />
</asp:Content>


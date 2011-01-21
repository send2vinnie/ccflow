<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Soft.aspx.cs" Inherits="Soft" Title="Untitled Page" %>

<%@ Register src="GE/Soft/Soft.ascx" tagname="Soft" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Soft ID="Soft1" runat="server" />
</asp:Content>


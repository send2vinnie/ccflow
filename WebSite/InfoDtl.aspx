<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InfoDtl.aspx.cs" Inherits="InfoDtl" Title="Untitled Page" %>

<%@ Register src="GE/Info/Info/InfoDtl.ascx" tagname="InfoDtl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:InfoDtl ID="InfoDtl1" runat="server" />
</asp:Content>


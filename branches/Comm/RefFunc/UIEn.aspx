<%@ Page Language="C#" MasterPageFile="~/Comm/RefFunc/MasterPage.master" AutoEventWireup="true" CodeFile="UIEn.aspx.cs" Inherits="Comm_RefFunc_En" Title="Untitled Page" %>
<%@ Register src="../UC/UIEn.ascx" tagname="UIEn" tagprefix="uc1" %>
<%@ Register src="Left.ascx" tagname="Left" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:Left ID="Left1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <uc1:UIEn ID="UIEn1" runat="server" />
</asp:Content>


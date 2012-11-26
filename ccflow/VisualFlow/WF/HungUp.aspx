<%@ Page Title="工作挂起" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="HungUp.aspx.cs" Inherits="WF_HungUp" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


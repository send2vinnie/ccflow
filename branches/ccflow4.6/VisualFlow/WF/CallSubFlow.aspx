﻿<%@ Page Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="CallSubFlow.aspx.cs" Inherits="WF_CallSubFlow" Title="Untitled Page" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
    <uc1:Pub ID="Pub2" runat="server" />
</asp:Content>


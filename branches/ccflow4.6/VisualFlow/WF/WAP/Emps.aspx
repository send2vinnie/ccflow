﻿<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Emps.aspx.cs" Inherits="WAP_Emps" Title="Untitled Page" %>

<%@ Register src="../UC/Emps.ascx" tagname="Emps" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Emps ID="Emps1" runat="server" />
</asp:Content>


﻿<%@ Page Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="Start.aspx.cs" Inherits="WF_Start" Title="无标题页" %>

<%@ Register src="UC/Start.ascx" tagname="Start" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Start ID="Start1" runat="server" />
</asp:Content>


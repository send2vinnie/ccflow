﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="BatchSmallSingle.aspx.cs" Inherits="WF_BatchSmallSingle" %>

<%@ Register src="UC/Batch.ascx" tagname="Batch" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Batch ID="Batch1" runat="server" />
</asp:Content>
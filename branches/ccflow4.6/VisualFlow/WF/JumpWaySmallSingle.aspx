﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="JumpWaySmallSingle.aspx.cs" Inherits="WF_JumpWaySmallSingle" %>

<%@ Register src="UC/JumpWay.ascx" tagname="JumpWay" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:JumpWay ID="JumpWay1" runat="server" />
</asp:Content>


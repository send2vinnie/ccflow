﻿<%@ Page Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="MyFlow.aspx.cs" Inherits="WF_MyFlow" Title="流程处理" %>
<%@ Register src="UC/MyFlowUC.ascx" tagname="MyFlowUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MyFlowUC ID="MyFlowUC1" runat="server" />
    </asp:Content>


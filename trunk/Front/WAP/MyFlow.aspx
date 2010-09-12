<%@ Page Language="C#" MasterPageFile="~/WAP/MasterPage.master" AutoEventWireup="true" CodeFile="MyFlow.aspx.cs" Inherits="WAP_MyFlow" Title="Untitled Page" %>
<%@ Register src="UC/MyFlowWap.ascx" tagname="MyFlowWap" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:MyFlowWap ID="MyFlowWap1" runat="server" />
</asp:Content>


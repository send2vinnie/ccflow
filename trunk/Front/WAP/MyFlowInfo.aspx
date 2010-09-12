<%@ Page Language="C#" MasterPageFile="~/WAP/MasterPage.master" AutoEventWireup="true" CodeFile="MyFlowInfo.aspx.cs" Inherits="WAP_MyFlowInfo" Title="Untitled Page" %>

<%@ Register src="../WF/UC/MyFlowInfo.ascx" tagname="MyFlowInfo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MyFlowInfo ID="MyFlowInfo1" runat="server" />
</asp:Content>


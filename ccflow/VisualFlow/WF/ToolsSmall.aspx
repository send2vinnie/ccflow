<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="ToolsSmall.aspx.cs" Inherits="WF_ToolsSmall" %>

<%@ Register src="UC/Tools.ascx" tagname="Tools" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Tools ID="Tools1" runat="server" />
</asp:Content>


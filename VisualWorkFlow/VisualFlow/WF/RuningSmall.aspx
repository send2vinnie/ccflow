<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Style/WinOpen.master" AutoEventWireup="true" CodeFile="RuningSmall.aspx.cs" Inherits="WF_RuningSmall" %>

<%@ Register src="UC/Runing.ascx" tagname="Runing" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Runing ID="Runing1" runat="server" />
</asp:Content>


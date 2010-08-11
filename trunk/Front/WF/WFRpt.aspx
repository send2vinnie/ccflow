<%@ Page Language="C#" MasterPageFile="~/WF/Style/WinOpen.master" AutoEventWireup="true" CodeFile="WFRpt.aspx.cs" Inherits="WF_WFRpt" Title="未命名頁面" %>
<%@ Register src="UC/WFRpt.ascx" tagname="WFRpt" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:WFRpt ID="WFRpt1" runat="server" />
</asp:Content>


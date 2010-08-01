<%@ Page Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="WF_Calendar" Title="未命名頁面" %>
<%@ Register src="UC/Calendar/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:Calendar ID="Calendar1" runat="server" />
</asp:Content>


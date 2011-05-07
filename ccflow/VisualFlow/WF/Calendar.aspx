<%@ Page Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="WF_Calendar" Title="未命名頁面" %>
<%@ Register src="UC/Calendar/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="UC/Calendar/Today.css" rel="stylesheet" type="text/css" />
    <link href="UC/Calendar/TA.css" rel="stylesheet" type="text/css" />
   <script language="JavaScript" src="UC/Calendar/TA.js"></script>
   <script language="JavaScript" src="UC/Calendar/TAMenu.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:Calendar ID="Calendar1" runat="server" />
</asp:Content>


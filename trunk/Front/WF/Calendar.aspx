<%@ page language="C#" masterpagefile="~/WF/MasterPage.master" autoeventwireup="true" inherits="WF_Calendar, App_Web_gjlztgne" title="未命名頁面" %>
<%@ Register src="UC/Calendar/Calendar.ascx" tagname="Calendar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:Calendar ID="Calendar1" runat="server" />
</asp:Content>


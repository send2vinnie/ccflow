<%@ page language="C#" masterpagefile="~/WF/Admin/WinOpen.master" autoeventwireup="true" inherits="WF_Admin_CondStation, App_Web_h5o3zino" title="未命名頁面" %>
<%@ Register src="UC/CondStation.ascx" tagname="CondStation" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:CondStation ID="CondStation1" runat="server" />
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/WF/Style/WinOpen.master" AutoEventWireup="true" CodeFile="SMS.aspx.cs" Inherits="WF_SMS" Title="未命名頁面" %>
<%@ Register src="../UC/SMS.ascx" tagname="SMS" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:SMS ID="SMS1" runat="server" />
    </asp:Content>


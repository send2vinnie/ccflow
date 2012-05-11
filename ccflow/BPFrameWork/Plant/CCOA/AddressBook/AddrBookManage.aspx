<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="AddrBookManage.aspx.cs" Inherits="CCOA_AddressBook_AddrBookManage" %>

<%@ Register Src="~/CCOA/AddressBook/Form.ascx" TagName="Form" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <xuc:XToolBar ID="XToolBar" runat="server" title="新增联系人" />
    <uc:Form ID="Form" runat="server" />
</asp:Content>

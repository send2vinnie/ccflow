<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="NoticeManage.aspx.cs" Inherits="CCOA_News_NoticeManage" %>

<%@ Register Src="~/CCOA/News/uc/NewsForm.ascx" TagName="NewsForm" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc:NewsForm ID="NewsForm1" runat="server" />
</asp:Content>

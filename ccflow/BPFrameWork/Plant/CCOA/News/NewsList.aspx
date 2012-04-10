<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="NewsList.aspx.cs" Inherits="CCOA_News_NewsList" %>

<%@ Register Src="~/CCOA/Controls/XPager.ascx" TagName="XPager" TagPrefix="uc" %>
<%@ Register Src="~/CCOA/Controls/XToolBar.ascx" TagName="XToolBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc:XToolBar ID="XToolBar1" runat="server" Title="新闻" AddUrl="NewsManage.aspx" />
    <uc:XPager ID="XPager1" runat="server" />
</asp:Content>

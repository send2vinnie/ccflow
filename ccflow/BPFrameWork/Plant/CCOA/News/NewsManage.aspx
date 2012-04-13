<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="NewsManage.aspx.cs" Inherits="CCOA_News_NewsManage" %>

<%@ Register Src="~/CCOA/News/uc/NewsForm.ascx" TagName="NewsForm" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <xuc:XToolBar ID="XToolBar1" runat="server" title="新闻添加" />
    <div style="width: 96%">
        <uc:NewsForm ID="NewsForm1" runat="server" />
    </div>
</asp:Content>

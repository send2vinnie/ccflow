<%@ Page Title="" Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="Channel.aspx.cs" Inherits="CCOA_Admin_Channel" %>

<%@ Register Src="~/CCOA/Controls/XToolBar.ascx" TagName="XToolBar" TagPrefix="uc" %>
<%@ Register Src="~/CCOA/Admin/ChannelTree.ascx" TagName="ChannelTree" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc:XToolBar ID="XToolBar1" runat="server" Title="栏目维护" AddUrl="ChannelManage.aspx" />
    <uc:ChannelTree id="ChannelTree1" runat="server" />
</asp:Content>

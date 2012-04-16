<%@ Page Title="" Language="C#" MasterPageFile="~/SSO/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SSO_Default" %>
<%@ Register src="InfoBar.ascx" tagname="InfoBar" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
                <uc2:InfoBar ID="ToolBar1" runat="server" />
</asp:Content>


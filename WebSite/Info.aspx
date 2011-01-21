<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Info.aspx.cs" Inherits="Info" Title="驰骋工作流程引擎，工作流程管理系统" %>

<%@ Register src="GE/Info/Info/Info.ascx" tagname="Info" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Info ID="Info1" runat="server" />
</asp:Content>


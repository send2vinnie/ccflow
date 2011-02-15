<%@ Page Language="C#" MasterPageFile="~/WF/Style/WinOpen.master" AutoEventWireup="true" CodeFile="MyFlowSmall.aspx.cs" Inherits="WF_MyFlowSmall" Title="工作处理" %>
<%@ Register src="UC/MyFlowUC.ascx" tagname="MyFlowUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MyFlowUC ID="MyFlowUC1" runat="server" />
    </asp:Content>


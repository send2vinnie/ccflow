<%@ Page Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="MyFlow.aspx.cs" Inherits="WF_MyFlow" Title="无标题页" %>
<%@ Register src="UC/MyFlow.ascx" tagname="MyFlow" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MyFlow ID="MyFlow1" runat="server" />
</asp:Content>


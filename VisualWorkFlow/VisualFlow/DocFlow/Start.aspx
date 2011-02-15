<%@ Page Language="C#" MasterPageFile="~/DocFlow/Style/WinOpen.master" AutoEventWireup="true" CodeFile="Start.aspx.cs" Inherits="GovDoc_Start" Title="无标题页" %>

<%@ Register src="../WF/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


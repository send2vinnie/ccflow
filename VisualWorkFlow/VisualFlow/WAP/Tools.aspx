<%@ Page Language="C#" MasterPageFile="~/WAP/MasterPage.master" AutoEventWireup="true" CodeFile="Tools.aspx.cs" Inherits="WAP_Tool" Title="Untitled Page" %>

<%@ Register src="../WF/UC/Tools.ascx" tagname="Tools" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Tools ID="Tools1" runat="server" />
</asp:Content>


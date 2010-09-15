<%@ Page Language="C#" MasterPageFile="~/GE/Template/MasterPage.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" Title="无标题页" %>

<%@ Register src="GE/FDB/PartDirTab.ascx" tagname="PartDirTab" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:PartDirTab ID="PartDirTab1" runat="server" />
</asp:Content>


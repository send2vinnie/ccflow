<%@ Page Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="Forward.aspx.cs" Inherits="WF_Forward" Title="无标题页" %>

<%@ Register src="UC/Forward.ascx" tagname="Forward" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Forward ID="Forward1" runat="server" />
</asp:Content>


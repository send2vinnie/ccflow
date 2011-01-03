<%@ Page Language="C#" MasterPageFile="~/WF/MasterPage.master" AutoEventWireup="true" CodeFile="Tools.aspx.cs" Inherits="WF_Tools" Title="无标题页" %>

<%@ Register src="UC/Tools.ascx" tagname="Tools" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Tools ID="Tools1" runat="server" />
</asp:Content>

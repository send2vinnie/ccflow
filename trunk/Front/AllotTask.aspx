<%@ Page Language="C#" MasterPageFile="~/WF/MapDef/WinOpen.master" AutoEventWireup="true" CodeFile="AllotTask.aspx.cs" Inherits="WF_AllotTask" Title="无标题页" %>

<%@ Register src="UC/AllotTask.ascx" tagname="AllotTask" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:AllotTask ID="AllotTask1" runat="server" />
</asp:Content>


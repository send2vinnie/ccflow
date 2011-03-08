<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Style/WinOpen.master" AutoEventWireup="true" CodeFile="StartSmall.aspx.cs" Inherits="WF_StartSmall" %>

<%@ Register src="UC/EmpWorks.ascx" tagname="EmpWorks" tagprefix="uc1" %>

<%@ Register src="UC/Start.ascx" tagname="Start" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:Start ID="Start1" runat="server" />
</asp:Content>


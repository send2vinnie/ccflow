<%@ Page Language="C#" MasterPageFile="~/WF/Admin/WinOpen.master" AutoEventWireup="true" CodeFile="FtpSet.aspx.cs" Inherits="OA_FtpSet" Title="Untitled Page" %>

<%@ Register src="UC/FtpSet.ascx" tagname="FtpSet" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:FtpSet ID="FtpSet1" runat="server" />
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="WinOpen.master" AutoEventWireup="true" CodeFile="MyFlowInfoSmall.aspx.cs" Inherits="WF_MyFlowInfoSmall" %>
<%@ Register src="UC/MyFlowInfo.ascx" tagname="MyFlowInfo" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width='80%' >
<tr>
<td width='80%'>
    <uc2:MyFlowInfo ID="MyFlowInfo1" runat="server" />
</td>
</tr>
</table>

</asp:Content>


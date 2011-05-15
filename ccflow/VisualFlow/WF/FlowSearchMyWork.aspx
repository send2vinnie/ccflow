<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Style/WinOpen.master" AutoEventWireup="true" CodeFile="FlowSearchMyWork.aspx.cs" Inherits="WF_FlowSearchMyWork" %>
<%@ Register src="UC/FlowSearchMyWork.ascx" tagname="FlowSearchMyWork" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
		<script language="JavaScript" src="../Comm/JS/Calendar/WdatePicker.js" ></script>

       <link href="./../Comm/Style/Table.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:FlowSearchMyWork ID="FlowSearchMyWork1" runat="server" />
</asp:Content>


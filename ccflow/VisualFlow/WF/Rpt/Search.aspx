﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="WF_Rpt_Search" %>
<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../../Comm/UC/ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width='100%' border=0>
<tr>
<td>
    <uc2:ToolBar ID="ToolBar1" runat="server" />
    </td>
    </tr>
<tr>
<td>
    <uc1:Pub ID="Pub1" runat="server" />
    </td>
    </tr>
    <tr>
<td>
    <uc1:Pub ID="Pub2" runat="server" />
    </td>
    </tr>
    </table>
</asp:Content>


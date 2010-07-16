<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Face_Home" Title="无标题页" %>
<%@ Register src="../WF/Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
		<script language="JavaScript" src="../Comm/JScript.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>



<%@ Page Title="" Language="C#" MasterPageFile="~/WF/Admin/WinOpen.master" AutoEventWireup="true" CodeFile="FreeFrm.aspx.cs" Inherits="WF_MapDef_FreeFrm" %>
<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="FreeFrm.ascx" tagname="FreeFrm" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="FreeFrm.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div style="float:right" >
    <uc3:FreeFrm ID="FreeFrm1" runat="server" />
    </div>
</asp:Content>


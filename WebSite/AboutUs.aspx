<%@ Page Title="" Language="C#" MasterPageFile="~/MLeft.master" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="AboutUs" %>

<%@ Register src="GE/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="LinkUsAndDown.ascx" tagname="LinkUsAndDown" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:LinkUsAndDown ID="LinkUsAndDown1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


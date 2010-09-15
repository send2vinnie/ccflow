<%@ Page Language="C#" MasterPageFile="~/GE/Template/WinOpen.master" AutoEventWireup="true" CodeFile="GETab.aspx.cs" Inherits="Peng_GETab" Title="无标题页" %>

<%@ Register assembly="BP.GE" namespace="BP.GE.Ctrl" tagprefix="cc1" %>

<%@ Register src="../GE/Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <cc1:GeTab ID="GeTab1" runat="server" >
    </cc1:GeTab>
    <br />
    <br />
    <br />
    <br />
    <br />
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


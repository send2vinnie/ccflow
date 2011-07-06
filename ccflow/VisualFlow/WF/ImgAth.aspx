<%@ Page Title="" Language="C#" MasterPageFile="~/WF/WinOpen.master" AutoEventWireup="true" CodeFile="ImgAth.aspx.cs" Inherits="WF_ImgAth" %>

<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="./Style/ImgAth/jquery-1.3.1.min.js"></script>
    <script type="text/javascript" src="./Style/ImgAth/jquery.bitmapcutter.js"></script>
    <link rel="Stylesheet" type="text/css" href="./Style/ImgAth/jquery.bitmapcutter.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc1:Pub ID="Pub1" runat="server" />

</asp:Content>


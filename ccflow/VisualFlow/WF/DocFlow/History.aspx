<%@ Page Language="C#" MasterPageFile="~/WF/DocFlow/Style/WinOpen.master" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="GovDoc_History" Title="无标题页" %>

<%@ Register src="../../WF/Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


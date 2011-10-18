<%@ Page Language="C#" MasterPageFile="~/WF/DocFlow/MasterPage.master" AutoEventWireup="true" CodeFile="MyFlow.aspx.cs" Inherits="GovDoc_MyFlow" Title="无标题页" %>
<%@ Register src="../../WF/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../../Comm/UC/UCEn.ascx" tagname="UCEn" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="PubBar" runat="server" />
    <br />
    <uc2:UCEn ID="UCEn1" runat="server" />
    <br />
</asp:Content>


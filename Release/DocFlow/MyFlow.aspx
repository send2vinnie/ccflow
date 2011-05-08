<%@ page language="C#" masterpagefile="~/DocFlow/MasterPage.master" autoeventwireup="true" inherits="GovDoc_MyFlow, App_Web_vmqi53kc" title="无标题页" %>
<%@ Register src="../WF/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<%@ Register src="../Comm/UC/UCEn.ascx" tagname="UCEn" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="PubBar" runat="server" />
    <br />
    <uc2:UCEn ID="UCEn1" runat="server" />
    <br />
</asp:Content>


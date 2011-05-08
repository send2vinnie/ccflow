<%@ page language="C#" masterpagefile="~/DocFlow/MasterPage.master" autoeventwireup="true" inherits="GovDoc_FlowSearch, App_Web_vmqi53kc" title="无标题页" %>
<%@ Register src="../WF/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub2" runat="server" />
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


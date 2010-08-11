<%@ page language="C#" masterpagefile="MasterPage.master" autoeventwireup="true" inherits="Face_FlowSearch, App_Web_gjlztgne" title="查询与分析" %>
<%@ Register src="../WF/Pub.ascx" tagname="Pub" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align=center >
    <uc1:Pub ID="Pub1" runat="server" />
    <uc1:Pub ID="Pub2" runat="server" />
    </div>
</asp:Content>


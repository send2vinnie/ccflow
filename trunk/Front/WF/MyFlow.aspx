<%@ page language="C#" masterpagefile="~/WF/MasterPage.master" autoeventwireup="true" inherits="WF_MyFlow, App_Web_gjlztgne" title="无标题页" %>
<%@ Register src="UC/MyFlowUC.ascx" tagname="MyFlowUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MyFlowUC ID="MyFlowUC1" runat="server" />
    </asp:Content>


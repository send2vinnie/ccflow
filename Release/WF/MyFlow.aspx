<%@ page language="C#" masterpagefile="~/WF/MasterPage.master" autoeventwireup="true" inherits="WF_MyFlow, App_Web_u0dbgd5p" title="流程处理" %>
<%@ Register src="UC/MyFlowUC.ascx" tagname="MyFlowUC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MyFlowUC ID="MyFlowUC1" runat="server" />
    </asp:Content>


<%@ page language="C#" masterpagefile="~/WF/MasterPage.master" autoeventwireup="true" inherits="WF_MyFlowInfo, App_Web_gjlztgne" title="无标题页" %>

<%@ Register src="UC/MyFlowInfo.ascx" tagname="MyFlowInfo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MyFlowInfo ID="MyFlowInfo1" runat="server" />
</asp:Content>


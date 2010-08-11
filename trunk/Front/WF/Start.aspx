<%@ page language="C#" masterpagefile="~/WF/MasterPage.master" autoeventwireup="true" inherits="WF_Start, App_Web_gjlztgne" title="无标题页" %>

<%@ Register src="UC/Start.ascx" tagname="Start" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Start ID="Start1" runat="server" />
</asp:Content>


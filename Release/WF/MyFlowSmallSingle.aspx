<%@ page title="" language="C#" masterpagefile="WinOpen.master" autoeventwireup="true" inherits="WF_MyFlowSmallSingle, App_Web_5dpdp204" %>
<%@ Register src="UC/MyFlowUC.ascx" tagname="MyFlowUC" tagprefix="uc1" %>
<%@ Register src="UC/MyFlow.ascx" tagname="MyFlow" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="../Comm/JScript.js"></script>
    <script language="JavaScript" src="../Comm/JS/Calendar/WdatePicker.js" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <uc2:MyFlow ID="MyFlow1" runat="server" />

    
</asp:Content>
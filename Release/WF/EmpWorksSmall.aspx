<%@ page title="" language="C#" masterpagefile="WinOpen.master" autoeventwireup="true" inherits="WF_EmpWorksSmall, App_Web_5dpdp204" %>

<%@ Register src="UC/EmpWorks.ascx" tagname="EmpWorks" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:EmpWorks ID="EmpWorks1" runat="server" />
</asp:Content>


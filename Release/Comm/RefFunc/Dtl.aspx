<%@ page title="" language="C#" masterpagefile="~/Comm/RefFunc/MasterPage.master" autoeventwireup="true" inherits="Comm_RefFunc_Dtl, App_Web_aujvbjmf" %>

<%@ Register src="Dtl.ascx" tagname="Dtl" tagprefix="uc1" %>
<%@ Register src="Left.ascx" tagname="Left" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:Left ID="Left1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <uc1:Dtl ID="Dtl1" runat="server" />
</asp:Content>


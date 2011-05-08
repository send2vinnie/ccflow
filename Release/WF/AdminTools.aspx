<%@ page language="C#" masterpagefile="~/WF/MasterPage.master" autoeventwireup="true" inherits="WF_AdminTools, App_Web_u0dbgd5p" title="Untitled Page" %>

<%@ Register src="Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="JavaScript" src="../../Comm/JScript.js"　type="text/javascript"></script>
    <script> 
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub2" runat="server" />
</asp:Content>


<%@ page language="C#" masterpagefile="MasterPage.master" autoeventwireup="true" inherits="Face_Login, App_Web_u0dbgd5p" title="登陆" %>
<%@ Register src="UC/Login.ascx" tagname="Login" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align=center width='500px'>
    <uc2:Login ID="Login1" runat="server" />
    </div>
</asp:Content>


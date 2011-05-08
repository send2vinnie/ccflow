<%@ page language="C#" masterpagefile="~/WF/MasterPage.master" autoeventwireup="true" inherits="WF_Link, App_Web_u0dbgd5p" title="常用链接" %>

<%@ Register src="UC/Link.ascx" tagname="Link" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr>
<td>
    &nbsp;</td>
<td>
    <uc1:Link ID="Link2" runat="server" />
</td>
    </tr>
    </table>
</asp:Content>


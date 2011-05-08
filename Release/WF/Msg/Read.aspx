<%@ page language="C#" masterpagefile="~/WF/Style/WinOpen.master" autoeventwireup="true" inherits="WF_Msg_Read, App_Web_yh1jjfa5" title="无标题页" %>

<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<base target=_self /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Pub ID="Pub1" runat="server" />
</asp:Content>


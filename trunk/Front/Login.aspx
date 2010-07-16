<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" 
Inherits="Face_Login" Title="登陆" %>

<%@ Register src="../WF/Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script  language=javascript>
function ExitAuth( fk_emp )
{
   if (window.confirm('您确定要退出授权登陆模式吗？')==false)
       return;
       
    var url='Do.aspx?DoType=ExitAuth&FK_Emp='+fk_emp;
    WinShowModalDialog(url,'');
    window.location.href='Tools.aspx';
}
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align=center>
    <uc1:Pub ID="Pub2" runat="server" />
    </div>
</asp:Content>


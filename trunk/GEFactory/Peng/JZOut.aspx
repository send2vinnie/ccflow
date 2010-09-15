<%@ Page Language="C#" MasterPageFile="~/GE/Template/WinOpen.master" AutoEventWireup="true" CodeFile="JZOut.aspx.cs" Inherits="Peng_JZOut" Title="矩阵输出" %>

<%@ Register src="../GE/Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table align=center width='80%'>
<tr>
<td class=BigDoc>
    <uc1:Pub ID="Pub1" runat="server" />
</td>
</tr>
</table>
    
</asp:Content>


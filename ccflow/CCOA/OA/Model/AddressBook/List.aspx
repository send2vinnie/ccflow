<%@ Page Title="" Language="C#" MasterPageFile="~/Model/WinOpen.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="Model_AddressBook_List" %>

<%@ Register src="../Pub.ascx" tagname="Pub" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr>
<td><uc1:Pub ID="Pub1" runat="server" /></td>
<td><uc1:Pub ID="Pub2" runat="server" /></td>
</tr>
    </table>
</asp:Content>


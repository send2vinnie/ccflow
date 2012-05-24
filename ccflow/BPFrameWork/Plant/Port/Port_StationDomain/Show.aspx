<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="BP.EIP.Web.Port_StationDomain.Show" Title="显示页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>                   
                    <td class="tdbg">
                               
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		No
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblNo" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		FK_Station
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblFK_Station" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		FK_Domain
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblFK_Domain" runat="server"></asp:Label>
	</td></tr>
</table>

                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>





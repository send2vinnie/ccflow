<%@ Page Language="C#"  AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="BP.EIP.Web.Port_StationDomain.Add" Title="增加页" %>

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
		<asp:TextBox id="txtNo" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		FK_Station
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtFK_Station" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		FK_Domain
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtFK_Domain" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
</table>

            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <lizard:XButton ID="btnSave" runat="server" Text="保存"
                    OnClick="btnSave_Click"  onmouseover="this.className='lizard-button-hover'"
                    onmouseout="this.className='lizard-button'"></lizard:XButton>
                <lizard:XButton ID="btnCancle" runat="server" Text="取消"
                    OnClick="btnCancle_Click"  onmouseover="this.className='lizard-button-hover'"
                    onmouseout="this.className='lizard-button'"></lizard:XButton>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>

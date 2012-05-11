<%@ Page Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="Lizard.OA.Web.OA_SMS.Add" Title="增加页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		SmsId
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSmsId" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		发送号码
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSenderNumber" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		接收号码
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtReciveNumber" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		发送内容
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSendContent" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		接收内容
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtReciveConent" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		发送时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtSendTime" runat="server" Width="70px"  ></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		接收时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtReciveTime" runat="server" Width="70px"  ></asp:TextBox>
	</td></tr>
</table>
<script src="/js/calendar1.js" type="text/javascript"></script>

            </td>
        </tr>
        <tr>
            <td class="tdbg" align="center" valign="bottom">
                <asp:Button ID="btnSave" runat="server" Text="保存"
                    OnClick="btnSave_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                    onmouseout="this.className='inputbutton'"></asp:Button>
                <asp:Button ID="btnCancle" runat="server" Text="取消"
                    OnClick="btnCancle_Click" class="inputbutton" onmouseover="this.className='inputbutton_hover'"
                    onmouseout="this.className='inputbutton'"></asp:Button>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>

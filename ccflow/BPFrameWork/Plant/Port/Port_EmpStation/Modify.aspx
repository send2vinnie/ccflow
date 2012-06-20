<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="BP.EIP.Web.Port_EmpStation.Modify" Title="修改页" %>
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
		<asp:label id="lblNo" runat="server"></asp:label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		FK_Emp
	：</td>
	<td height="25" width="*" align="left">
		<asp:label id="lblFK_Emp" runat="server"></asp:label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		工作岗位, 主外键:对应物理表
	：</td>
	<td height="25" width="*" align="left">
		<asp:label id="lblFK_Station" runat="server"></asp:label>
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
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>


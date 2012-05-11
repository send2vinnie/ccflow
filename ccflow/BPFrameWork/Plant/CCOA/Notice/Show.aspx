<%@ Page Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="Lizard.OA.Web.OA_Notice.Show" Title="显示页" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>                   
                    <td class="tdbg">
                               
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		主键Id
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblNoticeId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		通告标题
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblNoticeTitle" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		副标题
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblNoticeSubTitle" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		通告类型
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblNoticeType" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		通告内容
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblNoticeContent" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		发布人
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblAuthor" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		发布时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblCreateTime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		点击量
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblClicks" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		是否阅读
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblIsRead" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		更新时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblUpDT" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		更新人
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblUpUser" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		状态
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblStatus" runat="server"></asp:Label>
	</td></tr>
</table>

                    </td>
                </tr>
            </table>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>





<%@ Page Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="Lizard.OA.Web.OA_Meeting.Show" Title="显示页" %>
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
		<asp:Label id="lblMeetingId" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		议题
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblTopic" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		计划开始时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblPlanStartTime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		计划结束时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblPlanEndTime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		计划召开地址
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblPlanAddress" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		计划参加人员
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblPlanMembers" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		实际开始时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblRealStartTime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		实际结束时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblRealEndTime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		实际召开地址
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblRealAddress" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		实际参加人员
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblRealMembers" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		记录人
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblRecorder" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		会议纪要
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblSummary" runat="server"></asp:Label>
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
		更新时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblUpDT" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		状态：0-未召开1-已召开
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





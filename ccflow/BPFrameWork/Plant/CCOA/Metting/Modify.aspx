<%@ Page Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true" CodeFile="Modify.aspx.cs" Inherits="Lizard.OA.Web.OA_Meeting.Modify" Title="修改页" %>
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
		<asp:label id="lblMeetingId" runat="server"></asp:label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		议题
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtTopic" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		计划开始时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtPlanStartTime" runat="server" Width="70px"  ></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		计划结束时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtPlanEndTime" runat="server" Width="70px"  ></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		计划召开地址
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtPlanAddress" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		计划参加人员
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtPlanMembers" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		实际开始时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtRealStartTime" runat="server" Width="70px"  ></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		实际结束时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtRealEndTime" runat="server" Width="70px"  ></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		实际召开地址
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtRealAddress" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		实际参加人员
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtRealMembers" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		记录人
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtRecorder" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		会议纪要
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSummary" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		更新人
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtUpUser" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		更新时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtUpDT" runat="server" Width="70px"  ></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		状态：0-未召开1-已召开
	：</td>
	<td height="25" width="*" align="left">
		<asp:CheckBox ID="chkStatus" Text="状态：0-未召开1-已召开" runat="server" Checked="False" />
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
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>--%>


<%@ Page Language="C#" MasterPageFile="~/CCOA/WinOpen.master" AutoEventWireup="true" CodeFile="Modify.aspx.cs" Inherits="Lizard.OA.Web.OA_Notice.Modify" Title="修改页" %>
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
		<asp:label id="lblNoticeId" runat="server"></asp:label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		通告标题
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtNoticeTitle" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		副标题
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtNoticeSubTitle" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		通告类型
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtNoticeType" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		通告内容
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtNoticeContent" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		发布人
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtAuthor" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		发布时间
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox ID="txtCreateTime" runat="server" Width="70px"  ></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		点击量
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtClicks" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		是否阅读
	：</td>
	<td height="25" width="*" align="left">
		<asp:CheckBox ID="chkIsRead" Text="是否阅读" runat="server" Checked="False" />
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
		更新人
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtUpUser" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		状态
	：</td>
	<td height="25" width="*" align="left">
		<asp:CheckBox ID="chkStatus" Text="状态" runat="server" Checked="False" />
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


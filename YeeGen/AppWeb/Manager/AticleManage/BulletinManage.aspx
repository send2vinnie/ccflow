<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulletinManage.aspx.cs" Inherits="Tax666.AppWeb.Manager.AticleManage.BulletinManage" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="Tax666.WebControls" namespace="Tax666.WebControls" tagprefix="Tax666WebControls" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <%=Global.GetHeadInfo()%>
</head>
<body>
  <form id="form1" runat="server">
        <div class="main">
           <Tax666WebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="对系统公告信息进行添加、修改、浏览和删除等操作" HeadTitleTxt="系统公告信息管理" HeadHelpTxt="点击公告标题可对该公告属性进行编辑修改">
                <Tax666WebControls:HeadMenuButtonItem ButtonUrlType="Href" ButtonVisible="false" />
            </Tax666WebControls:HeadMenuWebControls>
            <Tax666WebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
                <Tax666WebControls:TabOptionItem id="TabOptionItem1" runat="server" Tab_Name="系统公告信息列表">
                    <table class="tableBorder1" cellSpacing="1" cellPadding="0" align="center">
	                    <tr><td class="TableBody2" noWrap align="left" style="height:36px;padding-left:15px;"><%--公告发布有效范围：--%>
			                <%--<asp:dropdownlist id="dplScore" runat="server" AutoPostBack="True" Width="168px" OnSelectedIndexChanged="dplScore_SelectedIndexChanged">
				                <asp:ListItem Value="0">不限范围</asp:ListItem>
				                <asp:ListItem Value="1">本系统内有效</asp:ListItem>
				                <asp:ListItem Value="2">所有产品系列(需审核)</asp:ListItem>--%>
			                <%--</asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;--%> 审核情况：
		                    <asp:dropdownlist id="dplAudit" runat="server" Width="128px" AutoPostBack="True" OnSelectedIndexChanged="dplScore_SelectedIndexChanged">
		                        <asp:ListItem Value="2">不限</asp:ListItem>
			                    <asp:ListItem Value="1">通过审核</asp:ListItem>
			                    <asp:ListItem Value="0">未审核</asp:ListItem>
		                    </asp:dropdownlist>
	                    </td></tr>
                    </table>
                    <table class="tableBorder1" cellspacing="1" cellpadding="0" align="center">
	                    <tr>
		                    <th width="36">&nbsp;</th>
                            <th width="*">公告标题</th>
                            <th width="80">发布人</th>
                            <th width="80">开始时间</th>
                            <th width="80">结束时间</th>
                            <th width="48">已结束?</th>
                            <th width="48">审核?</th>
		                    <th width="54">排序</th>
                        </tr>
                    </table>
                    <table class="tableBorder4" id="tabNoRec" cellSpacing="1" cellPadding="0" align="center" runat="server" style="height:32px;">
	                    <tr>
		                    <td class="TableBody1">&nbsp;&nbsp;对不起，该类别下没有满足条件的记录列表。</td>
	                    </tr>
                    </table>
                    <asp:datagrid id="listRecord" runat="server" CssClass="tableBorder1" Width="100%" GridLines="None"
	                    AutoGenerateColumns="False" CellPadding="0" HorizontalAlign="Center" ShowHeader="False" CellSpacing="1" OnItemCommand="listRecord_ItemCommand">
	                    <Columns>
	                        <asp:TemplateColumn HeaderText="操作">
			                    <ItemStyle HorizontalAlign="Center" Width="36px" CssClass="TableBody2"></ItemStyle>
			                    <ItemTemplate>
				                    <asp:CheckBox id="chk" runat="server"></asp:CheckBox>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="公告标题">
			                    <ItemStyle CssClass="TableBody1" HorizontalAlign="Left"></ItemStyle>
			                    <ItemTemplate>
				                    <a href='<%=Global.WebPath%>/Manager/AticleManage/BulletinEdit.aspx?bulletinid=<%#DataBinder.Eval(Container.DataItem,"BulletinID")%>' title='<%#DataBinder.Eval(Container.DataItem,"BulletinTitle")%>'>
					                    <%# Tax666.Common.StringUtil.ControlStrLength(DataBinder.Eval(Container.DataItem, "BulletinTitle").ToString(), 50)%>
				                    </a>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                <%--    <asp:BoundColumn DataField="PublishName" HeaderText="发布人">
			                    <ItemStyle Wrap="False" Width="80px" CssClass="TableBody1" HorizontalAlign="Center" Font-Size="8pt"></ItemStyle>
		                    </asp:BoundColumn>--%>
		                    <asp:BoundColumn DataField="StartTime" HeaderText="开始日期" DataFormatString="{0:yyyy-MM-dd}">
			                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="80px" CssClass="TableBody2"></ItemStyle>
		                    </asp:BoundColumn>
		                    <asp:BoundColumn DataField="EndTime" HeaderText="结束日期" DataFormatString="{0:yyyy-MM-dd}">
			                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="80px" CssClass="TableBody2"></ItemStyle>
		                    </asp:BoundColumn>
		                   <asp:TemplateColumn HeaderText="结束">
			                    <ItemStyle HorizontalAlign="Center" Width="48px" CssClass="TableBody1"></ItemStyle>
			                    <ItemTemplate>
				                    <%#((DateTime)DataBinder.Eval(Container.DataItem,"EndTime"))>DateTime.Now ? "<b>否</b>" : "<font color=red face=Arial><b>是</b></font>"%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="审核">
			                    <ItemStyle HorizontalAlign="Center" Width="48px" CssClass="TableBody2"></ItemStyle>
			                    <ItemTemplate>
				                    <%#((bool)DataBinder.Eval(Container.DataItem, "IsAudit")) ? "<b>√</b>" : "<b><font color=red>×</font></b>"%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="排序">
			                    <ItemStyle HorizontalAlign="Center" Width="54px" CssClass="TableBody1"></ItemStyle>
			                    <ItemTemplate>
				                    <asp:ImageButton id="UpImageButton" runat="server" AlternateText="上移" ImageUrl="~/Manager/Images/up01.gif" CommandName="OrderUp"></asp:ImageButton><br />
				                    <asp:ImageButton id="DownImageButton" runat="server" AlternateText="下移" ImageUrl="~/Manager/Images/down01.gif" CommandName="OrderDown"></asp:ImageButton>
				                    <asp:Label id="BulletinIDLabel" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "BulletinID") %>'></asp:Label>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
	                    </Columns>
	                </asp:datagrid>
	                <table class="tableBorder4" cellSpacing="0" cellPadding="0" align="center">
	                    <tr>
		                    <td class="TableBody1" vAlign="middle" align="left" style="height:40px;">&nbsp;
		                        <input id="CheckAll" name="CheckAll" type="checkbox" onclick="ChooseAll()"/><label for="CheckAll">选择当前页面所有记录</label>&nbsp;&nbsp;
		                        <asp:button id="btnAudit" runat="server" Text="审核有效/无效" CssClass="btnbig" OnClick="btnAudit_Click"></asp:button>&nbsp;&nbsp;
			                    <asp:button id="btnDelete" runat="server" Text="删除选中记录" CssClass="btnbig" CausesValidation="False" OnClick="btnDelete_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
			                    <asp:button id="btnAdd" runat="server" Text="添加公告" CssClass="btnbig" CausesValidation="False" OnClick="btnAdd_Click"></asp:button>
		                    </td>
	                    </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="font-size:12px;font-family:Tahoma;color:#666;" height="32">
                        <tr>
                            <td width="30%" align="left">总记录：<%=m_RecordCount%>&nbsp;&nbsp;每页：10&nbsp;&nbsp;当前页：<%=m_CurrentPageIndex%>/<%=m_PageCount%></td>
                            <td width="70%" align="right"><webdiyer:AspNetPager CssClass="pages" CurrentPageButtonClass="cpb" ID="pagesTrade" runat="server" AlwaysShow="True" FirstPageText="首页" Font-Size="12px" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页" ShowBoxThreshold="1" OnPageChanged="pagesTrade_PageChanged" PageIndexBoxClass="inputbox" ShowPageIndexBox="Always" SubmitButtonClass="btngo" TextAfterPageIndexBox="页" TextBeforePageIndexBox="第" NumericButtonCount="5"></webdiyer:AspNetPager></td>
                        </tr>
                    </table>
                </Tax666WebControls:TabOptionItem>
            </Tax666WebControls:TabOptionWebControls> 
        </div>
    </form>
</body>
</html>

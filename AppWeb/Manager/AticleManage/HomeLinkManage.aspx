<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeLinkManage.aspx.cs" Inherits="Tax666.AppWeb.Manager.AticleManage.HomeLinkManage" %>
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
            <Tax666WebControls:HeadMenuWebControls ID="HeadMenuWebControls1" runat="server" HeadOPTxt="对网站友情链接记录进行添加、修改、浏览、删除和审核等操作" HeadTitleTxt="网站友情链接记录管理" HeadHelpTxt="点击网站友情链接标题可对该记录属性进行编辑修改">
                <Tax666WebControls:HeadMenuButtonItem ButtonUrlType="Href" ButtonVisible="false" />
            </Tax666WebControls:HeadMenuWebControls>
            <Tax666WebControls:TabOptionWebControls ID="TabOptionWebControls1" runat="server">
                <Tax666WebControls:TabOptionItem id="TabOptionItem1" runat="server" Tab_Name="网站友情链接记录列表">
                    <table class="tableBorder1" cellSpacing="1" cellPadding="0" align="center">
	                    <tr><td class="TableBody2" noWrap align="left" style="height:36px;padding-left:15px;">友情链接类型
						    <asp:dropdownlist id="dplType" runat="server" AutoPostBack="True" Width="120px" OnSelectedIndexChanged="dplType_SelectedIndexChanged"></asp:dropdownlist><span style="padding:0 15px;">有效：
						    <asp:dropdownlist id="dplvalid" runat="server" Width="80px" AutoPostBack="True" OnSelectedIndexChanged="dplType_SelectedIndexChanged">
						        <asp:ListItem Value="2">不限</asp:ListItem>
							    <asp:ListItem Value="1">有效链接</asp:ListItem>
							    <asp:ListItem Value="0">无效链接</asp:ListItem>
						    </asp:dropdownlist></span>审核：
						    <asp:dropdownlist id="dplAudit" runat="server" Width="108px" AutoPostBack="True" OnSelectedIndexChanged="dplType_SelectedIndexChanged">
						        <asp:ListItem Value="2">全部链接</asp:ListItem>
							    <asp:ListItem Value="0">未审核链接</asp:ListItem>
							    <asp:ListItem Value="1">审核通过链接</asp:ListItem>
						    </asp:dropdownlist>
	                    </td></tr>
                    </table>
                    <table class="tableBorder1" cellSpacing="1" cellPadding="0" align="center">
	                    <tr>
		                    <th width="36">选择</th>
		                    <th width="108">链接名称</th>
		                    <th width="100">网站性质</th>
		                    <th width="*">网站链接地址</th>
		                    <th width="100">网站Logo</th>
		                    <th width="64">添加日期</th>
                            <th width="36">审核</th>
		                    <th width="36">有效</th>
		                    <th width="48">排序</th>
	                    </tr>
                    </table>
                    <table class="tableBorder4" id="tabNoRec" cellSpacing="1" cellPadding="5" align="center" runat="server" style="height:32px;">
	                    <tr>
		                    <td class="TableBody1">&nbsp;&nbsp;对不起，该类别下没有满足条件的记录列表。</td>
	                    </tr>
                    </table>
                     <asp:datagrid id="listRecord" runat="server" CssClass="tableBorder1" Width="100%" GridLines="None" AutoGenerateColumns="False" CellPadding="0" HorizontalAlign="Center" ShowHeader="False" CellSpacing="1" ItemStyle-Height="40px">
                        <Columns>
	                        <asp:TemplateColumn HeaderText="操作">
			                    <ItemStyle HorizontalAlign="Center" Width="36px" CssClass="TableBody1"></ItemStyle>
			                    <ItemTemplate>
				                    <asp:CheckBox id="chk" runat="server"></asp:CheckBox>				
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="链接名称">
			                    <ItemStyle CssClass="TableBody1" HorizontalAlign="center" Width="108" Font-Size="8pt"></ItemStyle>
			                    <ItemTemplate>
		                            <a href='<%=Global.WebPath%>/Manager/AticleManage/HomeLinkEdit.aspx?linkid=<%#DataBinder.Eval(Container.DataItem,"LinkID")%>' class="a1" title='<%# DataBinder.Eval(Container.DataItem, "HomeDesc")%>'>
			                            <%# Tax666.Common.StringUtil.ControlStrLength(DataBinder.Eval(Container.DataItem, "LinkName").ToString(), 36)%>
		                            </a>
	                            </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="网站性质">
			                    <ItemStyle CssClass="TableBody1" HorizontalAlign="center" Width="100px" Font-Size="8pt" Font-Names="Tahoma"></ItemStyle>
			                    <ItemTemplate>
				                    <%# DataBinder.Eval(Container.DataItem, "TypeName")%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:BoundColumn DataField="LinkUrl" HeaderText="网站链接地址">
			                    <ItemStyle CssClass="TableBody1" HorizontalAlign="Left" Font-Size="8pt" Font-Names="Tahoma"></ItemStyle>
		                    </asp:BoundColumn>
		                    <asp:TemplateColumn HeaderText="网站Logo">
			                    <ItemStyle CssClass="TableBody1" Width="100" HorizontalAlign="Center"></ItemStyle>
			                    <ItemTemplate>
				                    <%# DataBinder.Eval(Container.DataItem,"LogoPic")%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:BoundColumn DataField="CreateTime" HeaderText="添加日期" DataFormatString="{0:yyyy-MM-dd}">
			                    <ItemStyle HorizontalAlign="Right" Width="64px" CssClass="TableBody2" Font-Size="8pt" Font-Names="Tahoma"></ItemStyle>
		                    </asp:BoundColumn>
		                    <asp:TemplateColumn HeaderText="审核">
			                    <ItemStyle HorizontalAlign="Center" Width="36px" CssClass="TableBody2"></ItemStyle>
			                    <ItemTemplate>
				                    <%#((bool)DataBinder.Eval(Container.DataItem, "IsAudit")) ? "<b>√</b>" : "<b><font color=red>×</font></b>"%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="有效">
			                    <ItemStyle HorizontalAlign="Center" Width="36px" CssClass="TableBody2"></ItemStyle>
			                    <ItemTemplate>
				                    <%#((bool)DataBinder.Eval(Container.DataItem, "IsAvailable")) ? "<b>√</b>" : "<b><font color=red>×</font></b>"%>
			                    </ItemTemplate>
		                    </asp:TemplateColumn>
		                    <asp:TemplateColumn HeaderText="排序">
			                    <ItemStyle HorizontalAlign="Center" Width="48px" CssClass="TableBody1"></ItemStyle>
			                    <ItemTemplate>
                                    <asp:ImageButton id="UpImageButton" runat="server" AlternateText="上移" ImageUrl="~/Manager/Images/up01.gif" CommandName="OrderUp"></asp:ImageButton><br />
                                    <asp:ImageButton id="DownImageButton" runat="server" AlternateText="下移" ImageUrl="~/Manager/Images/down01.gif" CommandName="OrderDown"></asp:ImageButton>
                                    <asp:Label id="LinkIDLabel" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "LinkID") %>'></asp:Label>
                                </ItemTemplate>
		                    </asp:TemplateColumn>
	                    </Columns>
                    </asp:datagrid>
                    <table class="tableBorder4" cellSpacing="0" cellPadding="0" align="center">
	                    <tr>
		                    <td class="TableBody1" vAlign="middle" align="left" style="height:40px;">&nbsp;
		                        <input id="CheckAll" name="CheckAll" type="checkbox" onclick="ChooseAll()"/><label for="CheckAll">选择当前页面所有记录</label>&nbsp;&nbsp;
			                    <asp:button id="btnValid" runat="server" Text="设置有效/无效" CssClass="btnbig" OnClick="btnValid_Click"></asp:button>&nbsp;
			                    <asp:button id="btnAudit" runat="server" Text="审核通过/取消" CssClass="btnbig" OnClick="btnAudit_Click"></asp:button>&nbsp;
			                    <asp:button id="btnDelete" runat="server" Text="删除选中记录" CssClass="btnbig" OnClick="btnDelete_Click"></asp:button>&nbsp;
			                    <asp:button id="btnAdd" runat="server" Text="添加新链接" CssClass="btnbig" OnClick="btnAdd_Click"></asp:button>
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

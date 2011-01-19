<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleSmallType.ascx.cs" Inherits="Tax666.AppWeb.UserControls.ArticleSmallType" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<table class="tableBorder1" cellSpacing="1" cellPadding="0" align="center">
    <tr><td class="TableBody2" noWrap align="left" style="height:36px;padding-left:15px;">文章大类
        <asp:DropDownList ID="dplBigType" runat="server" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="dplBigType_SelectedIndexChanged">
        </asp:DropDownList><span style="padding-left:15px;">有效
        <asp:dropdownlist id="dplvalid" runat="server" Width="128px" AutoPostBack="True" OnSelectedIndexChanged="dplBigType_SelectedIndexChanged">
            <asp:ListItem Value="2">不限</asp:ListItem>
            <asp:ListItem Value="1">有效小类</asp:ListItem>
            <asp:ListItem Value="0">无效小类</asp:ListItem>
        </asp:dropdownlist></span>
    </td></tr>
</table>
<table class="tableBorder1" cellSpacing="1" cellPadding="0" align="center">
    <tr>
        <th width="36">选择</th>
        <th width="148">文章小类名称</th>
        <th width="*">文章小类描述</th>
        <th width="120">所属文章大类</th>
        <th width="48">有效？</th>
        <th width="48">系统？</th>
    </tr>
</table>
<table class="tableBorder4" id="tabNoRec" cellSpacing="1" cellPadding="5" align="center" runat="server" style="height:32px;">
    <tr>
        <td class="TableBody1">&nbsp;&nbsp;对不起，该类别下没有满足条件的记录列表。</td>
    </tr>
</table>
<asp:datagrid id="listRecord" runat="server" CssClass="tableBorder1" Width="100%" GridLines="None" AutoGenerateColumns="False" CellPadding="0" HorizontalAlign="Center" ShowHeader="False" CellSpacing="1">
    <Columns>
        <asp:TemplateColumn HeaderText="操作">
            <ItemStyle HorizontalAlign="Center" Width="36px" CssClass="TableBody2"></ItemStyle>
            <ItemTemplate>
                <asp:CheckBox id="chk" runat="server"></asp:CheckBox>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="类别名称">
            <ItemStyle CssClass="TableBody1" Width="148px" HorizontalAlign="Center"></ItemStyle>
            <ItemTemplate>
	            <a href='<%=Global.WebPath%>/Manager/AticleManage/ArticleSmallTypeEdit.aspx?smallid=<%#DataBinder.Eval(Container.DataItem,"SmallTypeID")%>&type=1' title='<%#DataBinder.Eval(Container.DataItem,"SmallTypeName")%>'>
		            <%# DataBinder.Eval(Container.DataItem, "SmallTypeName").ToString()%>
	            </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="SmallTypeDesc" HeaderText="类别描述">
            <ItemStyle CssClass="TableBody1" HorizontalAlign="Center"></ItemStyle>
        </asp:BoundColumn>
        <asp:BoundColumn DataField="BigTypeName" HeaderText="所属大类">
            <ItemStyle Wrap="False" Width="120px" CssClass="TableBody1" HorizontalAlign="Center"></ItemStyle>
        </asp:BoundColumn>
        <asp:TemplateColumn HeaderText="有效">
            <ItemStyle HorizontalAlign="Center" Width="48px" CssClass="TableBody2"></ItemStyle>			
            <ItemTemplate>
	            <%#((bool)DataBinder.Eval(Container.DataItem,"IsAvailable")) ? "<b>√</b>" : "<b><font color=red>×</font></b>"%>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="系统">
            <ItemStyle HorizontalAlign="Center" Width="48px" CssClass="TableBody2"></ItemStyle>			
            <ItemTemplate>
	            <%#((bool)DataBinder.Eval(Container.DataItem,"IsSystem")) ? "<b>√</b>" : "<b><font color=red>×</font></b>"%>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:datagrid>
<table class="tableBorder4" cellSpacing="0" cellPadding="0" align="center">
    <tr>
        <td class="TableBody1" vAlign="middle" align="left" style="height:40px;padding-left:15px;">
            <asp:button id="btnDel" runat="server" Text="设置为有效/无效" CssClass="btnbig" OnClick="btnDel_Click"></asp:button>&nbsp;&nbsp;
            <asp:button id="btnSystem" runat="server" Text="设置为系统/私有" CssClass="btnbig" OnClick="btnSystem_Click"></asp:button>&nbsp;&nbsp;
            <asp:button id="btnDelete" runat="server" Text="删除选中的类别" CssClass="btnbig" OnClick="btnDelete_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:button id="btnAddTab" runat="server" Text="添加新小类" CssClass="btnbig" OnClick="btnAddTab_Click"></asp:button>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" border="0" width="100%" style="font-size:12px;font-family:Tahoma;color:#666;" height="32">
    <tr>
        <td width="30%" align="left">总记录：<%=m_RecordCount%>&nbsp;&nbsp;每页：10&nbsp;&nbsp;当前页：<%=m_CurrentPageIndex%>/<%=m_PageCount%></td>
        <td width="70%" align="right"><webdiyer:AspNetPager CssClass="pages" CurrentPageButtonClass="cpb" ID="pagesTrade" runat="server" AlwaysShow="True" FirstPageText="首页" Font-Size="12px" LastPageText="尾页" NextPageText="下一页" PageSize="10" PrevPageText="上一页" ShowBoxThreshold="1" OnPageChanged="pagesTrade_PageChanged" PageIndexBoxClass="inputbox" ShowPageIndexBox="Always" SubmitButtonClass="btngo" TextAfterPageIndexBox="页" TextBeforePageIndexBox="第" UrlPaging="True" NumericButtonCount="5"></webdiyer:AspNetPager></td>
    </tr>
</table>
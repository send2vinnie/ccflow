<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleBigType.ascx.cs" Inherits="Tax666.AppWeb.UserControls.ArticleBigType" %>
<%@ Import Namespace="Tax666.AppWeb" %>
<table class="tableBorder1" cellSpacing="1" cellPadding="0" align="center">
    <tr>
        <th width="36">选择</th>
        <th width="160">文章大类名称</th>
        <th width="*">文章大类描述</th>
        <th width="48">系统？</th>
    </tr>
</table>
<table class="tableBorder4" id="tabNoRec" cellSpacing="1" cellPadding="5" align="center" runat="server" style="height:32px;">
    <tr>
        <td class="TableBody1">&nbsp;&nbsp;对不起，该类别下没有满足条件的记录列表。</td>
    </tr>
</table>
<asp:datagrid id="listRecord1" runat="server" CssClass="tableBorder1" Width="100%" GridLines="None" AutoGenerateColumns="False" CellPadding="0" HorizontalAlign="Center" ShowHeader="False" CellSpacing="1">
    <Columns>
        <asp:TemplateColumn HeaderText="操作">
            <ItemStyle HorizontalAlign="Center" Width="36px" CssClass="TableBody2"></ItemStyle>
            <ItemTemplate>
                <asp:CheckBox id="chk" runat="server"></asp:CheckBox>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="大类名称">
            <ItemStyle CssClass="TableBody1" Width="160px" HorizontalAlign="center"></ItemStyle>
            <ItemTemplate>
	            <a href='<%=Global.WebPath%>/Manager/AticleManage/ArticleBigTypeEdit.aspx?bigid=<%#DataBinder.Eval(Container.DataItem,"BigTypeID")%>' title='<%#DataBinder.Eval(Container.DataItem,"BigTypeName")%>'>
		            <%# DataBinder.Eval(Container.DataItem, "BigTypeName").ToString()%>
	            </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:BoundColumn DataField="BigTypeDesc" HeaderText="文章大类描述">
            <ItemStyle CssClass="TableBody1" HorizontalAlign="center"></ItemStyle>
        </asp:BoundColumn>
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
            <asp:button id="btnDel1" runat="server" Text="设置为系统/私有" CssClass="btnbig" OnClick="btnDel1_Click"></asp:button>&nbsp;&nbsp;
            <asp:button id="btnDelete1" runat="server" Text="删除选中记录" CssClass="btnbig" OnClick="btnDelete1_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:button id="btnAddBigType" runat="server" Text="添加文章大类" CssClass="btnbig" OnClick="btnAddBigType_Click"></asp:button>
        </td>
    </tr>
</table>
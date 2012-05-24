<%@ Page Title="OA_News" Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs"
    Inherits="Lizard.OA.Web.OA_News.List" %>

<%@ Register Src="~/CCOA/Controls/MiniPager.ascx" TagPrefix="xuc" TagName="MiniPager" %>
<%@ Register Src="../Controls/MiniToolBar.ascx" TagName="MiniToolBar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server">
    <div>
        <uc1:MiniToolBar ID="MiniToolBar1" runat="server" />
        <!--Search end-->
        <br />
        <asp:GridView ID="gridView" runat="server" AllowPaging="True" Width="100%" CellPadding="3"
            OnPageIndexChanging="gridView_PageIndexChanging" BorderWidth="1px" DataKeyNames="No"
            OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false" PageSize="10"
            RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated" CssClass="lizard-grid">
            <Columns>
                <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                    <ItemTemplate>
                        <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                        <asp:HiddenField ID="DeleteNo" runat="server" Value='<%#Eval("No") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="No" HeaderText="主键Id" SortExpression="No" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:TemplateField HeaderText="新闻标题" SortExpression="NewsTitle" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <a href="Show.aspx?id=<%#Eval("No") %>" target="_blank">
                            <%# Eval("NewsTitle") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NewsSubTitle" HeaderText="副标题" SortExpression="NewsSubTitle"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="NewsType" HeaderText="新闻类型" SortExpression="NewsType"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="NewsContent" HeaderText="新闻内容" SortExpression="NewsContent"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="Author" HeaderText="发布人" SortExpression="Author" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreateTime" HeaderText="发布时间" SortExpression="CreateTime"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Clicks" HeaderText="点击量" SortExpression="Clicks" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="IsRead" HeaderText="是否阅读" SortExpression="IsRead" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="UpDT" HeaderText="更新时间" SortExpression="UpDT" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:BoundField DataField="UpUser" HeaderText="更新人" SortExpression="UpUser" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center"
                    Visible="false" />
                <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="No"
                    DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="编辑" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <xuc:XPager ID="XPager1" runat="server" OnPagerChanged="XPager1_PagerChanged" />
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyMessage.aspx.cs" Inherits="CCOA_Message_MyMessage" %>

<%@ Register Src="../Controls/MiniToolBar.ascx" TagName="MiniToolBar" TagPrefix="uc1" %>
<%@ Register Src="../Controls/MiniPager.ascx" TagName="MiniPager" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/js/CheckBox.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:MiniToolBar ID="MiniToolBar1" runat="server" />
    &nbsp;<!--Search end--><br />
    <lizard:xgridview id="gridView" runat="server" width="100%" cellpadding="3" onpageindexchanging="gridView_PageIndexChanging"
        borderwidth="1px" datakeynames="No" onrowdatabound="gridView_RowDataBound" autogeneratecolumns="False"
        rowstyle-horizontalalign="Center" onrowcreated="gridView_OnRowCreated" cssclass="lizard-grid">
        <columns>
            <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                     <asp:HiddenField ID="DeleteNo" runat="server" Value='<%#Eval("No") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="No" HeaderText="主键Id" SortExpression="No" ItemStyle-HorizontalAlign="Center" Visible="false"/>
            <asp:BoundField DataField="MessageName" HeaderText="消息标题" SortExpression="MessageName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="MeaageType" HeaderText="消息类型" SortExpression="MeaageType"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Author" HeaderText="发布人" SortExpression="Author" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="CreateTime" HeaderText="发布时间" SortExpression="CreateTime"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="UpDT" HeaderText="最后更新时间" SortExpression="UpDT" ItemStyle-HorizontalAlign="Center" Visible="false"/>
            <asp:BoundField DataField="Status" HeaderText="状态：1-有效0-无效" SortExpression="Status"
                ItemStyle-HorizontalAlign="Center" Visible="false"/>
            <asp:HyperLinkField HeaderText="详细" ControlStyle-Width="50" DataNavigateUrlFields="No"
                DataNavigateUrlFormatString="Show.aspx?id={0}" Text="详细" Visible="false"/>
            <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="No"
                DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="编辑" />
            <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </columns>
    </lizard:xgridview>
    <xuc:XPager ID="XPager1" runat="server" OnPagerChanged="XPager1_PagerChanged" />
    </form>
</body>
</html>

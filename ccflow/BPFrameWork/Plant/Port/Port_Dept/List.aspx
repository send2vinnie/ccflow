<%@ Page Title="部门" Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs"
    Inherits="BP.EIP.Web.Port_Dept.List" %>

<%@ Register Src="../../CCOA/Controls/MiniToolBar.ascx" TagName="MiniToolBar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/js/CheckBox.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <uc1:MiniToolBar ID="MiniToolBar1" runat="server" />
    <br />
    <lizard:XGridView ID="gridView" runat="server" Width="100%" CellPadding="3" OnPageIndexChanging="gridView_PageIndexChanging"
        BorderWidth="1px" DataKeyNames="No" OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="false"
        PageSize="10" RowStyle-HorizontalAlign="Center" OnRowCreated="gridView_OnRowCreated">
        <Columns>
            <asp:TemplateField ControlStyle-Width="30" HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="DeleteThis" onclick="javascript:CCA(this);" runat="server" />
                    <asp:HiddenField ID="DeleteNo" runat="server" Value='<%#Eval("No") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="No" HeaderText="No" SortExpression="No" ItemStyle-HorizontalAlign="Center"
                Visible="false" />
            <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="FullName" HeaderText="全称" SortExpression="FullName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Pid" HeaderText="上级部门" SortExpression="Pid" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
            <asp:HyperLinkField HeaderText="编辑" ControlStyle-Width="50" DataNavigateUrlFields="No"
                DataNavigateUrlFormatString="Modify.aspx?id={0}" Text="编辑" />
            <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="删除"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </lizard:XGridView>
    <xuc:XPager ID="XPager1" runat="server" OnPagerChanged="XPager1_PagerChanged" />
    </form>
</body>
</html>

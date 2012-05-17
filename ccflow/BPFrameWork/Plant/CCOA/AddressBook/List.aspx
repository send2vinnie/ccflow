<%@ Page Title="OA_AddrBook" Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs"
    Inherits="Lizard.OA.Web.OA_AddrBook.List" %>

<%@ Register Src="../Controls/MiniToolBar.ascx" TagName="MiniToolBar" TagPrefix="uc1" %>
<%@ Register Src="../Controls/MiniPager.ascx" TagName="MiniPager" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../Style/control.css" rel="stylesheet" type="text/css" />
    <link href="../Style/demo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
    <uc1:MiniToolBar ID="MiniToolBar1" runat="server" />
    &nbsp;<!--Search end--><br />
    <lizard:XGridView ID="gridView" runat="server" AllowPaging="True" Width="100%" CellPadding="3"
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
            <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="NickName" HeaderText="NickName" SortExpression="NickName"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Sex" HeaderText="性别" SortExpression="Sex" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Birthday" HeaderText="生日" SortExpression="Birthday" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Email" HeaderText="电子邮件" SortExpression="Email" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Mobile" HeaderText="手机" SortExpression="Mobile" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="QQ" HeaderText="QQ" SortExpression="QQ" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="WorkUnit" HeaderText="工作单位" SortExpression="WorkUnit"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="WorkPhone" HeaderText="工作电话" SortExpression="WorkPhone"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="WorkAddress" HeaderText="工作地址" SortExpression="WorkAddress"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="HomePhone" HeaderText="家庭电话" SortExpression="HomePhone"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="HomeAddress" HeaderText="家庭地址" SortExpression="HomeAddress"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="Grouping" HeaderText="分组" SortExpression="Grouping" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="Status" HeaderText="状态0-停用1-启用" SortExpression="Status"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:HyperLinkField HeaderText="详细" ControlStyle-Width="50" DataNavigateUrlFields="No"
                DataNavigateUrlFormatString="Show.aspx?id={0}" Text="详细" />
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
